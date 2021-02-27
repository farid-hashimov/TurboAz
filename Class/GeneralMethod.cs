using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Class
{
    class GeneralMethod
    {
        ClssInfoAdapter clssInfoAdapter = new ClssInfoAdapter();

        public void SetGeneralInfo(LookUpEdit lkpGenInfo, string typeID)
        {
            lkpGenInfo.Properties.DataSource = clssInfoAdapter.GetGeneralType(typeID);
            lkpGenInfo.Properties.DisplayMember = "Name";
            lkpGenInfo.Properties.ValueMember = "ID";
        }
        public void SetYears(LookUpEdit lkpProductYear)
        {
            List<int> listYears = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 1960; i <= currentYear; i++)
            {
                listYears.Add(i);
            }

            lkpProductYear.Properties.DataSource = listYears;


        }
        public void SetBrandData(LookUpEdit lkpCarBrand)
        {
            lkpCarBrand.Properties.DataSource = clssInfoAdapter.GetBrands();
            lkpCarBrand.Properties.DisplayMember = "Brand_Name";
            lkpCarBrand.Properties.ValueMember = "ID";
        }

        public void SetModelData(LookUpEdit lkpCarModel, string brandID)
        {
            lkpCarModel.Properties.DataSource = clssInfoAdapter.GetModels(brandID);
            lkpCarModel.Properties.DisplayMember = "Model_Name";
            lkpCarModel.Properties.ValueMember = "ID";
        }

    }
}
