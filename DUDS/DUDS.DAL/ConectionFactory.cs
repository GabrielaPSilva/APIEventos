using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DUDS.DAL
{
    public class ConnectionFactory
    {
        public static DbConnection DahliaDatabaseContext()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DahliaDatabaseContext"].ConnectionString);
            connection.Open();

            return connection;
        }
        public static DbConnection ConexaoAsync(string banco = "DahliaDatabaseContext")
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[banco].ConnectionString);
            connection.OpenAsync();

            return connection;
        }
    }
}
