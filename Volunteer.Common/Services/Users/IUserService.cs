using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain.Enum;
using Volunteer.Common.Models.DTOs.User;

namespace Volunteer.Common.Services.Users
{
    public interface IUserService
    {
        public Task<UserProfileDto> AddAsync(UserAddActionDto userUpdateActionDto, CancellationToken cancellationToken);
        public Task<UserProfileDto> UpdateAsync(UserUpdateActionDto userUpdateActionDto, CancellationToken cancellationToken);
        public Task<PageResponse<UserProfileDto>> GetAll(PageRequest pageRequest, CancellationToken cancellationToken);
        public Task<UserProfileDto> GetSignedUser(CancellationToken cancellationToken);
        public Task<UserProfileDto> GetAsync(int id, CancellationToken cancellationToken);
        public Task<UserProfileDto> GetByPhoneAsync(string phone, CancellationToken cancellationToken);
        public Task<UserProfileDto> GetByLoginAsync(string login, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task BanAsync(int id, CancellationToken cancellationToken);
        public Task ChangeRole(int id, UserRoles role, CancellationToken cancellationToken);
    }
}
