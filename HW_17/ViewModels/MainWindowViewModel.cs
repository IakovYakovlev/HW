using HW_17.Infrastructure;
using HW_17.ViewModels.Base;
using HW_17.Views.Main.Accessory;
using System.Windows.Input;

namespace HW_17.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Домашняя работа 17";

        #endregion

        #endregion

        #region Команды

        #region Открыть окно для проверки соединения с базами данных.

        /// <summary>Открыть окно для проверки соеднинения с базой</summary>
        public ICommand OpenDBConnectionTestWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для проверки соеднинения с базой</summary>
        private bool CanOpenDBConnectionTestWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для проверки соеднинения с базой</summary>
        private void OnOpenDBConnectionTestWindowCommanExecuted(object p)
        {
            DBConnectionTest DBConTest = new DBConnectionTest();
            DBConTest.Show();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            OpenDBConnectionTestWindowCommand = new LambdaCommand(OnOpenDBConnectionTestWindowCommanExecuted, CanOpenDBConnectionTestWindowCommanExecute);

            #endregion
        }
    }
}
