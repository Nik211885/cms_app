using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;

namespace backend.Services.CMS.Menu
{
    public interface IMenuServices
    {
        Task<int> CreateMenuChildAsync(CreateMenuChildRequest request);
        Task<int> CreateMenuParentAsync(CreateMenuParentRequest request);
        Task<IEnumerable<MenuReponse>> GetMenuAsync(string? nameType, bool getChild = true);
    }
}
