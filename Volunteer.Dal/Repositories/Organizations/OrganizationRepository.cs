using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.ClientRequests;
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

        public async Task<Organization> GetAsync(int organizationId)
        {
            var organizations = await _db.Organizations.FirstOrDefaultAsync(x => x.OrganizationId == organizationId);
            return organizations;
        }

        public async Task<Organization> GetByUserId(int userId)
        {
            var organization = await _db.Organizations.FirstOrDefaultAsync(x => x.UserId == userId);
            return organization;
        }

        public ICollection<Organization> GetAll(FilterOrganizationRequest request)
        {
            var entity = _db.Organizations.ToList();

            if (request.Region.HasValue)
            {
                entity = entity.Where(x => x.Region == request.Region).ToList();
            }

            if (request.OrganizationTypes.HasValue)
            {
                entity = entity.Where(x => x.OrganizationTypes.Any(x => x == request.OrganizationTypes)).ToList();
            }

            if (request.VolunteeringCategories.HasValue)
            {
                entity = entity.Where(x => x.VolunteeringCategories.Any(x => x == request.VolunteeringCategories)).ToList();
            }

            return entity;
        }

        public ICollection<Common.Models.Domain.Volunteer> GetAllVolunteers(FilterVolunteerRequest request)
        {
            var entity = _db.Volunteers.ToList();

            if (request.Region.HasValue)
            {
                entity = entity.Where(x => x.Region == request.Region).ToList();
            }

            if (request.VolunteeringCategories.HasValue)
            {
                entity = entity.Where(x => x.VolunteeringCategories.Any(x => x == request.VolunteeringCategories)).ToList();
            }

            return entity;
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
            var entity = await _db.Organizations.FirstOrDefaultAsync(x => x.OrganizationId == organization.OrganizationId);

            entity.Region = organization.Region ?? entity.Region;
            entity.Location = organization.Location ?? entity.Location;
            entity.Experience = organization.Experience ?? entity.Experience;
            entity.OrganizationName = organization.OrganizationName ?? entity.OrganizationName;
            entity.OrganizationTypes = organization.OrganizationTypes ?? entity.OrganizationTypes;
            entity.OrganizedDate = organization.OrganizedDate ?? entity.OrganizedDate;
            entity.VolunteeringCategories = organization.VolunteeringCategories ?? entity.VolunteeringCategories;
            entity.Description = organization.Description ?? entity.Description;
            entity.CEO = organization.CEO ?? entity.CEO;

            var result = _db.Organizations.Update(entity);
            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(int organizationId)
        {
            var entity = await _db.Organizations.FirstOrDefaultAsync(x => x.OrganizationId == organizationId);
            _db.Organizations.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Organization> VerifyOrganization(int organizationId)
        {
            var entity = await _db.Organizations.FirstOrDefaultAsync(x => x.OrganizationId == organizationId);

            entity.ValidationStatus = Common.Models.Domain.Enum.ValidationStatus.Verified;

             _db.Organizations.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
