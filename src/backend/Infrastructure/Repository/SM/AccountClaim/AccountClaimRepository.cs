using backend.Core.Entities.SM;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.SM.AccountClaim
{
    public class AccountClaimRepository : Repository<int, sm_account_claims>, IAccountClaimRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public AccountClaimRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<IEnumerable<sm_account_claims>> GetAccountClaimByUserIdAsync(int userId)
        {
            var sql = "SELECT * FROM sm_account_claims WHERE account_id = @userId";
            var result = await _unitOfWork.Repository.QueryListAsync<sm_account_claims>(sql, new {userId});
            return result;
        }
    }
}
