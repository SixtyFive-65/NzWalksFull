using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser model, List<string> roles);
    }
}
