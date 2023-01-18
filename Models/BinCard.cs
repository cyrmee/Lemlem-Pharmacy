using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.Models
{
    public class BinCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Invoice { get; set; } = string.Empty;

		[Required]
		public Guid MedicineId { get; set; }

		[Required]
        public string BatchNo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }

        [Required]
        public int AmountRecived { get; set; }

        [Required]
        public int Damaged { get; set; }


		public Medicine? Medicine { get; set; }
		public Medicine? MedicineID { get; set; }
	}
}
