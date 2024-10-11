using Npgsql;
using System.Data;
using UC.Core.Abstracts;

namespace backend.Infrastructure.Data.DbContext.master
{
    public class DbSession : absBaseSession, IDisposable
    {
        public DbSession(IConfiguration configuration)
        {
            if (configuration.GetConnectionString("master") != null && !string.IsNullOrEmpty(configuration.GetConnectionString("master").ToString()))
            {
                Connection = new NpgsqlConnection(configuration.GetConnectionString("master"));
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
