using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tohow.Domain.DTO
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public long ProfileId { get; set; }
        public DateTime Expiry { get; set; }
        public string IPAddress { get; set; }
    }
}
