using LemlemPharmacy.Data;
using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Data;
using System.Text.RegularExpressions;

namespace LemlemPharmacy.DAL
{
	public class CustomerRepository : ICustomerRepository, IDisposable
	{
		private readonly LemlemPharmacyContext _context;
		private readonly string pattern = @"(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))";

		public CustomerRepository(LemlemPharmacyContext context)
		{
			_context = context;
		}

		public async Task<CustomerDTO> GetCustomer(Guid id)
		{
			var customer = await _context.Customer.FindAsync(id);
			if (customer == null) throw new Exception("Customer not found");
			return new CustomerDTO(customer);
		}

		public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
		{
			var result = await _context.Customer.ToListAsync();
			var customerDTOs = new List<CustomerDTO>();
			foreach (var item in result)
				customerDTOs.Add(new CustomerDTO(item));

			return customerDTOs;
		}

		public async Task<IEnumerable<CustomerDTO>> GetCustomerByPhone(string phoneNo)
		{
			string storedProc = $"SpSelectCustomerByPhone '{phoneNo}'";
			var result = await _context.Customer.FromSqlRaw(storedProc).ToListAsync();

			if (result == null) throw new Exception("Customer not found");

			var customers = new List<CustomerDTO>();
			foreach (var item in result)
				customers.Add(new CustomerDTO(item));

			return customers;
		}

		public async Task<IEnumerable<CustomerDTO>> SearchCustomer(string phrase)
		{
			string storedProc = $"SpSearchCustomer '{phrase}'";
			var result = await _context.Customer.FromSqlRaw(storedProc).ToListAsync();

			if (result == null) throw new Exception("Customer not found");

			var customers = new List<CustomerDTO>();
			foreach (var item in result)
				customers.Add(new CustomerDTO(item));

			return customers;
		}

		public async Task<IEnumerable<CustomerDTO>> GetCustomerByName(string name)
		{
			var result = await _context.Customer.FromSqlRaw($"SpSelectCustomerByName '{name}'").ToListAsync();

			if (result == null) throw new Exception("Customer not found");

			var customers = new List<CustomerDTO>();
			foreach (var item in result)
				customers.Add(new CustomerDTO(item));

			return customers;
		}

		public async Task<IEnumerable<CustomerDTO>> UpdateCustomer(CustomerDTO customer)
		{
			string storedProc = $"EXEC SpUpdateCustomer @Id = '{customer.Id}',@Name = '{customer.Name}',@PhoneNo  = '{customer.PhoneNo}'";

			if (Regex.IsMatch(customer.PhoneNo, pattern))
			{
				var result = await _context.Customer.FromSqlRaw(storedProc).ToListAsync();
				var customers = new List<CustomerDTO>();
				foreach (var item in result)
					customers.Add(new CustomerDTO(item));

				return customers;
			}
			else
				throw new Exception("Phone number not in the right format. Example: +251 91 234 5678 +251912345678");
		}

		public async Task<IEnumerable<CustomerDTO>> AddCustomer(AddCustomerDTO customer)
		{
			string storedProc = $"EXEC SpAddCustomer @Name = '{customer.Name}',@PhoneNo  = '{customer.PhoneNo}'";

			if (Regex.IsMatch(customer.PhoneNo, pattern))
			{
				var result = await _context.Customer.FromSqlRaw(storedProc).ToListAsync();
				var customerDTOs = new List<CustomerDTO>();
				foreach (var item in result)
					customerDTOs.Add(new CustomerDTO(item));

				return customerDTOs;
			}
			else
				throw new Exception("Phone number not in the right format. Example: +251 91 234 5678 +251912345678");

		}

		public async Task<ActionResult> DeleteCustomer(Guid id)
		{
			var customer = await _context.Customer.FindAsync(id);
			if (customer == null) throw new Exception("Customer not found.");
			_context.Customer.Remove(customer);
			_context.SaveChanges();
			return new NoContentResult();
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
