using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.Interfaces
{
    public class SellMedicineDTO
    {
        [Required]
        public string PharmacistId { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        [Required]
        public Guid MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? SellingDate { get; set; }

        public int Interval { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
