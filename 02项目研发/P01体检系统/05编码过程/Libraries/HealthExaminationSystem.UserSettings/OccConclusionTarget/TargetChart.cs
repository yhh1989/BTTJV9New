using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionTarget;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionTarget
{
    public partial class TargetChart : UserBaseForm
    {
        private readonly IOccConclusionTargetAppService _OccConclusionTargetAppService = new OccConclusionTargetAppService();
        private List<OccConclusionTargetDto> occTarget;

        public TargetChart()
        {
            InitializeComponent();
        }
        public TargetChart(List<OccConclusionTargetDto> input) : this()
        {
            occTarget = input;
        }
        private void TargetChart_Load(object sender, EventArgs e)
        {
            try
            {

                var results = occTarget.GroupBy(o => o.Target).Select(o => new result
                {
                    conName = o.Key,
                    conCount = o.Count()
                }).ToList();
                ToLineGragh(results);

            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        public class result
        {
            public virtual string conName { get; set; }
            public virtual int conCount { get; set; }

        }
        /// <summary>
        /// 把接收到的数据转换额外柱状图表所需要的数据, 绘制图表
        /// </summary>
        private void ToLineGragh(List<result> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "统计图";
            ViewType seriesType = ViewType.Bar;
            seriesType = ViewType.Bar;
            ChartControl.Series.Clear();
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
            _series.ValueDataMembers.AddRange(yBindName);

            _series.ShowInLegend = true;
            //_series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
            _series.Label.TextPattern = "{A}//{V}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);


        }
    }
}
