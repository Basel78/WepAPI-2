using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

namespace MobileWebAPI.Identity
{
    public class DomanUserLoginProvider : ILoginProvider
    {
        public bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity)
        {
            using (var pc = new PrincipalContext(ContextType.Domain, _domain))
            {
                UserPrincipal User = default(UserPrincipal);
                bool isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    User = UserPrincipal.FindByIdentity(pc, userName);
                    identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));

                    foreach (Principal result in User.GetGroups())
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, result.Name));
                    }
                }
                else
                {
                    identity = null;
                }

                return isValid;
            }
        }

        public DomanUserLoginProvider(string domain)
        {
            _domain = domain;
        }

        private readonly string _domain;
    }
}