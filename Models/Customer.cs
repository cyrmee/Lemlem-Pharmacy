using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LemlemPharmacy.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string PhoneNo { get; set; } = string.Empty;


        public ICollection<SoldMedicine>? SoldMedicines { get; set; }
		public ICollection<CustomerNotification>? CustomerNotifications { get; set; }
	}
}
