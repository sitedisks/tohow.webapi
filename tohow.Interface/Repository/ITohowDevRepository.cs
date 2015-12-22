using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Domain.Database;
using tohow.Domain.DTO;

namespace tohow.Interface.Repository
{
    public interface ITohowDevRepository : IDisposable
    {
        #region image
        Task<tbImage> GetImageByIdAsync(Guid imageId);
        Task<IEnumerable<tbImage>> GetImagesByUserIdAsync(int userId);
        #endregion

        #region user
        Task<tbProfile> CreateNewUser(UserProfile user);

        Task CreateNewSession(UserProfileDetails userPro, string IPAddress);
        Task<tbSession> GetSessionByProfileId(long profileId);
        Task DeleteSessionByProfileId(tbSession theSession);
        Task<tbProfile> GetTbProfileByProfileId(int profileId);
        Task<tbProfile> GetTbProfileByUserId(string userId);
        Task<tbProfile> GetTbProfileByEmail(string email);
        #endregion
    }
}
