using System;
using System.Threading.Tasks;
using System.Web.Http;
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

            try
            {
                var existUser = await _tohowSvc.GetUserProfileByEmail(req.Email);
                if (existUser != null)
                    throw new ApplicationException("User Exist.");

                await _tohowSvc.CreateNewUserProfile(req);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

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
    }
}
