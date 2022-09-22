using HW_17.Models.SQL;
using Microsoft.EntityFrameworkCore;

namespace HW_17.Models.DataContext
{
    internal class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> opt) : base(opt) { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
