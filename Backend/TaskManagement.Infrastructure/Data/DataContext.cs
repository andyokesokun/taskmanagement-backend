using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Entities;
using TaskManagement.Infrastructure.Data.Seeds;

namespace TaskManagement.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {

        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<Entities.TaskStatus> TaskStatuses { get; set; }


       

    }
}
