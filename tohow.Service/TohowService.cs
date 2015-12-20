using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Interface.Service;
using tohow.Domain.DTO;
using tohow.Interface.Repository;
using tohow.Domain.Extensions;
using tohow.Domain.Database;
using tohow.Domain.Enum;


namespace tohow.Service
{
    public class TohowService: ITohowService
    {
        private readonly ITohowDevRepository _reposTohowDev;

        public TohowService(ITohowDevRepository repoTH) {
            _reposTohowDev = repoTH;
        }

        #region image
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
        #endregion

        #region user
        public async Task CreateNewUserProfile(UserProfile req)
        {
            try {
                await _reposTohowDev.CreateNewUser(req);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error register new user", ex);
            }
        }

        public async Task<UserProfileDetails> GetUserProfileByUserId(string userId) {
            UserProfileDetails userProfile = new UserProfileDetails();

            try {
                var user = await _reposTohowDev.GetAspNetUserByUserId(userId);
                var profile = await _reposTohowDev.GetTbProfileByUserId(userId);

                userProfile = ConvertDBUserToUserProfile(profile, user);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Error retriving User Profile", ex);
            }

            return userProfile;
        }

        public async Task<UserProfileDetails> GetUserProfileByProfileId(int profileId) {
            UserProfileDetails userPro = new UserProfileDetails();

            try {
                var user = await _reposTohowDev.GetAspNetUserByProfileId(profileId);
                var profile = await _reposTohowDev.GetTbProfileByProfileId(profileId);

                userPro = ConvertDBUserToUserProfile(profile, user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving User Profile", ex);
            }

            return userPro;
        }

        public async Task<UserProfileDetails> GetUserProfileByEmail(string email) {
            UserProfileDetails userPro = new UserProfileDetails();

            try {
                var user = await _reposTohowDev.GetAspNetUserByEmail(email);
                var profile = await _reposTohowDev.GetTbProfileByEmail(email);

                if (user == null && profile == null)
                    return null;

                userPro = ConvertDBUserToUserProfile(profile, user);
            }
            catch (Exception ex) {
                throw new ApplicationException("Error retriving User Profile", ex);            
            }

            return userPro;
        }
        #endregion

        #region private function
        private UserProfileDetails ConvertDBUserToUserProfile(tbProfile tbProfile, AspNetUser aspUser)
        {
            UserProfileDetails userProfile = new UserProfileDetails();

            userProfile.UserId = Guid.Parse(tbProfile.UserId);
            userProfile.ProfileId = tbProfile.ProfileId;
            userProfile.Email = tbProfile.Email;
            userProfile.UserName = aspUser.UserName;
            userProfile.Sex = (Gender)Enum.Parse(typeof(Gender), tbProfile.Gender);
            userProfile.Age = tbProfile.Age;
            userProfile.CreatedTime = tbProfile.CreateDateTime;
            userProfile.UpdatedTime = tbProfile.UpdatedDateTime;
            userProfile.IsDeleted = tbProfile.IsDeleted;
            userProfile.Credits = tbProfile.Points;

            return userProfile;
        }
        #endregion
    }
}
