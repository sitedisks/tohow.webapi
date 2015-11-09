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
        public static tblImage ConvertToTblImage(this Image source, tblImage data = null) {
            if (data == null)
                data = new Database.tblImage();

            if (source == null)
                return null;

            data.Id = source.ImageId;
            data.userId = source.UserId;
            data.IsDeleted = source.IsDeleted;
            data.Uri = source.URL;

            return data;
        }

        public static Image ConverToImageDTO(this tblImage source, Image data = null) {
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
