using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.DTOs;

namespace Volunteer.Common.Services.Auth
{
    public interface IAuthenticationService
    {
        public Task<AuthenticateResponse> GetTokenByLogin(string login, string password, CancellationToken cancellationToken);
        //public Task<bool> VerifySmsCode(string phone, string code, CancellationToken cancellationToken);
        //public Task<string> RequestSmsCode(string phone, CancellationToken cancellationToken);
        public Task<AuthenticateResponse> RefreshToken(string refreshToken, CancellationToken cancellationToken);
        public Task<AuthenticateResponse> UserRegister(string phone, string password, string repeatPassword, CancellationToken cancellationToken);
        public Task<bool> IsUserExists(string phone, CancellationToken cancellationToken = default);
        public Task RevokeToken(string token, CancellationToken cancellationToken);
    }
}
