using backend.Core.Entities.SM;

namespace backend.Infrastructure.Repository.SM.Role
{
    public interface IRoleRepository : IRepositoryBase<int, sm_roles>
    {
        Task<IReadOnlyCollection<sm_roles>> GetAllRoleAsync();
        Task<IEnumerable<sm_roles>> GetRolesByUserIdAsync(int userId);
        Task<IEnumerable<sm_role_claims>> GetRoleClaimsByRoleIdAsync(int roleId);
    }
}
