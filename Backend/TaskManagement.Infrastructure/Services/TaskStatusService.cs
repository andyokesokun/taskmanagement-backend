using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskStatusService : BaseRepository<Core.Entities.TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
