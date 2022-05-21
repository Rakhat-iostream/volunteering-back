using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Organizations
{
    public interface IOrganizationRepository
    {
        public Task<Organization> CreateAsync(Organization organization);
        public Task<Organization> UpdateAsync(Organization organization);
    }
}
