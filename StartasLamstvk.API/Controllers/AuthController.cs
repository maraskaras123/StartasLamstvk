using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StartasLamstvk.API.Entities;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models.Auth;
using StartasLamstvk.Shared.Models.Enum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StartasLamstvk.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<User> userManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route(Routes.Auth.Login.Endpoint)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .ThenInclude(x => x.RoleTranslations)
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Name, user.UserName),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

                var token = GetToken(authClaims);
                string language = HttpContext.Request.Headers["x-language"];
                //language = string.IsNullOrEmpty(language) ? "lt" : language;
                return Ok(new AuthResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiredAt = token.ValidTo,
                    User = new()
                    {
                        FullName = $"{user.Name} {user.Surname}",
                        Id = user.Id,
                        PhoneNumber = user.PhoneNumber,
                        Role = new()
                        {
                            Id = (EnumRole)user.RoleId,
                            Name = user.Role.RoleTranslations.First(x => x.LanguageCode == (language ?? "lt")).Text
                        }
                    }
                });
            }

            return Unauthorized();
        }

        /*[HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                throw new ValidationException("User already exists!");
            }

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new AuthResponse
                    {
                        Status = "Error", Message = "User creation failed! Please check user details and try again."
                    });
            }

            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }*/

        [HttpDelete]
        [Authorize]
        [Route(Routes.Auth.Logout.Endpoint)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return NoContent();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new (authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}