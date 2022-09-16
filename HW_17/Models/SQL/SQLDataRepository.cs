using HW_17.Data;
using HW_17.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace HW_17.Models.SQL
{
    internal class SQLDataRepository : IDataRepository<Person>
    {
        PersonContext _context;

        public SQLDataRepository(PersonContext context) => _context = context;

        public IEnumerable<Person> GetAllData
        {
            get
            {
                using (SqlConnection sql = new SqlConnection(GetConnectionString()))
                    return _context.GetAllPerson(sql);
            }
        }

        /// <summary>
        /// Строка подключения.
        /// </summary>
        /// <returns>Строка</returns>
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder conStr = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "SQLDataBase",
                IntegratedSecurity = true,
                UserID = "admin",
                Password = "admin123",
                ConnectTimeout = 10,
            };

            return conStr.ConnectionString;
        }

        /// <summary>
        /// Проверка подключения к базе деннах.
        /// </summary>
        /// <param name="conStr">Строка подключения из View</param>
        /// <returns>Успешно или нет</returns>
        public string ConnectionsTest(string conStr)
        {
            try
            {
                SqlConnection conTest = new SqlConnection(conStr);
                conTest.Open();
                if (conTest.State.HasFlag(ConnectionState.Open))
                {
                    conTest.Close();
                    return "Успешное подключение к базе деннах SQL";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах SQL";
            }
            return "Ошибка подключения к базе деннах SQL";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Person Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Person item)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.AddPersonToTable(sql, item);
            }
        }

        public void Update(Person item)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.UpdatePerson(sql, item);
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.RemovePerson(sql, id);
            }
        }

        public void RemoveAll()
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.RemoveAllDataPerson(sql);
            }
        }
    }
}
