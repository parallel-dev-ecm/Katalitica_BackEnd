using Katalitica_API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity
{
    public class AuthService : LdapProvider, IAuthService
    {
        private IOptions<IdentityOptions> _options;
        public AuthService(IOptions<IdentityOptions> options) : base(options)
        {
            _options = options;
        }

        public async Task<IEnumerable<ModulesHierachy>> GetModulesHierachy(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInformation?> Login(string username, string password)
        {
            UserInformation? userInformation = null;
            Trace.WriteLine("Logging in...");

            Trace.WriteLine(username);
            Trace.WriteLine(password);


            try
            {

                using (DirectoryEntry directoryEntry = GetConnection(username, password))
                {
                    using (DirectorySearcher adsSearcher = new DirectorySearcher(directoryEntry))
                    {
                        adsSearcher.Filter = string.Format(_options.Value.SearchPattern, username);

                        SearchResult adsSearchResult = adsSearcher.FindOne();

                        if (adsSearchResult != null)
                        {
                            Trace.WriteLine("User found");

                            ResultPropertyCollection res = adsSearchResult.Properties;

                            userInformation = new UserInformation();
                            userInformation.cn = res["cn"][0].ToString();
                            userInformation.givenName = res["givenName"][0].ToString();
                            userInformation.distinguishedName = res["distinguishedName"][0].ToString();
                            userInformation.sAMAccountName = res["sAMAccountName"][0].ToString();

                            List<string> memberOfList = new List<string>();
                            foreach (var p in res["memberOf"]) 
                            {
                                memberOfList.Add(p.ToString());
                            }

                            userInformation.memberOf = memberOfList;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                userInformation = null;
            }

            return userInformation;
        }

        public string CreateToken(UserInformation user)
        {

            try
            {

                byte[] key = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(GetClaims(user)),
                    Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpiryTimeInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(descriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private Claim[] GetClaims(UserInformation user)
        {
            List<Claim> list = new List<Claim>();
            list.Add(new Claim(ClaimTypes.Name, user.sAMAccountName));

            // AGREGAR ROLES
            foreach (string memberOfItem in user.memberOf)
            {
                int indexStart = memberOfItem.IndexOf("=");
                int indexEnd = memberOfItem.IndexOf(",");
                string group = memberOfItem.Substring(indexStart + 1, indexEnd - (indexStart + 1));
                list.Add(new Claim(ClaimTypes.Role, group));
            }

            return list.ToArray<Claim>();

        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        Task<UserInformation> IAuthService.Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
