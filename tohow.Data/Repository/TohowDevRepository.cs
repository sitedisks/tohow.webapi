using System;
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

        public async Task<AspNetUser> GetAspNetUserByProfileId(int userId) {

            AspNetUser user = null;

            try { 
                //
            }
            catch (DataException dex) {
                throw new ApplicationException("Data error!", dex);
            }
            return user;
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
