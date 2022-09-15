using HW_17.Services;
using System.Data;
using System.Data.OleDb;

namespace HW_17.Models.Access
{
    internal class AccessDataRepository : IDataRepostitory<AccessDataRepository>
    {
        public AccessDataRepository() { }

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
    }
}
