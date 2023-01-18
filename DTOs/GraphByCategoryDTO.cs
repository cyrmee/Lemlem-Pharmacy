using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class GraphByCategoryDTO
	{
		[Required]
		[StringLength(64, ErrorMessage = "Category character length cannot exceed 64!")]
		public string Category { get; set; } = string.Empty;

		[Required]
		public int Amount { get; set; }
	}
}
