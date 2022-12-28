using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Account;
using ForestryAnimals_AtaRK.Models.Animal;
using ForestryAnimals_AtaRK.Models.Camera;
using ForestryAnimals_AtaRK.Models.Forestry;
using ForestryAnimals_AtaRK.Models.User;

namespace ForestryAnimals_AtaRK.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<RegisterRequest, Owner>().ForMember(u => u.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<LoginRequest, User>();
            CreateMap<RegisterRequest, LoginRequest>();

            CreateMap<AddPersonnelRequest, Personnel>();

            CreateMap<AddForestryRequest, Forestry>();

            CreateMap<AddAnimalRequest, Animal>();
            CreateMap<EditAnimalRequest, Animal>();

            CreateMap<AddCameraRequest, Camera>();
        }
    }
}
