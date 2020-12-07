using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Interfaces;


namespace TaskManagement.Infrastructure.Data.Seeds
{
    public class TaskStatusSeed
    {

        public static async System.Threading.Tasks.Task Create(ITaskStatusRepository  taskStatusRepository)
        {

            int records = await taskStatusRepository.Count();
            if (records == 0) {

                var taskStatuses = new  Entities.TaskStatus[] {
                    new Entities.TaskStatus { Id = 1, Status =  Entities.TaskStatus.Type.Pending.ToString() },
                    new Entities.TaskStatus { Id = 2, Status =  Entities.TaskStatus.Type.Started.ToString() },
                    new Entities.TaskStatus { Id = 3, Status =  Entities.TaskStatus.Type.Completed.ToString() }
                };

                await taskStatusRepository.AddRange(taskStatuses);

            }


        }

    }
}
