using ForestryAnimals_AtaRK.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForestryAnimals_AtaRK
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Camera> Cameras { get; set; } = null!;
        public DbSet<Forestry> Forestries { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Personnel> Personnel { get; set; } = null!;

        public ApplicationContext (DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
