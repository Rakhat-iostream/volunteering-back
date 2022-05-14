using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Auth
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken> GetAsync(string token, CancellationToken cancellationToken = default);
        public Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);
        public Task DeleteAsync(RefreshToken token, CancellationToken cancellationToken = default);
    }
}
