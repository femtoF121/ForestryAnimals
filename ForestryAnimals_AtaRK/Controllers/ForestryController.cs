using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Extensions;
using ForestryAnimals_AtaRK.Models.Forestry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    [Authorize(Roles = "owner, admin")]
    public class ForestryController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ForestryController(UserManager<User> userManager,
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
        public async Task<IActionResult> AddForestry([FromForm] AddForestryRequest request)
        {
            Forestry forestry = _mapper.Map<AddForestryRequest, Forestry>(request!);
            forestry.OwnerId = _userManager.GetUserId(User);
            _context.Forestries.Add(forestry);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllForestriesForOwner()
        {
            var forestries = _context.Forestries.Where(f => f.OwnerId == _userManager.GetUserId(User));
            return Ok(forestries);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditForestry([FromForm] EditForestryRequest request)
        {
            var forestry = _context.Forestries
                .Include(f => f.Personnel).Include(f => f.Cameras).Include(f => f.Animals)
                .Where(f => f.Id == request.Id)
                .FirstOrDefault();

            if (forestry is null)
                return BadRequest(_localizer["Forestry does not exist with this ID"].Value);

            forestry.MapFrom(request);

            _context.Forestries.Update(forestry);

            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest(_localizer["Something went wrong"].Value);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteForestry([FromForm] DeleteForestryRequest request)
        {
            var forestry = _context.Forestries.Where(f => f.Id == request.Id).FirstOrDefault();
            if (forestry == null) return BadRequest(_localizer["Forestry does not exist with this ID"].Value);
            var curUser = _context.Owners.Where(o => o.Id == _userManager.GetUserId(User)).FirstOrDefault();
            if (forestry.OwnerId == curUser!.Id)
            {
                _context.Forestries.Remove(forestry!);
                return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest(_localizer["Something went wrong"].Value);
            }
            return BadRequest(_localizer["Something went wrong"].Value);
        }
    }
}