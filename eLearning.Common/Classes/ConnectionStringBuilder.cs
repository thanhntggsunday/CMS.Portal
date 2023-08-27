
using Common.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.Common.Classes
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
