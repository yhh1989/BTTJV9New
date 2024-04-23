using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic
{
    public partial class frmOccDayStatic : UserBaseForm
    {
        private List<outresut> occConclusls;
        private int MoDay = 0; 

        public frmOccDayStatic()
        {
            InitializeComponent();
        }
        public frmOccDayStatic(List<outresut> input,int MD) : this()
        {
                MoDay = MD;
               occConclusls = input;
        }
        private void OccDayStatic_Load(object sender, EventArgs e)
        {
           
            //var list = occConclusls.SelectMany(o=>o.outOccMothDtos).ToList();
            ToLineGragh(occConclusls);
        }
        private void ToLineGragh(List<outresut> data)
        {
            ViewType seriesType = ViewType.Area;
            if (comboBoxEdit1.Text.Contains("饼状图"))
            {
                seriesType = ViewType.Pie;
            }
            else if (comboBoxEdit1.Text.Contains("折线图"))
            {
                
                seriesType = ViewType.Line;
            }
            else if (comboBoxEdit1.Text.Contains("柱状图"))
            {
             
                seriesType = ViewType.Bar;
            }
            

            ChartControl.Series.Clear();        
            Init(ChartControl, seriesType, data);
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


        public void Init( ChartControl chartControl2, ViewType seriesType, object dataSource)
        {
            if (chartControl2 == null)
                throw new ArgumentNullException("chat");       
           
            chartControl2.Series.Clear();
           
            //获取曲线图所要显示的数据
            List<outresut> outOccMothDtos = (List<outresut>)dataSource;
            SeriesPoint point = null;
            int ConCount = 0;
           
            foreach (var serie in outOccMothDtos)
            {
                string clientName = serie.ClientName;
                if (string.IsNullOrWhiteSpace( clientName))
                {
                    clientName = "个人";
                }
                Series series1 = new Series(clientName, seriesType);               
                chartControl2.Series.Add(series1);

                for (int i =1; i <= MoDay; i++)
                {
                    point = new SeriesPoint(i.ToString());
                    double[] vals = { Convert.ToDouble(serie.outOccMothDtos.FirstOrDefault(o=>o.ConName==i)?.ConCount) };
                    point.Values = vals;
                    series1.Points.Add(point);
                }
               
               // BarSeriesView sv1 = (BarSeriesView)series1.View;           
                //sv1.BarWidth = 0.5;           
               // sv1.Color = Color.Red;            
            }
            string mo = "天";
            if (MoDay == 12)
            {
                mo = "月份";
            }


            if (chartControl2.Diagram.GetType().Name == "XYDiagram")
            {

                XYDiagram xyDiagram = (XYDiagram)chartControl2.Diagram;

                xyDiagram.AxisX.Title.Text = mo;
                xyDiagram.AxisX.Title.Alignment = StringAlignment.Center;
                xyDiagram.AxisX.Title.EnableAntialiasing = 0;
                xyDiagram.AxisX.Title.Visibility = 0;
                xyDiagram.AxisX.Title.Font = new Font("微软雅黑", 14, FontStyle.Regular);

                xyDiagram.AxisY.Title.Text = "人数";
                xyDiagram.AxisY.Title.Alignment = StringAlignment.Center;
                xyDiagram.AxisY.Title.EnableAntialiasing = 0;
                xyDiagram.AxisY.Title.Visibility = 0;
                xyDiagram.AxisY.Title.Font = new Font("微软雅黑", 14, FontStyle.Regular);
            }
        }
        public class result
        {
            public virtual int? conName { get; set; }
            public virtual int conCount { get; set; }
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ToLineGragh(occConclusls);
        }
    }
}
