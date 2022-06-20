using Application.Commands;
using Application.Commands.Blogs;
using Application.Queries;
using Application.Queries.Blogs;
using Application.Response;
using Application.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.ApiControllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogsController : ApiControllerBase
    {
        // GET: api/Blogs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetBlogWithPaginationQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<BlogResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // GET api/Blogs/5
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
                GetBlogWithIdQuery query = new GetBlogWithIdQuery()
                {
                    BlogId = id
                };
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<BlogResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // POST api/Blogs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateBlogCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<BlogResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // PUT api/Blogs/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Blogs/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
