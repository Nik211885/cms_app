using backend.Core.Entities.CMS;
using backend.Core.ValueObject;
using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
using backend.Helper;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace backend.Services.SM.News
{
    public class SMNewUntil
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UnitOfWork _unitOfWork;
        protected SMNewUntil(IRepositoryWrapper repository, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        protected async Task<int> UpdateNewsAsync(int userId, int newsId, dynamic request, bool services, IFormFile? avatar = null)
        {
            RuleCreateNews(request.status);
            var statusNow = await _repository.NewsStatusRepository.GetNewStatusByNewsAsync(newsId);
            if (statusNow.status == Status.Send)
            {
                throw new Exception("Tin của bạn đang trong quá trình kiểm duyệt không thể cập nhật");
            }
            if (statusNow.status == Status.Success)
            {
                throw new Exception("Tin đã duyệt không thể thay đổi trạng thái");
            }
            await ClassifyNewsAsync(request, services);
            var news = await ForbiddenNewsAsync(userId, newsId);
            ObjectHelpers.Mapping(request, news);
            if(avatar is not null)
            {
                var urlAvatar = await FileHelper.UploadImageAsync(avatar);
                news.image_description = urlAvatar;
            }
            news.update_at = DateTime.Now;
            _unitOfWork.Repository.BeginTransaction();
            try
            {
                //update news
                await _repository.NewsRepository.UpdateEntityAsync(news, default!);
                //update menu
                if (request.menu_id is not null)
                {
                    // delete all menu news
                    await _repository.MenuNewsRepository.DeleteAllByNewsIdAsync(newsId, default!);
                    //insert news menu
                    foreach (var m in request.menu_id)
                    {
                        var menuNews = new cms_menu_news(m, newsId);
                        await _repository.MenuNewsRepository.AddAsync(menuNews, default!);
                    }
                }
                // update news content
                // if news content need update bigger news content has created update all news old and create news content
                var newsContent = await _repository.NewsContentRepository.GetAllNewsContentByNewsId(newsId, default!);
                if (services)
                {
                    var newsContentRequest = (List<UpdateNewsContentRequest>)request.news_content;
                    foreach (var n in newsContent)
                    {
                        var m = newsContentRequest.FirstOrDefault(x => x.id == n.id);
                        if (m is null)
                        {
                            // delete news content
                            await _repository.NewsContentRepository.DeleteEntityAsync(n.id, default!);
                        }
                        else
                        {
                            // update
                            ObjectHelpers.Mapping(m, n);
                            await _repository.NewsContentRepository.UpdateEntityAsync(n, default!);
                            newsContentRequest.Remove(m);
                        }
                    }
                    // insert news content
                    foreach (var n in newsContentRequest is null ? [] : newsContentRequest)
                    {
                        var newsContentInsert = new cms_news_content(newsId, n.content_html, n.title);
                        await _repository.NewsContentRepository.InsertEntityAsync(newsContentInsert, default!);
                    }
                }
                else
                {
                    var content = newsContent.ElementAt(0);
                    ObjectHelpers.Mapping(request, content);
                    await _repository.NewsContentRepository.UpdateEntityAsync(content, default!);
                }
                var status = new cms_news_status(request.status, newsId, request.status_message, userId);
                await _repository.NewsStatusRepository.AddAsync(status);
                _unitOfWork.Repository.Commit();
                return news.id;
            }
            catch
            {
                _unitOfWork.Repository.Rollback();
                throw;
            }

        }
        protected async Task<int> CreateNewsAsync(int userId, dynamic request, Func<int, IEnumerable<cms_news_content>> func, bool services, IFormFile? avatar =  null)
        {
            RuleCreateNews(request.status);
            if (request.menu_id is null)
            {
                throw new Exception("Chọn loại tin trước khi thêm tin");
            }
            await ClassifyNewsAsync(request, services);
            var news = new cms_news(userId);
            ObjectHelpers.Mapping(request, news);
            if(avatar is not null)
            {
                var pathAvatar = await FileHelper.UploadImageAsync(avatar);
                news.image_description = pathAvatar;
            }
            _unitOfWork.Repository.BeginTransaction();
            try
            {                
                // insert news
                var newsAfterInsert = await _repository.NewsRepository.InsertEntityAsync(news, default!);
                //insert menu
                foreach (var m in request.menu_id)
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
                var status = new cms_news_status(request.status, newsAfterInsert.id, request.status_message, userId);
                await _repository.NewsStatusRepository.AddAsync(status);
                _unitOfWork.Repository.Commit();
                return newsAfterInsert.id;
            }
            catch
            {
                _unitOfWork.Repository.Rollback();
                throw;
            }
        }
        protected async Task ClassifyNewsAsync(dynamic request, bool services)
        {
            foreach (var m in request.menu_id)
            {
                var menu = await _repository.MenuRepository.GetEntityByIdAsync(m, null)
                    ?? throw new Exception($"Khong tim thay menu co id {m}");
                var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menu.menu_type_id, null)
                    ?? throw new Exception("Menu type is null");
                if (services)
                {
                    if (!menuType.name_type.ToUpper().Equals("DỊCH VỤ"))
                    {
                        throw new Exception("Đây là mục thêm tin dịch vụ");
                    }
                }
                else
                {
                    if (menuType.name_type.ToUpper().Equals("DỊCH VỤ"))
                    {
                        throw new Exception("Đây là mục thêm tin bài");
                    }
                }
            }
        }
        protected async Task<cms_news> ForbiddenNewsAsync(int userId, int newsId)
        {
            var news = await _repository.NewsRepository.GetEntityByIdAsync(newsId, default!);
            if (news is null)
            {
                throw new Exception($"Không tìm thấy bài viết có id là {newsId}");
            }
            if (news.create_by != userId)
            {
                throw new Exception("403: Bạn không thể cập nhật bài viết không thuộc về bạn");
            }
            return news;
        }
        protected static void RuleCreateNews(Status status)
        {
            if (status > Status.Send)
            {
                throw new Exception($"Bạn không thể thay đổi bài sang trạng thái {status}");
            }
        }
        protected async Task<NewsReponse> GetNewsBaseAsync(int userId, int newsId, bool services)
        {
            var news = await ForbiddenNewsAsync(userId, newsId);
            NewsReponse newsReponse;
            if (services)
            {
                newsReponse = new NewsServicesReponse();
            }
            else
            {
                newsReponse = new NewsNormalReponse();
            }
            ObjectHelpers.Mapping(news, newsReponse);
            var cmsMenuNews = await _repository.MenuRepository.GetAllMenuByNewsIdAsync(newsId, default!);
            newsReponse.menu = cmsMenuNews.Select((x) =>
            {
                var menu = new MenuReponse();
                ObjectHelpers.Mapping(x, menu);
                return menu;
            });
            //status
            var status = await _repository.NewsStatusRepository.GetNewStatusByNewsAsync(newsId);
            newsReponse.status = status.status;
            newsReponse.status_message = status.message;
            var createBy = await _repository.AccountRepository.GetEntityByIdAsync(news.create_by, default!);
            newsReponse.create_by_id = createBy.id;
            newsReponse.create_by_full_name = createBy.full_name;
            return newsReponse;
        }
    }
}
