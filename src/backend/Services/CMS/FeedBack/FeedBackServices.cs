using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Repository;
using UC.Core.Models.FormData;

namespace backend.Services.CMS.FeedBack
{
    public class FeedBackServices : IFeedBackServices
    {
        private readonly IRepositoryWrapper _repository;
        public FeedBackServices(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task CreateFeedBackASync(CreateFeedBackRequest request)
        {
            var feedBack = new cms_feedbacks();
            ObjectHelpers.Mapping(request, feedBack);
            await _repository.FeedBackRepository.AddAsync(feedBack);
        }

        public async Task<dynamic> SearchFeedBackAsync(OSearch search, bool isPagination = false, int currentPage = 1, int pageSize = 20)
        {
            var result = await _repository.FeedBackRepository.SearchFeedBackAsync(search, isPagination, currentPage,pageSize);
            return result;
        }
    }
}
