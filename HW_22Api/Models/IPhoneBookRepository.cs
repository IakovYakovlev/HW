namespace HW_22Api.Models
{
    public interface IPhoneBookRepository
    {
        /// <summary>
        /// Все записи из базы данных
        /// </summary>
        IEnumerable<PhoneBook> GetAllData { get; }

        /// <summary>
        /// Получить одну запись из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ода запись</returns>
        PhoneBook Get(int id);

        /// <summary>
        /// Добавить одну запись в базу данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Add(PhoneBook item);

        /// <summary>
        /// Обновить информацию одной записи в базе данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Update(PhoneBook item);

        /// <summary>
        /// Удалить одну запись из базы данных
        /// </summary>
        /// <param name="id">Номер записи</param>
        void Remove(int id);
    }
}
