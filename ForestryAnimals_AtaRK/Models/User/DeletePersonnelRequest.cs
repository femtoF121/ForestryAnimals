using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.User
{
    public class DeletePersonnelRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
    }
}
