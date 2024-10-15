using backend.Core.Entities.SM;

namespace backend.Infrastructure.Repository.SM.Account
{
    public interface IAccountRepository : IRepositoryBase<int, sm_accounts>
    {
        Task<sm_accounts> GetAccountByUserNameAsync(string userName);
    }
}
