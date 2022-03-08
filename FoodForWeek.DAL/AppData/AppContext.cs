using FoodForWeek.DAL.AppData.EntityBuilders;
using FoodForWeek.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodForWeek.DAL
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityBuilder(modelBuilder.Entity<User>());
            new MenuTypeBuilder(modelBuilder.Entity<Menu>());
            new DishEntityBuilder(modelBuilder.Entity<Dish>());
            new IngredientEntityBuilder(modelBuilder.Entity<Ingredient>());
        }
    }
}
