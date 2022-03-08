using FoodForWeek.DAL.AppData.Models;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> ExistUserByEmailAsync(string email);
    }
}
