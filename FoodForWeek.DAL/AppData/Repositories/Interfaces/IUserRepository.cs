using FoodForWeekApp.DAL.AppData.Models;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> ExistUserByEmailAsync(string email);
    }
}
