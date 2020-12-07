using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Entities
{
    public class TaskStatus : Dtos.TaskStatus
    {
       

        public enum Type
        {
            Pending =1,
            Started = 2,
            Completed = 3,
        }
    }

    
}
