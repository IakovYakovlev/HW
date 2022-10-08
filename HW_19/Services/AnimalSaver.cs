using HW_19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_19.Services
{
    internal class AnimalSaver
    {
        public IAnimalsSave Mode { get; set; }

        public AnimalSaver() { }

        public void Save(IEnumerable<IAnimals> animals)
        {
            Mode.SaveAnimals(animals);
        }
    }
}
