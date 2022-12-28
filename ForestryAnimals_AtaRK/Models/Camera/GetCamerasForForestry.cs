using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Camera
{
    public class GetCamerasForForestry
    {
        [Required] public int ForestryId { get; set; }
    }
}
