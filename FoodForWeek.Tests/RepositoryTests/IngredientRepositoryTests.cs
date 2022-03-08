using FluentAssertions;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodForWeek.Tests.RepositoryTests
{
    public class IngredientRepositoryTests : BaseRepositoryTests<Ingredient>
    {
        public IngredientRepositoryTests() : base()
        {
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMethod(int IngredientId)
        {
            IIngredientRepository IngredientRepository = new IngredientRepository(_mryContext);
            var result = await IngredientRepository.Get(IngredientId);
            result.Should().NotBeNull().And.Match<Ingredient>(u => u.Id == IngredientId);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task TestGetThrowExceptions(int IngredientId)
        {
            IIngredientRepository IngredientRepository = new IngredientRepository(_mryContext);
            Func<Task<Ingredient>> getMethod = () => IngredientRepository.Get(IngredientId);
            await getMethod.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested Ingredient doesn`t exist!");
        }
        [Fact]
        public async Task TestCreatingIngredient()
        {
            var createdIngredient = new Ingredient() { Title="Carrot", Weight=0.3, DishId=1 };
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            var expectedIngredient = await repo.Create(createdIngredient);
            expectedIngredient.Should().NotBeNull().And.Match<Ingredient>(u => u.Id == 7);
            _mrySet.Should().HaveCount(7);
            EntityEntry<Ingredient> createdEntry = _mryContext.Entry(createdIngredient);
            createdEntry.State.Should().Be(EntityState.Unchanged);
        }
        [Fact]
        public async Task TestThrowArgumentNullException()
        {
            Ingredient Ingredient = null;
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            Func<Task> createMethod = () => repo.Create(Ingredient);
            await createMethod.Should().ThrowAsync<ArgumentNullException>().WithParameterName("newEntity").WithMessage("Ingredient instance cannot be null!*");
        }
        [Fact]
        public async Task TestGetList()
        {
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            var result = await repo.GetList();
            result.Should().HaveCount(6);
            foreach (var Ingredient in result)
            {
                EntityEntry<Ingredient> state = _mryContext.Entry(Ingredient);
                state.State.Should().Be(EntityState.Detached);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        public async Task TestRemoveMethod(int IngredientId)
        {
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            var result = await repo.Remove(IngredientId);
            if (IngredientId == int.MinValue)
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
        public async Task TestUpdateMethod(int IngredientId, Ingredient editedIngredient)
        {
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            if (IngredientId == int.MaxValue)
            {
                Func<Task> method = () => repo.Update(IngredientId, editedIngredient);
                await method.Should().ThrowAsync<InvalidOperationException>();
            }
            else if (editedIngredient is null)
            {
                Func<Task> method = () => repo.Update(IngredientId, editedIngredient);
                await method.Should().ThrowAsync<ArgumentNullException>();
            }
            else
            {
                var updatedIngredient = await repo.Update(IngredientId, editedIngredient);
                editedIngredient.Id = IngredientId;
                updatedIngredient.Should().Be(editedIngredient);
            }
        }
        [Theory]
        [InlineData(1)]
        public async Task TestGetIngredientListForSpecifiedDish(int dishId)
        {
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            IEnumerable<Ingredient> list = await repo.GetIngredientsForDish(dishId);
            list.Should().HaveCount(count => count > 0).And.Match<IEnumerable<Ingredient>>(list => list.All(d => d.DishId == dishId));
        }
        [Fact]
        public async Task TestGetDishListForSpecifiedMenuThrowException()
        {
            int menuId = -124;
            IIngredientRepository repo = new IngredientRepository(_mryContext);
            Func<Task> method = () => repo.GetIngredientsForDish(menuId);
            await method.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested ingredient list for dish:-124 doesn`t exist!");
            AbortConnection();
        }
        public static IEnumerable<object[]> GetDataForUpdateTest()
        {
            yield return new object[] { 1, new Ingredient() { Title = "Dill", Weight = 0.2, DishId = 2 } };
            yield return new object[] { 2, new Ingredient() { Title = "Carrot", Weight = 0.6, DishId = 3 } };
            yield return new object[] { 1, null };
            yield return new object[] { int.MaxValue, new Ingredient() { Title = "Carrot", Weight = 0.3, DishId = 1 } };
        }
    }
}
