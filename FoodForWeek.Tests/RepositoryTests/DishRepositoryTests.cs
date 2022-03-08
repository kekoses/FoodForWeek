using FluentAssertions;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodForWeek.Tests.RepositoryTests
{
    public class DishRepositoryTests : BaseRepositoryTests<Dish>
    {
        public DishRepositoryTests() : base()
        {                
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMethod(int DishId)
        {
            IDishRepository DishRepository = new DishRepository(_mryContext);
            var result = await DishRepository.Get(DishId);
            result.Should().NotBeNull().And.Match<Dish>(u => u.Id == DishId);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task TestGetThrowExceptions(int DishId)
        {
            IDishRepository DishRepository = new DishRepository(_mryContext);
            Func<Task<Dish>> getMethod = () => DishRepository.Get(DishId);
            await getMethod.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested Dish doesn`t exist!");
        }
        [Fact]
        public async Task TestCreatingDish()
        {
            var createdDish = new Dish() { Title="Okroshka", Calories=300d, MenuId=1, Photo=new byte[] { 1, 2 } };
            IDishRepository repo = new DishRepository(_mryContext);
            var expectedDish = await repo.Create(createdDish);
            expectedDish.Should().NotBeNull().And.Match<Dish>(u => u.Id == 5);
            _mrySet.Should().HaveCount(5);
            EntityEntry<Dish> createdEntry = _mryContext.Entry(createdDish);
            createdEntry.State.Should().Be(EntityState.Unchanged);
        }
        [Fact]
        public async Task TestThrowArgumentNullException()
        {
            Dish Dish = null;
            IDishRepository repo = new DishRepository(_mryContext);
            Func<Task> createMethod = () => repo.Create(Dish);
            await createMethod.Should().ThrowAsync<ArgumentNullException>().WithParameterName("newEntity").WithMessage("Dish instance cannot be null!*");
        }
        [Fact]
        public async Task TestGetList()
        {
            IDishRepository repo = new DishRepository(_mryContext);
            var result = await repo.GetList();
            result.Should().HaveCount(4);
            foreach (var Dish in result)
            {
                EntityEntry<Dish> state = _mryContext.Entry(Dish);
                state.State.Should().Be(EntityState.Detached);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        public async Task TestRemoveMethod(int DishId)
        {
            IDishRepository repo = new DishRepository(_mryContext);
            var result = await repo.Remove(DishId);
            if (DishId == int.MinValue)
            {
                result.Should().Be(false);
            }
            else
            {
                result.Should().Be(true);
            }
        }
        [Theory]
        [MemberData(nameof(GetDataForUpdateTest))]
        public async Task TestUpdateMethod(int DishId, Dish editedDish)
        {
            IDishRepository repo = new DishRepository(_mryContext);
            if (DishId == int.MaxValue)
            {
                Func<Task> method = () => repo.Update(DishId, editedDish);
                await method.Should().ThrowAsync<InvalidOperationException>();
            }
            else if (editedDish is null)
            {
                Func<Task> method = () => repo.Update(DishId, editedDish);
                await method.Should().ThrowAsync<ArgumentNullException>();
            }
            else
            {
                var updatedDish = await repo.Update(DishId, editedDish);
                editedDish.Id = DishId;
                updatedDish.Should().Be(editedDish);
            }
        }
        [Theory]
        [InlineData(1)]
        public async Task TestGetDishListForSpecifiedMenu(int menuId)
        {
            IDishRepository repo = new DishRepository(_mryContext);
            IEnumerable<Dish> list = await repo.GetDishListForMenu(menuId);
            list.Should().HaveCount(count => count > 0).And.Match<IEnumerable<Dish>>(list=>list.All(d=>d.MenuId==menuId));
        }
        [Fact]
        public async Task TestGetDishListForSpecifiedMenuThrowException()
        {
            int menuId = -124;
            IDishRepository repo = new DishRepository(_mryContext);
            Func<Task> method= () => repo.GetDishListForMenu(menuId);
            await method.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested dish list for menu:-124 doesn`t exist!");
            AbortConnection();
        }
        public static IEnumerable<object[]> GetDataForUpdateTest()
        {
            yield return new object[] { 1, new Dish() { Title = "Pelmeni", Calories = 600d, MenuId = 1, Photo = new byte[] { 1, 2 } } };
            yield return new object[] { 2, new Dish() {Title = "Vareniki", Calories = 350d, MenuId = 2, Photo = new byte[] { 1, 2 } } };
            yield return new object[] { 1, null };
            yield return new object[] { int.MaxValue, new Dish() { Title = "Shawerma", Calories = 200d, MenuId = 3, Photo = new byte[] { 1, 2 } } };
        }
    }
}
