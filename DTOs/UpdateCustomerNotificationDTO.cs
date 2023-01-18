using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class UpdateCustomerNotificationDTO
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

		[Required]
		[DataType(DataType.Date)]
		public DateTime NextDate { get; set; }

		public UpdateCustomerNotificationDTO(string phoneNo, string batchNo, int interval, DateTime endDate, DateTime nextDate)
		{
			PhoneNo = phoneNo;
			BatchNo = batchNo;
			Interval = interval;
			EndDate = endDate;
			NextDate = nextDate;
		}
	}
}
