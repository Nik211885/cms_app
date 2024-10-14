using backend.DTOs.CMS.Reponse;

namespace backend.Services.CMS.Menu
{
    public interface IMenuServices
    {
        Task<IEnumerable<MenuReponse>> GetMenuAsync(string? nameType, bool getChild = true);
    }
}
