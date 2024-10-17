using backend.Core.Entities.CMS;
using backend.Infrastructure.Repository;

namespace backend.Services.CMS.MenuType
{
    public class MenuTypeServices : IMenuTypeServices
    {
        private readonly IRepositoryWrapper _repository;
        public MenuTypeServices(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateMenuTypeAsync(string nameMenuType)
        {
            var menuType = new cms_menu_type(nameMenuType);
            var result = await _repository.MenuTypeRepository.InsertEntityAsync(menuType, default!);
            return result.id;
        }

        public async Task<IEnumerable<cms_menu_type>> GetAllMenuTypeAsync()
        {
            var result = await _repository.MenuTypeRepository.GetEntitiesAsync(null, null, default!);
            return result;
        }

        public async Task<int> UpdateMenuTypeAsync(int menuTypeId, string newNameMenuType)
        {
            var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menuTypeId, default!);
            if(menuType is null)
            {
                throw new Exception("Menu type is not exits");
            }
            menuType.name_type = newNameMenuType;
            menuType.update_at = DateTime.Now;
            var result = await _repository.MenuTypeRepository.UpdateEntityAsync(menuType, default!);
            return result.id;
        }

        public async Task DeleteMenuTypeAsync(int menuTypeId)
        {
            var menuType = await _repository.MenuTypeRepository.GetEntityByIdAsync(menuTypeId, default!);
            if (menuType is null)
            {
                throw new Exception("Menu type is not exits");
            }
            await _repository.MenuTypeRepository.DeleteEntityAsync(menuTypeId, default!);
        }
    }
}
