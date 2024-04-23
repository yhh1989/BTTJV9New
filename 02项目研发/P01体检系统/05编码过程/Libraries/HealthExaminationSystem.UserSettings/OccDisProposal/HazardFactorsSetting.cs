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
using Sw.Hospital.HealthExaminationSystem.Application.WorkType;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposal
{
    public partial class HazardFactorsSetting : UserBaseForm
    {
        public HazardFactorsSetting()
        {
            riskFactorAppService = new RiskFactorAppService();
            workTypeAppService = new WorkTypeAppService();
            postStateAppService = new PostStateAppService();
            InitializeComponent();
        }
        #region 系统变量
        IRiskFactorAppService riskFactorAppService;
        IWorkTypeAppService workTypeAppService;
        IPostStateAppService postStateAppService;
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
            txtRiskName.Text = oRiskFactorDto.Name;
            //txtRiskCode.Text = oRiskFactorDto.RiskCode;
            txtRiskNameJP.Text = oRiskFactorDto.HelpChar;
            //lookUpEditGangWeiType.EditValue = oRiskFactorDto.JobCategoryId;
            lookUpEditGongZhong.EditValue = oRiskFactorDto.WorkTypeId;
            txtFHCS.Text = oRiskFactorDto.ProtectiveMeasures;
            txtJSSM.Text = oRiskFactorDto.Describe;
            txtJCZL.Text = oRiskFactorDto.Data;
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
            if (!groupRisk.Enabled)
                return;
            if (strtype == "新建")
            {
                ORiskFactorDto oRiskFactorDto = new ORiskFactorDto();
                oRiskFactorDto.Name = txtRiskName.Text;
                if (lookUpEditGongZhong.EditValue != null)
                {
                    oRiskFactorDto.WorkTypeId = Guid.Parse(lookUpEditGongZhong.EditValue.ToString());
                }
                oRiskFactorDto.HelpChar = txtRiskNameJP.Text;
                if (lookUpEditGangWeiType.EditValue != null)
                {
                    //oRiskFactorDto.JobCategoryId = Guid.Parse(lookUpEditGangWeiType.EditValue.ToString());
                }
                oRiskFactorDto.ProtectiveMeasures = txtFHCS.Text;
                oRiskFactorDto.Describe = txtJSSM.Text;
                oRiskFactorDto.Data = txtJCZL.Text;
                riskFactorAppService.Insert(oRiskFactorDto);
            }
            else
            {
                ORiskFactorDto oRiskFactorDto = grdData.GetFocusedRowDto<ORiskFactorDto>();
                oRiskFactorDto.Name = txtRiskName.Text;
                if (lookUpEditGongZhong.EditValue != null)
                {
                    oRiskFactorDto.WorkTypeId = Guid.Parse(lookUpEditGongZhong.EditValue.ToString());
                }
                oRiskFactorDto.HelpChar = txtRiskNameJP.Text;
                if (lookUpEditGangWeiType.EditValue != null)
                {
                    //oRiskFactorDto.JobCategoryId = Guid.Parse(lookUpEditGangWeiType.EditValue.ToString());
                }
                oRiskFactorDto.ProtectiveMeasures = txtFHCS.Text;
                oRiskFactorDto.Describe = txtJSSM.Text;
                oRiskFactorDto.Data = txtJCZL.Text;
                riskFactorAppService.Edit(oRiskFactorDto);
            }
            txtrisk.Text = "";
            groupRisk.Enabled = false;
            ClearWindow();
            BindData();
        }
        #endregion

        #region 删除
        private void btndel_Click(object sender, EventArgs e)
        {
            ORiskFactorDto oRiskFactorDto = grdData.GetFocusedRowDto<ORiskFactorDto>();
            riskFactorAppService.Delete(oRiskFactorDto);
            txtrisk.Text = "";
            lookUpEditGangWeiType.EditValue = -1;
            lookUpEditGongZhong.EditValue = -1;
            BindData();
            
        }
        #endregion
        #region 生成简码
        private void txtRiskName_Leave(object sender, EventArgs e)
        {

            var name = txtRiskName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = common.GetHansBrief(new ChineseDto { Hans = name });
                    txtRiskNameJP.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                txtRiskNameJP.Text = string.Empty;
            }
        }
        #endregion
        #endregion

        #region 公共方法
        #region 初始化下拉框
        private void BindingData()
        {
            var GongZhong = workTypeAppService.Query(new Application.WorkType.Dto.WorkTypeDto() { IsActive = true, Category = 1 });
            lookUpEditGongZhong.Properties.DataSource = GongZhong;
            lookUpEditGongZhong.Properties.ValueMember = "Id";   //相当于editvalue
            lookUpEditGongZhong.Properties.DisplayMember = "Name";    //相当于text
            lookUpEditGongZhong.EditValue = -1;
            var GangWei = postStateAppService.GetAll(new Application.OccupationalDiseases.WorkType.Dto.JobCategoryDto() { IsActive = true });
            lookUpEditGangWeiType.Properties.DataSource = GangWei;
            lookUpEditGangWeiType.Properties.ValueMember = "Id";   //相当于editvalue
            lookUpEditGangWeiType.Properties.DisplayMember = "Name";    //相当于text
            lookUpEditGangWeiType.EditValue = -1;
            groupRisk.Enabled = false;

        }
        #endregion

        #region 绑定列表
        public void BindData()
        {
            if (string.IsNullOrWhiteSpace(txtrisk.Text.Trim()))
            {
                grdData.DataSource = riskFactorAppService.GetAll();
            }
            else
            {
                ORiskFactorDto oRisk = new ORiskFactorDto();
                oRisk.Name = txtrisk.Text;
                grdData.DataSource=riskFactorAppService.Get(oRisk);
            }
           
        }

        #endregion

        #region 初始化数据
        public void ClearWindow()
        {
            txtRiskName.Text = "";
            txtRiskName.Text = "";
            txtRiskNameJP.Text = "";
            lookUpEditGangWeiType.EditValue = -1;
            lookUpEditGongZhong.EditValue = -1;
            txtFHCS.Text = "";
            txtJSSM.Text = "";
            txtJCZL.Text = "";
        }

        #endregion

        #endregion

        
    }
}