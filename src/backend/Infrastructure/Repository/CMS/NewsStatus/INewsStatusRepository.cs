using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using System.Data;

namespace backend.Infrastructure.Repository.CMS.NewsStatus
{
    public interface INewsStatusRepository
    {
        Task<cms_news_status> AddAsync(cms_news_status status, IDbTransaction dbTransaction);
        Task<cms_news_status> GetNewStatusByNewsAsync(int newsId);
        Task<IEnumerable<cms_news_status>> GetAllStatusByNewsAsync(int newsId);
        Task<IEnumerable<cms_news_status>> GetAllStatusByNewsSpecific(int newsId, Status status);
    }
}
