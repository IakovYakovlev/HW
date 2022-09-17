using HW_17.Models.Access;
using System.Collections.Generic;

namespace HW_17.Models.SQL
{
    internal class Person
    {
        /// <summary>
        /// Номер записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Оnчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Коллекция продуктов к записи
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
    }
}
