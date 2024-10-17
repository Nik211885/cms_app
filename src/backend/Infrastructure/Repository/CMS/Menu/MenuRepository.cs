using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;
using System.Data.Common;
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

        public async Task<List<cms_menu>> GetAllMenuByNewsIdAsync(int newsId, DbTransaction transaction)
        {
            var sql = @"SELECT cms_menu.* FROM cms_menu
                        JOIN cms_menu_news ON cms_menu.id = cms_menu_news.menu_id
                        WHERE cms_menu_news.news_id = @newsId";
            var result = await _unitOfWork.Repository.QueryListAsync<cms_menu>(sql, new { newsId }, transaction);
            return result;
        }

        public async Task<IReadOnlyCollection<cms_menu>> GetMenuAsync(string? type, bool getChild)
        {
            var queryGetRootNode = new StringBuilder()
                            .Append(@"SELECT cms_menu.* FROM ""cms_menu""");

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
            parentNode.RaiseMenuChild(childNote);
            foreach (var child in childNote is null ? [] : childNote)
            {
                await GetChildRecursionAsync(child);
            }
        }
    }
}
