using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列配置
    /// </summary>
    public partial class FormDynamicColumnConfiguration : UserBaseForm
    {
        /// <summary>
        /// 初始化“动态列配置”
        /// </summary>
        public FormDynamicColumnConfiguration()
        {
            InitializeComponent();
            ColumnBindingList = new BindingList<DynamicColumnConfigurationDtoNo1>();
            gridControlColumn.DataSource = ColumnBindingList;
        }

        /// <summary>
        /// 待编辑表格视图
        /// </summary>
        public GridView CurrentGridView { get; set; }

        /// <summary>
        /// 待编辑表格标识
        /// </summary>
        public string CurrentGridViewId { get; set; }

        /// <summary>
        /// 列绑定列表
        /// </summary>
        private BindingList<DynamicColumnConfigurationDtoNo1> ColumnBindingList { get; set; }

        /// <summary>
        /// 窗体首次加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDynamicColumnConfiguration_Load(object sender, EventArgs e)
        {
            if (CurrentGridView != null && !string.IsNullOrWhiteSpace(CurrentGridViewId))
            {
                foreach (GridColumn column in CurrentGridView.Columns)
                {
                    var row = new DynamicColumnConfigurationDtoNo1();
                    row.GridViewId = CurrentGridViewId;
                    row.GridViewColumnName = column.Name;
                    row.GridViewColumnCaption = column.Caption;
                    row.Visible = column.Visible;
                    row.VisibleIndex = column.VisibleIndex;
                    if (column.Fixed == FixedStyle.None)
                    {
                        row.Fixed = false;
                    }
                    else
                    {
                        row.Fixed = true;
                        row.IsLeft = column.Fixed == FixedStyle.Left;
                    }
                    ColumnBindingList.Add(row);
                }

                gridViewColumn.BestFitColumns();
            }
        }

        /// <summary>
        /// 操作列按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEditId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag == null)
            {
                return;
            }
            if (e.Button.Tag.Equals(1))
            {
                // 上移
                if (gridViewColumn.GetFocusedRow() is DynamicColumnConfigurationDtoNo1 row)
                {
                    if (row.VisibleIndex > 0)
                    {
                        var newIndex = row.VisibleIndex - 1;
                        var oldRow = ColumnBindingList.FirstOrDefault(r => r.VisibleIndex == newIndex && r.Fixed == row.Fixed);
                        if (oldRow != null)
                        {
                            oldRow.VisibleIndex = row.VisibleIndex;
                            row.VisibleIndex = newIndex;
                        }
                    }
                }
            }
            else if (e.Button.Tag.Equals(2))
            {
                // 下移
                if (gridViewColumn.GetFocusedRow() is DynamicColumnConfigurationDtoNo1 row)
                {
                    if (row.VisibleIndex >= 0)
                    {
                        var newIndex = row.VisibleIndex + 1;
                        var oldRow = ColumnBindingList.FirstOrDefault(r => r.VisibleIndex == newIndex && r.Fixed == row.Fixed);
                        if (oldRow != null)
                        {
                            oldRow.VisibleIndex = row.VisibleIndex;
                            row.VisibleIndex = newIndex;
                        }
                    }
                }
            }
            else if (e.Button.Tag.Equals(3))
            {
                // 切换显示隐藏
                if (gridViewColumn.GetFocusedRow() is DynamicColumnConfigurationDtoNo1 row)
                {
                    if (row.Visible)
                    {
                        row.Visible = false;
                        var rowIndex = row.VisibleIndex;
                        row.VisibleIndex = -1;
                        row.Fixed = false;
                        row.IsLeft = false;
                        foreach (var rowNo1 in ColumnBindingList)
                        {
                            if (rowNo1.VisibleIndex > rowIndex)
                            {
                                rowNo1.VisibleIndex -= 1;
                            }
                        }
                    }
                    else
                    {
                        row.Visible = true;
                        row.VisibleIndex = ColumnBindingList.Max(r => r.VisibleIndex) + 1;
                        var index = 0;
                        foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && r.IsLeft).OrderBy(r => r.VisibleIndex))
                        {
                            no1.VisibleIndex = no1.Visible ? index++ : -1;
                        }
                        foreach (var no1 in ColumnBindingList.Where(r => !r.Fixed).OrderBy(r => r.VisibleIndex))
                        {
                            no1.VisibleIndex = no1.Visible ? index++ : -1;
                        }
                        foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && !r.IsLeft).OrderBy(r => r.VisibleIndex))
                        {
                            no1.VisibleIndex = no1.Visible ? index++ : -1;
                        }
                    }
                }
            }
            else if (e.Button.Tag.Equals(4))
            {
                // 切换固定
                if (gridViewColumn.GetFocusedRow() is DynamicColumnConfigurationDtoNo1 row)
                {
                    row.Fixed = !row.Fixed;
                    var index = 0;
                    foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && r.IsLeft).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                    foreach (var no1 in ColumnBindingList.Where(r => !r.Fixed).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                    foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && !r.IsLeft).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                }
            }
            else if (e.Button.Tag.Equals(5))
            {
                // 切换左右固定
                if (gridViewColumn.GetFocusedRow() is DynamicColumnConfigurationDtoNo1 row)
                {
                    row.IsLeft = !row.IsLeft;
                    var index = 0;
                    foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && r.IsLeft).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                    foreach (var no1 in ColumnBindingList.Where(r => !r.Fixed).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                    foreach (var no1 in ColumnBindingList.Where(r => r.Fixed && !r.IsLeft).OrderBy(r => r.VisibleIndex))
                    {
                        no1.VisibleIndex = no1.Visible ? index++ : -1;
                    }
                }
            }
            ColumnBindingList.ResetBindings();
        }

        /// <summary>
        /// “保存”按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBC保存_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                DefinedCacheHelper.DefinedApiProxy.DynamicColumnAppService
                    .SaveDynamicColumnConfigurationList(ColumnBindingList.ToList()).Wait();
            });
            DialogResult = DialogResult.OK;
        }
    }
}
