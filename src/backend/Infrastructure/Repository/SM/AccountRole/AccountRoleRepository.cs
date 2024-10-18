using backend.Core.Entities.SM;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.SM.AccountRole
{
    public class AccountRoleRepository : Repository<int, sm_account_roles>, IAccountRoleRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public AccountRoleRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<sm_account_roles> GetAccountRoleSpecificUserAndRoleAsync(int userId, int roleId)
        {
            var sql = $"SELECT * FROM {nameof(sm_account_roles)} WHERE {nameof(sm_account_roles.account_id)} = @userId AND {nameof(sm_account_roles.role_id)} = @roleId";
            var result = await _unitOfWork.Repository.QueryFirstAsync<sm_account_roles>(sql, new {userId, roleId }, default!);
            return result;
        }
    }
}
