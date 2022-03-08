using FoodForWeek.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodForWeek.DAL.AppData.EntityBuilders
{
    public class IngredientEntityBuilder
    {
        public IngredientEntityBuilder(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasOne(i => i.Dish).WithMany(d => d.Ingredients).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
