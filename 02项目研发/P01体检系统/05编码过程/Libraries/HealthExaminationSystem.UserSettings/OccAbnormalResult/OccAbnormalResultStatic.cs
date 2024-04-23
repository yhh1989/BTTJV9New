using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccAbnormalResult
{
    public partial class OccAbnormalResultStatic : UserBaseForm
    {
        private List<OutOccAbnormalResult> occConclusls;

        public OccAbnormalResultStatic()
        {
            InitializeComponent();
        }
        public OccAbnormalResultStatic(List<OutOccAbnormalResult> input) : this()
        {
            occConclusls = input;
        }

        private void OccAbnormalResultStatic_Load(object sender, EventArgs e)
        {
            var results = occConclusls.GroupBy(o => o.Sex=1).Select(o => new result
            {
                conName = o.Key,
                conCount = o.Count(),

            }).ToList();          
            ToLineGragh(results);
            var result = occConclusls.GroupBy(o => o.Sex = 0).Select(o => new result
            {
                conName = o.Key,
                conCount = o.Count(),

            }).ToList();
            ToLineGraghs(result);
            var resultes = occConclusls.GroupBy(o => o.Name).Select(o => new results
            {
                conName = o.Key,
                conCount = o.Count(),

            }).ToList();
            ToLineGraghes(resultes);

        }
        private void ToLineGraghes(List<results> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "123";
            ViewType seriesType = ViewType.Bar;
            seriesType = ViewType.Bar;

            chartControl1.Series.Clear();
            CreateSeries(chartControl1, seriesName, seriesType, data, xBindName, yBindName, null);
        }
        private void ToLineGraghs(List<result> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "123";
            ViewType seriesType = ViewType.Bar;
            seriesType = ViewType.Bar;
          
            chartControl2.Series.Clear();
            CreateSeries(chartControl2, seriesName, seriesType, data, xBindName, yBindName, null);
        }
        private void ToLineGragh(List<result> data)
        {
            string xBindName = "conName";
            string yBindName = "conCount";
            string seriesName = "123";
            ViewType seriesType = ViewType.Bar;
            seriesType = ViewType.Bar;
            ChartControl.Series.Clear();
            CreateSeries(ChartControl, seriesName, seriesType, data, xBindName, yBindName, null);
            chartControl2.Series.Clear();
            CreateSeries(chartControl2, seriesName, seriesType, data, xBindName, yBindName, null);
        }
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
            _series.Label.TextPattern = "{A}//{V}//{S}";
            _series.DataSource = dataSource;
            if (createSeriesRule != null)
                createSeriesRule(_series);
            chat.Series.Add(_series);


        }
    }
    public class result
        {
            public virtual int? conName { get; set; }
            public virtual int conCount { get; set; }                
        }
    public class results
    {
        public virtual string conName { get; set; }
        public virtual int conCount { get; set; }
    }
}
