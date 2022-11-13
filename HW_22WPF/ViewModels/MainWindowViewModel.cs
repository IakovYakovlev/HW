using HW_22WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System;
using IdentityShared;
using HW_22WPF.Services;

namespace HW_22WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        IPhoneBook _phoneBook;

        #region Поля

        public string Title { get; set; } = "PhoneBook";

        
        #region ListPerson : ObservableCollection<Person> - Список с клиентами

        /// <summary>Список с клиентами</summary>
        private ObservableCollection<PhoneBook> _listPhoneBook;

        /// <summary>Список с клиентами</summary>
        public ObservableCollection<PhoneBook> ListPhoneBook { get => _listPhoneBook; set => Set(ref _listPhoneBook, value); }

        #endregion

        #endregion

        public MainWindowViewModel(IPhoneBook phoneBook)
        {
           _phoneBook = phoneBook;

            FillList();
        }

        private void FillList()
        {
            ListPhoneBook = new ObservableCollection<PhoneBook>(_phoneBook.GetAll().Result);
        }
    }
}
