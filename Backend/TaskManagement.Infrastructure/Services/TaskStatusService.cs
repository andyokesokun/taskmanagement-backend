using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Interfaces;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskStatusService : BaseRepository<Entities.TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
