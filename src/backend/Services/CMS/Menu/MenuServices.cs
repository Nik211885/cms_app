using backend.Core.Entities.CMS;
using backend.DTOs.CMS.Reponse;
using backend.DTOs.CMS.Request;
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
                GetChildRecursion(menuDTO, m);
                menuReponse.Add(menuDTO);
            }
            return menuReponse;
        }
        private static void GetChildRecursion(MenuReponse menuDTO, cms_menu menu)
        {
            ObjectHelpers.Mapping(menu, menuDTO);
            menu.GetMenu(out List<cms_menu>? menuList);
            if (menuList is null)
            {
                return;
            }
            foreach (var child in menuList)
            {
                var childDTO = new MenuReponse();
                ObjectHelpers.Mapping(child, childDTO);
                menuDTO.menu_child ??= [];
                menuDTO.menu_child.Add(childDTO);
                GetChildRecursion(childDTO, child);
            }
        }


        public async Task<int> CreateMenuChildAsync(CreateMenuChildRequest request)
        {
            // get menu type in parent id
            var menuType = await _repository.MenuTypeRepository.GetMenuTypeByMenuAsync(request.parent_menu_id);
            var cmsMenu = new cms_menu(request.parent_menu_id,menuType.id, request.name);
            var result = await _repository.MenuRepository.InsertEntityAsync(cmsMenu, default!);
            return result.id;

        }

        public async Task<int> CreateMenuParentAsync(CreateMenuParentRequest request)
        {
            var menuServices = await _repository.MenuRepository.GetMenuAsync("dịch vụ", false);
            var menuServicesParent = menuServices.ElementAt(0);
            var cmsMenu = new cms_menu(menuServicesParent.id, menuServicesParent.menu_type_id, request.name);
            var result = await _repository.MenuRepository.InsertEntityAsync(cmsMenu,default!);
            return result.id;
        }
    }
}
