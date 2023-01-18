using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.Models
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ReportId { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; }


    }
}
