using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;
using backend.Validation;

namespace backend.Services.CMS.News
{
    public class NewsServices : NewUntil, INewsServices
    {
        public NewsServices(IRepositoryWrapper repository, UnitOfWork unitOfWork) 
            :base(repository,unitOfWork) 
        {
           
        }
        public async Task<int> CreateNewsNormalAsync(int userId, CreateNewsNormalRequest request)
        {
            return await CreateNewsAsync(userId, request, (newsId) =>
            {
                return [new cms_news_content(newsId, request.content_html, request.title)];
            }, false);
        }

        public async Task<int> CreateNewsServicesAsync(int userId, CreateNewsServicesRequest request)
        {
            return await CreateNewsAsync(userId, request, (newsId) =>
            {
                return Enumerable.Select(request.news_content, (n) =>
                {
                    return new cms_news_content(newsId, n.content_html, n.title);
                });
            }, true);
        }

        public async Task<int> UpdateNewsNormalAsync(int userId, int newsId, UpdateNewsNormalRequest request)
        {
            var result = await UpdateNewsAsync(userId, newsId, request, false);
            return result;
        }

        public async Task<int> UpdateNewsServicesAsync(int userId, int newsId, UpdateNewsServicesRequest request)
        {
            var result = await UpdateNewsAsync(userId, newsId, request, true);
            return result;
        }
    }
}
