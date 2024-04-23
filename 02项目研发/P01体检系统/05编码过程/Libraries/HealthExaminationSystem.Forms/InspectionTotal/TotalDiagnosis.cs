using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class TotalDiagnosis : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        //保存匹配到的科室建议
        public List<SummarizeAdviceDto> SummarizeList = new List<SummarizeAdviceDto>();

        public TotalDiagnosis(List<SummarizeAdviceDto> _SummarizeList)
        {
            SummarizeList = _SummarizeList;
            InitializeComponent();
            InitForm();
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
        }

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void InitForm()
        {
            gridControl1.DataSource = SummarizeList.OrderBy(l => l.OrderNum).ToList();
        }

        /// <summary>
        /// 上移下移事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                //上
                //MessageBox.Show("上");
                var list = gridControl1.GetDtoListDataSource<SummarizeAdviceDto>();
                if (list == null)
                    return;
                var currentItem = gridControl1.GetFocusedRowDto<SummarizeAdviceDto>();
                if (currentItem == null)
                    return;
                if (currentItem == list.FirstOrDefault())
                    return;
                var currentOrder = currentItem.OrderNum;
                var currentIndex = list.IndexOf(currentItem);
                list[currentIndex].OrderNum = list[currentIndex - 1].OrderNum;
                list[currentIndex - 1].OrderNum = currentOrder;
                gridControl1.DataSource = list.OrderBy(l => l.OrderNum).ToList();
                gridControl1.RefreshDataSource();
                gridView1.FocusedRowHandle = currentIndex - 1;
            }
            else if (e.Button.Index == 1)
            {
                //下
                //MessageBox.Show("下");
                var list = gridControl1.GetDtoListDataSource<SummarizeAdviceDto>();
                if (list == null)
                    return;
                var currentItem = gridControl1.GetFocusedRowDto<SummarizeAdviceDto>();
                if (currentItem == null)
                    return;
                if (currentItem == list.LastOrDefault())
                    return;
                var currentOrder = currentItem.OrderNum;
                var currentIndex = list.IndexOf(currentItem);
                list[currentIndex].OrderNum = list[currentIndex + 1].OrderNum;
                list[currentIndex + 1].OrderNum = currentOrder;
                gridControl1.DataSource = list.OrderBy(l => l.OrderNum).ToList();
                gridControl1.RefreshDataSource();
                gridView1.FocusedRowHandle = currentIndex + 1;
            }
            else if (e.Button.Index == 2)
            {
                //删除
                //MessageBox.Show("删除");
                var currentItem = gridControl1.GetFocusedRowDto<SummarizeAdviceDto>();
                if (currentItem == null)
                    return;
                SummarizeList.Remove(currentItem);
                gridControl1.DataSource = SummarizeList.OrderBy(l => l.OrderNum).ToList();
                gridControl1.RefreshDataSource();
            }
        }

        public event EventHandler SimpleButtonZhenduanClick;

        /// <summary>
        /// 点击生成将事件抛出父窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonZhenduan_Click(object sender, EventArgs e)
        {
            SimpleButtonZhenduanClick?.Invoke(sender, e);
        }

        private void textEditName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)//如果输入的是回车键  
            {
                SearchSummarize();
            }
        }

        /// <summary>
        /// 检索建议
        /// </summary>
        private void SearchSummarize()
        {
            List<SummarizeAdviceDto> _summarizeAdviceFull = _summarizeAdviceAppService.GetSummAll(new InputSearchSumm() { Name = textEditName.Text.Trim() });
            gridControl2.DataSource = _summarizeAdviceFull.OrderBy(l => l.AdviceName).ToList();
        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var currentItem = gridControl2.GetFocusedRowDto<SummarizeAdviceDto>();
                    foreach (var item in SummarizeList)
                    {
                        if (item.Id == currentItem.Id)
                        {
                            MessageBox.Show("已存在项目，不可重复选择。");
                            return;
                        }
                    }

                    SummarizeList.Add(currentItem);
                    this.gridControl1.DataSource = SummarizeList;
                    this.gridControl1.RefreshDataSource();
                    var list = gridControl2.GetDtoListDataSource<SummarizeAdviceDto>();
                    this.gridView2.FocusedRowHandle = list.IndexOf(currentItem);
                }
            }
        }
    }
}
