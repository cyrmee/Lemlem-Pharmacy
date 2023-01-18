using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class UpdateMedicineQuantityDTO
	{
		[Required]
		[StringLength(100, ErrorMessage = "BatchNo character length cannot exceed 100!")]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		public int Quantity { get; set; }

		[Required]
		public string Invoice { get; set; } = string.Empty;
	}
}
