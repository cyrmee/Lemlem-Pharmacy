using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using static LemlemPharmacy.ForecastingModel;

namespace LemlemPharmacy.DAL
{
	public interface IDssRepository : IDisposable
	{
		public Task<IEnumerable<dynamic>> GetFullRUCReport();
		public Task<IEnumerable<dynamic>> GetDamagedGraphByCategory();
		public Task<IEnumerable<dynamic>> GetSoldGraphByCategory();
		public Task<IEnumerable<dynamic>> GetInStockGraphByCategory();
		public Task<IEnumerable<dynamic>> GetProfitLossReport();
		public Task<IEnumerable<dynamic>> GetProfitLossReportByDate(DateRangeDTO dateRange);
		public ModelOutput PredictNextThreeMonthsSale(int horizon);
		public Task<IEnumerable<dynamic>> MostSoldItemByDate(DateRangeDTO dateRange);
		public Task<IEnumerable<dynamic>> GenerateStockCard(CardDateRangeDTO stockCardRange);
	}
}
