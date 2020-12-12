using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Dtos;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class EntityExtensions
    {


        public static TaskResponse MapTaskResponse(this Entities.Task task) {

            return new TaskResponse
            {

                Id = task.Id,
                DueDate = task.DueDate,
                Name = task.Name,
                Description = task.Description,
                TaskStatusId = task.TaskStatusId,
                TaskStatus = task.TaskStatus,  
                AssignedUsers = task.AssignedTasks?.Select(s => new UserResponse{ UserName = s.AppUser.UserName } ).ToList()

            };
        }


        public static UserResponse MapUserResponse(this Entities.AppUser appUser)
        {

            return new UserResponse
            {
                  UserId =appUser.Id,
                  UserName = appUser.UserName
            };
        }
    }
}
