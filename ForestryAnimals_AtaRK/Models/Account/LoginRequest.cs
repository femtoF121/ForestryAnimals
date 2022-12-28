using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Account
{
    public class LoginRequest
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
