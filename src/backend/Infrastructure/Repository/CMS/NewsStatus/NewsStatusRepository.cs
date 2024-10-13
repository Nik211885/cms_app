using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.CMS.NewsStatus
{
    public class NewsStatusRepository : INewsStatusRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public NewsStatusRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }
    }
}
