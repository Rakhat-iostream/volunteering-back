using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.News;
using Volunteer.Common.Services;

namespace volunteering_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(News))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _newsService.GetAsync(id);

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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<News>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _newsService.GetAll(pageRequest);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(News))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("news/create")]
        public async Task<IActionResult> CreateAsync(NewsAddOrUpdateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _newsService.CreateAsync(dto);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(News))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("news/update")]
        public async Task<IActionResult> UpdateAsync(NewsAddOrUpdateDto dto)
        {
            try
            {
                var result = await _newsService.UpdateAsync(dto);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(JsonResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _newsService.DeleteAsync(id);
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
