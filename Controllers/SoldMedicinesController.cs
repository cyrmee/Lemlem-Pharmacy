using Microsoft.AspNetCore.Mvc;
using LemlemPharmacy.Interfaces;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LemlemPharmacy.Controllers
{
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = $"{UserRole.Pharmacist}")]
	[ApiController]
    public class SoldMedicinesController : ControllerBase
    {
        private readonly ISoldMedicineRepository _soldMedicineRepository;

        public SoldMedicinesController(ISoldMedicineRepository soldMedicineRepository)
        {
			_soldMedicineRepository = soldMedicineRepository;
        }

        // POST: api/SoldMedicines/Sell
        [HttpPost]
        [Route("Sell")]
        public async Task<ActionResult<IEnumerable<SoldMedicineDTO>>> SellMedicine([FromBody] SellMedicineDTO soldMedicine)
        {
            try
            {
                var result = await _soldMedicineRepository.SellMedicine(soldMedicine);
                if (result == null)
                    return BadRequest(new Response()
                    {
                        Status = "Error",
                        Message = "Check your empty fields."
                    });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new Response() { Status = "Error", Message = e.Message });
            }
        }

        // GET: api/SoldMedicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoldMedicineDTO>>> GetAllSoldMedicines()
        {
            try
            {
				var result = await _soldMedicineRepository.GetAllSoldMedicines();
                return Ok(result);
			}
            catch(Exception e)
            {
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
        }

        // GET: api/SoldMedicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoldMedicineDTO>> GetSoldMedicine(Guid id)
        {
			try
			{
				var result = await _soldMedicineRepository.GetSoldMedicine(id);
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpGet("phrase/{phrase}")]
		[Authorize(Roles = $"{UserRole.Pharmacist}, {UserRole.Manager}")]
        //[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<SoldMedicineDTO>>> SearchSoldMedicine(string phrase)
		{
			var result = await _soldMedicineRepository.SearchSoldMedicine(phrase);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No record available."
				});
			return Ok(result);
		}
	}
}
