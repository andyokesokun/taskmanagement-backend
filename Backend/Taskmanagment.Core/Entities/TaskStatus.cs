using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class TaskStatus : Dtos.TaskStatus
    {
       

        public enum Type
        {
            Pending,
            Started,
            Completed,
        }
    }

    
}
