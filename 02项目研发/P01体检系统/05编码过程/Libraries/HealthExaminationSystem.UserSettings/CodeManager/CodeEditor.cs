using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CodeManager
{
    public partial class CodeEditor : UserBaseForm
    {
        private List<IdValue> _idValues;

        private readonly string _id;

        private readonly IDNumberAppService _idNumberAppService;

        public CodeEditor()
        {
            InitializeComponent();

            _idValues = new List<IdValue>();

            _idNumberAppService = new IDNumberAppService();
        }

        public CodeEditor(string id) : this()
        {
            _id = id;
        }

        private void OrganizeData()
        {
            _idValues.Add(new IdValue { Id = "DepartmentID", Value = "科室编码", Type = 1 });
            _idValues.Add(new IdValue { Id = "ClientID", Value = "单位编码", Type = 2 });
            _idValues.Add(new IdValue { Id = "ItemGroupID", Value = "项目组合编码", Type = 3 });
            _idValues.Add(new IdValue { Id = "ItemID", Value = "项目编码", Type = 4 });
            _idValues.Add(new IdValue { Id = "EmployeeID", Value = "人员编码", Type = 5 });
            _idValues.Add(new IdValue { Id = "ItemSuitID", Value = "项目套餐编码", Type = 6 });
            _idValues.Add(new IdValue { Id = "ClientRegID", Value = "单位预约编码", Type = 7 });
            _idValues.Add(new IdValue { Id = "CustomerID", Value = "患者编码", Type = 8 });
            _idValues.Add(new IdValue { Id = "CustomerRegID", Value = "患者预约编码", Type = 9 });
            _idValues.Add(new IdValue { Id = "ArchivesNumBM", Value = "档案编码", Type = 10 });
            _idValues.Add(new IdValue { Id = "BarNum", Value = "条码编码", Type = 11 });
            _idValues.Add(new IdValue { Id = "adviceBM", Value = "建议编码", Type = 13 });
            _idValues.Add(new IdValue { Id = "ApplicatioBM", Value = "申请单编码", Type = 14 });
            _idValues.Add(new IdValue { Id = "JKZBM", Value = "健康证编码", Type = 15 });
            _idValues.Add(new IdValue { Id = "RegNum", Value = "预约编码", Type = 18 });
            _idValues.Add(new IdValue { Id = "HGZBM", Value = "合格证编码", Type = 19 });
            lookUpEditCode.Properties.DataSource = _idValues;
        }

        private void CodeEditor_Load(object sender, EventArgs e)
        {
            OrganizeData();
            if (string.IsNullOrWhiteSpace(_id))
            {
                lookUpEditCode.EditValue = "DepartmentID";
            }
            else
            {
                lookUpEditCode.EditValue = _id;
            }
        }

        private void lookUpEditCode_EditValueChanged(object sender, EventArgs e)
        {
            var id = lookUpEditCode.EditValue.ToString();

            AutoLoading(() =>
            {
                var result = _idNumberAppService.GetByName(new NameDto { IDName = id });
                if (result != null)
                {
                    textEditValue.ReadOnly = true;
                    textEditPrefix.Text = string.Empty;
                    textEditPrefix.Text = result.prefix;
                    textEditDatePrefix.Text = string.Empty;
                    textEditDatePrefix.Text = result.Dateprefix;
                    textEditValue.Text = string.Empty;
                    textEditValue.Text = result.IDValue.ToString();
                }
            });

            var idValue = _idValues.Find(r => r.Id == id);
            emptySpaceItemCodeInfo.Text =
                string.Format(emptySpaceItemCodeInfo.CustomizationFormText, idValue.Id, idValue.Type);
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            var id = lookUpEditCode.EditValue.ToString();
            var idValue = _idValues.Find(r => r.Id == id);
            var createIdNumberDto = new CreateIdNumberDto();
            createIdNumberDto.IDName = idValue.Id;
            createIdNumberDto.IDType = idValue.Type.ToString();
            createIdNumberDto.prefix = textEditPrefix.Text.Trim();
            createIdNumberDto.Dateprefix = textEditDatePrefix.Text.Trim();
            createIdNumberDto.IDValue = Convert.ToInt32(textEditValue.Text.Trim());
            AutoLoading(() =>
            {
                _idNumberAppService.Create(createIdNumberDto);
                DialogResult = DialogResult.OK;
            });
        }
    }

    public class IdValue
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public int Type { get; set; }
    }
}