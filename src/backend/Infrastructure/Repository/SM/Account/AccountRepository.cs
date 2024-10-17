using backend.Core.Entities.CMS;
using backend.Core.Entities.SM;
using backend.Infrastructure.Data.DbContext.master;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Linq;
using System.Text;
using UC.Core.Models.FormData;

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

        public async Task<IEnumerable<sm_accounts>> SearchUserWithPaginationAsync(Func<int, PagedRequest> Func, List<Field> search = null)
        {
            var result = new Dictionary<int, IEnumerable<sm_accounts>>();
            var sql = new StringBuilder().Append(@"SELECT {0} FROM sm_accounts");
            if(search is not null && search.Count > 0)
            {
                var countSearch = search.Count;
                if(countSearch > 0)
                {
                    sql.Append(" WHERE");
                }
                for(int i = 0; i< countSearch -  1; i++)
                {
                    var searchFirst = search.ElementAt(i);
                    sql.Append($" {searchFirst.code} ILIKE '{searchFirst.value}%'");
                    sql.Append(" OR");
                }
                var searchLast = search.ElementAt(countSearch - 1);
                sql.Append($" {searchLast.code} ILIKE '{searchLast.value}%'");
            }
            var countItem = await _unitOfWork.Repository.QueryFirstAsync<int>(String.Format(sql.ToString(), "COUNT(*)"), default!);
            var pageRequest = Func(countItem);
            sql.Append(" ORDER BY create_at DESC");
            sql.Append(" LIMIT @size OFFSET @offset");
            var items = await _unitOfWork.Repository
                .QueryListAsync<sm_accounts>(String.Format(sql.ToString(), "*"), new {size = pageRequest.PageSize, offset = pageRequest.SkipRows}, default!);
            return items;
        }
    }
}
