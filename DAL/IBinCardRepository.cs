using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LemlemPharmacy.DAL
{
	public interface IBinCardRepository : IDisposable
	{
		public Task<BinCardDTO> GetBinCard(Guid id);
		public Task<IEnumerable<BinCardDTO>> GetAllBinCards();
		public Task<IEnumerable<BinCardDTO>> GetBinCardByBatchNo(string batchNo);
		public Task<IEnumerable<BinCardDTO>> SearchBinCard(string phrase);
		public Task<IEnumerable<BinCardDTO>> GetBinCardByDate(CardDateRangeDTO binCardDate);
	}
}
