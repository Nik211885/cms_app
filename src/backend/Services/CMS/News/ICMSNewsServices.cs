using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Reponse;

namespace backend.Services.CMS.News
{
    public interface ICMSNewsServices
    {
        Task<IReadOnlyCollection<CMSNewsDescriptionReponse>> GetNewsDescriptionSignificantAsync();
        Task<IReadOnlyCollection<CMSNewsDescriptionReponse>> GetNewsDescriptionByMenuIdAsync(int menuId);
        Task<CMSNewsDetailReponse> GetNewsDetailHasActive(int newsId);
    }
}
