using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class AddCustomerNotificationDTO
	{
		[Required]
		public string PhoneNo { get; set; } = string.Empty;

		[Required]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		public int Interval { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }
	}
}
