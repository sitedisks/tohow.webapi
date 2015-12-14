using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tohow.Domain.Database;
using tohow.Domain.DTO;

namespace tohow.Domain.Extensions
{
    public static class DTOConverter
    {
        public static tbImage ConvertToTblImage(this Image source, tbImage data = null) {
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
    }
}
