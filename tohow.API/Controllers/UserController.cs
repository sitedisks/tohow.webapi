using System;
using System.Threading.Tasks;
using System.Web.Http;
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
    }
}
