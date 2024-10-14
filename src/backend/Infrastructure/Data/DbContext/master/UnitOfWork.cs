namespace backend.Infrastructure.Data.DbContext.master
{
    public class UnitOfWork : AbsUnitOfWork<DbSession>
    {
        public UnitOfWork(DbSession session) : base(session)
        {

        }
    }
}
