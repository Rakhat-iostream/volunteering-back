using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth.Jwt
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string Generate(User user)
        {
            var expTime = _jwtSettings.RefreshTokenExpirationMinutes;
            if (user.Role == UserRoles.User)
            {
                expTime = _jwtSettings.MobileRefreshTokenExpirationMinutes;
            }
            var token = _tokenGenerator.Generate(_jwtSettings.RefreshTokenSecret,
                _jwtSettings.Issuer, _jwtSettings.Audience,
                expTime);
            return token;
        }
    }
}
