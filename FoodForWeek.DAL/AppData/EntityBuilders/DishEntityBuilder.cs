using FoodForWeek.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodForWeek.DAL.AppData.EntityBuilders
{
    public class DishEntityBuilder
    {
        public DishEntityBuilder(EntityTypeBuilder<Dish> builder)
        {
            builder.HasOne(d => d.Menu).WithMany(m => m.Dishes).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
