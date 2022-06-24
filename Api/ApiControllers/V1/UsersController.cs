﻿using Application.Commands;
using Application.Commands.Users;
using Application.Queries;
using Application.Queries.Users;
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
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetUserWithPaginationQuery query)
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

        // POST api/Users
        [HttpPost("Student")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostStudent([FromBody] CreateStudentCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<UserResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // POST api/Mentor
        [HttpPost("Mentor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostMentor([FromBody] CreateMentorCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception ex)
            {
                var response = new Response<UserResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
