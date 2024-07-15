using Identity;
using Katalitica_API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.DirectoryServices;
using System.Security.Claims;

namespace Katalitica_API.Controllers
{
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("LdapLogin")]

        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            //string path = "LDAP://KDC2019-SRV.KATALITICA.com/cn=GI,dc=KATALTICIA,dc=com";
            string path = "LDAP://KDC2019-SRV.KATALITICA.com:1389";

            try
            {
                 using (DirectoryEntry entry = new DirectoryEntry(path, loginRequest.username, loginRequest.password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = "(samaccountname=" + loginRequest.username + ")";
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            string role = "";
                            ResultPropertyCollection fields = result.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    if (ldapField == "employeetype")
                                        role = myCollection.ToString().ToLower();

                                }
                            }

                            var claims = new[]
                            {
                                new Claim(ClaimTypes.Name, loginRequest.username),
                                new Claim(ClaimTypes.Role, role),
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                            LoginResponse loginResponse = new LoginResponse
                            {
                                DisplayName = loginRequest.username,
                                Token = role
                            };
                            return Ok(loginResponse);
                        }
                        else { return Unauthorized(); } 
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("LdapLogin2")]
        public async Task<IActionResult> Login2([FromBody] LoginRequest loginRequest)
        {
            string path = "LDAP://10.20.30.2:389/dc=KATALITICA,dc=com";

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(path, loginRequest.username, loginRequest.password, AuthenticationTypes.Secure))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        // searcher.Filter = "(samaccountname=" + loginRequest.username + ")";
                        searcher.Filter = $"(&(objectClass=user)(objectClass=person)(sAMAccountName={loginRequest.username}))";
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            string role = "";
                            ResultPropertyCollection fields = result.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    if (ldapField == "employeetype")
                                        role = myCollection.ToString().ToLower();

                                }
                            }

                            var claims = new[]
                            {
                                new Claim(ClaimTypes.Name, loginRequest.username),
                                new Claim(ClaimTypes.Role, role),
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                            LoginResponse loginResponse = new LoginResponse
                            {
                                DisplayName = loginRequest.username,
                                Token = role
                            };
                            return Ok(loginResponse);
                        }
                        else { return Unauthorized(); }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
    }
 
}
