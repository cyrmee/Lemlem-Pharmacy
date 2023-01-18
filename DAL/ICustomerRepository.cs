using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LemlemPharmacy.DAL
{
	public interface ICustomerRepository : IDisposable
	{
		public Task<CustomerDTO> GetCustomer(Guid id);
		public Task<IEnumerable<CustomerDTO>> GetAllCustomers();
		public Task<IEnumerable<CustomerDTO>> GetCustomerByPhone(string phoneNo);
		public Task<IEnumerable<CustomerDTO>> SearchCustomer(string phrase);
		public Task<IEnumerable<CustomerDTO>> GetCustomerByName(string name);
		public Task<IEnumerable<CustomerDTO>> UpdateCustomer(CustomerDTO customer);
		public Task<IEnumerable<CustomerDTO>> AddCustomer(AddCustomerDTO customer);
		public Task<ActionResult> DeleteCustomer(Guid id);
	}
}
