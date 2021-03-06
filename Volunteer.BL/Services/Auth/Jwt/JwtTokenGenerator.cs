using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth.Jwt
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        public string Generate(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim> claims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(issuer, audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expires),
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
