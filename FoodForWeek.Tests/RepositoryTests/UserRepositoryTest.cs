using Xunit;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FoodForWeek.Tests.Tools.DataTools;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;

namespace FoodForWeek.Tests.RepositoryTests
{
    public class UserRepositoryTest : BaseRepositoryTests<User>
    {
        public UserRepositoryTest() : base()
        {
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task TestGetMethod(int userId)
        {
            IUserRepository userRepository = new UserRepository(_mryContext);
            var result = await userRepository.Get(userId);
            result.Should().NotBeNull().And.Match<User>(u => u.Id == userId);
        }


        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task TestGetThrowExceptions(int userId)
        {
            IUserRepository userRepository = new UserRepository(_mryContext);
            Func<Task<User>> getMethod = () => userRepository.Get(userId);
            await getMethod.Should().ThrowAsync<InvalidOperationException>().WithMessage("Requested User doesn`t exist!");
        }
        [Fact]
        public async Task TestCreatingUser()
        {
            var createdUser = new User() { FirstName = "Afanasiy", LastName = "Denisov", Email = "qwerty", UserName="qwerty00" };
            IUserRepository repo = new UserRepository(_mryContext);
            var expectedUser = await repo.Create(createdUser);
            expectedUser.Should().NotBeNull().And.Match<User>(u => u.Id == 5);
            _mrySet.Should().HaveCount(5);
            EntityEntry<User> createdEntry = _mryContext.Entry(createdUser);
            createdEntry.State.Should().Be(EntityState.Unchanged);
        }
        [Fact]
        public async Task TestThrowArgumentNullException()
        {
            User user = null;
            IUserRepository repo = new UserRepository(_mryContext);
            Func<Task> createMethod = () => repo.Create(user);
            await createMethod.Should().ThrowAsync<ArgumentNullException>().WithParameterName("newEntity").WithMessage("User instance cannot be null!*");
        }
        [Fact]
        public async Task TestGetList()
        {
            IUserRepository repo = new UserRepository(_mryContext);
            var result = await repo.GetList();
            result.Should().HaveCount(4);
            foreach (var user in result)
            {
                EntityEntry<User> state = _mryContext.Entry(user);
                state.State.Should().Be(EntityState.Detached);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        public async Task TestRemoveMethod(int userId)
        {
            IUserRepository repo = new UserRepository(_mryContext);
            var result = await repo.Remove(userId);
            if (userId == int.MinValue)
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
        public async Task TestUpdateMethod(int userId, User editedUser)
        {
            IUserRepository repo = new UserRepository(_mryContext);
            if (userId == int.MaxValue)
            {
                Func<Task> method = () => repo.Update(userId, editedUser);
                await method.Should().ThrowAsync<InvalidOperationException>();
            }
            else if(editedUser is null)
            {
                Func<Task> method = () => repo.Update(userId, editedUser);
                await method.Should().ThrowAsync<ArgumentNullException>();
            }
            else
            {
                var updatedUser = await repo.Update(userId, editedUser);
                editedUser.Id = userId;
                updatedUser.Should().Be(editedUser);
            }
            AbortConnection();
        }
        public static IEnumerable<object[]> GetDataForUpdateTest()
        {
            yield return new object[] { 1, new User() { FirstName = "Kit", LastName = "Kotov", Email = "qwerty123", UserName = "qwerty" } };
            yield return new object[] { 2, new User() { FirstName = "Karina", LastName = "Denisova", Email = "qwerty1233", UserName = "qwerty2" } };
            yield return new object[] { 1,null };
            yield return new object[] { int.MaxValue, new User() { FirstName = "Karina", LastName = "Denisova", Email = "qwerty3456", UserName = "qwerty23" } };
        }
        
    }
}
