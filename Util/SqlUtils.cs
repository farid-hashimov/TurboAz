using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TurboAz.Util
{
    class SqlUtils
    {
        private static SqlUtils sqlUtils;
        public  string conString { get; set; }

        public SqlUtils()
        {
            conString = ConfigurationManager.ConnectionStrings["MainConString"].ConnectionString;
        }

        public static SqlUtils GetInstance()
        {
            if (sqlUtils==null)
            {
                sqlUtils = new SqlUtils();
                
            }
            return sqlUtils;
        }

        public DataTable GetDataWithAdapter(string _query)
        {
            SqlConnection sqlCon = new SqlConnection(sqlUtils.conString);
            SqlDataAdapter adapter = new SqlDataAdapter(_query, sqlCon);
            DataTable dtTable = new DataTable();
            adapter.Fill(dtTable);
            return dtTable;
        }

       
    }
}
