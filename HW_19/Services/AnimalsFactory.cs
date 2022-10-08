using HW_19.Models;

namespace HW_19.Services
{
    internal class AnimalsFactory
    {
        public static IAnimals CreateNewAnimal(int id, string name, string type)
        {
            IAnimals animal;
            switch (type.ToLower())
            {
                case "земноводные": animal = new Amphibians(id, name, "Земноводные"); break;
                case "млекопитающие": animal = new Mammals(id, name, "Млекопитающие"); break;
                case "птицы": animal = new Birds(id, name, "Птицы"); break;
                default: animal = new NullAnimal(id, name, "Неизвестный тип"); break;
            }

            return animal;
        }
    }
}
