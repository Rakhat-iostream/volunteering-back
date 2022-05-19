using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Volunteer.Common.Repositories.Volunteers
{
    public interface IVolunteerRepository
    {
        public Task<Models.Domain.Volunteer> CreateAsync(Models.Domain.Volunteer volunteer);
        public Task<Common.Models.Domain.Volunteer> UpdateAsync(Models.Domain.Volunteer volunteer);
    }
}
