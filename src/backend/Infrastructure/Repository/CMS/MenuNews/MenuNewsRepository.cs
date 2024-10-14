using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.MenuNews
{
    public class MenuNewsRepository : IMenuNewsRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public MenuNewsRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<cms_menu_news> AddAsync(cms_menu_news menuNews, DbTransaction dbTransaction)
        {
            var type = typeof(cms_menu_news);
            var sql = SqlQueryBuilder.BuildInsertQuery(type, out object param, menuNews, false);
            var result = await _unitOfWork.Repository.ExecuteAsync(sql, param, dbTransaction);
            return menuNews;
        }

        public async Task DeleteAllByNewsIdAsync(int newsId, DbTransaction transaction)
        {
            var sql = "DELETE FROM cms_menu_news WHERE news_id =  @newsId";
            await _unitOfWork.Repository.ExecuteAsync(sql, new { newsId }, transaction);
        }

        public async Task<IEnumerable<cms_menu_news>> GetAllByNewsIdAsync(int newsId, DbTransaction transaction)
        {
            var sql = "SELECT * FROM cms_menu_news WHERE news_id = @newsId";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_menu_news>(sql, new { newsId }, transaction);
            return result;
        }
    }
}
