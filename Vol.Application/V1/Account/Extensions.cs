using Microsoft.AspNetCore.Identity;
using Serilog;
using Vol.Domain.Shared;
using Vol.Infrastructure;

namespace Vol.V1.Account
{
    public static class Extensions
    {
        public static void CheckErrors(this IdentityResult result)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    switch (error.Code)
                    {
                        case "DefaultError":
                            throw new BusinessException(VolDomainErrorCodes.AccountDefaultError);
                        case "LoginAlreadyAssociated":
                            throw new BusinessException(VolDomainErrorCodes.AccountLoginAlreadyAssociated);
                        case "InvalidEmail":
                            throw new BusinessException(VolDomainErrorCodes.AccountInvalidEmail);
                        case "DuplicateEmail":
                            throw new BusinessException(VolDomainErrorCodes.AccountDuplicateEmail);
                        case "InvalidUserName":
                            throw new BusinessException(VolDomainErrorCodes.AccountInvalidUserNameOrPassword);
                        case "DuplicateUserName":
                            throw new BusinessException(VolDomainErrorCodes.AccountDuplicateUserName);
                        case "PasswordMismatch":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordMismatch);
                        case "PasswordTooShort":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordTooShort);
                        case "PasswordRequiresUniqueChars":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordRequiresUniqueChars);
                        case "PasswordRequiresNonAlphanumeric":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordRequiresNonAlphanumeric);
                        case "PasswordRequiresDigit":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordRequiresDigit);
                        case "PasswordRequiresLower":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordRequiresLower);
                        case "PasswordRequiresUpper":
                            throw new BusinessException(VolDomainErrorCodes.AccountPasswordRequiresUpper);
                        case "UserLockoutNotEnabled":
                            throw new BusinessException(VolDomainErrorCodes.AccountUserLockoutNotEnabled);
                        case "InvalidToken":
                            throw new BusinessException(VolDomainErrorCodes.AccountInvalidToken);
                        case "FailedOperation":
                            throw new BusinessException(VolDomainErrorCodes.AccountFailedOperation);
                        default:
                            Log.Logger.Error($"Internal error code = {error.Code}");
                            throw new BusinessException(VolDomainErrorCodes.AccountDefaultError);
                    }
                }
            }
        }
    }
}
