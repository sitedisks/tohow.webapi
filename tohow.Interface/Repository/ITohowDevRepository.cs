using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Domain.Database;

namespace tohow.Interface.Repository
{
    public interface ITohowDevRepository : IDisposable
    {
        #region image
        Task<tbImage> GetImageByIdAsync(Guid imageId);
        Task<IEnumerable<tbImage>> GetImagesByUserIdAsync(int userId);
        #endregion

        #region user
        Task<AspNetUser> GetAspNetUserByProfileId(int profileId);
        Task<AspNetUser> GetAspNetUserByUserId(string userId);
        Task<tbProfile> GetUserProfileByProfileId(int profileId);
        Task<tbProfile> GetUserProfileByUserId(string userId);
        #endregion
    }
}
