using Microsoft.EntityFrameworkCore;
using NogaWebApplication.Models.Domain;

namespace NogaWebApplication.Data
{
    public class NogaDbContext : DbContext
    {
        public NogaDbContext(DbContextOptions options) : base(options)
        {
        
        
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Address> Addresses { get; set; }


    }
}
