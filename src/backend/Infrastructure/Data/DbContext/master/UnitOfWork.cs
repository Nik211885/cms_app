using Dapper;
using System.Data;
using UC.Core.Abstracts;
using UC.Core.Models;

namespace backend.Infrastructure.Data.DbContext.master
{
    public class UnitOfWork : AbsUnitOfWork<DbSession>
    {
        public UnitOfWork(DbSession session) : base(session)
        {

        }
    }
}
