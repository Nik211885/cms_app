using backend.Core.Entities.CMS;

namespace backend.Services.CMS.MenuType
{
    public interface IMenuTypeServices
    {
        Task<int> CreateMenuTypeAsync(string nameMenuType);
        Task<int> UpdateMenuTypeAsync(int menuTypeId, string newNameMenuType);
        Task<IEnumerable<cms_menu_type>> GetAllMenuTypeAsync();
        Task DeleteMenuTypeAsync(int menuTypeId);
    }
}
