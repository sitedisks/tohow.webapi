﻿using System;
using CryptSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using tohow.Interface.Service;
using tohow.Domain.DTO;
using tohow.Interface.Repository;
using tohow.Domain.Extensions;


namespace tohow.Service
{
    public class TohowService : ITohowService
    {
        private readonly ITohowDevRepository _reposTohowDev;

        public TohowService(ITohowDevRepository repoTH)
        {
            _reposTohowDev = repoTH;
        }

        #region image
        public async Task<Image> GetImageByImageId(Guid imageId)
        {

            Image img = new Image();

            try
            {
                var tblImg = await _reposTohowDev.GetImageByIdAsync(imageId);

                if (tblImg == null)
                    throw new ApplicationException("Invalid Image");

                img = tblImg.ConverToImageDTO();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving Image", ex);
            }

            return img;
        }

        public async Task<IList<Image>> GetImagesByUserId(int userId)
        {
            IList<Image> imgList = new List<Image>();

            try
            {
                var ImgList = await _reposTohowDev.GetImagesByUserIdAsync(userId);

                if (ImgList == null)
                    throw new ApplicationException("Invalid User");

                foreach (var item in ImgList)
                {
                    imgList.Add(item.ConverToImageDTO());
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving Images", ex);
            }

            return imgList;
        }
        #endregion

        #region user
        public Session GetSessionById(Guid sessionId) {
            Session session = null;

            try {
                var tbsession = _reposTohowDev.GetSessionById(sessionId);
                session = tbsession.ConverToSessionDTO();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieve user session", ex);
            }
            return session;
        }

        public void DeleteSession(Session session) {
            try {
                _reposTohowDev.DeleteSession(session.ConverToTbSession());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error delete user session", ex);
            }
        }

        public void UpdateSessionByOneDay(Session session) {
            try {
                _reposTohowDev.UpdateSession(session.ConverToTbSession());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error update user session", ex);
            }
        }

        public async Task LogoutUser(Guid sessionId) {
            try {
                var session = await _reposTohowDev.GetSessionByIdAsync(sessionId);
                if (session != null)
                {
                    await _reposTohowDev.DeleteSessionAsync(session);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error logout user", ex);
            }
        }

        public async Task<UserProfileDetails> CreateNewUserProfile(UserProfile req)
        {
            UserProfileDetails userPro = new UserProfileDetails();

            try
            {
                var user = await _reposTohowDev.CreateNewUser(req);
                userPro = user.ConvertToUserProfile();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error register new user", ex);
            }

            return userPro;
        }

        public async Task<UserProfileDetails> LoginUser(UserProfile req, string IPAddress)
        {
            UserProfileDetails userPro = null;

            try
            {
                var profile = await _reposTohowDev.GetTbProfileByEmail(req.Email);
                if (profile != null)
                {
                    if (Crypter.CheckPassword(req.Password, profile.AspNetUser.PasswordHash))
                    {
                        userPro = profile.ConvertToUserProfile();
                        var session = await _reposTohowDev.GetSessionByProfileId(userPro.ProfileId);
                        if (session != null)
                        {
                            await _reposTohowDev.DeleteSessionAsync(session); //delete the previous session
                        }

                        await _reposTohowDev.CreateNewSession(userPro, IPAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error login user", ex);
            }

            return userPro;
        }

        public async Task<UserProfileDetails> GetUserProfileByUserId(string userId)
        {
            UserProfileDetails userPro = new UserProfileDetails();

            try
            {
                var profile = await _reposTohowDev.GetTbProfileByUserId(userId);

                if (profile != null)
                    userPro = profile.ConvertToUserProfile();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving User Profile", ex);
            }

            return userPro;
        }

        public async Task<UserProfileDetails> GetUserProfileByProfileId(int profileId)
        {
            UserProfileDetails userPro = new UserProfileDetails();

            try
            {
                var profile = await _reposTohowDev.GetTbProfileByProfileId(profileId);

                if (profile != null)
                    userPro = profile.ConvertToUserProfile();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving User Profile", ex);
            }

            return userPro;
        }

        public async Task<UserProfileDetails> GetUserProfileByEmail(string email)
        {
            UserProfileDetails userPro = new UserProfileDetails();

            try
            {
                var profile = await _reposTohowDev.GetTbProfileByEmail(email);

                if (profile != null)
                    userPro = profile.ConvertToUserProfile();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retriving User Profile", ex);
            }

            return userPro;
        }
        #endregion
    }
}
