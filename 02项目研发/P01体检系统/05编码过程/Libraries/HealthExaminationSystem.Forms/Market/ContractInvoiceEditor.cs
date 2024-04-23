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

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 合同开票记录编辑
    /// </summary>
    public partial class ContractInvoiceEditor : UserBaseForm
    {
        /// <summary>
        /// 合同标识
        /// </summary>
        private readonly Guid _contractId;

        /// <summary>
        /// 初始化合同开票记录编辑
        /// </summary>
        public ContractInvoiceEditor(Guid contractId)
        {
            _contractId = contractId;
            InitializeComponent();
        }

        /// <summary>
        /// 保存开票记录数据成功事件
        /// </summary>
        public event Action<ContractInvoiceDto> SaveComplete;

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton保存_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (string.IsNullOrWhiteSpace(textEdit发票号.Text))
            {
                dxErrorProvider.SetError(textEdit发票号, "请填写发票号");
            }

            if (spinEdit金额.Value <= 0)
            {
                dxErrorProvider.SetError(spinEdit金额, "请正确填写金额");
            }

            if (dxErrorProvider.HasErrors)
            {
                return;
            }
            AutoLoading(() =>
            {
                var input = new ContractInvoiceDto
                {
                    Amount = spinEdit金额.Value,
                    ContractId = _contractId,
                    Date = dateEdit日期.EditValue == null ? DateTime.Now : dateEdit日期.DateTime,
                    InvoiceNumber = textEdit发票号.Text
                };

                var data = DefinedCacheHelper.DefinedApiProxy.ContractAppService.InsertContractInvoice(input).Result;
                SaveComplete?.Invoke(data);
                DialogResult = DialogResult.OK;
            });
        }

        /// <summary>
        /// 窗体首次加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractInvoiceEditor_Load(object sender, EventArgs e)
        {
            dateEdit日期.DateTime = DateTime.Now;
        }
    }
}