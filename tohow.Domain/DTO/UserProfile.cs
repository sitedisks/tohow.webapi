using System.ComponentModel.DataAnnotations;

namespace tohow.Domain.DTO
{
    public class UserProfile
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
