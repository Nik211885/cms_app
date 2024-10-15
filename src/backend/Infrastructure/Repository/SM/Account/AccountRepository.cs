using backend.Core.Entities.SM;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.SM.Account
{
    public class AccountRepository : Repository<int, sm_accounts>, IAccountRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public AccountRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<sm_accounts> GetAccountByUserNameAsync(string userName)
        {
            var sql = "SELECT * FROM sm_accounts WHERE user_name = @userName";
            var result = await _unitOfWork.Repository.QueryFirstAsync<sm_accounts>(sql, new { userName }, default!);
            return result;
        }
    }
}
