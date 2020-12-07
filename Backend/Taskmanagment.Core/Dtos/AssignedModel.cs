using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Dtos
{
    public class AssignedModel
    {
    
        [Required]
        public string userName { get; set; }
        [Required]
        public string TaskId { get; set; }
    }
}
