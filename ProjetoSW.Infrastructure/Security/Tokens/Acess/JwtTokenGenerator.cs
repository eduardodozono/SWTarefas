﻿using Microsoft.IdentityModel.Tokens;
using SWTarefas.Infrastructure.Security.Tokens.Acess.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SWTarefas.Infrastructure.Security.Tokens.Acess
{
    public class JwtTokenGenerator : JwtTokenHandler, IJwtTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _signingKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signingKey = signingKey;
        }

        public string Generate(Guid userIdentifier)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Sid,userIdentifier.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(GetSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
