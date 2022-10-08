namespace HW_19.Models
{
    internal class Amphibians : IAnimals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Amphibians() { }

        public Amphibians(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
