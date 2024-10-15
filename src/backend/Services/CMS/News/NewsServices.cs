using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;

namespace backend.Services.CMS.News
{
    public class NewsServices : NewUntil, INewsServices
    {
        private readonly IRepositoryWrapper _repository;
        public NewsServices(IRepositoryWrapper repository, UnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _repository = repository;
        }

        public async Task<int> ChangeSignificant(int userId, int newsId, bool significant)
        {
            var news = await ForbiddenNewsAsync(userId, newsId);
            news.significant = significant;
            // no update news
            // can user update patch method 
            var result = await _repository.NewsRepository.UpdateEntityAsync(news, default!);
            return result.id;
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

        public async Task DeleteNewsAsync(int userId, int newsId)
        {
            await ForbiddenNewsAsync(userId, newsId);
            var newsStatus = await _repository.NewsStatusRepository.GetAllStatusByNewsSpecific(newsId, Status.Failed);
            if (newsStatus.Any())
            {
                throw new Exception("Tin đã từng gửi đi không thể xóa");
            }
            await _repository.NewsRepository.DeleteEntityAsync(newsId, default!);
        }

        public async Task<NewsNormalReponse> GetNewsNormalByIdAsync(int userId, int newsId)
        {
            var newsReponse = (NewsNormalReponse)await GetNewsBaseAsync(userId, newsId, false);
            //news
            //news content
            var newsContent = await _repository.NewsContentRepository.GetAllNewsContentByNewsId(newsId, default!);
            var content = newsContent.ElementAt(0);
            newsReponse.title = content.title;
            newsReponse.content_html = content.content_html;
            // menu  
            return newsReponse;
            // want need create-by
        }
        public async Task<NewsServicesReponse> GetNewsServicesByIdAsync(int userId, int newsId)
        {
            var newsReponse = (NewsServicesReponse)await GetNewsBaseAsync(userId, newsId, true);
            //news content
            var newsContents = await _repository.NewsContentRepository.GetAllNewsContentByNewsId(newsId, default!);
            newsReponse.content = Enumerable.Select(newsContents, (x) =>
            {
                return new NewsContentReponse(x.id, x.content_html, x.title);
            }); 
            return newsReponse;
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
