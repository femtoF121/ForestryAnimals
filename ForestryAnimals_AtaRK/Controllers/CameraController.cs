using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller/action]")]
    public class CameraController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CameraController(UserManager<User> userManager,
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
    }
}
