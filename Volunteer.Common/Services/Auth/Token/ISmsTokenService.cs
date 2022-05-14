using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Services.Auth.Token
{
    public interface ISmsTokenService
    {
        public Task<string> Generate(User user, CancellationToken cancellationToken);
        public Task<bool> Verify(User user, string token, CancellationToken cancellationToken);
    }
}
