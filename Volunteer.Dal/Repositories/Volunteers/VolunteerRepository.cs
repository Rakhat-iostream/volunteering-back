using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Repositories.Volunteers;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories.Volunteers
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly VolContext _db;

        public VolunteerRepository(VolContext db)
        {
            _db = db;
        }

        public async Task<Common.Models.Domain.Volunteer> GetById(int volunteerId)
        {
            Common.Models.Domain.Volunteer volunteer = _db.Volunteers.AsNoTracking().Where(x => x.VolunteerId == volunteerId).FirstOrDefault(); 

            return volunteer;
        }

        public async Task<Common.Models.Domain.Volunteer> CreateAsync(Common.Models.Domain.Volunteer volunteer)
        {
            if (volunteer != null)
            {
                await _db.Volunteers.AddAsync(volunteer);
                await _db.SaveChangesAsync();
            }

            return volunteer;
        }

        public async Task<Common.Models.Domain.Volunteer> UpdateAsync(Common.Models.Domain.Volunteer volunteer)
        {
            var entity = await _db.Volunteers.FirstOrDefaultAsync(x => x.VolunteerId == volunteer.VolunteerId);

            entity.BirthDate = volunteer.BirthDate ?? entity.BirthDate;
            entity.Description = volunteer.Description ?? entity.Description;
            entity.Region = volunteer.Region ?? entity.Region;
            entity.Sex = volunteer.Sex ?? entity.Sex;
            entity.Experience = volunteer.Experience ?? entity.Experience;
            entity.VolunteeringCategories = volunteer.VolunteeringCategories ?? entity.VolunteeringCategories;

            var result = _db.Volunteers.Update(entity);
            await _db.SaveChangesAsync();

            return result.Entity;
        }
    }
}
