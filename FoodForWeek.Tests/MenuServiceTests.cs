using Xunit;
using FluentAssertions;
using FoodForWeek.Library.Services.Interfaces;
using System.Collections.Generic;
using FoodForWeek.ViewComponents;
using FoodForWeek.Library.Services.Implementations;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace FoodForWeek.Tests
{
    public class MenuServiceTests
    {
        private readonly IMenuService _menuService;
        public MenuServiceTests()
        {
            _menuService = new MenuService();
        }
        [Fact]
        public void TestMenuServiceGetCorrectCurrentMenu()
        {
            var receivedDishes = _menuService.GetCurrentMenu();
            IEnumerable<Dish> expectedDishes = new List<Dish>()
            {
                new Dish {Name="Borsh", Calories=1200d, Ingredients=new []{"Napkin", "Cabbage", "Water"}, PhotoUrl="Resources/images/borsh.jpg"},
                new Dish {Name="French fries", Calories=123d, Ingredients=new []{"Potatoes", "Oil"}, PhotoUrl="Resources/images/french_fries.jpg"},
                new Dish { Name = "Pork steak", Calories = 1900d, Ingredients = new[] { "Pork", "Dill", "Tomatoes" }, PhotoUrl = "Resources/images/steak_pork.jpg" }
            };
            receivedDishes.Should().BeEquivalentTo(expectedDishes);
        }
        [Fact]
        public void TestMenuViewComponentWithMenuService()
        {
            CurrentMenuViewComponent menuViewComponent = new CurrentMenuViewComponent(_menuService);
            var result=menuViewComponent.Invoke();
            result.Should().BeOfType<ViewViewComponentResult>().And
                   .As<ViewViewComponentResult>().ViewData.Model.Should().BeOfType<List<Dish>>().Which.Should().HaveCount(3);
        }
    }
}
