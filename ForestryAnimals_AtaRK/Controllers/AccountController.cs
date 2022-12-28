using AutoMapper;
using ForestryAnimals_AtaRK.Entities;
using ForestryAnimals_AtaRK.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ApplicationContext context,
                                 IMapper mapper,
                                 RoleManager<IdentityRole> roleManager,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
            _ = RoleInitializer.RoleInit(userManager, roleManager, configuration);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterRequest request)
        {
            try
            {
                if (request.Password == request.PasswordConfirmation)
                {
                    if (request.UserName == "" || request.UserName == null) request.UserName = request.Email;
                    Owner owner = _mapper.Map<RegisterRequest, Owner>(request!);
                    var result = await _userManager.CreateAsync(owner!, request.Password);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddToRoleAsync(owner!, "owner");
                        if (result.Succeeded)
                        {
                            //SendEmail(model, callbackUrl, callbackDomain, url, user);
                            await LogInAsync(_mapper.Map<RegisterRequest, LoginRequest>(request!));
                            return Ok();
                        }
                    }
                    return BadRequest(result.Errors);
                }
                return BadRequest("Passwords are not the same");
            }
            catch (MimeKit.ParseException)
            {
                var user = _mapper.Map<Owner>(request);
                await _userManager.DeleteAsync(user);
                return BadRequest(new[] { "Invalid email" });
            }
            //catch
            //{
            //    var user = _mapper.Map<User>(request);
            //    await _userManager.DeleteAsync(user);
            //    return BadRequest(new[] {"Something went wrong. Please try again"});
            //}
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogInAsync([FromForm] LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: true, lockoutOnFailure: false);
            return result.Succeeded ? Ok() : BadRequest("Invalid login and/or password");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogOutAsync()
        {
            if (await _userManager.GetUserAsync(User) == null) return Unauthorized();
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user != null) return Ok(user);
            return Unauthorized(new[] { "Firstly you have to log in" });
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUserRoleAsync()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user != null) return Ok(await _userManager.GetRolesAsync(user));
            return Unauthorized(new[] { "Firstly you have to log in" });
        }

        [Authorize(Roles = "owner")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser()
        {
            User user = await _userManager.GetUserAsync(User);
            if (user is null)
                return Unauthorized(new[] { "Firstly you have to log in" });
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? await LogOutAsync() : BadRequest(result.Errors.Select(e => e.Description));
        }
    }
}
