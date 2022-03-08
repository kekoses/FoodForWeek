using FoodForWeek.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Interfaces
{
    public interface IDishRepository : IRepository<Dish>
    {
        public Task<IEnumerable<Dish>> GetDishListForMenu(int menuId);
    }
}
