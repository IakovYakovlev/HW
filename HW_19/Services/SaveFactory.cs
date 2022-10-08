using HW_19.Data;
using HW_19.Models;
using System.Collections.Generic;

namespace HW_19.Services
{
    internal class SaveFactory
    {
        public static void SaveToFile(string type, IEnumerable<IAnimals> animals)
        {
            AnimalSaver save = new AnimalSaver();

            switch (type)
            {
                case "PDF":
                    save.Mode = new KeeperPdf();
                    save.Save(animals);
                    break;
                case "TXT":
                    save.Mode = new KeeperTxt();
                    save.Save(animals);
                    break;
                default: return;
            }
        }
    }
}
