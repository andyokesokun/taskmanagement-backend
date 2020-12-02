using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class Task : Dtos.Task
    {
        public virtual ICollection<AppUser> AppUsers { get; set;}

    }
}
