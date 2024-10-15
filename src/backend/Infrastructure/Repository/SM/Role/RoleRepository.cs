using backend.Core.Entities.SM;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.SM.Role
{
    public class RoleRepository : Repository<int, sm_roles>, IRoleRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public RoleRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<IEnumerable<sm_role_claims>> GetRoleClaimsByRoleIdAsync(int roleId)
        {
            var sql = @"SELECT * FROM sm_role_claims WHERE role_id = @roleId";
            var result = await _unitOfWork.Repository.QueryListAsync<sm_role_claims>(sql, new { roleId }, default!);
            return result;
        }

        public async Task<IEnumerable<sm_roles>> GetRolesByUserIdAsync(int userId)
        {
            var sql = @"SELECT sm_roles.* FROM sm_account_roles
                        JOIN sm_roles ON sm_roles.id = sm_account_roles.role_id
                        WHERE sm_account_roles.account_id = @userId";
            var result = await _unitOfWork.Repository.QueryListAsync<sm_roles>(sql, new { userId }, default!);
            return result;
        }
    }
}
