using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using UC.Core.Models.FormData;

namespace backend.Services.CMS.FeedBack
{
    public interface IFeedBackServices
    {
        Task CreateFeedBackASync(CreateFeedBackRequest request);
        Task<dynamic> SearchFeedBackAsync(OSearch search, bool isPagination = false, int currentPage = 1, int pageSize = 20);
    }
}
