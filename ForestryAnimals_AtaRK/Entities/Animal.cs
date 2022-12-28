namespace ForestryAnimals_AtaRK.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public Forestry Forestry { get; set; } = null!;
        public int ForestryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Species { get; set; }
        public int? Age { get; set; }
        public string? Health { get; set; }
    }
}
