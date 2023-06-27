using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AIF.Data.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(int id);

        Task<JwtSecurityToken> VerifyAsync(string jwt);
    }
}
