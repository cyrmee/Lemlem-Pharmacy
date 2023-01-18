using Microsoft.AspNetCore.Identity;

namespace LemlemPharmacy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<SoldMedicine>? SoldMedicines { get; set; }
    }
}
