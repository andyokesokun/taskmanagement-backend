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
        public string name { get; set; }
        [MaxLength(2000)]
        public string description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
