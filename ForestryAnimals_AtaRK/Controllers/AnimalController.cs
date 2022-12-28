using AutoMapper;
using ForestryAnimals_AtaRK;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Extensions;
using ForestryAnimals_AtaRK.Models.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AnimalController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public AnimalController(UserManager<User> userManager,
                               ApplicationContext context,
                               IMapper mapper,
                               RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration,
                               IStringLocalizer<SharedResource> localizer)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _ = RoleInitializer.RoleInit(userManager, roleManager, configuration);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAnimal([FromForm] AddAnimalRequest request)
        {
            Animal animal = _mapper.Map<AddAnimalRequest, Animal>(request!);
            animal.ForestryId = request.ForestryId;
            animal.Forestry = _context.Forestries.Where(f => f.Id == request.ForestryId).FirstOrDefault()!;
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAnimal([FromForm] EditAnimalRequest request)
        {
            var animal = _context.Animals
                .Where(a => a.Id == request.AnimalId)
                .FirstOrDefault();

            if (animal is null)
                return BadRequest(_localizer["Animal does not exist with this ID"].Value);

            animal.MapFrom(request);

            _context.Animals.Update(animal);

            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest(_localizer["An error occured during saving changes"].Value);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAnimalsForForestry([FromQuery] GetAnimalsRequest request)
        {
            var animals = _context.Animals.Where(a => a.ForestryId == request.ForestryId);
            return Ok(animals);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAnimal([FromForm] DeleteAnimalRequest request)
        {
            var animal = _context.Animals.Where(a => a.Id == request.AnimalId).FirstOrDefault();
            if (animal == null) return BadRequest(_localizer["Animal does not exist with this ID"].Value);
            _context.Animals.Remove(animal);
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest(_localizer["Something went wrong"].Value);
        }
    }
}
