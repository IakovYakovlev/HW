using HW_17.Models.Access;
using System.Collections.Generic;

namespace HW_17.Models.SQL
{
    internal class Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
