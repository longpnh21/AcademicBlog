using Application.Commands;
using Application.Commands.Tags;
using Application.Queries;
using Application.Queries.Tags;
using Application.Response;
using Application.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.ApiControllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TagsController : ApiControllerBase
    {
        // GET: api/Tags
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetTagWithPaginationQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // GET api/Tags/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest();
                }
                GetTagWithIdQuery query = new GetTagWithIdQuery()
                {
                    TagId = id
                };
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // POST api/Tags
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateTagCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // PUT api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditTagCommand command)
        {
            try
            {
                if(id != command.Id)
                {
                    var response = new Response<TagResponse>("The Id do not match");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode((int)response.StatusCode, response);
                }
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // DELETE api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                DeleteTagCommand command = new DeleteTagCommand();
                command.TagId = id;
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
