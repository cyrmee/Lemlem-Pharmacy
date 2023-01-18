using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class CardDateRangeDTO
	{
		[Required]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime EndDate { get; set; }
	}
}
