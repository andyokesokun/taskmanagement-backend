using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Core.Dtos
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
