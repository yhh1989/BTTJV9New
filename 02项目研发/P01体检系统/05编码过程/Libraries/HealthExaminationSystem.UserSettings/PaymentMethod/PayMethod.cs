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
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.PaymentMethod
{

    public partial class PayMethod : UserBaseForm
    {
        private PaymentMethodAppService paymentMethodApPaymentService = new PaymentMethodAppService();
        private CommonAppService commonAppService;
        private Guid id = Guid.Empty;
        public PayMethod()
        {
            InitializeComponent();
            Initial();
            commonAppService = new CommonAppService();
        }
        /// <summary>
        /// 初始化控件数据
        /// </summary>
        private void Initial()
        {
            var paymentType = PaymentTypeHelper.GetPaymentTypeModels();
            foreach (var item in paymentType)
            {
                var radio = new RadioGroupItem();
                radio.Description = item.Display;
                radio.Value = item.Id;
                rdo_type.Properties.Items.Add(radio);
            }
            rdo_type.EditValue = (int)PaymentType.None;

            var chargeApply = ChargeApplyHelper.GetChargeApplyModels();
            foreach (var item in chargeApply)
            {
                var radio = new RadioGroupItem();
                radio.Description = item.Display;
                radio.Value = item.Id;
                rdo_apply.Properties.Items.Add(radio);
            }
            rdo_apply.EditValue = (int)ChargeApply.Currency;
            var IfList = IfTypeHelper.GetIfTypeModels();
            foreach (var item in IfList)
            {
                RadioGroupItem radio = new RadioGroupItem();
                radio.Description = item.Display;
                radio.Value = item.Id;
                rdo_zwssk.Properties.Items.Add(radio);
            }
            rdo_zwssk.EditValue = (int)IfType.False;
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_xinjian_Click(object sender, EventArgs e)
        {
            //如果有数据则获取已有数据编码最大的一个+1位新增数据的编码。测试提出的要求默认填充编码
            txt_bianhao.EditValue = 0;
            var rows = gc_zffs.DataSource as List<MChargeTypeDto>;
            if (rows != null)
            {
                if (rows.Count > 0)
                {
                    var currentData = rows.OrderByDescending(o => o.ChargeCode).FirstOrDefault();
                    if (currentData != null)
                    {
                        txt_bianhao.EditValue = currentData.ChargeCode + 1;
                    }
                }
            }

            id = Guid.Empty;
            //txt_bianhao.Text = string.Empty;
            txt_zffs.Text = string.Empty;
            txt_beizhu.Text = string.Empty;
            textEditZhujima.Text = string.Empty;
            rdo_type.EditValue = (int)PaymentType.None;
            rdo_apply.EditValue = (int)ChargeApply.Currency;
            rdo_zwssk.EditValue = (int)IfType.False;
            var list = gc_zffs.GetDtoListDataSource<MChargeTypeDto>();
            var maxNum = 0;
            if (list.Count > 0)
                maxNum = list.Max(l=>l.OrderNum).Value;
            num_xuhao.Value = (decimal) maxNum+ 1;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_baocun_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (string.IsNullOrWhiteSpace(txt_bianhao.Text.Trim()))
            {
                dxErrorProvider.SetError(txt_bianhao, string.Format(Variables.MandatoryTips, "编号"));
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_zffs.Text.Trim()))
            {
                dxErrorProvider.SetError(txt_zffs, string.Format(Variables.MandatoryTips, "支付方式"));
                return;
            }
            MChargeTypeDto input = new MChargeTypeDto();
            input.ChargeCode = Convert.ToInt32(txt_bianhao.Text.Trim());
            input.AccountingState = (int)rdo_zwssk.EditValue;
            input.ChargeApply = (int)rdo_apply.EditValue;
            input.ChargeName = txt_zffs.Text.Trim();
            input.PrintName = (int)rdo_type.EditValue;
            input.Remarks = txt_beizhu.Text.Trim();
            input.OrderNum = Convert.ToInt32(num_xuhao.EditValue);
            input.HelpChar = textEditZhujima.Text.Trim();

            if (id == Guid.Empty)
            {
                try
                {
                    var result = paymentMethodApPaymentService.AddMCargeType(input);
                    gc_zffs.AddDtoListItem(result);
                    id = result.Id;
                    ShowMessageSucceed("添加成功！");
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
            else
            {
                try
                {
                    input.Id = id;
                    var result = paymentMethodApPaymentService.EditMCargeType(input);
                    LoadData();
                    ShowMessageSucceed("修改成功！");
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 列表数据绑定
        /// </summary>
        private void LoadData()
        {
            var result = paymentMethodApPaymentService.GetMChargeType();
            gc_zffs.DataSource = result.OrderBy(r => r.OrderNum).ToList();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_shanchu_Click(object sender, EventArgs e)
        {
            if (id == Guid.Empty)
                return;
            DialogResult dr = XtraMessageBox.Show("是否删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
                return;
            MChargeTypeDto input = new MChargeTypeDto();
            input.Id = id;
            try
            {
                paymentMethodApPaymentService.DeleteMCargeType(input);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
            LoadData();
            btn_xinjian.PerformClick();
        }

        private void btn_shangyi_Click(object sender, EventArgs e)
        {
            var list = gc_zffs.GetDtoListDataSource<MChargeTypeDto>();
            if (list == null)
                return;
            var currentItem = gc_zffs.GetFocusedRowDto<MChargeTypeDto>();
            if (currentItem == null)
                return;
            if (currentItem == list.FirstOrDefault())
                return;
            var currentIndex = list.IndexOf(currentItem);
            var currentOrder = currentItem.OrderNum;
            var changeOrder = list[currentIndex - 1].OrderNum;
            if (currentOrder == changeOrder)
                changeOrder = changeOrder - 1;
            list[currentIndex].OrderNum = changeOrder;
            list[currentIndex - 1].OrderNum = currentOrder;
            gc_zffs.DataSource = list.OrderBy(l => l.OrderNum).ToList();
            gc_zffs.RefreshDataSource();
            gv_zffs.FocusedRowHandle = currentIndex - 1;
        }

        private void FrmZFFS_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_xiayi_Click(object sender, EventArgs e)
        {
            var list = gc_zffs.GetDtoListDataSource<MChargeTypeDto>();
            if (list == null)
                return;
            var currentItem = gc_zffs.GetFocusedRowDto<MChargeTypeDto>();
            if (currentItem == null)
                return;
            if (currentItem == list.LastOrDefault())
                return;
            var currentIndex = list.IndexOf(currentItem);
            var currentOrder = currentItem.OrderNum;
            var changeOrder = list[currentIndex + 1].OrderNum;
            if (currentOrder == changeOrder)
                changeOrder = changeOrder + 1;
            list[currentIndex].OrderNum = changeOrder;
            list[currentIndex + 1].OrderNum = currentOrder;
            gc_zffs.DataSource = list.OrderBy(l => l.OrderNum).ToList();
            gc_zffs.RefreshDataSource();
            gv_zffs.FocusedRowHandle = currentIndex + 1;
        }

        private void btn_pxbc_Click(object sender, EventArgs e)
        {
            var allList = paymentMethodApPaymentService.GetMChargeType();
            var list = gc_zffs.GetDtoListDataSource<MChargeTypeDto>();
            foreach (var item in list)
            {
                var oldData = allList.Where(a => a.Id == item.Id).FirstOrDefault();
                if (oldData.OrderNum != item.OrderNum)
                {
                    try
                    {
                        var newData = paymentMethodApPaymentService.EditMCargeType(item);
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBoxError(ex.ToString());
                    }
                }
            }
            ShowMessageSucceed("保存成功!");
        }

        private void txt_zffs_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_zffs.Text.Trim()))
            {
                textEditZhujima.Text = string.Empty;
                return;
            }
            ChineseDto input = new ChineseDto();
            input.Hans = txt_zffs.Text.Trim();
            textEditZhujima.Text = commonAppService.GetHansBrief(input).Brief;
        }

        private void gv_zffs_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dto = gc_zffs.GetFocusedRowDto<MChargeTypeDto>();
            if (dto == null)
                return;
            id = dto.Id;
            txt_bianhao.Text = dto.ChargeCode.ToString();
            txt_zffs.Text = dto.ChargeName;
            txt_beizhu.Text = dto.Remarks;
            num_xuhao.EditValue = dto.OrderNum;
            textEditZhujima.Text = dto.HelpChar;
            rdo_type.SelectedIndex = 0;
            var rdoType = rdo_type.Properties.Items.Where(r => (int)r.Value == dto.PrintName).FirstOrDefault();
            rdo_type.SelectedIndex = (int)dto.PrintName - 1;
            rdo_apply.SelectedIndex = (int)dto.ChargeApply - 1;
            rdo_zwssk.SelectedIndex = (int)dto.AccountingState - 1;
        }
    }
}