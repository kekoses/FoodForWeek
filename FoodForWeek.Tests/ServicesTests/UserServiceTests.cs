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
using FoodForWeek.DAL.AppData.Repositories.Implementations;

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
        public async Task TestAuthenticateNewUserAsync()
        {
             IUserService service=new Mock<UserService>((UserRepository)_fakeUserRepository,
                                                        _mapperMock.Object,
                                                        (UserManager<AppUser>)_fakeUserManager,
                                                        (SignInManager<AppUser>)_fakeSignManager).Object;
            var expectedUser=new AppUser() {Email="firstemail@mail.com",NormalizedEmail="firstemail@mail.com",Login="firstemail@mail.com"};
            var registedDTO=new RegisterUserDTO(){Email="firstemail@mail.com", Password="qwesdasdas"};
            var actualUSer=await service.AuthenticateNewUserAsync(registedDTO);
            expectedUser.Should().Be(actualUSer);
        }
        private void SetupMapperMock()
        {
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,AppUser>(It.IsNotNull<RegisterUserDTO>()))
                       .Returns(new AppUser()
                        {
                            Email="EXISTEDEMAIL@.RU",
                            Login="kekoses",
                            NormalizedEmail="EXISTEDEMAIL@.RU"
                        });
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,User>(It.IsNotNull<RegisterUserDTO>()))
                       .Returns(new User()
                       {
                            Email="EXISTEDEMAIL@.RU"
                       });
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,AppUser>(It.Is<RegisterUserDTO>(dto=>dto==null)))
                        .Throws<InvalidCastException>();
            _mapperMock.Setup(u=>u.Map<RegisterUserDTO,User>(It.Is<RegisterUserDTO>(dto=>dto==null)))
                        .Throws<InvalidCastException>();
        }












        


    }
}