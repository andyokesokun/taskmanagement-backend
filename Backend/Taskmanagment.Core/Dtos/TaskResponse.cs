using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Dtos
{
    public class TaskResponse : Task
    {
        public  ICollection<UserResponse> UserResponses { get; set; }
    }
}
