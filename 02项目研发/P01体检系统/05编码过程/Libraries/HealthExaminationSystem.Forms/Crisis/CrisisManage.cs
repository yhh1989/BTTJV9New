using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{

    public partial class CrisisManage  : UserBaseForm
    {
        private CrisisCustomerDto CrisisCustomerDto;
        private CrisisLoad CrisisLoad;
        private readonly Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService _crisisappservice;
        public event SendEventHandlers SendEvents;
        public CrisisManage(CrisisLoad CrisisLoad)
        {
            _crisisappservice = new Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis.CrisisAppService();
            InitializeComponent();
            this.CrisisLoad = CrisisLoad;
            this.CrisisLoad.SendEvents += new SendEventHandlers(CrisisLoad_SendEvent);
        }


        private void CrisisManage_Load(object sender, EventArgs e)
        {
            //绑定诊断搜索下拉框
            var userDto = DefinedCacheHelper.GetComboUsers();
            txtsearchP.Properties.DataSource = userDto;
            txtsearchP.Properties.ValueMember = "Id";
            txtsearchP.Properties.DisplayMember = "Name";
            txtsearchP.EditValue = CurrentUser.Id;
        }

        void CrisisLoad_SendEvent(CrisisCustomerDto CrisisCustomerDto)
        {
            label3.Text = CrisisCustomerDto.CustomerBM;
            label7.Text = CrisisCustomerDto.Age.ToString();
            label6.Text = CrisisCustomerDto.Sexs;
            label8.Text = CrisisCustomerDto.ItemName;
            label10.Text = CrisisCustomerDto.ItemResultChar;
            label12.Text = CrisisCustomerDto.Name;
            label14.Text = CrisisCustomerDto.Stand;
            this.CrisisCustomerDto = CrisisCustomerDto;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.CrisisCustomerDto.CallBacKContent = richTextBox1.Text;
            this.CrisisCustomerDto.CallBacKUserId = (Int64)txtsearchP.EditValue;
            this.CrisisCustomerDto.CallBacKDate = DateTime.Now;
            var result=_crisisappservice.UpdateCrisis(this.CrisisCustomerDto);
            if (!string.IsNullOrEmpty(result.CustomerRegId.ToString()))
            {
                this.Close();
                //页面加载查询危急值
                var resultls = _crisisappservice.GetCrisisCustome().ToList();
                if (resultls.Count > 0)
                {
                    
                    CrisisLoad crisisLoad = new CrisisLoad(resultls.FirstOrDefault());                    
                    crisisLoad.ShowDialog();                    
                   
                }
            }
            else
            {
                MessageBox.Show("程序错误，请重试！！");
            }
            


        }

        private void txtsearchP_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
