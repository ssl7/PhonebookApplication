using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhonebookApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PhonebookRecordModel> Phonebook { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = phonebookdb; Trusted_Connection = True;");
        }
    }
}
