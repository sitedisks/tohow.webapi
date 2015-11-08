using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Interface.Service;
using tohow.Data.Repository;
using tohow.Domain.DTO;
using tohow.Interface.Repository;

namespace tohow.Service
{
    public class TohowService: ITohowService
    {
        private readonly ITohowDevRepository _reposTohowDev;

        public TohowService(ITohowDevRepository repoTH) {
            _reposTohowDev = repoTH;
        }
        // use the repos and implement the interface function
        public Task<Image> GetImageByImageId(Guid imageId) {

            Image img = new Image();

            try {
                var imgTT =  _reposTohowDev.GetImageById(imageId);


                if (imgTT == null)
                    throw new ApplicationException("Invalid Image!");

                
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error retriving Image", ex);
            }

            return img;
        }
    }
}
