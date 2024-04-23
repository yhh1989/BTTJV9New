using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public partial class ContractManager : UserBaseForm
    {
        /// <summary>
        /// 初始化 合同管理
        /// </summary>
        public ContractManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化 合同管理
        /// </summary>
        /// <param name="admin">管理员</param>
        public ContractManager(bool admin) : this()
        {
            Admin = admin;
        }

        /// <summary>
        /// 管理员
        /// </summary>
        public bool Admin { get; }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton添加_Click(object sender, EventArgs e)
        {
            using (var frm = new ContractEditor())
            {
                frm.RepositoryItemLookUpEdit合同类别 = repositoryItemLookUpEdit合同类别;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    simpleButton查询.PerformClick();
                }
            }
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
                var input = new QueryContractConditionInput();
                if (!string.IsNullOrWhiteSpace(textEdit编号检索.Text))
                {
                    input.Number = textEdit编号检索.Text;
                }

                if (searchLookUpEdit单位检索.EditValue is Guid companyId)
                {
                    input.CompanyId = companyId;
                }

                if (!string.IsNullOrWhiteSpace(textEdit签字人检索.Text))
                {
                    input.Signatory = textEdit签字人检索.Text;
                }

                if (dateEdit签约时间起.EditValue != null)
                {
                    input.SubmitTimeStart = dateEdit签约时间起.DateTime;
                }

                if (dateEdit签约时间止.EditValue != null)
                {
                    input.SubmitTimeEnd = dateEdit签约时间止.DateTime;
                }

                if (dateEdit有效期起.EditValue != null)
                {
                    input.ValidTimeStart = dateEdit有效期起.DateTime;
                }

                if (dateEdit有效期止.EditValue != null)
                {
                    input.ValidTimeEnd = dateEdit有效期止.DateTime;
                }

                if (!Admin)
                {
                    input.CreatorUserId = CurrentUser.Id;
                }

                var resultList = DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractList(input);
                gridControl合同列表.DataSource = resultList.Result;
            });
        }

        /// <summary>
        /// 合同列表数据源改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl合同列表_DataSourceChanged(object sender, EventArgs e)
        {
            if (gridControl合同列表.DataSource != null)
            {
                gridView合同列表视图.BestFitColumns();
            }
        }

        /// <summary>
        /// 窗体第一次显示时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractManager_Shown(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                repositoryItemLookUpEdit合同类别.DataSource = DefinedCacheHelper.DefinedApiProxy.ContractAppService
                    .QueryContractCategoryList(new QueryContractCategoryConditionInput()).Result;

                repositoryItemLookUpEdit公司标识.DataSource =
                searchLookUpEdit单位检索.Properties.DataSource =
                    DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryCompanyComboBoxList().Result;

                simpleButton查询.PerformClick();
            });
        }

        /// <summary>
        /// 单位检索下拉检索显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEdit单位检索_Popup(object sender, EventArgs e)
        {
            searchLookUpEdit单位检索.Properties.View.BestFitColumns();
        }

        /// <summary>
        /// 窗体第一次加载时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractManager_Load(object sender, EventArgs e)
        {
            var start = DateTime.Now;
            var end = start.AddMonths(1);
            dateEdit有效期起.DateTime = start;
            dateEdit有效期止.DateTime = end;
        }

        /// <summary>
        /// 编辑按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton编辑_Click(object sender, EventArgs e)
        {
            if (gridView合同列表视图.GetFocusedRow() is ContractInformationDto row)
            {
                using (var frm = new ContractEditor(row.Id))
                {
                    frm.RepositoryItemLookUpEdit合同类别 = repositoryItemLookUpEdit合同类别;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        simpleButton查询.PerformClick();
                    }
                }
            }
        }

        /// <summary>
        /// 操作按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit操作_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "附件")
            {
                var id = gridView合同列表视图.GetFocusedRowCellValue(gridColumn1);
                var name = gridView合同列表视图.GetFocusedRowCellValue(gridColumn6);
                using (var frm = new ContractAdjunctManager((Guid)id))
                {
                    frm.Text = string.Format(frm.Text, name);
                    frm.ShowDialog(this);
                }
            }
            else if (e.Button.Caption == "重要事项")
            {
                var content = gridView合同列表视图.GetFocusedRowCellValue(gridColumn5) as string;
                using (var frm = new ImportantMatterViewer(content))
                {
                    frm.ShowDialog(this);
                }
            }
            else if (e.Button.Caption == "回款记录")
            {
                var id = gridView合同列表视图.GetFocusedRowCellValue(gridColumn1);
                var name = gridView合同列表视图.GetFocusedRowCellValue(gridColumn6);
                var amount = gridView合同列表视图.GetFocusedRowCellValue(gridColumn15);
                using (var frm = new ContractProceedsManager((Guid)id))
                {
                    frm.Text = string.Format(frm.Text, name);
                    frm.SetTotalAmount((decimal)amount);
                    frm.AmountChanged += Frm_AmountChanged;
                    frm.ShowDialog(this);
                }
            }
            else if (e.Button.Caption == "开票记录")
            {
                var id = gridView合同列表视图.GetFocusedRowCellValue(gridColumn1);
                var name = gridView合同列表视图.GetFocusedRowCellValue(gridColumn6);
                var amount = gridView合同列表视图.GetFocusedRowCellValue(gridColumn16);
                using (var frm = new ContractInvoiceManager((Guid)id))
                {
                    frm.Text = string.Format(frm.Text, name);
                    frm.SetProceedsAmount((decimal)amount);
                    frm.AmountChanged += Frm_AmountChanged1;
                    frm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 开票金额改变事件
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void Frm_AmountChanged1(ContractInvoiceDto arg1, bool arg2)
        {
            if (gridView合同列表视图.GetFocusedRow() is ContractInformationFullDto row)
            {
                if (arg2)
                {
                    row.InvoiceCollection.Add(arg1);
                }
                else
                {
                    var dto = row.InvoiceCollection.Find(r => r.Id == arg1.Id);
                    row.InvoiceCollection.Remove(dto);
                }
                gridView合同列表视图.RefreshRow(gridView合同列表视图.FocusedRowHandle);
                gridView合同列表视图.BestFitColumns();
            }
        }

        /// <summary>
        /// 回款管理金额改变事件
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void Frm_AmountChanged(ContractProceedsDto arg1, bool arg2)
        {
            if (gridView合同列表视图.GetFocusedRow() is ContractInformationFullDto row)
            {
                if (arg2)
                {
                    row.ProceedsCollection.Add(arg1);
                }
                else
                {
                    var dto = row.ProceedsCollection.Find(r => r.Id == arg1.Id);
                    row.ProceedsCollection.Remove(dto);
                }
                gridView合同列表视图.RefreshRow(gridView合同列表视图.FocusedRowHandle);
                gridView合同列表视图.BestFitColumns();
            }
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton删除_Click(object sender, EventArgs e)
        {
            if (gridView合同列表视图.GetFocusedRow() is ContractInformationDto row)
            {
                if (XtraMessageBox.Show(this, $"确定要删除合同“{row.Name}”吗？", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    AutoLoading(() =>
                    {
                        var task = DefinedCacheHelper.DefinedApiProxy.ContractAppService.DeleteContract(
                            new EntityDto<Guid>(row.Id));
                        task.Wait();
                        gridView合同列表视图.DeleteRow(gridView合同列表视图.FocusedRowHandle);
                    });
                }
            }
        }
    }
}