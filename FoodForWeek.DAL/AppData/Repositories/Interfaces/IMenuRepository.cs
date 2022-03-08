using FoodForWeek.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IEnumerable<Menu>> GetMenuListExceptSimillarForUser(int userId);
        Task<Menu> GetMenuForUser(int userId);
    }
}
