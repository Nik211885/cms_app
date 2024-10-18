using backend.Core.Exceptions;

namespace backend.DTOs.SM.Request
{
    public record CreateRoleRequest(string name) : UpdateRoleRequest(name);
    public static class CreateRoleRequestValidation 
    {
        public static void Validation(CreateRoleRequest request)
        {
            UpdateRoleRequestValidation.Validation(request);
        }
    }
}
