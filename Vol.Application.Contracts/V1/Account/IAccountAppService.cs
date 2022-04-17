using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Infrastructure;

namespace Vol.V1.Account
{
    public interface IAccountAppService : IApplicationService
    {
        Task<UserProfileDto> RegisterAsync(RegisterDto input);

        Task ConfirmEmailAsync(ConfirmEmailDto input);

        Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input);

        Task ResetPasswordAsync(ResetPasswordDto input);

        Task<UserProfileDto> Login(UserLoginInfo login);

        Task Logout();

        Task<UserProfileDto> GetProfileAsync();

        Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileDto input);

        Task ChangePasswordAsync(ChangePasswordInput input);

        /*Task<UserProfileDto> FacebookLogin(FacebookCredentials credentials);

        Task<UserProfileDto> GoogleLogin(GoogleCredentials credentials);*/
    }
}
