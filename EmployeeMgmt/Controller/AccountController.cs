using EmployeeMgmt.Domain;
using EmployeeMgmt.DTO;
using EmployeeMgmt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeMgmt.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceMethod _serviceMethod;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IServiceMethod serviceMethod)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceMethod = serviceMethod;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        [Authorize(Roles = "Admin")]
        //[Authorize]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                };



                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                    return BadRequest(new RegisterResponseDto
                    {
                        Message = $"Registration Unsuccessful.{result.Errors.First().Description}",
                        Success = false
                    });

                // add only employee users
                result = await _userManager.AddToRoleAsync(user,
                    _roleManager.Roles.ToList().Single(c => c.Name == Role.Employee.ToString()).Name);

                return Ok(new RegisterResponseDto
                {
                    Message = "Registration successful",
                    Success = true
                });

            }

            catch (Exception ex)
            {
                var response = new
                {
                    Msg = "Processing request failed.",
                    Errorlst = new List<ErrorMessage>() {
                        new ErrorMessage() { Error = ex.Message } }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            try
            {

                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

                if (!result.Succeeded)

                    return BadRequest(new LoginResponseDto
                    {
                        Message = "Login Failed, Username or Password is wrong.",
                        Success = false
                    });

                return Ok(new LoginResponseDto
                {
                    Token = _serviceMethod.GenerateToken(loginDto.Email),
                    Success = true
                });
            }

            catch (Exception ex)
            {
                var response = new
                {
                    Msg = "Processing request failed.",
                    Errorlst = new List<ErrorMessage>() {
                        new ErrorMessage() { Error = ex.Message } }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


    }
}
