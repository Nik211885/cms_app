using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace uc.api.cms.Attribute.Validation
{
    public class PasswordValidationAttribute : RegularExpressionAttribute
    {
        public PasswordValidationAttribute([StringSyntax("Regex")] 
            string pattern = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            string messageError = "Password is not correct format") 
            : base(pattern)
        {
            ErrorMessage = messageError;
        }
    }
}
