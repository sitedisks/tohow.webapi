﻿using System;
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
        Task<UserProfileDetails> CreateNewUser(UserProfile user);

        Task<tbSession> CreateNewSession(Session session);
        Task<tbProfile> GetTbProfileByProfileId(int profileId);
        Task<tbProfile> GetTbProfileByUserId(string userId);
        Task<tbProfile> GetTbProfileByEmail(string email);
        #endregion
    }
}
