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
using TaskManagement.Infrastructure.Extensions;
using TaskManagement.Dtos;
using TaskManagement.Interfaces;

namespace TaskManagement.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
   
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _taskService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TasksController(ILogger<TasksController> logger,ITaskRepository  taskService, IMapper mapper, IUserService userService )

        {
            _logger = logger;
            _taskService = taskService;
            _mapper = mapper;
            _userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IEnumerable<TaskResponse>> GetTasks()
        {

            var task = await _taskService.FindAllWithRelations();
            return task.Select(s => s.MapTaskResponse());

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<TaskResponse> >  Get([FromRoute] int id)
        {

            
            var task = await _taskService.Find(id);
            if (task == null) {
                return NotFound();
            }

            return task.MapTaskResponse();



        }

        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public  async Task<ActionResult<TaskResponse>>  Post([Bind] Dtos.Task task) {

            
            var entity = _mapper.Map<Entities.Task>(task);
            entity.TaskStatusId = (int)Entities.TaskStatus.Type.Pending;

            await _taskService.Save(entity);

            return CreatedAtAction(nameof(Post), entity); 



        }



        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [Route("assign")]
        public async Task<ActionResult<TaskResponse>> AssignTask([Bind] Dtos.AssignedModel assignedModel)
        {


            int.TryParse(assignedModel.TaskId , out var taskId);

            var task = await _taskService.Find(taskId);
            if (task == null)
            {
                return NotFound();
            }


            var user = await _userService.FindByUserName(assignedModel.userName);
            if (user == null)
            {
                return NotFound();
            }



            var entity = new Entities.AssignedTask
            {
                AppUserId = user.Id,
                TaskId = task.Id
            };

            await _taskService.SaveAssignedTask(entity);

            var result = await _taskService.FindWithRelations(task.Id);

            return CreatedAtAction(nameof(Post), result);



        }


        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<TaskResponse>> Put([FromRoute]int id, [Bind] Dtos.Task task)
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


            entity = _mapper.Map<Entities.Task>(task);

            await _taskService.Save(entity);

            return Ok(entity.MapTaskResponse());



        }


        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<TaskResponse>> Delete([FromRoute]int id)
        {

      

            var entity = await _taskService.Find(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _taskService.Delete(id);

            return Ok();



        }


    }
}