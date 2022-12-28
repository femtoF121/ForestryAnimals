namespace ForestryAnimals_AtaRK.Entities
{
    public class Owner : User
    {
        public List<Forestry> Forestries { get; set; } = new();
    }
}
