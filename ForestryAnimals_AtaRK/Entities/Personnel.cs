namespace ForestryAnimals_AtaRK.Entities
{
    public class Personnel : User
    {
        public int ForestryId { get; set; }
        public Forestry Forestry { get; set; } = null!;
    }
}
