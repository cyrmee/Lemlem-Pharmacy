using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemlemPharmacy.DAL
{
	public class BinCardRepository : IBinCardRepository, IDisposable
	{
		private readonly LemlemPharmacyContext _context;

		public BinCardRepository(LemlemPharmacyContext context)
		{
			_context = context;
		}

		public async Task<BinCardDTO> GetBinCard(Guid id)
		{
			var binCard = await _context.BinCard.FindAsync(id);
			if (binCard == null) throw new Exception("Bin Card not found!");
			return new BinCardDTO(binCard);
		}

		public async Task<IEnumerable<BinCardDTO>> GetAllBinCards()
		{
			var result = await _context.BinCard.ToListAsync();
			var binCardDTOs = new List<BinCardDTO>();
			foreach (var item in result)
				binCardDTOs.Add(new BinCardDTO(item));

			return binCardDTOs;
		}

		public async Task<IEnumerable<BinCardDTO>> GetBinCardByBatchNo(string batchNo)
		{
			var result = await _context.BinCard.FromSqlRaw($"SpSelectBinCardByBatchNo '{batchNo}'").ToListAsync();

			if (result == null) throw new Exception("Bin Card not found!");

			var binCards = new List<BinCardDTO>();
			foreach (var item in result)
				binCards.Add(new BinCardDTO(item));

			return binCards;
		}

		public async Task<IEnumerable<BinCardDTO>> SearchBinCard(string phrase)
		{
			var result = await _context.BinCard.FromSqlRaw($"SpSearchBinCard '{phrase}'").ToListAsync();

			if (result == null) throw new Exception("Bin Card not found!");

			var binCards = new List<BinCardDTO>();
			foreach (var item in result)
				binCards.Add(new BinCardDTO(item));

			return binCards;
		}

		public async Task<IEnumerable<BinCardDTO>> GetBinCardByDate(CardDateRangeDTO binCardDateRangeDTO)
		{
			var result = await _context.BinCard.FromSqlRaw($"EXEC SpGenerateBinCardForMedicineUsingRange @BatchNo = '{binCardDateRangeDTO.BatchNo}',@StartDate = '{binCardDateRangeDTO.StartDate}',@EndDate  = '{binCardDateRangeDTO.EndDate}'").ToListAsync();
			if (result == null) throw new Exception("Bin Card for medicine not found!");
			var binCards = new List<BinCardDTO>();
			foreach (var item in result)
				binCards.Add(new BinCardDTO(item));
			return binCards;
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
