using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Card;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TurboAz.Class;
using TurboAz.Util;

namespace TurboAz
{
    public partial class frmAddCars : Form
    {
        ClssInfoAdapter clssInfoAdapter = new ClssInfoAdapter();
        GeneralMethod generalMethod = new GeneralMethod();
        public frmAddCars()
        {
            InitializeComponent();
        }

        private void frmAddCars_Load(object sender, EventArgs e)
        {
            generalMethod.SetBrandData(lkpCarBrand);

            generalMethod.SetGeneralInfo(lkpCarType, "1");
            generalMethod.SetGeneralInfo(lkpColor, "2");
            generalMethod.SetGeneralInfo(lkpFuel, "4");
            generalMethod.SetGeneralInfo(lkpGear, "5");
            generalMethod.SetGeneralInfo(lkpGearBox, "6");
            generalMethod.SetGeneralInfo(lkpCities, "7");
            generalMethod.SetGeneralInfo(lkpCapacity, "8");
            generalMethod.SetYears(lkpProductYear);
            gridCntrlImages.DataSource = clssInfoAdapter.GetImage("-1");
        }

        private CardView GetcrdViewImage()
        {
            return crdVwImages;
        }

        private void lkpCarBrand_EditValueChanged(object sender, EventArgs e)
        {
            generalMethod.SetModelData(lkpCarModel, lkpCarBrand.EditValue.ToString());
        }




        private bool ControlComponentEmpty()
        {
            bool control = true;

            if (lkpCarBrand.EditValue == null)
            {
                lkpCarBrand.ErrorText = "Marka seçin";
                control = false;
            }

            if (lkpCarModel.EditValue == null)
            {
                lkpCarModel.ErrorText = "Model seçin";
                control = false;
            }
            if (lkpCarType.EditValue == null)
            {
                lkpCarType.ErrorText = "Ban seçin";
                control = false;
            }
            if (nmUpDownAmount.Value == 0)
            {
                MessageBox.Show("Qiyməti  doldurun");
                control = false;
            }
            if (nmUpDownKm.Value == 0)
            {
                MessageBox.Show("Yürüşü  doldurun");
                control = false;
            }
            if (rdgCurrency.EditValue == null)
            {
                rdgCurrency.ErrorText = "Valiyutanı seçin";
                control = false;
            }

            if (lkpColor.EditValue == null)
            {
                lkpColor.ErrorText = "Rəngi seçin";
                control = false;
            }
            if (lkpFuel.EditValue == null)
            {
                lkpFuel.ErrorText = "Yanacağı seçin";
                control = false;
            }

            if (lkpGear.EditValue == null)
            {
                lkpGear.ErrorText = "Ötürücünü seçin";
                control = false;
            }

            if (lkpGearBox.EditValue == null)
            {
                lkpGearBox.ErrorText = "Sürət qutusunu seçin";
                control = false;
            }
            if (lkpProductYear.EditValue == null)
            {
                lkpProductYear.ErrorText = "Buraxılış ilini seçin";
                control = false;
            }
            if (lkpCapacity.EditValue == null)
            {
                lkpCapacity.ErrorText = "Mühərrik həcmini seçin";
                control = false;
            }

            if (nmUpDownHp.Value == 0)
            {
                MessageBox.Show("Mühərrik gücünü doldurun");
                control = false;
            }
            if (txtName.EditValue == null)
            {
                txtName.ErrorText = "Adınızı daxil edin";
                control = false;
            }

            if (lkpCities.EditValue == null)
            {
                lkpCities.ErrorText = "Şəhərinizi seçin";
                control = false;
            }
            if (crdVwImages.DataRowCount < 3)
            {
                MessageBox.Show("Ən az 3 şəkil əlavə edin");
                control = false;
            }

            return control;
        }



