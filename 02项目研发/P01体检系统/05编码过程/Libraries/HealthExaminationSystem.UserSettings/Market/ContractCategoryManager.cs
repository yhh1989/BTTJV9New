using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Market
{
    /// <summary>
    /// 合同类别管理
    /// </summary>
    public partial class ContractCategoryManager : UserBaseForm
    {
        /// <summary>
        /// 初始化合同类别管理
        /// </summary>
        public ContractCategoryManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton查询_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var input = new QueryContractCategoryConditionInput();
                if (!string.IsNullOrWhiteSpace(textEdit名称检索.Text))
                {
                    input.Name = textEdit名称检索.Text;
                }

                var resultTask = DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractCategoryList(input: input);

                gridControl合同类别列表.DataSource = resultTask.Result;
            });
        }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton添加_Click(object sender, EventArgs e)
        {
            using (var frm = new ContractCategoryEditor())
            {
                var dialogResult = frm.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    simpleButton查询.PerformClick();
                }
            }
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton编辑_Click(object sender, EventArgs e)
        {
            if (gridView合同类别列表.GetFocusedRow() is ContractCategoryDto row)
            {
                using (var frm = new ContractCategoryEditor(row.Id))
                {
                    var dialogResult = frm.ShowDialog(this);
                    if (dialogResult == DialogResult.OK)
                    {
                        simpleButton查询.PerformClick();
                    }
                }
            }
        }

        /// <summary>
        /// 启用或禁用按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton启用或禁用_Click(object sender, EventArgs e)
        {
            if (gridView合同类别列表.GetFocusedRow() is ContractCategoryDto row)
            {
                row.IsActive = !row.IsActive;
                AutoLoading(() =>
                {
                    var task = DefinedCacheHelper.DefinedApiProxy.ContractAppService.UpdateOrInsertContractCategory(row);
                    task.Wait();
                    simpleButton查询.PerformClick();
                });
            }
        }
    }
}