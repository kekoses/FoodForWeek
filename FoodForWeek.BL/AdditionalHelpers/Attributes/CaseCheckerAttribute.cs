using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FoodForWeek.Library.AdditionalHelpers.Attributes
{
    public class CaseCheckerAttribute : ValidationAttribute, IClientModelValidator
    {
        public CaseCheckerAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-case", "Invalid definition of password, you need to use as low as upp case in the password");
        }

        public override bool IsValid(object value)
        {
            var checkedPassword = value as string;
            string subPasswrod = new string(checkedPassword.Where(char.IsLetter).ToArray());
            bool firstCondition = subPasswrod.All(char.IsUpper);
            bool secondCondition = subPasswrod.All(char.IsLower);
            bool result = !(firstCondition || secondCondition);
            return result;
        }
    }
}
