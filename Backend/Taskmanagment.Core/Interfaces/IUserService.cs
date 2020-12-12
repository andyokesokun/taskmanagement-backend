using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagement.Interfaces
{
    public interface IUserService
    {
 
        Task<bool> Authenticate(string username, string password);
        Task <List<Claim>> GetUserClaims(String username);
     
        Task<IEnumerable<Entities.AppUser> > GetAllUsers();

        Task<Entities.AppUser> FindByUserName(string username);
        Task<Entities.AppUser> Find(string userId);
        Task SignIn(string username);
        Task<Entities.AppUser> CreateUser(Dtos.UserModel userModel, String Password);
        string GenerateToken();
        Task<bool> UserExist(String userName);
        bool IsAdmin();

     
    }
}
