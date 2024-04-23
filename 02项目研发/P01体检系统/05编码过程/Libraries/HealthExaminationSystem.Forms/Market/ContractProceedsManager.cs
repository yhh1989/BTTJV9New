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
    /// 合同回款管理
    /// </summary>
    public partial class ContractProceedsManager : UserBaseForm
    {
        /// <summary>
        /// 合同标识
        /// </summary>
        private readonly Guid _contractId;

        /// <summary>
        /// 初始化合同回款管理
        /// </summary>
        public ContractProceedsManager(Guid contractId)
        {
            InitializeComponent();
            _contractId = contractId;
        }

        /// <summary>
        /// 设置总金额
        /// </summary>
        /// <param name="amount"></param>
        public void SetTotalAmount(decimal amount)
        {
            simpleLabelItem总金额.Text = $@"总金额：{amount:c}";
        }

        /// <summary>
        /// 窗体首次显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractProceedsManager_Shown(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var data =
                    DefinedCacheHelper.DefinedApiProxy.ContractAppService.QueryContractProceeds(
                        new EntityDto<Guid>(_contractId));

                gridControl回款记录列表.DataSource = data.Result;
            });
        }

        /// <summary>
        /// 新增回款按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton新增回款_Click(object sender, EventArgs e)
        {
            using (var frm = new ContractProceedsEditor(_contractId))
            {
                frm.SaveComplete += Frm_SaveComplete;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 回款记录增加成功事件
        /// </summary>
        /// <param name="obj"></param>
        private void Frm_SaveComplete(Application.Market.Dto.ContractProceedsDto obj)
        {
            var data = gridControl回款记录列表.DataSource as List<ContractProceedsDto> ?? new List<ContractProceedsDto>();
            data.Add(obj);
            gridControl回款记录列表.DataSource = data.OrderByDescending(r => r.Date).ToList();
            gridControl回款记录列表.RefreshDataSource();
            AmountChanged?.Invoke(obj, true);
        }

        /// <summary>
        /// 回款金额改变事件
        /// </summary>
        public event Action<ContractProceedsDto, bool> AmountChanged;

        /// <summary>
        /// 删除回款按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton删除回款_Click(object sender, EventArgs e)
        {
            if (gridView回款记录列表.GetFocusedRow() is ContractProceedsDto row)
            {
                AutoLoading(() =>
                {
                    var entity =
                        DefinedCacheHelper.DefinedApiProxy.ContractAppService.DeleteContractProceeds(
                            new EntityDto<Guid>(row.Id)).Result;
                    gridView回款记录列表.DeleteRow(gridView回款记录列表.FocusedRowHandle);
                    AmountChanged?.Invoke(entity, false);
                });
            }
        }
    }
}