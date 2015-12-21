using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Domain.DTO;

namespace tohow.Interface.Service
{
    public interface ITohowService
    {
        #region image
        Task<Image> GetImageByImageId(Guid imageId);
        Task<IList<Image>> GetImagesByUserId(int userId);
        #endregion

        #region user
        Task CreateNewUserProfile(UserProfile req);
        Task<UserProfileDetails> LoginUser(UserProfile req);
        Task<UserProfileDetails> GetUserProfileByUserId(string userId);
        Task<UserProfileDetails> GetUserProfileByProfileId(int profileId);
        Task<UserProfileDetails> GetUserProfileByEmail(string email);
        #endregion
    }
}
