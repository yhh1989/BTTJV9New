using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using DevExpress.XtraLayout.Utils;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemInfo
{
    public partial class ItemEditor : UserBaseForm 
    {
        private readonly IItemInfoAppService _itemInfoAppService;

        private readonly ICommonAppService _commonAppService;

        private readonly Guid _id;

        private string _itemBm;
        

        public Guid DepartmentId { get; set; }
        public ItemInfoViewDto _Model { get; private set; }

        public ItemEditor()
        {
            InitializeComponent();

            _itemInfoAppService = new ItemInfoAppService();
            _commonAppService = new CommonAppService();
        }

        public ItemEditor(Guid id) : this()
        {
            _id = id;
        }

        private void FrmItemInfo_Load(object sender, EventArgs e)
        {
            InitializeData();
            if (DepartmentId != Guid.Empty)
            {
                lookUpEditDepartments.EditValue = DepartmentId;
            }
            if (_id != Guid.Empty)
            {
                LoadData();
            }
            else
            {
                _itemBm = _commonAppService.GetDateTimeNow().Now.ToString("yyyyMMddHHmmssfff");
                Text = $@"{Text}-{_itemBm}";
                lookUpEditSex.EditValue = (int)Sex.GenderNotSpecified;
                var query = _itemInfoAppService.GetMaxOrderNum();
                spinEditItemNumber.EditValue = query + 1;
                textBM.Text = _itemBm;
            }
        }

        private void LoadData()
        {
            try
            {
                var data = _itemInfoAppService.GetById(new EntityDto<Guid> { Id = _id });
                if (string.IsNullOrWhiteSpace(data.ItemBM))
                {
                    data.ItemBM = _commonAppService.GetDateTimeNow().Now.ToString("yyyyMMddHHmmssfff");
                }
                _itemBm = data.ItemBM;
                textBM.Text = _itemBm;
                Text = $@"{Text}-{data.ItemBM}";
                if (data.Ckisrpot.HasValue)
                    radioGroupIsPrinting.EditValue = data.Ckisrpot;
                if (data.DiagnosisComplexSate.HasValue)
                    radioGroupIsCompositeJudgment.EditValue = data.DiagnosisComplexSate;
                lookUpEditDepartments.EditValue = data.Department.Id;
                textEditHelpChar.Text = data.HelpChar;
                textEditHisCode.Text = data.HISCode;
                if (data.IsSummary.HasValue)
                    radioGroupIsInKnobble.EditValue = data.IsSummary;
                if (data.IsSummaryName.HasValue)
                    radioGroupIsShowKnobbleName.EditValue = data.IsSummaryName;
                if (data.ItemDecimal.HasValue)
                    spinEditRadixPoint.Value = (decimal)data.ItemDecimal;
                lookUpEditClinicType.EditValue = data.Lclxid;
                if (data.MaxAge.HasValue)
                    spinEditMaxAge.Value = (decimal)data.MaxAge;
                if (data.MinAge.HasValue)
                    spinEditMinAge.Value = (decimal)data.MinAge;
                lookUpEditItemType.EditValue = data.moneyType;
                textEditItemName.Text = data.Name;
                textEditItemEnName.Text = data.NameEngAbr;
                textEditPrintName.Text = data.NamePM;
                textEditMatters.Text = data.Notice;
                textEditGWBM.Text = data.GWBM;
                if (data.OrderNum.HasValue)
                    spinEditItemNumber.Value = (decimal)data.OrderNum;
                if (data.SeeState.HasValue)
                    lookUpEditCCHYXL.EditValue = data.SeeState;
                if (data.ISLJ.HasValue && data.ISLJ == 1)
                {
                    checkLJ.Checked = true;
                }
                else
                {
                    checkLJ.Checked = false;
                }
                if (data.MinValue.HasValue )
                {
                    spinEditMin.EditValue = data.MinValue.Value;

                }
                else
                {
                    spinEditMin.EditValue ="";
                }
                if (data.MaxValue.HasValue)
                {
                    spinEditMax.EditValue = data.MaxValue.Value;

                }
                else
                {
                    spinEditMax.EditValue = "";
                }

                textBoxUnitBM.Text= data.UnitBM;
                textEditItemDesc.Text = data.Remark;
                textEditReportCode.Text = data.ReportCode;
                lookUpEditSex.EditValue = data.Sex;
                textEditStandardCode.Text = data.StandardCode;
                textEditCompany.Text = data.Unit;
                textEditWuBi.Text = data.WBCode;
                //if (data.IsActive == 0)
                //    lookUpEditEnable.EditValue = (int)InvoiceState.Enable;
                //else
                    lookUpEditEnable.EditValue = data.IsActive;
                if (data.ItemInfos != null && data.ItemInfos.Count > 0)
                {
                    txtHCItems.Tag = data.ItemInfos;
                    string itemnames = "";
                    foreach (ItemInfoSimpleDto item in data.ItemInfos)
                    {
                        itemnames += item.Name + ",";
                    }
                    txtHCItems.Text = itemnames.TrimEnd(',');

                }
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBoxError(e.ToString());
            }
        }

        /// <summary>
        /// 下拉框绑定数据
        /// </summary>
        private void InitializeData()
        {
            var departments = _itemInfoAppService.DepartmentGetAll();
            lookUpEditDepartments.Properties.DataSource = departments;
            lookUpEditDepartments.EditValue = departments.FirstOrDefault()?.Id;

            var sexs = SexHelper.GetSexModelsForItemInfo();
            lookUpEditSex.Properties.DataSource = sexs;

            var itemTypes = ItemTypeHelper.GetItemTypeModels();
            lookUpEditItemType.Properties.DataSource = itemTypes;
            lookUpEditItemType.EditValue =(int)ItemType.Explain;

            var enableItems = InvoiceStateHelper.GetInvoiceStateModels();
            lookUpEditEnable.Properties.DataSource = enableItems;
            lookUpEditEnable.EditValue = (int)InvoiceState.Enable;

            var seeState = SeeStateHelper.GetSeeStateModels();
            seeState.Insert(0,new SeeStateModel ());
            lookUpEditCCHYXL.Properties.DataSource = seeState;
            lookUpEditCCHYXL.EditValue = (int)SeeState.Normal;

            var ifList = IfTypeHelper.GetIfTypeModels();
            foreach (var item in ifList)
            {
                var radioGroupItem = new RadioGroupItem(item.Id, item.Display);
                radioGroupIsCompositeJudgment.Properties.Items.Add(radioGroupItem);
                radioGroupIsInKnobble.Properties.Items.Add(radioGroupItem);
                radioGroupIsPrinting.Properties.Items.Add(radioGroupItem);
                radioGroupIsShowKnobbleName.Properties.Items.Add(radioGroupItem);
            }
            radioGroupIsShowKnobbleName.SelectedIndex = 1;
            radioGroupIsPrinting.SelectedIndex = 0;
            radioGroupIsInKnobble.SelectedIndex = 0;
            radioGroupIsCompositeJudgment.SelectedIndex = 1;
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            var keshiId = lookUpEditDepartments.EditValue;
            if (keshiId == null)
            {
                dxErrorProvider.SetError(lookUpEditDepartments, string.Format(Variables.MandatoryTips, "所属科室"));
                return;
            }
            var xmmc = textEditItemName.Text.Trim();
            if (String.IsNullOrWhiteSpace(xmmc))
            {
                dxErrorProvider.SetError(textEditItemName, string.Format(Variables.MandatoryTips, "项目名称"));
                textEditItemName.Focus();
                return;
            }
            var xmdymc = textEditPrintName.Text.Trim();
            if (string.IsNullOrWhiteSpace(xmdymc))
            {
                dxErrorProvider.SetError(textEditPrintName, string.Format(Variables.MandatoryTips, "项目打印名称"));
                textEditPrintName.Focus();
                return;
            }
            var helpChar = textEditHelpChar.Text.Trim();
            if (string.IsNullOrWhiteSpace(helpChar))
            {
                dxErrorProvider.SetError(textEditHelpChar, string.Format(Variables.MandatoryTips, "助记码"));
                textEditHelpChar.Focus();
                return;
            }
            var sex = lookUpEditSex.EditValue;
            if (sex == null)
            {
                dxErrorProvider.SetError(lookUpEditSex, string.Format(Variables.MandatoryTips, "性别"));
                return;
            }
            try
            {
                ItemInfoDto inputDto = new ItemInfoDto();
                inputDto.Name = textEditItemName.Text.Trim();
                inputDto.DepartmentId = (Guid)lookUpEditDepartments.EditValue;
                inputDto.NamePM = textEditPrintName.Text.Trim();
                inputDto.NameEngAbr = textEditItemEnName.Text.Trim();
                inputDto.HelpChar = textEditHelpChar.Text.Trim();
                inputDto.WBCode = textEditWuBi.Text.Trim();
                inputDto.Sex = Convert.ToInt16(lookUpEditSex.EditValue);
                inputDto.MinAge = Convert.ToInt16(spinEditMinAge.Value);
                inputDto.MaxAge = Convert.ToInt16(spinEditMaxAge.Value);
                inputDto.Unit = textEditCompany.Text.Trim();
                inputDto.Notice = textEditMatters.Text.Trim();
                inputDto.Remark = textEditItemDesc.Text.Trim();
                inputDto.OrderNum = Convert.ToInt16(spinEditItemNumber.Value);
                inputDto.moneyType = Convert.ToInt16(lookUpEditItemType.EditValue);
                inputDto.Lclxid = Convert.ToInt16(lookUpEditClinicType.EditValue);
                inputDto.Ckisrpot = Convert.ToInt16(radioGroupIsPrinting.EditValue);
                inputDto.ReportCode = textEditReportCode.Text.Trim();
                inputDto.HISCode = textEditHisCode.Text.Trim();
                inputDto.StandardCode = textEditStandardCode.Text.Trim();
                inputDto.DiagnosisComplexSate = Convert.ToInt16(radioGroupIsCompositeJudgment.EditValue);
                inputDto.IsSummary = Convert.ToInt16(radioGroupIsInKnobble.EditValue);
                inputDto.IsSummaryName = Convert.ToInt16(radioGroupIsShowKnobbleName.EditValue);
                inputDto.ItemDecimal = Convert.ToInt16(spinEditRadixPoint.Value);
                inputDto.UnitBM = textBoxUnitBM.Text;
               
                if (!string.IsNullOrEmpty(textBM.Text))
                {
                    inputDto.ItemBM = textBM.Text;
                }
                else
                {
                    inputDto.ItemBM = _itemBm;
                }
                inputDto.SeeState = Convert.ToInt16(lookUpEditCCHYXL.EditValue);
                inputDto.ItemInfos =(List<ItemInfoSimpleDto>) txtHCItems.Tag;
                inputDto.IsActive = (int)lookUpEditEnable.EditValue;
                inputDto.GWBM = textEditGWBM.Text;
                if (checkLJ.Checked == true)
                {
                    inputDto.ISLJ = 1;

                }
                else
                {
                    inputDto.ISLJ = 0;
                }
                if (!string.IsNullOrEmpty(spinEditMin.EditValue?.ToString()))
                {
                    inputDto.MinValue =decimal.Parse( spinEditMin.EditValue?.ToString());
                }
                else
                { inputDto.MinValue = null; }

                if (!string.IsNullOrEmpty(spinEditMax.EditValue?.ToString()))
                {
                    inputDto.MaxValue = decimal.Parse(spinEditMax.EditValue?.ToString());
                }
                else
                { inputDto.MaxValue = null; }

                if (_id != Guid.Empty)
                {
                    inputDto.Id = _id;
                    //if ((int)lookUpEditEnable.EditValue == (int)InvoiceState.Discontinuation)
                    //{
                    //    _itemInfoAppService.DeleteItemInfo(new EntityDto<Guid> {Id=inputDto.Id });
                    //    DialogResult = System.Windows.Forms.DialogResult.OK;
                    //    return;
                    //}
                    //else
                    //    inputDto.IsDeleted = 0;
                    var editResult = _itemInfoAppService.EditItemInfo(inputDto);
                    #region 获取刷新数据
                    _Model = new ItemInfoViewDto();
                    _Model.Name = textEditItemName.Text.Trim();            
                    _Model.HelpChar = textEditHelpChar.Text.Trim();                 
                    _Model.Sex = Convert.ToInt16(lookUpEditSex.EditValue);
                    _Model.MinAge = Convert.ToInt16(spinEditMinAge.Value);
                    _Model.MaxAge = Convert.ToInt16(spinEditMaxAge.Value);
                    _Model.Unit = textEditCompany.Text.Trim();
                    _Model.Notice = textEditMatters.Text.Trim();
                    _Model.Remark = textEditItemDesc.Text.Trim();
                    _Model.OrderNum = Convert.ToInt16(spinEditItemNumber.Value);
                    _Model.moneyType = Convert.ToInt16(lookUpEditItemType.EditValue);                  
                    _Model.ItemBM = _itemBm;
                    _Model.Department = new DepartmentIdNameDto();
                    _Model.Department.Id = editResult.Department.Id;
                    _Model.Department.Name = editResult.Department.Name;
                    _Model.Department.OrderNum = editResult.Department.OrderNum;
                    _Model.IsActive = (int)lookUpEditEnable.EditValue;
                    _Model.Id = _id;
                    #endregion

                    if (editResult != null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    if ((int)lookUpEditEnable.EditValue == (int)InvoiceState.Discontinuation)
                    {
                        dxErrorProvider.SetError(lookUpEditEnable, "不能为停用状态！");
                        textEditItemName.Focus();
                        return;
                    }
                    var addRsult = _itemInfoAppService.AddItemInfo(inputDto);
                    if (addRsult != null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBoxError(ex.Description);
                ShowMessageBox(ex);
            }

        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void textEditItemName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEditItemName.Text))
                return;
            ChineseDto input = new ChineseDto();
            input.Hans = textEditItemName.Text.Trim();
            textEditHelpChar.Text = _commonAppService.GetHansBrief(input).Brief;
            textEditPrintName.Text = textEditItemName.Text;
            WuBiHelper wbHelper = new WuBiHelper();
            textEditWuBi.Text = wbHelper.GetWBCode(textEditItemName.Text.Trim());
        }

        private void butCheck_Click(object sender, EventArgs e)
        {
            frmHCITems frmHCITems = new frmHCITems();
            if (txtHCItems.Tag != null)
            {
                frmHCITems.selectItems = (List<ItemInfoSimpleDto>)txtHCItems.Tag;
            }
            if (frmHCITems.ShowDialog() == DialogResult.OK)
            {
                txtHCItems.Tag = frmHCITems.selectItems;
                string itemnames = "";
                foreach (ItemInfoSimpleDto item in frmHCITems.selectItems)
                {
                    itemnames += item.Name + ",";
                }
                txtHCItems.Text = itemnames.TrimEnd(',');
            }

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLJ.Checked == true)
            {

                layoutControlItem10.Visibility = LayoutVisibility.Always;
                layoutControlItem11.Visibility = LayoutVisibility.Always;

            }
            else
            {
                layoutControlItem10.Visibility = LayoutVisibility.Never;
                layoutControlItem11.Visibility = LayoutVisibility.Never;
            }
        }
    }
}