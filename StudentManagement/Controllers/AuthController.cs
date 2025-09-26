using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// Authentication controller for login and JWT token generation.
    /// Supports multiple roles per user.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login attempt for user: {Username}", request.Username);

            // Dummy users with multiple roles
            var users = new Dictionary<string, (string Password, List<string> Roles)>
            {
                { "admin", ("admin123", new List<string> { "Admin", "Moderator" }) },
                { "moderator", ("mod123", new List<string> { "Moderator" }) },
                { "reader", ("read123", new List<string> { "ReadOnly" }) }
            };

            // Validate credentials
            if (users.TryGetValue(request.Username.ToLower(), out var user) && user.Password == request.Password)
            {
                _logger.LogInformation("Login successful for user: {Username}", request.Username);

                var token = GenerateJwtToken(request.Username, user.Roles);
                _logger.LogInformation("JWT token generated for user: {Username}", request.Username);

                return Ok(new { token });
            }

            _logger.LogWarning("Login failed for user: {Username}", request.Username);
            return Unauthorized("Invalid username or password");
        }

        private string GenerateJwtToken(string username, List<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Add multiple roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    /// DTO for login request.
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
