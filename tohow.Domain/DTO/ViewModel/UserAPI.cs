using System.ComponentModel.DataAnnotations;

namespace tohow.Domain.DTO.ViewModel
{
    public enum Gender
    {
        NONE = 0,
        MALE = 1,
        FEMALE = 2
    }

    public class RegisterPostRequest
    {
        [Required]
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public Gender Sex { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
