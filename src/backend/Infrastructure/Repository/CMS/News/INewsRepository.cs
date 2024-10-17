using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.SM.Reponse;
using UC.Core.Models.FormData;

namespace backend.Infrastructure.Repository.CMS.News
{
    public interface INewsRepository : IRepositoryBase<int, cms_news>
    {
        Task<IEnumerable<StatisticalStatusNewsReponse>> GetStatisticalNewsAsync(DateTime startDay, DateTime endDay, List<Field>? field);
        Task<IEnumerable<cms_news>> GetAllNewsFollowMenuIdAsync(int menuId, bool active = true);
        Task<IEnumerable<cms_news>> GetAllNewsSignificantAsync(bool active = true);
        Task<IEnumerable<cms_news>> GetAllNewsAsync
            (Status status, OSearch? search, int? userId = null, bool active = false, bool isStatus = false);
        Task<PagedResponse> GetNewsWithPaginationAsync
            (OSearch? search, int? userId = null, bool active = true, bool status = false);
    }
}
