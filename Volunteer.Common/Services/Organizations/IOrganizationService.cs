using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Organizations;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Common.Services.Organizations
{
    public interface IOrganizationService
    {
        public Task<OrganizationProfileDto> GetAsync(int organizationId);
        public Task<Organization> GetByUserId(int userId);
        public Task<PageResponse<OrganizationProfileDto>> GetAll(FilterOrganizationRequest request);
        public Task<PageResponse<VolunteerProfileDto>> GetAllVolunteers(FilterVolunteerRequest request);
        public Task<OrganizationProfileDto> CreateAsync(OrganizationAddDto dto, User user);
        public Task<OrganizationProfileDto> UpdateAsync(OrganizationUpdateDto dto, User user);
        public Task DeleteAsync(int organizationId);
        public Task<Organization> VerifyOrganization(int organizationId);
    }
}
