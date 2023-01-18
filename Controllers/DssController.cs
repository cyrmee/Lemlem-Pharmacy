using LemlemPharmacy.DAL;
using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static LemlemPharmacy.ForecastingModel;

namespace LemlemPharmacy.Controllers
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{UserRole.Manager}")]
	[ApiController]
	public class DssController : ControllerBase
	{
		private readonly IDssRepository _dssRepository;

		public DssController(IDssRepository dssRepository)
		{
			_dssRepository = dssRepository;
		}


		// GET: api/<DssController>
		[HttpGet("FullRUCRecords")]
		public async Task<ActionResult<IEnumerable<FullRucDTO>>> GetFullRucReport()
		{
			try
			{
				return Ok(await _dssRepository.GetFullRUCReport());
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("damagedGraphByCategory")]
		public async Task<ActionResult<IEnumerable<GraphByCategoryDTO>>> GetDamagedGraphByCategory()
		{
			try
			{
				return Ok(await _dssRepository.GetDamagedGraphByCategory());
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("soldGraphByCategory")]
		public async Task<ActionResult<IEnumerable<GraphByCategoryDTO>>> GetSoldGraphByCategory()
		{
			try
			{
				return Ok(await _dssRepository.GetSoldGraphByCategory());
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("inStockGraphByCategory")]
		public async Task<ActionResult<IEnumerable<GraphByCategoryDTO>>> GetInStockGraphByCategory()
		{
			try
			{
				return Ok(await _dssRepository.GetInStockGraphByCategory());
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("profitloss")]
		public async Task<ActionResult<IEnumerable<ProfitLossDTO>>> GetProfitLossReport()
		{
			try
			{
				return Ok(await _dssRepository.GetProfitLossReport());
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("profitlossbydate")]
		public async Task<ActionResult<IEnumerable<ProfitLossDTO>>> GetProfitLossReportByDate([FromQuery] DateRangeDTO dateRange)
		{
			try
			{
				return Ok(await _dssRepository.GetProfitLossReportByDate(dateRange));
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("forecast")]
		public ActionResult<ModelOutput> Forecast(int horizon = 3)
		{
			try
			{
				return Ok(_dssRepository.PredictNextThreeMonthsSale(horizon));
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("mostSoldMedicine")]
		public async Task<ActionResult<IEnumerable<ProductRecommendationDTO>>> MostSoldItemByDate([FromQuery]DateRangeDTO dateRange)
		{
			try
			{
				return Ok(await _dssRepository.MostSoldItemByDate(dateRange));
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		[HttpGet("generateStockCard")]
		public async Task<ActionResult<IEnumerable<ProductRecommendationDTO>>> GenerateStockCard([FromQuery] CardDateRangeDTO stockCardRange)
		{
			try
			{
				return Ok(await _dssRepository.GenerateStockCard(stockCardRange));
			}
			catch (Exception e)
			{
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}
	}
}
