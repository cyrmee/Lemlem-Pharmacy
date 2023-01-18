using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LemlemPharmacy.DAL
{
	public interface IMedicineRepository : IDisposable
	{
		public Task<MedicineDTO> GetMedicine(Guid id);
		public Task<IEnumerable<MedicineDTO>> GetAllMedicine();
		public Task<IEnumerable<MedicineDTO>> GetToBeExpiredMedicineInOneMonth();
		public Task<IEnumerable<MedicineDTO>> GetToBeExpiredMedicineInSixMonth();
		public Task<IEnumerable<MedicineDTO>> GetMedicineByBatchNo(string batchNo);
		public Task<IEnumerable<MedicineDTO>> SearchMedicine(string phrase);
		public Task<IEnumerable<MedicineDTO>> GetMedicineByCategory(string category);
		public Task<IEnumerable<MedicineDTO>> UpdateMedicineWithoutQuantity(UpdateMedicineWithoutQuantityDTO medicine);
		public Task<IEnumerable<MedicineDTO>> UpdateMedicineQuantity(UpdateMedicineQuantityDTO medicine);
		public Task<IEnumerable<MedicineDTO>> AddMedicineQuantity(AddMedicineQuantityDTO medicine);
		public Task<IEnumerable<MedicineDTO>> RemoveMedicine(RemoveMedicineDTO medicine);
		public Task<IEnumerable<MedicineDTO>> AddMedicine(AddMedicineDTO medicine);

	}
}
