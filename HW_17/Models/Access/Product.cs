using System.Windows.Xps.Serialization;

namespace HW_17.Models.Access
{
    internal class Product
    {
        /// <summary>
        /// Номер записи
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        public string ProductName { get; set; }
    }
}
