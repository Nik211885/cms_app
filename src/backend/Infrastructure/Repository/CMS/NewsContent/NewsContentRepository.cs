using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.NewsContent
{
    public class NewsContentRepository : Repository<int, cms_news_content>, INewsContentRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public NewsContentRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<IEnumerable<cms_news_content>> GetAllNewsContentByNewsId(int newsId, DbTransaction transaction)
        {
            var sql = "SELECT * FROM cms_news_content WHERE news_id = @newsId";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_news_content>(sql, new { newsId }, transaction);
            return result;
        }
    }
}
