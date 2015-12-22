using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Web;
using System.ServiceModel.Channels;
using tohow.Domain.DTO;
using tohow.Interface.Service;

namespace tohow.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly ITohowService _tohowSvc;

        public UserController(ITohowService tohowSvc)
        {
            _tohowSvc = tohowSvc;
        }

        [HttpPost, Route("register")]
        public async Task<IHttpActionResult> Register([FromBody] UserProfile req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserProfileDetails userPro = null;

            try
            {
                var existUser = await _tohowSvc.GetUserProfileByEmail(req.Email);
                if (existUser != null)
                    throw new ApplicationException("User Exist.");

                userPro = await _tohowSvc.CreateNewUserProfile(req);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(userPro);
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] UserProfile req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserProfileDetails userPro = null;

            try
            {
                userPro = await _tohowSvc.LoginUser(req, GetClientIp());
                if (userPro == null)
                    return NotFound();
            }
            catch (ApplicationException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(userPro);
        }

        [HttpPut, Route("userprofile/update")]
        public async Task<IHttpActionResult> UpdateProfile([FromBody] UserProfileDetails req, [FromUri] int profileId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // use for update the profile page

            return Ok();
        }

        [HttpGet, Route("userprofile/byuser")]
        public async Task<IHttpActionResult> GetUerProfileByUserId(string userId)
        {
            UserProfileDetails userProfile = null;

            try
            {
                userProfile = await _tohowSvc.GetUserProfileByUserId(userId);
                if (userProfile == null)
                    return NotFound();
            }
            catch (ApplicationException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(userProfile);
        }

        [HttpGet, Route("userprofile/byprofile")]
        public async Task<IHttpActionResult> GetUserProfileByProfileId(int profileId)
        {
            UserProfileDetails userProfile = null;

            try
            {
                userProfile = await _tohowSvc.GetUserProfileByProfileId(profileId);
                if (userProfile == null)
                    return NotFound();
            }
            catch (ApplicationException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(userProfile);
        }

        [HttpGet, Route("userprofile/byemail")]
        public async Task<IHttpActionResult> GetUserProfileByEmail(string email)
        {
            UserProfileDetails userProfile = null;

            try
            {
                userProfile = await _tohowSvc.GetUserProfileByEmail(email);
                if (userProfile == null)
                    return NotFound();
            }
            catch (ApplicationException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(userProfile);
        }

        #region private functions
        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
