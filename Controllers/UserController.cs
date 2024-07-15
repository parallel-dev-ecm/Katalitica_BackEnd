using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Katalitica_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Katalitica_API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController: ControllerBase
	{
        public IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        [Route("signInGenerateToken")]
        public dynamic UserLogin([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string user = data.userEmail.ToString();
            string password = data.userPassword.ToString();
            UserModel _user = UserModel.testDB().Where(x => x.userEmail == user && x.userPassword == password).FirstOrDefault();

            if (_user == null)
            {
                return new
                {
                    sucess = false,
                    message = "Unauthorized access",
                    result = " "
                };
            }
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", _user.userId),
                new Claim("id", _user.userEmail)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, signingCredentials: signIn, expires: DateTime.Now.AddHours(2), notBefore: DateTime.Now);
            return new
            {
                success = true,
                message = "Signed in succesfully",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }


    }

    

}


