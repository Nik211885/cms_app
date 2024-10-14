using backend.DTOs.CMS.Request;

namespace backend.Services.CMS.News
{
    public interface INewService
    {
        Task<int> CreateNewsServicesAsync(int userId, CreateNewsServicesRequest request);
        Task<int> UpdateNewsServicesAsync(int userId, int newsId ,UpdateNewsServicesRequest request);
    }
}
