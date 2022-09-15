using HW_17.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HW_17.Models.SQL
{
    internal class SQLDataRepository : IDataRepostitory<SQLDataRepository>
    {
        public SQLDataRepository() { }

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
    }
}
