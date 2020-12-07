using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagement.Dtos;
using TaskManagement.Infrastructure.Extensions;
using TaskManagement.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IEnumerable<UserResponse>> GetTasks()
        {

            var users = await _userService.GetAllUsers();
            return users.Select(s => s.MapUserResponse());

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("tasks/{username}")]
        [HttpGet]  
        public async Task<IEnumerable<TaskResponse>> GetUserTask(string username)
        {

            var user = await _userService.FindByUserName(username);
            var tasks = await _userService.GetUserTask(username);


            return tasks.Select(s => s.MapTaskResponse());

        }


        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post([Bind] UserModel userModel)
        {

          

            var defaultPassword = "password@123";
            var user=await _userService.CreateUser(userModel, defaultPassword);

            return CreatedAtAction(nameof(Post), user.MapUserResponse());


        }




    }
}