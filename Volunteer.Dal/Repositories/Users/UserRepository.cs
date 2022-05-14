using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Users;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly VolContext _db;

        public UserRepository(VolContext db)
        {
            _db = db;
        }


        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _db.Users.AddAsync(user, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }

        public IQueryable<User> GetAll()
        {
            var user = _db.Users.AsNoTracking();
            return user;
        }

        public async Task<User> GetAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync((u => u.Id == id), cancellationToken);
            return user;
        }

        public async Task<User> GetAsyncByLogin(string login, CancellationToken cancellationToken)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Login == login), cancellationToken);
        }
        public async Task<User> GetAsyncByPhone(string phone, CancellationToken cancellationToken = default)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Phone == phone), cancellationToken);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _db.Users.FirstOrDefaultAsync((u => u.Id == user.Id), cancellationToken: cancellationToken);

            entity.Login = user.Login ?? entity.Login;
            entity.Phone = user.Phone ?? entity.Phone;
            entity.Email = user.Email ?? entity.Email;
            entity.FullName = user.FullName ?? entity.FullName;
            entity.PasswordHash = user.PasswordHash ?? entity.PasswordHash;
            entity.Role = user.Role;
            entity.Status = user.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            var result = _db.Users.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _db.Users.FirstOrDefaultAsync((u => u.Id == id), cancellationToken: cancellationToken);
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
