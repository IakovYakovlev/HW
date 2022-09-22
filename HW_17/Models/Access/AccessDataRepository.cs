using HW_17.Models.DataContext;
using HW_17.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace HW_17.Models.Access
{
    internal class AccessDataRepository : IDataRepository<Product>
    {
        AccessContext _context;

        public AccessDataRepository(AccessContext context) => _context = context;

        public IEnumerable<Product> GetAllData => _context.Products.AsNoTracking();

        public void Add(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public Product Get(int id) => _context.Products.Find(id);

        public void Remove(int id)
        {
            _context.Products.Remove(Get(id));
            _context.SaveChanges();
        }

        public void RemoveAll()
        {
            // Исключаем из отслеживания
            var person = _context.Products;
            //_context.Entry(person).State = EntityState.Detached;

            _context.RemoveRange(person);
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            // Исключаем из отслеживания
            Product person = _context.Products.First(e => e.ID == item.ID);
            _context.Entry(person).State = EntityState.Detached;

            _context.Products.Update(item);
            _context.SaveChanges();
        }

        public string GetConnectionString() =>
            new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Data").GetConnectionString("AccessData");

        public string ConnectionsTest(string conStr)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    con.Open();
                    return "Успешное подключение к базе деннах Access";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах Access";
            }
        }
    }
}
