using HW_19.Models;
using System.Collections.Generic;

namespace HW_19.Services
{
    internal interface IAnimalsSave
    {
        void SaveAnimals(IEnumerable<IAnimals> animals);
    }
}
