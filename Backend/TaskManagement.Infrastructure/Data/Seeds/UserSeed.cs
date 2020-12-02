using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Constants;
using TaskManagement.Core.Dtos;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Data.Seeds
{
    public static class UserSeed
    {

        public static async System.Threading.Tasks.Task CreateUsers(IUserService userService, IConfiguration configuration)
        {

     

            var adminUser = new UserModel
            {

                UserName = "Admin@test.com",
                IsAdmin = true

            };


            var user = new UserModel
            {

                UserName = "Joe@test.com",
                IsAdmin = true

            };

             await userService.CreateUser(adminUser, "Admin@123");
             await userService.CreateUser(user, "joe@123");



        }

 
    }
}
