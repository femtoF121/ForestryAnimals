using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.User
{
    public class AddPersonnelRequest
    {
        [Required] public string UserName { get; set; } = null!;

        [Required] public string Password { get; set; } = null!;

        [Required] public string PasswordConfirmation { get; set; } = null!;

        [Required] public string FullName { get; set; } = null!;
        [Required] public int ForestryId { get; set; }
    }
}
