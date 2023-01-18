using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.Models
{
    public class SoldMedicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        // From Username
        [Required]
        public string PharmacistId { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        [Required]
        public Guid MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float SellingPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? SellingDate { get; set; }


        public Medicine? Medicine { get; set; }
        public Customer? Customer { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
