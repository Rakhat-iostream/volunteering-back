using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Events
{
    public interface IEventRepository
    {
        public Task<Event> GetAsync(int eventId);
        public ICollection<Event> GetAll(PageRequest request);
        public ICollection<Event> GetAllForOrganization(PageRequest request, int organizationId);
        public Task<Event> CreateAsync(Event events);
        public Task<Event> UpdateAsync(Event events);
        public void CompleteEvent(int eventId);
        public Task DeleteAsync(int eventId);
        public Task<Event> JoinToEvent(int eventId, Common.Models.Domain.Volunteer volunteer);
        public Task<Event> LeaveFromEvent(int eventId, Common.Models.Domain.Volunteer volunteer);
    }
}
