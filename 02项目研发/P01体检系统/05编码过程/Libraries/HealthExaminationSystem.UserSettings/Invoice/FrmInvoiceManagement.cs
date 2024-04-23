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
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Invoice
{
    public partial class FrmInvoiceManagement : UserBaseForm
    {
        InvoiceManagementAppService invocieService = new InvoiceManagementAppService();
        private List<InvoiceTypeModel> invoiceTypes;
        private List<InvoiceStateModel> invoiceStates;
        
        public FrmInvoiceManagement()
        {
            InitializeComponent();
            invoiceTypes = InvoiceTypeHelper.GetInvoiceTypeModels();
            invoiceStates = InvoiceStateHelper.GetInvoiceStateModels();
            
        }
        private Guid Id = Guid.Empty;
        private int? State = (int)InvoiceState.Enable;
        private void FrmFPHDGL_Load(object sender, EventArgs e)
        {
            Initial();
            LoadData();
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (string.IsNullOrWhiteSpace(cbo_yonghu.EditValue.ToString()))
            {
                dxErrorProvider.SetError(cbo_yonghu, string.Format(Variables.MandatoryTips, "请至少选择一名用户"));
                return;
            }
            
            var userList = cbo_yonghu.EditValue.ToString().Split(',');
            if (userList.Count() < 1)
            {
                dxErrorProvider.SetError(cbo_yonghu, string.Format(Variables.MandatoryTips, "用户"));
                return;
            }
            var startNum = Convert.ToInt64(num_ksh.EditValue);
            var endNum = Convert.ToInt64(num_jsh.EditValue);
            if (startNum >= endNum)
            {
                dxErrorProvider.SetError(num_ksh, string.Format(Variables.GreaterThanTips, "开始发票","结束发票"));
                dxErrorProvider.SetError(num_jsh, string.Format(Variables.GreaterThanTips, "开始发票", "结束发票"));
                return;
            }
            var currentNum=Convert.ToInt32(num_dqh.EditValue);
            if (currentNum < startNum)
            {
                dxErrorProvider.SetError(num_dqh, string.Format(Variables.GreaterThanTips, "当前发票", "开始发票"));
                return;
            } else if (currentNum > endNum)
            {
                dxErrorProvider.SetError(num_dqh, string.Format(Variables.GreaterThanTips, "当前发票", "结束发票"));
                return;
            }

            MReceiptManagerDto dto = new MReceiptManagerDto();
            dto.NowCard = Convert.ToInt32(num_dqh.EditValue);
            dto.Remarks = txt_beizhu.Text.Trim();
            dto.SerialNumber = Convert.ToInt32(num_xuhao.EditValue);
            dto.State = Convert.ToInt32(lue_zhuangtai.EditValue);
            dto.StratCard = Convert.ToInt32(num_ksh.EditValue);
            dto.EndCard= Convert.ToInt32(num_jsh.EditValue);
            dto.Type = (int)lue_leixing.EditValue;
            if (Id == Guid.Empty)
            {
                AddReceiptManageDto addInput = new AddReceiptManageDto();
                addInput.receiptManage = dto;
                addInput.users = new List<UserViewDto>();
                foreach (var userId in userList)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        addInput.users.Add(new UserViewDto { Id = Convert.ToInt64(userId) });
                    }
                }
                try
                {
                    var addResult = invocieService.AddReceiptManage(addInput);
                    if(addResult!=null)
                    {
                        LoadData();
                        btn_xinjian.PerformClick();
                        //XtraMessageBox.Show("保存成功！");
                        ShowMessageSucceed("保存成功！");
                    }
                }
                catch (UserFriendlyException ex)
                {
                    //ShowMessageBox(ex);
                    ShowMessageBoxError(ex.Description);
                }
            }
            else
            {
                dto.Id = Id;
                AddReceiptManageDto editInput = new AddReceiptManageDto();
                editInput.receiptManage = dto;
                editInput.users = new List<UserViewDto>();
                foreach (var userId in userList)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        editInput.users.Add(new UserViewDto { Id = Convert.ToInt64(userId) });
                    }
                }
                try
                {
                    var editResult = invocieService.EditReceiptManage(editInput);
                    if (editResult != null)
                    {
                        LoadData();
                        //XtraMessageBox.Show("保存成功！");
                        ShowMessageSucceed("保存成功！");
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.Description);
                }
            }
        }

        private void LoadData()
        {
            var data = invocieService.QueryReceiptManage(new MReceiptManagerDto {State=State });
            data = data.Where(d=>d.User!=null).ToList();
            var groups = data.GroupBy(d=>d.SerialNumber).Select(d=>d.FirstOrDefault()).ToList();
            var bindList = new List<MReceiptManagerDto>();
            foreach (var group in groups)
            {
                var users = data.Where(d=>d.SerialNumber==group.SerialNumber);
                foreach (var user in users)
                {
                    if (user.User == null)
                        continue;
                    if (user.User != group.User)
                    {
                        group.User.Name += ","+user.User.Name ;
                    }
                }
                group.User.Name = group.User.Name.Trim(',');
                bindList.Add(group);
            }
            gridControlInvoice.DataSource = bindList;
        }

        private void Initial()
        {
            var userList = invocieService.GetUser();
            cbo_yonghu.Properties.DataSource = userList;
            cbo_yonghu.SetEditValue(string.Empty);
            
            lue_leixing.Properties.DataSource = invoiceTypes;
            lue_leixing.EditValue = invoiceTypes.FirstOrDefault().Id;
            
            lue_zhuangtai.Properties.DataSource = invoiceStates;
            lue_zhuangtai.EditValue = invoiceStates.FirstOrDefault().Id;

            gridViewInvoice.Columns[Types.FieldName].DisplayFormat.FormatType= FormatType.Custom;
            gridViewInvoice.Columns[Types.FieldName].DisplayFormat.Format = new CustomFormatter(FormatInvoiceType);
            gridViewInvoice.Columns[States.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridViewInvoice.Columns[States.FieldName].DisplayFormat.Format = new CustomFormatter(FormatInvoiceState);
        }



        private string FormatInvoiceType(object arg)
        {
            try
            {
                return invoiceTypes.Find(t=>t.Id==(int)arg).Display;
            }
            catch 
            {
                return invoiceTypes.Find(t => t.Id == (int)InvoiceType.Receipt).Display;
            }
        }

        private string FormatInvoiceState(object arg)
        {
            try
            {
                return invoiceStates.Find(t => t.Id == (int)arg).Display;
            }
            catch
            {
                return invoiceStates.Find(t => t.Id == (int)InvoiceState.Enable).Display;
            }
        }


        private void btn_xinjian_Click(object sender, EventArgs e)
        {
            var allData = gridControlInvoice.GetDtoListDataSource<MReceiptManagerDto>();
            num_xuhao.Value = (decimal)allData.Max(a=>a.SerialNumber)+1;
            Id = Guid.Empty;
            num_dqh.EditValue = 0;
            txt_beizhu.Text=string.Empty;
            num_ksh.EditValue=0;
            num_jsh.EditValue=0;
            cbo_yonghu.SetEditValue(string.Empty);
            num_dqh.ReadOnly = true;
        }
        

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sfxsyty.Checked)
                State = null;
            else
                State = (int)InvoiceState.Enable;

            LoadData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridViewInvoice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var currentItem = gridControlInvoice.GetFocusedRowDto<MReceiptManagerDto>();
            if (currentItem == null)
                return;
            var groupItems = invocieService.QueryReceiptManage(currentItem);
            cbo_yonghu.SetEditValue(string.Empty);
            foreach (var item in groupItems)
            {
                var checkItem = cbo_yonghu.Properties.Items.Where(i => Convert.ToInt64(i.Value) == item.User?.Id).FirstOrDefault();
                if (checkItem != null)
                {
                    checkItem.CheckState = CheckState.Checked;
                }
            }
            num_dqh.EditValue = currentItem.NowCard;
            txt_beizhu.Text = currentItem.Remarks;
            num_xuhao.EditValue = currentItem.SerialNumber;
            num_ksh.EditValue = currentItem.StratCard;
            num_jsh.EditValue = currentItem.EndCard;
            lue_leixing.EditValue = currentItem.Type;
            lue_zhuangtai.EditValue = currentItem.State;

            Id = currentItem.Id;

            num_dqh.ReadOnly = false;
        }

        private void simpleButtonShuaxin_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void num_ksh_Leave(object sender, EventArgs e)
        {
            if (Id == Guid.Empty)
            {
                num_dqh.Value = num_ksh.Value;
            }
        }
    }
}