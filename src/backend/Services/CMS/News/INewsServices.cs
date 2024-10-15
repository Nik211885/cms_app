namespace backend.Services.CMS.News
{
    public interface INewsServices : INewsNormal, INewService
    {
        Task<int> ChangeActiveNewsAsync(int newsId, bool active);
        Task<int> CensorAsync(int userId, int newsId, bool censor, string? message);
        Task DeleteNewsAsync(int userId, int newsId);
        Task<int> ChangeSignificant(int userId, int newsId, bool significant);
    }
}
