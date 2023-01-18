using Microsoft.AspNetCore.Mvc;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LemlemPharmacy.Controllers
{
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{UserRole.Manager}")]
	[ApiController]
    public class BinCardsController : ControllerBase
    {
        private readonly IBinCardRepository _binCardRepository;

        public BinCardsController(IBinCardRepository binCardRepository)
        {
            _binCardRepository = binCardRepository;
        }

        // GET: api/BinCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BinCardDTO>>> GetAllBinCards()
        {
            try
            {
                return Ok(await _binCardRepository.GetAllBinCards());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

		}

        // GET: api/BinCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BinCardDTO>> GetBinCard(Guid id)
        {
            try
            {
                return Ok(await _binCardRepository.GetBinCard(id));
			}
            catch(Exception e)
            {
				return BadRequest(e.Message);
			}
        }

		[HttpGet("batchNo/{batchNo}")]
		public async Task<ActionResult<IEnumerable<BinCardDTO>>> GetBinCardByBatchNo(string batchNo)
		{
            try
            {
                return Ok(await _binCardRepository.GetBinCardByBatchNo(batchNo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
		}

		[HttpGet("phrase/{phrase}")]
		public async Task<ActionResult<IEnumerable<BinCardDTO>>> SearchBinCard(string phrase)
		{
			try
			{
				return Ok(await _binCardRepository.SearchBinCard(phrase));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("byDate/")]
		public async Task<ActionResult<IEnumerable<BinCardDTO>>> GetBinCardByDate([FromQuery] CardDateRangeDTO binCardDateRangeDTO)
		{
			try
			{
				return Ok(await _binCardRepository.GetBinCardByDate(binCardDateRangeDTO));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
