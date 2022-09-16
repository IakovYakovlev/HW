using HW_17.Data;
using HW_17.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace HW_17.Models.Access
{
    internal class AccessDataRepository : IDataRepository<Product>
    {
        ProductContext _context;

        public AccessDataRepository(ProductContext context) => _context = context;

        /// <summary>
        /// Список всех продуктов
        /// </summary>
        public IEnumerable<Product> GetAllData
        {
            get
            {
                using (OleDbConnection sql = new OleDbConnection(GetConnectionString()))
                    return _context.GetAllProducts(sql);
            }
        }

        /// <summary>
        /// Строка подключения.
        /// </summary>
        /// <returns>Строка</returns>
        public string GetConnectionString()
        {
            OleDbConnectionStringBuilder conStr = new OleDbConnectionStringBuilder();
            conStr.Provider = "Microsoft.ACE.OLEDB.12.0";
            conStr.DataSource = "..\\..\\..\\Data\\AccessDataBase.mdb";
            conStr["User ID"] = "Admin";
            conStr["Jet OLEDB:Database Password"] = "admin123";

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
                OleDbConnection conTest = new OleDbConnection(conStr);
                conTest.Open();
                if (conTest.State.HasFlag(ConnectionState.Open))
                {
                    conTest.CloseAsync();
                    return "Успешное подключение к базе деннах Access";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах Access";
            }
            return "Ошибка подключения к базе деннах Access";
        }

        public Product Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Product item)
        {
            _context.AddProductToTable(GetConnectionString(), item);
        }

        public void Update(Product item)
        {
            _context.UpdateProduct(GetConnectionString(), item);
        }

        public void Remove(int id)
        {
            using (OleDbConnection sql = new OleDbConnection(GetConnectionString()))
            {
                _context.RemoveProduct(GetConnectionString(), id);
            }
        }

        public void RemoveAll()
        {
            using (OleDbConnection sql = new OleDbConnection(GetConnectionString()))
            {
                _context.RemoveAllDataProduct(sql);
            }
        }
    }
}
