using HW_17.Infrastructure;
using HW_17.ViewModels.Base;
using HW_17.Views.Main.Accessory;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HW_17.Models.SQL;
using HW_17.Models.Access;
using HW_17.Services;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Linq;

namespace HW_17.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        IDataRepository<Person> _personRepostitory;
        IDataRepository<Product> _productRepostitory;

        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Домашняя работа 17";

        #endregion

        #region ListPerson : ObservableCollection<Person> - Список с клиентами

        /// <summary>Список с клиентами</summary>
        private ObservableCollection<Person> _listPerson;

        /// <summary>Список с клиентами</summary>
        public ObservableCollection<Person> ListPerson { get => _listPerson; set => Set(ref _listPerson, value); }

        #endregion

        #region ListPersonSelectedItem : Person - Выделенный клиент

        /// <summary>Выделенный клиент</summary>
        private Person _listPersonSelectedItem;

        /// <summary>Выделенный клиент</summary>
        public Person ListPersonSelectedItem { get => _listPersonSelectedItem; set => Set(ref _listPersonSelectedItem, value); }

        #endregion

        #region ListProductSelectedItem : Product - Выделенный продукт

        /// <summary>Выделенный продукт</summary>
        private Product _listProductSelectedItem;

        /// <summary>Выделенный продукт</summary>
        public Product ListProductSelectedItem { get => _listProductSelectedItem; set => Set(ref _listProductSelectedItem, value); }

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

        #region Открыть окно для добавляения или редактирования пользователя

        /// <summary>Открыть окно для добавляения или редактирования пользователя</summary>
        public ICommand OpenPersonWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для добавляения или редактирования пользователя</summary>
        private bool CanOpenPersonWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для добавляения или редактирования пользователя</summary>
        private void OnOpenPersonWindowCommanExecuted(object p)
        {
            PersonWindow personeWindow;
            Person person;

            if (Convert.ToInt32(p) != 0 && ListPersonSelectedItem == null) return;

            if (Convert.ToInt32(p) == 0)
            {
                personeWindow = new PersonWindow();
                if (personeWindow.ShowDialog() == true)
                {
                    person = new Person();
                    person.Surname = personeWindow.Surname;
                    person.Name = personeWindow.Name;
                    person.Patronymic = personeWindow.Patronymic;
                    person.Tel = personeWindow.Tel;
                    person.Email = personeWindow.Email;

                    _personRepostitory.Add(person);
                    MessageBox.Show("Успешное добавление", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                personeWindow = new PersonWindow();
                personeWindow.Id = ListPersonSelectedItem.Id;
                personeWindow.Surname = ListPersonSelectedItem.Surname;
                personeWindow.Name = ListPersonSelectedItem.Name;
                personeWindow.Patronymic = ListPersonSelectedItem.Patronymic;
                personeWindow.Tel = ListPersonSelectedItem.Tel;
                personeWindow.Email = ListPersonSelectedItem.Email;

                if (personeWindow.ShowDialog() == true)
                {
                    person = new Person();
                    person.Id = personeWindow.Id;
                    person.Surname = personeWindow.Surname;
                    person.Name = personeWindow.Name;
                    person.Patronymic = personeWindow.Patronymic;
                    person.Tel = personeWindow.Tel;
                    person.Email = personeWindow.Email;

                    _personRepostitory.Update(person);
                    MessageBox.Show("Успешное изменение", "Изменения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            ListPerson.Clear();
            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);

        }

        #endregion

        #region Удаление пользователя

        /// <summary>Удаление пользователя</summary>
        public ICommand DeletePersonCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление пользователя</summary>
        private bool CanDeletePersonCommanExecute(object p) => ListPersonSelectedItem != null;

        /// <summary>Логика выполнения - Удаление пользователя</summary>
        private void OnDeletePersonCommanExecuted(object p)
        {
            _personRepostitory.Remove(ListPersonSelectedItem.Id);
            ListPerson.Remove(ListPersonSelectedItem);
        }

        #endregion

        #region Открыть окно для добавляения или редактирования продукта

        /// <summary>Открыть окно для добавляения или редактирования продукта</summary>
        public ICommand OpenProductWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для добавляения или редактирования продукта</summary>
        private bool CanOpenProductWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для добавляения или редактирования продукта</summary>
        private void OnOpenProductWindowCommanExecuted(object p)
        {
            // Если не выделили клиента и нажали на кнопку добавить нового.
            if (ListPersonSelectedItem == null) return;

            // Если не выделили продукт и нажали на кнопку редактировать.
            if (Convert.ToInt32(p) != 0 && ListProductSelectedItem == null) return;

            ProductWindow productWindow;
            Product product;

            if (Convert.ToInt32(p) == 0)
            {
                // Создаем новый продукт
                productWindow = new ProductWindow();
                productWindow.Email = ListPersonSelectedItem.Email;

                if (productWindow.ShowDialog() == true)
                {
                    product = new Product();
                    product.Email = productWindow.Email;
                    product.ProductCode = productWindow.ProductCode;
                    product.ProductName = productWindow.ProductName;

                    _productRepostitory.Add(product);
                    MessageBox.Show("Успешное добавление", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                // Делаем изменения выделенного продукта
                productWindow = new ProductWindow();
                productWindow.ID = ListProductSelectedItem.ID;
                productWindow.Email = ListProductSelectedItem.Email;
                productWindow.ProductCode = ListProductSelectedItem.ProductCode;
                productWindow.ProductName = ListProductSelectedItem.ProductName;

                if (productWindow.ShowDialog() == true)
                {
                    product = new Product();
                    product.ID = productWindow.ID;
                    product.Email = productWindow.Email;
                    product.ProductCode = productWindow.ProductCode;
                    product.ProductName = productWindow.ProductName;

                    _productRepostitory.Update(product);
                    MessageBox.Show("Успешное изменение", "Изменения", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            ListPerson.Clear();
            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);

        }

        #endregion

        #region Удаление продукта

        /// <summary>Удаление продукта</summary>
        public ICommand DeleteProductCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление продукта</summary>
        private bool CanDeleteProductCommanExecute(object p) => ListProductSelectedItem != null;

        /// <summary>Логика выполнения - Удаление продукта</summary>
        private void OnDeleteProductCommanExecuted(object p)
        {
            _productRepostitory.Remove(ListProductSelectedItem.ID);
            ListPersonSelectedItem.Products = _productRepostitory.GetAllData.Where(x => x.Email == ListPersonSelectedItem.Email);
            
            // Рефрешим выделенную запись.
            var s = ListPersonSelectedItem;
            ListPersonSelectedItem = null;
            ListPersonSelectedItem = s;
        }

        #endregion

        #region Удаление всего

        /// <summary>Удаление пользователя</summary>
        public ICommand DeleteAllCommand { get; }

        /// <summary>Проверка возможности выполнения - Удаление пользователя</summary>
        private bool CanDeleteAllCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Удаление пользователя</summary>
        private void OnDeleteAllCommandExecuted(object p)
        {
            _productRepostitory.RemoveAll();
            _personRepostitory.RemoveAll();
            ListPerson.Clear();
        }

        #endregion

        #endregion

        public MainWindowViewModel(IDataRepository<Person> personRepostitory, IDataRepository<Product> productRepostitory)
        {
            _personRepostitory = personRepostitory;
            _productRepostitory = productRepostitory;

            #region Команды

            OpenDBConnectionTestWindowCommand = new LambdaCommand(OnOpenDBConnectionTestWindowCommanExecuted, CanOpenDBConnectionTestWindowCommanExecute);
            OpenPersonWindowCommand = new LambdaCommand(OnOpenPersonWindowCommanExecuted, CanOpenPersonWindowCommanExecute);
            DeletePersonCommand = new LambdaCommand(OnDeletePersonCommanExecuted, CanDeletePersonCommanExecute);
            DeleteAllCommand = new LambdaCommand(OnDeleteAllCommandExecuted, CanDeleteAllCommandExecute);
            OpenProductWindowCommand = new LambdaCommand(OnOpenProductWindowCommanExecuted, CanOpenProductWindowCommanExecute);
            DeleteProductCommand = new LambdaCommand(OnDeleteProductCommanExecuted, CanDeleteProductCommanExecute);

            #endregion

            ListPerson = new ObservableCollection<Person>(_personRepostitory.GetAllData);
        }
    }
}
