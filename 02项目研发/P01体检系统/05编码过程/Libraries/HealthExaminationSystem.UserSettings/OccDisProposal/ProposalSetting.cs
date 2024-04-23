using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.ORiskFactor;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OConDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    public partial class ProposalSetting : UserBaseForm
    {
        public ProposalSetting()
        {
            riskFactorAppService = new OConDictionaryAppService();
            riskFactorAppServicevs = new RiskFactorAppService();
            InitializeComponent();
        }
        #region 系统变量
        IOConDictionaryAppService riskFactorAppService;
        IRiskFactorAppService riskFactorAppServicevs;
        private readonly ICommonAppService common = new CommonAppService();
        string strtype = "修改";
        #endregion

        #region 系统事件
        #region 系统加载
        private void HazardFactorsSetting_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsView.ShowIndicator = false; //不显示指示器
            gridView1.OptionsBehavior.ReadOnly = false;
            gridView1.OptionsBehavior.Editable = false;
            BindingData();
            BindData();

            groupRisk.Enabled = false;
        }


        #endregion

        #region 新建
        private void btnadd_Click(object sender, EventArgs e)
        {
            groupRisk.Enabled = true;
            strtype = "新建";
            ClearWindow();
        }
        #endregion

        #region 查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        } 
        #endregion
        #region 单击显示
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ORiskFactorDto oRiskFactorDto = grdData.GetFocusedRowDto<ORiskFactorDto>();
            //txtRiskName.Text = oRiskFactorDto.RiskName;
            //txtRiskCode.Text = oRiskFactorDto.RiskCode;
            //txtRiskNameJP.Text = oRiskFactorDto.RiskNameJP;
            //txtRiskTypeIDs.EditValue = oRiskFactorDto.RiskTypeIDs;
            txtSymptom.Text = oRiskFactorDto.ProtectiveMeasures;
            txtZyContents.Text = oRiskFactorDto.Describe;
            txtZyjjContents.Text = oRiskFactorDto.Data;
        }
        #endregion
        #region 修改
        private void btnend_Click(object sender, EventArgs e)
        {
            strtype = "修改";
            groupRisk.Enabled = true;
        }

        #endregion
        #region 保存
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (strtype == "新建")
            {
                OConDictionaryDto oRiskFactorDto = new OConDictionaryDto();
                oRiskFactorDto.OPostStates = txtOPostStates.Text;
                oRiskFactorDto.ZyContents = txtZyContents.Text;
                oRiskFactorDto.ZyjjContents = txtZyjjContents.Text;
                oRiskFactorDto.Symptom = txtSymptom.Text;
                oRiskFactorDto.RiskNames = txtRiskNames.Text;
                riskFactorAppService.Insert(oRiskFactorDto);
            }
            else
            {
                OConDictionaryDto oRiskFactorDto = grdData.GetFocusedRowDto <OConDictionaryDto>();
                oRiskFactorDto.OPostStates = txtOPostStates.Text;
                oRiskFactorDto.ZyContents = txtZyContents.Text;
                oRiskFactorDto.ZyjjContents = txtZyjjContents.Text;
                oRiskFactorDto.Symptom = txtSymptom.Text;
                oRiskFactorDto.RiskNames = txtRiskNames.Text;
                riskFactorAppService.Edit(oRiskFactorDto);
            }
            txtrisk.Text = "";
            lookRiskTypeIDs.EditValue = -1;
            BindData();
        }
        #endregion

        #region 删除
        private void btndel_Click(object sender, EventArgs e)
        {
            OConDictionaryDto oRiskFactorDto = grdData.GetFocusedRowDto<OConDictionaryDto>();
            riskFactorAppService.Delete(oRiskFactorDto);
            txtrisk.Text = "";
            lookRiskTypeIDs.EditValue = -1;
            BindData();
            
        }
        #endregion
        #region 生成简码
        private void txtRiskName_Leave(object sender, EventArgs e)
        {

            var name = "";// txtRiskName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = common.GetHansBrief(new ChineseDto { Hans = name });
                    //txtRiskNameJP.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
               // txtRiskNameJP.Text = string.Empty;
            }
        }
        #endregion
        #endregion

        #region 公共方法
        #region 初始化下拉框
        private void BindingData()
        {
            List<BasicDictionaryDto> lstbasicDictionaryDtos = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.HazardCategory);
            lookRiskTypeIDs.Properties.DataSource = lstbasicDictionaryDtos;
            lookRiskTypeIDs.Properties.ValueMember = "Text";   //相当于editvalue
            lookRiskTypeIDs.Properties.DisplayMember = "Text";    //相当于text
            lookRiskTypeIDs.EditValue = -1;

            txtOPostStates.Properties.DataSource = lstbasicDictionaryDtos;
            txtOPostStates.Properties.ValueMember = "Text";   //相当于editvalue
            txtOPostStates.Properties.DisplayMember = "Text";    //相当于text
            txtOPostStates.EditValue = -1;

            List<ORiskFactorDto> lstoRiskFactors = riskFactorAppServicevs.GetAll(); ;
            lookRiskTypeIDs.Properties.DataSource = lstbasicDictionaryDtos;
            lookRiskTypeIDs.Properties.ValueMember = "RiskName";   //相当于editvalue
            lookRiskTypeIDs.Properties.DisplayMember = "RiskName";    //相当于text
            lookRiskTypeIDs.EditValue = -1;

            //txtRiskTypeIDs.Properties.DataSource = lstbasicDictionaryDtos;
            //txtRiskTypeIDs.Properties.ValueMember = "Text";   //相当于editvalue
            //txtRiskTypeIDs.Properties.DisplayMember = "Text";    //相当于text
            //txtRiskTypeIDs.EditValue = -1;
            groupRisk.Enabled = false;

        }
        #endregion

        #region 绑定列表
        public void BindData()
        {
            if (txtrisk.Text == "" &&lookRiskTypeIDs.Text== "[编辑值为空]")
            {
                grdData.DataSource = riskFactorAppService.GetAll();
            }
            else
            {
                OConDictionaryDto oRisk = new OConDictionaryDto();
                oRisk.RiskNames = txtrisk.Text;
                oRisk.OPostStates = lookRiskTypeIDs.Text;
                grdData.DataSource=riskFactorAppService.Get(oRisk);
            }
           
        }

        #endregion

        #region 初始化数据
        public void ClearWindow()
        {
            txtRiskNames.Text = "";
            txtOPostStates.Text = "";
            txtSymptom.Text = "";
            txtZyContents.Text = "";
            txtZyjjContents.Text = "";
        }

        #endregion

        #endregion

        
    }
}