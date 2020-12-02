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
using TaskManagement.Core.Dtos;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Extensions;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskService;
        private readonly IMapper _mapper;

        public TaskController(ILogger<TaskController> logger,ITaskRepository  taskService, IMapper mapper)
        {
            _logger = logger;
            _taskService = taskService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IEnumerable<TaskResponse>> GetTasks()
        {

            var task = await _taskService.FindAll();
            return task.Select(s => s.MapTaskResponse());



        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("id")]
        public async Task<ActionResult<TaskResponse> >  Get([FromRoute] int id)
        {

            
            var task = await _taskService.Find(id);
            if (task == null) {
                return NotFound();
            }

            return task.MapTaskResponse();



        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public  async Task<ActionResult<TaskResponse>>  Post([Bind] Core.Dtos.Task task) {

            var role = User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role)?.Value;

            //admin creates task
            if (role == null) {
                return Unauthorized();
            }

     
            var entity = _mapper.Map<Core.Entities.Task>(task);

            await _taskService.Save(entity);

            return CreatedAtAction(nameof(Post), entity);



        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPut]
        [Route("id")]
        public async Task<ActionResult<TaskResponse>> Put([FromRoute]int id, [Bind] Core.Dtos.Task task)
        {

            var role = User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role)?.Value;

            //admin creates task
            if (role == null)
            {
                return Unauthorized();
            }

        

            if(id != task.Id ) {
                return NotFound();
            }


            var entity = await _taskService.Find(id);

            if (entity  == null) {
                return NotFound();
            }


             entity = _mapper.Map<Core.Entities.Task>(task);

            await _taskService.Save(entity);

            return Ok(entity.MapTaskResponse());



        }

        


    }
}