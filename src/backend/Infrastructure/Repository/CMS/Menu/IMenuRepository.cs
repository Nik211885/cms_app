using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.CMS.Menu
{
    public interface IMenuRepository : IRepositoryBase<int,cms_menu>
    {
    }
}
