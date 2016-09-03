using System.Security.Claims;

namespace MobileWebAPI.Identity
{
    public interface ILoginProvider
    {
        bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity);
    }
}