using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Animal
{
    public class DeleteAnimalRequest
    {
        [Required] public int AnimalId { get; set; }
    }
}
