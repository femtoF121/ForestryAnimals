using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ForestryAnimals_AtaRK.Models.Forestry
{
    public class AddForestryRequest
    {
        [Required] public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool? IsPrivate { get; set; }
        public double? Area { get; set; }
    }
}
