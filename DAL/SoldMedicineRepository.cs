using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.Interfaces;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LemlemPharmacy.DAL
{
	public class SoldMedicineRepository : ISoldMedicineRepository, IDisposable
	{
		private readonly LemlemPharmacyContext _context;
		private readonly string pattern = @"(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))";

		public SoldMedicineRepository(LemlemPharmacyContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<SoldMedicineDTO>> SellMedicine(SellMedicineDTO soldMedicine)
		{
			if (Regex.IsMatch(soldMedicine.CustomerPhone, pattern))
			{
				string StoredProc = string.Empty;
				var medicine = await _context.Medicine.FindAsync(soldMedicine.MedicineId);
				if (medicine != null && medicine.Type == "ShortTerm")
				{
					StoredProc = $"EXEC SpSellMedicine @PharmacistId = '{soldMedicine.PharmacistId}',@MedicineId = '{soldMedicine.MedicineId}',@Quantity = {soldMedicine.Quantity},@SellingDate = '{soldMedicine.SellingDate}',@CustomerPhone  = '{soldMedicine.CustomerPhone}'";
				}
				else if (medicine != null && medicine.Type == "LongTerm")
				{
					StoredProc = $"EXEC SpSellLongTermMedicine @PharmacistId = '{soldMedicine.PharmacistId}',@MedicineId = '{soldMedicine.MedicineId}',@Quantity = {soldMedicine.Quantity},@SellingDate = '{soldMedicine.SellingDate}',@CustomerPhone  = '{soldMedicine.CustomerPhone}',@Interval = {soldMedicine.Interval},@EndDate = '{soldMedicine.EndDate}',@NextDate = '{new DateTime(
								year: soldMedicine.SellingDate.Value.Year,
								month: soldMedicine.SellingDate.Value.Month + soldMedicine.Interval,
								day: soldMedicine.SellingDate.Value.Day)}'";
				}

				if (soldMedicine.SellingDate == null) soldMedicine.SellingDate = DateTime.Now;
				var result = await _context.SoldMedicine.FromSqlRaw(StoredProc).ToListAsync();
				var soldMedicines = new List<SoldMedicineDTO>();
				foreach (var item in result)
					soldMedicines.Add(new SoldMedicineDTO(item));
				return soldMedicines;
			}
			else
				throw new Exception("Phone number not in the right format. Example: +251 91 234 5678 +251912345678");
		}

		public async Task<IEnumerable<SoldMedicineDTO>> GetAllSoldMedicines()
		{
			var result = await _context.SoldMedicine.ToListAsync();
			if (result == null) throw new Exception("Transaction record not found!");
			var soldMedicines = new List<SoldMedicineDTO>();
			foreach (var item in result)
				soldMedicines.Add(new SoldMedicineDTO(item));
			return soldMedicines;
		}

		public async Task<SoldMedicineDTO> GetSoldMedicine(Guid id)
		{
			var result = await _context.SoldMedicine.FindAsync(id);
			if (result == null) throw new Exception("Transaction record not found!");
			return new SoldMedicineDTO(result);
		}

		public async Task<IEnumerable<SoldMedicineDTO>> SearchSoldMedicine(string phrase)
		{
			string storedProc = $"SpSearchSoldMedicine '{phrase}'";
			var result = await _context.SoldMedicine.FromSqlRaw(storedProc).ToListAsync();

			if (result == null) throw new Exception("Record not found");

			var soldMedicines = new List<SoldMedicineDTO>();
			foreach (var item in result)
				soldMedicines.Add(new SoldMedicineDTO(item));

			return soldMedicines;
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
