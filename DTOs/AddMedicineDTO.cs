using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class AddMedicineDTO
	{
		[Required]
		[StringLength(100, ErrorMessage = "BatchNo character length cannot exceed 100!")]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public DateTime ExpireDate { get; set; }

		[Required]
		[StringLength(64, ErrorMessage = "Unit character length cannot exceed 64!")]
		public string Unit { get; set; } = string.Empty;

		[Required]
		public int Quantity { get; set; }

		[Required]
		public float Price { get; set; }

		[Required]
		[StringLength(1024, ErrorMessage = "Description character length cannot exceed 1024!")]
		public string Description { get; set; } = string.Empty;

		[Required]
		[StringLength(64, ErrorMessage = "Category character length cannot exceed 64!")]
		public string Category { get; set; } = string.Empty;

		[Required]
		[StringLength(64, ErrorMessage = "Type character length cannot exceed 64!")]
		public string Type { get; set; } = string.Empty;

		public string Invoice { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public DateTime? DateReceived { get; set; }
	}
}
