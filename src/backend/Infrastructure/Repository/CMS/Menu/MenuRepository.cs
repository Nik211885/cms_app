using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;
using System.Text;

namespace backend.Infrastructure.Repository.CMS.Menu
{
    public class MenuRepository : Repository<int, cms_menu>, IMenuRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public MenuRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<IReadOnlyCollection<cms_menu>> GetMenuAsync(string? type, bool getChild)
        {
            var queryGetRootNode = new StringBuilder()
                            .Append(@"SELECT ""cms_menu"".""id"", ""cms_menu"".""name"" FROM ""cms_menu""");

            queryGetRootNode.Append(@" JOIN ""cms_menu_type"" ON ""cms_menu_type"".""id"" = ""cms_menu"".""menu_type_id""");
            queryGetRootNode.Append(@" WHERE ""parent_menu_id"" IS NULL AND UPPER(""cms_menu_type"".""name_type"")");
            if (type is null)
            {
                queryGetRootNode.Append(" != UPPER('Nhóm Tin')");
            }
            else
            {
                queryGetRootNode.Append($" = UPPER('{type}')");
            }
            if (!getChild)
            {
                return await _unitOfWork.Repository.QueryListAsync<cms_menu>(queryGetRootNode.ToString(), default!);
            }
            var menu = await _unitOfWork.Repository.QueryListAsync<cms_menu>(queryGetRootNode.ToString(), default!);
            foreach (var parent_menu in menu)
            {
                await GetChildRecursionAsync(parent_menu);
            }
            return menu;
        }
        private async Task GetChildRecursionAsync(cms_menu parentNode)
        {
            var queryGetSubNode = @"SELECT * FROM ""cms_menu"" WHERE ""parent_menu_id"" = @id";
            var childNote = await _unitOfWork.Repository
                .QueryListAsync<cms_menu>(queryGetSubNode, new { parentNode.id });
            if (childNote.Count == 0)
            {
                return;
            }
            else
            {
                parentNode.MenuChild ??= [];
            }
            parentNode.MenuChild.AddRange(childNote);
            foreach (var child in childNote is null ? [] : childNote)
            {
                await GetChildRecursionAsync(child);
            }
        }
    }
}
