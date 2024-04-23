
using DevExpress.XtraCharts;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.DiseaseStatistics
{
    public partial class Disease : UserBaseForm
    {
        private readonly IPrintPreviewAppService _printPreviewAppService;
        private readonly IInspectionTotalAppService _InspectionTotalAppService;
        private readonly ISummarizeAdviceAppService _SummarizeAdviceAppService;
        private readonly ICustomerAppService _ICustomerAppService;
        /// <summary>
        /// 分布
        /// </summary>
        DataTable dtrowsfb = new DataTable();
        /// <summary>
        /// 总检建议列表
        /// </summary>
        List<SearchSummarizeAdviceDto> lstSummarizeSummarizeNameDto = new List<SearchSummarizeAdviceDto>();
        /// <summary>
        /// 体检人
        /// </summary>
        List<DisageSumDTO> DisageSumDTOs = new List<DisageSumDTO>();
        /// <summary>
        /// 体检人排序后
        /// </summary>
        List<DisageSumDTO> DisageCusSumDTOs = new List<DisageSumDTO>();
        public Disease()
        {
            InitializeComponent();
            _printPreviewAppService = new PrintPreviewAppService();
            _InspectionTotalAppService = new InspectionTotalAppService();
            _SummarizeAdviceAppService = new SummarizeAdviceAppService();
            _ICustomerAppService = new CustomerAppService();
        }

        #region 事件

        #region 导出

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excle文件（*.xls）|*.xls|文件（*.xls）|*.xls";
                saveFileDialog.Title = "导出疾患分布.xls";

                DialogResult dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {

                    bandedGridView2.OptionsPrint.PrintHeader = false;
                    bandedGridView2.OptionsPrint.AutoWidth = false;
                    bandedGridView2.ExportToXls(saveFileDialog.FileName);
                    chartControl2.ExportToXls(Path.GetDirectoryName(saveFileDialog.FileName) + "\\疾患分布前十.xls");
                    gridView2.ExportToXls(Path.GetDirectoryName(saveFileDialog.FileName) + "\\疾患分布前十.xls");
                    DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        #endregion
        #region 疾病列表
        private void luesexlb_EditValueChanged(object sender, EventArgs e)
        {
            InitTreeFunction("", luesexlb.EditValue.ToString());
        }
        #endregion
        #region 切换图表
        private void ckdzzt_CheckedChanged(object sender, EventArgs e)
        {
            BindChar(dtrowsfb);
            ckdzzt.Checked = true;
            ckebt.Checked = false;
        }
        private void ckebt_CheckedChanged(object sender, EventArgs e)
        {
            BindChar(dtrowsfb);
            ckdzzt.Checked = false;
            ckebt.Checked = true;
        }
        #endregion
        #region 树选择
        private void treeFunction_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CheckSelect(e.Node, e.Node.Checked);
        }

        private void treeFunction_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckSelect(e.Node, e.Node.Checked);
        } 
        #endregion
        #region 系统加载
        private void Disease_Load(object sender, EventArgs e)
        {
            //设置报表样式
            bandedGridView2.OptionsView.ShowColumnHeaders = false;
            bandedGridView2.OptionsView.ShowGroupPanel = false;
            for (int i = 0; i < bandedGridView2.Columns.Count; i++)
            {
                bandedGridView2.Columns[i].MinWidth = 100;

            }
            bandedGridView2.Columns[1].MinWidth = 120;
            loadSerData();
        }


        #endregion
        #region 切换
        private void xtratj_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtratj.SelectedTabPage.Text == "前十分布")
            {
                if(dtrowsfb==null)
                {
                    BingData();
                }
                BindChar(dtrowsfb);
            }
            else if (xtratj.SelectedTabPage.Text == "人员统计")
            {
                if (dtrowsfb == null)
                {
                    BingData();
                }
                gridControl1.DataSource = DisageSumDTOs;
            }
        }
        #endregion
        #region 打印
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bandedGridView2.OptionsPrint.PrintHeader = false;
            bandedGridView2.OptionsPrint.AutoWidth = false;
            bandedGridView2.Print();
            chartControl2.Print();
            gridView2.Print();
            DevExpress.XtraEditors.XtraMessageBox.Show("打印完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region 查询
        private void sbQuery_Click(object sender, EventArgs e)
        {
            if(xtratj.SelectedTabPage.Text=="统计结果")
            {
                BingData();
            }
            else if(xtratj.SelectedTabPage.Text == "前十分布")
            {
                BingData();
                BindChar(dtrowsfb);
            }
            else if (xtratj.SelectedTabPage.Text == "人员统计")
            {
                BingData();
                gridControl1.DataSource = DisageSumDTOs;
            }


        }
        #endregion
        #region 置空
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sleDW.EditValue = null;
            luesex.EditValue = null;
            luesexlb.EditValue = null;
            deTJRQT.EditValue = null;
            deTJRQO.EditValue = null;
        } 
        #endregion
        #region 建议检索
        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            InitTreeFunction(searchControl1.Text, "");
        } 
        #endregion
        #endregion

        #region 方法
        #region 初始化数据
        private void loadSerData()
        {
            deTJRQO.EditValue = DateTime.Now;
            deTJRQT.EditValue = DateTime.Now;
            //性别
            var sexlist = SexHelper.GetSexModelsForItemInfo();
            luesex.Properties.DataSource = sexlist;
            //单位
            sleDW.Properties.DataSource = _printPreviewAppService.GetClientInfos();
            //疾病类别
            var lblist = IllnessSateHelp.GetIfTypeModels();
            luesexlb.Properties.DataSource = lblist;
            lstSummarizeSummarizeNameDto = _SummarizeAdviceAppService.QueryAll();
            InitTreeFunction("","");
        }
        #endregion
        #region 初始化树
        private void InitTreeFunction(string strname,string strtype)
        {
            this.treeFunction.Nodes.Clear();
            treeFunction.CheckBoxes = true;

            //初始化全部功能树
            TreeNode parentNode = this.treeFunction.Nodes.Add("", "疾病名称");
            List<string> lstStrDepName = new List<string>();
            foreach (var typeInfo in lstSummarizeSummarizeNameDto)
            {
                if (lstStrDepName.Contains(typeInfo.Name.ToString()))
                {
                    continue;
                }
                else
                {
                    lstStrDepName.Add(typeInfo.Name.ToString());
                }
                
                TreeNode ptNode = new TreeNode(typeInfo.Name.ToString());
                parentNode.Nodes.Add(ptNode);

                AddFunctionNode(ptNode, typeInfo.Name, strname, strtype);
            }
            treeFunction.Nodes[0].Expand();
            treeFunction.SelectedNode = treeFunction.Nodes[0];
            treeFunction.Focus();

            this.treeFunction.EndUpdate();
        }
        #endregion
        #region 初始化功能树
        /// <summary>
        /// 初始化功能树
        /// </summary>
        private void AddFunctionNode(TreeNode node, string DepartmentName, string strname,string strtype)
        {
            List<SearchSummarizeAdviceDto> lstselect = new List<SearchSummarizeAdviceDto>();
          
            if (strtype == "8")
            {
                lstselect = (from c in lstSummarizeSummarizeNameDto
                             where c.Name == DepartmentName && c.DiagnosisAType.ToString() == strtype
                             select c).ToList();
            }
            else
            {
                lstselect = (from c in lstSummarizeSummarizeNameDto
                             where c.Name == DepartmentName 
                             select c).ToList();
            }
            foreach (var info in lstselect)
            {
                if (info.AdviceName.ToString().Contains(strname) || strname == "")
                {
                    TreeNode subNode = new TreeNode(info.AdviceName.ToString());
                    subNode.Tag = info;
                    node.Nodes.Add(subNode);
                }

            }
        }
        #endregion
        #region 查询统计图表
        private void BindChar(DataTable dtss )
        {
            ViewType vt;
            if (ckdzzt.Checked == true)
            {
                vt = ViewType.Bar;

            }
            else
            {
                vt = ViewType.Pie;
            }
            try
            {
                chartControl2.Series.Clear();
                //Series series1 = new Series("病患统计", ViewType.Pie);
                Series series2 = new Series("病患统计", vt);
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//每个标签显示数据 
                series2.Label.PointOptions.PointView = PointView.ArgumentAndValues;   // 设置Label显示方式  
                //mySeries.ToolTipPointPattern = "hello world";   // 自定义ToolTip显示  
                series2.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                chartControl2.Series.Add(series2);

                DataTable tableMain = dtrowsfb;
                tableMain.TableName = "病患统计";

                DataView dv = tableMain.DefaultView;
                dv.Sort = "SummarizeNameNum desc";
                DataTable dt2 = dv.ToTable();
                SeriesPoint point = null;
                for (int i = 0; i < 11; i++)
                {
                    if (dt2.Rows.Count > i)
                    {
                        if (dt2.Rows[i]["SummarizeNameName"] != null && i < dt2.Rows.Count)
                        {
                            point = new SeriesPoint(dt2.Rows[i]["SummarizeNameName"].ToString());
                            double[] vals = { Convert.ToDouble(dt2.Rows[i]["SummarizeNameNum"]) };
                            point.Values = vals;
                            series2.Points.Add(point);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
        #region 人员统计
        private void BindGrid(DataTable dtss)
        {
            
        }

        #endregion
        #region 查询统计
        private void BingData()
        {
            dtrowsfb = getDataSummarizeNamefb();
            QueryCustomerRegDto queryCustomerRegDto = new QueryCustomerRegDto();
            queryCustomerRegDto.Customer = new QueryCustomerDto();
            if (luesex.EditValue != null)
                queryCustomerRegDto.sex = int.TryParse(luesex.EditValue.ToString(), out var sex) ? (int?)sex : null;
            //遍历控件 登记时间
            if (ceTJRQQ.Checked)
            {
                if (Convert.ToDateTime(deTJRQT.Text) > Convert.ToDateTime(deTJRQO.Text))
                {
                    dxErrorProvider.SetError(deTJRQT, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
                    deTJRQT.Focus();
                    return;
                }

                //体检时间(用导诊时间来传值)
                queryCustomerRegDto.LoginDateStartTime = Convert.ToDateTime(deTJRQT.Text);
                queryCustomerRegDto.LoginDateEndTime = Convert.ToDateTime(deTJRQO.Text);
            }
            if (Convert.ToInt32(seO.Value) > Convert.ToInt32(seT.Value))
            {
                dxErrorProvider.SetError(seT, string.Format(Variables.GreaterThanTips, "起始年龄", "结束年龄"));
                seT.Focus();
                return;
            }
            if (sleDW.EditValue != null)
            {
                queryCustomerRegDto.ClientInfoId = (Guid)sleDW.EditValue;
            }
            //年龄
            queryCustomerRegDto.AgeStart = Convert.ToInt32(seO.Value);
            queryCustomerRegDto.AgeEnd = Convert.ToInt32(seT.Value);
            DevExpress.Utils.WaitDialogForm sdfs = new DevExpress.Utils.WaitDialogForm("提示", "正在加载用户信息......");
            DisageSumDTOs = _ICustomerAppService.GetCustomerDisageSum(queryCustomerRegDto);
            DisageCusSumDTOs = new List<DisageSumDTO>();
            sdfs.Close();
            DataTable dtshow = getDataAgeFb();
            dtshow.Columns["总人数"].DataType = typeof(int);
            DevExpress.Utils.WaitDialogForm sdf = new DevExpress.Utils.WaitDialogForm("提示", "正在加载用户信息......");
            int inmnu = 0;
            int inwdlnum = 0;
            DataTable dtrows = LstSummarizeName();
            #region 遍历获取数据
            int num = 1;
            List<string> illnames = new List<string>();
            foreach (DataRow item in dtrows.Rows)
            {

                inmnu++;
                sdf.SetCaption("加载进度" + inmnu + "/" + dtrows.Rows.Count);
                //  var cli = from c in DisageSumDTOs where c.CharacterSummary.Contains(item["SummarizeNameName"].ToString()) select c;
                List<DisageSumDTO> ls = new List<DisageSumDTO>();
                List<DisageSumDTO> clsSummarizeName = new List<DisageSumDTO>();
                var cli = from c in DisageSumDTOs where c.SummarizeName==item["SummarizeNameName"].ToString() select c;
                clsSummarizeName = cli.ToList();

                illnames.Add(item["SummarizeNameName"].ToString());
                if (clsSummarizeName.Count > 0)
                {
                    DataRow drnew = dtshow.NewRow();
                    DataRow drfbnew = dtrowsfb.NewRow();
                    drfbnew[0] = item["SummarizeNameName"].ToString();
                    drfbnew[1] = clsSummarizeName.Count;
                    drfbnew[2] = ((clsSummarizeName.Count / DisageSumDTOs.Count) * 100).ToString("00.00") + "%";
                    dtrowsfb.Rows.Add(drfbnew);

                    drnew[0] = item["SummarizeNameName"].ToString();
                    drnew[1] = clsSummarizeName.Count;
                    var cnan = from c in clsSummarizeName where c.Sex == 1 select c;
                    List<DisageSumDTO> lnan = cnan.ToList();
                    drnew[2] = lnan.Count;
                    drnew[3] = (((double)lnan.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    var cnv = from c in clsSummarizeName where c.Sex == 2 select c;
                    List<DisageSumDTO> lnv = cnv.ToList();
                    drnew[4] = lnv.Count;
                    drnew[5] = (((double)lnv.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    var cage1 = from c in clsSummarizeName where Convert.ToInt16( c.Age) < 20 select c;
                    List<DisageSumDTO> lage1 = cage1.ToList();
                    drnew[6] = lage1.Count;
                    drnew[7] = (((double)lage1.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w1 = lage1.Where(p => p.Sex == 1).ToList();
                    var m1 = lage1.Where(p => p.Sex == 2).ToList();
                    drnew[25] = w1.Count;
                    drnew[26] = (((double)w1.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[27] = m1.Count;
                    drnew[28] = (((double)m1.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage2 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 20 && Convert.ToInt16( c.Age) < 30 select c;
                    List<DisageSumDTO> lage2 = cage2.ToList();
                    drnew[8] = lage2.Count;
                    drnew[9] = (((double)lage2.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w2 = lage2.Where(p => p.Sex == 1).ToList();
                    var m2 = lage2.Where(p => p.Sex == 2).ToList();
                    drnew[29] = w2.Count;
                    drnew[30] = (((double)w2.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[31] = m2.Count;
                    drnew[32] = (((double)m2.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage3 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 30 && Convert.ToInt16( c.Age) < 40 select c;
                    List<DisageSumDTO> lage3 = cage3.ToList();
                    drnew[10] = lage3.Count;
                    drnew[11] = (((double)lage3.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w3 = lage3.Where(p => p.Sex == 1).ToList();
                    var m3 = lage3.Where(p => p.Sex == 2).ToList();
                    drnew[33] = w3.Count;
                    drnew[34] = (((double)w3.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[35] = m3.Count;
                    drnew[36] = (((double)m3.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    var cage4 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 40 && Convert.ToInt16( c.Age) < 50 select c;
                    List<DisageSumDTO> lage4 = cage4.ToList();
                    drnew[12] = lage4.Count;
                    drnew[13] = (((double)lage4.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w4 = lage4.Where(p => p.Sex == 1).ToList();
                    var m4 = lage4.Where(p => p.Sex == 2).ToList();
                    drnew[37] = w4.Count;
                    drnew[38] = (((double)w4.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[39] = m4.Count;
                    drnew[40] = (((double)m4.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage5 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 50 && Convert.ToInt16( c.Age) < 60 select c;
                    List<DisageSumDTO> lage5 = cage5.ToList();
                    drnew[14] = lage5.Count;
                    drnew[15] = (((double)lage5.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w5 = lage5.Where(p => p.Sex == 1).ToList();
                    var m5 = lage5.Where(p => p.Sex == 2).ToList();
                    drnew[41] = w5.Count;
                    drnew[42] = (((double)w5.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[43] = m5.Count;
                    drnew[44] = (((double)m5.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage6 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 60 && Convert.ToInt16( c.Age) < 70 select c;
                    List<DisageSumDTO> lage6 = cage6.ToList();
                    drnew[16] = lage6.Count;
                    drnew[17] = (((double)lage6.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w6 = lage6.Where(p => p.Sex == 1).ToList();
                    var m6 = lage6.Where(p => p.Sex == 2).ToList();
                    drnew[45] = w6.Count;
                    drnew[46] = (((double)w6.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[47] = m6.Count;
                    drnew[48] = (((double)m6.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage7 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 70 && Convert.ToInt16( c.Age) < 80 select c;
                    List<DisageSumDTO> lage7 = cage7.ToList();
                    drnew[18] = lage7.Count;
                    drnew[19] = (((double)lage7.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w7 = lage7.Where(p => p.Sex == 1).ToList();
                    var m7 = lage7.Where(p => p.Sex == 2).ToList();
                    drnew[49] = w7.Count;
                    drnew[50] = (((double)w7.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[51] = m7.Count;
                    drnew[52] = (((double)m7.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage8 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 80 && Convert.ToInt16( c.Age) < 90 select c;
                    List<DisageSumDTO> lage8 = cage8.ToList();
                    drnew[20] = lage8.Count;
                    drnew[21] = (((double)lage8.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w8 = lage8.Where(p => p.Sex == 1).ToList();
                    var m8 = lage8.Where(p => p.Sex == 2).ToList();
                    drnew[53] = w8.Count;
                    drnew[54] = (((double)w8.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[55] = m8.Count;
                    drnew[56] = (((double)m8.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";


                    var cage9 = from c in clsSummarizeName where Convert.ToInt16( c.Age) >= 90 select c;
                    List<DisageSumDTO> lage9 = cage9.ToList();
                    drnew[22] = lage9.Count;
                    drnew[23] = (((double)lage9.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //性别年龄段
                    var w9 = lage9.Where(p => p.Sex == 1).ToList();
                    var m9 = lage9.Where(p => p.Sex == 2).ToList();
                    drnew[57] = w9.Count;
                    drnew[58] = (((double)w9.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";
                    drnew[59] = m9.Count;
                    drnew[60] = (((double)m9.Count / (double)clsSummarizeName.Count) * 100).ToString("00.00") + "%";

                    //  drnew[24] = num;
                    dtshow.Rows.Add(drnew);
                    num = num + 1;
                }


            }
            DisageSumDTOs = DisageSumDTOs.Where(p => illnames.Contains(
                 p.SummarizeName)).ToList();

            #endregion
            sdf.Close();
            DataView dv1 = new DataView(dtshow);
            dv1.Sort = "总人数 desc";
            dtshow = dv1.ToTable();
            grdsum.DataSource = dtshow;

        }
        #endregion
        #region 获取建议
        public DataTable LstSummarizeName()
        {
            DataTable dt = getDataSummarizeName();
            List<string> lstr = getSummarizeNameNode();
            foreach (string item in lstr)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item;
                dt.Rows.Add(dr);
            }
            return dt;
        } 
        #endregion
        #region 获取所有选择的疾病名称
        public List<string> getSummarizeNameNode()
        {
            List<string> lstr = new List<string>();
            foreach (TreeNode item in this.treeFunction.Nodes[0].Nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    foreach (TreeNode itemnode in item.Nodes)
                    {
                        if (itemnode.Checked)
                        {
                            if (lstr.Count == 0)
                            {
                                lstr.Add(itemnode.Text);
                            }
                            else
                            {
                                if (!lstr.Contains(itemnode.Text))
                                {
                                    lstr.Add(itemnode.Text);
                                }
                            }
                        }
                    }
                }
            }
            return lstr;
        }
        #endregion
        #region 获取年龄分布DATAtable
        /// <summary>
        /// 获取年龄分布DATAtable
        /// </summary>
        /// <returns></returns>
        public DataTable getDataAgeFb()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("疾病名称");
            dt.Columns.Add("总人数");
            dt.Columns.Add("男人数");
            dt.Columns.Add("男检出率");
            dt.Columns.Add("女人数");
            dt.Columns.Add("女检出率");

            dt.Columns.Add("<20人数");
            dt.Columns.Add("<20检出率");

            dt.Columns.Add("20-30人数");
            dt.Columns.Add("20-30检出率");

            dt.Columns.Add("30-40人数");
            dt.Columns.Add("30-40检出率");


            dt.Columns.Add("40-50人数");
            dt.Columns.Add("40-50检出率");


            dt.Columns.Add("50-60人数");
            dt.Columns.Add("50-60检出率");


            dt.Columns.Add("60-70人数");
            dt.Columns.Add("60-70检出率");


            dt.Columns.Add("70-80人数");
            dt.Columns.Add("70-80检出率");


            dt.Columns.Add("80-90人数");
            dt.Columns.Add("80-90检出率");


            dt.Columns.Add(">90人数");
            dt.Columns.Add(">90检出率");
            dt.Columns.Add("序号");



            #region MyRegion
            dt.Columns.Add("<20男");
            dt.Columns.Add("<20男检出率");
            dt.Columns.Add("<20女");
            dt.Columns.Add("<20女检出率");

            dt.Columns.Add("20-30男");
            dt.Columns.Add("20-30男检出率");
            dt.Columns.Add("20-30女");
            dt.Columns.Add("20-30女检出率");

            dt.Columns.Add("30-40男");
            dt.Columns.Add("30-40男检出率");
            dt.Columns.Add("30-40女");
            dt.Columns.Add("30-40女检出率");


            dt.Columns.Add("40-50男");
            dt.Columns.Add("40-50男检出率");
            dt.Columns.Add("40-50女");
            dt.Columns.Add("40-50女检出率");

            dt.Columns.Add("50-60男");
            dt.Columns.Add("50-60男检出率");
            dt.Columns.Add("50-60女");
            dt.Columns.Add("50-60女检出率");

            

            dt.Columns.Add("60-70男");
            dt.Columns.Add("60-70男检出率");
            dt.Columns.Add("60-70女");
            dt.Columns.Add("60-70女检出率");

            dt.Columns.Add("70-80男");
            dt.Columns.Add("70-80男检出率");
            dt.Columns.Add("70-80女");
            dt.Columns.Add("70-80女检出率");

            dt.Columns.Add("80-90男");
            dt.Columns.Add("80-90男检出率");
            dt.Columns.Add("80-90女");
            dt.Columns.Add("80-90女检出率");

            dt.Columns.Add(">90男");
            dt.Columns.Add(">90男检出率");
            dt.Columns.Add(">90女");
            dt.Columns.Add(">90女检出率");
            #endregion
            return dt;

        }
        #endregion

        #region 获取年龄分布DATAtable
        /// <summary>
        /// 获取年龄分布DATAtable
        /// </summary>
        /// <returns></returns>
        public DataTable getDataSummarizeName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SummarizeNameName");
            return dt;

        }
        #endregion
        #region 获取年龄分布DATAtablefb
        /// <summary>
        /// 获取年龄分布DATAtable
        /// </summary>
        /// <returns></returns>
        public DataTable getDataSummarizeNamefb()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SummarizeNameName");
            dt.Columns.Add("SummarizeNameNum", Type.GetType("System.Int32"));
            dt.Columns.Add("SummarizeNameSign");
            return dt;

        }


        #endregion
        #region 子树选择
        /// <summary>
        /// 子树选择
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selectAll"></param>
        private void CheckSelect(TreeNode node, bool selectAll)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                subNode.Checked = selectAll;

                CheckSelect(subNode, selectAll);
            }
        }



        #endregion

        #endregion

        private void luesex_Properties_Click(object sender, EventArgs e)
        {

        }

        private void luesex_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                luesex.EditValue = null;
        }

        private void luesexlb_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                luesexlb.EditValue = null;
        }

       
    }
}
