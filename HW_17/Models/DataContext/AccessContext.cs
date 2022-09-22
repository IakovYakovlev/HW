using HW_17.Models.Access;
using Microsoft.EntityFrameworkCore;

namespace HW_17.Models.DataContext
{
    internal class AccessContext : DbContext
    {
        public AccessContext(DbContextOptions<AccessContext> opt) : base(opt) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
        }
    }
}
