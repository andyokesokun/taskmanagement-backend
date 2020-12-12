using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Dtos;
using TaskManagement.Entities;
using TaskManagement.Interfaces;

namespace TaskManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        private readonly SignInManager<AppUser> _signManager;
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IServiceProvider _serviceProvider;
        private  AppUser CurrentLoginAppUser;

        public UserService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<UserService> logger)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _signManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();
        }


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


            var claims = new Claim[2];
            claims[0] = new Claim(ClaimTypes.Name, CurrentLoginAppUser.UserName);

            if (CurrentLoginAppUser.IsAdmin) {
                claims[1] = new Claim("Admin","true");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
              _config["JWT:Issuer"], 
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async  System.Threading.Tasks.Task<AppUser> CreateUser(UserModel userModel, string password)
        {
            var user = new AppUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                IsAdmin = userModel.IsAdmin

            };


            if (await UserExist(user.UserName))
            {
                return await FindByUserName(user.UserName);
            }


          
            await _userManager.CreateAsync(user,password);

            return user;

            

        }

        public async Task<AppUser> FindByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            
            return user;
        }


        public async Task<AppUser> Find(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }


        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            var user = await _userManager.Users.Where(s => !s.IsAdmin).ToListAsync();
            return user;
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

        public  bool IsAdmin()
        {
            if (CurrentLoginAppUser !=null) {
                return CurrentLoginAppUser.IsAdmin;
            }

            return false;
        }


     
  

    }

   
    
}
