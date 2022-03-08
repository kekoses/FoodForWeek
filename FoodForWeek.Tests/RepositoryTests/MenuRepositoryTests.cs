using FluentAssertions;
using FoodForWeek.Tests.Tools.DataTools;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FoodForWeek.Tests.RepositoryTests
{
    public class MenuRepositoryTests : BaseRepositoryTests<Menu>
    {
        public MenuRepositoryTests() : base()
        {
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMenu(int menuId)
        {
            IMenuRepository menuRepository = new MenuRepository(_mryContext);
            var menu = await menuRepository.Get(menuId);
            menu.Should().NotBeNull().And.Match<Menu>(m => m.Id == menuId);
        }
        [Fact]
        public async Task TestGetThrowException()
        {
            IMenuRepository menuRepository = new MenuRepository(_mryContext);
            int menuId = -1234;
            Func<Task<Menu>> method = () => menuRepository.Get(menuId);
            await method.Should().ThrowAsync<InvalidOperationException>().WithMessage("* doesn`t exist!");
        }
        [Fact]
        public async Task TestCreatingMenu()
        {
            var createdMenu = new Menu() { InitialDate=DateTime.Now, ExpiredTimeStep=TimeSpan.FromDays(3), ExpiredDate= DateTime.Now.AddDays(3), UserId=2 };
            IMenuRepository repo = new MenuRepository(_mryContext);
            var expectedMenu = await repo.Create(createdMenu);
            expectedMenu.Should().NotBeNull().And.Match<Menu>(u => u.Id == 5);
            _mrySet.Should().HaveCount(5);
            EntityEntry<Menu> createdEntry = _mryContext.Entry(createdMenu);
            createdEntry.State.Should().Be(EntityState.Unchanged);
        }
        [Fact]
        public async Task TestThrowArgumentNullException()
        {
            Menu Menu = null;
            IMenuRepository repo = new MenuRepository(_mryContext);
            Func<Task> createMethod = () => repo.Create(Menu);
            await createMethod.Should().ThrowAsync<ArgumentNullException>().WithParameterName("newEntity").WithMessage("Menu instance cannot be null!*");
        }
        [Fact]
        public async Task TestGetList()
        {
            IMenuRepository repo = new MenuRepository(_mryContext);
            var result = await repo.GetList();
            result.Should().HaveCount(4);
            foreach (var Menu in result)
            {
                EntityEntry<Menu> state = _mryContext.Entry(Menu);
                state.State.Should().Be(EntityState.Detached);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        public async Task TestRemoveMethod(int MenuId)
        {
            IMenuRepository repo = new MenuRepository(_mryContext);
            var result = await repo.Remove(MenuId);
            if (MenuId == int.MinValue)
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
        public async Task TestUpdateMethod(int MenuId, Menu editedMenu)
        {
            IMenuRepository repo = new MenuRepository(_mryContext);
            if (MenuId == int.MaxValue)
            {
                Func<Task> method = () => repo.Update(MenuId, editedMenu);
                await method.Should().ThrowAsync<InvalidOperationException>();
            }
            else if (editedMenu is null)
            {
                Func<Task> method = () => repo.Update(MenuId, editedMenu);
                await method.Should().ThrowAsync<ArgumentNullException>();
            }
            else
            {
                var updatedMenu = await repo.Update(MenuId, editedMenu);
                editedMenu.Id = MenuId;
                updatedMenu.Should().Be(editedMenu);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMethodForSpecifiedUserId(int userId)
        {
            IMenuRepository repo = new MenuRepository(_mryContext);
            var menu = await repo.GetMenuForUser(userId);
            menu.Should().NotBeNull().And.Match<Menu>(m => m.User!=null && m.User.Id == userId);
        }
        [Fact]
        public async Task TestGetMethodForSpecifiedUserIdThrowException()
        {
            int userId = -2134;
            IMenuRepository repo = new MenuRepository(_mryContext);
            Func<Task<Menu>> method = () => repo.GetMenuForUser(userId);
            await method.Should().ThrowAsync<InvalidOperationException>().WithMessage("* doesn`t exist!");
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMenuListMethodForSpecifiedUserId(int userId)
        {
            IMenuRepository repo = new MenuRepository(_mryContext);
            IEnumerable<Menu> list = await repo.GetMenuListExceptSimillarForUser(userId);
            list.Should().HaveCount(count => count > 0).And.NotContain(m => m.UserId == userId);
        }
        [Fact]
        public async Task TestGetMenuListMethodForSpecifiedUserIdThrowExcption()
        {
            int userId = -124;
            IMenuRepository repo = new MenuRepository(_mryContext);
            Func<Task> method = ()=> repo.GetMenuListExceptSimillarForUser(userId);
            await method.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested user:-124 doesn`t exist!");
            AbortConnection();
        }
        public static IEnumerable<object[]> GetDataForUpdateTest()
        {
            yield return new object[] { 1, new Menu() { InitialDate = DateTime.Now, ExpiredTimeStep = TimeSpan.FromDays(5), ExpiredDate = DateTime.Now.AddDays(3), UserId = 1 } };
            yield return new object[] { 2, new Menu() { InitialDate = DateTime.Now, ExpiredTimeStep = TimeSpan.FromDays(1), ExpiredDate = DateTime.Now.AddDays(3), UserId = 3 } };
            yield return new object[] { 1, null };
            yield return new object[] { int.MaxValue, new Menu() { InitialDate = DateTime.Now, ExpiredTimeStep = TimeSpan.FromDays(9), ExpiredDate = DateTime.Now.AddDays(3), UserId = 4 } };
        }
    }
}
