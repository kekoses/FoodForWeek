using System.Collections.Generic;

namespace FoodForWeek.Library.Services.Interfaces
{
    public interface IMenuService
    {
        public IEnumerable<Dish> GetCurrentMenu();
    }
}

namespace FoodForWeek.Library.Services.Interfaces
{
    public class Dish
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public string PhotoUrl { get; set; }
    }
}