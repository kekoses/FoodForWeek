using FoodForWeekApp.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId);
    }
}
