 using Microsoft.AspNetCore.Identity;

namespace ForestryAnimals_AtaRK.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
