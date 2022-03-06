using FoodForWeek.Library.AdditionalHelpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FoodForWeek.Library.Models.Mapper.DTO
{
    public class BaseUserDTO
    {
        [Required(ErrorMessage = "Email should be fill!")]
        [EmailCheck]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password should be fill!")]
        [MinLength(8, ErrorMessage = "Password length must be not less than 8 symbols!")]
        [CaseChecker("Password should contain as lower as upper case in definition")]
        public string Password { get; set; }
    }
}
