using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Account
{
    public class RegisterRequest
    {
        [Required] public string Email { get; set; } = null!;

        [Required] public string Password { get; set; } = null!;

        [Required] public string PasswordConfirmation { get; set; } = null!;

        [Required] public string FullName { get; set; } = null!;
        public string? UserName { get; set; }
    }
}
