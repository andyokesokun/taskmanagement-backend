using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Dtos
{
    public class AssignedTask
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int TaskId { get; set; }
    }
}
