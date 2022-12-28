using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Camera;
using ForestryAnimals_AtaRK.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class CameraController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CameraController(UserManager<User> userManager,
                               ApplicationContext context,
                               IMapper mapper,
                               RoleManager<IdentityRole> roleManager,
                               IStringLocalizer<SharedResource> localizer,
                               IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _ = RoleInitializer.RoleInit(userManager, roleManager, configuration);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCamera([FromForm] AddCameraRequest request)
        {
            Camera camera = _mapper.Map<AddCameraRequest, Camera>(request!);

            camera.ForestryId = request.ForestryId;
            camera.Forestry = _context.Forestries.Where(f => f.Id == request.ForestryId).First();
            _context.Cameras.Add(camera);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllCameras([FromQuery] GetCamerasForForestry request)
        {
            var cams = _context.Cameras.Where(c => c.ForestryId == request.ForestryId);
            return Ok(cams);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCamera([FromForm] DeleteCameraRequest request)
        {
            var cam = _context.Cameras.Where(c => c.Id == request.Id).FirstOrDefault();
            if (cam == null) return BadRequest(_localizer["Camera does not exist with this id"].Value);
            _context.Cameras.Remove(cam);
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest(_localizer["Something went wrong"].Value);
        }
    }
}
