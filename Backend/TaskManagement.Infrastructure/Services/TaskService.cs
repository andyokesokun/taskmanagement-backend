using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : BaseRepository<Core.Entities.Task>, ITaskRepository
    {
        public TaskService(DataContext dataContext) : base(dataContext)
        {
        

        }

        public async Task<ICollection<Core.Entities.Task>> FindAllWithRelations()
        {

            return await _dataContext.Tasks
                 .Include(s => s.AppUsers)
                 .Include(s => s.TaskStatus)
                 .ToListAsync();

        }

        public async Task<Core.Entities.Task> FindWithRelations(int id)
        {

            return await _dataContext.Tasks
                 .Include(s => s.AppUsers)
                 .Include(s => s.TaskStatus)
                 .Where(s => s.Id == id)
                 .SingleOrDefaultAsync();

        }
    }
}
