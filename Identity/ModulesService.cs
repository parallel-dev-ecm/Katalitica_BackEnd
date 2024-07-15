using Identity;
using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace Katalitica_API.Identity
{

using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace Identity
    {
        public class ModulesService : LdapProvider
        {
            private IOptions<IdentityOptions> _options;
            public ModulesService(IOptions<IdentityOptions> options) : base(options)
            {
                _options = options;
            }

            public async Task<bool> GetModules()
            {
                try
                {
                    string path = string.Concat("LDAP://", _options.Value.ModulesContainer);
                    using (DirectoryEntry directoryEntry = GetConnection(basePath: path))
                    {
                        using (DirectorySearcher adsSearcher = new DirectorySearcher(directoryEntry))
                        {
                            adsSearcher.Filter = $"((objectClass=organizationalUnit))";

                            SearchResultCollection adsSearchResult = adsSearcher.FindAll();

                            if (adsSearchResult != null)
                            {
                                List<ModulesHierachy> modulesHierachy = new List<ModulesHierachy>();
                                foreach (SearchResult result in adsSearchResult)
                                {
                                    modulesHierachy.Add(new ModulesHierachy
                                    {
                                        Name = getEntryString(result, "name")
                                    });
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                return true;
            }


        }
    }
}
