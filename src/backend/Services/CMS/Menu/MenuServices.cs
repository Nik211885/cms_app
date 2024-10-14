using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Reponse;
using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Repository;

namespace backend.Services.CMS.Menu
{
    public class MenuServices : IMenuServices
    {
        private readonly IRepositoryWrapper _repository;
        public MenuServices(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuReponse>> GetMenuAsync(string? type, bool getChild = true)
        {
            var cmsMenu = await _repository.MenuRepository.GetMenuAsync(type, getChild);
            var menuReponse = new List<MenuReponse>();
            foreach (var m in cmsMenu)
            {
                var menuDTO = new MenuReponse();
                GetChildRecursion(menuDTO,m);
                menuReponse.Add(menuDTO);
            }
            return menuReponse;
        }
        private static void GetChildRecursion(MenuReponse menuDTO, cms_menu menu)
        {
            ObjectHelpers.Mapping(menu, menuDTO);
            if (menu.MenuChild is null || menu.MenuChild.Count == 0)
            {
                return;
            }
            foreach(var child in menu.MenuChild)
            {
                var childDTO = new MenuReponse();
                ObjectHelpers.Mapping(child, childDTO);
                menuDTO.menu_child ??= [];
                menuDTO.menu_child.Add(childDTO);
                GetChildRecursion(childDTO,child);
            }
        }
    }
}
