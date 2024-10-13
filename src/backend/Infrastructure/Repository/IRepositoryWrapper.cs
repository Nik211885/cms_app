using backend.Infrastructure.Repository.CMS.Contact;
using backend.Infrastructure.Repository.CMS.FeedBack;
using backend.Infrastructure.Repository.CMS.Menu;
using backend.Infrastructure.Repository.CMS.MenuNews;
using backend.Infrastructure.Repository.CMS.MenuType;
using backend.Infrastructure.Repository.CMS.News;
using backend.Infrastructure.Repository.CMS.NewsContent;
using backend.Infrastructure.Repository.CMS.NewsStatus;
using backend.Infrastructure.Repository.SM.Account;
using backend.Infrastructure.Repository.SM.AccountClaim;
using backend.Infrastructure.Repository.SM.AccountRole;
using backend.Infrastructure.Repository.SM.Role;
using backend.Infrastructure.Repository.SM.RoleClaim;

namespace backend.Infrastructure.Repository
{
    public interface IRepositoryWrapper
    {
        IContactRepository ContactRepository { get; }
        IFeedBackRepository FeedBackRepository { get; }
        IMenuRepository MenuRepository { get; }
        IMenuNewsRepository MenuNewsRepository { get; }
        IMenuTypeRepository MenuTypeRepository { get; }
        INewsRepository NewsRepository { get; }
        INewsContentRepository NewsContentRepository { get; }
        INewsStatusRepository NewsStatusRepository { get; }
        IAccountRepository AccountRepository { get; }
        IAccountClaimRepository AccountClaimRepository { get; }
        IAccountRoleRepository AccountRoleRepository { get; }
        IRoleRepository RoleRepository { get; }
        IRoleClaimRepository RoleClaimRepository { get; }
    }
}
