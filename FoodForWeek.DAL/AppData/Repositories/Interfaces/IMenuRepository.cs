using FoodForWeekApp.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IEnumerable<Menu>> GetMenuListExceptSimillarForUser(int userId);
        Task<Menu> GetMenuForUser(int userId);
    }
}
