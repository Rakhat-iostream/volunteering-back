using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Organizations
{
    public interface IOrganizationRepository
    {
        public Task<Organization> GetAsync(int organizationId);
        public Task<Organization> GetByUserId(int userId);
        public Task<Organization> CreateAsync(Organization organization);
        public Task<Organization> UpdateAsync(Organization organization);
        public ICollection<Organization> GetAll(FilterOrganizationRequest request);
        public ICollection<Common.Models.Domain.Volunteer> GetAllVolunteers(FilterVolunteerRequest request);
    }
}
