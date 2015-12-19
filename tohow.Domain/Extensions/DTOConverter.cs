using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Domain.Database;
using tohow.Domain.DTO;
using CryptSharp;

namespace tohow.Domain.Extensions
{
    public static class DTOConverter
    {
        public static tbImage ConvertToTbImage(this Image source, tbImage data = null) {
            if (data == null)
                data = new Database.tbImage();

            if (source == null)
                return null;

            data.Id = source.ImageId;
            data.userId = source.UserId;
            data.IsDeleted = source.IsDeleted;
            data.Uri = source.URL;

            return data;
        }

        public static Image ConverToImageDTO(this tbImage source, Image data = null) {
            if (data == null)
                data = new Image();

            if (source == null)
                return null;

            data.ImageId = source.Id;
            data.UserId = source.userId;
            data.IsDeleted = source.IsDeleted;
            data.URL = source.Uri;

            return data;
        }

        public static AspNetUser ConvertToAspNetUser(this UserProfile source, AspNetUser data = null) {
            if (data == null)
                data = new AspNetUser();

            if (source == null)
                return null;

            data.UserId = source.UserId.ToString();
            data.Email = source.Email;
            data.PasswordHash = Crypter.Blowfish.Crypt(source.Password);
            data.UserName = source.UserName;

            return data;
        }

        public static tbProfile ConvertToTbProfile(this UserProfile source, tbProfile data = null) { 
            if(data == null)
                data = new tbProfile();

            if(source == null) 
                return null;

            data.ProfileId = source.ProfileId;
            data.UserId = source.UserId.ToString();
            data.Email = source.Email;
            data.Gender = source.Gender;
            data.Age = source.Age;
            data.CreateDateTime = source.CreatedTime;
            data.UpdatedDateTime = source.UpdatedTime;
            data.IsDeleted = source.IsDeleted;
            data.Points = source.Credits;

            return data;
        }
    }
}
