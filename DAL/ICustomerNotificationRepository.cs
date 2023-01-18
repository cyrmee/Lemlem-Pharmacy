using LemlemPharmacy.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LemlemPharmacy.DAL
{
	public interface ICustomerNotificationRepository : IDisposable
	{
		public Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotification();
		public Task<CustomerNotificationDTO> GetCustomerNotification(Guid id);
		public Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotificationByBatchNo(string batchNo);
		public Task<IEnumerable<CustomerNotificationDTO>> GetCustomerNotificationByPhoneNo(string phoneNo);
		public Task<IEnumerable<CustomerNotificationDTO>> SearchCustomerNotification(string phrase);
		public Task<IEnumerable<CustomerNotificationDTO>> EditCustomerNotification(Guid id, UpdateCustomerNotificationDTO customerNotification);
		public Task<IEnumerable<CustomerNotificationDTO>> AddCustomerNotification(AddCustomerNotificationDTO customerNotification);
		public Task<ActionResult> DeleteCustomerNotification(Guid id);
		public Task<IEnumerable<dynamic>> SendSMSToCustomers();
	}
}
