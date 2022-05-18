using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories
{
    public class SmsTokenRepository : ISmsTokenRepository
    {
        private readonly VolContext _db;

        public SmsTokenRepository(VolContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SmsCode code, CancellationToken cancellationToken = default)
        {
            await _db.SmsCodes.AddAsync(code, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<SmsCode> codes, CancellationToken cancellationToken = default)
        {
            _db.RemoveRange(_db.SmsCodes);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public IEnumerable<SmsCode> GetAll()
        {
            var smsCodes = _db.SmsCodes.AsNoTracking();
            return smsCodes;
        }
    }
}
