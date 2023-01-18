using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class CustomerNotificationDTO
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

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

		public CustomerNotificationDTO()
		{

		}

		public CustomerNotificationDTO(Guid id, string phoneNo, string batchNo, int interval, DateTime endDate, DateTime nextDate)
		{
			Id = id;
			PhoneNo = phoneNo;
			BatchNo = batchNo;
			Interval = interval;
			EndDate = endDate;
			NextDate = nextDate;
		}

		public CustomerNotificationDTO(CustomerNotification customerNotification)
		{
			Id = customerNotification.Id;
			PhoneNo = customerNotification.PhoneNo;
			BatchNo = customerNotification.BatchNo;
			Interval = customerNotification.Interval;
			EndDate = customerNotification.EndDate;
			NextDate = customerNotification.NextDate;
		}
	}
}