        private void btnAddAds_Click(object sender, EventArgs e)
        {
            if (ControlComponentEmpty())
            {
                if (MessageBox.Show("Elanı yerləşdirmək istediyinizə əminsiniz?", "Sual", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    InsertAllInfo();

                }

            }
        }
        private void InsertAllInfo()
        {
            SqlTransaction sqlTransaction = null;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(SqlUtils.GetInstance().conString);
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                string insertedID = InsertAds(sqlTransaction);
                InsertAdsImage(sqlTransaction, insertedID);
                sqlTransaction.Commit();
                sqlConnection.Close();

                MessageBox.Show("Melumat yadda saxlanildi!");
                this.Close();
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                MessageBox.Show("Elan yerləşdirmə zamanı xəta baş verdi" + ex.Message);

            }

        }


        private string InsertAds(SqlTransaction sqlTransaction)
        {
            string query = @"INSERT INTO [dbo].[TB_ADS]
           ([Model_ID]
           ,[CarType_ID]
           ,[Km_ID]
           ,[Color_ID]
           ,[Amount_ID]
           ,[Currency_ID]
           ,[Credit]
           ,[Change]
           ,[Fuel_ID]
           ,[Gear_ID]
           ,[Gearbox_ID]
           ,[Years_ID]
           ,[Capasity_ID]
           ,[Horsepower_ID]
           ,[EditText_ID]
           ,[Wheel]
           ,[ABS]
           ,[Centrallocking]
           ,[Skin]
           ,[Ventilation]
           ,[ParkRadar]
           ,[XenonLamp]
           ,[Hatch]
           ,[AirConditioners]
           ,[BackCamera]
           ,[RainSensor]
           ,[SeatHeating]
           ,[Curtain]
           ,[Name]
           ,[Cities_ID]
           ,[Email])
     VALUES
           (@Model_ID
           ,@CarType_ID
           ,@Km_ID
           ,@Color_ID
           ,@Amount_ID
           ,@Currency_ID
           ,@Credit
           ,@Change
           ,@Fuel_ID
           ,@Gear_ID
           ,@Gearbox_ID
           ,@Years_ID
           ,@Capasity_ID
           ,@Horsepower_ID
           ,@EditText_ID
           ,@Wheel
           ,@ABS
           ,@Centrallocking
           ,@Skin
           ,@Ventilation
           ,@ParkRadar
           ,@XenonLamp
           ,@Hatch
           ,@AirConditioners
           ,@BackCamera
           ,@RainSensor
           ,@SeatHeating
           ,@Curtain
           ,@Name
           ,@Cities_ID
           ,@Email ); SELECT SCOPE_IDENTITY();";


            SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.Add("Model_ID", SqlDbType.Int).Value = lkpCarModel.EditValue;
            sqlCommand.Parameters.Add("CarType_ID", SqlDbType.Int).Value = lkpCarType.EditValue;
            sqlCommand.Parameters.Add("Color_ID", SqlDbType.Int).Value = lkpColor.EditValue;
            sqlCommand.Parameters.Add("Currency_ID", SqlDbType.Int).Value = rdgCurrency.EditValue;
            sqlCommand.Parameters.Add("Fuel_ID", SqlDbType.Int).Value = lkpFuel.EditValue;
            sqlCommand.Parameters.Add("Gear_ID", SqlDbType.Int).Value = lkpGear.EditValue;
            sqlCommand.Parameters.Add("Gearbox_ID", SqlDbType.Int).Value = lkpGearBox.EditValue;
            sqlCommand.Parameters.Add("Years_ID", SqlDbType.Int).Value = lkpProductYear.EditValue;
            sqlCommand.Parameters.Add("Capasity_ID", SqlDbType.Int).Value = lkpCapacity.EditValue;
            sqlCommand.Parameters.Add("Km_ID", SqlDbType.Int).Value = nmUpDownKm.Value;
            sqlCommand.Parameters.Add("Amount_ID", SqlDbType.Int).Value = nmUpDownAmount.Value;
            sqlCommand.Parameters.Add("Horsepower_ID", SqlDbType.Int).Value = nmUpDownHp.Value;
            sqlCommand.Parameters.Add("EditText_ID", SqlDbType.NVarChar).Value = mmEditText.EditValue;
            sqlCommand.Parameters.Add("Credit", SqlDbType.Bit).Value = chcCredit.Checked;
            sqlCommand.Parameters.Add("Change", SqlDbType.Bit).Value = chcChange.Checked;
            sqlCommand.Parameters.Add("Wheel", SqlDbType.Bit).Value = chcWheel.Checked;
            sqlCommand.Parameters.Add("ABS", SqlDbType.Bit).Value = chcABS.Checked;
            sqlCommand.Parameters.Add("Centrallocking", SqlDbType.Bit).Value = chcCentralLocking.Checked;
            sqlCommand.Parameters.Add("Skin", SqlDbType.Bit).Value = chcSkin.Checked;
            sqlCommand.Parameters.Add("Ventilation", SqlDbType.Bit).Value = chcVentilation.Checked;
            sqlCommand.Parameters.Add("ParkRadar", SqlDbType.Bit).Value = chcParkRadar.Checked;
            sqlCommand.Parameters.Add("XenonLamp", SqlDbType.Bit).Value = chcXenonLamp.Checked;
            sqlCommand.Parameters.Add("Hatch", SqlDbType.Bit).Value = chcHatch.Checked;
            sqlCommand.Parameters.Add("AirConditioners", SqlDbType.Bit).Value = chcAirConditioners.Checked;
            sqlCommand.Parameters.Add("BackCamera", SqlDbType.Bit).Value = chcBackCamera.Checked;
            sqlCommand.Parameters.Add("RainSensor", SqlDbType.Bit).Value = chcRainSensor.Checked;
            sqlCommand.Parameters.Add("SeatHeating", SqlDbType.Bit).Value = chcSeatHeating.Checked;
            sqlCommand.Parameters.Add("Curtain", SqlDbType.Bit).Value = chcCurtain.Checked;
            sqlCommand.Parameters.Add("Name", SqlDbType.NVarChar).Value = txtName.EditValue;
            sqlCommand.Parameters.Add("Cities_ID", SqlDbType.Int).Value = lkpCities.EditValue;
            sqlCommand.Parameters.Add("Email", SqlDbType.NVarChar).Value = txtEmail.EditValue;
            return sqlCommand.ExecuteScalar().ToString();
        }
        private void InsertAdsImage(SqlTransaction sqlTransaction, string adsID)
        {
            DataTable dtTableImage = (DataTable)gridCntrlImages.DataSource;
            for (int i = 0; i < dtTableImage.Rows.Count; i++)
            {
                DataRow dtRowImage = dtTableImage.Rows[i];
                string query = @"INSERT INTO [dbo].[Car_Image]
                        (Car_Image,ADS_ID)
            VALUES
                        (@Car_Image,@ADS_ID)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlTransaction.Connection);
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Add("Car_Image", SqlDbType.VarBinary).Value = dtRowImage["Car_Image"];
                sqlCommand.Parameters.Add("ADS_ID", SqlDbType.Int).Value = adsID;
                sqlCommand.ExecuteNonQuery();
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupCntrImage_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {


            if (e.Button == groupCntrImage.CustomHeaderButtons[0])
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image file|*.jpg;*.png;*.jpeg;";
                DataTable dtTableImage = (DataTable)gridCntrlImages.DataSource;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        dtTableImage.Rows.Add(0, GetImage(fileName));
                        GetImage(fileName);
                    }

                }
                gridCntrlImages.Refresh();
            }

        }
        private byte[] GetImage(string fileName)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            imageByteArray = binaryReader.ReadBytes((int)fileStream.Length);
            binaryReader.Close();
            fileStream.Close();
            return imageByteArray;

        }

    }
}
