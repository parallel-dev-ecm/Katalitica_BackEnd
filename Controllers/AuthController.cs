using Katalitica_API.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // For accessing app settings

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        private readonly LdapService _ldapService;
        private readonly IConfiguration _configuration;  // To access app settings

        public AuthController(LdapService ldapService, IConfiguration configuration)  // Inject IConfiguration
        {
            _ldapService = ldapService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Convert the provided username into UPN format
            string userUPN = $"{username}@KATALITICA.com";

            if (_ldapService.Authenticate(username, password))
            {
                // Create JWT token or do other authorization stuff
                return Ok();
            }

            return Unauthorized();
        }
    }
}

