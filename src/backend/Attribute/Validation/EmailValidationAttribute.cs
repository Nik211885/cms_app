using System.ComponentModel.DataAnnotations;
using UC.Core.Helpers;

namespace uc.api.cms.Attribute.Validation
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        public EmailValidationAttribute(string messageError = "Invalid email address") : base(messageError)
        {
            
        }
        public override bool IsValid(object? value)
        {
            if (value is null) return false;
            if (value.GetType() != typeof(string)) return false;
            return RegularHelpers.IsValidEmail((string)value);
        }
    }
}
