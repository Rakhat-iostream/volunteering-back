using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Services.Auth.Token;

namespace Volunteer.BL.Services.Auth.Sms
{
    public class SmsTokenService : ISmsTokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISmsTokenRepository _smsTokenRepository;

        public SmsTokenService(IUserRepository userRepository, ISmsTokenRepository smsTokenRepository)
        {
            _userRepository = userRepository;
            _smsTokenRepository = smsTokenRepository;
        }

        /*public async Task<bool> Verify(User user, string token, CancellationToken cancellationToken)
        {
            var userCodes = _smsTokenRepository.GetAll().OrderByDescending(x => x.CreatedAt).Where(x => x.UserId == user.Id);

            var code = userCodes.FirstOrDefault(x => x.Code == token);

            if (code == null) return false;
            if (code.CreatedAt.AddMinutes(10) < DateTime.UtcNow) return false;
            if (code.Code != token) return false;

            await _smsTokenRepository.DeleteRangeAsync(userCodes, cancellationToken);

            return true;
        }

        public async Task<string> Generate(User user, CancellationToken cancellationToken)
        {
            Random generator = new Random();
            var code = generator.Next(0, 10000).ToString("D4");

            var smsCode = new SmsCode() { Code = code, UserId = user.Id };
            await _smsTokenRepository.AddAsync(smsCode, cancellationToken);

            return code;
        }*/
    }
}
