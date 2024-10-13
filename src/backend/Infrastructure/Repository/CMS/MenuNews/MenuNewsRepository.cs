using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;
using System.Data;

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

        public async Task<cms_menu_news> AddAsync(cms_menu_news menuNews, IDbTransaction dbTransaction)
        {
            var type = typeof(cms_menu_news);
            var sql = SqlQueryBuilder.BuildInsertQuery(type, out object param, menuNews, false);
            var result = await _unitOfWork.Repository.ExecuteAsync(sql, param, dbTransaction);
            return menuNews;
        }
    }
}
