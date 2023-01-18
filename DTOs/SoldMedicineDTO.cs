using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class SoldMedicineDTO
	{
		public Guid TransactionId { get; set; }

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


		public SoldMedicineDTO()
		{

		}

		public SoldMedicineDTO(SoldMedicine soldMedicine)
		{
			TransactionId = soldMedicine.TransactionId;
			PharmacistId = soldMedicine.PharmacistId;
			CustomerPhone = soldMedicine.CustomerPhone;
			MedicineId = soldMedicine.MedicineId;
			Quantity = soldMedicine.Quantity;
			SellingPrice = soldMedicine.SellingPrice;
			SellingDate = soldMedicine.SellingDate;
		}
	}
}
