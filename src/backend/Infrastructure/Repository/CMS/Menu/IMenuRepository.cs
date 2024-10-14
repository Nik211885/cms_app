using backend.Core.Entities.CMS;
using System.Data.Common;

namespace backend.Infrastructure.Repository.CMS.Menu
{
    public interface IMenuRepository : IRepositoryBase<int, cms_menu>
    {
        Task<IReadOnlyCollection<cms_menu>> GetMenuAsync(string? type, bool getChild);
        Task<IEnumerable<cms_menu>> GetAllMenuByNewsIdAsync(int newsId, DbTransaction transaction);
    }
}
