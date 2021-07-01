using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserApi.Entities;
using UserApi.Models;
using UserApi.Services.Jwt;

namespace UserApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(UserManager<User> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<User> signInManager,
                              JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(_jwtTokenService.GetToken(user, roles));
            }

            return Forbid();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                BirthDate = model.BirthDate,
            };

            await CreateRoleIfNotExists("Admin");
            await CreateRoleIfNotExists("User");

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                switch (model.Username)
                {
                    case "Admin":
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                            break;
                        }

                    default:
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            break;
                        }
                }

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(_jwtTokenService.GetToken(user, roles));
            }

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            return _jwtTokenService.ValidateToken(token) ? Ok() : (IActionResult)Forbid();
        }

        #region Helpers

        private async Task CreateRoleIfNotExists(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        #endregion Helpers
    }
}