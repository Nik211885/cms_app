using backend.Attribute.Validation;

namespace backend.DTOs.SM.Request
{
    public record ChangePasswordRequest(string OldPassword, string NewPassword, string ConfirmPassword);
    public static class ChangePasswordRequestValidation
    {
        public static  void Validation(ChangePasswordRequest request)
        {
            if (!(RegularHelpersExtension.IsPassword(request.OldPassword) 
                && RegularHelpersExtension.IsPassword(request.NewPassword)))
            {
                throw new ArgumentException("Your password is not correct format");
            }
            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                throw new ArgumentException("New password and confirm password not duplicate");
            }
        }
    }
}
