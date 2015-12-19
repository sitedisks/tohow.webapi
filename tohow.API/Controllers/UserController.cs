using System;
using System.Threading.Tasks;
using System.Web.Http;
using tohow.Domain.DTO;
using tohow.Domain.DTO.ViewModel;
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
        public async Task<IHttpActionResult> Register([FromBody] RegisterPostRequest req) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

        [HttpGet, Route("user")]
        public async Task<IHttpActionResult> GetUerProfileByUserId(string userId) {
            UserProfile userProfile = null;

            try {
                userProfile = await _tohowSvc.GetUserProfileByUserId(userId);
            }
            catch (ApplicationException aex) {
                return BadRequest(aex.Message);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }

            return Ok(userProfile);
        }

        [HttpGet, Route("profile")]
        public async Task<IHttpActionResult> GetUserProfileByProfileId(int profileId) {
            UserProfile userProfile = null;

            try {
                userProfile = await _tohowSvc.GetUserProfileByProfileId(profileId);
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
