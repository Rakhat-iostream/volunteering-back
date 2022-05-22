using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Organizations;

namespace Volunteer.Common.Services.Organizations
{
    public interface IOrganizationService
    {
        public Task<Organization> GetByUserId(int userId);
        public Task<OrganizationProfileDto> CreateAsync(OrganizationAddDto dto, User user);
        public Task<OrganizationProfileDto> UpdateAsync(OrganizationUpdateDto dto, User user);
    }
}
