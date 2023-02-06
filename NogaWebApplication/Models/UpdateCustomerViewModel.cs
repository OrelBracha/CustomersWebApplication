using System.ComponentModel.DataAnnotations;

namespace NogaWebApplication.Models
{
	public class UpdateCustomerViewModel
	{
        [Required]
        public int id { get; set; }
        
        [Required]
        public string name { get; set; }
        
        [Required]
        public int customerNumber { get; set; }





    }
}
