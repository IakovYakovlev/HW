using System.Collections.Generic;

namespace HW_17.Services
{
    internal interface IDataRepository<T> where T : class
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        /// <returns>Строка</returns>
        public string GetConnectionString();

        /// <summary>
        /// Проверка подключения к базе деннах.
        /// </summary>
        /// <param name="conStr">Строка подключения из View</param>
        /// <returns>Успешно или нет</returns>
        public string ConnectionsTest(string conStr);

        /// <summary>
        /// Все записи из базы данных
        /// </summary>
        IEnumerable<T> GetAllData { get; }

        /// <summary>
        /// Получить одну запись из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ода запись</returns>
        T Get(int id);

        /// <summary>
        /// Добавить одну запись в базу данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Add(T item);

        /// <summary>
        /// Обновить информацию одной записи в базе данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Update(T item);

        /// <summary>
        /// Удалить одну запись из базы данных
        /// </summary>
        /// <param name="id">Номер записи</param>
        void Remove(int id);

        /// <summary>
        /// Удалить все записи изи базы данных
        /// </summary>
        void RemoveAll();
    }
}
