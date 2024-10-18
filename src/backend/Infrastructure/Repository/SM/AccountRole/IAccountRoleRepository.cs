using backend.Core.Entities.SM;

namespace backend.Infrastructure.Repository.SM.AccountRole
{
    public interface IAccountRoleRepository : IRepositoryBase<int, sm_account_roles>
    {
        Task<sm_account_roles> GetAccountRoleSpecificUserAndRoleAsync(int userId, int roleId);
    }
}
