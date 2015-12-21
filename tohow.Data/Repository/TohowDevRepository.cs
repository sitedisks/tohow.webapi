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
    public class TohowDevRepository: ITohowDevRepository
    {
        private readonly ITohowDevDbContext _db;

        public TohowDevRepository(ITohowDevDbContext db) {
            _db = db;
        }

        #region image
        public async Task<tbImage> GetImageByIdAsync(Guid imageId) {
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
            try {
                imgList = await _db.tblImages.Where(x => x.userId == userId && !x.IsDeleted).ToListAsync();
            }
            catch (DataException dex) {
                throw new ApplicationException("Data error!", dex);
            }
            return imgList;
        }
        #endregion

        #region user
        public async Task<UserProfileDetails> CreateNewUser(UserProfile user)
        {
            UserProfileDetails userProfile = new UserProfileDetails();

            try {
                userProfile.UserId = Guid.NewGuid();
                userProfile.Email = user.Email;
                userProfile.Password = user.Password;
                userProfile.CreatedTime = DateTime.UtcNow;
                userProfile.IsDeleted = false;
                userProfile.Credits = 0;

                var profile = userProfile.ConvertToTbProfile();
            
                _db.tbProfiles.Add(profile);
                await _db.SaveChangesAsync();
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }

            return userProfile;
        }

        public async Task<tbSession> CreateNewSession(Session session) {
            tbSession ses = new tbSession();

            try { }
            catch (DataException dex)
            { 
            }

            return ses;
        }

        public async Task<tbProfile> GetTbProfileByProfileId(int profileId) {
            tbProfile profile = null;
            try {
                profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.ProfileId == profileId && !x.IsDeleted);
                if (profile != null)
                    profile.AspNetUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == profile.UserId);
            }
            catch (DataException dex) {
                throw new ApplicationException("Data error!", dex);
            }
            return profile;
        }

        public async Task<tbProfile> GetTbProfileByUserId(string userId) {
            tbProfile profile = null;
            try {
                profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
                if(profile!=null)
                    profile.AspNetUser = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return profile;
        }

        public async Task<tbProfile> GetTbProfileByEmail(string email) {
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
