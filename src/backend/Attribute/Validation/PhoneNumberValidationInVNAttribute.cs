using System.ComponentModel.DataAnnotations;

namespace uc.api.cms.Attribute.Validation
{
    public class PhoneNumberValidationInVNAttribute : ValidationAttribute
    {
        public PhoneNumberValidationInVNAttribute(string messageError = "Invalid phone number") : base(messageError)
        {

        }
        public override bool IsValid(object? value)
        {
            if (value is null) return false;
            if (value.GetType() != typeof(string)) return false;
            return RegularHelpers.IsValidPhoneVN((string)value);
        }
    }
}
