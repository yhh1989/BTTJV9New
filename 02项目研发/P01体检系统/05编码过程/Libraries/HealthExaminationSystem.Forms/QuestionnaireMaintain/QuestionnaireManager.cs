using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.QuestionnaireMaintain
{
    /// <summary>
    /// 问卷管理
    /// </summary>
    public partial class QuestionnaireManager : UserBaseForm
    {
        /// <summary>
        /// 问卷应用服务
        /// </summary>
        private readonly IQuestionnaireAppService _questionnaireAppService;

        /// <summary>
        /// 初始化 问卷管理
        /// </summary>
        public QuestionnaireManager()
        {
            InitializeComponent();

            _questionnaireAppService = new QuestionnaireAppService();
        }

        /// <summary>
        /// 初始化 问卷管理
        /// </summary>
        /// <param name="checkNo">体检号</param>
        /// <param name="tempPersonCheckOrderno">线上预约体检订单号</param>
        public QuestionnaireManager(string checkNo, string tempPersonCheckOrderno) : this()
        {
            if (!string.IsNullOrWhiteSpace(checkNo))
            {
                textEdit1.Text = checkNo;
            }
            if (!string.IsNullOrWhiteSpace(tempPersonCheckOrderno))
            {
                textEdit2.Text = checkNo;
            }

            textEdit1.ReadOnly = true;
            textEdit2.ReadOnly = true;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionnaireManager_Load(object sender, EventArgs e)
        {
            // 已生成评估报告列格式化
            repositoryItemLookUpEdit1.DataSource = new[]
            {
                new { Id = 0, Name = "否" },
                new { Id = 1, Name = "是" }
            };

            // 问卷类型列格式化
            repositoryItemLookUpEdit2.DataSource = new[]
            {
                new { Id = 1, Name = "自定义问卷" },
                new { Id = 2, Name = "评估问卷" }
            };

            // 最后更新时间条件初始化
            var date = DateTime.Now;
            dateEdit1.DateTime = date.AddYears(-1);
            dateEdit2.DateTime = date;
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            try
            {
                var input = new QuestionnaireSearchInput();
                if (!string.IsNullOrWhiteSpace(textEdit1.Text))
                {
                    input.checkNo = textEdit1.Text;
                }

                if (!string.IsNullOrWhiteSpace(textEdit2.Text))
                {
                    input.tempPersonCheckOrderno = textEdit2.Text;
                }

                if (dateEdit1.EditValue != null)
                {
                    input.lastTimeStart = dateEdit1.DateTime;
                }

                if (dateEdit2.EditValue != null)
                {
                    input.lastTimeEnd = dateEdit2.DateTime;
                }

                AutoLoading(() =>
                {
                    var result = _questionnaireAppService.QueryQuestionnaireRecord(input);
                    gridControl1.DataSource = result;
                });
            }
            finally
            {
                simpleButton1.Enabled = true;
            }
        }

        /// <summary>
        /// 问卷列表数据源改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            if (gridControl1.DataSource != null)
            {
                gridView1.BestFitColumns();
            }
        }

        /// <summary>
        /// 问卷列表行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridView1.IsDataRow(e.RowHandle))
                {
                    popupMenu1.ShowPopup(MousePosition);
                }
            }
        }

        /// <summary>
        /// 查看/编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = false;
            try
            {
                if (gridView1.GetFocusedRow() is QuestionnaireDto row)
                {
                    using (var frm = new QuestionnaireEditor(row.Id))
                    {
                        frm.ShowDialog(this);
                    }
                }
            }
            finally
            {
                simpleButton2.Enabled = true;
            }
        }

        /// <summary>
        /// 复制体检号右键菜单（问卷列表）点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRow() is QuestionnaireDto row)
            {
                try
                {
                    Clipboard.SetText(row.checkNo);
                }
                catch (ExternalException)
                {
                    // ignored
                }
                alertControl1.Show(this, "提示", $"体检号：{row.checkNo} 复制成功！");
            }
        }

        /// <summary>
        /// 查看/编辑右键菜单（问卷列表）点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            simpleButton2.PerformClick();
        }

        /// <summary>
        /// 窗体第一次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionnaireManager_Shown(object sender, EventArgs e)
        {
            simpleButton1.PerformClick();
        }
    }
}