using System.ComponentModel.DataAnnotations.Schema;

namespace NogaWebApplication.Models.Domain
{
    public class Contact
    {

        public int Id { get; set; }

        public bool isDeleted { get; set; }

        public DateTime created { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string OfficeNumber { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }



    }
}
