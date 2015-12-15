using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Interface.Service;
using tohow.Data.Repository;
using tohow.Domain.DTO;
using tohow.Interface.Repository;
using tohow.Domain.Extensions;

namespace tohow.Service
{
    public class TohowService: ITohowService
    {
        private readonly ITohowDevRepository _reposTohowDev;

        public TohowService(ITohowDevRepository repoTH) {
            _reposTohowDev = repoTH;
        }
        // use the repos and implement the interface function
        public async Task<Image> GetImageByImageId(Guid imageId) {

            Image img = new Image();

            try {
                var tblImg = await _reposTohowDev.GetImageByIdAsync(imageId);

                if (tblImg == null)
                    throw new ApplicationException("Invalid Image");

                img = tblImg.ConverToImageDTO(); 
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error retriving Image", ex);
            }

            return img;
        }

        public async Task<IList<Image>> GetImagesByUserId(int userId) {
            IList<Image> imgList = new List<Image>();

            try
            {
                var ImgList = await _reposTohowDev.GetImagesByUserIdAsync(userId);

                if (ImgList == null)
                    throw new ApplicationException("Invalid User");

                foreach (var item in ImgList) {
                    imgList.Add(item.ConverToImageDTO());
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving Images", ex);
            }

            return imgList;
        }
    }
}
