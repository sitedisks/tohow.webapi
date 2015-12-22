using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using tohow.Interface.DbContext;
using tohow.Interface.Repository;
using tohow.Domain.Database;
using System.Data;
using tohow.Domain.Extensions;
using tohow.Domain.DTO;

namespace tohow.Data.Repository
{
    public class TohowDevRepository : ITohowDevRepository
    {
        private readonly ITohowDevDbContext _db;

        public TohowDevRepository(ITohowDevDbContext db)
        {
            _db = db;
        }

        #region image
        public async Task<tbImage> GetImageByIdAsync(Guid imageId)
        {
            tbImage img = null;
            try
            {
                img = await _db.tblImages.FirstOrDefaultAsync(x => x.Id == imageId && !x.IsDeleted);
            }
            catch (DataException dex)
            {
                //Logger.Error(dex);
                throw new ApplicationException("Data error!", dex);
            }

            return img;
        }

        public async Task<IEnumerable<tbImage>> GetImagesByUserIdAsync(int userId)
        {
            IList<tbImage> imgList = new List<tbImage>();
            try
            {
                imgList = await _db.tblImages.Where(x => x.userId == userId && !x.IsDeleted).ToListAsync();
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return imgList;
        }
        #endregion

        #region user
        public async Task<tbProfile> CreateNewUser(UserProfile user)
        {
            tbProfile profile = new tbProfile();

            try
            {
                UserProfileDetails userProfile = new UserProfileDetails();
                userProfile.UserId = Guid.NewGuid();
                userProfile.Email = user.Email;
                userProfile.Password = user.Password;
                userProfile.CreatedTime = DateTime.UtcNow;
                userProfile.IsDeleted = false;
                userProfile.Credits = 0;

                profile = userProfile.ConvertToTbProfile();

                _db.tbProfiles.Add(profile);
                await _db.SaveChangesAsync();
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }

            return profile;
        }

        public async Task CreateNewSession(UserProfileDetails userPro, string IPAddress)
        {
            try
            {
                tbSession ses = new tbSession();
                ses.Id = Guid.NewGuid();
                ses.CreateDateTime = DateTime.UtcNow;
                ses.Expiry = DateTime.UtcNow.AddMonths(1);
                ses.ProfileId = userPro.ProfileId;
                ses.IPAddress = IPAddress;

                _db.tbSessions.Add(ses);
                await _db.SaveChangesAsync();
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
        }

        public async Task<tbSession> GetSessionByProfileId(long profileId)
        {
            tbSession ses = null;

            try
            {
                ses = await _db.tbSessions.FirstOrDefaultAsync(x => x.ProfileId == profileId && !x.IsDeleted);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }

            return ses;
        }

        public async Task DeleteSession(tbSession theSession)
        {
            try
            {
                theSession.UpdateDateTime = DateTime.UtcNow;
                theSession.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
        }

        public async Task<tbProfile> GetTbProfileByProfileId(int profileId)
        {
            tbProfile profile = null;
            try
            {
                profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.ProfileId == profileId && !x.IsDeleted);
                if (profile != null)
                    profile.AspNetUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == profile.UserId);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return profile;
        }

        public async Task<tbProfile> GetTbProfileByUserId(string userId)
        {
            tbProfile profile = null;
            try
            {
                profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
                if (profile != null)
                    profile.AspNetUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return profile;
        }

        public async Task<tbProfile> GetTbProfileByEmail(string email)
        {
            tbProfile profile = null;
            try
            {
                profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
                if (profile != null)
                    profile.AspNetUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return profile;
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                    _db.Dispose();
            }
        }
        #endregion
    }
}
