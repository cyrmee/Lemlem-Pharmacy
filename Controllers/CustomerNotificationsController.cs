using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LemlemPharmacy.Data;
using LemlemPharmacy.Models;
using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Mvc.Routing;
using LemlemPharmacy.Services;
using LemlemPharmacy.DAL;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LemlemPharmacy.Controllers
{
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
    public class CustomerNotificationsController : ControllerBase
    {
		private readonly ICustomerNotificationRepository _customerNotificationRepository;

		public CustomerNotificationsController(ICustomerNotificationRepository customerNotificationRepository, IMedicineRepository medicineRepository)
        {
            _customerNotificationRepository = customerNotificationRepository;
        }

        // GET: api/CustomerNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerNotificationDTO>>> GetCustomerNotification()
        {
            try
            {
                return Ok(await _customerNotificationRepository.GetCustomerNotification());
            }
            catch(Exception e)
            {
                return BadRequest(new Response()
                {
                    Status = "Error",
                    Message = e.Message
                });
            }
        }

        // GET: api/CustomerNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerNotificationDTO>> GetCustomerNotification(Guid id)
        {
            try
            {
                return Ok(await _customerNotificationRepository.GetCustomerNotification(id));
            }
            catch(Exception e)
            {
                return BadRequest(new Response()
                {
                    Status = "Error",
                    Message = e.Message
                });
            }
        }

		// GET: api/CustomerNotifications/5
		[HttpGet("batchNo/{batchNo}")]
		public async Task<ActionResult<IEnumerable<CustomerNotificationDTO>>> GetCustomerNotificationByBatchNo(string batchNo)
		{
            try
            {
                return Ok(await _customerNotificationRepository.GetCustomerNotificationByBatchNo(batchNo));
            }
            catch(Exception e)
            {
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
				});
			}
		}

		// GET: api/CustomerNotifications/5
		[HttpGet("phoneNo/{phoneNo}")]
		public async Task<ActionResult<IEnumerable<CustomerNotificationDTO>>> GetCustomerNotificationByPhoneNo(string phoneNo)
		{
			try
			{
				return Ok(await _customerNotificationRepository.GetCustomerNotificationByPhoneNo(phoneNo));
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

		[HttpGet("phrase/{phrase}")]
		public async Task<ActionResult<IEnumerable<CustomerNotificationDTO>>> SearchCustomerNotification(string phrase)
		{
			try
			{
				return Ok(await _customerNotificationRepository.SearchCustomerNotification(phrase));
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

		// PUT: api/CustomerNotifications/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("id/{id}")]
        public async Task<ActionResult> PutCustomerNotification(Guid id, UpdateCustomerNotificationDTO customerNotification)
        {
            try
            {
                return Ok(await _customerNotificationRepository.EditCustomerNotification(id, customerNotification));
            }
            catch(Exception e)
            {
				return BadRequest(new Response()
				{
					Status = "Error",
					Message = e.Message
                });
            }
        }

        [HttpGet("sendtoall")]
		public async Task<ActionResult<IEnumerable<dynamic>>> SendSMSToCustomers()
        {
            try
            {
                return Ok(await _customerNotificationRepository.SendSMSToCustomers());
            }
            catch(Exception e)
            {
                return BadRequest(new Response()
                {
                    Status = "Error",
                    Message = e.Message
                });
            }
		}

        //[HttpPut("sendToCustomer")]
        //public async Task<ActionResult<IEnumerable<CustomerNotificationDTO>>> SendSMSToCustomer(string phoneNo)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new Response()
        //        {
        //            Status = "Error",
        //            Message = e.Message
        //        });
        //    }
        //}

        [HttpPost("add")]
		[Authorize(Roles = $"{UserRole.Manager}, {UserRole.Pharmacist}")]
		public async Task<ActionResult<CustomerNotificationDTO>> PostCustomerNotification(AddCustomerNotificationDTO customerNotification)
        {
            try
            {
                return Ok(await _customerNotificationRepository.AddCustomerNotification(customerNotification));
			}
            catch(Exception e)
            {
                return BadRequest(new Response()
                {
                    Status = "Error",
                    Message = $"{e.Message}"
                });
            }
        }

        // DELETE: api/CustomerNotifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerNotification(Guid id)
        {
			try
			{
				return await _customerNotificationRepository.DeleteCustomerNotification(id);
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
