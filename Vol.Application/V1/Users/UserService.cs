﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Infrastructure;
using Vol.Users;
using Vol.V1.Users;

namespace Vol.V1.Users
{
    public class UserService :
        ReadAppService<User, UserV1Dto, Guid>,
        IUserService
    {
        public UserService(IRepository<User, Guid> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IReadOnlyCollection<UserV1Dto>> GetListAsync(UserListQuery query)
        {
            if (query.UserIds?.Any() == true)
            {
                var users = await Repository.GetListAsync(x => query.UserIds.Contains(x.Id));
                return users.Select(base.MapToGetOutputDto).ToArray();
            }

            return null;
        }
    }
}
