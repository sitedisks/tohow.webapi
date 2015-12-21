using CryptSharp;
using tohow.Domain.Database;
using tohow.Domain.DTO;
using tohow.Domain.Enum;

namespace tohow.Domain.Extensions
{
    public static class DTOConverter
    {
        public static tbImage ConvertToTbImage(this Image source, tbImage data = null)
        {
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

        public static Image ConverToImageDTO(this tbImage source, Image data = null)
        {
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

        public static tbProfile ConvertToTbProfile(this UserProfileDetails source, tbProfile data = null)
        {
            if (data == null)
                data = new tbProfile();

            if (source == null)
                return null;

            data.ProfileId = source.ProfileId;
            data.AspNetUser = new AspNetUser();
            data.AspNetUser.UserId = source.UserId.ToString();
            data.AspNetUser.Email = source.Email;
            data.AspNetUser.PasswordHash = Crypter.Blowfish.Crypt(source.Password);
            data.AspNetUser.UserName = source.UserName != null ? source.UserName : source.Email;
            data.Email = source.Email;
            data.Gender = source.Sex.ToString();
            data.Age = source.Age;
            data.CreateDateTime = source.CreatedTime;
            data.UpdatedDateTime = source.UpdatedTime;
            data.IsDeleted = source.IsDeleted;
            data.Points = source.Credits;

            return data;
        }

        public static UserProfileDetails ConvertToUserProfile(this tbProfile source, UserProfileDetails data = null)
        {
            if (data == null)
                data = new UserProfileDetails();

            if (source == null)
                return null;

            data.ProfileId = source.ProfileId;
            data.UserId = new System.Guid(source.UserId);
            data.Email = source.Email;
            data.Sex = (Gender)System.Enum.Parse(typeof(Gender), source.Gender);
            data.UserName = source.AspNetUser.UserName;
            data.Age = source.Age;
            data.CreatedTime = source.CreateDateTime;
            data.UpdatedTime = source.UpdatedDateTime;
            data.IsDeleted = source.IsDeleted;
            data.Credits = source.Points;

            return data;
        }

        // Session converter
        public static tbSession ConverToTbSession(this Session source, tbSession data = null)
        {
            if (data == null)
                data = new Database.tbSession();

            if (source == null)
                return null;

            data.Id = source.SessionId;
            data.ProfileId = source.ProfileId;
            data.Expiry = source.Expiry;
            data.IPAddress = source.IPAddress;

            return data;
        }
    }
}
