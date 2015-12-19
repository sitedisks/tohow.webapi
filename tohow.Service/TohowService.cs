using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Interface.Service;
using tohow.Domain.DTO;
using tohow.Interface.Repository;
using tohow.Domain.Extensions;
using tohow.Domain.DTO.ViewModel;
using tohow.Domain.Database;

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

        //public async Task Register(RegisterPostRequest req) {

        //    try { }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Error register new user", ex);
        //    }
        //}

        public async Task<UserProfile> GetUserProfileByUserId(string userId) {
            UserProfile userProfile = new UserProfile();

            try {
                var user = await _reposTohowDev.GetAspNetUserByUserId(userId);
                var profile = await _reposTohowDev.GetTbProfileByUserId(userId);

                userProfile = ConvertDBUserToUserProfile(profile, user);
            }
            catch (Exception ex) { 
            }

            return userProfile;
        }

        private UserProfile ConvertDBUserToUserProfile(tbProfile tbProfile, AspNetUser aspUser)
        {
            UserProfile userProfile = new UserProfile();

            userProfile.UserId = Guid.Parse(tbProfile.UserId);
            userProfile.ProfileId = tbProfile.ProfileId;
            userProfile.Email = tbProfile.Email;
            userProfile.UserName = aspUser.UserName;
            userProfile.Gender = tbProfile.Gender;
            userProfile.Age = tbProfile.Age;
            userProfile.CreatedTime = tbProfile.CreateDateTime;
            userProfile.UpdatedTime = tbProfile.UpdatedDateTime;
            userProfile.IsDeleted = tbProfile.IsDeleted;
            userProfile.Credits = tbProfile.Points;

            return userProfile;
        }
    }
}
