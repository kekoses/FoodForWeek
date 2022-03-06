using FoodForWeekApp.DAL.AppData.Models;
using FoodForWeekApp.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Implementations
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(AppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Dish>> GetDishListForMenu(int menuId)
        {
            if(_currentSet.Any(d=>d.MenuId!= menuId))
            {
                throw new InvalidOperationException($"Requested dish list for menu:{menuId} doesn`t exist!");
            }
            return await _currentSet.AsNoTracking().Where(d => d.MenuId == menuId).AsNoTracking().ToListAsync();
        }
    }
}
