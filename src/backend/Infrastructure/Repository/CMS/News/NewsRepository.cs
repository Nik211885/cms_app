using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.SM.Reponse;
using backend.Infrastructure.Data.DbContext.master;
using Dapper;
using System.Text;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.CMS.News
{
    public class NewsRepository : Repository<int, cms_news>, INewsRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public NewsRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) 
            : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }
        public async Task<IEnumerable<cms_news>> GetAllNewsAsync(Status status, OSearch? search, int? userId = null, bool active = false, bool isStatus = false)
        {
            // chua support search
            var sql = new StringBuilder().Append(@"WITH template AS(SELECT * FROM (
			                                         SELECT *, ROW_NUMBER() OVER(PARTITION BY news_id ORDER BY create_at DESC) AS rn
			                                       FROM cms_news_status ) AS a WHERE rn = 1)");
            sql.Append(@" SELECT cms_news.* FROM template JOIN cms_news ON cms_news.id = template.news_id WHERE");
            string ope = isStatus ? "=" : "!=";
            sql.AppendLine($@" template.status {ope} {(int)(status)}");
            //var sql = new StringBuilder().Append("SELECT * FROM cms_news WHERE active = @active");
            if (userId is not null)
            {
                sql.Append(@" AND cms_news.create_by = @createBy");
            }
            if(active)
            {
                sql.AppendLine(@" AND cms_news.active = true");
            }
            sql.Append(" ORDER BY cms_news.create_at DESC");
            Console.WriteLine(sql.ToString());
            var news = await _unitOfWork.Repository.QueryListAsync<cms_news>(sql.ToString(), new {createBy = userId});
            return news;
        }

        public async Task<IEnumerable<cms_news>> GetAllNewsFollowMenuIdAsync(int menuId, bool active = true)
        {
            var sql = @"SELECT * FROM cms_news 
                        JOIN cms_menu_news ON cms_news.id = cms_menu_news.news_id
                        WHERE cms_menu_news.menu_id = @menuId AND cms_news.active = @active";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_news>(sql, new {menuId, active}, default!);
            return result;
        }

        public async Task<IEnumerable<cms_news>> GetAllNewsSignificantAsync(bool active = true)
        {
            var sql = "SELECT * FROM cms_news WHERE significant AND active = @active";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_news>(sql, new {active});
            return result;
        }

        public async Task<PagedResponse> GetNewsWithPaginationAsync(OSearch? search, int? userId = null, bool active = true, bool status = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StatisticalStatusNewsReponse>> GetStatisticalNewsAsync(DateTime startDay, DateTime endDay, List<Field>? field)
        {
            var sql = new StringBuilder();
            // select count 
            sql.Append(@"SELECT cms_news_status.status, COUNT(*) AS ""count_status"" FROM cms_news");
            // join 
            sql.Append(@" JOIN cms_news_status ON cms_news.id = cms_news_status.news_id");
            sql.Append(@" WHERE cms_news_status.create_at >= @startDay AND cms_news_status.create_at <= @endDay");
            if(field is not null && field.Count != 0)
            {
                foreach(var f in field)
                {
                    sql.Append($" {f.code} {f.value}");
                }
            }
            sql.Append(@" GROUP BY cms_news_status.status");
            var statisticalNews = await _unitOfWork.Repository.QueryListAsync<StatisticalStatusNewsReponse>(sql.ToString(), new {startDay, endDay});
            return statisticalNews;
        }
    }
}
