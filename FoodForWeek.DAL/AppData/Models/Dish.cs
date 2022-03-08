using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodForWeek.DAL.AppData.Models
{
    public class Dish : BaseType
    {
        [Required]
        [MinLength(2)]
        [MaxLength(70)]
        public string Title { get; set; }
        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public double Calories { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public override bool Equals(object obj)
        {
            var secondObj = obj as Dish;
            if (secondObj == null && this == null) return true;
            if (secondObj != null && this != null) return true;
            if ((secondObj != null && this == null) || (this != null && secondObj == null)) return false;
            return GetHashCode() == secondObj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Photo, Calories, MenuId);
        }

    }
}
