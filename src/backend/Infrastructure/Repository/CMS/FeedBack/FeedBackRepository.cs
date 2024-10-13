using backend.Core.Entities.CMS;
using backend.Infrastructure.Data.DbContext.master;

namespace backend.Infrastructure.Repository.CMS.FeedBack
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public FeedBackRepository(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }
    }
}
