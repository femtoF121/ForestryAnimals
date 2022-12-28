using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Forestry;
using ForestryAnimals_AtaRK.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager,
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPersonnel([FromForm] AddPersonnelRequest request)
        {
            var forestries = _context.Forestries.Where(f => f.OwnerId == _userManager.GetUserId(User)).ToList();
            if (forestries.Count == 0) return BadRequest("You have to create forestry firstly");
            if (request.Password == request.PasswordConfirmation)
            {
                Personnel personnel = _mapper.Map<AddPersonnelRequest, Personnel>(request!);
                personnel.ForestryId = request.ForestryId;
                personnel.Forestry = _context.Forestries.Where(f => f.Id == request.ForestryId).First();
                var result = await _userManager.CreateAsync(personnel!, request.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(personnel!, "personnel");
                    if (result.Succeeded)
                    {
                        //SendEmail(model, callbackUrl, callbackDomain, url, user);
                        return Ok();
                    }
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Passwords are not the same");
        }

        [HttpGet]
        [Authorize(Roles = "owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllPersonnelForForestry([FromQuery] GetPersonnelForForestry request)
        {
            var personnel = _context.Personnel.Where(p => p.ForestryId == request.ForestryId);
            return Ok(personnel);
        }

        [HttpDelete]
        [Authorize(Roles = "owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePersonnel([FromForm] DeletePersonnelRequest request)
        {
            var personnel = _context.Personnel.Where(p => p.UserName == request.UserName).FirstOrDefault();
            if (personnel == null) return BadRequest("Personnel does not exist with this username");
            _context.Personnel.Remove(personnel);
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }
    }
}

