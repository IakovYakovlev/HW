using HW_19.ViewModels.Base;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using HW_19.Views;
using HW_19.Infrastructure;
using HW_19.Models;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;
using HW_19.Services;

namespace HW_19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля

        #region Title : string - Название окна

        /// <summary>
        /// Название окна
        /// </summary>
        public string Title { get; } = "Домашняя работа 19";

        #endregion

        #region SelectedSaveType : string - Выделенный тип для сохранения

        /// <summary>Выделенный тип для сохранения</summary>
        private string _selectedSaveType;

        /// <summary>Выделенный тип для сохранения</summary>
        public string SelectedSaveType { get => _selectedSaveType; set => Set(ref _selectedSaveType, value); }

        #endregion

        #region SaveType : List<string> - Тип для сохранения

        /// <summary>Тип для сохранения</summary>
        private List<string> _saveType;

        /// <summary>Тип для сохранения</summary>
        public List<string> SaveType { get => _saveType; set => Set(ref _saveType, value); }

        #endregion

        #region SelectedAnimal : IAnimals - Выделенная строка в списки с животными

        /// <summary>Выделенная строка в списки с животными</summary>
        private IAnimals _selectedAnimal;

        /// <summary>Выделенная строка в списки с животными</summary>
        public IAnimals SelectedAnimal { get => _selectedAnimal; set => Set(ref _selectedAnimal, value); }

        #endregion

        #region SelectedAnimal : IAnimals - Выделенная строка в списки с животными

        /// <summary>Выделенная строка в списки с животными</summary>
        private ObservableCollection<IAnimals> _listAnimals;

        /// <summary>Выделенная строка в списки с животными</summary>
        public ObservableCollection<IAnimals> ListAnimals { get => _listAnimals; set => Set(ref _listAnimals, value); }

        #endregion

        #endregion

        #region Команды

        #region Открыть окно для редактирования данных

        /// <summary>Открыть окно для редактирования данных</summary>
        public ICommand OpenAddOrCorrectAnimalWindowCommand { get; }

        /// <summary>Проверка возможности выполнения - Открыть окно для редактирования данных</summary>
        private bool CanOpenAddOrCorrectAnimalWindowCommanExecute(object p) => true;

        /// <summary>Логика выполнения - Открыть окно для редактирования данных</summary>
        private void OnOpenAddOrCorrectAnimalWindowCommanExecuted(object p)
        {
            AddOrCorrectAnimal addOrCorrectAnimal;

            if (Convert.ToInt32(p) != 0 && SelectedAnimal == null) return;

            if (Convert.ToInt32(p) == 0)
            {
                addOrCorrectAnimal = new AddOrCorrectAnimal();
                if(addOrCorrectAnimal.ShowDialog() == true)
                {
                    ListAnimals.Add(AnimalsFactory.CreateNewAnimal(addOrCorrectAnimal.Id, addOrCorrectAnimal.CName, addOrCorrectAnimal.CType));
                }
            }
            if(Convert.ToInt32(p) == 1)
            {
                addOrCorrectAnimal = new AddOrCorrectAnimal();
                addOrCorrectAnimal.Id = SelectedAnimal.Id;
                addOrCorrectAnimal.CName = SelectedAnimal.Name;
                addOrCorrectAnimal.CType = SelectedAnimal.Type;
                if(addOrCorrectAnimal.ShowDialog() == true)
                {
                    ListAnimals.Remove(SelectedAnimal);
                    ListAnimals.Add(AnimalsFactory.CreateNewAnimal(addOrCorrectAnimal.Id, addOrCorrectAnimal.CName, addOrCorrectAnimal.CType));
                }
            }
        }

        #endregion

        #region Удалить выделенную запсь

        /// <summary>Удалить выделенную запсь</summary>
        public ICommand DeleteCommand { get; }

        /// <summary>Проверка возможности выполнения - Удалить выделенную запсь</summary>
        private bool CanDeleteCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Удалить выделенную запсь</summary>
        private void OnDeleteCommandExecuted(object p)
        {
            if(SelectedAnimal == null) return;
            ListAnimals.Remove(SelectedAnimal);

        }

        #endregion

        #region Сохранение в файл

        /// <summary>Сохранение в файл</summary>
        public ICommand SaveToFileCommand { get; }

        /// <summary>Проверка возможности выполнения - Сохранение в файл</summary>
        private bool CanSaveToFileCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Сохранение в файл</summary>
        private void OnSaveToFileCommandExecuted(object p)
        {
            if(SelectedSaveType == null) return;
            SaveFactory.SaveToFile(SelectedSaveType, ListAnimals);
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            OpenAddOrCorrectAnimalWindowCommand = new LambdaCommand(OnOpenAddOrCorrectAnimalWindowCommanExecuted, CanOpenAddOrCorrectAnimalWindowCommanExecute);
            DeleteCommand = new LambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            SaveToFileCommand = new LambdaCommand(OnSaveToFileCommandExecuted, CanSaveToFileCommandExecute);

            #endregion

            _listAnimals = new();
            _saveType = new List<string>(new string[]{ "-", "PDF", "TXT" });
        }
    }
}
