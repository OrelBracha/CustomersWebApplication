using System.ComponentModel.DataAnnotations.Schema;

namespace NogaWebApplication.Models.Domain
{
    public class Address
    {
    
        public int Id { get; set; }

        public bool isDeleted { get; set; }

        public DateTime created { get; set; }

        public string city { get; set; }

        public string street { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
    
    
    }
}
