﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using tohow.Interface.DbContext;
using tohow.Interface.Repository;
using tohow.Domain.Database;
using System.Data;

namespace tohow.Data.Repository
{
    public class TohowDevRepository: ITohowDevRepository
    {
        private readonly ITohowDevDbContext _db;

        public TohowDevRepository(ITohowDevDbContext db) {
            _db = db;
        }

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

        public async Task<AspNetUser> GetAspNetUserByProfileId(int profileId) {

            AspNetUser user = null;

            try {
                var profile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.ProfileId == profileId && !x.IsDeleted);
                user = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == profile.UserId);
            }
            catch (DataException dex) {
                throw new ApplicationException("Data error!", dex);
            }
            return user;
        }

        public async Task<tbProfile> GetUserProfileByProfileId(int profileId) {
            tbProfile userProfile = null;
            try {
                userProfile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.ProfileId == profileId && !x.IsDeleted);
            }
            catch (DataException dex) {
                throw new ApplicationException("Data error!", dex);
            }
            return userProfile;
        }

        public async Task<AspNetUser> GetAspNetUserByUserId(string userId) {
            AspNetUser user = null;
            try {
                user = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return user;
        }

        public async Task<tbProfile> GetUserProfileByUserId(string userId) {
            tbProfile userProfile = null;
            try {
                var user = await _db.AspNetUsers.FirstOrDefaultAsync(x => x.UserId == userId);
                userProfile = await _db.tbProfiles.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            }
            catch (DataException dex)
            {
                throw new ApplicationException("Data error!", dex);
            }
            return userProfile;
        }

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
