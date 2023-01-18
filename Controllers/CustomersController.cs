using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using System.Text.RegularExpressions;
using LemlemPharmacy.DAL;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LemlemPharmacy.Controllers
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		#region Unit of Work Pattern
		private readonly ICustomerRepository _customerRepository;

		public CustomersController(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerDTO>> GetCustomer(Guid id)
		{
			try
			{
				var customer = await _customerRepository.GetCustomer(id);
				if (customer == null)
					return NotFound(new Response()
					{
						Status = "Error",
						Message = "Customer not found."
					});
				return Ok(customer);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
		{
			var result = await _customerRepository.GetAllCustomers();

			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No customer available."
				});

			return Ok(result);
		}

		[HttpGet("phoneNo/{phoneNo}")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomerByPhone(string phoneNo)
		{
			var result = await _customerRepository.GetCustomerByPhone(phoneNo);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No customer available."
				});
			return Ok(result);
		}

		[HttpGet("phrase/{phrase}")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> SearchCustomer(string phrase)
		{
			var result = await _customerRepository.SearchCustomer(phrase);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No customer available."
				});
			return Ok(result);
		}

		[HttpGet("name/{name}")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomerByName(string name)
		{
			var result = await _customerRepository.GetCustomerByName(name);
			if (result == null)
				return NotFound(new Response()
				{
					Status = "Error",
					Message = "No customer available."
				});
			return Ok(result);
		}

		[HttpPut("update")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> UpdateCustomer([FromBody] CustomerDTO customer)
		{
			try
			{
				var result = await _customerRepository.UpdateCustomer(customer);
				if (result == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Customer couldn't be updated. Please check your fields."
					});
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpPost("add")]
		[Authorize(Roles = $"{UserRole.Pharmacist}")]
		public async Task<ActionResult<IEnumerable<CustomerDTO>>> AddCustomer([FromBody] AddCustomerDTO customer)
		{
			try
			{
				var result = await _customerRepository.AddCustomer(customer);
				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(new Response() { Status = "Error", Message = e.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteCustomer(Guid id)
		{
			try
			{
				return await _customerRepository.DeleteCustomer(id);
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
		#endregion
	}
}
