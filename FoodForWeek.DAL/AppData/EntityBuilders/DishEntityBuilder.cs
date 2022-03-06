using FoodForWeekApp.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodForWeekApp.DAL.AppData.EntityBuilders
{
    public class DishEntityBuilder
    {
        public DishEntityBuilder(EntityTypeBuilder<Dish> builder)
        {
            builder.HasOne(d => d.Menu).WithMany(m => m.Dishes).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
