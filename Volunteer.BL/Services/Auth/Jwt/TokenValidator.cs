using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth.Jwt
{
    public class TokenValidator : ITokenValidator
    {
        private readonly JwtSettings _jwtSettings;

        public TokenValidator(JwtSettings jwtSettings) => _jwtSettings = jwtSettings;

        public bool Validate(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecret)),
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                jwtSecurityTokenHandler.ValidateToken(token, validationParameters,
                    out SecurityToken _);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
