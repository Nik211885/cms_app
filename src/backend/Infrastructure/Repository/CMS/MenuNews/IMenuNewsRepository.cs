using backend.Core.Entities.CMS;
using System.Data;

namespace backend.Infrastructure.Repository.CMS.MenuNews
{
    public interface IMenuNewsRepository
    {
        Task<cms_menu_news> AddAsync(cms_menu_news menuNews, IDbTransaction dbTransaction);
    }
}
