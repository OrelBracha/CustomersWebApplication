using System.ComponentModel.DataAnnotations;

namespace NogaWebApplication.Models
{
	public class AddContactViewModel
	{
		[Required]
		public string FullName { get; set; }

		public string Email { get; set; }

		public string OfficeNumber { get; set; }

		[Required]
		public int CustomerId { get; set; }




	}
}
