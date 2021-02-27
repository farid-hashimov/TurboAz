using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurboAz.Class;
using TurboAz.Util;

namespace TurboAz
{
    public partial class frmHomePage : Form
    {
        GeneralMethod generalMethod = new GeneralMethod();
        public frmHomePage()
        {
            InitializeComponent();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            GetRefresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddCars frmAddCars = new frmAddCars();
            frmAddCars.Show();
        }





        private void frmHomePage_Load(object sender, EventArgs e)
        {
            generalMethod.SetBrandData(lkpCarBrand);
            generalMethod.SetGeneralInfo(lkpCurrency, "3");
            generalMethod.SetYears(lkpProductYearBegin);
            generalMethod.SetYears(lkpProductYearEnd);
            generalMethod.SetGeneralInfo(lkpCities, "7");
            GetSearch();

        }


        private void lkpCarBrand_EditValueChanged(object sender, EventArgs e)
        {
            generalMethod.SetModelData(lkpCarModel, lkpCarBrand.EditValue.ToString());

        }

        private void lkpCities_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void GetSearch()
        {
            string query = $@"select 
                    (SELECT top(1) IMG.CAR_IMAGE FROM Car_Image IMG WHERE IMG.ADS_ID =TB_ADS.ID) CAR_IMAGE,
                    (CB.Brand_Name +' '+CM.Model_Name)as Model_Name,
                    GI.ID,
                    GIY.Name,
                    TB_ADS.Change,
                    TB_ADS.Credit,
                    TB_ADS.Amount_ID,   
                    TB_ADS.Years_ID 
                                    from TB_ADS
                    inner join Car_Model as CM on CM.ID=TB_ADS.Model_ID
                    inner join Car_Brand as CB on CB.ID=CM.Brand_ID
                    inner join General_Info as GI on GI.ID=TB_ADS.Cities_ID
                    inner join General_Info as GIY on GIY.ID=TB_ADS.Currency_ID
                ";
            if (lkpCarBrand.EditValue != null)
            {
                query = query + $" AND CB.ID ={lkpCarBrand.EditValue}";
            }

            if (lkpCarModel.EditValue != null)
            {
                query = query + $" AND CM.ID={lkpCarModel.EditValue}";
            }
            if (lkpCurrency.EditValue != null)
            {
                query = query + $" AND GIY.Name={lkpCurrency.EditValue}";
            }

            if (numericUpDownMin.Value != 0)
            {
                query = query + $" AND TB_ADS.Amount_ID >={numericUpDownMin.Value}";
            }

            if (numericUpDownMax.Value != 0)
            {
                query = query + $" AND TB_ADS.Amount_ID <={numericUpDownMax.Value}";
            }

            if (lkpProductYearBegin.EditValue != null)
            {
                query = query + $" AND TB_ADS.Years_ID>={lkpProductYearBegin.EditValue}";
            }

            if (lkpProductYearEnd.EditValue != null)
            {
                query = query + $" AND TB_ADS.Years_ID <={lkpProductYearEnd.EditValue}";
            }

            if (lkpCities.EditValue != null)
            {
                query = query + $" AND GI.ID ={lkpCities.EditValue}";
            }


            if (chcCredit.Checked)
            {
                query = query + $" AND TB_ADS.Credit= 1";
            }


            if (chcChange.Checked)
            {
                query = query + $" AND TB_ADS.Change = 1";
            }


            SqlConnection sqlConnection = new SqlConnection(SqlUtils.GetInstance().conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTableCars = new DataTable();
            sqlDataAdapter.Fill(dataTableCars);
            grdCntrlADS.DataSource = dataTableCars;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetSearch();
        }

        private void GetRefresh()
        {
            string query = $@"select 
                    (SELECT top(1) IMG.CAR_IMAGE FROM Car_Image IMG WHERE IMG.ADS_ID =ID) CAR_IMAGE,
                    CB.ID,
                    CM.ID,
                    GI.ID,
                    GIY.ID,
                    TB_ADS.Change,
                    TB_ADS.Credit,
                    TB_ADS.Amount_ID,   
                    TB_ADS.Years_ID 
                                    from TB_ADS
                    inner join Car_Model as CM on CM.ID=TB_ADS.Model_ID
                    inner join Car_Brand as CB on CB.ID=CM.Brand_ID
                    inner join General_Info as GI on GI.ID=TB_ADS.Cities_ID
                    inner join General_Info as GIY on GIY.ID=TB_ADS.Currency_ID
                ";
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.GetInstance().conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTableCars = new DataTable();
            sqlDataAdapter.Fill(dataTableCars);
            grdCntrlADS.DataSource = dataTableCars;
        }
    }
        
}
