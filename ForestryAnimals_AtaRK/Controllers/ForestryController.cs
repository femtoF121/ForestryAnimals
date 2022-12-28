using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Forestry;
using ForestryAnimals_AtaRK.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class ForestryController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ForestryController(UserManager<User> userManager,
                               ApplicationContext context,
                               IMapper mapper,
                               RoleManager<IdentityRole> roleManager,
                               IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _ = RoleInitializer.RoleInit(userManager, roleManager, configuration);
        }

        [Authorize(Roles = "owner")]
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
        [Authorize(Roles = "owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllForestriesForOwner()
        {
            var forestries = _context.Forestries.Where(f => f.OwnerId == _userManager.GetUserId(User));
            return Ok(forestries);
        }

        [HttpPatch]
        [Authorize(Roles = "owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditForestry([FromForm] EditForestryRequest request)
        {
            var forestry = _context.Forestries.Where(f => f.Id == request.Id).FirstOrDefault();
            if (forestry == null) return BadRequest("Forestry does not exist with this ID");
            forestry = _mapper.Map<EditForestryRequest, Forestry>(request);
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest("An error occured during saving changes");
        }

        [HttpDelete]
        [Authorize(Roles = "owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteForestry([FromForm] DeleteForestryRequest request)
        {
            var forestry = _context.Forestries.Where(f => f.Id == request.Id).FirstOrDefault();
            if (forestry == null) return BadRequest("Forestry does not exist with this ID");
            var curUser = _context.Owners.Where(o => o.Id == _userManager.GetUserId(User)).FirstOrDefault();
            if (forestry.OwnerId == curUser!.Id)
            {
                _context.Forestries.Remove(forestry!);
                return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
            }
            return BadRequest();
        }
    }
}
