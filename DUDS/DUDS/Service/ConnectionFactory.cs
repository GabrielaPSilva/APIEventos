using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ConnectionFactory
    {
        public static DbConnection Dahlia()
        {
            return null;
            //var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InsertOnlyEvent"].ConnectionString);
            //connection.Open();

            //return connection;
        }
    }
}
