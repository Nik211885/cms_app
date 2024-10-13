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

        public async Task<cms_news_status> AddAsync(cms_news_status status, IDbTransaction transaction)
        {
            var type = typeof(cms_news_status);
            var sql = SqlQueryBuilder.BuildInsertQuery<cms_news_status>(type,out object param,status,false);
            int result = await _unitOfWork.Repository.ExecuteAsync(sql, param, transaction);
            return status;
        }
    }
}
