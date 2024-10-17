using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Reponse;
using backend.Infrastructure.Repository;
using System.Collections.Immutable;

namespace backend.Services.CMS.News
{
    public class CMSNewsServices : ICMSNewsServices
    {
        private readonly IRepositoryWrapper _repository;
        public CMSNewsServices(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<CMSNewsDescriptionReponse>> GetNewsDescriptionByMenuIdAsync(int menuId)
        {
            return await AggregateNewsDescriptionAsync(async () =>
            {
                return await _repository.NewsRepository.GetAllNewsFollowMenuIdAsync(menuId);
            });
           
        }

        public async Task<IReadOnlyCollection<CMSNewsDescriptionReponse>> GetNewsDescriptionSignificantAsync()
        {
            return await AggregateNewsDescriptionAsync(async () =>
            {
                return await _repository.NewsRepository.GetAllNewsSignificantAsync();
            });  
        }

        public Task<CMSNewsDetailReponse> GetNewsDetailHasActive(int newsId)
        {
            throw new NotImplementedException();
        }

        private async Task<IReadOnlyCollection<CMSNewsDescriptionReponse>> AggregateNewsDescriptionAsync(Func<Task<IEnumerable<cms_news>>> Func)
        {
            var response = new List<CMSNewsDescriptionReponse>();
            var news = await Func();
            foreach (var n in news)
            {
                var newsReponse = new CMSNewsDescriptionReponse();
                ObjectHelpers.Mapping(n, newsReponse);
                var newsContent = await _repository.NewsContentRepository.GetAllNewsContentByNewsId(n.id, default!);
                newsReponse.title = newsContent.ElementAt(0).title;
                response.Add(newsReponse);
            }
            return response;
        }
        
    }
}
