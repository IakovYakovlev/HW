using HW_22WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System;
using IdentityShared;
using HW_22WPF.Services;
using System.Windows.Input;
using HW_22WPF.Infrastructure.Commands;
using HW_22WPF.Views.Main.Accessory;
using System.Windows;

namespace HW_22WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        IPhoneBookRepository _phoneBook;

        #region Поля

        /// <summary>Заголовок окна</summary>
        public string Title { get; set; } = "PhoneBook";

        /// <summary>Токен пользователя</summary>
        private string Token { get; set; }

        #region UserRole : string - Роль пользователя

        /// <summary>Роль пользователя</summary>
        private string _userRole;

        /// <summary>Роль пользователя</summary>
        public string UserRole { get => _userRole; set => Set(ref _userRole, value); }

        #endregion

        #region UserEmail : string - Электронная почта пользователя

        /// <summary>Электронная почта пользователя</summary>
        private string _userEmail;

        /// <summary>Электронная почта пользователя</summary>
        public string UserEmail { get => _userEmail; set => Set(ref _userEmail, value); }

        #endregion

        #region ListPerson : ObservableCollection<PhoneBook> - Список с клиентами

        /// <summary>Список с клиентами</summary>
        private ObservableCollection<PhoneBook> _listPhoneBook;

        /// <summary>Список с клиентами</summary>
        public ObservableCollection<PhoneBook> ListPhoneBook { get => _listPhoneBook; set => Set(ref _listPhoneBook, value); }

        #endregion

        #region SelectedPerson : PhoneBook - Выделенный клиент

        /// <summary>Выделенный клиент</summary>
        private PhoneBook _selectedPerson;

        /// <summary>Выделенный клиент</summary>
        public PhoneBook SelectedPerson { get => _selectedPerson; set => Set(ref _selectedPerson, value); }

        #endregion

        #endregion

        #region Команды

        #region Открыть окно для проверки добавления или редактирования

        /// <summary>Открыть окно для проверки добавления или редактирования</summary>
        public ICommand OpenAddOrUpdateWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для проверки добавления или редактирования</summary>
        private bool CanOpenAddOrUpdateWindowCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для проверки добавления или редактирования</summary>
        private void OnOpenAddOrUpdateWindowCommandExecuted(object p)
        {
            if(Token != null)
            {
                AddOrUpdate add = new AddOrUpdate();

                int flag = Convert.ToInt32(p);

                if (flag == 0)
                {
                    PhoneBook phoneBook = new PhoneBook();

                    if (add.ShowDialog() == true)
                    {
                        phoneBook.Id = add.Id;
                        phoneBook.Surname = add.Surname;
                        phoneBook.Name = add.FullName;
                        phoneBook.Patronymic = add.Patronymic;
                        phoneBook.PhoneNumber = add.PhoneNumber;
                        phoneBook.Adress = add.Adress;
                        phoneBook.Description = add.Description;

                        _phoneBook.Add(phoneBook, Token);
                        
                        FillList();
                    }
                }
                else if(flag == 1 && SelectedPerson != null)
                {
                    add.Id = SelectedPerson.Id;
                    add.Surname = SelectedPerson.Surname;
                    add.FullName = SelectedPerson.Name;
                    add.Patronymic = SelectedPerson.Patronymic;
                    add.PhoneNumber = SelectedPerson.PhoneNumber;
                    add.Adress = SelectedPerson.Adress;
                    add.Description = SelectedPerson.Description;

                    if(add.ShowDialog() == true)
                    {
                        SelectedPerson.Surname = add.Surname.Trim();
                        SelectedPerson.Name = add.FullName.Trim();
                        SelectedPerson.Patronymic = add.Patronymic.Trim();
                        SelectedPerson.PhoneNumber = add.PhoneNumber.Trim();
                        SelectedPerson.Adress = add.Adress.Trim();
                        SelectedPerson.Description = add.Description.Trim();

                        _phoneBook.Update(SelectedPerson, Token);

                        FillList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Необходимо зайти в систему", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Открыть окно для входа в систему

        /// <summary>Открыть окно для входа в систему</summary>
        public ICommand OpenLoginWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для входа в систему</summary>
        private bool CanOpenLoginWindowCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для входа в систему</summary>
        private void OnOpenLoginWindowCommandExecuted(object p)
        {
            LoginWindow lw = new LoginWindow();

            if(lw.ShowDialog() == true)
            {
                UserEmail = lw.UserEmail;
                Token = lw.Token;
                UserRole = lw.UserRole;
            }
        }

        #endregion

        #region Удалить выделенную запись

        /// <summary>Удалить выделенную запись</summary>
        public ICommand DeleteSelectedPersonCommand { get; }

        /// <summary>Проверка возможности выполнения - Удалить выделенную запись</summary>
        private bool CanDeleteSelectedPersonCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Удалить выделенную запись</summary>
        private void OnDeleteSelectedPersonCommandExecuted(object p)
        {
            if(Token != null)
            {
                if (SelectedPerson != null)
                {
                    _phoneBook.Remove(SelectedPerson.Id, Token);

                    FillList();
                }
            }   
            else
            {
                MessageBox.Show("Необходимо зайти в систему", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Открыть окно администратора

        /// <summary>Открыть окно администратора</summary>
        public ICommand OpenAdministratorWindowCommand { get; }

        /// <summary>Проверка возможности ыполнения - Открыть окно администратора</summary>
        private bool CanOpenAdministratorWindowCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно администратора</summary>
        private void OnOpenAdministratorWindowCommandExecuted(object p)
        {
            if (UserRole == null || UserRole == "" || UserRole != "Admins")
            {
                MessageBox.Show("У вас не достаточно прав", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AdministratorWindow aw = new AdministratorWindow();
            aw.Show();
        }

        #endregion

        #region Открыть окно регистрации

        public ICommand OpenRegistrationWindowCommand { get; }

        private bool CanOpenRegistrationWindowCommandExecute(object p) => true;

        private void OnOpenRegistrationWindowCommandExecuted(object p)
        {
            AddNewUser anu = new AddNewUser();

            if (anu.ShowDialog() == true)
                MessageBox.Show("Новый пользователя успешно добавлен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #endregion

        public MainWindowViewModel(IPhoneBookRepository phoneBook)
        {
           _phoneBook = phoneBook;

            #region Команды

            OpenAddOrUpdateWindowCommand = new LambdaCommand(OnOpenAddOrUpdateWindowCommandExecuted, CanOpenAddOrUpdateWindowCommandExecute);
            OpenLoginWindowCommand = new LambdaCommand(OnOpenLoginWindowCommandExecuted, CanOpenLoginWindowCommandExecute);
            DeleteSelectedPersonCommand = new LambdaCommand(OnDeleteSelectedPersonCommandExecuted, CanDeleteSelectedPersonCommandExecute);
            OpenAdministratorWindowCommand = new LambdaCommand(OnOpenAdministratorWindowCommandExecuted, CanOpenAdministratorWindowCommandExecute);
            OpenRegistrationWindowCommand = new LambdaCommand(OnOpenRegistrationWindowCommandExecuted, CanOpenRegistrationWindowCommandExecute);

            #endregion

            FillList();
        }

        private void FillList()
        {
            ListPhoneBook = new ObservableCollection<PhoneBook>(_phoneBook.GetAllData().Result);
        }
    }
}
