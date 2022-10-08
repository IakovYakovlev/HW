using HW_19.Models;
using HW_19.Services;
using System.Collections.Generic;
using System.IO;

namespace HW_19.Data
{
    internal class KeeperTxt : IAnimalsSave
    {
        public void SaveAnimals(IEnumerable<IAnimals> animals)
        {
            using (StreamWriter sw = new StreamWriter("File.txt"))
            {
                foreach (IAnimals animal in animals)
                    sw.WriteLine(animal.Id + ";" + animal.Name + ";" + animal.Type);
            }
        }
    }
}
