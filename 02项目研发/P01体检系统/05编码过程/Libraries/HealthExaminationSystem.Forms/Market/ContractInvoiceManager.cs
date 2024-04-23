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
    /// 合同开票记录
    /// </summary>
    public partial class ContractInvoiceManager : UserBaseForm
    {
        /// <summary>
        /// 合同标识
        /// </summary>
        private readonly Guid _contractId;

        /// <summary>
        /// 初始化合同开票记录
        /// </summary>
        public ContractInvoiceManager(Guid contractId)
        {
            InitializeComponent();
            _contractId = contractId;
        }

        /// <summary>
        /// 设置回款金额
        /// </summary>
        /// <param name="amount"></param>
        public void SetProceedsAmount(decimal amount)
        {
            simpleLabelItem回款金额.Text = $@"回款金额：{amount:c}";
        }

        /// <summary>
        /// 窗体首次显示完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractInvoiceManager_Shown(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var data =
                    DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractInvoiceList(
                        new EntityDto<Guid>(_contractId));

                gridControl合同开票记录.DataSource = data.Result;
            });
        }

        /// <summary>
        /// 开票金额改变事件
        /// </summary>
        public event Action<ContractInvoiceDto, bool> AmountChanged;

        /// <summary>
        /// 新增发票按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton新增发票_Click(object sender, EventArgs e)
        {
            using (var frm = new ContractInvoiceEditor(_contractId))
            {
                frm.SaveComplete += Frm_SaveComplete;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 合同发票记录保存成功事件
        /// </summary>
        /// <param name="obj"></param>
        private void Frm_SaveComplete(ContractInvoiceDto obj)
        {
            var data = gridControl合同开票记录.DataSource as List<ContractInvoiceDto> ?? new List<ContractInvoiceDto>();
            data.Add(obj);
            gridControl合同开票记录.DataSource = data.OrderByDescending(r => r.Date).ToList();
            gridControl合同开票记录.RefreshDataSource();
            AmountChanged?.Invoke(obj, true);
        }

        /// <summary>
        /// 删除发票按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton删除发票_Click(object sender, EventArgs e)
        {
            if (gridView合同开票记录.GetFocusedRow() is ContractInvoiceDto row)
            {
                AutoLoading(() =>
                {
                    var task =
                        DefinedCacheHelper.DefinedApiProxy.ContractAppService.DeleteContractInvoice(
                            new EntityDto<Guid>(row.Id));
                    task.Wait();
                    gridView合同开票记录.DeleteRow(gridView合同开票记录.FocusedRowHandle);
                    AmountChanged?.Invoke(task.Result, false);
                });
            }
        }
    }
}