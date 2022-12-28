using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Camera
{
    public class DeleteCameraRequest
    {
        [Required] public int Id { get; set; } 
    }
}
