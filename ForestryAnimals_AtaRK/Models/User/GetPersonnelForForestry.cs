using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.User
{
    public class GetPersonnelForForestry
    {
        [Required] public int ForestryId { get; set; }
    }
}
