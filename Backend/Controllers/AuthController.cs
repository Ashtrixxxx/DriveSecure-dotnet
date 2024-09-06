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
    [Route("api/[controller]")]
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
        public bool Validate(string username, string password)
        {

            var s = _con.UserDetails.Select(i => i.UserName == username && i.UserPass == password).FirstOrDefault();
            if (s != null)
            {
                return true;
            }
            return false;

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] UserDetails user)
        {
            IActionResult response = Unauthorized();

            if (user != null)
            {
                if (Validate(user.UserName, user.UserPass))
                {

                    var issuer = _config["Jwt:Issuer"];
                    var audience = _config["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature);

                    var subject = new ClaimsIdentity(new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString()) // Assign role to the token
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
            }
            return response;
        }
    }

}
