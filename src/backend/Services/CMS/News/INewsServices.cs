namespace backend.Services.CMS.News
{
    public interface INewsServices : INewsNormal, INewService
    {
        Task DeleteNewsAsync(int userId, int newsId);
    }
}
