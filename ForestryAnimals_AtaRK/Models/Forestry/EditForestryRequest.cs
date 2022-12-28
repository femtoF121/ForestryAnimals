using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Forestry
{
    public class EditForestryRequest : AddForestryRequest
    {
        [Required]
        public int Id { get; set; }
        public new string? Name { get; set; }
    }
}
