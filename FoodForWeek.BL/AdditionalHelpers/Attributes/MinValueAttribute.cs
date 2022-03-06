using System.ComponentModel.DataAnnotations;

namespace FoodForWeek.Library.AdditionalHelpers.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        public MinValueAttribute(double minValue)
        {
            MinValue = minValue;
        }
        public MinValueAttribute(double minValue,string errorMessage) : base(errorMessage)
        {
            MinValue = minValue;
        }
        public double MinValue { get; init; }
        public override bool IsValid(object value)
        {
            if ((double)value <= 0) return false;
            return true;
        }
    }
}
