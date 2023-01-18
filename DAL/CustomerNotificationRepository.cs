using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using LemlemPharmacy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LemlemPharmacy.DAL
{
	public class CustomerNotificationRepository : ICustomerNotificationRepository, IDisposable
	{
		private readonly LemlemPharmacyContext _context;
		private readonly string pattern = @"(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))";

		public CustomerNotificationRepository(LemlemPharmacyContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotification()
		{
			var result = await _context.CustomerNotification.ToListAsync();
			var customerNotificationDTOs = new List<CustomerNotificationDTO>();
			foreach (var item in result)
				customerNotificationDTOs.Add(new CustomerNotificationDTO(item));

			return customerNotificationDTOs;
		}

		public async Task<CustomerNotificationDTO> GetCustomerNotification(Guid id)
		{
			var customerNotification = await _context.CustomerNotification.FindAsync(id);
			if (customerNotification == null) throw new Exception("Record not found!");
			return new CustomerNotificationDTO(customerNotification);
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotificationByBatchNo(string batchNo)
		{
			var result = await _context.CustomerNotification.FromSqlRaw($"SpSelectCustomerNotificationByBatchNo '{batchNo}'").ToListAsync();
			if (result == null) throw new Exception("Record not found!");
			var customerNotificationDTOs = new List<CustomerNotificationDTO>();
			foreach (var item in result)
				customerNotificationDTOs.Add(new CustomerNotificationDTO(item));
			return customerNotificationDTOs;
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotificationByPhoneNo(string phoneNo)
		{
			var result = await _context.CustomerNotification.FromSqlRaw($"SpSelectCustomerNotificationByPhone '{phoneNo}'").ToListAsync();
			if (result == null) throw new Exception("Record not found!");
			var customerNotificationDTOs = new List<CustomerNotificationDTO>();
			foreach (var item in result)
				customerNotificationDTOs.Add(new CustomerNotificationDTO(item));
			return customerNotificationDTOs;
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> SearchCustomerNotification(string phrase)
		{
			var result = await _context.CustomerNotification.FromSqlRaw($"SpSearchCustomerNotification '{phrase}'").ToListAsync();
			if (result == null) throw new Exception("Record not found!");
			var customerNotificationDTOs = new List<CustomerNotificationDTO>();
			foreach (var item in result)
				customerNotificationDTOs.Add(new CustomerNotificationDTO(item));
			return customerNotificationDTOs;
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> EditCustomerNotification(Guid id, UpdateCustomerNotificationDTO customerNotification)
		{
			string storedProc = $"EXEC SpUpdateCustomerNotification @Id = '{id}',@PhoneNo  = '{customerNotification.PhoneNo}',@BatchNo  = '{customerNotification.BatchNo}',@Interval  = {customerNotification.Interval},@EndDate  = '{customerNotification.EndDate}',@NextDate  = '{customerNotification.NextDate}'";

			if (Regex.IsMatch(customerNotification.PhoneNo, pattern))
			{
				var result = await _context.CustomerNotification.FromSqlRaw(storedProc).ToListAsync();
				var customerNotifications = new List<CustomerNotificationDTO>();
				foreach (var item in result)
					customerNotifications.Add(new CustomerNotificationDTO(item));

				return customerNotifications;
			}
			else
				throw new Exception("Phone number not in the right format. Example: +251 91 234 5678 +251912345678");
		}

		public async Task<IEnumerable<CustomerNotificationDTO>> AddCustomerNotification(AddCustomerNotificationDTO customerNotification)
		{
			string storedProc = $"EXEC SpAddCustomerNotification @CustomerPhoneNo  = '{customerNotification.PhoneNo}',@BatchNo  = '{customerNotification.BatchNo}',@Interval  = {customerNotification.Interval},@EndDate  = '{customerNotification.EndDate}',@NextDate  = '{new DateTime().AddMonths(customerNotification.Interval)}'";

			if (Regex.IsMatch(customerNotification.PhoneNo, pattern))
			{
				var result = await _context.CustomerNotification.FromSqlRaw(storedProc).ToListAsync();
				var customerNotifications = new List<CustomerNotificationDTO>();
				foreach (var item in result)
					customerNotifications.Add(new CustomerNotificationDTO(item));

				return customerNotifications;
			}
			else
				throw new Exception("Phone number not in the right format. Example: +251 91 234 5678 +251912345678");
		}

		public async Task<ActionResult> DeleteCustomerNotification(Guid id)
		{
			var customerNotification = await _context.CustomerNotification.FindAsync(id);
			if (customerNotification == null) throw new Exception("Record not found!");
			_context.CustomerNotification.Remove(customerNotification);
			await _context.SaveChangesAsync();
			return new NoContentResult();
		}

		public async Task<IEnumerable<dynamic>> SendSMSToCustomers()
		{
			var result = await (from customerNotification in _context.Set<CustomerNotification>().DefaultIfEmpty()
								join medicine in _context.Set<Medicine>().DefaultIfEmpty()
									on customerNotification.BatchNo equals medicine.BatchNo
								join customer in _context.Set<Customer>().DefaultIfEmpty()
									on customerNotification.PhoneNo equals customer.PhoneNo
								where DateTime.Now.AddDays(-3) <= customerNotification.NextDate && customerNotification.NextDate <= DateTime.Now
								select new
								{
									customerNotification.Id,
									customer.Name,
									customerNotification.PhoneNo,
									customerNotification.BatchNo,
									customerNotification.Interval,
									customerNotification.EndDate,
									customerNotification.NextDate,
									medicine.Description,
									medicine.Category
								}
						  ).ToListAsync();

			if (result == null) throw new Exception("No pending notification.");
			foreach (var item in result)
			{
				SMSService.SendSMS(
						item.PhoneNo,
						$"Dear {item.Name},\nPlease get your {item.Description} on {item.NextDate}.\nSincerley,\nLemlem Pharmacy");
				await EditCustomerNotification(
					item.Id,
					new UpdateCustomerNotificationDTO(
						item.PhoneNo,
						item.BatchNo,
						item.Interval,
						item.EndDate,
						item.NextDate.AddMonths(item.Interval)));
			}
			return result;
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
