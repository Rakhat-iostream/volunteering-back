using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Crypto;
using Volunteer.Common.Extensions;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;
using Volunteer.Common.Models.DTOs.User;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Services.Users;

namespace Volunteer.BL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserProfileDto> AddAsync(UserAddActionDto userUpdateActionDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserAddActionDto, User>(userUpdateActionDto);
            if (user.PasswordHash != userUpdateActionDto.RepeatedPassword)
            {
                throw new Exception("Passwords doesn`t match");
            }
            user.PasswordHash = _passwordHasher.Hash(user.PasswordHash);
            var entity = await _userRepository.AddAsync(user, cancellationToken);
            return _mapper.Map<User, UserProfileDto>(entity);
        }

        public async Task<UserProfileDto> UpdateAsync(UserUpdateActionDto userUpdateActionDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserUpdateActionDto, User>(userUpdateActionDto);
            var entity = await _userRepository.GetAsync(user.Id);
            if (user.PasswordHash != null)
            {
                /*if (entity.Role == UserRoles.User)
                {
                    user.PasswordHash = null;
                }*/
                if (userUpdateActionDto.Password != userUpdateActionDto.RepeatedPassword)
                {
                    throw new Exception("New passwords doesn`t match");
                }

                user.PasswordHash = _passwordHasher.Hash(user.PasswordHash);
            }

            var result = await _userRepository.UpdateAsync(user, cancellationToken);
            return _mapper.Map<User, UserProfileDto>(result);
        }

        public async Task<PageResponse<UserProfileDto>> GetAll(PageRequest request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetAll(request);

            var total = users.Count();
            var model = _mapper.Map<List<UserProfileDto>>(users);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<UserProfileDto>
            {
                Total = total,
                Result = result

            };
        }

        public async Task<UserProfileDto> GetSignedUser(CancellationToken cancellationToken)
        {
            var login = _httpContextAccessor.HttpContext.User.Identity.Name;

            return await GetByLoginAsync(login, cancellationToken);
        }

        public async Task<UserProfileDto> GetAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            if (user == null) throw new Exception("User not found");
            return _mapper.Map<User, UserProfileDto>(user);
        }

        public async Task<UserProfileDto> GetByPhoneAsync(string phone, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByPhone(phone, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<User, UserProfileDto>(user);
        }

        public async Task<UserProfileDto> GetByLoginAsync(string login, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncByLogin(login, cancellationToken);
            if (user == null) throw new Exception("User not found");
            return _mapper.Map<User, UserProfileDto>(user);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task BanAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            if (user == null) throw new Exception("User not found");
            user.Status = UserStatus.Banned;
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task ChangeRole(int id, UserRoles role, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            if (user == null) throw new Exception("User not found");
            if (role == UserRoles.None) throw new Exception("Incorrect data");
            user.Role = role;
            await _userRepository.UpdateAsync(user, cancellationToken);
        }
    }
}
