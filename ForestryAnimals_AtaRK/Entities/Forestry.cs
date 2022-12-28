namespace ForestryAnimals_AtaRK.Entities
{
    public class Forestry
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool? IsPrivate { get; set; }
        public double? Area { get; set; }
        public string OwnerId { get; set; } = null!;
        public List<Camera> Cameras { get; set; } = new();
        public List<Animal> Animals { get; set; } = new();
        public List<Personnel> Personnel { get; set; } = new();
    }
}
