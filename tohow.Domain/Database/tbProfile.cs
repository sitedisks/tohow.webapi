using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tohow.Domain.Database
{
    public class tbProfile
    {
        [Key]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
    }
}
