namespace TaskManagement.Core.Entities
{
    public class AssignedTask : Dtos.AssignedTask
    {
      
        public virtual AppUser AppUser { get; set; } 
        public virtual Task Task { get; set; }
      
       
    }
}
