using FoodForWeek.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodForWeek.DAL.AppData.EntityBuilders
{
    public class MenuTypeBuilder
    {
        public MenuTypeBuilder(EntityTypeBuilder<Menu> builder)
        {
            builder.HasOne(m => m.User).WithMany(u => u.Menus).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
