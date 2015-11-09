using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Domain.Database;

namespace tohow.Interface.Repository
{
    public interface ITohowDevRepository: IDisposable
    {
        Task<tblImage> GetImageByIdAsync(Guid imageId);
        Task<IEnumerable<tblImage>> GetImagesByUserIdAsync(int userId);
    }
}
