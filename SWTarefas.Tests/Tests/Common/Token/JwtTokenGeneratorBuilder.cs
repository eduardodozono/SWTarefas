
using SWTarefas.Infrastructure.Security.Tokens.Acess;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;

namespace SWTarefas.Tests.TestsMoq.Common.Token
{
    public static class JwtTokenGeneratorBuilder
    {
        public static IJwtTokenGenerator Build()
        {
            return new JwtTokenGenerator(expirationTimeMinutes: 10, signingKey: "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        }
    }
}
