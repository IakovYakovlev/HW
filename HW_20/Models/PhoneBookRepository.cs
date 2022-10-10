using Microsoft.EntityFrameworkCore;

namespace HW_20.Models
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
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PhoneBook item)
        {
            throw new NotImplementedException();
        }
    }
}
