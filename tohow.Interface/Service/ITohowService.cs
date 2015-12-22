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
        Session GetSessionById(Guid sessionId);
        void DeleteSession(Session session);
        void UpdateSessionByOneDay(Session session);
        Task LogoutUser(Guid sessionId);
        Task<UserProfileDetails> CreateNewUserProfile(UserProfile req);
        Task<UserProfileDetails> LoginUser(UserProfile req, string IPAddress);
        Task<UserProfileDetails> GetUserProfileByUserId(string userId);
        Task<UserProfileDetails> GetUserProfileByProfileId(int profileId);
        Task<UserProfileDetails> GetUserProfileByEmail(string email);
        #endregion
    }
}
