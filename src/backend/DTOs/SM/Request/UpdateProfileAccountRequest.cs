using backend.Core.ValueObject;

namespace backend.DTOs.SM.Request
{
    public record UpdateProfileAccountRequest(string full_name, string phone_number, string email,
        Gender gender, string address, string? avatar, IEnumerable<int>? roleId);
    public static class UpdateProfileAccountRequestValidation
    {
        public static void Validation(UpdateProfileAccountRequest request)
        {
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
            if (String.IsNullOrWhiteSpace(request.address))
            {
                throw new ArgumentException("Address is not null");
            }
        }
    }
}
