using System;
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
        public int Age { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public int Points { get; set; }

        [ForeignKey("UserId")]
        public AspNetUser AspNetUser { get; set; }

    }
}
