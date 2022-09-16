using HW_17.Models.SQL;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using System.Data.OleDb;
using HW_17.Models.Access;
using System.Linq;
using HW_17.Services;

namespace HW_17.Data
{
    internal class PersonContext
    {
        IDataRepository<Product> _productRepository;

        public PersonContext(IDataRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Person> GetAllPerson(SqlConnection db)
        {
            var person = db.Query<Person>("SELECT Id, Surname, Name, Patronymic, Tel, Email FROM Person");

            using (var oleCon = new OleDbConnection(_productRepository.GetConnectionString()))
            {
                oleCon.Open();
                var product = oleCon.Query<Product>("SELECT ID, Email, ProductCode, ProductName FROM Product");
                foreach (Person p in person)
                {
                    p.Products = product.Where(x => x.Email.Trim() == p.Email);
                }
            }

            return person;
        }

        public void AddPersonToTable(SqlConnection db, Person person)
        {
            db.Query<Person>($"INSERT INTO Person (Surname, Name, Patronymic, Tel, Email) " +
                             $"VALUES (N'{person.Surname}', N'{person.Name}', N'{person.Patronymic}','{person.Tel}', N'{person.Email}')");
        }


        public void UpdatePerson(SqlConnection db, Person person) =>
            db.Query<Person>($"UPDATE Person " +
                             $"SET Surname = N'{person.Surname}'," +
                             $"Name = N'{person.Name}'," +
                             $"Patronymic = N'{person.Patronymic}'," +
                             $"Tel = '{person.Tel}'," +
                             $"Email = '{person.Email}'" +
                             $"WHERE Id = '{person.Id}'");

        public void RemovePerson(SqlConnection db, int id) =>
                        db.Query<Person>($"DELETE FROM Person " +
                                         $"WHERE Id = '{id}'");

        public void RemoveAllDataPerson(SqlConnection db) => db.Query<Person>($"DELETE FROM Person");

    }
}
