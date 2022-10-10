using Microsoft.EntityFrameworkCore;

namespace HW_20.Models
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> opt) : base(opt) { }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBook>().ToTable("PhoneBook");
        }
    }
}
