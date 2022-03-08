
using FoodForWeek.DAL.AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeek.Tests.Tools.DataTools
{
    public static class DMLHelper
    {
        public static void SeedData(this DbContext context)
        {
            var users = new List<User> { new User { FirstName = "Maxim", LastName = "Denisov", Email = "qwerty1", UserName="qwerty09" },
                                        new User { FirstName = "Slava", LastName = "Denisov", Email = "qwerty2", UserName="qwerty08"},
                                        new User { FirstName = "Vasya", LastName = "Denisov",Email = "qwerty3", UserName="qwerty07" },
                                        new User { FirstName = "Egor", LastName = "Denisov", Email = "qwerty4", UserName="qwerty06" },
            };
            var menus = new List<Menu>()
            {
                new Menu(){ ExpiredTimeStep=TimeSpan.FromDays(3), InitialDate= DateTime.Now , UserId=1,ExpiredDate=DateTime.Now.AddDays(3) },
                new Menu(){ ExpiredTimeStep=TimeSpan.FromDays(3), InitialDate= DateTime.Now , UserId=2 ,ExpiredDate=DateTime.Now.AddDays(3)},
                new Menu(){ ExpiredTimeStep=TimeSpan.FromDays(3), InitialDate= DateTime.Now , UserId=3,ExpiredDate=DateTime.Now.AddDays(3) },
                new Menu(){ ExpiredTimeStep=TimeSpan.FromDays(3), InitialDate= DateTime.Now , UserId=4 ,ExpiredDate=DateTime.Now.AddDays(3)},
            };
            var dishes = new List<Dish>()
            {
                new Dish(){ Title="Pork steak", Calories=1000d, MenuId=1, Photo=new byte[2] { 1,2 } },
                new Dish(){ Title="Borsh", Calories=100d, MenuId=1, Photo=new byte[2] { 1,2 } },
                new Dish(){ Title="French fries", Calories=180d, MenuId=1, Photo=new byte[2] { 1,2 } },
                new Dish(){ Title="Milkshake", Calories=10d, MenuId=1, Photo=new byte[2] { 1,2 } }
            };
            var ingredients = new List<Ingredient>()
            {
                new Ingredient(){ Title="Cabbage", DishId=2, Weight=1},
                new Ingredient(){ Title="Pork", DishId=1, Weight=0.5},
                new Ingredient(){ Title="Parsley", DishId=2, Weight=0.5},
                new Ingredient(){ Title="Potato", DishId=3, Weight=1},
                new Ingredient(){ Title="Milk", DishId=4, Weight=1},
                new Ingredient(){ Title="Banana", DishId=4, Weight=1}
            };
            DbSet<User> userSet = context.Set<User>();

            DbSet<Menu> menuSet = context.Set<Menu>();
            DbSet<Dish> dishSet = context.Set<Dish>();
            DbSet<Ingredient> ingredientSet = context.Set<Ingredient>();
             userSet.AddRange(users);
             context.SaveChanges();
             menuSet.AddRange(menus);
             context.SaveChanges();
             dishSet.AddRange(dishes);
             context.SaveChanges();
             ingredientSet.AddRange(ingredients);
             context.SaveChanges();
            return;
        }
    }
}
