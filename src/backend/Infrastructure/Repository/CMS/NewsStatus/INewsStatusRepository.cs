using backend.Core.Entities.CMS;
using System.Data;

namespace backend.Infrastructure.Repository.CMS.NewsStatus
{
    public interface INewsStatusRepository
    {
        Task<cms_news_status> AddAsync(cms_news_status status, IDbTransaction dbTransaction);
    }
}
