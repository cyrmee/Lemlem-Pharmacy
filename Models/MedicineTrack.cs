using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LemlemPharmacy.Models
{
	public class MedicineTrack
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		public string Invoice { get; set; } = string.Empty;
	}
}
