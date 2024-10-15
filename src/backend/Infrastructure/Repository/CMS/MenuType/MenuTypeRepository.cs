using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.CMS.MenuType
{
    public class MenuTypeRepository : Repository<int, cms_menu_type>, IMenuTypeRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public MenuTypeRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<cms_menu_type> GetMenuTypeByMenuAsync(int menuId)
        {
            var sql = @"WITH template AS (
                        SELECT menu_type_id FROM cms_menu WHERE id = @menuId)
                        SELECT cms_menu_type.* FROM cms_menu_type
                        JOIN template ON template.menu_type_id = cms_menu_type.id";
            var result = await _unitOfWork.Repository.QueryFirstAsync<cms_menu_type>(sql, new{ menuId});
            return result;
        }
    }
}
