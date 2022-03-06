using FoodForWeekApp.DAL.AppData.Models;
using FoodForWeekApp.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Implementations
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AppContext context) : base(context)
        {
        }
        public async Task<Menu> GetMenuForUser(int userId)
        {
            Menu menu = await _currentSet.AsNoTracking().Where(m => m.UserId == userId).Include(m => m.User).AsNoTracking().SingleOrDefaultAsync();
            if(menu is not null)
            {
                return menu;
            }
            throw new InvalidOperationException($"Requested menu for user {userId} doesn`t exist!");
        }

        public async Task<IEnumerable<Menu>> GetMenuListExceptSimillarForUser(int userId)
        {
            if (_currentSet.Any(m=>m.UserId != userId))
            {
                throw new InvalidOperationException($"Requested user:{userId} doesn`t exist!");
            }
            return await _currentSet.Where(m => m.UserId != userId).AsNoTracking().ToListAsync();
        }
    }
}
