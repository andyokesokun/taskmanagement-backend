using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<AssignedTask> AppUserTasks { get; set; }

    }
}
