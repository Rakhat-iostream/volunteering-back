using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            if (volunteer != null)
            {
                 _db.Volunteers.Update(volunteer);
                await _db.SaveChangesAsync();
            }

            return volunteer;
        }
    }
}
