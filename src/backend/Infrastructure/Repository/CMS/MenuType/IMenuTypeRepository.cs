﻿using backend.Core.Entities.CMS;

namespace backend.Infrastructure.Repository.CMS.MenuType
{
    public interface IMenuTypeRepository : IRepositoryBase<int, cms_menu_type>
    {
        Task<cms_menu_type> GetMenuTypeByMenuAsync(int menuId);  
    }
}
