using backend.Core.ValueObject;
using backend.DTOs.SM.Reponse;
using UC.Core.Models.FormData;

namespace backend.Services.SM.News
{
    public interface ISMNewsServices : ISMNewsNormal, ISMNewService
    {
        // support two way pagination in server all return all items
        //Task<PagedResponse> GetNewsDescriptionForUserWithPaginationAsync(int userId);
        //Task<SMNewsDescriptionReponse> GetAllNewsDescriptionForUserAsync(int userId);
        Task<PagedResponse> SearchNewsDescriptionWithPaginationAsync(OSearch? search = null, int? userId = null, bool active = false, bool status = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="search"></param>
        /// <param name="userId"></param>
        /// <param name="active"></param>
        /// <param name="isStatus">true operator =  false operator !=</param>
        /// <returns></returns>
        // cac hanh dong nay thuoc ve noi tai cua entity news
        Task<IEnumerable<SMNewsDescriptionReponse>> SearchAllNewsDescriptionAsync(Status status, OSearch? search, int? userId = null, bool active = false, bool isStatus = false);
        Task<int> ChangeActiveNewsAsync(int newsId, bool active);
        Task<int> CensorAsync(int userId, int newsId, bool censor, string? message);
        Task DeleteNewsAsync(int userId, int newsId);
        Task<int> ChangeSignificant(int userId, int newsId, bool significant);
        Task<int> SendNewsAsync(int userId, int newsId, string? message);
        Task<IEnumerable<StatisticalStatusNewsReponse>> GetStatisticalNewsAsync(DateTime startDay, DateTime endDay, OSearch? search);
    }
}
