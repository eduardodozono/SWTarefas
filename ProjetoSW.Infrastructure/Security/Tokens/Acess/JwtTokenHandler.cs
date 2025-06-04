using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SWTarefas.Infrastructure.Security.Tokens.Acess
{
    public abstract class JwtTokenHandler
    {
        protected SymmetricSecurityKey GetSecurityKey(string _signingKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
        }
    }
}
