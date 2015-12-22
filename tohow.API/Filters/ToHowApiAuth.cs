using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using tohow.Domain.Constants;
using tohow.Interface.Service;

namespace tohow.API.Filters
{
    public class ToHowAPIIdentity : IIdentity
    {
        private bool _isAuth = false;
        private string _name = string.Empty;
        private long? _profileId = null;
        private Guid? _sessionId = null;
        private AuthenticationTypes _type = AuthenticationTypes.HttpHeader;

        public string AuthenticationType
        {
            get { return ((int)_type).ToString(); }
            set
            {
                AuthenticationTypes _output = AuthenticationTypes.HttpHeader;
                Enum.TryParse<AuthenticationTypes>(value, out _output);
                _type = _output;
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return (ProfileId.HasValue && ProfileId.Value > 0 && SessionId.HasValue);
            }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public long? ProfileId
        {
            get { return _profileId; }
            set { _profileId = value; }
        }
        public Guid? SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
    }

    public class ToHowAPIUser : IPrincipal
    {
        private ToHowAPIIdentity _identity = null;

        private ToHowAPIUser() { }
        public ToHowAPIUser(ToHowAPIIdentity identity) { _identity = identity; }

        public IIdentity Identity
        {
            get { return _identity; }
            set { _identity = (ToHowAPIIdentity)value; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }


    public enum AuthenticationTypes
    {
        HttpHeader = 0,
        Cookie = 1,
        Both = 2
    }

    public class AuthenticatedRequestAttribute : AuthorizationFilterAttribute
    {
        private ITohowService _tohowSvc;

        #region named parameters
        public AuthenticationTypes AuthType = AuthenticationTypes.HttpHeader;
        public bool IsOptional = false;
        #endregion

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var requestScope = actionContext.Request.GetDependencyScope();
            _tohowSvc = requestScope.GetService(typeof(ITohowService)) as ITohowService;

            // initi default user
            HttpContext.Current.User = new ToHowAPIUser(new ToHowAPIIdentity());

            try
            {
                string token = string.Empty;

                switch (AuthType)
                {
                    case AuthenticationTypes.HttpHeader:
                        IEnumerable<string> sessionToken = new List<string>();

                        // missing token will return unauthorised
                        if (!actionContext.Request.Headers.TryGetValues(Constants.TOKEN.API_AUTH_TOKEN, out sessionToken))
                            if (IsOptional)
                                return;
                            else
                                throw new UnauthorizedAccessException("Missing authentication headers!");

                        token = sessionToken.First();
                        break;
                    case AuthenticationTypes.Cookie:
                        if (HttpContext.Current.Request.Cookies != null
                        && HttpContext.Current.Request.Cookies.Get(Constants.TOKEN.API_AUTH_COOKIE) != null)
                        {
                            token = HttpContext.Current.Request.Cookies.Get(Constants.TOKEN.API_AUTH_COOKIE).Value;
                        }
                        break;
                    case AuthenticationTypes.Both:
                        // try to get token from header first
                        IEnumerable<string> headerToken = new List<string>();

                        // missing token will return unauthorised
                        if (actionContext.Request.Headers.TryGetValues(Constants.TOKEN.API_AUTH_TOKEN, out headerToken))
                            token = headerToken.First();

                        if (!string.IsNullOrEmpty(token))
                            break;

                        // try to get token from cookie instead
                        if (HttpContext.Current.Request.Cookies != null
                       && HttpContext.Current.Request.Cookies.Get(Constants.TOKEN.API_AUTH_COOKIE) != null)
                        {
                            token = HttpContext.Current.Request.Cookies.Get(Constants.TOKEN.API_AUTH_COOKIE).Value;
                        }
                        break;
                    default:
                        throw new UnauthorizedAccessException("Error in authentication type!");
                }

                if (string.IsNullOrEmpty(token))
                {
                    if (IsOptional)
                        return;
                    else
                        throw new UnauthorizedAccessException("insufficient credential provided!");
                }

         
               
                    var session = _tohowSvc.GetSessionById(new Guid(token));

                    if (session == null)
                    {
                        if (!IsOptional)
                            throw new UnauthorizedAccessException("Invalid session");
                        else
                            return;
                    }

                    if (session.Expiry == null ||
                        session.Expiry < DateTime.UtcNow)
                    {
                        _tohowSvc.DeleteSession(session);

                        if (!IsOptional)
                            throw new UnauthorizedAccessException("Expired session");
                        else
                            return;
                    }

                    _tohowSvc.UpdateSessionByOneDay(session);

                    //HttpContext.Current.User = new ToHowAPIUser(new ToHowAPIIdentity { Name = session.t.DisplayName, SessionId = session, ProfileId = session.ProfileId });
                
            }
            catch (UnauthorizedAccessException uex)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);
                if (actionContext.Response.Headers.Contains(Constants.TOKEN.API_AUTH_TOKEN))
                    actionContext.Response.Headers.Remove(Constants.TOKEN.API_AUTH_TOKEN);

            }
            catch (Exception ex)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.InternalServerError);
                if (actionContext.Response.Headers.Contains(Constants.TOKEN.API_AUTH_TOKEN))
                    actionContext.Response.Headers.Remove(Constants.TOKEN.API_AUTH_TOKEN);
            }
        }
    }
}