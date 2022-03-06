using FoodForWeekApp.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodForWeekApp.DAL.AppData.EntityBuilders
{
    public class IngredientEntityBuilder
    {
        public IngredientEntityBuilder(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasOne(i => i.Dish).WithMany(d => d.Ingredients).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
