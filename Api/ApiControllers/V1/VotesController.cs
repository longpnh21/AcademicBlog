using Application.Commands.Votes;
using Application.Queries.Votes;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.ApiControllers.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VotesController : ApiControllerBase
    {
        private readonly UserManager<User> _userManager;

        public VotesController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Votes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] GetVoteQuery query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return StatusCode((int)response.StatusCode, response);
            }
        }


        // POST api/Votes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] CreateVoteCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = (User)HttpContext.Items["User"];
                //remove when release
                var admin = await _userManager.FindByEmailAsync("administrator@academicvote.com");
                command.UserId = user.Id ?? admin.Id;

                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // PUT api/Votes/5
        [HttpPut("{blogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(int blogId, [FromBody] EditVoteCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (blogId != command.BlogId)
                {
                    return BadRequest();
                }

                var user = (User)HttpContext.Items["User"];
                //remove when release
                var admin = await _userManager.FindByEmailAsync("administrator@academicvote.com");
                command.UserId = user.Id ?? admin.Id;

                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // DELETE api/Votes/5
        [HttpDelete("{blogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int blogId)
        {
            try
            {
                if (blogId < 0)
                {
                    return BadRequest();
                }

                var user = (User)HttpContext.Items["User"];
                var result = await Mediator.Send(new DeleteVoteCommand { BlogId = blogId, UserId = user.Id });
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
