using Microsoft.Build.Framework;

namespace LemlemPharmacy.DTOs
{
	public class ProductRecommendationDTO
	{
		[Required]
		public Guid MedicineId { get; set; }

		[Required]
		public string Description { get; set; } = string.Empty;

		[Required]
		public float SoldQuantity { get; set; }
	}
}
