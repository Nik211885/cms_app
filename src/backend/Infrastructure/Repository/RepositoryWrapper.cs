using backend.Infrastructure.Data.DbContext.master;
using backend.Infrastructure.Data.DbContext.slave;
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
    }
}
