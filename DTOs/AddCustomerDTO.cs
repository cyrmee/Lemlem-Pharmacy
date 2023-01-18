using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LemlemPharmacy.DTOs
{
	public class AddCustomerDTO
	{
		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		[RegexStringValidator(@"/(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))/")]
		public string PhoneNo { get; set; } = string.Empty;
	}
}
