using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dtos;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly ILogger<UserService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private  AppUser CurrentLoginAppUser;

        public UserService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<UserService> logger)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _signManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();
        }

        public UserManager<AppUser> UserManager => _userManager;

        public async Task<bool> Authenticate(string username, string password)
        {

           var result =await _signManager.PasswordSignInAsync(username, password,false, false);
       

            if (result.Succeeded)
            {
                CurrentLoginAppUser = await FindByUserName(username);
                return true;
            }

            return false;
       

        }

        public String GenerateToken(){


            if(CurrentLoginAppUser == null){
                return String.Empty;
            }


            var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, CurrentLoginAppUser.UserName )
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
              _config["JWT:Issuer"], 
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async  System.Threading.Tasks.Task CreateUser(UserModel userModel, string password)
        {
            var user = new AppUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                IsAdmin = userModel.IsAdmin

            };


            if (await UserExist(user.UserName))
            {
                return;
            }


          
            await _userManager.CreateAsync(user,password);

        }

        public async Task<AppUser> FindByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            
            return user;
        }

        public Task<IEnumerable<AppUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<List<Claim>> GetUserClaims(string username)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task SignIn(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExist(string username)
        {

            var user = await FindByUserName(username);
            return user != null ? true : false;
        }

        public  bool isAdmin()
        {
            if (CurrentLoginAppUser !=null) {
                return CurrentLoginAppUser.IsAdmin;
            }

            return false;
        }
    }
}
