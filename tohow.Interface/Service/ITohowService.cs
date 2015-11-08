using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Domain.DTO;

namespace tohow.Interface.Service
{
    public interface ITohowService
    {
        Task<Image> GetImageByImageId(Guid imageId);
    }
}
