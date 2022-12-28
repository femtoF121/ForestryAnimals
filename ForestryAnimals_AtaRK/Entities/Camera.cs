namespace ForestryAnimals_AtaRK.Entities
{
    public class Camera
    {
        public int Id { get; set; }
        public Forestry Forestry { get; set; } = null!;
        public int ForestryId { get; set; }
        public string SerialNumber { get; set; } = null!;
    }
}
