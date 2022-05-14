using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Users
{
    public interface IUserRepository
    {
        public IQueryable<User> GetAll();
        public Task<User> GetAsync(int id, CancellationToken cancellationToken = default);
        public Task<User> GetAsyncByLogin(string login, CancellationToken cancellationToken = default);
        public Task<User> GetAsyncByPhone(string phone, CancellationToken cancellationToken = default);
        public Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
        public Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
        public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
