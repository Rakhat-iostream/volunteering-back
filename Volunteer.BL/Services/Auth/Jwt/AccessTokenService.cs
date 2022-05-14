using System.Collections.Generic;
using System.Security.Claims;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth.Jwt
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string Generate(User user)
        {
            var expirationTime = _jwtSettings.AccessTokenExpirationMinutes;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            if (user.Login != null)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login));
            }

            if (user.Phone != null)
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.Phone));
            }

            if (user.Email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            return _tokenGenerator.Generate(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience,
                                            expirationTime, claims);
        }
    }
}
