using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ClearData;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ClearData
{
    public partial class frmCheck :UserBaseForm
    {
        private readonly ClearDataAppService _clearDataAppService;
        public bool isUser = false;
        public bool isInterfase = false;
        public bool isSuit=false;
        public bool isAbpLog = false;
        public bool isLog = false;
        public bool isTjl = false;
        public bool isAD = false;
        public bool isBar = false;


        public frmCheck()
        {
            _clearDataAppService = new ClearDataAppService();
            InitializeComponent();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (checkUser.Checked == true)
            {
                isUser = true;
            }
            if (checkInterface.Checked == true)
            {
                isInterfase = true;
            }
            if (checkSuit.Checked == true)
            {
                isSuit = true;
            }           
            if (checkabpLog.Checked == true)
            {
                isAbpLog = true;
            }
            if (checkLog.Checked == true)
            {
                isLog = true;
            }
            if (checkTjl.Checked == true)
            {
                isTjl = true;
            }
            if (checkAd.Checked == true)
            {
                isAD = true;
            }
            if (checkBar.Checked==true)
            {
                isBar = true;
            }
            DialogResult dr = XtraMessageBox.Show("确认初始化已选择的数据么？初始化后数据将无法恢复，请谨慎操作！", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                InputClearData inputClearData = new InputClearData();
                inputClearData.isAbpLog = isAbpLog;
                inputClearData.isInterfase = isInterfase;
                inputClearData.isLog = isLog;
                inputClearData.isSuit = isSuit;
                inputClearData.isTjl = isTjl;
                inputClearData.isUser = isUser;
                inputClearData.isAdVice = isAD;
                inputClearData.isBar = isBar;
                var result = _clearDataAppService.AllDeleteData(inputClearData);
                if (result.Leixing > 0)
                {
                    MessageBox.Show("删除成功");
                }
            }
           // this.DialogResult = DialogResult.OK;

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {

        }
    }
}
