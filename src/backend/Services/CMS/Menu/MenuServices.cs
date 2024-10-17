using backend.Core.Entities.CMS;
using backend.Core.Exceptions;
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


        //public async Task<int> CreateMenuChildAsync(CreateMenuRequest request)
        //{
        //    // get menu type in parent id
        //    var menuType = await _repository.MenuTypeRepository.GetMenuTypeByMenuAsync(request.parent_menu_id);
        //    var cmsMenu = new cms_menu(request.parent_menu_id,menuType.id, request.name);
        //    var result = await _repository.MenuRepository.InsertEntityAsync(cmsMenu, default!);
        //    return result.id;

        //}

        //public async Task<int> CreateMenuParentAsync(CreateMenuParentRequest request)
        //{
        //    var menuServices = await _repository.MenuRepository.GetMenuAsync("dịch vụ", false);
        //    var menuServicesParent = menuServices.ElementAt(0);
        //    var cmsMenu = new cms_menu(menuServicesParent.id, menuServicesParent.menu_type_id, request.name);
        //    var result = await _repository.MenuRepository.InsertEntityAsync(cmsMenu,default!);
        //    return result.id;
        //}
        public async Task DeleteMenuAsync(int menuId)
        {
            var menu = await _repository.MenuRepository.GetEntityByIdAsync(menuId, default!);
            if (menu is null)
            {
                throw new NotFoundException(menuId);
            }
            await _repository.MenuRepository.DeleteEntityAsync(menuId, default!);
        }

        public async Task<int> UpdateMenuAsync(int id, UpdateMenuRequest request)
        {
            var menu = await _repository.MenuRepository.GetEntityByIdAsync(id, default!);
            if(menu is null)
            {
                throw new NotFoundException(id);
            }
            ObjectHelpers.Mapping(request, menu);   
            var result = await _repository.MenuRepository.UpdateEntityAsync(menu,default!);
            return result.id;

        }

        public async Task<int> CreateMenuAsync(CreateMenuRequest request)
        {
            if(request.parent_menu_id is not null)
            {
                var menuParentId = (int)request.parent_menu_id;
                // check parent menu id has exits
                var menu = await _repository.MenuRepository.GetEntityByIdAsync(menuParentId, default!);
                if(menu is null)
                {
                    throw new NotFoundException(menuParentId);
                }
                var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menu.menu_type_id, default!);
                if(menuType is null)
                {
                    throw new NotFoundException(menu.menu_type_id);
                }
                var newMenu = new cms_menu(menuParentId,menu.menu_type_id, request.name);
                var result = await _repository.MenuRepository.InsertEntityAsync(newMenu, default!);
                return result.id;
            }
            else
            {
                if(request.menu_type_id is not null)
                {
                    var menuTypeId = (int)request.menu_type_id;
                    var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menuTypeId, default!);
                    if (menuType is null)
                    {
                        throw new NotFoundException(menuTypeId);
                    }
                    var newMenu = new cms_menu(null,menuTypeId,request.name);
                    var result = await _repository.MenuRepository.InsertEntityAsync(newMenu, default!);
                    return result.id;
                }
                else
                {
                    throw new ValidationException("Menu phải có type");
                }
            }
        }
    }
}
