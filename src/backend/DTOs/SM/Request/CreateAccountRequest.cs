using backend.Attribute.Validation;
using backend.Core.ValueObject;

namespace backend.DTOs.SM.Request
{
    public record CreateAccountRequest(string user_name,
        string full_name, string phone_number, string email,
        string password, Gender gender, string address, string? avatar, IEnumerable<int>? roleId) 
        : UpdateProfileAccountRequest(full_name,phone_number,email,gender,address,avatar, roleId);
    public static class CreateAccountValidation
    {
        //
        public static void Validation(CreateAccountRequest request)
        {
            UpdateProfileAccountRequestValidation.Validation(request);
            if (String.IsNullOrWhiteSpace(request.user_name))
            {
                throw new ArgumentException("User name is not null");
            }
            if (!RegularHelpersExtension.IsPassword(request.password))
            {
                throw new ArgumentException("Password should has length bigger 8 character and contain number, upper case , lower case");
            }
        }
    }
}
