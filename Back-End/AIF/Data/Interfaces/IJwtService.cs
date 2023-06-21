using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AIF.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(int id);
        Task<JwtSecurityToken> VerifyAsync(string jwt);
    }
}
