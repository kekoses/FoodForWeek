using FoodForWeek.Library.Models.Mapper.DTO;
using FoodForWeek.DAL.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FoodForWeek.Library.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser> AuthenticateNewUserAsync(RegisterUserDTO newUser);
        public Task<SignInResult> CheckAuthForLoggingUserAsync(LoginUserDTO loggingUser);
        public Task<SignInResult> AuthorizeNewUserAsync(AppUser newUser, string password);
        public Task<SignInResult> ProcessNewUserAsync(RegisterUserDTO newUser);
        public Task Logout();
    }
}
