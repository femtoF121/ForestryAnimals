using ForestryAnimals_AtaRK.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForestryAnimals_AtaRK
{
    public class RoleInitializer
    {
        public static async Task RoleInit(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Admin123";

            if (await roleManager.FindByNameAsync("admin") is null)
                await roleManager.CreateAsync(new IdentityRole("admin"));

            if (await roleManager.FindByNameAsync("owner") is null)
                await roleManager.CreateAsync(new IdentityRole("owner"));

            if (await roleManager.FindByNameAsync("personnel") is null)
                await roleManager.CreateAsync(new IdentityRole("personnel"));

            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                User admin = new() { Email = adminEmail, UserName = "admin", FullName = "Admin" };
                IdentityResult result = await userManager.CreateAsync(admin!, password);
                if (result.Succeeded)
                {
                    //await userManager.ConfirmEmailAsync(admin!, await userManager.GenerateEmailConfirmationTokenAsync(admin));
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
