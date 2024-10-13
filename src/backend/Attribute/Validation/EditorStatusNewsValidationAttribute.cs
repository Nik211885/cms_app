using backend.Core.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace uc.api.cms.Attribute.Validation
{
    public class EditorStatusNewsValidationAttribute : ValidationAttribute
    {
        public EditorStatusNewsValidationAttribute(string messageError = "You can't switch this status") : base(messageError)
        {
            
        }
        public override bool IsValid(object? value)
        {
            if (value is null) return false;
            if (value.GetType() != typeof(Status)) return false;
            if((Status)value > Status.Send)
            {
                return false;
            }
            return true;
            
        }
    }
}
