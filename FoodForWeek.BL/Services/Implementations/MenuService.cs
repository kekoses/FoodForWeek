using FoodForWeek.Library.Services.Interfaces;
using System.Collections.Generic;

namespace FoodForWeek.Library.Services.Implementations
{
    public class MenuService : IMenuService
    {
        public IEnumerable<Dish> GetCurrentMenu()
        {
            List<Dish> dishes = new List<Dish>()
            {
                new Dish {Name="Borsh", Calories=1200d, Ingredients=new []{"Napkin", "Cabbage", "Water"}, PhotoUrl="Resources/images/borsh.jpg"},
                new Dish {Name="French fries", Calories=123d, Ingredients=new []{"Potatoes", "Oil"}, PhotoUrl="Resources/images/french_fries.jpg"},
                new Dish { Name = "Pork steak", Calories = 1900d, Ingredients = new[] { "Pork", "Dill", "Tomatoes" }, PhotoUrl = "Resources/images/steak_pork.jpg" }
            };
            return dishes;
        }
    }
}