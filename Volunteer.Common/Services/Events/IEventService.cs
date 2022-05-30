using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Events;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Common.Services.Events
{
    public interface IEventService
    {
        public Task<EventViewDto> GetAsync(int eventId);
        public Task<PageResponse<EventViewDto>> GetAll(PageRequest request);
        public Task<PageResponse<EventViewDto>> GetAllForOrganization(PageRequest pageRequest, int organizationId);
        public Task<EventViewDto> CreateAsync(EventAddDto dto, Organization organization);
        public Task<EventViewDto> UpdateAsync(EventUpdateDto dto);
        public void CompleteEvent(int eventId);
        public Task DeleteAsync(int eventId);
        public Task<Event> JoinToEvent(int eventId, Models.Domain.Volunteer volunteer);
        public Task<Event> LeaveFromEvent(int eventId, Models.Domain.Volunteer volunteer);
        public Task<PageResponse<VolunteerProfileDto>> GetEventMembers(EventClientRequest request);
        public Task<PageResponse<VolunteerProfileDto>> GetEventAttenders(EventClientRequest request);
        public Task<Event> SubmitAttendance(int eventId, string code, int volunteerId);

    }
}
