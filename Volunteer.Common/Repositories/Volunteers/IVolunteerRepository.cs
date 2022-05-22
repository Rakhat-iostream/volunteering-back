using System.Threading.Tasks;

namespace Volunteer.Common.Repositories.Volunteers
{
    public interface IVolunteerRepository
    {
        public Task<Common.Models.Domain.Volunteer> GetByUserId(int userId);
        public Task<Models.Domain.Volunteer> GetById(int volunteerId);
        public Task<Models.Domain.Volunteer> CreateAsync(Models.Domain.Volunteer volunteer);
        public Task<Models.Domain.Volunteer> UpdateAsync(Models.Domain.Volunteer volunteer);
    }
}
