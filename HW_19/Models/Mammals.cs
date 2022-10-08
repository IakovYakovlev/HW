namespace HW_19.Models
{
    internal class Mammals : IAnimals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Mammals() { }

        public Mammals(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
