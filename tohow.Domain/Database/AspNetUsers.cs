using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    [Table("AspNetUsers")]
    public class AspNetUsers
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }
}
