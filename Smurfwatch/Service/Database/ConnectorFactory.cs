using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Smurfwatch.Service.Database
{
    public class ConnectorFactory
    {
        private const string DatabaseName = "SmurfDatabase";
        private readonly IConfiguration configuration;

        public ConnectorFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(configuration.GetConnectionString(DatabaseName));
        }
    }
}
