using System.Collections.Generic;

namespace HW_17.Services
{
    internal interface IDataRepository<T> where T : class
    {
        public string GetConnectionString();

        public string ConnectionsTest(string conStr);

        IEnumerable<T> GetAllData { get; }
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Remove(int id);
        void RemoveAll();
    }
}
