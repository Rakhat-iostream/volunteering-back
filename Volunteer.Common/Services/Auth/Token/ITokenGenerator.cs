using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Services.Auth.Token
{
    public interface ITokenGenerator
    {
        string Generate(string secretKey, string issuer, string audience, double expires,
        IEnumerable<Claim> claims = null);
    }
}
