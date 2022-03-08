using FluentAssertions;
using FoodForWeek.ViewComponents;
using FoodForWeek.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Xunit;

namespace FoodForWeek.Tests
{
    public class LoginViewComponentTests
    {
        [Fact]
        public void TestPassingLoginVM()
        {
            var loginVM = new LoginViewModel();
            var loginViewComponent = new LoginViewComponent();
            var result=loginViewComponent.Invoke(loginVM);
            result.Should().NotBeNull().And
                            .BeOfType<ViewViewComponentResult>().And
                            .Subject.As<ViewViewComponentResult>().ViewData.Model.Should().BeEquivalentTo(loginVM);
        }
    }
}
