using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
    public class LogInDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
