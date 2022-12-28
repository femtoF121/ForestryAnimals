using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Forestry
{
    public class DeleteForestryRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
