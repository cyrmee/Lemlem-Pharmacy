using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LemlemPharmacy.DAL
{
	public class MedicineRepository : IMedicineRepository, IDisposable
	{
		private readonly LemlemPharmacyContext _context;

		public MedicineRepository(LemlemPharmacyContext context)
		{
			_context = context;
		}

		public async Task<MedicineDTO> GetMedicine(Guid id)
		{
			var medicine = await _context.Medicine.FindAsync(id);
			if (medicine == null) throw new Exception("Medicine not found!");
			return new MedicineDTO(medicine);
		}

		public async Task<IEnumerable<MedicineDTO>> GetAllMedicine()
		{
			var result = await _context.Medicine.FromSqlRaw($"SpSelectAllMedicine").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> GetToBeExpiredMedicineInOneMonth()
		{
			var result = await _context.Medicine.FromSqlRaw($"SpToBeExpiredMedicineInOneMonth").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> GetToBeExpiredMedicineInSixMonth()
		{
			var result = await _context.Medicine.FromSqlRaw($"SpToBeExpiredMedicineInSixMonths").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> GetMedicineByBatchNo(string batchNo)
		{
			var result = await _context.Medicine.FromSqlRaw($"SpSelectByBatchNo '{batchNo}'").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicine = new List<MedicineDTO>();
			foreach (var item in result)
				medicine.Add(new MedicineDTO(item));

			return medicine;
		}

		public async Task<IEnumerable<MedicineDTO>> SearchMedicine(string phrase)
		{
			var result = await _context.Medicine.FromSqlRaw($"SpSearchMedicine '{phrase}'").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicine = new List<MedicineDTO>();
			foreach (var item in result)
				medicine.Add(new MedicineDTO(item));

			return medicine;
		}

		public async Task<IEnumerable<MedicineDTO>> GetMedicineByCategory(string category)
		{
			var result = await _context.Medicine.FromSqlRaw($"SpSelectByCategory '{category}'").ToListAsync();

			if (result == null) throw new Exception("Medicine not found!");

			var medicine = new List<MedicineDTO>();
			foreach (var item in result)
				medicine.Add(new MedicineDTO(item));

			return medicine;
		}

		public async Task<IEnumerable<MedicineDTO>> UpdateMedicineWithoutQuantity(UpdateMedicineWithoutQuantityDTO medicine)
		{
			string StoredProc = $"EXEC SpUpdateMedicineWithoutQuantity @id = '{medicine.Id}',@batchNo = '{medicine.BatchNo}',@expireDate = '{medicine.ExpireDate}',@unit = '{medicine.Unit}',@price  = {medicine.Price},@description  = '{medicine.Description}',@Category  = '{medicine.Category}',@Type  = '{medicine.Type}'";

			if (IsExpired(medicine.ExpireDate))
				throw new Exception("Please check expiry date.");

			var result = await _context.Medicine.FromSqlRaw(StoredProc).ToArrayAsync();
			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> UpdateMedicineQuantity(UpdateMedicineQuantityDTO medicine)
		{
			string StoredProc = $"EXEC SpUpdateMedicineQuantity @BatchNo = '{medicine.BatchNo}',@Quantity  = '{medicine.Quantity}',@Invoice  = '{medicine.Invoice}'";

			var result = await _context.Medicine.FromSqlRaw(StoredProc).ToListAsync();
			if (result == null) throw new Exception("Medicine not found");
			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> AddMedicineQuantity(AddMedicineQuantityDTO medicine)
		{
			string StoredProc = $"EXEC SpAddMedicineQuantity @BatchNo = '{medicine.BatchNo}',@Quantity  = {medicine.Quantity},@Invoice  = '{medicine.Invoice}',@DateReceived  = '{medicine.DateReceived}'";

			if (medicine.DateReceived == null) medicine.DateReceived = DateTime.Now;
			var result = await _context.Medicine.FromSqlRaw(StoredProc).ToListAsync();
			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> RemoveMedicine(RemoveMedicineDTO medicine)
		{
			string StoredProc = $"EXEC SpRemoveMedicine @Id = '{medicine.Id}',@Quantity = {medicine.Quantity},@DateReceived  = '{medicine.DateReceived}',@Invoice  = '{medicine.Invoice}'";

			if (medicine.DateReceived == null) medicine.DateReceived = DateTime.Now;
			var result = await _context.Medicine.FromSqlRaw(StoredProc).ToListAsync();
			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public async Task<IEnumerable<MedicineDTO>> AddMedicine(AddMedicineDTO medicine)
		{
			string StoredProc = $"EXEC SpAddMedicine @BatchNo = '{medicine.BatchNo}',@ExpireDate = '{medicine.ExpireDate}',@Unit = '{medicine.Unit}',@Quantity = {medicine.Quantity},@Price  = {medicine.Price},@Description  = '{medicine.Description}',@Category  = '{medicine.Category}',@Type  = '{medicine.Type}',@Invoice  = '{medicine.Invoice}',@DateReceived  = '{medicine.DateReceived}'";
			if (IsExpired(medicine.ExpireDate))
				throw new Exception("Please check expiry date.");
			if (medicine.DateReceived == null) medicine.DateReceived = DateTime.Now;
			var result = await _context.Medicine.FromSqlRaw(StoredProc).ToListAsync();
			var medicineDTOs = new List<MedicineDTO>();
			foreach (var item in result)
				medicineDTOs.Add(new MedicineDTO(item));

			return medicineDTOs;
		}

		public bool IsExpired(DateTime expireDate)
		{
			var expiryCheck = new DateTime(
				year: DateTime.Now.Year,
				month: DateTime.Now.Month + 1,
				day: DateTime.Now.Day
			);
			if (expireDate <= expiryCheck) return true;
			else return false;
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
