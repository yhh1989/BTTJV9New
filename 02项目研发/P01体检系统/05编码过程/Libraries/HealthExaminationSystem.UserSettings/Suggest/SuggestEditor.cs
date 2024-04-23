using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest
{
    public partial class SummarizeAdviceEdit : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService service = new SummarizeAdviceAppService();
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        private IIDNumberAppService iIDNumberAppService =new IDNumberAppService();
        public FullSummarizeAdviceDto _Model { get; private set; }
        /// <summary>
        /// 新增
        /// </summary>
        public SummarizeAdviceEdit()
        {
            InitializeComponent();
            if (_Model == null) _Model = new FullSummarizeAdviceDto();

            lueSexState.SetClearButton();
            lueMarrySate.SetClearButton();
            lueDiagnosisAType.SetClearButton();
            lueSexState.EditValue = (int)Sex.GenderNotSpecified;
            lueMarrySate.EditValue = (int)MarrySate.Unstated;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public SummarizeAdviceEdit(FullSummarizeAdviceDto model) : this()
        {
            _Model = model;
            LoadData();
        }
        /// <summary>
        /// 总检新增
        /// </summary>
        /// <param name="model"></param>
        public SummarizeAdviceEdit(string advice,string adContent) : this()
        {
            teAdviceName.Text = advice;

            teAdvicevalue.Text = advice;
            meSummAdvice.Text = adContent;

            teUid.Text = iIDNumberAppService.CreateAdviceBM();
        }

        #region 事件
        private void SummarizeAdviceEdit_Load(object sender, EventArgs e)
        {
            LoadControlData();
        }
        private void sbOk_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void ceDiagnosisSate_CheckedChanged(object sender, EventArgs e)
        {
            lueDiagnosisAType.Enabled = ceDiagnosisSate.Checked;
            teDiagnosisExpain.Enabled = ceDiagnosisSate.Checked;
        }

        // 助记码
        private void teAdviceName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teHelpChar.Text))
                return;
            var name = teAdviceName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    teHelpChar.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                teHelpChar.Text = string.Empty;
            }
        }

        private void linkReloadDropdown_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                lueDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments();
                lueDiagnosisAType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.DiagnosisAType);
            });
        }
        #endregion

        #region 处理

        /// <summary>
        /// 加载控件数据
        /// </summary>
        private void LoadControlData()
        {
            var dept=  DefinedCacheHelper.GetDepartments();
            lueDepartment.Properties.DataSource = dept; 
            lueSexState.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lueMarrySate.Properties.DataSource = MarrySateHelper.GetMarrySateModelsForItemInfo();
            lueDiagnosisAType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.DiagnosisAType);
            
        }
        
        /// <summary>
        /// 确定
        /// </summary>
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            // 编码
            _Model.Uid = teUid.Text.Trim();
            if (string.IsNullOrEmpty(_Model.Uid))
            {
                //dxErrorProvider.SetError(teUid, string.Format(Variables.MandatoryTips, "编码"));
                //teUid.Focus();
                //return false;
                _Model.Uid  = iIDNumberAppService.CreateAdviceBM();

            }
            // 科室
            _Model.DepartmentId = lueDepartment.EditValue == null ? Guid.Empty : (Guid)lueDepartment.EditValue;
            if (_Model.DepartmentId != Guid.Empty)
            {
                _Model.DepartmentId = (Guid)lueDepartment.EditValue;
                var  deptdto= (TbmDepartmentDto)lueDepartment.GetSelectedDataRow();
                
                _Model.Department = ModelHelper.CustomMapTo2<TbmDepartmentDto, DepartmentSimpleDto>(deptdto);
            }
            else
            {
                dxErrorProvider.SetError(lueDepartment, string.Format(Variables.MandatoryTips, "科室"));
                lueDepartment.Focus();
                return false;
            }
            // 建议名称
            _Model.AdviceName = teAdviceName.Text.Trim();
            if (string.IsNullOrEmpty(_Model.AdviceName))
            {
                dxErrorProvider.SetError(teAdviceName, string.Format(Variables.MandatoryTips, "建议名称"));
                teAdviceName.Focus();
                return false;
            }

            // 总检建议
            _Model.SummAdvice = meSummAdvice.Text;
            if (string.IsNullOrWhiteSpace(_Model.SummAdvice))
            {
                dxErrorProvider.SetError(meSummAdvice, string.Format(Variables.MandatoryTips, "建议内容"));
                xtraTabPage1.Select(); // 激活tab页
                meSummAdvice.Focus();
                return false;
            }

            // 助记码
            _Model.HelpChar = teHelpChar.Text.Trim();
            // 建议依据
            _Model.Advicevalue = teAdvicevalue.Text.Trim();
            // 阳性状态 1阳性2正常
            _Model.SummState = ceSummState.Checked ? 1 : 2;
            // 危急值状态 1危急值2正常
            _Model.CrisisSate = ceCrisisSate.Checked ? 1 : 2;
            // 团报隐藏 1隐藏2显示
            _Model.HideOnGroupReport = ceHideOnGroupReport.Checked ? 1 : 2;

            // 疾病状态 1疾病2正常
            _Model.DiagnosisSate = ceDiagnosisSate.Checked ? 1 : 2;
            // 疾病介绍
            _Model.DiagnosisExpain = teDiagnosisExpain.Text;
            // 疾病类别 字典
            _Model.DiagnosisAType = (int?)lueDiagnosisAType.EditValue;

            // 专科建议
            _Model.DepartmentAdvice = meDepartmentAdvice.Text;
            // 团体建议
            _Model.ClientAdvice = meClientAdvice.Text;
            // 饮食指导
            _Model.DietGuide = meDietGuide.Text;
            // 运动指导
            _Model.SportGuide = meSportGuide.Text;
            // 健康指导
            _Model.Knowledge = meKnowledge.Text;
            // 健康建议
            _Model.HealthcareAdvice = meHealthcareAdvice.Text;
            // 适用性别
            _Model.SexState = (int?)lueSexState.EditValue;
            // 适用婚别
            _Model.MarrySate = (int?)lueMarrySate.EditValue;
            // 最小年龄
            _Model.MinAge = int.TryParse(teMinAge.Text.Trim(), out int minAge) ? (int?)minAge : null;
            // 最大年龄
            _Model.MaxAge = int.TryParse(teMaxAge.Text.Trim(), out int maxAge) ? (int?)maxAge : null;

            bool res = false;
            AutoLoading(() =>
            {
                SummarizeAdviceInput input = new SummarizeAdviceInput()
                {
                    SummarizeAdvice = ModelHelper.CustomMapTo2<FullSummarizeAdviceDto, CreateOrUpdateSummarizeAdviceDto>(_Model),
                };
                FullSummarizeAdviceDto dto = null;
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                }
                else
                {
                    dto = service.Edit(input);
                }
                dto.Department = _Model.Department;
                _Model = dto;
                res = true;
            });
            return res;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (_Model == null)
            {
                return;
            }
            lueDepartment.EditValue = _Model.DepartmentId;
            teUid.Text = _Model.Uid;
            teAdviceName.Text = _Model.AdviceName;
            teHelpChar.Text = _Model.HelpChar;
            teAdvicevalue.Text = _Model.Advicevalue;
            ceSummState.Checked = _Model.SummState == 1;
            ceCrisisSate.Checked = _Model.CrisisSate == 1;
            ceHideOnGroupReport.Checked = _Model.HideOnGroupReport == 1;
            ceDiagnosisSate.Checked = _Model.DiagnosisSate == 1;
            teDiagnosisExpain.Text = _Model.DiagnosisExpain;
            lueDiagnosisAType.EditValue = _Model.DiagnosisAType;
            meSummAdvice.Text = _Model.SummAdvice;
            meDepartmentAdvice.Text = _Model.DepartmentAdvice;
            meClientAdvice.Text = _Model.ClientAdvice;
            meDietGuide.Text = _Model.DietGuide;
            meSportGuide.Text = _Model.SportGuide;
            meKnowledge.Text = _Model.Knowledge;
            meHealthcareAdvice.Text = _Model.HealthcareAdvice;
            lueSexState.EditValue = _Model.SexState;
            lueMarrySate.EditValue = _Model.MarrySate;
            teMinAge.Text = _Model.MinAge?.ToString();
            teMaxAge.Text = _Model.MaxAge?.ToString();
        }
        #endregion

    }
}
