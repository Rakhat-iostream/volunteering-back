using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.DTOs;
using Volunteer.Common.Models.DTOs.Auth;
using Volunteer.Common.Services.Auth;

namespace Volunteer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        // Auth

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> Refresh(RefreshTokenDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    refreshToken = dto.RefreshToken;
                }

                var result = await _authService.RefreshToken(refreshToken, cancellationToken);
                SetTokenCookie(result.RefreshToken);
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

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.GetTokenByLogin(loginDto.Login, loginDto.Password, cancellationToken);
                SetTokenCookie(result.RefreshToken);
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

        [HttpPost("revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> RevokeToken(RevokeTokenDto tokenDto, CancellationToken cancellationToken)
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                {
                    refreshToken = tokenDto.Token;
                }

                await _authService.RevokeToken(refreshToken, cancellationToken);
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

        //SMS Auth

        [HttpPost("request-code")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> Code(RequestCodeDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var code = await _authService.RequestSmsCode(dto.Phone, cancellationToken);
                return Ok(new { Code = code });
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

        [HttpPost("verify-code")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> VerifyCode(VerifyCodeDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var isVerified = await _authService.VerifySmsCode(dto.Phone, dto.Code, cancellationToken);

                return Ok(new { IsVerified = isVerified });
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

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> RegisterAsync(UserRegisterOrRecoveryDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto, cancellationToken);
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

        //SMS Registration

        [HttpPost("register-user")]
        [HttpPost("recover-password")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        public async Task<IActionResult> UserRegisterOrRecovery(UserRegisterOrRecoveryDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.UserRegister(dto.Phone, dto.Password, dto.RepeatPassword, cancellationToken);
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

        [HttpGet("is-exists")]
        public async Task<IActionResult> IsUserExists(string phone, CancellationToken cancellationToken)
        {
            try
            {
                var isExists = await _authService.IsUserExists(phone, cancellationToken);

                return Ok(new { IsExists = isExists });
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

        // helper methods

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

    }
}
