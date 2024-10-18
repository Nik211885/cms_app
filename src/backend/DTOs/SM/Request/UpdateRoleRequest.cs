using backend.Core.Exceptions;

namespace backend.DTOs.SM.Request
{
    public record UpdateRoleRequest(string name);
    public static class UpdateRoleRequestValidation
    {
        public static void Validation(CreateRoleRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.name))
            {
                throw new ValidationException("Name role is not null");
            }
        }
    }
}
