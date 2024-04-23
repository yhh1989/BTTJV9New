using Sw.Hospital.HealthExaminationSystem.ApiProxy.AddStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.AddStatistics.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.AddStatistics
{
    public partial class AddStatiList : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;
        private readonly AddStatisticsAppService _addStatisticsAppService;
        public AddStatiList()
        {
            _commonAppService = new CommonAppService();
            _addStatisticsAppService = new AddStatisticsAppService();
            InitializeComponent();
        }

        private void AddStatiList_Load(object sender, EventArgs e)
        {
            
            //初始化  操作人姓名
           var ds = searchLookUpEditName.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            //初始化   体检加项
            searchLookUpEditxiang.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            //初始化  时间
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditstart.DateTime = date;
            dateEditend.DateTime = date;

            gridView1.IndicatorWidth = 50;
        }
        public void ShowList()
        {
            AutoLoading(() =>
            {
                var datestart = dateEditstart.EditValue as DateTime?;
                var dateend = dateEditend.EditValue as DateTime?;
                var checkname = searchLookUpEditName.EditValue as long?;
                var groupname = searchLookUpEditxiang.EditValue as Guid?;
                var input = new SearchStatisticsDto();
                input.CheckName = checkname;
                input.GroupName = groupname;
                input.DataTimeEnd = dateend.Value;
                input.DataTimeStart = datestart.Value;
                var rult = _addStatisticsAppService.AddStatisticsList(input);
                gridControlData.DataSource = rult;

            });
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;  //当前时间
            //DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一
            //DateTime endWeek = startWeek.AddDays(6);  //本周周日

            dateEditstart.DateTime = DateTime.Parse( dt.ToShortDateString());
            dateEditend.DateTime = DateTime.Parse(dt.AddDays(1).ToShortDateString());
            ShowList();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;  //当前时间
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末//

            dateEditstart.DateTime = startMonth;
            dateEditend.DateTime = endMonth;
            ShowList();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;  //当前时间
            DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初
            DateTime endYear = new DateTime(dt.Year, 12, 31);  //本年年末至于昨天、明天、上周、上月、上季度、上年度等等
            dateEditstart.DateTime = startYear;
            dateEditend.DateTime = endYear;
            ShowList();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ShowList();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "加项统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gridControlData.ExportToXls(saveFileDialog.FileName);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
