using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Dtos;
using TaskManagement.Interfaces;


namespace TaskManagement.Infrastructure.Data.Seeds
{
    public static class UserSeed
    {

        public static async System.Threading.Tasks.Task Create(IUserService userService)
        {

     

            var adminUser = new UserModel
            {

                UserName = "admin@test.com",
                IsAdmin = true

            };


            var user = new UserModel
            {

                UserName = "joe@test.com",
                IsAdmin = false

            };

             // creates only if users dont exist
             await userService.CreateUser(adminUser, "Admin@123");
             await userService.CreateUser(user, "Joe@123");



        }

 
    }
}
