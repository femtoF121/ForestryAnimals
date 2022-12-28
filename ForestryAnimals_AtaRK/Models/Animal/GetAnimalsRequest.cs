using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Animal
{
    public class GetAnimalsRequest
    {
        [Required] public int ForestryId { get; set; }
    }
}
