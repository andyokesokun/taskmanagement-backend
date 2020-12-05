using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Data.Seeds
{
    public class TaskSeed
    {

        public static async System.Threading.Tasks.Task Create(ITaskRepository  taskRepository)
        {

            int records = await taskRepository.Count();
            if (records == 0) {

                var tasks = new Core.Entities.Task[]{
                new Core.Entities.Task { Id = 1, Name = "Create users", Description = "Handle this, this", DueDate = DateTime.Now, TaskStatusId = 1 },
                    new Core.Entities.Task { Id = 2, Name = "Create database", Description = "Handle this, this", DueDate = DateTime.Now.AddDays(-2), TaskStatusId = 2 },
                    new Core.Entities.Task { Id = 3, Name = "Call Admin ", Description = "Email or call Joe on +8939337381", DueDate = DateTime.Now, TaskStatusId = 3 }
                };

                await taskRepository.AddRange(tasks);

            }


        }

    }
}
