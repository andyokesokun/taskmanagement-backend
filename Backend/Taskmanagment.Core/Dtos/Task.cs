using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Core.Dtos
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int TaskStatusId { get; set; }

    }
}
