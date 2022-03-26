using Xunit;
using Moq;
using System.Threading.Tasks;
using AutoMapper;
using FoodForWeek.DAL.Identity.Models;
using FoodForWeek.Library.Models.Mapper.DTO;
using System;
using FoodForWeek.DAL.AppData.Models;
using FoodForWeek.Library.Services.Interfaces;
using FoodForWeek.Library.Services.Implementations;
using FluentAssertions;
using FoodForWeek.Tests.ServicesTests.FakeModels;
using Microsoft.AspNetCore.Identity;

namespace FoofForWeek.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly FakeUserManager _fakeUserManager;
        private readonly FakeSignInManager _fakeSignManager;
        private readonly FakeUserRepository _fakeUserRepository;
        public UserServiceTests()
        {
            _fakeUserManager=new FakeUserManager();
            _fakeSignManager=new FakeSignInManager();
            _fakeUserRepository=new FakeUserRepository();
            _mapperMock=new Mock<IMapper>();
            SetupMapperMock();
        }
        [Fact]
        public async Task TestCheckAuthForExistingUser()
        {
            IUserService service = new UserService(_fakeUserRepository,
                                                       _mapperMock.Object,
                                                       _fakeSignManager,
                                                       _fakeUserManager);
            var expectedResult = SignInResult.Success;
            var loginDTO=new LoginUserDTO(){Email="firstemail@mail.com", Password="qwesdasdas"};
            var actualResult=await service.CheckAuthForLoggingUserAsync(loginDTO);
            expectedResult.Should().Be(actualResult);
        }
        private void SetupMapperMock()
        {
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,AppUser>(It.IsNotNull<RegisterUserDTO>()))
                       .Returns(new AppUser()
                        {
                           Email = "firstemail@mail.com",
                           NormalizedEmail = "firstemail@mail.com",
                           Login = "firstemail@mail.com"
                       });
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,User>(It.IsNotNull<RegisterUserDTO>()))
                       .Returns(new User()
                       {
                            Email= "firstemail@mail.com"
                       });
            _mapperMock.Setup(u => u.Map<LoginUserDTO, AppUser>(It.IsNotNull<LoginUserDTO>()))
                      .Returns(new AppUser()
                      {
                          Email = "firstemail@mail.com",
                          NormalizedEmail = "firstemail@mail.com",
                          Login = "firstemail@mail.com"
                      });
            _mapperMock.Setup(u => u.Map<LoginUserDTO, User>(It.IsNotNull<LoginUserDTO>()))
                      .Returns(new User()
                      {
                          Email = "firstemail@mail.com"
                      });
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,AppUser>(It.Is<RegisterUserDTO>(dto=>dto==null)))
                        .Throws<InvalidCastException>();
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,User>(It.Is<RegisterUserDTO>(dto=>dto==null)))
                        .Throws<InvalidCastException>();
        }












        


    }
}