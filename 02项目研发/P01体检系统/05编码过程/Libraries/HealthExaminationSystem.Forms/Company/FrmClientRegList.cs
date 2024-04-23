using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmClientRegList : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService;
        private readonly IClientInfoesAppService _ClientInfoesAppService;
        public FrmClientRegList()
        {
            InitializeComponent();

            _clientRegAppService = new ClientRegAppService();
            _ClientInfoesAppService = new ClientInfoesAppService();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".xlsx")
                {
                    var xlsxExportOptions = new XlsxExportOptions();
                    gridViewClientRegs.ExportToXlsx(saveFileDialog.FileName, xlsxExportOptions);
                }
                else
                {
                    var xlsExportOptions = new XlsExportOptions();
                    gridViewClientRegs.ExportToXls(saveFileDialog.FileName, xlsExportOptions);
                }
                ShowMessageSucceed("保存成功！");
            }
        }

        private void FrmClientRegList_Load(object sender, EventArgs e)
        {
            //LoadData();

            //封账状态下拉框绑定
            txtFZState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(FZState));

            //锁定状态下拉框绑定
            txtSDState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(SDState));

            //散检状态下拉框绑定
            txtClientSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ClientSate));

            //已检状态下拉框绑定
            txtClientCheckSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ClientCheckSate));
            sechClientName.Properties.DataSource = _ClientInfoesAppService.Query(new ClientInfoesListInput());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit4_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "删除")
            {
                var et = new EntityDto<Guid>();
                et.Id = GetSelectGuid();
                //if (_clientRegAppService.GetClientTeam(et).Count > 0)
                //{
                //    XtraMessageBox.Show("该单位还有检查分组信息,不能删除", "提示");
                //    return;
                //}
                AutoLoading(() =>
                {
                    if (XtraMessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        deleteData();
                    else
                        return; //即取消此操作。
                });
            }
            else if (e.Button.Caption == "打印")
            {
                PrintCustomerRegLst();
            }
            else if (e.Button.Caption == "编辑")
            {
                openFormGetClick();
            }
            else if (e.Button.Caption == "查看人员信息")
            {
                // 把需要的数据标识传给窗体，剩下的工作由窗体自己完成
                // 而不是把工作交给调用者
                var frmcuslist = new FrmClientRegCustomerList();
                frmcuslist.ClientRegId = GetSelectGuid();
                frmcuslist.cteDto = _clientRegAppService.getClientRegList(new EntityDto<Guid> { Id = frmcuslist.ClientRegId });
                frmcuslist.Show();
            }
        }

        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {

            var frmEditTjlClientRegs = new FrmEditTjlClientRegs() { EditMode = (int)EditModeType.Add };
            frmEditTjlClientRegs.Show();
            LoadData();

        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
                openFormGetClick();
        }

        private void dataNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            sechClientName.EditValue = null;
            txtClientRegBM.EditValue = null;
            txtStartCheckDate.EditValue = null;
            txtEndCheckDate.EditValue = null;
            txtFZState.EditValue = null;
            txtSDState.EditValue = null;
            txtClientSate.EditValue = null;
            txtClientCheckSate.EditValue = null;
        }

        private void PrintCustomerRegLst()
        {
            var guid = GetSelectGuid();
            var entityDto = new EntityDto<Guid>();
            entityDto.Id = guid;
            var lstcustomerRegSimpleViewDtos = _clientRegAppService.QueryCustomerReg(entityDto);
            var lstyd = new List<CustomerRegSimpleViewDto>();
            var lstwd = new List<CustomerRegSimpleViewDto>();
            if (DialogResult.Cancel == XtraMessageBox.Show("总共" + lstcustomerRegSimpleViewDtos.Count + "人是否确认打印", "提示",
                    MessageBoxButtons.OKCancel))
                return;

            var sdf = new WaitDialogForm("提示", "正在打印用户信息......");
            var inmnu = 0;
            foreach (var item in lstcustomerRegSimpleViewDtos)
            {
                inmnu++;

                try
                {
                    var cus = new CusNameInput();
                    cus.Id = item.Id;

                    //打印导引单
                    sdf.SetCaption("打印导引单进度" + inmnu + "/" + lstcustomerRegSimpleViewDtos.Count);
                    //var printGuidance = new PrintGuidance();
                    //printGuidance.cusNameInput = cus;
                    //printGuidance.IsShowDialog = false;
                    //printGuidance.Print();

                    PrintGuidanceNew.Print(item.Id);
                    //PrintGuidance printGuidance = new PrintGuidance();
                    //CusNameInput cusNameInput = new CusNameInput();
                    //cusNameInput.Id = item.Id;
                    //printGuidance.cusNameInput = cusNameInput;
                    //printGuidance.IsShowDialog = false;
                    //printGuidance.Print();
                    //打印条形码
                    sdf.SetCaption("打印条形码进度" + inmnu + "/" + lstcustomerRegSimpleViewDtos.Count);
                    var frmBarPrint = new FrmBarPrint();
                    frmBarPrint.cusNameInput = cus;
                    frmBarPrint.IsPrintShowDialog = true;
                    frmBarPrint.ShowDialog();

                    //增加已打队列
                    lstyd.Add(item);
                }
                catch (Exception ex)
                {
                    item.errorinfo = ex.Message;
                    lstwd.Add(item);
                }
                finally
                {
                    sdf.Close();
                }
            }

            sdf.Close();
            XtraMessageBox.Show("总共:" + lstcustomerRegSimpleViewDtos.Count + "人; 成功打印:" + lstyd.Count
                                + "人; 未成功:" + lstwd.Count + "人", "提示");
        }

        public Guid GetSelectGuid()
        {
            var selectRow = gridViewClientRegs.GetSelectedRows()[0];
            var id = gridViewClientRegs.GetRowCellValue(selectRow, "Id").ToString();
            return Guid.Parse(id);
        }

        /// <summary>
        /// 窗体 Grid 数据加载
        /// </summary>
        private void LoadData()
        {
            dxErrorProvider.ClearErrors();
            var clientRegDto = new ClientRegSelectDto();

            var clientRegBmString = txtClientRegBM.Text.Trim();
            clientRegDto.ClientRegBM = clientRegBmString;

            var clientName = sechClientName.EditValue?.ToString();
            if (!string.IsNullOrWhiteSpace(clientName))
                clientRegDto.ClientInfo_Id = Guid.Parse(clientName);

            if (txtStartCheckDate.EditValue != null)
                clientRegDto.StartCheckDate = txtStartCheckDate.DateTime;

            if (txtEndCheckDate.EditValue != null)
                clientRegDto.EndCheckDate = txtEndCheckDate.DateTime;

            if (clientRegDto.StartCheckDate > clientRegDto.EndCheckDate)
            {
                dxErrorProvider.SetError(txtStartCheckDate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(txtEndCheckDate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }

            var fzState = txtFZState.EditValue;
            if (fzState != null)
                clientRegDto.FZState = (int)(FZState)fzState;

            var sdState = txtSDState.EditValue;
            if (sdState != null)
                clientRegDto.SDState = (int)(SDState)sdState;

            var clientSate = txtClientSate.EditValue;
            if (clientSate != null)
                clientRegDto.ClientSate = (int)(ClientSate)clientSate;

            var clientCheckSate = txtClientCheckSate.EditValue;
            if (clientCheckSate != null)
                clientRegDto.ClientCheckSate = (int)(ClientCheckSate)clientCheckSate;

            gridControl.DataSource = null;

            AutoLoading(() =>
            {
                var output = _clientRegAppService.PageFulls(new PageInputDto<ClientRegSelectDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = clientRegDto
                });
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridControl.DataSource = output.Result;
            });
        }

        /// <summary>
        /// 打开编辑页面
        /// </summary>
        private void openFormGetClick()
        {
            var id = gridViewClientRegs.GetRowCellValue(this.gridViewClientRegs.FocusedRowHandle, "Id").ToString();
            var clientid = gridViewClientRegs.GetRowCellValue(this.gridViewClientRegs.FocusedRowHandle, "ClientInfo.Id").ToString(); 
            //var clientRegDto = new ClientRegSelectDto();
            //clientRegDto.Id = Guid.Parse(id);
            //var list = _clientRegAppService.getClientRegList(clientRegDto);
            var frmEditTjlClientRegs = new FrmEditTjlClientRegs() { clireg_Id = new Guid(id), EditMode = (int)EditModeType.Edit, Cli_infoId= new Guid(clientid)  };
            frmEditTjlClientRegs.Show(this);

            // 添加 Loading 窗体会一直保留到窗体关闭。请使用 FrmEditTjlClientRegs 的 Loading 窗体
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        private void deleteData()
        {
            //待完善
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }

            splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
            try
            {
                var selectRow = gridViewClientRegs.GetSelectedRows()[0];
                var id = gridViewClientRegs.GetRowCellValue(selectRow, "Id").ToString();
                var clientRegDto = new CreateClientRegDto();
                clientRegDto.Id = Guid.Parse(id);
                _clientRegAppService.deleteClientReag(clientRegDto);
            }
            finally
            {
                if (closeWait)
                {
                    FrmClientRegList_Load(this, null);
                    splashScreenManager.CloseWaitForm();
                }
            }
        }
        /// <summary>
        /// 分账状态（清空）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFZState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtFZState.EditValue = null;
        }

        /// <summary>
        /// 锁定状态（清空）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSDState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtSDState.EditValue = null;
        }

        /// <summary>
        /// 散检状态（清空）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClientSate_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtClientSate.EditValue = null;
        }

        /// <summary>
        /// 已检状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClientCheckSate_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                txtClientCheckSate.EditValue = null;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }
    }
}