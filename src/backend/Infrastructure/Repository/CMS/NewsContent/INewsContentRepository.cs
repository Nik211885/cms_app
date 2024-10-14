using backend.Core.Entities.CMS;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.NewsContent
{
    public interface INewsContentRepository : IRepositoryBase<int, cms_news_content>
    {
        Task<IEnumerable<cms_news_content>> GetAllNewsContentByNewsId(int newsId, DbTransaction transaction);
    }
}
