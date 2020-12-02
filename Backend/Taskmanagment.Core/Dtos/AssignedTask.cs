using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Dtos
{
    public class AssignedTask
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int TaskId { get; set; }
    }
}
