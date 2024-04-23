using Sw.Hospital.HealthExaminationSystem.ApiProxy.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusRegStatis
{
    public partial class frmRegList : UserBaseForm
    {
        public IFrmMakeCollectAppService frmMakeCollectAppService = new FrmMakeCollectAppService();

        public frmRegList()
        {
            InitializeComponent();
        }

        private void frmRegList_Load(object sender, EventArgs e)
        {
            var clienttype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.ClientSource).ToString())?.ToList();
            lookRegType.Properties.DataSource = clienttype;
            lookClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            repositoryItemLookUpEdit2.DataSource = clienttype;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InIdDto InseachCusReg = new InIdDto();
            if (!string.IsNullOrEmpty(dateEditStartTime.EditValue?.ToString()))
            {
                InseachCusReg.BookingStar = dateEditStartTime.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditEndTime.EditValue?.ToString()))
            {
                InseachCusReg.BookingEnd = dateEditEndTime.DateTime;
            }
            if (!string.IsNullOrEmpty(textCustomerBM.EditValue?.ToString()))
            {
                InseachCusReg.customerBM = textCustomerBM.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEditName.EditValue?.ToString()))
            {
                InseachCusReg.SearchName = textEditName.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEditIDCard.EditValue?.ToString()))
            {
                InseachCusReg.IdCard = textEditIDCard.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEditMoblie.EditValue?.ToString()))
            {
                InseachCusReg.Moblie = textEditMoblie.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEditOrderNum.EditValue?.ToString()))
            {
                InseachCusReg.OrderNum = textEditOrderNum.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(textEdituser.EditValue?.ToString()))
            {
                InseachCusReg.UserName = textEdituser.EditValue.ToString();
            }
            //if (lookUpState.SelectedIndex > 0)
            //{
            //    InseachCusReg.RegState = lookUpState.SelectedIndex;
            //}
            if (lookRegType.EditValue != null)
            {
                InseachCusReg.RegType = (int)lookRegType.EditValue;
            }
          
            var departlist = frmMakeCollectAppService.getRegCusLis(InseachCusReg);
            gridControl2.DataSource = departlist;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridView1.CustomExport("预约查询");
         
        }
    }
}
