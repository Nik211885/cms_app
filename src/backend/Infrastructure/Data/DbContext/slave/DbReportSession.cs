using Npgsql;
using System.Data;
using UC.Core.Abstracts;

namespace backend.Infrastructure.Data.DbContext.slave
{
    public class DbReportSession : absBaseSession, IDisposable
    {
        public DbReportSession(IConfiguration configuration)
        {
            if (configuration.GetConnectionString("report") != null && !string.IsNullOrEmpty(configuration.GetConnectionString("report").ToString()))
            {
                Connection = new NpgsqlConnection(configuration.GetConnectionString("report"));
                Connection.Open();
            }
        }

        public void Dispose()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
