using Microsoft.Build.Framework;

namespace LemlemPharmacy.DTOs
{
	public class DateRangeDTO
	{
		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}
