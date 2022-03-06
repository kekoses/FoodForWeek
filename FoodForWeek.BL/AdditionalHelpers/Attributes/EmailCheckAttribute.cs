using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FoodForWeek.Library.AdditionalHelpers.Attributes
{
    public class EmailCheckAttribute : ValidationAttribute, IClientModelValidator
    {
       private const string _matchEmailPattern =
                      @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
               + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
               + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
               + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        public EmailCheckAttribute()
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-email", "Invalid email. Please, write the correct email.");
        }
        public override bool IsValid(object value)
        {
            var email = value as string;
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            return Regex.Match(email, _matchEmailPattern).Success;
        }
    }
}
