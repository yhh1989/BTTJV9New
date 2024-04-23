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
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System.Globalization;
using Sw.Hospital.HealthExaminationSystem.Application.Common;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Department
{
    public partial class FrmKSHBGZLTJ : UserBaseForm
    {
        public FrmKSHBGZLTJ()
        {
            InitializeComponent();
        }
        DepartmentAppService _departProxy = new DepartmentAppService();
        DoctorStationAppService _proxy = new DoctorStationAppService();
        private List<KSHBGZLStatisticsDto> result = null;
        CommonAppService dtApp = new CommonAppService();

        private void FrmKSHBGZLTJ_Load(object sender, EventArgs e)
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
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                dgc.DataSource = null;
                result = new List<KSHBGZLStatisticsDto>();
                if (this.txt_Department.EditValue == null || this.txt_Department.EditValue.ToString() == "nulltext")
                {
                    MessageBox.Show("查询科室不能为空");
                    return;
                }
                HBQueryClass query = new HBQueryClass();
                DateTime QueryStartTime = Convert.ToDateTime(dtpStart.EditValue.ToString());
                DateTime QueryEndTime = Convert.ToDateTime(dtpEnd.EditValue.ToString());
                if (QueryStartTime > QueryEndTime || QueryStartTime.Year != QueryEndTime.Year || dtpStart.EditValue == null || dtpEnd.EditValue == null)
                {
                    MessageBox.Show("日期格式不正确，请选择正确的时间范围");
                    return;
                }
                QueryEndTime = QueryEndTime.Date.AddDays(1);
                var endMoth= QueryEndTime.Date.Month;
                splashScreenManager.ShowWaitForm();
                //时间
                if (dtpStart.EditValue != null)
                {
                    query.DQStartTime = QueryStartTime;
                    query.LSStartTime = QueryStartTime.AddYears(-1);
                }
                if (dtpEnd.EditValue != null)
                {
                    query.DQEndTime = QueryEndTime;
                    query.LSEndTime = QueryEndTime.AddYears(-1);
                }
                if (rdo_Period.SelectedIndex == 0)
                {
                    query.WeekQuery = true;
                }
                else
                    query.WeekQuery = false;
                List<Guid> departmentIds = new List<Guid>();//科室id
                departmentIds.Add((System.Guid)txt_Department.EditValue);
                query.DepartmentBMList = departmentIds;
                result = _proxy.KSHBGZLStatistics(query);
                gcComparativeData.Caption = query.LSStartTime.Value.Year.ToString() + "年";
                gcCurrentData.Caption = query.DQStartTime.Value.Year.ToString() + "年";

                //KSHBGZLStatisticsDto sumData = new KSHBGZLStatisticsDto()
                //{
                //    Type = "环比汇总",
                //    ComparativeData = result.Sum(r => r.ComparativeData),
                //    CurrentData = result.Sum(r => r.CurrentData),
                //};
                //result.Add(sumData);

                if (rdo_Period.SelectedIndex == 0)
                {
                    gcType.Caption = "周期(周)";
                    GetPeriod(QueryStartTime, QueryEndTime);
                    List<KSHBGZLStatisticsDto> QueryDataList = new List<KSHBGZLStatisticsDto>();
                    for (int i = 0; i < YearList.Count; i++)
                    {
                        KSHBGZLStatisticsDto addweekRow = new KSHBGZLStatisticsDto();
                        addweekRow.Type = YearList[i].Type;
                        addweekRow.CurrentData = result.Where(r => Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                        > YearList[i].STtime && Convert.ToDateTime((QueryStartTime.Year + "-" + r.Type))
                        < YearList[i].EDtime).Sum(r => r.CurrentData);
                        List<Statistic> GetLastYearRow = LastYearList.Where(n => n.Type.Equals(YearList[i].Type)).ToList();
                        addweekRow.ComparativeData = result.Where(r => Convert.ToDateTime((QueryEndTime.Year - 1 + "-" + r.Type))
                        > GetLastYearRow[0].STtime && Convert.ToDateTime((QueryEndTime.Year - 1 + "-" + r.Type))
                        < GetLastYearRow[0].EDtime).Sum(r => r.ComparativeData);
                        QueryDataList.Add(addweekRow);
                        //YearList
                        //  LastYearList
                    }
                    result = QueryDataList;
                }
                if (rdo_Period.SelectedIndex == 1)
                {
                    gcType.Caption = "周期(月)";
                    //result = result.Where(r => Convert.ToInt32(r.Type.Substring(0, r.Type.Length - 1)) >= QueryStartTime.Month
                    //                    && Convert.ToInt32(r.Type.Substring(0, r.Type.Length - 1)) <= endMoth).ToList();
                    result = result.ToList();
                }
                if (rdo_Period.SelectedIndex == 2)
                {
                    gcType.Caption = "周期(季)";
                    List<KSHBGZLStatisticsDto> QueryDataList = new List<KSHBGZLStatisticsDto>();
                    foreach (var item in QueryList())
                    {
                        KSHBGZLStatisticsDto QueryData = new KSHBGZLStatisticsDto();
                        QueryData.Type = item.Type;
                        QueryData.CurrentData = result.Where(r => item.Query.Contains(r.Type)).Sum(r => r.CurrentData);
                        QueryData.ComparativeData = result.Where(r => item.Query.Contains(r.Type)).Sum(r => r.ComparativeData);
                        QueryDataList.Add(QueryData);
                    }
                    result = QueryDataList.Where(r => r.ComparativeData != 0 || r.CurrentData != 0).ToList();
                }
                ToLineGragh(result);
                dgc.DataSource = result;
                splashScreenManager.CloseWaitForm();
            }
            catch (UserFriendlyException ex)
            {
                XtraMessageBox.Show(ex.Description, ex.Code.ToString(), ex.Buttons, ex.Icon);
                splashScreenManager.CloseWaitForm();
                return;
            }
        }

        /// <summary>
        /// 把接收到的数据转换额外柱状图表所需要的数据, 绘制图表
        /// </summary>
        private void ToLineGragh(List<KSHBGZLStatisticsDto> data)
        {
            string xBindName = "type";
            string yBindName = "CurrentData";
            string seriesName = gcCurrentData.Caption;
            ViewType seriesType = ViewType.Bar;
            if (cob_LeiXing.Text == "饼图")
            {
                seriesType = ViewType.Pie;
            }
            else if (cob_LeiXing.Text == "折线图")
            {
                seriesType = ViewType.Line;
            }
            ChartControl.Series.Clear();
            CreateSeries(ChartControl, seriesName, seriesType, data, xBindName, yBindName, null);
            seriesName = gcComparativeData.Caption;
            yBindName = "ComparativeData";
            CreateSeries(ChartControl, seriesName, seriesType, data, xBindName, yBindName, null);
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
        public void CreateSeries(ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string xBindName, string yBindName,
            Action<Series> createSeriesRule)
        {
            if (chat == null)
                throw new ArgumentNullException("chat");
            if (string.IsNullOrEmpty(seriesName))
                throw new ArgumentNullException("seriesType");
            if (string.IsNullOrEmpty(xBindName))
                throw new ArgumentNullException("xBindName");
            if (string.IsNullOrEmpty(yBindName))
                throw new ArgumentNullException("yBindName");
            Series _series = new Series(seriesName, seriesType);
            _series.ArgumentScaleType = ScaleType.Qualitative;
            _series.ArgumentDataMember = xBindName;
            _series.ValueDataMembers[0] = yBindName;

            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);
        }

        /// <summary>
        /// 获取指定期间的起止日期
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        List<Statistic> YearList;
        List<Statistic> LastYearList;
        public void GetPeriod(DateTime beginDate, DateTime endDate)
        {
            YearList = new List<Statistic>();
            LastYearList = new List<Statistic>();
            int Syear = beginDate.Year;
            beginDate = beginDate.AddDays(1 - Convert.ToInt32(beginDate.DayOfWeek.ToString("d")));  //本周周一
            int weeks = GetWeekOfYear(beginDate) - 1;//本年第几周
            GetWeek(Syear - 1, weeks + 1);//去年第几周的开始时间和结束时间
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
            //往年查询周数
            int weeks2 = weeks - YearList.Count;
            for (int i = 0; i < YearList.Count; i++)
            {
                if (i != 0)
                    dtweekStart = dtweekStart.AddDays(6);
                weeks2 = weeks2 + 1;
                Statistic result2 = new Statistic();
                result2.Type = "第" + weeks2 + "周";
                result2.STtime = dtweekStart;
                result2.EDtime = dtweekStart.AddDays(7);
                LastYearList.Add(result2);
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
        public class Statistic
        {
            public string Type { get; set; }
            public DateTime STtime { get; set; }
            public DateTime EDtime { get; set; }
            public List<string> Query { get; set; }
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            dgv.CustomExport("科室环比工作量统计");
        }
    }
}