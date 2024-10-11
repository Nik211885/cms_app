using Dapper;
using System.Data;
using UC.Core.Abstracts;
using UC.Core.Interfaces;
using UC.Core.Models;

namespace backend.Infrastructure.Data.DbContext.slave
{
    public class UnitOfWorkReport : AbsUnitOfWork<DbReportSession>
    {
        public UnitOfWorkReport(DbReportSession session) : base(session)
        {

        }
    }
}
