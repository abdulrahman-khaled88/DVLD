using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class DataAccessSettinegs
    {
        public static string ConnectionInfo = "Server=.;Database=DVLD;User Id=sa;Password=123456;";
        public static SqlConnection Connection = new SqlConnection(ConnectionInfo);
    }
}
