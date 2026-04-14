using Microsoft.EntityFrameworkCore;
using Petrov_Tema_19.Models;

namespace Petrov_Tema_19.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}