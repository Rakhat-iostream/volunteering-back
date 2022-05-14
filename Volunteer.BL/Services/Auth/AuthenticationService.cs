using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Crypto;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;
using Volunteer.Common.Models.DTOs;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Services.Auth;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        //private readonly ISmsTokenService _smsTokenService;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenValidator _tokenValidator;


        public AuthenticationService(IAccessTokenService accessTokenService, 
            IRefreshTokenService refreshTokenService, 
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            ITokenValidator tokenValidator, 
            /*ISmsTokenService smsTokenService,*/ 
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenValidator = tokenValidator;
            //_smsTokenService = smsTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        /*public async Task<bool> VerifySmsCode(string phone, string code, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByPhone(phone, cancellationToken);

            if (user == null) throw new Exception("User not found");

            var isVerified = await _smsTokenService.Verify(user, code, cancellationToken);

            if (isVerified)
            {
                user.Status = UserStatus.Verified;

                await _userRepository.UpdateAsync(user, cancellationToken);
            }

            return isVerified;

        }

        public async Task<string> RequestSmsCode(string phone, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByPhone(phone, cancellationToken);

            if (user == null)
            {
                var entity = new User()
                {
                    Phone = phone,
                    Login = phone,
                    Status = UserStatus.InActive,
                    Role = UserRoles.User
                };

                user = await _userRepository.AddAsync(entity, cancellationToken);
            }

            var code = await _smsTokenService.Generate(user, cancellationToken);

            //Sms Provider Integration
            // What if code didn`t send?

            return code;
        }*/

        public async Task<AuthenticateResponse> GetTokenByLogin(string login, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByLogin(login, cancellationToken);
            if (user is null || !_passwordHasher.Verify(password, user.PasswordHash))
            {
                throw new Exception("Incorrect username or password");
            }

            return await Authenticate(user, cancellationToken);
        }

        public async Task<AuthenticateResponse> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            if (!_tokenValidator.Validate(refreshToken))
            {
                throw new Exception("Invalid token");
            }

            var token = await _refreshTokenRepository.GetAsync(refreshToken, cancellationToken);

            if (token is null)
            {
                throw new Exception("Invalid token");
            }

            await _refreshTokenRepository.DeleteAsync(token, cancellationToken);

            var user = await _userRepository.GetAsync(token.UserId, cancellationToken);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            return await Authenticate(user, cancellationToken);
        }

        public async Task<AuthenticateResponse> UserRegister(string phone, string password, string repeatPassword, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByPhone(phone, cancellationToken);

            if (user == null || user.Status != UserStatus.Verified)
            {
                throw new Exception("Something goes wrong");
            }

            if (password != repeatPassword)
            {
                throw new Exception("Passwords doesn`t match");
            }

            user.PasswordHash = _passwordHasher.Hash(password);

            var entity = await _userRepository.UpdateAsync(user, cancellationToken);

            var token = await Authenticate(entity, cancellationToken);

            return token;
        }

        public async Task<bool> IsUserExists(string phone, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsyncByPhone(phone, cancellationToken);
            if (user != null)
            {
                if (user.Role != UserRoles.User)
                {
                    throw new Exception("Not allowed");
                }

                if (user.Status == UserStatus.InActive || user.Status == UserStatus.Verified)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public async Task RevokeToken(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token, cancellationToken);

            if (refreshToken == null)
            {
                throw new Exception("Token is already revoked or incorrect");
            }

            await _refreshTokenRepository.DeleteAsync(refreshToken, cancellationToken);
        }


        private async Task<AuthenticateResponse> Authenticate(User user, CancellationToken cancellationToken)
        {
            if (user.Status == UserStatus.Verified)
            {
                user.Status = UserStatus.Active;
                user = await _userRepository.UpdateAsync(user, cancellationToken);
            }
            var refreshToken = _refreshTokenService.Generate(user);
            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, Token = refreshToken },
                cancellationToken);

            return new AuthenticateResponse
            {
                AccessToken = _accessTokenService.Generate(user),
                RefreshToken = refreshToken,
            };
        }
    }
}
