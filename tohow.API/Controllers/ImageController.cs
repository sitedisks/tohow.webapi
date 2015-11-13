using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using tohow.Domain.DTO;
using tohow.Interface.Service;

namespace tohow.API.Controllers
{
    public class ImageController : ApiController
    {
        private readonly ITohowService _tohowSvc;

        public ImageController(ITohowService tohowSvc) {
            _tohowSvc = tohowSvc;
        }

        [Route("image/{imageId:Guid}"), HttpGet]
        public async Task<IHttpActionResult> GetImageByImageId(Guid imageId)
        {
            Image img = new Image();

            try
            {
                img = await _tohowSvc.GetImageByImageId(imageId);
                if (img == null)
                    return NotFound();
            }
            catch (ApplicationException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex) {
                return InternalServerError(ex);
            }

            return Ok(img);
        }
    }
}
