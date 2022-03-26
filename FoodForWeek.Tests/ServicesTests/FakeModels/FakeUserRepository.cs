using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.DAL.AppData.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FoodForWeek.Tests.ServicesTests.FakeModels
{
    public class FakeUserRepository : UserRepository
    {
        private readonly List<User> _fakeUserList;
        public FakeUserRepository() : base(new Mock<DAL.AppContext>(new DbContextOptions<DAL.AppContext>()).Object)
        {
            _fakeUserList=new List<User>()
            {
                new User() {Email="firstemail@mail.com"},
                new User() {Email="secondemail@mail.com"},
                new User() {Email="thirdemail@mail.com"}  
            };
        }

        public override Task<bool> ExistUserByEmailAsync(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                return Task.FromResult(_fakeUserList.Any(u=>u.Email==email));
            }
            throw new ArgumentNullException(nameof(email));
        }
    }
}