using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Organizations;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories.Organizations
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly VolContext _db;

        public OrganizationRepository(VolContext db)
        {
            _db = db;
        }

        public async Task<Organization> CreateAsync(Organization organization)
        {
            if (organization != null)
            {
                await _db.Organizations.AddAsync(organization);
                await _db.SaveChangesAsync();   
            }

            return organization;
        }

        public async Task<Organization> UpdateAsync(Organization organization)
        {
            if (organization != null)
            {
                 _db.Organizations.Update(organization);
                await _db.SaveChangesAsync();
            }

            return organization;
        }
    }
}
