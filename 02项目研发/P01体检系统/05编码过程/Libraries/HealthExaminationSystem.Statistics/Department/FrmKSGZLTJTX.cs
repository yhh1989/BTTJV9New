using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    public partial class FrmKSGZLTJTX : UserBaseForm
    {
        public FrmKSGZLTJTX()
        {
            InitializeComponent();
        }
        DepartmentAppService _departProxy = new DepartmentAppService();
        DoctorStationAppService _proxy = new DoctorStationAppService();
        CommonAppService dtApp = new CommonAppService();

        private void FrmKSGZLTJTX_Load(object sender, EventArgs e)
        {
            Init();
            dtpStart.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            dtpEnd.Text = dtApp.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 科室加载
        /// </summary>
        private void Init()
        {
            List<TbmDepartmentDto> departmentList = _departProxy.GetAll();
            txt_Department.Properties.DataSource = departmentList;
            txt_Department.Properties.ValueMember = "Id";
            txt_Department.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            dgc.DataSource = null;
            DataTable dt = null;
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {
                if ( string.IsNullOrWhiteSpace( this.txt_Department.EditValue?.ToString()))
                {
                    ShowMessageBoxWarning("查询科室不能为空");
                    return;
                }
                HBQueryClass query = new HBQueryClass();
                DateTime QueryStartTime = Convert.ToDateTime(dtpStart.EditValue.ToString());
                DateTime QueryEndTime = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                if (QueryStartTime > QueryEndTime || QueryStartTime.Year != QueryEndTime.Year || dtpStart.EditValue == null || dtpEnd.EditValue == null)
                {
                    ShowMessageBoxWarning("日期格式不正确，请选择正确的时间范围");
                    return;
                }
                //时间
                if (dtpStart.EditValue != null)
                {
                    query.DQStartTime = QueryStartTime;
                }
                if (dtpEnd.EditValue != null)
                {
                    query.DQEndTime = QueryEndTime;
                }
                if (rdo_Period.SelectedIndex == 0)
                {
                    query.WeekQuery = true;
                }
                else
                    query.WeekQuery = false;
                List<Guid> departmentIds = new List<Guid>();//科室id

                if (!string.IsNullOrWhiteSpace(txt_Department.EditValue.ToString()))
                {
                    string[] str = txt_Department.EditValue?.ToString()?.Split(',');
                    if (str != null)
                    {
                        List<System.Guid> list = new List<System.Guid>();
                        foreach (var item in str)
                        {
                            list.Add(new Guid(item));
                        }
                        departmentIds = list;
                    }
                }
                query.DepartmentBMList = departmentIds;
                dt = _proxy.KSGZLSTStatistics(query);
                if (dt.Rows.Count <= 0)
                {
                    splashScreenManager.CloseWaitForm();
                    return;
                }
                if (rdo_Period.SelectedIndex == 0)
                {
                    GetPeriod(QueryStartTime, QueryEndTime);
                    DataTable quarter = new DataTable();
                    quarter = dt.Clone();
                    var list = YearList;
                    foreach (var groupitem in list)
                    {
                        var dr = quarter.NewRow();
                        for (int j = 1; j < quarter.Columns.Count; j++)
                        {
                            string columName = quarter.Columns[j].ColumnName;
                            int tola = dt.AsEnumerable().Where(r => Convert.ToDateTime(r["周期"].ToString())
                            > groupitem.STtime && Convert.ToDateTime(r["周期"].ToString())
                            < groupitem.EDtime).Sum(n => Convert.ToInt32(n.IsNull(columName) ? 0 : n[columName]));
                            dr[columName] = tola;
                        }
                        dr["周期"] = groupitem.Type;
                        quarter.Rows.Add(dr);
                    }
                    dt = quarter;
                }
                if (rdo_Period.SelectedIndex == 2)//季度
                {
                    DataTable quarter = new DataTable();
                    quarter = dt.Clone();
                    var list = QueryList();
                    foreach (var groupitem in list)
                    {
                        var dr = quarter.NewRow();
                        for (int i = 1; i < quarter.Columns.Count; i++)
                        {
                            string columName = quarter.Columns[i].ColumnName;
                            int tola = dt.AsEnumerable().Where(r => groupitem.Query.Contains(r["周期"].ToString())).Sum(n => Convert.ToInt32(n.IsNull(columName) ? 0 : n[columName]));
                            dr[columName] = tola;
                        }
                        dr["周期"] = groupitem.Type;
                        quarter.Rows.Add(dr);
                    }
                    dt = quarter;
                }
                ToLineGragh(dt);
                dgv.Columns.Clear();
                DataTable NewDt = dt.Copy();
                var total = NewDt.NewRow();
                for (int j = 1; j < NewDt.Columns.Count; j++)
                {
                    int Count = 0;
                    for (int i = 0; i < NewDt.Rows.Count; i++)
                    {
                        Count += Int32.Parse(NewDt.Rows[i][NewDt.Columns[j].ColumnName].ToString());
                    }
                    total[NewDt.Columns[j].ColumnName] = Count;
                    total["周期"] = "汇总";
                }
                NewDt.Rows.Add(total);
                dgc.DataSource = NewDt;
                splashScreenManager.CloseWaitForm();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }

        #region 日期获取
        public class Statistic
        {
            public string Type { get; set; }
            public DateTime STtime { get; set; }
            public DateTime EDtime { get; set; }
            public List<string> Query { get; set; }
        }
        public class JiDuUtil
        {
            public string JiDu { get; set; }
            public string YueFen { get; set; }
        }
        /// <summary>
        /// 季度
        /// </summary>
        /// <returns></returns>
        public List<Statistic> QueryList()
        {
            List<Statistic> list = new List<Statistic>();
            list.Add(new Statistic() { Type = "第一季度", Query = new List<string>() { "1月", "2月", "3月" } });
            list.Add(new Statistic() { Type = "第二季度", Query = new List<string>() { "4月", "5月", "6月" } });
            list.Add(new Statistic() { Type = "第三季度", Query = new List<string>() { "7月", "8月", "9月" } });
            list.Add(new Statistic() { Type = "第四季度", Query = new List<string>() { "10月", "11月", "12月" } });
            return list;
        }

        /// <summary>
        /// 获取指定期间的起止日期
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        List<Statistic> YearList;
        public void GetPeriod(DateTime beginDate, DateTime endDate)
        {
            YearList = new List<Statistic>();
            int Syear = beginDate.Year;
            beginDate = beginDate.AddDays(1 - Convert.ToInt32(beginDate.DayOfWeek.ToString("d")));  //本周周一
            int weeks = GetWeekOfYear(beginDate) - 1;//本年第几周
            GetWeek(Syear - 1, weeks + 1);
            //去年第几周的开始时间和结束时间
            //本年查询周数
            for (DateTime dt = beginDate.Date; dt < Convert.ToDateTime(dtpEnd.EditValue.ToString()); dt = dt.AddDays(6))
            {
                weeks = weeks + 1;
                Statistic result = new Statistic();
                result.Type = "第" + weeks + "周";
                result.STtime = dt;
                result.EDtime = dt.AddDays(7);
                YearList.Add(result);
            }
        }

        /// <summary>
        /// 一年中的周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        /// <summary>
        /// 用年份和第几周来获得一周开始和结束的时间,这里用星期一做开始。
        /// </summary>
        DateTime dtweekStart;
        DateTime dtweekeEnd;
        public void GetWeek(int year, int weekNum)
        {
            var dateTime = new DateTime(year, 1, 1);
            dateTime = dateTime.AddDays(7 * weekNum);
            dtweekStart = dateTime.AddDays(-(int)dateTime.DayOfWeek + (int)DayOfWeek.Monday);
            dtweekeEnd = dateTime.AddDays((int)DayOfWeek.Saturday - (int)dateTime.DayOfWeek + 1);
        }
        #endregion
        class RecordData
        {
            public string name { get; set; }
            public int value { get; set; }
        }
        #region 图形
        /// <summary>
        /// 把接收到的数据转换额外柱状图表所需要的数据, 绘制图表
        /// </summary>
        private void ToLineGragh(DataTable data)
        {
            string xBindName = "周期";
            //string seriesName = "科室";
            ViewType seriesType = ViewType.Bar;
            ChartControl.Series.Clear();
            if (cob_LeiXing.Text == "饼图")
            {
                seriesType = ViewType.Pie;
                string[] ValueDataMembers = new string[data.Columns.Count - 1];

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    //  var dts = data.Rows.AsEnumerable().Select(r => new { name = r["周期"], value = "" }).ToList();

                    string yBindName = data.Rows[i]["周期"].ToString();
                    //DataTable drTable = data.Rows[i].Table.Clone();
                    //drTable.Rows.Add(data.Rows[i].ItemArray);
                    List<RecordData> list = new List<RecordData>();
                    for (int j = 1; j < data.Columns.Count; j++)
                    {
                        string columnName = data.Columns[j].ColumnName;

                        list.Add(new RecordData() { name = columnName, value = Convert.ToInt32(string.IsNullOrEmpty(data.Rows[i][columnName].ToString()) ? "0" : data.Rows[i][columnName].ToString()) });
                    }
                    CreateSeries(ChartControl, yBindName, seriesType, list, "name", new string[] { "value" }, null);
                }
            }
            else
            {
                if (cob_LeiXing.Text == "折线图")
                    seriesType = ViewType.Line;
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    string yBindName = data.Columns[i].ColumnName;
                    CreateSeries(ChartControl, yBindName, seriesType, data, xBindName, new string[] { yBindName }, null);
                }
            }
        }
        /// <summary>
        /// 绘制图标
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="seriesName"></param>
        /// <param name="seriesType"></param>
        /// <param name="dataSource"></param>
        /// <param name="xBindName"></param>
        /// <param name="yBindName"></param>
        /// <param name="createSeriesRule"></param>
        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string[] ValueDataMembers,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (ValueDataMembers == null)
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            //  _series.ValueDataMembers[0] = yBindName;
            _series.ValueDataMembers.AddRange(ValueDataMembers);

            _series.ShowInLegend = true;
            //_series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
            _series.Label.TextPattern = "{A}//{V}";
            //{A}表示argument   {V}表示数据值      {S}表示series
            //  series.Label.TextPattern = "{A}//{V}//{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);

        }
        #endregion

        private void btnExprt_Click(object sender, EventArgs e)
        {
            dgv.CustomExport("科室工作量统计");
        }
    }
}
