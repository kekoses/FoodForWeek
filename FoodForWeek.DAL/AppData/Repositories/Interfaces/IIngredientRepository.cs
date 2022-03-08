using FoodForWeek.DAL.AppData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsForDish(int dishId);
    }
}
