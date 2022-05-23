using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Extensions;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Events;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Repositories.Events;
using Volunteer.Common.Repositories.Organizations;
using Volunteer.Common.Services.Events;

namespace Volunteer.BL.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IOrganizationRepository organizationRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventViewDto> GetAsync(int eventId)
        {
            var events = await _eventRepository.GetAsync(eventId);
            return _mapper.Map<Event, EventViewDto>(events);
        }

        public async Task<PageResponse<EventViewDto>> GetAll(PageRequest request)
        {
            var organizations = _eventRepository.GetAll(request);

            var total = organizations.Count();
            var model = _mapper.Map<List<EventViewDto>>(organizations);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<EventViewDto>
            {
                Total = total,
                Result = result

            };
        }

        public async Task<PageResponse<EventViewDto>> GetAllForOrganization(PageRequest request, int organizationId)
        {
            var events = _eventRepository.GetAllForOrganization(request, organizationId);

            var total = events.Count();
            var model = _mapper.Map<List<EventViewDto>>(events);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<EventViewDto>
            {
                Total = total,
                Result = result

            };
        }

        public async Task<EventViewDto> CreateAsync(EventAddDto dto, Organization organization)
        {
            Event events = new Event(); 
            events.Organization = organization;
            events.OrganizationId = organization.OrganizationId;
            events.EventId = new int();
            events.EventName = dto.EventName;
            events.Deadline = dto.Deadline.Date;
            events.EndDate = dto.EndDate.Date;
            events.Description = dto.Description;
            events.IsFinished = false;
            events.VolunteeringCategory = dto.VolunteeringCategory;

            await _eventRepository.CreateAsync(events);

            var model = _mapper.Map<EventViewDto>(events);
            return model;

        }

        public async Task<EventViewDto> UpdateAsync(EventUpdateDto dto)
        {
            var events = _mapper.Map<EventUpdateDto, Event>(dto);

            await _eventRepository.UpdateAsync(events);

            var profile = _mapper.Map<EventViewDto>(events);
            return profile;
        }

        public void CompleteEvent(int eventId)
        {
            _eventRepository.CompleteEvent(eventId);
        }

        public async Task DeleteAsync(int eventId)
        {
            await _eventRepository.DeleteAsync(eventId);
        }

        public async Task<Event> JoinToEvent(int eventId, Common.Models.Domain.Volunteer volunteer)
        {
            var result = await _eventRepository.JoinToEvent(eventId, volunteer);
            return result;
        }

        public async Task<Event> LeaveFromEvent(int eventId, Common.Models.Domain.Volunteer volunteer)
        {
            var result = await _eventRepository.LeaveFromEvent(eventId, volunteer);
            return result;
        }

        public async Task<PageResponse<VolunteerProfileDto>> GetEventMembers(EventClientRequest request)
        {
            var candidates = _eventRepository.GetEventMembers(request.EventId ?? 0);

            var total = candidates.Count();
            var model = _mapper.Map<List<VolunteerProfileDto>>(candidates);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<VolunteerProfileDto>
            {
                Total = total,
                Result = result
            };

        }
    }
}
