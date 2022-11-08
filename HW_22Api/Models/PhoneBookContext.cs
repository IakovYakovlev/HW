using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Models
{
    public class PhoneBookContext : IdentityDbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> opt) : base(opt) { }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBook>().ToTable("PhoneBook");
        }
    }
}
