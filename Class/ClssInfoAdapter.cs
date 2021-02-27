using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Util;

namespace TurboAz.Class
{
    class ClssInfoAdapter
    {
        SqlUtils sqlUtils = SqlUtils.GetInstance();
        public DataTable GetBrands()
        {      
            string query = "select ID,Brand_Name from Car_Brand";
            return sqlUtils.GetDataWithAdapter(query);
        }
        public DataTable GetModels(string brandID)
        {
            string query = $"select ID,Model_Name from Car_Model where Brand_ID={brandID}";
            return  sqlUtils.GetDataWithAdapter(query);

        }
        public DataTable GetGeneralType(string typeID)
        {
            string query = $"select ID,Name from General_Info where Type_ID={typeID}";
            return sqlUtils.GetDataWithAdapter(query);

        }
        public DataTable GetImage(string adsID)
        {
            string query = $"SELECT [ID],[Car_Image] FROM[dbo].[Car_Image] where ADS_ID={adsID}";
            return sqlUtils.GetDataWithAdapter(query);

        }
    }
}
