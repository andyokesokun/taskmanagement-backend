using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class AppUser : IdentityUser
    {    
        public bool  IsAdmin { get; set; }
        public ICollection<Task> Tasks { get; set; } 
    }
}
