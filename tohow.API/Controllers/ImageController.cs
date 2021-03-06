﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using tohow.Domain.DTO;
using tohow.Interface.Service;

namespace tohow.API.Controllers
{
    [RoutePrefix("image")]
    public class ImageController : ApiController
    {
        private readonly ITohowService _tohowSvc;

        public ImageController(ITohowService tohowSvc) {
            _tohowSvc = tohowSvc;
        }

        [Route("{imageId:Guid}"), HttpGet]
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

        [Route("{userId}"), HttpGet]
        public async Task<IHttpActionResult> GetImagesByUserId(int userId) {
            IList<Image> imgList = new List<Image>();

            try {
                imgList = await _tohowSvc.GetImagesByUserId(userId);
                if (imgList == null)
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

            return Ok(imgList);
        }
    }
}
