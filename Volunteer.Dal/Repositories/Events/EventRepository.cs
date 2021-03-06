using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories.Events;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly VolContext _db;

        public EventRepository(VolContext db)
        {
            _db = db;
        }

        public ICollection<Event> GetAll(EventClientRequest request)
        {
            var entity = _db.Events.ToList();

            if (request.Region.HasValue)
            {
                entity = entity.Where(x => x.Region == request.Region).ToList();
            }

            if (request.VolunteeringCategory.HasValue)
            {
                entity = entity.Where(x => x.VolunteeringCategory == request.VolunteeringCategory).ToList();
            }

            if (request.IsFinished.HasValue)
            {
                entity = entity.Where(x => x.IsFinished == request.IsFinished).ToList();
            }


            return entity;
        }

        public ICollection<Event> GetAllForOrganization(EventClientRequest request, int organizationId)
        {
            var entity = _db.Events.Where(x => x.OrganizationId == organizationId).ToList();

            if (request.IsFinished.HasValue)
            {
                entity = entity.Where(x => x.IsFinished == request.IsFinished).ToList();
            }

            return entity;
        }

        public async Task<Event> GetAsync(int eventId)
        {
            var events = await _db.Events.FirstOrDefaultAsync(x => x.EventId == eventId);
            return events;
        }

        public async Task<Event> CreateAsync(Event events)
        {
            if (events != null)
            {
                await _db.Events.AddAsync(events);
                await _db.SaveChangesAsync();
            }

            return events;
        }

        public async Task<Event> UpdateAsync(Event events)
        {
            var entity = await _db.Events.FirstOrDefaultAsync(x => x.EventId == events.EventId);

            entity.VolunteeringCategory = events.VolunteeringCategory ?? entity.VolunteeringCategory;
            entity.EndDate = events.EndDate ?? entity.EndDate;
            entity.Deadline = events.Deadline ?? entity.Deadline;
            entity.Description = events.Description ?? entity.Description;
            entity.EventName = events.EventName ?? entity.EventName;
            entity.IsFinished = events.IsFinished ?? entity.IsFinished;

            var result = _db.Events.Update(entity);
            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public void CompleteEvent(int eventId)
        {
            var entity =  _db.Events.FirstOrDefaultAsync(x => x.EventId == eventId);

            entity.Result.IsFinished = true;

            var result = _db.Events.Update(entity.Result);
             _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            var entity = await _db.Events.FirstOrDefaultAsync(x => x.EventId == eventId);
            _db.Events.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Event> JoinToEvent(int eventId, Common.Models.Domain.Volunteer volunteer)
        {
            var events = await _db.Events.FirstOrDefaultAsync(x => x.EventId == eventId && x.IsFinished == false);

            var volId = volunteer.VolunteerId;

            if (events.VolunteerIds == null)
            {
                events.VolunteerIds = new List<int> { };
                events.VolunteerIds.Add(volId);
                await _db.SaveChangesAsync();
            }

            else if (!events.VolunteerIds.Contains(volId))
            {
                events.VolunteerIds.Add(volId);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("You have already joined to this event");
            }

            return events;
        }

        public async Task<Event> LeaveFromEvent(int eventId, Common.Models.Domain.Volunteer volunteer)
        {
            var events = await _db.Events.FirstOrDefaultAsync(x => x.EventId == eventId && x.IsFinished == false);

            var volId = volunteer.VolunteerId;

            if (events.VolunteerIds.Contains(volId))
            {
                events.VolunteerIds.Remove(volId);
                await _db.SaveChangesAsync();
            }

            return events;
        }

        public ICollection<Common.Models.Domain.Volunteer> GetEventMembers(int eventId)
        {
            var events = _db.Events.Where(x => x.EventId == eventId).FirstOrDefault();

            var volunteers = _db.Volunteers.Where(x => events.VolunteerIds.Contains(x.VolunteerId)).ToList();

            return volunteers;
        }

        public ICollection<Common.Models.Domain.Volunteer> GetEventAttenders(int eventId)
        {
            var events = _db.Events.Where(x => x.EventId == eventId).FirstOrDefault();

            var volunteers = _db.Volunteers.Where(x => events.AttendedVolunteerIds.Contains(x.VolunteerId)).ToList();

            return volunteers;
        }

        public async Task<Event> SubmitAttendance(int eventId, string code, int volunteerId)
        {
            var events = _db.Events.FirstOrDefault(x => x.EventId == eventId && x.IsFinished == false);

            if (events.AttendanceCode == code)
            {
                if (events.AttendedVolunteerIds == null)
                {
                    events.AttendedVolunteerIds = new List<int> { };
                    events.AttendedVolunteerIds.Add(volunteerId);
                     await _db.SaveChangesAsync();
                }

                else if (!events.AttendedVolunteerIds.Contains(volunteerId))
                {
                    events.AttendedVolunteerIds.Add(volunteerId);
                     await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("You have already in attended volunteers list");
                }
            }

            return events;
        }
    }
}
