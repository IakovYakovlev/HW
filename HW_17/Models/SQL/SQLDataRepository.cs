using HW_17.Models.DataContext;
using HW_17.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace HW_17.Models.SQL
{
    internal class SQLDataRepository : IDataRepository<Person>
    {
        SQLContext _context;

        public SQLDataRepository(SQLContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAllData => _context.Persons.AsNoTracking();

        public void Add(Person item)
        {
            _context.Persons.Add(item);
            _context.SaveChanges();
        }

        public Person Get(int id) => _context.Persons.Find(id);

        public void Remove(int id)
        {
            // Исключаем из отслеживания
            Person person = _context.Persons.First(e => e.Id == id);

            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        public void RemoveAll()
        {
            // Исключаем из отслеживания
            var person = _context.Persons;

            _context.Persons.RemoveRange(person);
            _context.SaveChanges();
        }

        public void Update(Person item)
        {
            // Исключаем из отслеживания и обнуляем продукт.
            item.Products = null;
            Person person = _context.Persons.First(e => e.Id == item.Id);
            _context.Entry(person).State = EntityState.Detached;

            _context.Persons.Update(item);
            _context.SaveChanges();
        }

        public string GetConnectionString() => 
            new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Data").GetConnectionString("MSSQL");
            

        public string ConnectionsTest(string conStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    return "Успешное подключение к базе деннах SQL";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах SQL";
            }
        }
    }
}
