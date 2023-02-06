using System.ComponentModel.DataAnnotations;

namespace NogaWebApplication.Models
{
    public class AddCustomerViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{9,9}$", ErrorMessage = "Must be 9 digits long. only numbers ")]
        public int customerNumber { get; set; }


    }
}
