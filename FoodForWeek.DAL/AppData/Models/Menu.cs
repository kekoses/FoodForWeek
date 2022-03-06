using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodForWeekApp.DAL.AppData.Models
{
    public class Menu : BaseType
    {
        [Required]
        public DateTime InitialDate { get; set; }
        [Required]
        public TimeSpan ExpiredTimeStep { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Dish> Dishes { get; set; }
        public override bool Equals(object obj)
        {
            var secondObj = obj as Menu;
            if (secondObj == null && this == null) return true;
            if (secondObj != null && this != null) return true;
            if ((secondObj != null && this == null) || (this != null && secondObj == null)) return false;
            return GetHashCode() == secondObj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(InitialDate, ExpiredTimeStep, ExpiredDate, UserId);
        }
    }
}
