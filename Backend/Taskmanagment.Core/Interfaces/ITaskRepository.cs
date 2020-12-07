
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement;
using System.Threading.Tasks;

namespace TaskManagement.Interfaces
{
    public interface  ITaskRepository : IRepository<Entities.Task>
    {

        Task<ICollection<Entities.Task>> FindAllWithRelations();
        Task  <Entities.Task> FindWithRelations(int id);
        Task SaveAssignedTask(Entities.AssignedTask assignedTask);



    }
}
