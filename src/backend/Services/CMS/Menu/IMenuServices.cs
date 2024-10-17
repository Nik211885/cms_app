using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;

namespace backend.Services.CMS.Menu
{
    public interface IMenuServices
    {
        Task<int> CreateMenuAsync(CreateMenuRequest request);
        Task<IEnumerable<MenuReponse>> GetMenuAsync(string? nameType, bool getChild = true);
        Task DeleteMenuAsync(int id);
        Task<int> UpdateMenuAsync(int id, UpdateMenuRequest request);
    }
}
