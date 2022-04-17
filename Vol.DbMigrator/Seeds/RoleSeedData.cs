using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.EntityFrameworkCore;
using Vol.Roles;

namespace Vol.DbMigrator.Seeds
{
    public class RoleSeedData
    {
        private VolMigrationsDbContext _context;

        public RoleSeedData(VolMigrationsDbContext context)
        {
            _context = context;
        }

        public async Task SeedRoles()
        {
            var roles = new string[] { RoleConstants.Admin/*, RoleConstants.Partner, RoleConstants.Worker, RoleConstants.Customer*/ };

            var roleStore = new RoleStore<IdentityRole<Guid>, VolMigrationsDbContext, Guid>(_context);

            foreach (var role in roles)
            {
                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole<Guid> { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
