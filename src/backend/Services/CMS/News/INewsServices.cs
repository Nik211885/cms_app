namespace backend.Services.CMS.News
{
    public interface INewsServices : INewsNormal, INewService
    {
        Task DeleteNewsAsync(int userId, int newsId);
        Task<int> ChangeSignificant(int userId, int newsId, bool significant);
    }
}
