using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("tbSession")]
    public class tbSession
    {
        [Key]
        public Guid Id { get; set; }
        public long ProfileId { get; set; }
        public DateTime Expiry { get; set; }
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Nullable<DateTime> UpdateDateTime { get; set; }

        [ForeignKey("ProfileId")]
        public virtual tbProfile tbProfile { get; set; }
    }
}
