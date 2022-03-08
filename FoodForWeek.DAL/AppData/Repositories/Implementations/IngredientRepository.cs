using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Implementations
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId)
        {
            if (_currentSet.All(d => d.DishId != dishId))
            {
                throw new InvalidOperationException($"Requested ingredient list for dish:{dishId} doesn`t exist!");
            }
            return await _currentSet.Where(i => i.DishId == dishId).AsNoTracking().ToListAsync();
        }
    }
}
