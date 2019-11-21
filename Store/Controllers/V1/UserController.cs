using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.V1;
using Store.Contracts.V1.Requests;
using Store.Contracts.V1.Responses;
using Store.Models;
using Store.Services;

namespace Store.Controllers.V1
{
    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost(ApiRoutes.Users.Add)]
        public async Task<IActionResult> Add([FromBody]UserRequest userRequest)
        {
            var userResult = await userService.AddAsync(userRequest.Email, userRequest.Password);
            if (userResult.Errors.Any())
            {
                return BadRequest(new AuthFailedResponse { Errors = userResult.Errors });
            }
            return Ok(new AuthSuccesResponse { Token = userResult.Token });
        }

        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            var userResult = await userService.LoginAsync(userRequest.Email, userRequest.Password);
            
            if (userResult.Errors.Any())
            {
                return BadRequest(new AuthFailedResponse { Errors = userResult.Errors });
            }
            return Ok(new AuthSuccesResponse { Token=userResult.Token});
        }


    }
}