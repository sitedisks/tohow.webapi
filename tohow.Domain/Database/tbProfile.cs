using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("tbProfile")]
    public class tbProfile
    {
        [Key]
        [Column("Id")]
        public long ProfileId { get; set; }
        public string UserId { get; set; }
        [Column("DisplayName")]
        public string Email { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Nullable<DateTime> UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public int Points { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<tbSession> tbSessions { get; set; }
    }
}
