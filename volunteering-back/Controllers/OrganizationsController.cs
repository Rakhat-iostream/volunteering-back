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
using Volunteer.Common.Models.DTOs.Organizations;
using Volunteer.Common.Services.Organizations;
using Volunteer.Common.Services.Users;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrganizationsController(IOrganizationService organizationService, 
            IUserService userService, 
            IMapper mapper)
        {
            _organizationService = organizationService;
            _userService = userService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _organizationService.GetAsync(id);

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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organization))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("organization/userId")]
        public async Task<IActionResult> GetByUserId(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                //var mapped = _mapper.Map<User>(user);
                var result = await _organizationService.GetByUserId(user.Id);
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
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<OrganizationProfileDto>))]
            [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            [ProducesResponseType(StatusCodes.Status403Forbidden)]
            [HttpGet("list")]
            public async Task<IActionResult> GetAll([FromQuery] FilterOrganizationRequest request)
            {
                try
                {
                    var result = await _organizationService.GetAll(request);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("organization-register")]
        public async Task<IActionResult> RegisterAsync(OrganizationAddDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                var result = await _organizationService.CreateAsync(dto, mapped);
                result.Login = user.Login;
                result.Phone = user.Phone;
                result.Email = user.Email;
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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("organization/update")]
        public async Task<IActionResult> UpdateAsync(OrganizationUpdateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                var result = await _organizationService.UpdateAsync(dto, mapped);
                result.Login = user.Login;
                result.Phone = user.Phone;
                result.Email = user.Email;
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
