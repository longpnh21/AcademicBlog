using Application.Commands;
using Application.Commands.Categories;
using Application.Queries;
using Application.Queries.Categories;
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
    public class CategoriesController : ApiControllerBase
    {
        // GET: api/Categorys
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetCategoryWithPaginationQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // GET api/Categorys/5
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
                GetCategoryWithIdQuery query = new GetCategoryWithIdQuery()
                {
                    CategoryId = id
                };
                var result = await Mediator.Send(query);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // POST api/Categorys
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // PUT api/Categorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCategoryCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    var response = new Response<CategoryResponse>("The Id do not match");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode((int)response.StatusCode, response);
                }
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // DELETE api/Categorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                DeleteCategoryCommand command = new DeleteCategoryCommand();
                command.CategoryId = id;
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
