using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Data.Seeds
{
    public class TaskStatusSeed
    {

        public static async System.Threading.Tasks.Task Create(ITaskStatusRepository  taskStatusRepository)
        {

            int records = await taskStatusRepository.Count();
            if (records == 0) {

                var taskStatuses = new TaskStatus[] {
                    new TaskStatus { Id = 1, Status = TaskStatus.Type.Pending.ToString() },
                    new TaskStatus { Id = 2, Status = TaskStatus.Type.Started.ToString() },
                    new TaskStatus { Id = 3, Status = TaskStatus.Type.Completed.ToString() }
                };

                await taskStatusRepository.AddRange(taskStatuses);

            }


        }

    }
}
