using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Dtos
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [MaxLength(2000)]
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public int TaskStatusId { get; set; }

    }
}
