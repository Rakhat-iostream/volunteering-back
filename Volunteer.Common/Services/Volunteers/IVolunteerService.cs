using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Common.Services.Volunteers
{
    public interface IVolunteerService
    {
        public Task<VolunteerProfileDto> CreateAsync(VolunteerAddDto dto, User user);
    }
}
