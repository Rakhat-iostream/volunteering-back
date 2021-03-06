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
using Volunteer.Common.Models.Domain.ViewModels;
using Volunteer.Common.Models.DTOs.Memberships;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Services.Memberships;
using Volunteer.Common.Services.Organizations;
using Volunteer.Common.Services.Users;
using Volunteer.Common.Services.Volunteers;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IVolunteerService _volunteerService;
        private readonly IMapper _mapper;

        public MembershipController(IMembershipService membershipService, 
            IOrganizationService organizationService, 
            IUserService userService, 
            IVolunteerService volunteerService,
            IMapper mapper)
        {
            _membershipService = membershipService;
            _organizationService = organizationService;
            _userService = userService;
            _volunteerService = volunteerService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Volunteer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MembershipViewModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("volunteer/request-membership")]
        public async Task<IActionResult> CreateAsync([FromBody] MembershipAddDto model, CancellationToken cancellationToken)
        {
            try
            {
                var user =  await _userService.GetSignedUser(cancellationToken);

                var volunteer = await _volunteerService.GetByUserId(user.Id);

                var res = await _membershipService.AddMembership(model, volunteer.VolunteerId);
                var result = _mapper.Map<MembershipViewModel>(res);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembershipViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("organization/changeRequest")]
        public async Task<IActionResult> ChangeRequest([FromQuery] MembershipClientRequest clientRequest)
        {
            try
            {
                var model = await _membershipService.ChangeMembershipStatus(clientRequest);

                var result = _mapper.Map<MembershipViewModel>(model);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<VolunteerProfileDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("organization/candidates")]
        public async Task<IActionResult> GetCandidates([FromQuery] PageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);

                var organization = await _organizationService.GetByUserId(user.Id);

                var result = await _membershipService.GetCandidates(request, organization.OrganizationId);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<VolunteerProfileDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("organization/members")]
        public async Task<IActionResult> GetMembers([FromQuery] PageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);

                var organization = await _organizationService.GetByUserId(user.Id);

                var result = await _membershipService.GetMembers(request, organization.OrganizationId);
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

        [Authorize(Roles = "Volunteer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MembershipViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("volunteer/answer")]
        public async Task<IActionResult> VolunteerAnswer([FromQuery] MembershipClientRequest clientRequest)
        {
            try
            {
                var model = await _membershipService.ChangeVolunteerAnswer(clientRequest);

                var result = _mapper.Map<MembershipViewModel>(model);
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
        [HttpPost("organization/invite-volunteer")]
        public async Task<IActionResult> InviteVolunteer(int volunteerId, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                //var mapped = _mapper.Map<User>(user);

                var organization = await _organizationService.GetByUserId(user.Id);

                 _membershipService.InviteMembership(organization.OrganizationId, volunteerId);
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

        [Authorize(Roles = "Volunteer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<MembershipViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list/volunteer/invitations")]
        public async Task<IActionResult> InvitationsList([FromQuery] FilterMembershipRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);

                var volunteer = await _volunteerService.GetByUserId(user.Id);

                var result = await _membershipService.InvitationsList(request, volunteer.VolunteerId);
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

        [Authorize(Roles = "Volunteer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<MembershipViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list/volunteer/memberships")]
        public async Task<IActionResult> GetMembershipsByVolunteerId([FromQuery] FilterMembershipRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);

                var volunteer = await _volunteerService.GetByUserId(user.Id);

                var result = await _membershipService.GetMemberShipsByVolunteerId(request, volunteer.VolunteerId);
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

    }
}
