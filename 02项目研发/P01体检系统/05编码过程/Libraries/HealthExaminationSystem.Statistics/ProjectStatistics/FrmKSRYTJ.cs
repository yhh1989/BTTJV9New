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
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Views.Base;
using HealthExaminationSystem.Enumerations;
using DevExpress.Utils;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.ProjectStatistics
{
    public partial class FrmKSRYTJ: UserBaseForm
    {
        private ICustomerAppService customerSvr;
        public FrmKSRYTJ()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            customerSvr=new CustomerAppService();
            var input = new QueryDepartCustomerRegDto();
            AutoLoading(() =>
            {
                if (checkDate.Checked)
                {
                    input.StartTime = new DateTime(dateEditStart.DateTime.Year, dateEditStart.DateTime.Month, dateEditStart.DateTime.Day, 0, 0, 0);
                    input.EndTime = new DateTime(dateEditEnd.DateTime.Year, dateEditEnd.DateTime.Month, dateEditEnd.DateTime.Day, 23, 59, 59);
                }
                input.Introducer = txtIntroducer.Text.Trim();
                //if (!string.IsNullOrWhiteSpace(cbo_ksmc.EditValue.ToString()))
                //{
                //    var str = cbo_ksmc.EditValue?.ToString()?.Split(',');
                //    if (str != null)
                //    {
                //        var list = new List<Guid>();
                //        foreach (var item in str)
                //            list.Add(Guid.Parse(item));

                //        input.DepartMentIds = list;
                //    }
                //}
                
                //科室
                if (!string.IsNullOrWhiteSpace(searchDep.EditValue?.ToString()))
                {
                    //var strList= searchDep.EditValue as List<Guid>;
                    //var list = new List<Guid>();
                    //foreach (var item in strList)
                    //{
                    //    list.Add(Guid.Parse(item));
                    //}
                    input.DepartMentIds = searchDep.EditValue as List<Guid>;
                }
                if (!string.IsNullOrWhiteSpace(SeachGroup.EditValue?.ToString()))
                {
                    input.ItemGroupId = SeachGroup.EditValue as Guid?;
                }
                if (!string.IsNullOrWhiteSpace(comState.EditValue?.ToString()))
                {
                    input.GroupState = comState.EditValue as int?;
                }
                //var regId = chkcmbDepartment.EditValue;
                var  ClientRegIds = chkcmbDepartment.Properties.GetCheckedItems().ToString().Split(',').Where(p=>p !="" && p !=null).ToList();
                if (ClientRegIds.Count > 0)
                {
                    input.ClientRegIds = ClientRegIds.Select(p => Guid.Parse(p)).ToList();
                }
                var data = customerSvr.QueryDepartMentCustomerReg(input);
                gC.DataSource = data;
            });
        }

        private void FrmKSRYTJ_Load(object sender, EventArgs e)
        {
            //总检状态枚举绑定
            repositoryItemLookUpEditcolSummSate.DataSource = SummSateHelper.GetSelectList();
            comState.Properties.DataSource = ProjectIStateHelper.GetProjectModels();
            comState.Properties.ValueMember = "Id";
            comState.Properties.DisplayMember = "Display";

            //体检状态枚举绑定
            repositoryItemLookUpEdit1.DataSource = PhysicalEStateHelper.YYGetList();
            repositoryItemLookUpEdit2.DataSource = SexHelper.GetSexModels();


            SeachGroup.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            SeachGroup.Properties.ValueMember = "Id";
            SeachGroup.Properties.DisplayMember = "ItemGroupName";

            cbo_ksmc.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            cbo_ksmc.Properties.ValueMember = "Id";
            cbo_ksmc.Properties.DisplayMember = "Name";

            searchDep.Properties.DataSource = CurrentUser.TbmDepartments?.OrderBy(o=>o.OrderNum)?.ToList();// DefinedCacheHelper.GetDepartments();
            searchDep.Properties.ValueMember = "Id";
            searchDep.Properties.DisplayMember = "Name";

            chkcmbDepartment.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto(); //departmentList;
            chkcmbDepartment.Properties.DisplayMember = "ClientName";
            chkcmbDepartment.Properties.ValueMember = "Id";

            repositoryItemSearchLookUpEdit1.DataSource = DefinedCacheHelper.GetDepartments();

            dateEditEnd.EditValue = DateTime.Now;
            dateEditStart.EditValue = DateTime.Now;
            checkDate.Checked = true;

            //gV.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "CustomerBM", gridColumn3, "{0:N3}");
            //gV.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Age", gridColumn7, "{0:N0}");
            //gV.Columns[gridColumn7.FieldName].DisplayFormat.FormatType = FormatType.Numeric;

        }

        private void gV_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Value == null)
            //    return;
            //if (e.Column.FieldName == colCheckState.FieldName)
            //{
            //    e.DisplayText = EnumHelper.GetEnumDesc((PhysicalEState)e.Value);
            //}
            //if (e.Column.FieldName == colSex.FieldName)
            //{
            //    e.DisplayText = EnumHelper.GetEnumDesc((Sex)e.Value);
            //}
        }

        private void gV_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo row = e.Info as GridGroupRowInfo;
            GridView gridview = sender as GridView;
            int index = gridview.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            var id= gridview.GetRowCellValue(index, "DepartmentId");
            var name= gridview.GetRowCellValue(index, "DepartMentName");
            var list = gridview.DataSource as List<DepartMentCustomerRegOutPut>;
            var sum= list.Where(o => o.DepartmentId.ToString() == id.ToString()).Count();
            var yijian = list.Where(o => o.DepartmentId.ToString() == id.ToString() && o.CheckState == (int)PhysicalEState.Process).Count();
            var weijian = list.Where(o => o.DepartmentId.ToString() == id.ToString() && o.CheckState == (int)PhysicalEState.Not).Count();
            var wancheng = list.Where(o => o.DepartmentId.ToString() == id.ToString() && o.CheckState == (int)PhysicalEState.Complete).Count();
            var zongjian=list.Where(o => o.DepartmentId.ToString() == id.ToString() && o.SummSate != (int)SummSate.NotAlwaysCheck).Count();
            row.GroupText = "科室："+name+"    总数："+sum+"    未体检:"+weijian+ "    <color='Blue'>体检中：" + yijian+ "</color>   <color='Green'>体检完成：" + wancheng+ "</color>    <color='LightCoral'>已总检：" + zongjian+ "</color>";

        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName == colItemCheckState.FieldName)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((ProjectIState)e.Value);
            }
            else if (e.Column.FieldName == colIsAddMinus.FieldName)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((AddMinusType)e.Value);
            }
            else if (e.Column.FieldName == colPayerCat.FieldName)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
            }
        }

        private void searchDep_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                searchDep.EditValue = null;
                searchLookUpEdit1View.ClearSelection();
                searchDep.ToolTip = "";
                //searchDep.RefreshEditValue();
            }
        }

        private void searchDep_Popup(object sender, EventArgs e)
        {
            //得到当前SearchLookUpEdit弹出窗体
            PopupSearchLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            SearchEditLookUpPopup popup = form.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            LayoutControl layout = popup.Controls.OfType<LayoutControl>().FirstOrDefault();

            //如果窗体内空间没有确认按钮，则自定义确认simplebutton，取消simplebutton，选中结果label
            if (layout.Controls.OfType<Control>().Where(ct => ct.Name == "btOK").FirstOrDefault() == null)
            {
                //得到空的空间
                EmptySpaceItem a = layout.Items.Where(it => it.TypeName == "EmptySpaceItem").FirstOrDefault() as EmptySpaceItem;

                //得到取消按钮，重写点击事件
                Control clearBtn = layout.Controls.OfType<Control>().Where(ct => ct.Name == "btClear").FirstOrDefault();
                LayoutControlItem clearLCI = (LayoutControlItem)layout.GetItemByControl(clearBtn);
                clearBtn.Click += clearBtn_Click;

                //添加一个simplebutton控件(确认按钮)
                LayoutControlItem myLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(clearLCI.Parent);
                myLCI.TextVisible = false;
                SimpleButton btOK = new SimpleButton() { Name = "btOK", Text = "确定" };
                btOK.Click += btOK_Click;
                myLCI.Control = btOK;
                myLCI.SizeConstraintsType = SizeConstraintsType.Custom;//控件的大小设置为自定义
                myLCI.MaxSize = clearLCI.MaxSize;
                myLCI.MinSize = clearLCI.MinSize;
                myLCI.Move(clearLCI, DevExpress.XtraLayout.Utils.InsertType.Left);

                //添加一个label控件（选中结果显示）
                LayoutControlItem msgLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(a.Parent);
                msgLCI.TextVisible = false;
                msgLCI.Move(a, DevExpress.XtraLayout.Utils.InsertType.Left);
                msgLCI.BestFitWeight = 100;
            }
        }


        private void btOK_Click(object sender, EventArgs e)
        {
            searchDep.ClosePopup();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            //luValues.Clear();//将保存的数据清空
            searchLookUpEdit1View.ClearSelection();
            searchDep.EditValue = null;
            searchDep.ToolTip = "";
        }

        private void searchDep_Closed(object sender, ClosedEventArgs e)
        {
            var row = searchLookUpEdit1View.GetSelectedRows();
            if (row.Count() > 0)
            {
                var str = "";
                var values = new List<Guid>();
                foreach (var r in row)
                {
                    var data = searchLookUpEdit1View.GetRow(r) as Sw.Hospital.HealthExaminationSystem.Application.Department.Dto.TbmDepartmentDto;
                    str += data.Name + "，";
                    values.Add(data.Id);
                }
                //txtClientRegId.Text = str;
                searchDep.ToolTip = str;
                searchDep.EditValue = values;
            }
        }

        private void searchDep_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (searchDep.EditValue != null)
                e.DisplayText = searchDep.ToolTip;
            else
                e.DisplayText = "";
        }

        private void FrmKSRYTJ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery.PerformClick();
            }
        }

        private void chkcmbDepartment_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                chkcmbDepartment.EditValue = null;
                chkcmbDepartment.RefreshEditValue();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Export();
            #region 注释的
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "科室体检人员";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gC.ExportToXls(saveFileDialog.FileName);
            #endregion

            //ExcelHelper.GridViewToExcel(gridView1, "科室体检人员", "科室体检人员");
            //ExcelHelper.ExportToExcel("科室体检人员", gC);

 
        }
        public static void ExportToExcel(System.Windows.Forms.SaveFileDialog saveFileDialog, params DevExpress.XtraPrinting.IPrintable[] panels)
        {

            string FileName = saveFileDialog.FileName;
            var ps = new DevExpress.XtraPrinting.PrintingSystem();
            var link = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            ps.Links.Add(link);
            foreach (var panel in panels)
            {
                link.Links.Add(CreatePrintableLink(panel));
            }
            link.Landscape = true;//横向
                                  //判断是否有标题，有则设置
                                  //link.CreateDocument(); //建立文档
            int count = 1;
            //在重复名称后加（序号）
            while (System.IO.File.Exists(FileName))
            {
                if (FileName.Contains(")."))
                {
                    int start = FileName.LastIndexOf("(");
                    int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                    FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                }
                else
                {
                    FileName = FileName.Replace(".", string.Format("({0}).", count));
                }
                count++;
            }
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                var options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXlsx(FileName, options);
            }
            else
            {
                var options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                link.ExportToXls(FileName, options);
            }
        }
        /// <summary>
        /// 创建打印Componet
        /// </summary>
        /// <param name="printable"></param>
        /// <returns></returns>
        public static DevExpress.XtraPrinting.PrintableComponentLink CreatePrintableLink(DevExpress.XtraPrinting.IPrintable printable)
        {
            var chart = printable as DevExpress.XtraCharts.ChartControl;
            if (chart != null)
                chart.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            var printableLink = new DevExpress.XtraPrinting.PrintableComponentLink() { Component = printable };
            return printableLink;
        }
    }
}