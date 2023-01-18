using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LemlemPharmacy.Models
{
	public class CustomerNotification
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

		public Medicine? Medicine { get; set; }
		public Customer? Customer { get; set; }
	}
}
