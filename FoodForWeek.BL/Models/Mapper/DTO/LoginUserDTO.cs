namespace FoodForWeek.Library.Models.Mapper.DTO
{
    public class LoginUserDTO : BaseUserDTO
    {
        public bool RememberMe { get; set; } = false;
    }
}
