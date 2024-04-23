using System;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Department
{
    public partial class DepartmentEditor : UserBaseForm
    {
        private readonly Guid _id;

        private readonly IDepartmentAppService _departmentAppService;
        private readonly IIDNumberAppService _iDNumberAppService;

        private readonly ICommonAppService _commonAppService;

        public TbmDepartmentDto _Model { get; private set; }

        /// <summary>
        /// 新增
        /// </summary>
        public DepartmentEditor()
        {
            InitializeComponent();

            _departmentAppService = new DepartmentAppService();
            _commonAppService = new CommonAppService();
            _iDNumberAppService = new IDNumberAppService();
        }

        public DepartmentEditor(Guid id) : this()
        {
            _id = id;
        }

        private void InitializeData()
        {
            var sexs = SexHelper.GetSexModelsForItemInfo();
            lookUpEditSex.Properties.DataSource = sexs;
            lookUpEditSex.EditValue = (int)Sex.GenderNotSpecified;

            comboBoxEditDepartmentType1.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.LargeDepatType);

            comboBoxEditDepartmentType.SelectedIndex = 5;
        }

        //保存
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            var code = textEditCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                dxErrorProvider.SetError(textEditCode, string.Format(Variables.MandatoryTips, "编码"));
                textEditCode.Focus();
                return;
            }

            var name = textEditName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                dxErrorProvider.SetError(textEditName, string.Format(Variables.MandatoryTips, "名称"));
                textEditName.Focus();
                return;
            }

            var helpChar = textEditHelpChar.Text.Trim();
            var applySex = (int)lookUpEditSex.EditValue;
            var deptType = comboBoxEditDepartmentType.SelectedItem.ToString();
        
            var maxDailyString = textEditDailyCheckNumber.Text.Trim();
            if (!int.TryParse(maxDailyString, out var maxDaily))
            {
                dxErrorProvider.SetError(textEditDailyCheckNumber, string.Format(Variables.NumberTips, "最大日检数"));
                textEditDailyCheckNumber.Focus();
                return;
            }
            var duty = textEditDuty.Text.Trim();
            var desc = textEditDescription.Text.Trim();
            var manAddress = textEditManAddress.Text.Trim();
            var womanAddress = textEditWomanAddress.Text.Trim();
            var vipAddress = textEditVipAddress.Text.Trim();
            var oreryNum = textEditOrderNum.Text.Trim();
            var sumFormat = txtSumformat.Text.Trim();
            try
            {
                if (_id == Guid.Empty)
                {
                    var input = new CreateDepartmentDto
                    {
                        DepartmentBM = code,
                        Name = name,
                        HelpChar = helpChar,
                        Sex = applySex,
                        Category = deptType,
                        MaxCheckDay = maxDaily,
                        Duty = duty,
                        Remarks = desc,
                        MenAddress = manAddress,
                        WomenAddress = womanAddress,
                        Address = vipAddress,
                        OrderNum = Convert.ToInt32(oreryNum),
                        IsActive = checkEdit1.Checked ? false : true,
                        SumFormat = sumFormat,
                        


                    };
                    if (!string.IsNullOrEmpty(comboBoxEditDepartmentType1.EditValue?.ToString()))
                    {
                        input.LargeDepart = (int)comboBoxEditDepartmentType1.EditValue;

                    }
                    _departmentAppService.InsertDepartmentn(input);
                }
                else
                {
                    var input = new UpdateDepartmentDto
                    {
                        Id = _id,
                        DepartmentBM = code,
                        Name = name,
                        HelpChar = helpChar,
                        Sex = applySex,
                        Category = deptType,
                        MaxCheckDay = maxDaily,
                        Duty = duty,
                        Remarks = desc,
                        MenAddress = manAddress,
                        WomenAddress = womanAddress,
                        Address = vipAddress,
                        OrderNum = Convert.ToInt32(oreryNum),
                        IsActive = checkEdit1.Checked ? false : true,
                        SumFormat = sumFormat
                    };
                    if (!string.IsNullOrEmpty(comboBoxEditDepartmentType1.EditValue?.ToString()))
                    {
                        input.LargeDepart = (int)comboBoxEditDepartmentType1.EditValue;

                    }
                    _departmentAppService.Update(input);
                    var tbminput = new TbmDepartmentDto
                    {
                        Id = _id,
                        DepartmentBM = code,
                        Name = name,
                        HelpChar = helpChar,
                        Sex = applySex,
                        Category = deptType,
                        MaxCheckDay = maxDaily,
                        Duty = duty,
                        Remarks = desc,
                        MenAddress = manAddress,
                        WomenAddress = womanAddress,
                        Address = vipAddress,
                        OrderNum = Convert.ToInt32(oreryNum),
                        IsActive = checkEdit1.Checked ? false : true,
                        SumFormat = sumFormat
                        
                    };
                    if (!string.IsNullOrEmpty(comboBoxEditDepartmentType1.EditValue?.ToString()))
                    {
                        tbminput.LargeDepart = (int)comboBoxEditDepartmentType1.EditValue;

                    }
                    _Model = tbminput;
                }
                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }

        }

        private void DepartmentEditor_Load(object sender, EventArgs e)
        {
            InitializeData();

            if (_id != Guid.Empty)
            {
                LoadData();
            }
            else
            {
                var Id = _iDNumberAppService.CreateDepartmentBM();
                textEditCode.Text = Id;
                var query = _departmentAppService.GetMaxOrderNum();
                textEditOrderNum.EditValue = query + 1;
            }
        }

        private void LoadData()
        {
            try
            {

                var dept = _departmentAppService.GetById(new EntityDto<Guid> { Id = _id });
                textEditCode.Text = dept.DepartmentBM;

                textEditName.Text = dept.Name;
                textEditOrderNum.Text = (dept.OrderNum??0).ToString();
                textEditHelpChar.Text = dept.HelpChar;
                if (dept.Sex.HasValue)
                    lookUpEditSex.EditValue = dept.Sex.Value;
                comboBoxEditDepartmentType.SelectedItem = dept.Category;
                if (dept.LargeDepart.HasValue)
                {

                    comboBoxEditDepartmentType1.EditValue = dept.LargeDepart;
                }

                if (dept.MaxCheckDay.HasValue)
                    textEditDailyCheckNumber.Text = dept.MaxCheckDay.Value.ToString();
                textEditDuty.Text = dept.Duty;
                textEditDescription.Text = dept.Remarks;
                textEditManAddress.Text = dept.MenAddress;
                textEditWomanAddress.Text = dept.WomenAddress;
                textEditVipAddress.Text = dept.Address;
                txtSumformat.Text = dept.SumFormat;
                // 是否启用
                checkEdit1.Checked = dept.IsActive == false;

            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
        }

        private void textEditName_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            CommonHelper.SetHelpChar(textEditHelpChar, textEditName);
        }
    }
}