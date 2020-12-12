using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Entities;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Interfaces;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : BaseRepository<Entities.Task>, ITaskRepository
    {
        public TaskService(DataContext dataContext) : base(dataContext)
        {
        

        }

        public async Task<ICollection<Entities.Task>> FindAllWithRelations()
        {

            return await _dataContext.Tasks
                 .Include( s =>  s.AssignedTasks)
                 .ThenInclude(s => s.AppUser)
                 .Include(s => s.TaskStatus)
                 .ToListAsync();

        }

        public async Task<Entities.Task> FindWithRelations(int id)
        {

            return await _dataContext.Tasks
                 .Include(s => s.AssignedTasks)
                 .ThenInclude(s => s.AppUser)
                 .Include(s => s.TaskStatus)
                 .Where(s => s.Id == id)
                 .SingleOrDefaultAsync();

        }

        public async Task<ICollection<Entities.Task> > FindUserTasks(string  userId)
        {


            return await _dataContext.Tasks
                 .Include(s => s.AssignedTasks)
                 .ThenInclude(s => s.AppUser)
                 .Include(s => s.TaskStatus)
                 .Where(s => s.AssignedTasks.Any(s => s.AppUserId == userId) )
                 .ToListAsync();
          

        }

        public async System.Threading.Tasks.Task SaveAssignedTask(AssignedTask assignedTask )
        {
              _dataContext.Add(assignedTask);
              await _dataContext.SaveChangesAsync();
        } 
    }
}
