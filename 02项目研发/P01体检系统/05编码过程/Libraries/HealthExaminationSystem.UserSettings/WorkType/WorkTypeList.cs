using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.WorkType
{
    public partial class WorkTypeList : UserBaseForm
    {
        private readonly WorkTypeAppService _workTypeAppService;

        public WorkTypeList()
        {
            InitializeComponent();

            _workTypeAppService = new WorkTypeAppService();

            gridViewWorkType.Columns[gridColumnWorkTypeValid.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewWorkType.Columns[gridColumnWorkTypeZyWorkTypes.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            //gridViewWorkType.Columns[gridColumnWorkTypeValid.FieldName].DisplayFormat.Format = new CustomFormatter(FormatValid);
            gridViewWorkType.Columns[gridColumnWorkTypeZyWorkTypes.FieldName].DisplayFormat.Format = new CustomFormatter(FormatZyWorkTypes);
        }

        private string FormatZyWorkTypes(object arg)
        {
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.Work))
                return "工种";
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.Workshop))
                return "车间";
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.Industry))
                return "行业";
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.IllName))
                return "问诊疾病";
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.Advice))
                return "处理意见";
            if (arg.Equals((int)HealthExaminationSystem.Common.Enums.WorkType.CheckType))
                return "检查类型";
            return "未知";
        }

        private string FormatValid(object arg)
        {
            if (arg.Equals(true))
                return "启用";
            if (arg.Equals(false))
                return "未启用";
            return "未知";
        }

        private void sbReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new WorkTypeEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void sbEdit_Click(object sender, EventArgs e)
        {
            if (gridViewWorkType.FocusedRowHandle < 0)
            {
                ShowMessageBoxWarning("请选择数据行后编辑");
                return;
            }
              
            using(var frm=new WorkTypeEditor())
            {
                frm.workTypeInfo = gridViewWorkType.GetFocusedRow() as WorkTypeDto;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            if (gridViewWorkType.FocusedRowHandle < 0)
            {
                ShowMessageBoxWarning("请选择数据行后编辑");
                return;
            }
            DialogResult dr = XtraMessageBox.Show("确定删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                var curGuid = (gridViewWorkType.GetFocusedRow() as WorkTypeDto).Id;
                try
                {
                    _workTypeAppService.Del(new WorkTypeDto() { Id = curGuid });
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                LoadData();
            }
           
        }

        private void WorkType_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var worktype = new WorkTypeDto();
                if (radioGroupType.SelectedIndex > -1  )
                {
                    if (int.Parse(radioGroupType.EditValue.ToString())!=10)
                    {
                        worktype.Category = int.Parse(radioGroupType.EditValue.ToString());
                    }
                }
                var data = _workTypeAppService.Query(worktype);
                gridControl.DataSource = data;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void gridViewWorkType_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.Name== gridColumnWorkTypeValid.Name)
            {
                if (gridControl.DataSource == null)
                    return;
                var data = gridControl.DataSource as List<WorkTypeDto>;
                if (data.Count < e.ListSourceRowIndex)
                    return;
                if (data[e.ListSourceRowIndex].IsActive == true)
                {
                    e.DisplayText = "启用";
                }
                else
                {
                    e.DisplayText = "不启用";
                }
            }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            sbEdit_Click(sender, e);
        }
    }
}
