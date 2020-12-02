﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Dtos;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class EntityExtensions
    {


        public static TaskResponse MapTaskResponse(this Core.Entities.Task task) {

            return new TaskResponse
            {

                Id = task.Id,
                DueDate = task.DueDate,
                name = task.name,
                description = task.description,
                UserResponses = task.AppUsers.Select(s => new UserResponse{ UserName = s.UserName } ).ToList()

            };
        }


        public static UserResponse MapUserResponse(this Core.Entities.AppUser appUser)
        {

            return new UserResponse
            {
                  UserName = appUser.UserName

            };
        }
    }
}