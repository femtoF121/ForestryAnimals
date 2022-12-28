using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Animal;
using ForestryAnimals_AtaRK.Models.Forestry;
using System.Runtime.CompilerServices;

namespace ForestryAnimals_AtaRK.Extensions;

public static class MappingExtensions
{
    public static void MapFrom(this Forestry @this, EditForestryRequest request)
    {
        @this.Name = request.Name ?? @this.Name;
        @this.Area = request.Area ?? @this.Area;
        @this.Description = request.Description ?? @this.Description;
        @this.IsPrivate = request.IsPrivate ?? @this.IsPrivate;
        @this.Location = request.Location ?? @this.Location;
    }

    public static void MapFrom(this Animal @this, EditAnimalRequest request)
    {
        @this.Species = request.Species ?? @this.Species;
        @this.ImgPath = request.ImgPath ?? @this.ImgPath;
        @this.Name = request.Name ?? @this.Name;
        @this.Description = request.Description ?? @this.Description;
        @this.Age = request.Age ?? @this.Age;
        @this.Health = request.Health ?? @this.Health;
    }
}