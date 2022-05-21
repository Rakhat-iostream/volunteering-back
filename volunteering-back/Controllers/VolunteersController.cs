using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Services.Users;
using Volunteer.Common.Services.Volunteers;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _volunteerService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public VolunteersController(IVolunteerService volunteerService,
            IUserService userService,
            IMapper mapper)
        {
            _volunteerService = volunteerService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VolunteerProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("volunteer/id")]
        public async Task<IActionResult> GetById(int volunteerId, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var result = await _volunteerService.GetById(volunteerId);
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


        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VolunteerProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("volunteer-register")]
        public async Task<IActionResult> RegisterAsync(VolunteerAddDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                //dto.UserId = user.Id;
                var result = await _volunteerService.CreateAsync(dto, mapped);
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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VolunteerProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("volunteer/update")]
        public async Task<IActionResult> UpdateAsync(VolunteerUpdateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                var mapped = _mapper.Map<User>(user);
                dto.UserId = user.Id;
                var result = await _volunteerService.UpdateAsync(dto, mapped);
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

        protected int? GetCurrentUserProfileId()
        {
            var user = HttpContext.User;
            try
            {
                if (user == null)
                {
                    return (null);
                }

                var raw = user.FindFirstValue("id");
                if (Int32.TryParse(raw, out var user_id))
                {
                    return (user_id);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("nope");
            }

            return (null);
        }


    }
}
