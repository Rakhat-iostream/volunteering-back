using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Events;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Services.Events;
using Volunteer.Common.Services.Organizations;
using Volunteer.Common.Services.Users;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, 
            IOrganizationService organizationService, 
            IUserService userService, 
            IMapper mapper)
        {
            _eventService = eventService;
            _organizationService = organizationService;
            _userService = userService;
            _mapper = mapper;
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventViewDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _eventService.GetAsync(id);

                if (result is null)
                {
                    var error = new JsonResult(new
                    {
                        statusCode = 400,
                        message = "Event not found",
                    });

                    return BadRequest(error);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<EventViewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _eventService.GetAll(pageRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize(Roles = "OrganizationAdmin, Volunteer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<EventViewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list/organization")]
        public async Task<IActionResult> GetAllForOrganization([FromQuery] PageRequest pageRequest,CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                var organization = await _organizationService.GetByUserId(mapped.Id);
                var result = await _eventService.GetAllForOrganization(pageRequest, organization.OrganizationId);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize(Roles = "OrganizationAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventViewDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("event/create")]
        public async Task<IActionResult> CreateAsync(EventAddDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                var organization = await _organizationService.GetByUserId(mapped.Id);
                var result = await _eventService.CreateAsync(dto, organization);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize(Roles = "OrganizationAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventViewDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("event/update")]
        public async Task<IActionResult> UpdateAsync(EventUpdateDto dto)
        {
            try
            {
                var result = await _eventService.UpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize(Roles = "OrganizationAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("event/complete")]
        public IActionResult CompleteEvent(int eventId)
        {
            try
            {
                _eventService.CompleteEvent(eventId);
                return Ok();
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize("OrganizationAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<VolunteerProfileDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("event/members")]
        public async Task<IActionResult> GetEventMembers([FromQuery] EventClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);

                var result = await _eventService.GetEventMembers(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int eventId)
        {
            try
            {
                await _eventService.DeleteAsync(eventId);
                return Ok();
            }
            catch (Exception e)
            {
                var error = new JsonResult(new
                {
                    statusCode = 400,
                    message = e.Message,
                });

                return BadRequest(error.Value);
            }
        }
    }
}
