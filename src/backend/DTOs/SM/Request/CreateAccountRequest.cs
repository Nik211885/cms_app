using backend.Attribute.Validation;
using backend.Core.ValueObject;

namespace backend.DTOs.SM.Request
{
    public record CreateAccountRequest(string user_name,
        string full_name, string phone_number, string email,
        string password, Gender gender, string address, string? avatar);
    public static class CreateAccountValidation
    {
        //
        public static void Validation(CreateAccountRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.user_name))
            {
                throw new ArgumentException("User name is not null");
            }
            if (String.IsNullOrWhiteSpace(request.full_name))
            {
                throw new ArgumentException("Full name is not null");
            }
            if (!RegularHelpers.IsValidPhoneVN(request.phone_number))
            {
                throw new ArgumentException("Phone  number is not correct format in viet nam");
            }
            if (!RegularHelpers.IsValidEmail(request.email))
            {
                throw new ArgumentException("Email is not correct format");
            }
            if (!RegularHelpersExtension.IsPassword(request.password))
            {
                throw new ArgumentException("Password should has length bigger 8 character and contain number, upper case , lower case");
            }
            if (String.IsNullOrWhiteSpace(request.address))
            {
                throw new ArgumentException("Address is not null");
            }
        }
    }
}
