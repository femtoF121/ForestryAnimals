using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Animal
{
    public class EditAnimalRequest
    {
        [Required] public int AnimalId { get; set; }
        public int? ForestryId { get; set; }
        public string? Species { get; set; }
        public string? ImgPath { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Age { get; set; }
        public string? Health { get; set; }
    }
}
