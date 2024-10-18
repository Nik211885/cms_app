using backend.Core.Entities.SM;
using backend.DTOs.SM.Request;

namespace backend.Services.SM.Role
{
    public interface IRoleServices
    {
        Task<int> CreateNewRoleAsync(CreateRoleRequest request);
        Task DeleteRoleAsync(int roleId);
        Task<int> UpdateRoleAsync(int roleId, UpdateRoleRequest request);
        Task<IReadOnlyCollection<sm_roles>> GetAllRolesAsync();
    }
}
