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
    /// 合同回款记录编辑
    /// </summary>
    public partial class ContractProceedsEditor : UserBaseForm
    {
        /// <summary>
        /// 合同标识
        /// </summary>
        private readonly Guid _contractId;

        /// <summary>
        /// 初始化合同回款记录编辑
        /// </summary>
        public ContractProceedsEditor(Guid contractId)
        {
            InitializeComponent();
            _contractId = contractId;
        }

        /// <summary>
        /// 窗体首次加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractProceedsEditor_Load(object sender, EventArgs e)
        {
            dateEdit日期.DateTime = DateTime.Now;
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton保存_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (spinEdit金额.Value <= 0)
            {
                dxErrorProvider.SetError(spinEdit金额, "请输入正确的金额");
            }

            if (dxErrorProvider.HasErrors)
            {
                return;
            }
            AutoLoading(() =>
            {
                var input = new ContractProceedsDto
                {
                    Amount = spinEdit金额.Value,
                    ContractId = _contractId,
                    Date = dateEdit日期.EditValue == null ? DateTime.Now : dateEdit日期.DateTime
                };

                var output = DefinedCacheHelper.DefinedApiProxy.ContractAppService.InsertContractProceeds(input);
                SaveComplete?.Invoke(output.Result);
                DialogResult = DialogResult.OK;
            });
        }

        /// <summary>
        /// 保存成功事件
        /// </summary>
        public event Action<ContractProceedsDto> SaveComplete;
    }
}