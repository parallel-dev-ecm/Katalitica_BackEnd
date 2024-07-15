using System.DirectoryServices.Protocols;
using System.Net;

namespace Katalitica_API.Identity
{
    public class LdapService
    {
        private readonly string _url;
        private readonly string _bindDn;
        private readonly string _bindCredentials;
        private readonly string _searchBase;

        public LdapService(IConfiguration configuration)
        {
            _url = configuration["Ldap:Url"];
            _bindDn = configuration["Ldap:BindDn"];
            _bindCredentials = configuration["Ldap:BindCredentials"];
            _searchBase = configuration["Ldap:SearchBase"];
        }

        public bool Authenticate(string username, string password)
        {
            using var connection = new LdapConnection(_url);
            connection.Credential = new NetworkCredential(_bindDn, _bindCredentials);
            System.Diagnostics.Debug.WriteLine(_url);
            connection.Bind();

            var searchRequest = new SearchRequest(
                _searchBase,
                $"(&(objectClass=user)(sAMAccountName={username}))",
                SearchScope.Subtree,
                null);

            var response = (SearchResponse)connection.SendRequest(searchRequest);

            if (response.Entries.Count > 0)
            {
                var userDn = response.Entries[0].DistinguishedName;

                try
                {
                    connection.Credential = new NetworkCredential(userDn, password);
                    connection.Bind();
                    return true;
                }
                catch (LdapException)
                {
                    return false;
                }
            }

            return false;
        }
    }

}
