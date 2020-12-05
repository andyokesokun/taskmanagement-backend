using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Data.Seeds
{
    public class DatabaseSeed
    {
        private readonly IUserService _userService;
        private readonly ITaskRepository _taskService;
        private readonly ITaskStatusRepository _taskStatusService;

        public DatabaseSeed(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
            _taskService = serviceProvider.GetRequiredService<ITaskRepository>();
            _taskStatusService = serviceProvider.GetRequiredService<ITaskStatusRepository>();
        }


        public async Task Run() {
           await UserSeed.Create(_userService);
           await TaskStatusSeed.Create(_taskStatusService);
           await TaskSeed.Create(_taskService);


        }
    }
}
