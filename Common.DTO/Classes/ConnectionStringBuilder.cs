using Common.Constants;
using System.Configuration;

namespace Common.Classes
{
    public class ConnectionStringBuilder
    {
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[SystemConstants.CONNECTION_STRING_KEY].ToString();

            return connectionString;
        }

        public static string ConnectionString => GetConnectionString();
    }
}