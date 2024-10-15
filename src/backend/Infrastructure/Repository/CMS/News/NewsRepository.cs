using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
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

        public async Task<PagedResponse> GetNewsWithPaginationAsync(OSearch? search, int? userId = null, bool active = true, bool status = false)
        {
            throw new NotImplementedException();
        }
    }
}
