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
        Task<tblImage> GetImageById(Guid imageId);
        Task<IEnumerable<tblImage>> GetImagesByUserId(int userId);
    }
}
