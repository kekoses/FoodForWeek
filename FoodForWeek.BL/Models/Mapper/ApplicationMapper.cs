using AutoMapper;
using FoodForWeek.Library.Models.Mapper.DTO;
using FoodForWeekApp.DAL.AppData.Models;
using FoodForWeekApp.DAL.Identity.Models;

namespace FoodForWeek.Library.Models.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<RegisterUserDTO, AppUser>().ForMember(d => d.Email, m => m.MapFrom(s => s.Email))
                                                  .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName))
                                                  .ForMember(d => d.RememberMe, m => m.MapFrom(s => s.RememberMe))
                                                  .ForMember(d => d.FirstName, m => m.MapFrom(s => s.FirstName))
                                                  .ForMember(d => d.LastName, m => m.MapFrom(s => s.LastName))
                                                  .ForMember(d => d.Login, m => m.MapFrom(s => s.UserName));
            CreateMap<RegisterUserDTO, User>().ForMember(d => d.FirstName, m => m.MapFrom(s => s.FirstName))
                                               .ForMember(d => d.LastName, m => m.MapFrom(s => s.LastName))
                                               .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName))
                                               .ForMember(d => d.Email, m => m.MapFrom(s => s.Email));
            CreateMap<LoginUserDTO, AppUser>().ForMember(d => d.RememberMe, m => m.MapFrom(s => s.RememberMe));
            CreateMap<LoginUserDTO, User>().ForMember(d => d.Email, m => m.MapFrom(s => s.Email));

        }
    }
}
