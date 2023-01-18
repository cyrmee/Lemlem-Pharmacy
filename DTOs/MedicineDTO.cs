using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace LemlemPharmacy.DTOs
{
	public class MedicineDTO
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "BatchNo character length cannot exceed 100!")]
		public string BatchNo { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public DateTime ExpireDate { get; set; }

		[Required]
		[StringLength(64, ErrorMessage = "Unit character length cannot exceed 64!")]
		public string Unit { get; set; } = string.Empty;

		[Required]
		public int Quantity { get; set; }

		[Required]
		public float Price { get; set; }

		[Required]
		[StringLength(1024, ErrorMessage = "Description character length cannot exceed 1024!")]
		public string Description { get; set; } = string.Empty;

		[Required]
		[StringLength(64, ErrorMessage = "Category character length cannot exceed 64!")]
		public string Category { get; set; } = string.Empty;

		[Required]
		[StringLength(64, ErrorMessage = "Type character length cannot exceed 64!")]
		public string Type { get; set; } = string.Empty;


		public MedicineDTO()
		{

		}

		public MedicineDTO(Guid id, string batchNo, DateTime expireDate, string unit, int quantity, float price, string description, string category, string type)
		{
			Id = id;
			BatchNo = batchNo;
			ExpireDate = expireDate;
			Unit = unit;
			Quantity = quantity;
			Price = price;
			Description = description;
			Category = category;
			Type = type;
		}

		public MedicineDTO(Medicine medicine)
		{
			Id = medicine.Id;
			BatchNo = medicine.BatchNo;
			ExpireDate = medicine.ExpireDate;
			Unit = medicine.Unit;
			Quantity = medicine.Quantity;
			Price = medicine.Price;
			Description = medicine.Description;
			Category = medicine.Category;
			Type = medicine.Type;
		}
	}
}
