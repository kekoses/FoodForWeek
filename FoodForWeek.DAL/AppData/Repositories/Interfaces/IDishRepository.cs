using FoodForWeekApp.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Interfaces
{
    public interface IDishRepository : IRepository<Dish>
    {
        public Task<IEnumerable<Dish>> GetDishListForMenu(int menuId);
    }
}
