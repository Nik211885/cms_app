using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;

namespace backend.Services.SM.News
{
    public interface ISMNewsNormal
    {
        Task<int> CreateNewsNormalAsync(int userId, CreateNewsNormalRequest request, IFormFile avatar);
        Task<int> UpdateNewsNormalAsync(int userId, int newsId, UpdateNewsNormalRequest request, IFormFile avatar);
        Task<NewsNormalReponse> GetNewsNormalByIdAsync(int userId, int newsId);
    }
}
