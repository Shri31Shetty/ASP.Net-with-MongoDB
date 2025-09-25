using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// Authentication controller for login and JWT token generation.
    /// Logs all login attempts, successes, failures, and token creation.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger; // Logger for tracking events

        /// <summary>
        /// Constructor injecting configuration and logger.
        /// </summary>
        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Login endpoint.
        /// Accepts username/password and returns a JWT token if credentials are valid.
        /// </summary>
        /// <param name="request">Login request DTO with username and password.</param>
        /// <returns>JWT token if successful, Unauthorized otherwise.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login attempt for user: {Username}", request.Username);

            // Dummy users with roles (for demonstration only)
            var users = new Dictionary<string, (string Password, string Role)>
            {
                { "admin", ("admin123", "Admin") },
                { "moderator", ("mod123", "Moderator") },
                { "reader", ("read123", "ReadOnly") }
            };

            // Validate credentials
            if (users.TryGetValue(request.Username.ToLower(), out var user) && user.Password == request.Password)
            {
                _logger.LogInformation("Login successful for user: {Username}", request.Username);

                // Generate JWT token
                var token = GenerateJwtToken(request.Username, user.Role);
                _logger.LogInformation("JWT token generated for user: {Username}", request.Username);

                return Ok(new { token });
            }

            // Login failed
            _logger.LogWarning("Login failed for user: {Username}", request.Username);
            return Unauthorized("Invalid username or password");
        }

        /// <summary>
        /// Generates JWT token for a user with a role claim.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="role">Role of the user (Admin/Moderator/ReadOnly).</param>
        /// <returns>Signed JWT token string.</returns>
        private string GenerateJwtToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Add user identity and role claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token valid for 1 hour
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    /// DTO for login request containing username and password.
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
