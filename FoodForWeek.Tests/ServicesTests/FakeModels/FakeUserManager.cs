using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodForWeek.DAL.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FoodForWeek.Tests.ServicesTests.FakeModels
{
    public class FakeUserManager : UserManager<AppUser>
    {
        private readonly List<AppUser> _fakeUserList;
        public FakeUserManager()
            : base(new Mock<IUserStore<AppUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<AppUser>>().Object,
                  new IUserValidator<AppUser>[0],
                  new IPasswordValidator<AppUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<AppUser>>>().Object)
        {
            _fakeUserList=new List<AppUser>()
            {
                new AppUser() {Email="firstemail@mail.com",NormalizedEmail="firstemail@mail.com",Login="firstemail@mail.com" },
                new AppUser() {Email="secondemail@mail.com",NormalizedEmail="secondemail@mail.com",Login="secondemail@mail.com" },
                new AppUser() {Email="thirdemail@mail.com",NormalizedEmail="thirdemail@mail.com",Login="thirdemail@mail.com" }  
            };
        }

        public override Task<IdentityResult> CreateAsync(AppUser user,string password)
        {
            if(user is null || string.IsNullOrEmpty(password))
            {
                 return Task.FromResult(IdentityResult.Failed());
            }
            if(_fakeUserList.FirstOrDefault(u=>u.Email==user.Email)is not null)
            {
                Task.FromResult(IdentityResult.Failed());
            }
            return Task.FromResult(IdentityResult.Success);
        }
        public override Task<AppUser> FindByEmailAsync(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            return Task.FromResult(_fakeUserList.FirstOrDefault(u=>u.Email==email));
        }
    }
}