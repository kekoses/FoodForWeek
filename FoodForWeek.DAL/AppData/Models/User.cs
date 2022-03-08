using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodForWeek.DAL.AppData.Models
{
    public class User : BaseType
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public override bool Equals(object obj)
        {
            var secondObj = obj as User;
            if (secondObj == null && this == null) return true;
            if (secondObj != null && this != null) return true;
            if ((secondObj != null && this == null) || (this != null && secondObj == null)) return false;
            return GetHashCode() == secondObj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, UserName);
        }
    }
}
