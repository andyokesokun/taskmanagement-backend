using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : BaseRepository<Task>, ITaskRepository
    {
        public TaskService(DataContext dataContext) : base(dataContext)
        {
        }

    }
}
