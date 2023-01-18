using LemlemPharmacy.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LemlemPharmacy.DTOs
{
	public class CustomerDTO
	{
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		[RegexStringValidator(@"/(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))/")]
		public string PhoneNo { get; set; } = string.Empty;


		public CustomerDTO()
		{

		}

		public CustomerDTO(Guid id, string name, string phoneNo)
		{
			Id = id;
			Name = name;
			PhoneNo = phoneNo;
		}

		public CustomerDTO(Customer customer)
		{
			Id = customer.Id;
			Name = customer.Name;
			PhoneNo = customer.PhoneNo;
		}
	}
}
