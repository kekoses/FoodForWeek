using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForWeekApp.DAL.AppData.Models
{
    public class Ingredient : BaseType
    {
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public double Weight { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public override bool Equals(object obj)
        {
            var secondObj = obj as Ingredient;
            if (secondObj == null && this == null) return true;
            if (secondObj != null && this != null) return true;
            if ((secondObj != null && this == null) || (this != null && secondObj == null)) return false;
            return GetHashCode() == secondObj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Title,Weight,DishId);
        }
    }
}
