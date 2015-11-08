using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tohow.Domain.DTO
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string URL { get; set; }
    }
}
