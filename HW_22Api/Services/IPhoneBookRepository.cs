using HW_22Api.Data;
using IdentityShared;
using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Services
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

    public class PhoneBookRepository : IPhoneBookRepository
    {
        PhoneBookContext _context;

        public PhoneBookRepository(PhoneBookContext context)
        {
            _context = context;
        }

        public IEnumerable<PhoneBook> GetAllData => _context.PhoneBooks.AsNoTracking();

        public PhoneBook Get(int id) => _context.PhoneBooks.Find(id);

        public void Add(PhoneBook item)
        {
            item.Id = 0;
            _context.PhoneBooks.Add(item);
            _context.SaveChanges();
        }

        public void Update(PhoneBook item)
        {
            _context.PhoneBooks.Update(item);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            _context.PhoneBooks.Remove(Get(id));
            _context.SaveChanges();
        }
    }
}
