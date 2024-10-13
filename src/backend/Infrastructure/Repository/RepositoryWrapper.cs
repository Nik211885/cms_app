using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Data.DbContext.slave;
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
using UC.Core.Interfaces;

namespace backend.Infrastructure.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UnitOfWorkReport _unitOfWorkReport;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;

        public RepositoryWrapper(UnitOfWork unitOfWork, UnitOfWorkReport unitOfWorkReport, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkReport = unitOfWorkReport;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }
        private readonly IContactRepository _contactRepository = null;
        private readonly IFeedBackRepository _feedBackRepository = null;
        private readonly IMenuRepository _menuRepository = null;
        private readonly IMenuNewsRepository _menuNewsRepository = null;
        private readonly IMenuTypeRepository _menuTypeRepository = null;
        private readonly INewsRepository _newsRepository = null;
        private readonly INewsContentRepository _newsContentRepository = null;
        private readonly INewsStatusRepository _newsStatusRepository = null;
        private readonly IAccountRepository _accountRepository = null;
        private readonly IAccountClaimRepository _accountClaimRepository = null;
        private readonly IAccountRoleRepository _accountRoleRepository = null;
        private readonly IRoleRepository _roleRepository = null;
        private readonly IRoleClaimRepository _roleClaimRepository = null!;
        public IContactRepository ContactRepository => _contactRepository ?? new ContactRepository(_unitOfWork,_dateTimeProvider,_userProvider);

        public IFeedBackRepository FeedBackRepository => _feedBackRepository ?? new FeedBackRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IMenuRepository MenuRepository => _menuRepository ?? new MenuRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IMenuNewsRepository MenuNewsRepository => _menuNewsRepository ?? new MenuNewsRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IMenuTypeRepository MenuTypeRepository => _menuTypeRepository ?? new MenuTypeRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public INewsRepository NewsRepository => _newsRepository ?? new NewsRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public INewsContentRepository NewsContentRepository => _newsContentRepository ?? new NewsContentRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public INewsStatusRepository NewsStatusRepository => _newsStatusRepository ?? new NewsStatusRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IAccountRepository AccountRepository => _accountRepository ?? new AccountRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IAccountClaimRepository AccountClaimRepository => _accountClaimRepository ?? new AccountClaimRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IAccountRoleRepository AccountRoleRepository => _accountRoleRepository ?? new AccountRoleRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_unitOfWork, _dateTimeProvider, _userProvider);

        public IRoleClaimRepository RoleClaimRepository => _roleClaimRepository ?? new RoleClaimRepository(_unitOfWork, _dateTimeProvider, _userProvider);
    }
}
