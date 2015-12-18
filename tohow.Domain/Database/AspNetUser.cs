using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("AspNetUsers")]
    public class AspNetUser
    {
        [Key]
        [Column("Id")]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }
}
