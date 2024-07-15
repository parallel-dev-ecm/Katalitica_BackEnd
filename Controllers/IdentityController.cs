using Identity;
using Katalitica_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Katalitica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private AuthService _authService;
        public IdentityController(AuthService authService) {
        
            
            _authService = authService;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                UserInformation? userInformation = await _authService.Login(loginRequest.username, loginRequest.password);
               // Console.ReadLine();
                Trace.WriteLine(loginRequest.username);
                Trace.WriteLine(loginRequest.password);


                if (userInformation is not null)
                {
                    string token = _authService.CreateToken(userInformation);
                    LoginResponse loginResponse = new LoginResponse
                    {
                        DisplayName = userInformation.cn,
                        Token = token
                    };
                return Ok(loginResponse);
            } 
                else
                {
                    return Unauthorized();
                }
            } catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}

