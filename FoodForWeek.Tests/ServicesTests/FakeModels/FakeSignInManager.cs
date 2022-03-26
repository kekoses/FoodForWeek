using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodForWeek.DAL.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FoodForWeek.Tests.ServicesTests.FakeModels
{
    public class FakeSignInManager : SignInManager<AppUser>
    {
        private readonly List<AppUser> _fakeUserList;

        public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object,
                  new HttpContextAccessor(),
                  new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<AppUser>>>().Object,
                  new Mock<IAuthenticationSchemeProvider>().Object,
                  new Mock<IUserConfirmation<AppUser>>().Object)
        { 
             _fakeUserList=new List<AppUser>()
            {
                new AppUser() {Email="firstemail@mail.com",NormalizedEmail="firstemail@mail.com",Login="firstemail@mail.com" },
                new AppUser() {Email="secondemail@mail.com",NormalizedEmail="secondemail@mail.com",Login="secondemail@mail.com" },
                new AppUser() {Email="thirdemail@mail.com",NormalizedEmail="thirdemail@mail.com",Login="thirdemail@mail.com" }  
            };
        }

        public override Task<SignInResult> PasswordSignInAsync(AppUser user, string password,bool isPersistent, bool lockoutOnFailure)
        {
            if(user is null || string.IsNullOrEmpty(password))
            {
                return Task.FromResult(SignInResult.Failed);
            }
            if(_fakeUserList.Contains(user))
            {
                return Task.FromResult(SignInResult.Success);
            }
            return Task.FromResult(SignInResult.Failed);
        }
    }
}

