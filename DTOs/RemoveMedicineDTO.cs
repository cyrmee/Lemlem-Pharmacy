using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class RemoveMedicineDTO
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime? DateReceived { get; set; }

		[Required]
		public string Invoice { get; set; } = string.Empty;
	}
}
