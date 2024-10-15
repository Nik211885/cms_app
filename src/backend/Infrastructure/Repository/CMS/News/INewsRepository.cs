using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.CMS.News
{
    public interface INewsRepository : IRepositoryBase<int, cms_news>
    {

        Task<IEnumerable<cms_news>> GetAllNewsAsync
            (Status status, OSearch? search, int? userId = null, bool active = false, bool isStatus = false);
        Task<PagedResponse> GetNewsWithPaginationAsync
            (OSearch? search, int? userId = null, bool active = true, bool status = false);
    }
}
