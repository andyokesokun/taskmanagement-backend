using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagement.Core.Interfaces
{
    public interface IUserService
    {
 
        Task<bool> Authenticate(string username, string password);
        Task <List<Claim>> GetUserClaims(String username);
     
        Task<IEnumerable<Entities.AppUser> > GetAllUsers();
        Task<Entities.AppUser> FindByUserName(string username);
        Task SignIn(string username);
        Task CreateUser(Dtos.UserModel userModel, String Password);
        string GenerateToken();
        Task<bool> UserExist(String userName);
        bool isAdmin();
    }
}
