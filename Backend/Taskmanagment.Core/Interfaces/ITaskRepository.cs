
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement;
using System.Threading.Tasks;

namespace TaskManagement.Core.Interfaces
{
    public interface  ITaskRepository : IRepository<Entities.Task>
    {

        Task<ICollection<Core.Entities.Task>> FindAllWithRelations();
        Task  <Core.Entities.Task> FindWithRelations(int id);



    }
}
