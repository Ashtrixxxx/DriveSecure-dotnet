using Backend.Models;
using Google.Apis.Drive.v3.Data;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private static readonly ILog log = LogManager.GetLogger(typeof(AuthController));

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
            try
            {
                // Log the start of the validation process
                log.Info($"Attempting to validate user with username: {username}");

                // Perform user validation
                UserDetails user = _con.UserDetails
                    .FirstOrDefault(i => i.UserName == username && i.UserPass == password);

                if (user != null)
                {
                    // Log successful validation
                    log.Info($"User '{username}' successfully validated.");
                }
                else
                {
                    // Log failed validation
                    log.Warn($"User '{username}' not found or password is incorrect.");
                }

                return user;
            }
            catch (Exception ex)
            {
                // Log any errors that occur during validation
                log.Error($"An error occurred while validating user '{username}': {ex.Message}", ex);
                throw; // Optionally rethrow the exception to propagate the error
            }
        }


        [NonAction]
        public AdminDetails ValidateAdmin(string email, string password)
        {
               AdminDetails a = _con.Admins
                .FirstOrDefault(i=> i.Email == email && i.password == password);
            return a;
        }

        public class AdminLoginDto
        {
            public string AdminEmail { get; set; }
            public string AdminPass { get; set; }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AdminAuth([FromBody] AdminLoginDto adminLoginDto)
        {
            IActionResult response = Unauthorized();

            // Validate the admin login details
            var s = ValidateAdmin(adminLoginDto.AdminEmail, adminLoginDto.AdminPass);
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
                new Claim(JwtRegisteredClaimNames.Email, s.Email),
                new Claim(ClaimTypes.Role, s.Role.ToString()) // Assign role to the token
            });

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

                return Ok(new { token = jwtToken });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UserAuth([FromBody] UserAuthRequest request)
        {
            Console.WriteLine($"UserName: {request.UserName}, UserPass: {request.UserPass}");
            IActionResult response = Unauthorized();

           
            var s = Validate(request.UserName, request.UserPass);
            if (s == null)
            {
                return Unauthorized(new { message = "Invalid user credentials" });
            }
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
            new Claim(ClaimTypes.NameIdentifier, s.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, s.UserName),
            new Claim(JwtRegisteredClaimNames.Email, s.Email),
            new Claim(ClaimTypes.Role, s.Role.ToString())
        });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(new { Token = jwtToken });
                }

            return response;
        }

        public class UserAuthRequest
        {
            public string UserName { get; set; }
            public string UserPass { get; set; }
        }

    }

}
