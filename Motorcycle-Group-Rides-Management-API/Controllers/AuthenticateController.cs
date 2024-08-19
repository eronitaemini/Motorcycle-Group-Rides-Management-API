using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Motorcycle_Group_Rides_Management_API.Authentications;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Dtos;

namespace Motorcycle_Group_Rides_Management_API.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticateController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IConfiguration config, UserManager<IdentityUser> userManager, ILogger<AuthenticateController> logger)
        {
            _config = config;
            _userManager = userManager;
            _logger = logger;
        }

       [HttpPost]
            [Route("UserLogin")]
            public async Task<IActionResult> Login([FromBody] UserDto userDto)
            {
                var user = await _userManager.FindByNameAsync(userDto.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
                {
                    string token = await GetToken(user);  // Await the GetToken method
                    return Ok(token);
                }
                return NotFound("User is not found");
            }


        [HttpPost]
        [Route("UserRegister")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
                return BadRequest();

            var user = new IdentityUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return Ok(result);

            return Ok("User registered successfully!");
        }



        private string GetToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                   _config["Jwt:Audience"],
                   claims,
                   expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }



       private async Task<string> GetToken(IdentityUser user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }.Union(roleClaims);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

    
    }
}

