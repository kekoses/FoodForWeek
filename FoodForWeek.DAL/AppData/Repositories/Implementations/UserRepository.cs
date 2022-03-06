using FoodForWeekApp.DAL.AppData.Models;
using FoodForWeekApp.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Implementations
{
    public class UserRepository : Repository<User>,  IUserRepository
    {
        public UserRepository(AppContext context):base(context)
        {
        }

        public async Task<bool> ExistUserByEmailAsync(string email) => await _currentSet.AsNoTracking().AnyAsync(u => u.Email == email);
    }
}
