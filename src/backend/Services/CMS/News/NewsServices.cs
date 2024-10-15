using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;
using UC.Core.Models.FormData;

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

        public async Task<int> CensorAsync(int userId ,int newsId, bool censor, string? message)
        {
            var news = await _repository.NewsRepository.GetEntityByIdAsync(newsId, default!);
            if (news is null)
            {
                throw new Exception($"Don't find news has news id is {newsId}");
            }
            var statusNew = await _repository.NewsStatusRepository.GetNewStatusByNewsAsync(newsId);
            if(statusNew.status != Status.Send)
            {
                throw new Exception("Bài viết đang không ở trạng thái chờ duyệt không thể duyệt bài");
            }
            var statusCode = censor ? Status.Success : Status.Failed;
            var status = new cms_news_status(statusCode,newsId,message, userId);
            await _repository.NewsStatusRepository.AddAsync(status);
            return news.id;
        }

        public async Task<int> ChangeActiveNewsAsync(int newsId, bool active)
        {
            var news = await _repository.NewsRepository.GetEntityByIdAsync(newsId,default!);
            if (news is null)
            {
                throw new Exception($"Không tin thấy bài viết có id là {newsId}");
            }
            var status = await _repository.NewsStatusRepository.GetNewStatusByNewsAsync(newsId);
            if(status.status != Status.Success)
            {
                throw new Exception("Tin chưa được duyệt không được tắt hay bật active");
            }
            news.active = active;
            await _repository.NewsRepository.UpdateEntityAsync(news, default!);
            return news.id;
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

        public async Task<IEnumerable<NewsDescriptionReponse>> SearchAllNewsDescriptionAsync
            (Status status,OSearch? search, int? userId = null, bool active = false, bool isStatus = false )
        {
            var news = await _repository.NewsRepository.GetAllNewsAsync(status, search, userId, active, isStatus);
            List<NewsDescriptionReponse> newsReponse = [];
            var type = search?.fields?.Select((x) =>
            {
                return x.code.ToUpper().Equals("TYPE") ? x.value.ToUpper() : null;
            });
            string? typeSearch = (type is null || !type.Any()) ? null : type.ElementAt(0);
            for (int i = 0; i< news.Count(); i++)
            {
                Console.WriteLine(i);
                var newsIndex = news.ElementAt(i);
                var newsR = new NewsDescriptionReponse
                {
                    stt = i + 1
                };
                ObjectHelpers.Mapping(newsIndex, newsR);
                var menuNews = await _repository.MenuRepository.GetAllMenuByNewsIdAsync(newsIndex.id, default!);
                var menuType = await _repository.MenuTypeRepository.GetMenuTypeByMenuAsync(menuNews.ElementAt(0).id);
                if(typeSearch is not null)
                {
                    if (!menuType.name_type.ToUpper().Equals(typeSearch))
                    {
                        continue;
                    }
                }
                var newsContent = await _repository.NewsContentRepository.GetAllNewsContentByNewsId(newsIndex.id, default!);
                var statusQuery = await _repository.NewsStatusRepository.GetNewStatusByNewsAsync(newsIndex.id);
                newsR.status = statusQuery.status;
                newsR.status_message = statusQuery.message;
                if (menuType.name_type.ToUpper().Equals("DỊCH VỤ"))
                {
                    newsR.title = menuNews.ElementAt(0).name;
                    newsR.content_title = String.Join(", ", newsContent.Select(x=>x.title).ToArray());
                }
                else
                {
                    var content = newsContent.ElementAt(0);
                    newsR.title = content.title;
                    newsR.content_title = newsIndex.news_description;
                }
                newsReponse.Add(newsR);
            }
            return newsReponse;
        }

        public async Task<PagedResponse> SearchNewsDescriptionWithPaginationAsync
            (OSearch? search, int? userId = null, bool active = true, bool status = false)
        {
            return null;
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
