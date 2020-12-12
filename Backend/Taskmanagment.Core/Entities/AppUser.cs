using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Entities
{
    public class AppUser : IdentityUser
    {    
        public bool  IsAdmin { get; set; }
        public virtual ICollection<AssignedTask> AssignedTasks { get; set; }
    }
}
