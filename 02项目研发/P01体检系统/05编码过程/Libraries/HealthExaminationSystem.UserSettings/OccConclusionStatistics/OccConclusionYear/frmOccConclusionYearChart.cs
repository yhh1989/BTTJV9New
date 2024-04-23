using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics.OccConclusionYear
{
    public partial class frmOccConclusionYearChart : UserBaseForm
    {
        private List<outresut> occConclusls;
        private List<GridColumn> gridColumns = new List<GridColumn>();
        public frmOccConclusionYearChart()
        {
            InitializeComponent();
        }
        public frmOccConclusionYearChart(List<outresut> input, List<GridColumn> grids) :this()
        {
            
            occConclusls = input;
            gridColumns = grids;

        }

        private void frmOccConclusionYearChart_Load(object sender, EventArgs e)
        {
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

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ToLineGragh(occConclusls);
        }
        public void Init(ChartControl chartControl2, ViewType seriesType, object dataSource)
        {
            if (chartControl2 == null)
                throw new ArgumentNullException("chat");

            chartControl2.Series.Clear();

            //获取曲线图所要显示的数据
            List<outresut> outOccMothDtos = (List<outresut>)dataSource;
            SeriesPoint point = null;
         

            foreach (var serie in outOccMothDtos)
            {
                string clientName = serie.ClientName;
                if (string.IsNullOrWhiteSpace(clientName))
                {
                    clientName = "个人";
                }
                Series series1 = new Series(clientName, seriesType);
                chartControl2.Series.Add(series1);

                foreach (var con in gridColumns)
                {
                    point = new SeriesPoint("["+con.Name.ToString()+"年]");
                    double[] vals = { Convert.ToDouble(serie.outOccMothDtos.FirstOrDefault(o => o.ConName ==int.Parse(con.Name.ToString()))?.ConCount) };
                    point.Values = vals;
                    series1.Points.Add(point);
                }

                // BarSeriesView sv1 = (BarSeriesView)series1.View;           
                //sv1.BarWidth = 0.5;           
                // sv1.Color = Color.Red;            
            }
            string mo = "年";          

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
    }
}
