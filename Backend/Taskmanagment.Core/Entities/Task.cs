using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Entities
{
    public class Task : Dtos.Task
    {
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual TaskStatus  TaskStatus{get; set;}
    }
}
