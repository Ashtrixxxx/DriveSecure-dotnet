using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration _config;
        private readonly DriveDbContext _con;
        public AuthController(IConfiguration configuration, DriveDbContext conn)
        {
            this._config = configuration;
            _con = conn;
        }

        [NonAction]
        public UserDetails Validate(string username, string password)
        {

            UserDetails s = _con.UserDetails
                .FirstOrDefault(i => i.UserName == username && i.UserPass == password);

            return s;
        }

        [NonAction]
        public AdminDetails ValidateAdmin(string email, string password)
        {
               AdminDetails a = _con.Admins
                .FirstOrDefault(i=> i.Email == email && i.password == password);
            return a;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AdminAuth(string AdminEmail, string AdminPass)
        {
            IActionResult response = Unauthorized();

            var s = ValidateAdmin(AdminEmail, AdminPass);
            if (s != null)
            {

                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha512Signature);

                var subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email,s.Email),
                    new Claim(ClaimTypes.Role, s.Role.ToString()) // Assign role to the token
                    });

                var expires = DateTime.UtcNow.AddMinutes(10);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);

            }
            return response;
         
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UserAuth(string UserName, string UserPass)
        {
            IActionResult response = Unauthorized();

            var s = Validate(UserName, UserPass);
                if (s != null)
            {
                
                    var issuer = _config["Jwt:Issuer"];
                    var audience = _config["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature);

                    var subject = new ClaimsIdentity(new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, s.UserName),
                    new Claim(JwtRegisteredClaimNames.Email,s.Email),
                    new Claim(ClaimTypes.Role, s.Role.ToString()) // Assign role to the token
                    });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(jwtToken);
                
            }
            return response;
        }
    }

}
