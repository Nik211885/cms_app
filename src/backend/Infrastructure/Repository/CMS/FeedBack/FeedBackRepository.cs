using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Data.DbContext.master;
using System.Text;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.CMS.FeedBack
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public FeedBackRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task AddAsync(cms_feedbacks feedBack)
        {
            var sql = SqlQueryBuilder.BuildInsertQuery<cms_feedbacks>(typeof(cms_feedbacks),out object param,feedBack,false);
            await _unitOfWork.Repository.ExecuteAsync(sql,param);
        }

        public async Task<dynamic> SearchFeedBackAsync(OSearch search, bool isPagination = false, int currentPage = 1, int pageSize = 20)
        {
            var sql = new StringBuilder().Append("SELECT {0} FROM cms_feedbacks");
            if(search is not null && search.fields is not null && search.fields.Count != 0)
            {
                sql.Append(" WHERE");
                int filedCount = search.fields.Count;
                for(int i = 0;i<filedCount-1;i++)
                {
                    var filed = search.fields[i];
                    sql.Append($" {filed.code} ILIKE '{filed.value}%'");
                    sql.Append(" OR");
                }
                sql.Append($" {search.fields[filedCount-1].code} ILIKE '{search.fields[filedCount - 1].value}%'");
            }
            if (!isPagination)
            {
                sql.Append(@" ORDER BY create_at DESC");
                var result =  await _unitOfWork.Repository.QueryListAsync<cms_feedbacks>(String.Format(sql.ToString(),"*"), default!);
                return result;
            }
            var countItem = await _unitOfWork.Repository.QueryFirstAsync<int>(String.Format(sql.ToString(), "COUNT(*)"), default!);
            sql.Append(@" ORDER BY create_at DESC");
            var pageRequest = new PagedRequest(currentPage, pageSize, countItem);
            sql.Append(" LIMIT @pageSize OFFSET @offset");
            var items = await _unitOfWork.Repository.
                QueryListAsync<cms_feedbacks>(String.Format(sql.ToString(), "*"), 
                new {pageSize = pageRequest.PageSize, offset = pageRequest.SkipRows} ,default!);
            return new PagedResponse(pageRequest.CurrentPage, pageRequest.TotalPages, items);
        }
    }
}
