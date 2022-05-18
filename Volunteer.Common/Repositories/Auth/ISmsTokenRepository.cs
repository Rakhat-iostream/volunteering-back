using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Auth
{
    public interface ISmsTokenRepository
    {
        public Task AddAsync(SmsCode code, CancellationToken cancellationToken = default);
        public Task DeleteRangeAsync(IEnumerable<SmsCode> codes, CancellationToken cancellationToken = default);
        public IEnumerable<SmsCode> GetAll();
    }
}
