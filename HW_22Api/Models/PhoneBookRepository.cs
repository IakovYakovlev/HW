
using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Models
{
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
