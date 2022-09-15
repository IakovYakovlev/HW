using HW_17.Infrastructure;
using HW_17.Models.Access;
using HW_17.Models.SQL;
using HW_17.Services;
using HW_17.ViewModels.Base;
using System.Threading;
using System.Windows.Input;

namespace HW_17.ViewModels
{
    internal class DBConnectionTestViewModel : ViewModel
    {
        IDataRepostitory<SQLDataRepository> _sqlRepository;
        IDataRepostitory<AccessDataRepository> _accessRepository;

        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Проверка соединения с базами данных";

        #endregion

        #region SQLDataBase : string - Строка подключения к БД SQL

        /// <summary>Строка подключения к БД SQL</summary>
        private string _sqlDataBase;

        /// <summary>Строка подключения к БД SQL</summary>
        public string SQLDataBase { get => _sqlDataBase; set => Set(ref _sqlDataBase, value); }

        #endregion

        #region SQLStatus : string - Статус подключения к базе данных SQL

        /// <summary>Статус подключения к базе данных SQL</summary>
        private string _sqlStatus;

        /// <summary>Статус подключения к базе данных SQL</summary>
        public string SQLStatus { get => _sqlStatus; set => Set(ref _sqlStatus, value); }

        #endregion

        #region AccessDataBase : string - Строка подключения к БД Access

        /// <summary>Строка подключения к БД Access</summary>
        private string _accessDataBase;

        /// <summary>Строка подключения к БД Access</summary>
        public string AccessDataBase { get => _accessDataBase; set => Set(ref _accessDataBase, value); }

        #endregion

        #region AccessStatus : string - Статус подключения к базе данных Access

        /// <summary>Статус подключения к базе данных Access</summary>
        private string _accessStatus;

        /// <summary>Статус подключения к базе данных Access</summary>
        public string AccessStatus { get => _accessStatus; set => Set(ref _accessStatus, value); }

        #endregion

        #endregion

        #region Команды

        #region Проверить соединение с базой данных

        /// <summary>Проверить соединение с базой данных</summary>
        public ICommand DBConnectionTestCommand { get; }

        /// <summary>Проверка возможности выполнения - Проверить соединение с базой данных</summary>
        private bool CanDBConnectionTestCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Проверить соединение с базой данных</summary>
        private void OnDBConnectionTestCommanExecuted(object p)
        {
            ThreadPool.QueueUserWorkItem(
            o =>
            {
                // Здесь что-то делаем
                switch (p)
                {
                    case "SQL":
                        SQLStatus = _sqlRepository.ConnectionsTest(SQLDataBase);
                        break;
                    case "Access":
                        AccessStatus = _accessRepository.ConnectionsTest(AccessDataBase);
                        break;
                }
            });
        }

        #endregion

        #endregion

        public DBConnectionTestViewModel(IDataRepostitory<SQLDataRepository> sqlRepository, IDataRepostitory<AccessDataRepository> accessRepository)
        {
            _sqlRepository = sqlRepository;
            _accessRepository = accessRepository;

            #region Команды

            DBConnectionTestCommand = new LambdaCommand(OnDBConnectionTestCommanExecuted, CanDBConnectionTestCommanExecute);

            #endregion

            // Выводим строки подключения во View
            _sqlDataBase = _sqlRepository.GetConnectionString();
            _accessDataBase = _accessRepository.GetConnectionString();
        }
    }
}
