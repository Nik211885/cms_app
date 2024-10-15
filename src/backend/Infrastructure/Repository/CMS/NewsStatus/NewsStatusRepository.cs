using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.Infrastructure.Data.DbContext.master;
using System.Data;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.NewsStatus
{
    public class NewsStatusRepository : INewsStatusRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public NewsStatusRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<cms_news_status> AddAsync(cms_news_status status)
        {
            var type = typeof(cms_news_status);
            var sql = SqlQueryBuilder.BuildInsertQuery<cms_news_status>(type, out object param, status, false);
            int result = await _unitOfWork.Repository.ExecuteAsync(sql, param);
            return status;
        }

        public async Task<IEnumerable<cms_news_status>> GetAllStatusByNewsAsync(int newsId)
        {
            var result = await _unitOfWork.Repository.QueryListAsync<cms_news_status>(String.Format(sql, ""), new { newsId });
            return result;
        }

        public async Task<cms_news_status> GetNewStatusByNewsAsync(int newsId)
        {
            var queryLimit = sql + " LIMIT 1";
            var result = await _unitOfWork.Repository.QueryFirstAsync<cms_news_status>(String.Format(sql, ""),
                new { newsId });
            return result;
        }

        public async Task<IEnumerable<cms_news_status>> GetAllStatusByNewsSpecific(int newsId, Status status)
        {
            var querySpecific = " AND status = @status";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_news_status>(String.Format(sql, querySpecific),
                new { newsId = newsId, status = status });
            return result;
        }

        private static string sql = ("SELECT * FROM cms_news_status WHERE news_id = @newsId{0} ORDER BY create_at DESC");

    }
}
