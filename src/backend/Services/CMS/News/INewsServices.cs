using backend.DTOs.CMS.Request;

namespace backend.Services.CMS.News
{
    public interface INewsServices
    {
        Task<int> CreateNewsNormalAsync(int userId, CreateNewsNormalRequest request);
        Task<int> CreateNewsServicesAsync(int userId, CreateNewsServicesRequest request);
    }
}
