using backend.Core.Entities.CMS;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.MenuNews
{
    public interface IMenuNewsRepository
    {
        Task<cms_menu_news> AddAsync(cms_menu_news menuNews, DbTransaction dbTransaction);
        Task<IEnumerable<cms_menu_news>> GetAllByNewsIdAsync(int newsId, DbTransaction transaction);
        Task DeleteAllByNewsIdAsync(int newsId, DbTransaction transaction);
    }
}
