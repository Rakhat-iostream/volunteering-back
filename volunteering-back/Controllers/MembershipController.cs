using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("request-membership")]
        public async Task<IActionResult> CreateAsync([FromBody] MembershipViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                var user =  await _userService.GetSignedUser(cancellationToken);
                //var mapped = _mapper.Map<User>(user);

                var volunteer = await _volunteerService.GetByUserId(user.Id);

                await _membershipService.AddMembership(model, volunteer.VolunteerId);
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
