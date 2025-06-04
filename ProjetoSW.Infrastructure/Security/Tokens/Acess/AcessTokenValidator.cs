using Microsoft.IdentityModel.Tokens;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SWTarefas.Infrastructure.Security.Tokens.Acess
{
    public class AcessTokenValidator : JwtTokenHandler, IAcessTokenValidator
    {
        private readonly string _signingKey;

        public AcessTokenValidator(string signingKey)
        {
            _signingKey = signingKey;
        }

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var validationParameter = new TokenValidationParameters
            {
                ClockSkew = new TimeSpan(0),
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = GetSecurityKey(_signingKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(userIdentifier);
        }
    }
}
