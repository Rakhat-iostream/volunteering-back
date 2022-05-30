using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain.Enum;
using Volunteer.Common.Models.DTOs.User;
using Volunteer.Common.Services.Users;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region User methods

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetSignedAccout(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetSignedUser(cancellationToken);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("edit-profile")]
        public async Task<IActionResult> EditProfile(UserUpdateActionDto userUpdateActionDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetSignedUser(cancellationToken);
                userUpdateActionDto.Id = user.Id;
                var result = await _userService.UpdateAsync(userUpdateActionDto, cancellationToken);
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

        /*[HttpPost("accept-invite")]
        public async Task<IActionResult> AcceptCompany(CancellationToken cancellationToken)
        {
            try
            {
                await _corparateUserService.AcceptCompany(cancellationToken);
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

        [HttpPost("decline-invite")]
        [HttpPost("remove-company")]
        public async Task<IActionResult> DeclineCompany(CancellationToken cancellationToken)
        {
            try
            {
                await _corparateUserService.DeclineCompany(cancellationToken);
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
        }*/

        #endregion

        #region Administrator methods

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAsync(id, cancellationToken);

                if (result is null)
                {
                    var error = new JsonResult(new
                    {
                        statusCode = 400,
                        message = "UserAction not found",
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

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<UserProfileDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.GetAll(pageRequest, cancellationToken);
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

        /*[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddActionDto userAddActionDto, CancellationToken cancellationToken)
        {
            try
            {
                if (userAddActionDto.Role == UserRoles.HR && (userAddActionDto.CompanyId == null || userAddActionDto.Position.IsNullOrEmpty()))
                {
                    var error = new JsonResult(new
                    {
                        statusCode = 400,
                        message = "Company or Position required",
                    });

                    return BadRequest(error);

                }

                var result = await _userService.AddAsync(userAddActionDto, cancellationToken);

                var corpUser = new AddExistingUserToCorporateDto()
                {
                    Phone = userAddActionDto.Phone
                };

                result.CorporateUser = await _corparateUserService.AddExistingUserToCorporate(corpUser, cancellationToken); ;

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
        }*/

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateActionDto userUpdateActionDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.UpdateAsync(userUpdateActionDto, cancellationToken);
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


        #endregion

        #region Administrator methods

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteAsync(id, cancellationToken);
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

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("ban")]
        public async Task<IActionResult> Ban(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.BanAsync(id, cancellationToken);
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

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole(int id, UserRoles role, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.ChangeRole(id, role, cancellationToken);
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
        #endregion
    }
}
