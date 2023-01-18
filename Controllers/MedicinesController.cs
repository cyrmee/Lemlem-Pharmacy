using Microsoft.AspNetCore.Mvc;
using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Cors;
using LemlemPharmacy.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LemlemPharmacy.Controllers
{
	[Route("api/[controller]")]
	[EnableCors]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	public class MedicinesController : ControllerBase
	{
		private readonly IMedicineRepository _medicineRepository;

		public MedicinesController(IMedicineRepository medicineRepository)
		{
			_medicineRepository = medicineRepository;
		}

		// GET: api/Medicines/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MedicineDTO>> GetMedicine(Guid id)
		{
			try
			{
				var medicine = await _medicineRepository.GetMedicine(id);
				return Ok(medicine);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// GET : api/Medicine/all
		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetAllMedicine()
		{
			var result = await _medicineRepository.GetAllMedicine();

			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});

			return Ok(result);
		}

		// GET : api/Medicine/batchNo/123451235
		[HttpGet("batchNo/{batchNo}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetMedicineByBatchNo(string batchNo)
		{
			var result = await _medicineRepository.GetMedicineByBatchNo(batchNo);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});
			return Ok(result);
		}

		[HttpGet("toBeExpiredInOneMonth")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetToBeExpiredMedicineInOneMonth()
		{
			var result = await _medicineRepository.GetToBeExpiredMedicineInOneMonth();
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});
			return Ok(result);
		}

		[HttpGet("toBeExpiredInSixMonth")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetToBeExpiredMedicineInSixMonth()
		{
			var result = await _medicineRepository.GetToBeExpiredMedicineInSixMonth();
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});
			return Ok(result);
		}

		[HttpGet("phrase/{phrase}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> SearchMedicine(string phrase)
		{
			var result = await _medicineRepository.SearchMedicine(phrase);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});
			return Ok(result);
		}

		// GET : api/Medicine/category/123451235
		[HttpGet("category/{category}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetMedicineByCategory(string category)
		{
			var result = await _medicineRepository.GetMedicineByCategory(category);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No medicine available."
				});
			return Ok(result);
		}

		// PUT: api/Medicines/5
		// To protect from overposting attacks, we use DTOs
		[HttpPut("updateDetails")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> UpdateMedicineWithoutQuantity([FromBody] UpdateMedicineWithoutQuantityDTO medicine)
		{
			try
			{
				var result = await _medicineRepository.UpdateMedicineWithoutQuantity(medicine);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Medicine couldn't be updated. Please check your fields"
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		// updates the quantity of both medicine and bincard record
		[HttpPut("updateQuantity")]
		[Authorize(Roles = $"{UserRole.Manager}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> UpdateMedicineQuantity([FromBody] UpdateMedicineQuantityDTO medicine)
		{
			try
			{
				var result = await _medicineRepository.UpdateMedicineQuantity(medicine);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Medicine couldn't be updated. Please check your fields"
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpPut("addQuantity")]
		[Authorize(Roles = $"{UserRole.Manager}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> AddMedicineQuantity([FromBody] AddMedicineQuantityDTO medicine)
		{
			try
			{
				var result = await _medicineRepository.AddMedicineQuantity(medicine);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Medicine couldn't be updated. Please check your fields."
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpPut("remove")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> RemoveMedicine([FromBody] RemoveMedicineDTO medicine)
		{
			try
			{
				var result = await _medicineRepository.RemoveMedicine(medicine);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Medicine couldn't be deleted. Please check your fields."
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}


		// POST: api/Medicines
		// To protect from overposting attacks, we use DTOs
		[HttpPost("add")]
		[Authorize(Roles = $"{UserRole.Manager}")]
		public async Task<ActionResult<IEnumerable<MedicineDTO>>> AddMedicine([FromBody] AddMedicineDTO medicine)
		{
			try
			{
				var result = await _medicineRepository.AddMedicine(medicine);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Medicine couldn't be adedd. Please check your fields."
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}
	}
}
