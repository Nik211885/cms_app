using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.CMS.News
{
    public class NewsRepository : Repository<int, CMSNews>, INewsRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public NewsRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider) : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }
    }
}
