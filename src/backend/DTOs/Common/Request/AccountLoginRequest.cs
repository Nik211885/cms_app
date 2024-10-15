using backend.Attribute.Validation;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Common.Request
{
    public record AccountLoginRequest(string user_name, string password);
    public static class AccountLoginRequestValidation
    {
        public static void Validation(AccountLoginRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.user_name))
            {
                throw new ArgumentException("User name is not null");
            }
            if(!RegularHelpersExtension.IsPassword(request.password))
            {
                throw new ArgumentException("Password is not correct format");
            }
        }
    } 
}
