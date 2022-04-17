﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Domain.Users;
using Vol.Infrastructure;

namespace Vol.V1.Account
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        //private readonly IFacebookProvider _facebookProvider;
        //private readonly IGoogleProvider _googleProvider;
        //private readonly IEmailService _emailService;
        //private readonly ICallbackUrlService _callbackUrlService;
        private readonly ICurrentUserService currentUserService;
        private readonly ILanguageSwitcherService _languageSwitcherService;

        protected SignInManager<User> SignInManager { get; }
        protected UserManager<User> UserManager { get; }

        public AccountAppService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            /*IFacebookProvider facebookProvider,
            IGoogleProvider googleProvider,
            IEmailService emailService,
            ICallbackUrlService callbackUrlService,*/
            ICurrentUserService currentUserService,
            ILanguageSwitcherService languageSwitcherService,
            IMapper mapper)
            : base(mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            /*_facebookProvider = facebookProvider;
            _googleProvider = googleProvider;
            _emailService = emailService;
            _callbackUrlService = callbackUrlService;*/
            this.currentUserService = currentUserService;
            _languageSwitcherService = languageSwitcherService;
        }
        public virtual async Task<UserProfileDto> RegisterAsync(RegisterDto input)

        {
            var language = await _languageSwitcherService.SetLanguage(input.PreferLanguage);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = input.Email,
                UserName = input.Email,
                Name = input.FirstName,
                Surname = input.LastName,
                PhoneNumber = input.Phone,
                PreferLanguage = language
            };
            user.SetFullName(input.FirstName, input.LastName);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

            await SignInManager.SignInAsync(user, false);

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = _callbackUrlService.GetUrl(new CallbackUrlModel()
            {
                Path = FrontUrls.CustomerConfirmEmailCallback
            }, new { userId = user.Id, code = code });

            await _emailService.SendCustomerRegistrationConfirm(user.Email, language, new CustomerRegistrationConfirmModel()
            {
                CustomerName = $"{user.Name} {user.Surname}",
                CallbackUrl = callbackUrl
            });

            return await this.GetUserProfile(user);
        }

        public virtual async Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            var user = await GetUserByEmail(input.Email);
            if (user.IsExternal)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountExternalUserPasswordChange);
            }
            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user);

            var language = await _languageSwitcherService.SetLanguage(user.PreferLanguage);
            await _emailService.SendPasswordResetLink(user.Email, language, null);
        }

        public virtual async Task ResetPasswordAsync(ResetPasswordDto input)
        {
            var user = await this.GetUserByEmail(input.Email);
            (await UserManager.ResetPasswordAsync(user, input.ResetToken, input.Password)).CheckErrors();
        }

        protected virtual async Task<User> GetUserByEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidEmailAddress);
            }

            return user;
        }

        public virtual async Task<UserProfileDto> Login(UserLoginInfo login)
        {
            var signInResult = await SignInManager.PasswordSignInAsync(
                login.Email,
                login.Password,
                login.RememberMe,
                true
            );

            CheckLoginResult(signInResult);
            var user = await this.GetUserByEmail(login.Email);
            return await this.GetUserProfile(user);
        }

        public virtual async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }

        public virtual async Task<UserProfileDto> GetProfileAsync()
        {
            var currentUser = await this.currentUserService.GetCurrentUserAsync();

            var res = this.mapper.Map<User, UserProfileDto>(currentUser);
            res.Roles = await this.currentUserService.GetCurrentUserRolesAsync(currentUser);

            return res;
        }

        public virtual async Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileDto input)
        {
            var user = await this.currentUserService.GetCurrentUserAsync();

            if (!string.IsNullOrEmpty(input.PhoneNumber) && !string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                user.Name = input.Name;
                user.SetFullName(input.Name, user.Surname);
            }

            if (!string.IsNullOrEmpty(input.Surname))
            {
                user.Surname = input.Surname;
                user.SetFullName(input.Name, input.Surname);
            }

            if (!string.IsNullOrEmpty(input.PreferLanguage))
            {
                user.PreferLanguage = input.PreferLanguage;
            }

            (await UserManager.UpdateAsync(user)).CheckErrors();

            var currentUser = await this.currentUserService.GetCurrentUserAsync();

            var res = this.mapper.Map<User, UserProfileDto>(currentUser);
            res.Roles = await this.currentUserService.GetCurrentUserRolesAsync(currentUser);

            return res;
        }

        public virtual async Task ChangePasswordAsync(ChangePasswordInput input)
        {

            var currentUser = await this.currentUserService.GetCurrentUserAsync();

            if (currentUser.IsExternal)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountExternalUserPasswordChange);
            }

            if (currentUser.PasswordHash == null)
            {
                (await UserManager.AddPasswordAsync(currentUser, input.NewPassword)).CheckErrors();

                return;
            }

            (await UserManager.ChangePasswordAsync(currentUser, input.CurrentPassword, input.NewPassword)).CheckErrors();
        }

        private static void CheckLoginResult(Microsoft.AspNetCore.Identity.SignInResult result)
        {
            if (result.IsLockedOut)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountLockedOut);
            }

            if (result.RequiresTwoFactor)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountRequiresTwoFactor);
            }

            if (result.IsNotAllowed)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountLockedOut);
            }

            if (!result.Succeeded)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.AccountInvalidUserNameOrPassword);
            }
        }

        public async Task<UserProfileDto> FacebookLogin(FacebookCredentials credentials)
        {
            var customer = await _facebookProvider.GetCustomerInfo(credentials);

            var signInResult = await SignInManager.ExternalLoginSignInAsync("Facebook",
                                        customer.Id, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                CheckLoginResult(signInResult);
                var user = await this.GetUserByEmail(customer.Email);
                return await this.GetUserProfile(user);
            }
            else
            {
                var email = customer.Email;

                if (email != null)
                {
                    var user = await UserManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            Id = Guid.NewGuid(),
                            Email = email,
                            UserName = email,
                            Name = customer.FirstName,
                            Surname = customer.LastName
                        };
                        user.SetFullName(customer.FirstName, customer.LastName);

                        (await UserManager.CreateAsync(user)).CheckErrors();
                    }

                    await UserManager.AddLoginAsync(user, new Microsoft.AspNetCore.Identity.UserLoginInfo("Facebook",
                                        customer.Id, "Facebook"));
                    signInResult = await SignInManager.ExternalLoginSignInAsync("Facebook",
                                        customer.Id, isPersistent: false, bypassTwoFactor: true);

                    CheckLoginResult(signInResult);
                    return await this.GetUserProfile(user);
                }

                throw new BusinessException(HaxpeDomainErrorCodes.AccountFacebookNoEmail);
            }
        }

        public async Task<UserProfileDto> GoogleLogin(GoogleCredentials credentials)
        {
            var customer = await _googleProvider.GetCustomerInfo(credentials);

            var signInResult = await SignInManager.ExternalLoginSignInAsync("Google",
                                        customer.Id, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                CheckLoginResult(signInResult);
                var user = await this.GetUserByEmail(customer.Email);
                return await this.GetUserProfile(user);
            }
            else
            {
                var email = customer.Email;

                if (email != null)
                {
                    var user = await UserManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            Id = Guid.NewGuid(),
                            Email = email,
                            UserName = email,
                            Name = customer.FirstName,
                            Surname = customer.LastName
                        };
                        user.SetFullName(customer.FirstName, customer.LastName);

                        (await UserManager.CreateAsync(user)).CheckErrors();
                    }

                    await UserManager.AddLoginAsync(user, new Microsoft.AspNetCore.Identity.UserLoginInfo("Google",
                                        customer.Id, "Google"));
                    signInResult = await SignInManager.ExternalLoginSignInAsync("Google",
                                        customer.Id, isPersistent: false, bypassTwoFactor: true);

                    CheckLoginResult(signInResult);
                    return await this.GetUserProfile(user);
                }

                throw new BusinessException(HaxpeDomainErrorCodes.AccountFacebookNoEmail);
            }
        }

        private async Task<UserProfileDto> GetUserProfile(User user)
        {
            var res = this.mapper.Map<User, UserProfileDto>(user);
            res.Roles = await this.currentUserService.GetCurrentUserRolesAsync(user);
            return res;
        }

        public async Task ConfirmEmailAsync(ConfirmEmailDto input)
        {
            var user = await UserManager.FindByEmailAsync(input.Email);
            if (user == null)
            {
                throw new BusinessException(HaxpeDomainErrorCodes.UserNotFound);
            }
            var result = await UserManager.ConfirmEmailAsync(user, input.Token);
            result.CheckErrors();
        }
    }
}
