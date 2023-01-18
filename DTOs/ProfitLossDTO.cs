using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace LemlemPharmacy.DTOs
{
	public class ProfitLossDTO
	{
		/*{
    "batchNo": "123",
    "description": "Med Name",
    "soldQuantity": 20,
    "sellingPrice": 125,
    "medicineCost": 100,
    "damaged": 5,
    "profit": 0
  },*/
		[Required]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		public string Description { get; set; } = string.Empty;

		[Required]
		public int SoldQuantity { get; set; }

		[Required]
		public double SellingPrice { get; set; }

		[Required]
		public double MedicineCost { get; set; }

		[Required]
		public int Damaged { get; set; }

		[Required]
		public float Profit { get; set; }


		public ProfitLossDTO()
		{

		}

		public ProfitLossDTO(string batchNo, string description, int soldQuantity, double sellingPrice, double medicineCost, int damaged, float profit)
		{
			BatchNo = batchNo;
			Description = description;
			SoldQuantity = soldQuantity;
			SellingPrice = sellingPrice;
			MedicineCost = medicineCost;
			Damaged = damaged;
			Profit = profit;
		}
	}
}
