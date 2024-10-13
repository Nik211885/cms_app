using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.CMS.Request;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;
using backend.Validation;

namespace backend.Services.CMS.News
{
    public class NewsServices : INewsServices
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UnitOfWork _unitOfWork;
        public NewsServices(IRepositoryWrapper repository, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<int> CreateNewsNormalAsync(int userId, CreateNewsNormalRequest request)
        {

            return await CreateNewsAsync(userId, request, (newsId) =>
            {
                return [new cms_news_content(newsId, request.content_html, request.title)];
            });
        }

        public async Task<int> CreateNewsServicesAsync(int userId, CreateNewsServicesRequest request)
        {
            foreach(var m in request.menu_id)
            {
                var menu = await _repository.MenuRepository.GetEntityByIdAsync(m, default!);
                var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menu.menu_type_id, default!);
                if(!menuType.name_type.ToUpper().Equals("DỊCH VỤ"))
                {
                    throw new Exception("Tin dịch vụ mới thể có các tin con");
                }
            }
            return await CreateNewsAsync(userId, request, (newsId) =>
            {
                return Enumerable.Select(request.news_content, (n) =>
                {
                    return new cms_news_content(newsId, n.content_html, n.title);
                });
            });
        }
        private async Task<int> CreateNewsAsync(int userId, dynamic request, Func<int, IEnumerable<cms_news_content>> func)
        {
            NewsValidation.NewsEdit(request.status.status);
            var news = new cms_news(userId);
            ObjectHelpers.Mapping(request, news);
            _unitOfWork.Repository.BeginTransaction();
            try
            {
                // insert news
                var newsAfterInsert = await _repository.NewsRepository.InsertEntityAsync(news, default!);
                //insert menu
                foreach(var m in request.menu_id)
                {
                    var menuNews = new cms_menu_news(m, newsAfterInsert.id);
                    await _repository.MenuNewsRepository.AddAsync(menuNews, default!);
                }
                // insert news_content
                var newsContent = func(newsAfterInsert.id);
                foreach (var n in newsContent)
                {
                    await _repository.NewsContentRepository.InsertEntityAsync(n, default!);
                }
                //insert status
                var status = new cms_news_status(request.status.status, newsAfterInsert.id, request.status.message, userId);
                await _repository.NewsStatusRepository.AddAsync(status, default!);
                _unitOfWork.Repository.Commit();
                return newsAfterInsert.id;
            }
            catch
            {
                _unitOfWork.Repository.Rollback();
                throw;
            }
        }
    }
}
