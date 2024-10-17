using backend.Core.Entities.SM;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.SM.Account
{
    public interface IAccountRepository : IRepositoryBase<int, sm_accounts>
    {
        Task<IEnumerable<sm_accounts>> SearchUserWithPaginationAsync(Func<int, PagedRequest> Func, List<Field> search = null);
        Task<sm_accounts> GetAccountByUserNameAsync(string userName);
    }
}
