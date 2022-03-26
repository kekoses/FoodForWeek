using System;
using System.Threading.Tasks;
using AutoMapper;
using FoodForWeek.Library.Models.Mapper.DTO;
using FoodForWeek.Library.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using FoodForWeek.DAL.AppData.Repositories.Interfaces;
using FoodForWeek.DAL.Identity.Models;
using FoodForWeek.Library.AdditionalHelpers.Extensions;
using FoodForWeek.DAL.AppData.Models;

namespace FoodForWeek.Library.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
                           IMapper mapper,
                           SignInManager<AppUser> signManager,
                           UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _signManager = signManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> AuthorizeNewUserAsync(AppUser newUser, string password)
        {
            newUser.CheckNull("Authorized user instance cannot be null!");
            password.CheckNull("Password cannot be null!");
            return await _signManager.PasswordSignInAsync(newUser, password, newUser.RememberMe, false);
        }

        public async Task<SignInResult> CheckAuthForLoggingUserAsync(LoginUserDTO loggingUser)
        {
            loggingUser.CheckNull("Logging user instance cannot be null!");
            AppUser identityUser = await _userManager.FindByEmailAsync(loggingUser.Email);
            if (identityUser != null)
            {
                SignInResult result = await _signManager.PasswordSignInAsync(identityUser, loggingUser.Password, loggingUser.RememberMe, false);
                return result;
            }
            return SignInResult.Failed;
        }

        public async Task<AppUser> AuthenticateNewUserAsync(RegisterUserDTO newUser)
        {
            await _userRepository.ExistUserByEmailAsync(newUser.Email);
            newUser.CheckNull("New user failed authentication process. New user is null instance");
            AppUser mappedIdentityUser = _mapper.Map<RegisterUserDTO, AppUser>(newUser);
            IdentityResult creatingResult = await _userManager.CreateAsync(mappedIdentityUser, newUser.Password);
            if (creatingResult.Succeeded && !await _userRepository.ExistUserByEmailAsync(newUser.Email))
            {
                User entityUser = _mapper.Map<RegisterUserDTO, User>(newUser);
                User addedUser = await _userRepository.Create(entityUser);
                if (addedUser != null)
                {
                    return await _userManager.FindByEmailAsync(newUser.Email);
                }
            }
            throw new InvalidOperationException($"Authentication process for user: {newUser.Email} failed!");
        }

        public async Task<SignInResult> ProcessNewUserAsync(RegisterUserDTO newUser)
        {
            AppUser createdUser = await AuthenticateNewUserAsync(newUser);
            return await AuthorizeNewUserAsync(createdUser, newUser.Password);
        }

        public async Task Logout()
        {
            await _signManager.SignOutAsync();
        }
    }
}
