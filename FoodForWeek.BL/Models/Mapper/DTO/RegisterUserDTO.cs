using System.ComponentModel.DataAnnotations;

namespace FoodForWeek.Library.Models.Mapper.DTO
{
    public class RegisterUserDTO : BaseUserDTO
    {
        [Compare(nameof(Password), ErrorMessage = "Passwords should be match!")]
        [Required(ErrorMessage = "Please, confirm your password!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name should be fill!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name should be fill!")]
        public string LastName { get; set; }
        public bool RememberMe { get; set; } = false;
        [Required]
        public string UserName { get; set; }
    }
}
