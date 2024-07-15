using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace Identity
{
    public class LdapProvider
    {
        private readonly IOptions<IdentityOptions> _options;
        private readonly string _basePath;
        public LdapProvider(IOptions<IdentityOptions> options)
        {
            _options = options;
            _basePath = string.Concat("LDAP://", _options.Value.BaseSearch);
        }

        protected DirectoryEntry GetConnection(string? _Username = null, string? _Password = null, string? basePath = null)
        {
            if (basePath == null)
            {
                basePath = _basePath;
            }

            if (string.IsNullOrEmpty(_Username) && string.IsNullOrEmpty(_Password))
            {
                _Username = _options.Value.AdminAccount.Split(":")[0];
                _Password = _options.Value.AdminAccount.Split(":")[1];
            }

            DirectoryEntry directoryEntry = new(basePath, _Username, _Password, AuthenticationTypes.Secure);
            return directoryEntry;
        }

        protected string? getEntryString(SearchResult context, string proprietaryName)
        {
            var result = context.Properties;
            return result[proprietaryName][0].ToString() != null ? result[proprietaryName][0].ToString() : null;
        }

    }
}
