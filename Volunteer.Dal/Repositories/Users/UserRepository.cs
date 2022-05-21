using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Crypto;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Auth;
using Volunteer.Common.Repositories.Users;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly VolContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(VolContext db, IPasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
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
            return await _db.Users.AsNoTracking().FirstOrDefaultAsync((u => u.Login == login), cancellationToken);
        }
        public async Task<User> GetAsyncByPhone(string phone, CancellationToken cancellationToken = default)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Phone == phone), cancellationToken);
        }

        public async Task<User> CreateAsync(UserRegisterOrRecoveryDto dto)
        {
            var user = new User
            {
                Id = new int(),
                Login = dto.Login,
                Phone = dto.Phone,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = Common.Models.Domain.Enum.UserRoles.User,
            };

            //user.SetFullName(dto.FirstName, dto.LastName);

            var result = _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _db.Users.AsNoTracking().FirstOrDefaultAsync((u => u.Id == user.Id), cancellationToken: cancellationToken);

            entity.Login = user.Login ?? entity.Login;
            entity.Phone = user.Phone ?? entity.Phone;
            entity.Email = user.Email ?? entity.Email;
            entity.FirstName = user.FirstName ?? entity.FirstName;
            entity.LastName = user.LastName ?? entity.LastName;
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
