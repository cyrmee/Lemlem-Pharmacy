using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
    public class UserInfoDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
		public string PhoneNo { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = default!;

        public UserInfoDTO()
        {

        }

        public UserInfoDTO(string userName, string name, string phoneNo, string email)
        {
            UserName = userName;
            Name = name;
            PhoneNo = phoneNo;
            Email = email;
        }

		public UserInfoDTO(ApplicationUser applicationUser)
		{
			UserName = applicationUser.UserName;
			Name = applicationUser.NormalizedUserName;
			PhoneNo = applicationUser.PhoneNumber;
			Email = applicationUser.Email;
		}

		public UserInfoDTO(ApplicationUser applicationUser, string role)
		{
			UserName = applicationUser.UserName;
			Name = applicationUser.NormalizedUserName;
			PhoneNo = applicationUser.PhoneNumber;
			Email = applicationUser.Email;
            Role = role;
		}
	}
}
