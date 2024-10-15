using backend.Core.Entities.SM;

namespace backend.Infrastructure.Repository.SM.AccountClaim
{
    public interface IAccountClaimRepository : IRepositoryBase<int, sm_account_claims>
    {
        Task<IEnumerable<sm_account_claims>> GetAccountClaimByUserIdAsync(int userId); 
    }
}
