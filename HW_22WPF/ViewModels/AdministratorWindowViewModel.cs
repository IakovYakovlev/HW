using HW_22WPF.Infrastructure.Commands;
using HW_22WPF.Services;
using HW_22WPF.ViewModels.Base;
using HW_22WPF.Views.Main.Accessory;
using IdentityShared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;

namespace HW_22WPF.ViewModels
{
    internal class AdministratorWindowViewModel : ViewModel
    {
        IUserService _userService;

        #region Поля

        public string Title { get; set; } = "Администратор";

        #region Спикос пользователей

        /// <summary>Список пользователей</summary>
        private ObservableCollection<Users> _listUsers;

        /// <summary>Список пользователей</summary>
        public ObservableCollection<Users> ListUsers { get => _listUsers; set => Set(ref _listUsers, value); }

        #endregion

        #region Выделенный пользователь

        /// <summary>Выделенный пользователь</summary>
        private Users _selectedUser;

        /// <summary>Выделенный пользователь</summary>
        public Users SelectedUser { get => _selectedUser; set => Set(ref _selectedUser, value); }

        #endregion

        #endregion

        #region Команды

        #region Открыть окно для создания нового пользователя

        /// <summary>Открыть окно для создания нового пользователя</summary>
        public ICommand OpenWindowAddNewUserCommand { get; }

        /// <summary>Проверка возможности - Открыть окно для создания нового пользователя</summary>
        private bool CanOpenWindowAddNewUserCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для создания нового пользователя</summary>
        private void OnOpenWindowAddNewUserCommandExecuted(object p) 
        {
            AddNewUser anu = new AddNewUser();

            if (anu.ShowDialog() == true)
            {
                FillList();
            }
        }

        #endregion

        #region Удалить выделенного пользователя

        /// <summary>Удалить выделенного пользователя</summary>
        public ICommand DeleteSelectedUserCommand { get; }

        /// <summary>Проверка возможности выполнения - Удалить выделенного пользователя</summary>
        private bool CanDeleteSelectedUserCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Удалить выделенного пользователя</summary>
        private async void OnDeleteSelectedUserCommandExecuted(object p)
        {
            if (SelectedUser == null) return;

            await _userService.DeleteUserAsync(SelectedUser.Id);

            FillList();
        }

        #endregion

        #endregion

        public AdministratorWindowViewModel(IUserService userService)
        {
            _userService = userService;

            #region Команды

            OpenWindowAddNewUserCommand = new LambdaCommand(OnOpenWindowAddNewUserCommandExecuted, CanOpenWindowAddNewUserCommandExecute);
            DeleteSelectedUserCommand = new LambdaCommand(OnDeleteSelectedUserCommandExecuted, CanDeleteSelectedUserCommandExecute);

            #endregion

            FillList();
        }

        private void FillList()
        {
            ListUsers = new ObservableCollection<Users>(_userService.GetAllUsers().Result);
        }
    }
}
