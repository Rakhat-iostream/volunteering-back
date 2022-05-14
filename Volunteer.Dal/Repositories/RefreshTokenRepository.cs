using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly VolContext _db;

        public RefreshTokenRepository(VolContext db)
        {
            _db = db;
        }


        public async Task<RefreshToken> GetAsync(string token, CancellationToken cancellationToken = default)
        {
            var entity = await _db.RefreshTokens.FirstOrDefaultAsync((t => t.Token == token), cancellationToken);
            return entity;
        }

        public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            await _db.RefreshTokens.AddAsync(token, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            _db.Remove(token);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
