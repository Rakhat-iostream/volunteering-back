using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Infrastructure;

namespace Vol.V1.Users
{
    public interface IUserService : IReadAppService<UserV1Dto, Guid>
    {
        Task<IReadOnlyCollection<UserV1Dto>> GetListAsync(UserListQuery query);
    }
}
