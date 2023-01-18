using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "OldPassword is required")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
