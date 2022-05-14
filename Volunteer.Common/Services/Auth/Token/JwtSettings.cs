using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Services.Auth.Token
{
    public class JwtSettings
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public double MobileRefreshTokenExpirationMinutes { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
