using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class CheckGroup : UserBaseForm
    {
        public DoctorStationAppService doctorStationAppService = new DoctorStationAppService();
       private  ICommonAppService _commonAppService = new CommonAppService();
        public CheckGroup()
        {
            InitializeComponent();
        }

        private void CheckGroup_Load(object sender, EventArgs e)
        {
          
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStar.DateTime = date;
            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();

            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();

            lUpDJState.Properties.DataSource = RegisterStateHelper.GetSelectList();
            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();

            //总检状态
            gridViewCus.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCus.Columns[conSummSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(SummSateHelper.SummSateFormatter);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SearchCusGroupDto searchCusGroup = new SearchCusGroupDto();
         

            //登记状态
            if (lUpDJState.EditValue != null)
            {
                searchCusGroup.RegisterState = int.TryParse(lUpDJState.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                searchCusGroup.RegisterState = searchCusGroup.RegisterState == 0 ? null : searchCusGroup.RegisterState;
            }

            //体检状态
            if (lookUpEditExaminationStatus.EditValue != null)
            {
                searchCusGroup.CheckSate = int.TryParse(lookUpEditExaminationStatus.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                searchCusGroup.CheckSate = searchCusGroup.CheckSate == 0 ? null : searchCusGroup.CheckSate;
            }

            //总检状态
            if ( lookUpEditSumStatus.EditValue != null)
            {
                searchCusGroup.SummSate = int.TryParse(lookUpEditSumStatus.EditValue.ToString(), out var DJZT) ? (int?)DJZT : null;
                searchCusGroup.SummSate = searchCusGroup.SummSate == 0 ? null : searchCusGroup.SummSate;
            }
            //单位名称
            if (!string.IsNullOrEmpty(sleDW.EditValue?.ToString()))
            {
                searchCusGroup.ClientRegId = Guid.Parse(sleDW.EditValue.ToString());                 
            }
            if (dateEditStar.EditValue != null && dateEditEnd.EditValue != null)
            {
                searchCusGroup.StartDate =DateTime.Parse( dateEditStar.DateTime.ToShortDateString());
                searchCusGroup.EndtDate = DateTime.Parse(dateEditEnd.DateTime.AddDays(1).ToShortDateString());
                if (comboBoxEdit1.Text.Contains("登记日期"))
                {
                    searchCusGroup.TimeType = 1;
                }
                else if (comboBoxEdit1.Text.Contains("到检日期"))
                { searchCusGroup.TimeType = 2; }
                else
                { searchCusGroup.TimeType = 3; }

            }

            var outRet = doctorStationAppService.getCheckCusGroup(searchCusGroup);
            gridControlCus.DataSource = outRet;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "到检统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
          

            gridControlCus.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
    }
}
