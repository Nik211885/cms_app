using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using UC.Core.Models.FormData;

namespace backend.Services.CMS.News
{
    public interface INewService
    {
        Task<int> CreateNewsServicesAsync(int userId, CreateNewsServicesRequest request);
        Task<int> UpdateNewsServicesAsync(int userId, int newsId, UpdateNewsServicesRequest request);
        Task<NewsServicesReponse> GetNewsServicesByIdAsync(int userId, int newsId);
    }
}
