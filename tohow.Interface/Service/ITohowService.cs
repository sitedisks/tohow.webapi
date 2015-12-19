using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Domain.DTO;
using tohow.Domain.DTO.ViewModel;

namespace tohow.Interface.Service
{
    public interface ITohowService
    {
        Task<Image> GetImageByImageId(Guid imageId);
        Task<IList<Image>> GetImagesByUserId(int userId);

        //Task Reigster(RegisterPostRequest req);

        Task<UserProfile> GetUserProfileByUserId(string userId);
    }
}
