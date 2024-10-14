namespace backend.Infrastructure.Data.DbContext.slave
{
    public class UnitOfWorkReport : AbsUnitOfWork<DbReportSession>
    {
        public UnitOfWorkReport(DbReportSession session) : base(session)
        {

        }
    }
}
