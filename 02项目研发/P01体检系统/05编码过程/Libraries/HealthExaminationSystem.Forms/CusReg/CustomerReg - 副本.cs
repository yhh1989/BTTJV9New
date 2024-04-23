using Abp.Application.Services.Dto;
using Btpd.BLL;
using Btpd.Model;
using DevExpress.Data;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using HealthExamination.HardwareDrivers;
using HealthExamination.HardwareDrivers.Models.IdCardReader;
using NeusoftInterface;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Charge;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.QuestionnaireMaintain;
using Sw.Hospital.HealthExaminationSystem.SoftFace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using WindowsFormsApp1;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CustomerReg : UserBaseForm
    {
        private List<SexModel> sexList;//性别字典
        private List<MarrySateModel> marrySateList;//婚育字典
        private List<BreedStateModel> breedStateList;//孕育字典
        private List<EnumModel> reviewStates;//复查状态字典
        private List<ClientRegDto> clientRegs;//单位及分组字典
        private ICustomerAppService customerSvr;//体检预约
        private QueryCustomerRegDto curCustomRegInfo;//当前客户预约信息
        private IItemSuitAppService itemSuitAppSvr;//套餐
        private IIDNumberAppService iIDNumberAppService;
        private bool isChangeItemGroup;//是否修改分组;
        private BasicDictionaryDto shenfenzhengData;//身份证类型的字典数据
        private PictureController _pictureController;
        private IIdCardReaderHardwareDriver driver;
        private ICardReadDeiver cardriver;
        private bool IsReging;//是否正在登记
        private IPersonnelCategoryAppService _personnelCategoryAppService;
        private ItemGroupAppService _ItemGroupAppService;
        private CameraHelper cameraHelper;
        private string customerIMG;
        private readonly IClientRegAppService _clientReg = new ClientRegAppService();
        private IChargeAppService _chargeAppService { get; set; }
        private readonly ICommonAppService _commonAppService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;
        private string customerBM = "";
        public CustomerReg()
        {
            _commonAppService = new CommonAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            InitializeComponent();
            driver = DriverFactory.GetDriver<IIdCardReaderHardwareDriver>();
            _chargeAppService = new ChargeAppService();
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                if (HISName == "北仑")
                {
                    cardriver = DriverFactory.GetDriver<ICardReadDeiver>();

                }
            }

        }

        public CustomerReg(string _customerBM) : this()
        {
            customerBM = _customerBM;
        }
        private void CustomerReg_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            _pictureController = new PictureController();
            _ItemGroupAppService = new ItemGroupAppService();
            customerSvr = new CustomerAppService();
            itemSuitAppSvr = new ItemSuitAppService();
            iIDNumberAppService = new IDNumberAppService();
            _personnelCategoryAppService = new PersonnelCategoryAppService();
            shenfenzhengData = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "身份证"); //Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType).FirstOrDefault(o => o.Text == "身份证");
            gridView1.Columns[conCollectionState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[conCollectionState.FieldName].DisplayFormat.Format = new CustomFormatter(CollectionStateHelper.PayerCollectionStateFormatter);

            repositoryItemLookUpEdit2.DataSource = IsZYBStateHelper.GetZYBStateModels();
            try
            {
                clientRegs = customerSvr.QuereyClientRegInfos(new FullClientRegDto() { FZState = (int)FZState.not });//加载单位分组数据
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }

            QueryRegNumbers();
            curCustomRegInfo = new QueryCustomerRegDto() { CustomerItemGroup = new List<TjlCustomerItemGroupDto>(), Customer = new QueryCustomerDto() };
            LoadControlBindData();
            SetBtnEnabled();
            if (!string.IsNullOrEmpty(customerBM))
            {
                var data = QueryCustomerRegData(new SearchCustomerDto {
                    CustomerBM = customerBM
                });
                LoadCustomerData(data);
            }
        }
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名</param>
        private void KillProcess(string processName)
        {
            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName == processName)
                {
                    item.Kill();
                }
            }
        }
        #region 事件
        #region 打印导引单
        //private void butprintFroms_Click(object sender, EventArgs e)
        //{
        //    //CusNameInput cus = new CusNameInput();
        //    //cus.Id = curCustomRegInfo.Id;
        //    //PrintGuidance printGuidance = new PrintGuidance();
        //    //printGuidance.cusNameInput = cus;
        //    //printGuidance.Print();
        //    PrintGuidanceNew.Print(curCustomRegInfo.Id);
        //    txtCustoemrCode.Focus();
        //    txtCustoemrCode.SelectAll();
        //}
        //打印条形码
        private void butprintBars_Click(object sender, EventArgs e)
        {
            FrmBarPrint frmBarPrint = new FrmBarPrint();
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
            if (HISjk!=null && HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                if (HISName!=null && HISName == "江苏鑫亿四院")
                {
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id= curCustomRegInfo.Id;
                    var slo=   customerSvr.getupdate(chargeBM);
                    if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                    {
                        MessageBox.Show("检验申请单还未申请成功，请稍后再打印，如果还未成功请从新登记！");
                        return;
                    }
                }
            }
                     
            CusNameInput cus = new CusNameInput();
            cus.Id = curCustomRegInfo.Id;
            frmBarPrint.cusNameInput = cus;
            frmBarPrint.IsPrintShowDialog = false;
            frmBarPrint.ShowDialog();
            txtCustoemrCode.Focus();
            txtCustoemrCode.SelectAll();
        }
        //打印条形码/导引单
        private void butPintFaB_Click(object sender, EventArgs e)
        {

            ThreadStart startJC = new ThreadStart(PrintGuidance);
            Thread threadJC = new Thread(startJC);
            threadJC.Start();
            threadJC.IsBackground = true;
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                if (HISName == "江苏鑫亿四院")
                {
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id = curCustomRegInfo.Id;
                    var slo = customerSvr.getupdate(chargeBM);
                    if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                    {
                        MessageBox.Show("检验申请单还未申请成功，请稍后再打印条码，如果还未成功请从新登记！");
                        return;
                    }
                }
            }
            ThreadStart startJC1 = new ThreadStart(printbar);
            Thread threadJC1 = new Thread(startJC1);
            threadJC1.Start();
            threadJC1.IsBackground = true;




        }
        /// <summary>
        /// 打印条码
        /// </summary>
        private void printbar()
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    FrmBarPrint frmBarPrint = new FrmBarPrint();
                    List<CusNameInput> cuslist = new List<CusNameInput>();
                    CusNameInput cus = new CusNameInput();
                    cus.Id = curCustomRegInfo.Id;
                    cus.Theme = "1";
                    cus.CusRegBM = curCustomRegInfo.CustomerBM;
                    cuslist.Add(cus);
                    frmBarPrint.BarPrintAll(cuslist);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            }));
        }
        /// <summary>
        /// 打印导引单
        /// </summary>
        private void PrintGuidance()
        {
            this.Invoke(new Action(() =>
            {
                dropDownButtonPrintGuidance.PerformClick();
            }));
        }
        #endregion
        /// <summary>
        /// 体检号回车事件
        /// </summary>
        private void txtCustoemrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtCustoemrCode.Text))
            {
                var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });
                LoadCustomerData(data);
            }
        }
        /// <summary>
        /// 身份证号回车事件
        /// </summary>
        private void txtIDCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtIDCardType.Text.Contains("身份证"))
            {
                if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtIDCardNo.Text))
                {
                    var data = QueryCusDataIdNum(new SearchCustomerDto { IDCardNo = txtIDCardNo.EditValue.ToString() });


                    if (data == null)
                    {
                        return;
                    }
                    if (data != null && data.CustomerBM != null && data.CustomerBM != "")
                    {
                        LoadCustomerData(data);
                    }
                    else
                    {
                        // alertInfo.Show(this, "提示!", "没有该体检人预约信息！");
                        bool isold = false;
                        if (data.Customer != null && (!string.IsNullOrEmpty(data.Customer.Mobile) || data.Customer.MarriageStatus.HasValue))
                        { isold = true; }
                        addCusByIdCard(txtIDCardNo.EditValue.ToString(), isold);

                    }

                }
            }
        }

        /// <summary>
        /// 姓名回车事件
        /// </summary>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtName.Text))
            {
                //var data = QueryCustomerRegData(new SearchCustomerDto { Name = txtName.EditValue.ToString(), NotCheckState = (int)PhysicalEState.Complete });
                var data = QueryCustomerRegData(new SearchCustomerDto { Name = txtName.EditValue.ToString() });
                if (data != null)
                {
                    LoadCustomerData(data);
                }
                else
                {

                    txtName.EditValue.ToString();
                }

            }
        }
        /// <summary>
        /// 值改变事件 单位改变分组改变
        /// </summary>
        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {

            //var control = sender as SearchLookUpEdit;
            if (txtClientRegID.EditValue == null)
            {
                txtTeamID.Properties.DataSource = null;
                if (curCustomRegInfo != null)
                {
                    if (curCustomRegInfo.SendToConfirm != (int)SendToConfirm.Yes)
                    {
                        cuslookItemSuit.Enabled = true;
                        //butAddSuit.Enabled = true;
                    }
                }
            }
            else
            {
                try
                {
                    //显示单位预约信息
                    var clietreg = txtClientRegID.GetSelectedDataRow() as ClientRegDto;
                    labelClientInfo.Text = clietreg.RegInfo;
                    var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(txtClientRegID.EditValue?.ToString()) });
                    if (!string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()) && gridLookUpSex.EditValue?.ToString() != ((int)Sex.GenderNotSpecified).ToString())
                    {
                        list = list.Where(o => o.Sex == Convert.ToInt32(gridLookUpSex.EditValue) || o.Sex == (int)Sex.GenderNotSpecified)?.ToList();
                    }
                    //if (gridLookUpMarriageStatus.EditValue != null)
                    //{
                    //    list = list.Where(o => o.MaritalStatus == Convert.ToInt32(gridLookUpMarriageStatus) || o.MaritalStatus == (int)MarrySate.Unstated)?.ToList();
                    //}
                    //if (gridLookUpConceive.EditValue != null)
                    //{
                    //    list = list.Where(o => o.ConceiveStatus == Convert.ToInt32(gridLookUpConceive.EditValue) || !o.ConceiveStatus.HasValue || o.ConceiveStatus == null)?.ToList();
                    //}

                    txtTeamID.EditValue = null;
                    txtTeamID.Properties.DataSource = list;
                    cuslookItemSuit.Enabled = false;
                    //butAddSuit.Enabled = false;
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

            }
        }
        /// <summary>
        /// 切换分组 选择不同的套餐
        /// </summary>
        private void txtTeamID_EditValueChanged(object sender, EventArgs e)
        {
            if (curCustomRegInfo == null)
                return;
            if (curCustomRegInfo.RegisterState != (int)RegisterState.No && curCustomRegInfo.RegisterState != null)
                return;
            if (txtTeamID.EditValue == null)
            {
                //删除了分组就将项目都清除
                if (curCustomRegInfo != null)
                {
                    if (curCustomRegInfo.CustomerItemGroup != null)
                    {
                        if (curCustomRegInfo.RegisterState != (int)RegisterState.Yes)
                            curCustomRegInfo.CustomerItemGroup = curCustomRegInfo.CustomerItemGroup.Where(o => o.Id != Guid.Empty || (o.PayerCat != (int)PayerCatType.NoCharge && o.PayerCat != (int)PayerCatType.ClientCharge))?.ToList();
                    }
                    curCustomRegInfo.ItemSuitBMId = null;
                    curCustomRegInfo.ItemSuitName = null;
                    curCustomRegInfo.ClientTeamInfo_Id = null;
                    gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
                    labItemSuitName.Text = "已选择套餐：";
                }
                return;
            }
            var _service = new ClientRegAppService();
            //if (txtTeamID.EditValue?.ToString() != txtTeamID.OldEditValue?.ToString())
            //{
                if (txtTeamID.EditValue != null && curCustomRegInfo.Remarks != "补检" && curCustomRegInfo.ReviewSate != 2)
                {
                    var list = txtTeamID.Properties.DataSource as List<ClientTeamInfoDto>;

                    var selectTeaminfo = list.FirstOrDefault(o => o.Id == Guid.Parse(txtTeamID.EditValue.ToString()));
                    if (selectTeaminfo == null)
                        return;

                    //更新危害因素等
                    lookUpEditClientType.EditValue = selectTeaminfo.TJType;
                    if (selectTeaminfo.OccHazardFactors != null && selectTeaminfo.OccHazardFactors.Count > 0)
                    {

                        txtRiskS.Tag = selectTeaminfo.OccHazardFactors;
                        txtRiskS.EditValue = selectTeaminfo.RiskName;
                    }
                    if (!string.IsNullOrEmpty( selectTeaminfo.WorkShop)  
                        && string.IsNullOrEmpty( curCustomRegInfo.WorkName))
                        txtWorkName.EditValue = selectTeaminfo.WorkShop;
                    if (!string.IsNullOrEmpty(selectTeaminfo.CheckType) &&
                         string.IsNullOrEmpty(curCustomRegInfo.PostState))
                        txtCheckType.EditValue = selectTeaminfo.CheckType;
                    if (!string.IsNullOrEmpty(selectTeaminfo.WorkType) &&
                        string.IsNullOrEmpty(curCustomRegInfo.TypeWork))
                        txtTypeWork.EditValue = selectTeaminfo.WorkType;

                    var groups = _service.GetClientTeamRegByClientId(new SearchClientTeamInfoDto() { Id = selectTeaminfo.Id });
                    if (groups == null)
                    {
                        ShowMessageBoxInformation("该分组无套餐项目");
                        curCustomRegInfo.CustomerItemGroup = null;
                        gridControlgroups.DataSource = null;
                        return;
                    }
                    if (groups.Count == 0)
                    {
                        ShowMessageBoxInformation("该分组无套餐项目");
                        curCustomRegInfo.CustomerItemGroup = null;
                        gridControlgroups.DataSource = null;
                        return;
                    }
                    if (curCustomRegInfo == null)
                        curCustomRegInfo = new QueryCustomerRegDto();
                    if (curCustomRegInfo.CustomerItemGroup == null)
                        curCustomRegInfo.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    else
                    {
                        var delList = new List<TjlCustomerItemGroupDto>();
                        foreach (var items in curCustomRegInfo.CustomerItemGroup)
                        {//已有正常项目的处理：
                            if (items.IsAddMinus != (int)AddMinusType.Normal)
                                continue;
                            if (items.ItemGroupName == "抹零项目")//ChargeAppService中保存抹零项目时用某个guid查出系统中存储的抹零项目，这里判断没必要在查
                                continue;                         //如果是抹零项目则跳过
                            if (!groups.Any(o => o.ItemGroup.Id == items.ItemGroupBM_Id))
                            {
                                if (items.MReceiptInfoClientlId.HasValue)
                                {
                                    ShowMessageBoxWarning("该人员所在单位已为其结算，请先作废发票，再调整分组。");
                                    txtTeamID.EditValue = txtTeamID.OldEditValue;
                                    return;
                                }
                                if (items.PayerCat == (int)PayerCatType.ClientCharge && items.CheckState != (int)PhysicalEState.Not)
                                {//原来分组里已经做了的项目转为个付未付款
                                    items.PayerCat = (int)PayerCatType.NoCharge;
                                    items.TTmoney = 0.00M;
                                    items.GRmoney = items.ItemPrice;
                                    items.PriceAfterDis = items.ItemPrice;
                                    items.DiscountRate = 1.00M;
                                    items.IsAddMinus = (int)AddMinusType.Add;
                                }
                                else
                                    delList.Add(items);
                            }
                            else
                            {
                                //处理价格和分组设置一致
                                var clientGroup = groups.FirstOrDefault(p => p.TbmItemGroupid == items.ItemGroupBM_Id);
                                if (clientGroup != null)
                                {
                                    items.PriceAfterDis = clientGroup.ItemGroupDiscountMoney;
                                    items.DiscountRate = clientGroup.Discount;
                                    items.ItemPrice = clientGroup.ItemGroupMoney;
                                    items.IsAddMinus = clientGroup.IsAddMinus ?? (int)AddMinusType.Normal;

                                    if (clientGroup.PayerCatType == (int)PayerCatType.ClientCharge)
                                    {
                                        items.PayerCat = clientGroup.PayerCatType;
                                        items.GRmoney = 0.00M;
                                        items.TTmoney = clientGroup.ItemGroupDiscountMoney;
                                    }
                                    else if (clientGroup.PayerCatType == (int)PayerCatType.PersonalCharge)
                                    {
                                        items.GRmoney = clientGroup.ItemGroupDiscountMoney;
                                        items.TTmoney = 0.00M;
                                        //items.PayerCat = (int)PayerCatType.NoCharge;
                                    }
                                }
                            }
                        }
                        foreach (var del in delList)
                        {
                            curCustomRegInfo.CustomerItemGroup.Remove(del);
                        }

                        if (curCustomRegInfo.RegisterState != (int)RegisterState.Yes)
                            curCustomRegInfo.CustomerItemGroup = curCustomRegInfo.CustomerItemGroup.Where(o => o.Id != Guid.Empty || (o.PayerCat != (int)PayerCatType.NoCharge && o.PayerCat != (int)PayerCatType.ClientCharge))?.ToList();
                    }
                    foreach (var g in groups)
                    {
                        if (curCustomRegInfo.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == g.ItemGroup.Id))
                        {
                            continue;
                        }
                        var info = new TjlCustomerItemGroupDto()
                        {
                            ItemGroupBM_Id = g.ItemGroup.Id,
                            ItemPrice = g.ItemGroupMoney,
                            PriceAfterDis = g.ItemGroupDiscountMoney,
                            ItemGroupName = g.ItemGroupName,
                            DiscountRate = g.Discount,
                            GRmoney = 0.00M,
                            IsAddMinus = (int)AddMinusType.Normal,
                            ItemGroupOrder = g.ItemGroupOrder,
                            PayerCat = g.PayerCatType,
                            TTmoney = g.ItemGroupDiscountMoney,
                            ItemSuitId = g.ItemSuitId,//g.ItemSuit_Id,
                            ItemSuitName = g.ItemSuitName,
                            CheckState = (int)ProjectIState.Not,
                            GuidanceSate = (int)PrintSate.NotToPrint,
                            BarState = (int)PrintSate.NotToPrint,
                            RequestState = (int)PrintSate.NotToPrint,
                            RefundState = (int)PayerCatType.NotRefund,
                            BillingEmployeeBMId = CurrentUser.Id,
                            SummBackSate = (int)SummSate.NotAlwaysCheck,
                            SFType = Convert.ToInt32(g.ItemGroup.ChartCode),

                        };
                        if (g.PayerCatType == (int)PayerCatType.ClientCharge)
                        {
                            info.GRmoney = 0.00M;
                            info.TTmoney = g.ItemGroupDiscountMoney;
                        }
                        else if (g.PayerCatType == (int)PayerCatType.PersonalCharge)
                        {
                            info.GRmoney = g.ItemGroupDiscountMoney;
                            info.TTmoney = 0.00M;
                            info.PayerCat = (int)PayerCatType.NoCharge;
                        }
                        var depart = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == g.ItemGroup.DepartmentId);
                        if (depart != null)
                        {
                            info.DepartmentId = depart.Id;
                            info.DepartmentName = depart.Name;
                            info.DepartmentOrder = depart.OrderNum;
                        }
                        if (g.IsZYB.HasValue)
                        {
                            info.IsZYB = g.IsZYB;
                        }
                        curCustomRegInfo.CustomerItemGroup.Add(info);
                    }
                    gridControlgroups.DataSource = null;
                    gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
                    curCustomRegInfo.ItemSuitBMId = groups.FirstOrDefault().ItemSuitId;
                    curCustomRegInfo.ItemSuitName = groups.FirstOrDefault().ItemSuitName;

                    labItemSuitName.Text = "已选择了套餐：【" + curCustomRegInfo.ItemSuitName + "】";
                    if (curCustomRegInfo.Id == Guid.Empty)
                    {
                        if (selectTeaminfo.EmailStatus == 2)
                            curCustomRegInfo.EmailReport = (int)MessageEmailState.Open;
                        else
                            curCustomRegInfo.EmailReport = (int)MessageEmailState.Close;
                        if (selectTeaminfo.MessageStatus == 2)
                            curCustomRegInfo.Message = (int)MessageEmailState.Open;
                        else
                            curCustomRegInfo.Message = (int)MessageEmailState.Close;
                        curCustomRegInfo.HaveBreakfast = selectTeaminfo.BreakfastStatus;
                        if (selectTeaminfo.HealthyMGStatus == 2)
                            curCustomRegInfo.MailingReport = 1;
                        else
                            curCustomRegInfo.MailingReport = 2;
                        curCustomRegInfo.BlindSate = selectTeaminfo.BlindSate;
                        curCustomRegInfo.CustomerType = selectTeaminfo.TJType;
                    }
                }
            //}
        }

        /// <summary>
        /// 添加登记按钮事件
        /// </summary>
        private void butAddReg_Click(object sender, EventArgs e)
        {
            butAddReg.Enabled = false;
            //清空控件数据、清空当前登记人信息
            ClearControlData();
            DateChekDate.EditValue = _commonAppService.GetDateTimeNow().Now;//测试人员要求默认今天
            try
            {
                //txtCustoemrCode.EditValue = iIDNumberAppService.CreateArchivesNumBM();
                //if (string.IsNullOrWhiteSpace(txtArchivesNum.Text))
                //{
                //    txtArchivesNum.EditValue = txtCustoemrCode.EditValue;
                //}
                var customBM = iIDNumberAppService.CreateArchivesNumBM();
                txtArchivesNum.EditValue = txtCustoemrCode.EditValue = customBM;
                if (curCustomRegInfo == null)
                {
                    curCustomRegInfo = new QueryCustomerRegDto() { CustomerItemGroup = new List<TjlCustomerItemGroupDto>(), Customer = new QueryCustomerDto() { ArchivesNum = customBM }, CustomerBM = customBM };
                }
                SetBtnEnabled();
                //控制定位
                var ISHIS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 155)?.Remarks;
                if (!string.IsNullOrEmpty(ISHIS) && ISHIS == "1")
                {
                    txtVisitCard.Focus();
                }
                else
                {

                    txtName.Focus();
                }
                IsReging = true;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        /// <summary>
        /// 取消登记
        /// </summary>
        private void butCancel_Click(object sender, EventArgs e)
        {
            if (curCustomRegInfo != null)
            {
                if (curCustomRegInfo.Id != Guid.Empty)
                {
                    EntityDto<Guid> cusName = new EntityDto<Guid>();
                    cusName.Id = curCustomRegInfo.Id;
                    // CustomerRegCostDto customerReg = customerSvr.GetCustomerRegCost(cusName);
                    CustomerRegCostDto customerReg = _chargeAppService.GetsfState(cusName);
                    if (customerReg.CostState != (int)PayerCatType.NoCharge)
                    {
                        MessageBox.Show("该体检人已收费不能取消登记！");
                        return;
                    }
                    if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
                    {
                        DialogResult dr = XtraMessageBox.Show("是否取消登记？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            #region HIS接口
                           
                            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                            if (HISjk == "1")
                            {
                                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;

                                if (HISName.Contains("江苏鑫亿"))
                                {
                                    WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                                   // neInterface.CancelTTPay(curCustomRegInfo.Customer.ArchivesNum, curCustomRegInfo.CustomerBM, CurrentUser.EmployeeNum, CurrentUser.Name);
                                    var outMess = neInterface.CancelTTPay(curCustomRegInfo.CustomerBM , ("删除申请单" + "|" + HISName));
                                    if (outMess.Code != "0")
                                    {
                                        MessageBox.Show(outMess.ReSult);
                                        return;
                                    }

                                }
                            
                            }
                            #region 登记推送
                            var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 1)?.Remarks;
                            var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                            if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                            {
                                NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                                jfyt.GetSqnasync(curCustomRegInfo.CustomerBM + "|" + DJTSCJ + "|" + CurrentUser.Name +"|作废", false); //体检号，是否撤销

                            }
                            #endregion
                            #endregion
                            curCustomRegInfo.RegisterState = (int)RegisterState.No;
                            try
                            {
                                customerSvr.CancelReg(new CustomerRegViewDto() { Id = curCustomRegInfo.Id });
                                curCustomRegInfo.LoginDate = null;
                                //curCustomRegInfo = customerSvr.RegCustomer(new List<QueryCustomerRegDto>() { curCustomRegInfo }).FirstOrDefault();
                                //日志
                                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                                createOpLogDto.LogBM = curCustomRegInfo.CustomerBM;
                                createOpLogDto.LogName = curCustomRegInfo.Customer.Name;
                                createOpLogDto.LogText = "取消登记";
                                createOpLogDto.LogDetail = "";
                                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                                _commonAppService.SaveOpLog(createOpLogDto);
                                LoadCustomerData(curCustomRegInfo);
                                IsReging = false;
                                return;
                            }
                            catch (UserFriendlyException ex)
                            {
                                curCustomRegInfo.RegisterState = (int)RegisterState.Yes;
                                ShowMessageBox(ex);
                                return;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        ShowMessageBoxInformation("当前未登记。");
                        return;
                    }
                }
            }
            ShowMessageBoxInformation("当前数据未保存不能取消登记");
        }
        /// <summary>
        /// 确认登记
        /// </summary>
        private void butOK_Click(object sender, EventArgs e)
        {
            #region 单位锁定不能登记
            if (!string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()))
            {
                
                EntityDto<Guid> entity = new EntityDto<Guid>();
                entity.Id = Guid.Parse(txtClientRegID.EditValue?.ToString());
                var clientinfo = clientRegs.FirstOrDefault(p => p.Id == entity.Id);
                if (clientRegs != null && clientinfo != null)
                { 
                    //优化2023-10-21
                   //var outCode = _clientReg.getSd(entity);
                if (clientinfo.SDState == 1)
                {
                    MessageBox.Show("该单位已锁定，不能登记！");
                    return;
                }
                }

            }
            #endregion
            var data = GetControlSaveData();
            if (data == null)
            {
                ShowMessageBoxWarning("没有可以保存的数据。");
                return;
            }        
            if (data.ClientRegId.HasValue && !data.ClientTeamInfo_Id.HasValue)
            {
                ShowMessageBoxWarning("团体登记必须选择分组。");
                return;
            }
            if (data == null)
                return;
            if (data.ClientTeamInfo_Id == null)
            {
                if (data.CustomerItemGroup != null)
                {
                    if (data.CustomerItemGroup.Any(o => o.PayerCat == (int)PayerCatType.ClientCharge))
                    {
                        DialogResult dr = XtraMessageBox.Show("您未选择单位或单位分组，将清除单位已付项目，是否继续？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            data.CustomerItemGroup = data.CustomerItemGroup.Where(o => o.PayerCat != (int)PayerCatType.ClientCharge)?.ToList();
                            curCustomRegInfo.CustomerItemGroup = data.CustomerItemGroup;
                            gridControlgroups.DataSource = null;
                            gridControlgroups.DataSource = data.CustomerItemGroup;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

            }
            var closeWait = false;
            butOK.Enabled = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            butOK.Enabled = false;
            try
            {
                //if (data.CustomerItemGroup.Any(o => o.PayerCat == (int)PayerCatType.NoCharge))
                //    data.CostState = (int)PayerCatType.NoCharge;
                if (curCustomRegInfo.CustomerItemGroup == null)
                {
                    if (closeWait)
                    {
                        if (splashScreenManager.IsSplashFormVisible)
                            splashScreenManager.CloseWaitForm();
                    }
                    ShowMessageBoxWarning("请先添加组合项目再保存。");
                    return;
                }
                if (curCustomRegInfo.CustomerItemGroup.Count == 0)
                {
                    if (closeWait)
                    {
                        if (splashScreenManager.IsSplashFormVisible)
                            splashScreenManager.CloseWaitForm();
                    }
                    ShowMessageBoxWarning("请先添加组合项目再保存。");
                    return;
                }
                var IsChage = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.IsCharge, 10);
                if (IsChage != null && IsChage.Text == "0")
                {
                    data = NoChargeState(data);
                }
                else
                {
                    #region 保存前验证收费是否发生改变
                    if (data.Id != Guid.Empty)
                    {
                        if (checkChargeState(data) == false)
                        {
                            MessageBox.Show("收费状态发生改变，请刷新后再操作！");
                            return;
                        }
                    }
                }
                #endregion
                #region 验证项目性别状态                
                if (checkSexState(data) == false)
                {
                    MessageBox.Show("所选组合和性别不匹配，请检查");
                    return;
                }
                #endregion
                SetPicToData();
                //南京飓风接口挂号
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "南京飓风" && data.NucleicAcidType.ToString() == "1096")
                    {
                        var groupinput = data.CustomerItemGroup.Where(o => o.PriceAfterDis != 0).Select(o => new ChargeBM
                        { Id = o.ItemGroupBM_Id, Name = o.ItemGroupName }).ToList();
                        var noprice = customerSvr.NoPriceGroup(groupinput);
                        if (noprice.Count > 0)
                        {
                            string nochecklist = string.Join(",", noprice.Select(o => o.Name).ToList());
                            MessageBox.Show(nochecklist + "未做医嘱项目对应，不能登记！请处理项目，或选择挂号科室：体检中心");
                            return;
                        }
                    }
                    //2为医保不能打折
                    if (HISName == "北仑" && data.NucleicAcidType.ToString() == "2")
                    {
                        var groupinput = data.CustomerItemGroup.Where(o => o.PriceAfterDis != 0 && o.IsAddMinus != (int)AddMinusType.Minus).Select(o => new ChargeBM
                        { Id = o.ItemGroupBM_Id, Name = o.ItemGroupName }).ToList();
                        var noprice = customerSvr.NoPriceGroup(groupinput);
                        if (noprice.Count > 0)
                        {
                            string nochecklist = string.Join(",", noprice.Select(o => o.Name).ToList());
                            MessageBox.Show(nochecklist + "未做医嘱项目对应，不能登记！请处理项目，或选择挂号科室：自费");
                            return;
                        }
                        var NoPay = data.CustomerItemGroup.Where(o => o.PayerCat == (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus).ToList();
                        var yj = NoPay.Sum(o => o.ItemPrice);
                        var zhj = NoPay.Sum(o => o.PriceAfterDis);
                        if (yj != zhj)
                        {
                            MessageBox.Show("医保项目不能打折！");
                            return;
                        }
                        //医嘱项目价格和组合价格不一致不能登记
                        //var groupPriceDto = data.CustomerItemGroup.Where(o => o.PriceAfterDis != 0 && o.IsAddMinus != (int)AddMinusType.Minus).Select(o => new GroupPriceDto
                        //{ Id = o.ItemGroupBM_Id, ItemPrice = o.ItemPrice }).ToList();
                        //var ErrPrice = customerSvr.ErrPriceGroup(groupPriceDto);
                        //if (ErrPrice.Count > 0)
                        //{
                        //    var errMess = string.Join(",", ErrPrice.Select(o => o.Name).ToList());
                        //    MessageBox.Show(errMess);
                        //    return;
                        //}
                    }
                    //医保不能打折              
                    if ((HISName == "江苏鑫亿" && data.Remarks?.ToString() == "医保") ||
                        (HISName == "江苏鑫亿四院" && data.NucleicAcidType.ToString() == "1096"))
                    {
                      
                        var NoPay = data.CustomerItemGroup.Where(o => o.PayerCat == (int)PayerCatType.NoCharge && o.IsAddMinus != (int)AddMinusType.Minus).ToList();
                        var yj = NoPay.Sum(o => o.ItemPrice);
                        var zhj = NoPay.Sum(o => o.PriceAfterDis);
                        if (yj != zhj)
                        {
                            MessageBox.Show("医保项目不能打折！");
                            return;
                        }
                      
                    }
                    //挂号
                    if (string.IsNullOrEmpty(data.HisSectionNum) && data.CustomerItemGroup.Where(o => o.PayerCat == (int)PayerCatType.NoCharge).Sum(o => o.GRmoney) > 0)
                    {


                        if (HISName == "南京飓风" || HISName == "世轩")
                        {//7|姓名|性别|年龄|证件类型|证件编码|手机电话|地址|科室编码|操作员
                            string cusName = data.Customer.Name;
                            if (!string.IsNullOrEmpty(txtFp.Text))
                            { cusName = txtFp.Text; }

                            string instr = "7|" + cusName + "|" + data.Customer.Sex + "|"
                                + data.Customer.Age + "|身份证|" + data.Customer.IDCardNo + "|"
                                + data.Customer.Mobile + "|" + data.Customer.Address + "|" + data.NucleicAcidType + "|"
                                + CurrentUser.EmployeeNum;

                            var outstr = customerSvr.SaveHisInfo(new InCarNumDto { CardNum = instr, HISName = HISName });
                            if (string.IsNullOrEmpty(outstr.CardNum))
                            {
                                MessageBox.Show("HIS挂号失败！，请重新登记！");
                                //return;
                            }
                            else
                            {
                                //|门诊号|患者ID|
                                var sp = outstr.CardNum.Split('|');

                                data.Customer.VisitCard = sp[1];
                                //data.Customer.ArchivesNum = sp[2];
                                data.HisPatientId = sp[2];
                                data.HisSectionNum = sp[1];
                            }
                        }
                    }
                    //修改发票信息
                    else if (!string.IsNullOrEmpty(data.HisSectionNum) && !string.IsNullOrEmpty(data.FPNo)
                        && data.NucleicAcidType.HasValue
                        && data.CustomerItemGroup.Where(o => o.PayerCat == (int)PayerCatType.NoCharge).Sum(o => o.GRmoney) > 0)
                    {
                        if (HISName == "南京飓风" || HISName == "世轩")
                        {//7|姓名|性别|年龄|证件类型|证件编码|手机电话|地址|科室编码|操作员
                            string cusName = data.Customer.Name;
                            if (!string.IsNullOrEmpty(txtFp.Text))
                            { cusName = txtFp.Text; }

                            string instr = data.HisSectionNum + "|" + data.FPNo + "|" + data.NucleicAcidType + "|修改发票";

                            var outstr = customerSvr.SaveHisInfo(new InCarNumDto { CardNum = instr, HISName = HISName });
                            if (string.IsNullOrEmpty(outstr.CardNum))
                            {
                                MessageBox.Show("更新His数据失败！，请重新登记！");
                                //return;
                            }

                        }
                    }
                }
                //保存登记信息

                //var tq = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 123)?.Remarks;
                //if (tq != null && tq == "1")
                //{
                    curCustomRegInfo = customerSvr.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();

                //}
                //else
                //{
                //    curCustomRegInfo = customerSvr.RegCustomerNew(data).FirstOrDefault();
                //}
                #region HIS接口
                //var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "南京飓风" || HISName == "世轩")
                    {
                        ICustomerAppService _customerSvr = new CustomerAppService();
                        TJSQDto input = new TJSQDto();
                        input.BRCS = data.Customer.Birthday;
                        input.BRH = data.CustomerBM;
                        input.BRKH = data.Customer.VisitCard;
                        input.BRLXDH = data.Customer.Mobile;
                        input.BRLXDZ = data.Customer.Address;
                        input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                        if (!string.IsNullOrEmpty(txtFp.Text))
                        { input.BRXM = txtFp.Text; }
                        else
                        {
                            input.BRXM = data.Customer.Name;
                        }
                        input.DJGH = int.Parse(Variables.User.EmployeeNum);
                        input.DJTIME = data.LoginDate;
                        //input.DWMC = data.ClientReg?.ClientInfo?.ClientName ?? "";
                        input.DWMC = txtClientRegID.Text;

                        input.HISName = HISName;
                        input.UserID = Variables.User.Id;
                        input.CustomerRegId = curCustomRegInfo.Id;
                        var appliy = _customerSvr.InsertSFCharg(input);
                        if (appliy != null)
                        {
                            if (appliy.ApplicationNum.Contains("失败"))
                            {
                                MessageBox.Show(appliy.ApplicationNum);
                            }

                        }
                    }
                    else if (HISName == "北仑")
                    {
                        ICustomerAppService _customerSvr = new CustomerAppService();
                        TJSQDto input = new TJSQDto();
                        input.BRCS = data.Customer.Birthday;
                        input.BRH = data.CustomerBM;
                        input.BRKH = data.Customer.VisitCard;
                        input.BRLXDH = data.Customer.Mobile;
                        input.BRLXDZ = data.Customer.Address;
                        input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                        input.BRXM = data.Customer.Name;
                        if (Variables.User != null)
                        {
                            if (int.TryParse(Variables.User.EmployeeNum, out int EmpBM))
                            {
                                input.DJGH = int.Parse(Variables.User.EmployeeNum);
                            }

                            input.UserID = Variables.User.Id;
                        }
                        input.DJTIME = data.LoginDate;
                        // input.DWMC = data.ClientReg?.ClientInfo?.ClientName ?? "";
                        input.HISName = HISName;
                        input.CustomerRegId = curCustomRegInfo.Id;
                        var appliy = _customerSvr.InsertSFCharg(input);
                        if (appliy != null)
                        {
                            if (appliy.ApplicationNum.Contains("失败"))
                            {
                                MessageBox.Show(appliy.ApplicationNum);
                            }
                        }
                    }
                    else if (HISName == "东软")
                    {

                        //NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                        //neInterface.shijian(data.Customer.ArchivesNum,data.CustomerBM);                     

                        ICustomerAppService _customerSvr = new CustomerAppService();
                        TJSQDto input = new TJSQDto();
                        input.BRCS = data.Customer.Birthday;
                        input.BRH = data.CustomerBM;
                        input.BRKH = data.Customer.VisitCard;
                        input.BRLXDH = data.Customer.Mobile;
                        input.BRLXDZ = data.Customer.Address;
                        input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                        input.BRXM = data.Customer.Name;
                        if (Variables.User != null)
                        {
                            if (int.TryParse(Variables.User.EmployeeNum, out int EmpBM))
                            {
                                input.DJGH = int.Parse(Variables.User.EmployeeNum);
                            }
                            input.UserID = Variables.User.Id;
                        }
                        input.DJTIME = data.LoginDate;
                        input.HISName = HISName;
                        input.CustomerRegId = curCustomRegInfo.Id;
                        var appliy = _customerSvr.InsertSFCharg(input);
                        if (appliy != null)
                        {
                            //收费
                            //WindowsFormsApp1.SFInterface sFInterface = new WindowsFormsApp1.SFInterface();
                            //sFInterface.InsertSFInforAsync(appliy.ApplicationNum);

                            NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                            var outstr = neInterface.shijianFH(curCustomRegInfo.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);
                            if (!string.IsNullOrEmpty(outstr))
                            {
                                var ts = outstr.Split('&');
                                if (ts.Length >= 1)
                                {
                                    var custs = ts[0].Split('|');
                                    if (custs.Length >= 2 && curCustomRegInfo.Customer != null)
                                    {
                                        curCustomRegInfo.Customer.CardNumber = custs[0];
                                        curCustomRegInfo.Customer.SectionNum = custs[1];
                                    }
                                    if (ts.Length >= 2)
                                    {
                                        curCustomRegInfo.HisSectionNum = ts[1];
                                    }
                                }
                            }
                            // curCustomRegInfo = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });

                            //curCustomRegInfo.HisSectionNum = appliy.Remark;
                            //curCustomRegInfo.Customer.SectionNum = appliy.FPName;

                            if (appliy.ApplicationNum.Contains("失败"))
                            {
                                MessageBox.Show(appliy.ApplicationNum);
                            }
                        }
                        else
                        {
                            NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                            var outstr = neInterface.shijianFH(curCustomRegInfo.Customer.ArchivesNum, data.CustomerBM, appliy.ApplicationNum);
                            if (!string.IsNullOrEmpty(outstr))
                            {
                                var ts = outstr.Split('&');
                                if (ts.Length >= 1)
                                {
                                    var custs = ts[0].Split('|');
                                    if (custs.Length >= 2 && curCustomRegInfo.Customer != null)
                                    {
                                        curCustomRegInfo.Customer.CardNumber = custs[0];
                                        curCustomRegInfo.Customer.SectionNum = custs[1];
                                    }
                                    if (ts.Length >= 2)
                                    {
                                        curCustomRegInfo.HisSectionNum = ts[1];
                                    }
                                }
                            }

                            //curCustomRegInfo = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });

                        }
                        try
                        {
                            //健康芜湖上传报告
                            if (data.InfoSource.HasValue && data.InfoSource == 2)
                            {
                                //健康芜湖接口上传到检状态
                                HealthInWuHuInterface healthInWuHuInterface = new HealthInWuHuInterface();
                                healthInWuHuInterface.IsSingle(data.CustomerBM);
                                curCustomRegInfo = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });

                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("上传健康芜湖接口失败：" + ex.Message);
                            //throw;
                        }
                    }
                    else if (HISName == "东软宜兴善卷骨科")
                    {
                        ICustomerAppService _customerSvr = new CustomerAppService();
                        TJSQDto input = new TJSQDto();
                        input.BRCS = data.Customer.Birthday;
                        input.BRH = data.CustomerBM;
                        input.BRKH = data.Customer.VisitCard;
                        input.BRLXDH = data.Customer.Mobile;
                        input.BRLXDZ = data.Customer.Address;
                        input.BRXB = SexHelper.CustomSexFormatter(data.Customer.Sex);
                        input.BRXM = data.Customer.Name;
                        if (Variables.User != null)
                        {
                            if (int.TryParse(Variables.User.EmployeeNum, out int EmpBM))
                            {
                                input.DJGH = int.Parse(Variables.User.EmployeeNum);
                            }
                            input.UserID = Variables.User.Id;
                        }
                        input.DJTIME = data.LoginDate;
                        input.HISName = HISName;
                        input.CustomerRegId = curCustomRegInfo.Id;
                        var appliy = _customerSvr.InsertSFCharg(input);
                        if (appliy != null)
                        {
                            //收费
                            //MessageBox.Show("开始调用接口");
                            WindowsFormsApp1.SFInterface sFInterface = new WindowsFormsApp1.SFInterface();
                            sFInterface.InsertSFInforYX(appliy.ApplicationNum, CurrentUser.EmployeeNum);

                            curCustomRegInfo.HisSectionNum = appliy.Remark;
                            curCustomRegInfo.Customer.SectionNum = appliy.FPName;

                            if (appliy.ApplicationNum.Contains("失败"))
                            {
                                MessageBox.Show(appliy.ApplicationNum);
                            }
                        }
                    }
                    else if (HISName == "天易")
                    {
                        //WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                        //neInterface.CreateApAsync(data.Customer.ArchivesNum, data.CustomerBM, CurrentUser.EmployeeNum, CurrentUser.Name);

                        WindowsFormsApp1.SFInterface sFInterface = new WindowsFormsApp1.SFInterface();
                        sFInterface.InsertTYSFInforYXAsync(data.CustomerBM);

                    }
                    else if (HISName.Contains("江苏鑫亿"))
                    {
                        string nm = "";
                        var kdys = CurrentUser.EmployeeNum;
                        var kdysname = CurrentUser.Name;
                        if (HISName.Contains("四院"))
                        {
                            var user = DefinedCacheHelper.GetComboUsers().ToList();
                            if (!string.IsNullOrEmpty(cglueJianChaYiSheng.EditValue?.ToString()))
                            {
                                var USerID = (long)cglueJianChaYiSheng.EditValue;
                                var curruser = user.FirstOrDefault(p => p.Id == USerID)?.EmployeeNum;
                                kdys = curruser;
                                kdysname = cglueJianChaYiSheng.Text;
                            }
                             
                            nm = "四院";
                        }
                        WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();

                        // neInterface.CreateApAsync(curCustomRegInfo.Customer.ArchivesNum, data.CustomerBM, kdys, kdysname + nm);
                        neInterface.CreateAp(curCustomRegInfo.Customer.ArchivesNum, data.CustomerBM, kdys, kdysname + nm);
                    }
                    else if (HISName == "金风易通")
                    {
                        NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                        jfyt.GetSqnasync(data.CustomerBM); //体检号，是否撤销

                    }
                }
                #endregion
                #region 登记推送
                var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 1)?.Remarks;
                var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                {
                    NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                    jfyt.GetSqnasync(data.CustomerBM +"|" + DJTSCJ  + "|" + CurrentUser.Name, false); //体检号，是否撤销

                }
                #endregion
               
                //套餐卡使用
                if (!string.IsNullOrEmpty(curCustomRegInfo.Customer.CardNumber)/* && curCustomRegInfo.ItemSuitBMId.HasValue*/)
                {
                    InCardChageDto inCardChage = new InCardChageDto();
                    inCardChage.CusRegId = curCustomRegInfo.Id;
                    if (curCustomRegInfo.ItemSuitBMId.HasValue)
                    {
                        inCardChage.SuitId = curCustomRegInfo.ItemSuitBMId.Value;
                    }
                    inCardChage.CardNo = curCustomRegInfo.Customer.CardNumber;
                    var isOk = _chargeAppService.ChargeCard(inCardChage);
                    var datals = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.Text });
                    LoadCustomerData(datals);
                }

                LoadCustomerDataforReg(curCustomRegInfo);
                //优化每次登记不刷新2023-10-21
                //QueryRegNumbers();
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
                //ShowMessageSucceed("保存成功。");
                txtCustoemrCode.Focus();
                IsReging = false;
                var strPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 110).Remarks;
                if (strPD == "Y")
                {
                    //排队接口
                    alertInfo.Show(this, "提示!", GetBTPD(curCustomRegInfo.CustomerBM));
                }
                #region 超声排队
                var strCSPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 111)?.Remarks;

                if (strCSPD != null && strCSPD == "Y")
                {
                    try
                    {
                        string strpdsql = "  select * from tj_requireinfo where DepartmentID='1001' and SelfBH='"
                           + curCustomRegInfo.CustomerBM + "'";
                        //string strcount = nowteam.SelectSql(strpdsql);

                        SqlConnection tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                        tjcon.Open();//连接体检数据库
                        SqlCommand tjcom = new SqlCommand(strpdsql, tjcon);
                        SqlDataReader tjread = tjcom.ExecuteReader();
                        if (tjread.HasRows)
                        {

                            Btpd.BLL.UserInfo userinfo = new Btpd.BLL.UserInfo(Btpd_base.PublicTool.SqlConnection);
                            List<Btpd.Model.UserInfo> lsuserinfo = userinfo.GetModelList(" tjcode='" + curCustomRegInfo.CustomerBM + "'");
                            Btpd.Model.UserInfo EnteruserinfoGd = new Btpd.Model.UserInfo();
                            if (lsuserinfo.Count > 0)
                            {
                                EnteruserinfoGd = lsuserinfo[0];
                            }
                            else
                            {
                                EnteruserinfoGd = PDCCInterface.getuserinfo(string.Empty, curCustomRegInfo.CustomerBM);
                            }
                            if (EnteruserinfoGd != null)
                            {
                                var ts = "登记成功,彩超排队号为:" + EnteruserinfoGd.TeamOrder;
                                alertInfo.Show(this, "提示!", ts);
                                labPD.Text = ts;

                            }
                            else
                            { labPD.Text = ""; }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                #endregion
                #region 提示当天是否有重名
             
                var qtcz = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 156)?.Remarks;
                if (qtcz == "1")
                {

                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id = curCustomRegInfo.Id;
                    var cuslIst = customerSvr.getRepeatCus(chargeBM);
                    if (cuslIst != null && cuslIst.Count > 0  )
                    {
                        string namestr = string.Join("、", cuslIst.Select(p=>p.CustomerBM).ToList()).TrimEnd('、');
                        MessageBox.Show("当天体检号：" + namestr  +",和当前体检人："+ curCustomRegInfo.Customer.Name
                            + "，姓名、性别重复！");

                    }
                }
                #endregion
                SetBtnEnabled();
                if (checkDD.Checked == true)
                {
                
                    if (HISjk == "1")
                    {
                        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                        if (HISName == "江苏鑫亿四院")
                        {
                            ChargeBM chargeBM = new ChargeBM();
                            chargeBM.Id = curCustomRegInfo.Id;
                            var slo = customerSvr.getupdate(chargeBM);
                            if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                            {
                                MessageBox.Show("检验申请单还未申请成功，请稍后再打印条码，如果还未成功请从新登记！");
                                return;
                            }
                        }
                    }
                    ThreadStart startJC1 = new ThreadStart(printbar);
                    Thread threadJC1 = new Thread(startJC1);
                    threadJC1.Start();
                    threadJC1.IsBackground = true;
                    ThreadStart startJC = new ThreadStart(PrintGuidance);
                    Thread threadJC = new Thread(startJC);
                    threadJC.Start();
                    threadJC.IsBackground = true;
                }
            }

            catch (UserFriendlyException ex)
            {

                butOK.Enabled = true;
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
                ShowMessageBox(ex);
                return;
            }
            finally
            {
                butOK.Enabled = true;
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
            butOK.Enabled = true;
            butAddReg.Enabled = true;

        }
        private void setAlter()
        {
            //以毫秒为单位
            alertInfo.AutoFormDelay = 4000;

            //出现的效果方式
            alertInfo.FormShowingEffect = AlertFormShowingEffect.FadeIn;

            //弹出的速度
            alertInfo.FormDisplaySpeed = AlertFormDisplaySpeed.Fast;
        }

        /// <summary>
        /// 性别改变后重新绑定套餐数据
        /// </summary>
        private void gridLookUpSex_EditValueChanged(object sender, EventArgs e)
        {
            var input = new SearchItemSuitDto() { ItemSuitType = (int)ItemSuitType.Suit };
            if (!string.IsNullOrEmpty(lookUpEditClientType.EditValue?.ToString()))
            {
                input.ExaminationType = (int)lookUpEditClientType.EditValue;
            }
            cuslookItemSuit.Properties.DataSource = QuerySuits(input, true);
            if (Convert.ToInt32(gridLookUpSex.EditValue) != (int)Sex.GenderNotSpecified)
                edior_ButtonClick(txtTeamID, new ButtonPressedEventArgs(new EditorButton(ButtonPredefines.Delete)));
            //txtClientRegID_EditValueChanged(sender, e);
            //刷新分组列表
            try
            {
                if (!string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()))
                {

                    var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(txtClientRegID.EditValue?.ToString()) });
                    if (!string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()) && gridLookUpSex.EditValue?.ToString() != ((int)Sex.GenderNotSpecified).ToString())
                    {
                        list = list.Where(o => o.Sex == Convert.ToInt32(gridLookUpSex.EditValue) || o.Sex == (int)Sex.GenderNotSpecified)?.ToList();
                    }

                    if (!string.IsNullOrEmpty(txtTeamID.EditValue?.ToString()) && !list.Any(p => p.Id == (Guid)txtTeamID.EditValue))
                    {

                        txtTeamID.EditValue = null;
                    }
                    txtTeamID.Properties.DataSource = list;
                    cuslookItemSuit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 套餐选择框值改变
        /// </summary>
        private void cuslookItemSuit_EditValueChanged(object sender, EventArgs e)
        {
            if (cuslookItemSuit.EditValue == null)
                return;
            var editor = sender as GridLookUpEdit;
            var data = editor.GetSelectedDataRow();
            if (data != null)
            {
                var itemsuit = data as SimpleItemSuitDto;
                FullItemSuitDto suitInfo = null;
                try
                {
                    var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto() { Id = itemsuit.Id });
                    if (ret != null)
                    {
                        if (ret.Count > 0)
                        {
                            suitInfo = ret.First();
                            suitInfo.ItemSuitItemGroups = suitInfo.ItemSuitItemGroups.OrderBy(o => o.ItemGroup?.Department?.OrderNum).ThenBy(o => o.ItemGroup?.OrderNum).ToList();
                        }
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                if (suitInfo == null)
                {
                    ShowMessageBoxWarning("您选择的套餐内无项目。");
                    return;
                }
                if (suitInfo.ItemSuitItemGroups.Count <= 0)
                {
                    ShowMessageBoxWarning("您选择的套餐内无项目。");
                    return;
                }
                var str = new StringBuilder();
                if (curCustomRegInfo == null)
                    curCustomRegInfo = new QueryCustomerRegDto();
                if (curCustomRegInfo.ItemSuitBMId.HasValue)
                {
                    if (curCustomRegInfo.ItemSuitBMId != itemsuit.Id)
                    {
                        str.Append("您已经选择了【");
                        str.Append(curCustomRegInfo.ItemSuitName);
                        str.Append("】套餐，");
                    }
                    else
                    {
                        ShowMessageBoxWarning("您已经使用了该套餐，无需重复添加。");
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(str?.ToString()))
                {
                    str.Append("确定切换套餐【");
                }
                else
                    str.Append("确定使用套餐【");

                str.Append(itemsuit.ItemSuitName);
                str.Append("】吗？");
                DialogResult dr = XtraMessageBox.Show(str.ToString(), "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    if (curCustomRegInfo.CustomerItemGroup == null)
                        curCustomRegInfo.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    if (curCustomRegInfo.ItemSuitBMId.HasValue)
                    {//切换套餐去掉所有已选未检查未收费的项目
                        curCustomRegInfo.CustomerItemGroup = curCustomRegInfo.CustomerItemGroup.Where(o => o.CheckState != (int)PayerCatType.NoCharge)?.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder).ToList();
                    }
                    if (txtClientRegID.EditValue == null && txtTeamID.EditValue == null)
                    {//个人登记添加套餐的情况
                        foreach (var item in suitInfo.ItemSuitItemGroups)
                        {
                            var group = new TjlCustomerItemGroupDto();
                            group.ItemGroupBM_Id = item.ItemGroupId;
                            group.ItemPrice = item.ItemPrice == null ? 0.00M : item.ItemPrice.Value;
                            group.PriceAfterDis = item.PriceAfterDis == null ? 0.00M : item.PriceAfterDis.Value;
                            group.ItemGroupName = item.ItemGroup?.ItemGroupName;
                            group.DiscountRate = item.Suitgrouprate == null ? 0.00M : item.Suitgrouprate.Value;
                            group.GRmoney = item.PriceAfterDis == null ? 0.00M : item.PriceAfterDis.Value;
                            if (textEditRegRemark.Text == "医保")
                            {
                                group.PriceAfterDis = group.ItemPrice;
                                group.DiscountRate = 1;
                                group.GRmoney = group.ItemPrice;
                            }
                            group.IsAddMinus = (int)AddMinusType.Normal;//是否加减项 正常项目
                            group.ItemGroupOrder = item.ItemGroup?.OrderNum;
                            group.PayerCat = (int)PayerCatType.NoCharge;
                            group.TTmoney = 0.00M;
                            group.ItemSuitId = itemsuit.Id;
                            group.ItemSuitName = itemsuit.ItemSuitName;
                            group.SFType = Convert.ToInt32(item.ItemGroup?.ChartCode);
                            var depart = DefinedCacheHelper.GetDepartments().OrderBy(o => o.Name).FirstOrDefault(o => o.Id == item.ItemGroup?.DepartmentId);
                            if (depart != null)
                            {
                                group.DepartmentId = depart.Id;
                                group.DepartmentName = depart.Name;
                                group.DepartmentOrder = depart.OrderNum;

                            }
                            group.CheckState = (int)ProjectIState.Not;
                            group.RefundState = (int)PayerCatType.NotRefund;
                            group.GuidanceSate = (int)PrintSate.NotToPrint;
                            group.BarState = (int)PrintSate.NotToPrint;
                            group.RequestState = (int)PrintSate.NotToPrint;
                            group.RefundState = (int)PayerCatType.NotRefund;
                            group.BillingEmployeeBMId = CurrentUser.Id;
                            group.SummBackSate = (int)SummSate.NotAlwaysCheck;
                            if (!curCustomRegInfo.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == group.ItemGroupBM_Id))
                            {
                                curCustomRegInfo.CustomerItemGroup.Add(group);
                            }


                        }
                        gridControlgroups.DataSource = null;
                        gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
                        curCustomRegInfo.ItemSuitBMId = itemsuit.Id;
                        curCustomRegInfo.ItemSuitName = itemsuit.ItemSuitName;
                        if (!string.IsNullOrWhiteSpace(itemsuit.ItemSuitName))
                            labItemSuitName.Text = "已选择了套餐：【" + itemsuit.ItemSuitName + "】";
                    }
                }
                else
                {
                    cuslookItemSuit.EditValue = null;
                }
            }

        }
        /// <summary>
        /// 高级检索按钮事件 
        /// </summary>
        private void btnAddvancedSearch_Click(object sender, EventArgs e)
        {
            var cusListForm = new CusList();
            if (cusListForm.ShowDialog() == DialogResult.OK)
            {
                var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = cusListForm.curCustomerBM });
                ClearControlData();
                LoadCustomerData(data);
                // SetBtnEnabled();
            }
        }

        /// <summary>
        /// 人员信息详情按钮
        /// </summary>
        private void butDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustoemrCode.Text))
            {
                ShowMessageSucceed("请先点击新登记。");
                return;
            }
            var cusptho = pictureCus.Image;
            var cuspthoReg = pictureCusReg.Image;
            var sin = pictureEdit1.Image;
            curCustomRegInfo = GetControlSaveData(true);
            if (curCustomRegInfo == null)
                curCustomRegInfo = new QueryCustomerRegDto() { Customer = new QueryCustomerDto { ArchivesNum = txtCustoemrCode.Text }, CustomerBM = txtCustoemrCode.Text };
            using (var detailsForm = new CusDetail())
            {
                detailsForm.isCustomerReg = true;
                detailsForm.sexList = sexList;
                detailsForm.marrySateList = marrySateList;
                detailsForm.breedStateList = breedStateList;
                detailsForm.curCustomRegInfo = curCustomRegInfo;
                detailsForm.clientRegs = clientRegs;
                detailsForm.reviewStates = reviewStates;
                detailsForm.WindowState = FormWindowState.Maximized;
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {
                    //ClearControlData();
                    curCustomRegInfo = detailsForm.curCustomRegInfo;
                    LoadCustomerData(curCustomRegInfo);
                    pictureCus.Image = cusptho;
                    pictureCusReg.Image = cuspthoReg;
                    pictureEdit1.Image = sin;
                }
            }
        }
        /// <summary>
        /// 添加套餐按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAddSuit_Click(object sender, EventArgs e)
        {
            //var data = GetControlSaveData();
            //if (data == null)
            //{
            //    return;
            //}
            //curCustomRegInfo = data;

            //var customer = ModelHelper.CustomMapTo2<QueryCustomerDto, TjlCustomerDto>(curCustomRegInfo.Customer);


            //using (var selectItemSuitForm = new FrmSelectItemSuit(curSelectItemGroup, customer, curCustomRegInfo, isPersonal, isCheckState))
            //{

            //    var isPersonal = true;
            //    var isCheckState = false;
            //    if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
            //        isCheckState = true;
            //    var curSelectItemGroup = new List<TjlCustomerItemGroupDto>();
            //    curSelectItemGroup = Clone<List<TjlCustomerItemGroupDto>>(curCustomRegInfo.CustomerItemGroup?.ToList());

            //    selectItemSuitForm.SeleteItemGroup(curSelectItemGroup, customer, curCustomRegInfo, isPersonal, isCheckState);


            //    var selSuitInfo = new SimpleItemSuitDto();
            //    var selItemGroups = new List<TjlCustomerItemGroupDto>();

            //    selectItemSuitForm.SaveDataComplateForPersonal += (OutputsimpleItemSuitDto, GROutputcurSelectItemGroup) =>
            //    {
            //        selSuitInfo = OutputsimpleItemSuitDto;
            //        selItemGroups = GROutputcurSelectItemGroup;

            //    };
            //    if (selectItemSuitForm.ShowDialog() == DialogResult.OK)
            //    {
            //        if (selItemGroups.Count == 0)
            //        {
            //            ShowMessageBoxWarning("您选择的套餐内无项目。");
            //            return;
            //        }
            //        if (curCustomRegInfo.ItemSuitBMId.HasValue)
            //        {
            //            if (curCustomRegInfo.ItemSuitBMId.Value == selSuitInfo.Id)
            //            {
            //                ShowMessageBoxInformation("您选择的套餐没有变更。");
            //                return;
            //            }
            //            curCustomRegInfo.CustomerItemGroup = curCustomRegInfo.CustomerItemGroup.Where(o => o.CheckState != (int)PayerCatType.NoCharge)?.ToList();
            //        }
            //        var itemGroups = curCustomRegInfo.CustomerItemGroup?.ToList();
            //        if (itemGroups == null)
            //            itemGroups = selItemGroups;
            //        else
            //            itemGroups.AddRange(selItemGroups);
            //        curCustomRegInfo.CustomerItemGroup = itemGroups;
            //        curCustomRegInfo.ItemSuitBMId = selSuitInfo.Id;
            //        curCustomRegInfo.ItemSuitName = selSuitInfo.ItemSuitName;
            //        gridControlgroups.DataSource = null;
            //        gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
            //        if (!string.IsNullOrWhiteSpace(curCustomRegInfo.ItemSuitName))
            //            labItemSuitName.Text = "已选套餐【" + curCustomRegInfo.ItemSuitName + "】";
            //    }
            //}
        }
        /// <summary>
        /// 1+X按钮
        /// </summary>
        private void butX_Click(object sender, EventArgs e)
        {

            var data = GetControlSaveData();
            if (data == null)
                return;
            curCustomRegInfo = data;
            var customer = ModelHelper.CustomMapTo2<QueryCustomerDto, TjlCustomerDto>(curCustomRegInfo.Customer);

            using (var selectItemSuitForm = new FrmSelectItemSuit(customer, curCustomRegInfo.CustomerItemGroup?.ToList()))
            {
                var selSuitInfo = new SimpleItemSuitDto();
                var selItemGroups = new List<TjlCustomerItemGroupDto>();
                #region 组合
                var isPersonal = true;
                var isCheckState = false;
                if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
                    isCheckState = true;
                var curSelectItemGroup = new List<TjlCustomerItemGroupDto>();
                curSelectItemGroup = Clone<List<TjlCustomerItemGroupDto>>(curCustomRegInfo.CustomerItemGroup?.ToList());
                selectItemSuitForm.SeleteItemGroup(curSelectItemGroup, customer, curCustomRegInfo, isPersonal, isCheckState);
                #endregion
                selectItemSuitForm.SaveDataComplateForPersonal += (OutputsimpleItemSuitDto, GROutputcurSelectItemGroup) =>
                {
                    selSuitInfo = OutputsimpleItemSuitDto;
                    selItemGroups = GROutputcurSelectItemGroup;
                };
                if (selectItemSuitForm.ShowDialog() == DialogResult.OK)
                {
                    //var itemGroups = curCustomRegInfo.CustomerItemGroup?.ToList();
                    //itemGroups.AddRange(selItemGroups);
                    curCustomRegInfo.CustomerItemGroup = selItemGroups;
                    gridControlgroups.DataSource = null;
                    gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
                }
            }
        }
        /// <summary>
        /// 添加项目按钮
        /// </summary>
        private void butAddGroup_Click(object sender, EventArgs e)
        {
            var data = GetControlSaveData(true);
            if (data == null)
                return;
            curCustomRegInfo = data;
            var isPersonal = true;
            var isCheckState = false;
            if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
                isCheckState = true;
            var curSelectItemGroup = new List<TjlCustomerItemGroupDto>();
            curSelectItemGroup = Clone<List<TjlCustomerItemGroupDto>>(curCustomRegInfo.CustomerItemGroup?.ToList());
            var customer = ModelHelper.CustomMapTo2<QueryCustomerDto, TjlCustomerDto>(curCustomRegInfo.Customer);

            using (FrmSeleteItemGroup frmSeleteItemGroup = new FrmSeleteItemGroup(curSelectItemGroup, customer, curCustomRegInfo, isPersonal, isCheckState))
            {
                var groups = new List<TjlCustomerItemGroupDto>();
                frmSeleteItemGroup.SaveDataComplateForPersonal += (itemgroups) =>
                 {
                     groups = itemgroups;
                     isChangeItemGroup = true;
                 };
                if (frmSeleteItemGroup.ShowDialog() == DialogResult.OK)
                {
                    curCustomRegInfo.CustomerItemGroup = groups;
                    gridControlgroups.DataSource = null;
                    gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
                    //if (curCustomRegInfo.CustomerItemGroup.Count != null)
                    //{

                    //    foreach(var r in curCustomRegInfo.CustomerItemGroup)
                    //    {
                    //        //if (curCustomRegInfo.CustomerItemGroup.Contains(r.NucleicAcidState == 1))
                    //        if (r.NucleicAcidState == 1)
                    //        {
                    //            comboBoxEdit1.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
                    //                break;
                    //        }
                    //        else
                    //        {
                    //            comboBoxEdit1.Properties.DataSource = null;
                    //        }
                    //    }
                    //}

                }
            }
        }
        public static T Clone<T>(object realObject)
        {
            using (Stream stream = new MemoryStream()) // 初始化一个 流对象
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T)); //将要序列化的对象序列化到xml文档（Formatter）
                serializer.Serialize(stream, realObject); //将序列后的对象写入到流中
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);// 反序列化得到新的对象
            }

        }
        /// <summary>
        /// 复制项目按钮
        /// </summary>
        private void butCopyGroups_Click(object sender, EventArgs e)
        {
            var data = GetControlSaveData();
            if (data != null)
            {
                curCustomRegInfo = data;
                if (curCustomRegInfo.CustomerItemGroup != null)
                {
                    if (curCustomRegInfo.CustomerItemGroup.Count > 0 
                        && curCustomRegInfo.CustomerItemGroup.Any(p=>p.IsAddMinus!=3))
                    {
                        ShowMessageBoxInformation("已选择项目，不可使用复制");
                        return;
                    }
                }
            }
            else
                return;
            var copyGroupsFrom = new CopyGroups();
            copyGroupsFrom.curSex = curCustomRegInfo.Customer.Sex;
            copyGroupsFrom.clientRegs = clientRegs;
            if (copyGroupsFrom.ShowDialog() == DialogResult.OK)
            {
                if (copyGroupsFrom.curSelectSuit != null)
                {
                    if (curCustomRegInfo == null)
                        curCustomRegInfo = new QueryCustomerRegDto { Customer = new QueryCustomerDto(), CustomerItemGroup = new List<TjlCustomerItemGroupDto>() };
                    if (curCustomRegInfo.CustomerItemGroup == null)
                        curCustomRegInfo.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                    if (!curCustomRegInfo.ItemSuitBMId.HasValue)
                    {//如果没有套餐id就复制来的套餐，其他项目都删除 只留下未收费的
                        curCustomRegInfo.CustomerItemGroup = curCustomRegInfo.CustomerItemGroup.Where(o => o.PayerCat != (int)PayerCatType.NoCharge)?.ToList();
                    }
                    foreach (var sgroup in copyGroupsFrom.curSelectSuit.ItemSuitItemGroups)
                    {//已有项目中包含了则不再添加
                        if (curCustomRegInfo.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == sgroup.ItemGroupId))
                            continue;
                        var group = new TjlCustomerItemGroupDto();
                        group.CheckState = (int)ProjectIState.Not;
                        group.DepartmentId = sgroup.ItemGroup.DepartmentId;
                        var depart = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == group.DepartmentId);
                        if (depart != null)
                        {
                            group.DepartmentName = depart.Name;
                            group.DepartmentOrder = depart.OrderNum;
                        }
                        group.DiscountRate = sgroup.Suitgrouprate == null ? 0 : sgroup.Suitgrouprate.Value;
                        group.GRmoney = sgroup.PriceAfterDis == null ? 0 : sgroup.PriceAfterDis.Value;

                        group.ItemGroupBM_Id = sgroup.ItemGroupId;
                        group.ItemGroupName = sgroup.ItemGroup.ItemGroupName;
                        group.ItemGroupOrder = sgroup.ItemGroup.OrderNum;
                        group.ItemPrice = sgroup.ItemPrice == null ? 0 : sgroup.ItemPrice.Value;
                        if (!curCustomRegInfo.ItemSuitBMId.HasValue)
                        {
                            group.ItemSuitId = copyGroupsFrom.curSelectSuit.Id;
                            group.ItemSuitName = copyGroupsFrom.curSelectSuit.ItemSuitName;
                            group.IsAddMinus = (int)AddMinusType.Normal;
                        }
                        else
                            group.IsAddMinus = (int)AddMinusType.Add;
                        group.SFType = Convert.ToInt32(sgroup.ItemGroup.ChartCode);
                        group.PayerCat = (int)PayerCatType.NoCharge;
                        group.PriceAfterDis = sgroup.PriceAfterDis == null ? 0 : sgroup.PriceAfterDis.Value;
                        group.TTmoney = 0;
                        curCustomRegInfo.CustomerItemGroup.Add(group);
                    }
                    if (!curCustomRegInfo.ItemSuitBMId.HasValue)
                    {
                        curCustomRegInfo.ItemSuitBMId = copyGroupsFrom.curSelectSuit.Id;
                        curCustomRegInfo.ItemSuitName = copyGroupsFrom.curSelectSuit.ItemSuitName;
                    }
                }
                if (copyGroupsFrom.curSelctGroup != null)
                {
                    if (copyGroupsFrom.curSelctGroup.Count > 0)
                    {
                        foreach (var g in copyGroupsFrom.curSelctGroup)
                        {//已有项目中包含了则不再添加
                            if (curCustomRegInfo.CustomerItemGroup == null)
                                curCustomRegInfo.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                            if (curCustomRegInfo.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == g.ItemGroupBM_Id))
                                continue;
                            g.GRmoney = g.PriceAfterDis;
                            g.PayerCat = (int)PayerCatType.NoCharge;
                            g.CheckState = (int)ProjectIState.Not;
                            curCustomRegInfo.CustomerItemGroup.Add(g);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(curCustomRegInfo.ItemSuitName))
                    labItemSuitName.Text = "已选套餐【" + curCustomRegInfo.ItemSuitName + "】";
                gridControlgroups.DataSource = null;
                gridControlgroups.DataSource = curCustomRegInfo.CustomerItemGroup;
            }
        }
        /// <summary>
        /// 收费结算按钮
        /// </summary>

        private void butCharge_Click(object sender, EventArgs e)
        {
            if (curCustomRegInfo.RegisterState != (int)RegisterState.Yes)
            {
                DialogResult dr = XtraMessageBox.Show("该人员未登记是否登记？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                    butOK.PerformClick();
                else
                    return;
            }
            if (curCustomRegInfo.CustomerItemGroup.Any(o => o.Id == Guid.Empty) || isChangeItemGroup)
            {
                DialogResult dr = XtraMessageBox.Show("有未保存的项目是否保存？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    butOK_Click(sender, e);
                }
            }
            if (!string.IsNullOrWhiteSpace(txtCustoemrCode.Text))
            {
                using (var personChargeForm = new PersonCharge(txtCustoemrCode.Text.Trim()))
                {
                    if (personChargeForm.ShowDialog() == DialogResult.Cancel)
                    {

                        var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.Text });
                        LoadCustomerData(data);
                    }
                }
            }
            else
            {
                ShowMessageBoxInformation("请选择体检人，再进行收费！");
            }
        }
        /// <summary>
        /// 编辑控件中的按钮点击事件
        /// </summary>
        private void edior_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
                if (editor.Name == txtClientRegID.Name)
                    txtTeamID.EditValue = null;

            }
            if (e.Button.Kind == ButtonPredefines.OK)
            {
                //加载默认查询条件
                string fp = System.Windows.Forms.Application.StartupPath + "\\CustomerReg.json";

                List<Search> searchlist = new List<Search>();


                if (!string.IsNullOrWhiteSpace(lookUpEditClientType.EditValue?.ToString()))
                {
                    Search search = new Search();
                    search.Name = "CheckType";
                    search.Text = lookUpEditClientType.EditValue?.ToString();
                    searchlist.Add(search);
                }

                if (searchlist.Count > 0)
                {

                    if (!File.Exists(fp))  // 判断是否已有相同文件 
                    {
                        FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                        fs1.Close();
                    }

                    File.WriteAllText(fp, JsonConvert.SerializeObject(searchlist));
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("保存成功！");
                }

            }
        }
        /// <summary>
        /// 数据源改变，项目统计数改变
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (curCustomRegInfo != null)
            {
                if (curCustomRegInfo.CustomerItemGroup != null)
                {
                    var sum = curCustomRegInfo.CustomerItemGroup.Count();
                    var add = curCustomRegInfo.CustomerItemGroup.Count(o => o.IsAddMinus == (int)AddMinusType.Add);
                    var minus = curCustomRegInfo.CustomerItemGroup.Count(o => o.IsAddMinus == (int)AddMinusType.Minus);
                    var normal = curCustomRegInfo.CustomerItemGroup.Count(o => o.IsAddMinus == (int)AddMinusType.Normal);
                    labGroups.Text = string.Format("已选项目{0}项，正常{1}，加项{2}，减项{3}", sum, normal, add, minus);
                }
            }
        }
        /// <summary>
        /// 加减项样式设置
        /// </summary>
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            if (e.Column.Name == conItemName.Name)
            {
                var isadd = gridView1.GetRowCellValue(e.RowHandle, colIsAddMinus);
                if (isadd == null)
                    return;
                if (Convert.ToInt32(isadd) == (int)AddMinusType.Add)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (Convert.ToInt32(isadd) == (int)AddMinusType.Minus)
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.FontStyleDelta = FontStyle.Strikeout;
                }
                var checkState = gridView1.GetRowCellValue(e.RowHandle, conCheckState);
                if (checkState == null)
                    return;
                if (Convert.ToInt32(checkState) == (int)ProjectIState.GiveUp)
                    e.Appearance.ForeColor = Color.Gray;
                if (Convert.ToInt32(checkState) == (int)ProjectIState.Stay)
                    e.Appearance.ForeColor = Color.Blue;

            }

        }
        /// <summary>
        /// 交表确认
        /// </summary>
        private void butHandform_Click(object sender, EventArgs e)
        {
            using (var crossTableForm = new CrossTable(curCustomRegInfo.CustomerBM))
            {
                var result = crossTableForm.ShowDialog();
                if (result == DialogResult.Cancel)
                {//窗体关闭重新加载数据，如果已交表则登记按钮不可点击
                    var tijianhao = txtCustoemrCode.EditValue;
                    var retdata = QueryCustomerRegData(new SearchCustomerDto() { CustomerBM = curCustomRegInfo.CustomerBM });
                    if (retdata != null)
                    {
                        if (retdata.SendToConfirm == (int)SendToConfirm.Yes)
                        {
                            butOK.Enabled = false;
                        }
                        ClearControlData();
                        curCustomRegInfo = retdata;

                        LoadCustomerData(curCustomRegInfo);
                    }
                    else
                    {
                        ClearControlData();
                        txtCustoemrCode.EditValue = tijianhao;
                    }
                }
            }
        }
        #endregion

        #region 处理
        /// <summary>
        /// 查询启用的各种套餐，并根据男女筛选
        /// </summary>
        private List<SimpleItemSuitDto> QuerySuits(SearchItemSuitDto dto, bool isType = false)
        {
            if (!string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()))
            {
                if (Convert.ToInt32(gridLookUpSex.EditValue) == (int)Sex.Man)
                {
                    dto.Sex = (int)Sex.Man;
                }
                else if (Convert.ToInt32(gridLookUpSex.EditValue) == (int)Sex.Woman)
                {
                    dto.Sex = (int)Sex.Woman;
                }
            }
            var result = DefinedCacheHelper.GetItemSuit().Where(o => (o.ItemSuitType == dto.ItemSuitType || o.ItemSuitType == (int)ItemSuitType.OnLine) && o.Available == 1)?.ToList();
            if (result != null && dto.Sex.HasValue)
            {
                result = result.Where(o => o.Sex == dto.Sex || o.Sex == (int)Sex.GenderNotSpecified)?.ToList();
            }
            if (result != null && dto.ExaminationType.HasValue)
            {
                if (isType == true)
                {
                    result = result.Where(o => o.ExaminationType == dto.ExaminationType || o.ExaminationType == null)?.ToList();
                }
                else
                {
                    result = result.Where(o => o.ExaminationType == dto.ExaminationType)?.ToList();
                }
            }
            //result = result.Where(p=>p.IsendDate !=1 || (p.IsendDate ==1 && p.endDate>=System. _commonAppService.GetDateTimeNow().Now)).ToList();
            return result;
        }
        /// <summary>
        /// 查询客户预约登记数据
        /// </summary>
        private QueryCustomerRegDto QueryCustomerRegData(SearchCustomerDto value)
        {
            try
            {
                var rows = customerSvr.QueryCustomerReg(value);
                // var rows = customerSvr.GetCustomerIDCard(value);
               
                if (rows != null)
                {
                    if (rows.Count > 1)
                    {
                        using (var frm = new SelectCusReg())
                        {
                            frm.Datas = rows;
                            var data = new QueryCustomerRegDto();
                            frm.SelectData += (selectData) =>
                            {
                                data = selectData;
                            };
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                curCustomRegInfo = data;
                            }
                            else
                                curCustomRegInfo = null;
                        }
                    }
                    else
                    {
                        curCustomRegInfo = rows.FirstOrDefault();
                        if (rows.Count == 0)
                        {
                            MessageBox.Show("姓名：" + txtName.EditValue.ToString() + "无预约记录！");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("没找到该体检人！");
                }

                //if (rows != null)
                //{              
                //   foreach(var r in rows)
                //   {
                //    if (r.NucleicAcidType!=null)
                //    {
                //            comboBoxEdit1.EditValue = r.NucleicAcidType;
                //    }
                //    else
                //    {
                //        comboBoxEdit1.Properties.DataSource = null;
                //    }
                //   }
                //}
                //curCustomRegInfo = row;
                QueryRegNumbers();
                SetBtnEnabled();
                return curCustomRegInfo;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return null;
            }
        }

        /// <summary>
        /// 身份证获取基本信息
        /// </summary>
        private QueryCustomerRegDto QueryCusDataIdNum(SearchCustomerDto value, string cusName = "",
            int sex = 0, int age = 0, Image image = null, string dress = "")
        {
            QueryCustomerRegDto regDto = new QueryCustomerRegDto();
            try
            {
                var rows = customerSvr.QueryCustomerReg(value);

                if (rows != null)
                {
                    //查看是否有未完成的体检信息
                    var cusNosum = rows.Where(o => o.SummSate == (int)SummSate.NotAlwaysCheck).ToList();
                    //过滤本次预约
                    if (curCustomRegInfo != null && curCustomRegInfo.Id != Guid.NewGuid())
                    {
                        cusNosum = cusNosum.Where(o => o.Id != curCustomRegInfo.Id).ToList();
                    }
                    if (cusNosum.Count > 0)
                    {//弹出提示

                        using (var frm = new frmCardIDShow())
                        {
                            if (!string.IsNullOrEmpty(txtIDCardNo.Text.Trim()))
                            {
                                frm.OldIDCardNo = txtIDCardNo.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                            {
                                frm.OldName = txtName.Text.Trim();
                            }
                            frm.Datas = cusNosum;
                            if (!string.IsNullOrEmpty(cusName))
                            {
                                frm.cusName = cusName;
                                frm.age = age;
                                frm.sex = sex;
                                frm.dress = dress;
                                frm.Image = image;
                                frm.IdCard = value.IDCardNo;
                            }
                            var data = new QueryCustomerRegDto();
                            frm.SelectData += (selectData) =>
                            {
                                data = selectData;
                            };
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                regDto = data;
                                if (frm.butType == 2)
                                {
                                    object sender = null;
                                    EventArgs e = null;
                                    butAddReg_Click(sender, e);
                                    txtIDCardNo.EditValue = data.Customer.IDCardNo;
                                }
                            }
                            else
                            {
                                regDto = null;
                            }
                        }
                    }
                }
                //curCustomRegInfo = row;
                QueryRegNumbers();
                SetBtnEnabled();
                return regDto;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return null;
            }
        }
        /// <summary>
        /// 新增人员根据身份证关联档案
        /// </summary>
        private void addCusByIdCard(string IDcard, bool isOld)
        {
            SearchCustomerDto searchCustomer = new SearchCustomerDto();
            searchCustomer.IDCardNo = IDcard;
            var rows = customerSvr.GetCustomerIDCard(searchCustomer);

            if (rows != null && rows.Count > 0)
            {
                if (curCustomRegInfo == null)
                {
                    curCustomRegInfo = new QueryCustomerRegDto()
                    { Customer = new QueryCustomerDto(), CustomerItemGroup = new List<TjlCustomerItemGroupDto>() };
                }
                //if (txtName.Text.Trim() != "" && rows[0].Customer.Name != txtName.Text.TrimEnd())
                //{
                //    string ts = "您确定将当前体检人【" + txtName.Text.Trim() + "】的基本信息，关联档案：【" + rows[0].Customer.Name + "】么？";
                //    if (DevExpress.XtraEditors.XtraMessageBox.Show(ts, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //        return;
                //}
                curCustomRegInfo.Customer = rows[0].Customer;
                txtArchivesNum.EditValue = rows[0].Customer.ArchivesNum;
                txtName.EditValue = rows[0].Customer.Name;
                gridLookUpSex.EditValue = rows[0].Customer.Sex;
                if (isOld == true)
                {
                    if (!string.IsNullOrEmpty(rows[0].Customer.Mobile))
                    {
                        txtMobile.EditValue = rows[0].Customer.Mobile;
                    }
                    if (rows[0].Customer.MarriageStatus.HasValue)
                    {
                        gridLookUpMarriageStatus.EditValue = rows[0].Customer.MarriageStatus;
                    }
                    curCustomRegInfo.Customer.Address = rows[0].Customer.Address;
                }
                DateBirthday.EditValue = rows[0].Customer.Birthday;

                #region 获取上次体检时间


                txtLasttime.Text = rows[0].LoginDate?.ToString();

                #endregion
                if (DateBirthday.EditValue != null)
                {
                    var age =  _commonAppService.GetDateTimeNow().Now.Year - DateBirthday.DateTime.Year;
                    if (txtAge.EditValue?.ToString() != age.ToString())
                        txtAge.EditValue = age;
                }
            }
            else
            {
                if (curCustomRegInfo == null)
                {
                    curCustomRegInfo = new QueryCustomerRegDto()
                    { Customer = new QueryCustomerDto(), CustomerItemGroup = new List<TjlCustomerItemGroupDto>() };
                }
            }
        }
        /// <summary>
        /// 加载客户数据
        /// </summary>
        private void LoadCustomerData(QueryCustomerRegDto data, Image photo = null)
        {
            //butAddReg.Enabled = true;
            ClearControlData();
            IsReging = true;
            if (data != null)
            {
                if (data.ClientRegId.HasValue)
                {
                    if (!clientRegs.Any(o => o.Id == data.ClientRegId))
                    {
                        clientRegs = customerSvr.QuereyClientRegInfos(new FullClientRegDto() { FZState = (int)FZState.not });//加载单位分组数据
                        if (!clientRegs.Any(o => o.Id == data.ClientRegId))
                        {
                            ShowMessageBoxWarning("该人员单位已经封账，不能再登记处查看.");
                            return;
                        }
                        else
                        {
                            txtClientRegID.Properties.DataSource = clientRegs;

                        }
                    }
                }
                curCustomRegInfo = data;

                //上面
                //txtkdEmp.EditValue=data.
                //if (data.InfoSource.HasValue)
                //{
                //    txtFrom.Text = data.InfoSource.ToString();
                //    var clienttype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.ClientSource).ToString())?.ToList().Find(o => o.Value == data.InfoSource.Value);
                //    if (clienttype != null)
                //    {
                //        txtFrom.Text = clienttype.Text;
                //    }
                //}
                //else
                //{
                //    txtFrom.Text = "";
                //    txtFrom.Text = null;
                //}
                #region 获取上次体检时间
                //SearchOldRegDto searchOldRegDto = new SearchOldRegDto();
                //if (data.Customer != null && !string.IsNullOrEmpty(data.Customer?.ArchivesNum))
                //{
                //    searchOldRegDto.ArchivesNum = data.Customer.ArchivesNum;
                //}
                //if (string.IsNullOrEmpty(txtIDCardNo?.ToString()))
                //{
                //    searchOldRegDto.IDCardNo = txtIDCardNo?.ToString();
                //}
                //if (!string.IsNullOrEmpty(searchOldRegDto.ArchivesNum) || !string.IsNullOrEmpty(searchOldRegDto.IDCardNo))
                //{
                //    searchOldRegDto.CustomerBM = data.CustomerBM;
                //    var cusinfo = customerSvr.QueryOldCustomer(searchOldRegDto);
                //    if (cusinfo != null)
                //    {
                //        txtLasttime.Text = cusinfo.LoginDate?.ToString();
                //    }
                //}
                #endregion
                //
                //txtLasttime.EditValue
                string strCheckSate = "";
                string strSFState = "";
                string strjbstate = "";
                string strCheckStates = "";
                //上一次复查体检号
                string ReviewRegINfo = "";
                if (data.ReviewRegID.HasValue)
                {
                    try
                    {
                        EntityDto<Guid> entity = new EntityDto<Guid>();
                        entity.Id = data.ReviewRegID.Value;
                        var rereg = customerSvr.getCusreg(entity);
                        ReviewRegINfo = " 原体检信息：" + rereg.CusRegBM + "("+ rereg.Custime?.ToShortDateString() + ")";
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    
                }
                if (data.RegisterState.HasValue)
                {
                    // txtCheckSate.Text = EnumHelper.GetEnumDesc((RegisterState)data.RegisterState);

                    strCheckSate = EnumHelper.GetEnumDesc((RegisterState)data.RegisterState);
                    if (strCheckSate == "已登记")
                    {
                        strCheckSate = "<Color=Blue>" + strCheckSate + "</Color>";
                    }
                }
                else
                {
                    strCheckSate = EnumHelper.GetEnumDesc(RegisterState.No);
                }
                if (data.CostState.HasValue)
                {
                    strSFState = EnumHelper.GetEnumDesc((PayerCatType)data.CostState);
                    if (strSFState == "已收费")
                    {
                        strSFState = "<Color=Blue>" + strSFState + "</Color>";
                    }
                }
                if (data.SendToConfirm.HasValue)
                {

                    strjbstate = EnumHelper.GetEnumDesc((SendToConfirm)data.SendToConfirm);

                    if (strjbstate == "已交表")
                    {
                        strjbstate = "<Color=Blue>" + strjbstate + "</Color>";
                    }
                }
                if (data.CheckSate.HasValue)
                {

                    strCheckStates = EnumHelper.GetEnumDesc((PhysicalEState)data.CheckSate);
                    if (strCheckStates != "未体检")
                    {
                        strCheckStates = "<Color=Blue>" + strCheckStates + "</Color>";
                    }
                }
                labelCusInfo.Text = strCheckSate + " " + strSFState + " " + strjbstate + " " + 
                    strCheckStates + " " + "人员编号：" + curCustomRegInfo.ClientRegNum + " 登记日期：" + curCustomRegInfo.LoginDate + ReviewRegINfo;
                //登记列表.Text = strCheckSate + " " + strSFState + " " + strjbstate + " " + strCheckStates + " " + "人员编号：" + curCustomRegInfo.ClientRegNum;
                txtkdEmp.Text = data.KaidanYisheng;

                #region 超声排队
                var strCSPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 111)?.Remarks;

                if (strCSPD != null && strCSPD == "Y")
                {
                    try
                    {
                        string strpdsql = "  select * from tj_requireinfo where DepartmentID='1001' and SelfBH='"
                           + curCustomRegInfo.CustomerBM + "'";
                        SqlConnection tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                        tjcon.Open();//连接体检数据库
                        SqlCommand tjcom = new SqlCommand(strpdsql, tjcon);
                        SqlDataReader tjread = tjcom.ExecuteReader();
                        if (tjread.HasRows)
                        {

                            Btpd.BLL.UserInfo userinfo = new Btpd.BLL.UserInfo(Btpd_base.PublicTool.SqlConnection);
                            List<Btpd.Model.UserInfo> lsuserinfo = userinfo.GetModelList(" tjcode='" + curCustomRegInfo.CustomerBM + "'");
                            Btpd.Model.UserInfo EnteruserinfoGd = new Btpd.Model.UserInfo();
                            if (lsuserinfo.Count > 0)
                            {
                                EnteruserinfoGd = lsuserinfo[0];
                                var ts = "登记成功,彩超排队号为:" + EnteruserinfoGd.TeamOrder;

                                labPD.Text = ts;
                            }
                            else
                            { labPD.Text = ""; }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                #endregion
                // txtWX.Text = data.Customer.Qq;
                #region 左侧
                if (photo != null)
                    pictureCus.Image = photo;
                if (data.Id == Guid.Empty && string.IsNullOrWhiteSpace(data.CustomerBM))
                {

                    var customBM = iIDNumberAppService.CreateArchivesNumBM();
                    data.CustomerBM = customBM;
                }
                txtCustoemrCode.EditValue = data.CustomerBM;
                if (data.Customer != null)
                {
                    if (string.IsNullOrWhiteSpace(data.Customer.ArchivesNum))
                        data.Customer.ArchivesNum = data.CustomerBM;
                    txtArchivesNum.EditValue = data.Customer.ArchivesNum;
                    txtName.EditValue = data.Customer.Name;
                    gridLookUpSex.EditValue = data.Customer.Sex;
                    // MessageBox.Show(data.Customer.Sex.ToString());
                    //txtIDCardNo.EditValue = data.IDCardNo;
                    txtAge.EditValue = data.Customer.Age;//data.AgeUnit
                    gridLookUpMarriageStatus.EditValue = data.Customer.MarriageStatus;
                    if (data.Customer.IDCardType.HasValue)
                        txtIDCardType.EditValue = data.Customer.IDCardType;
                    else
                    {
                        var shenfenzheng = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "身份证"); //Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType).FirstOrDefault(o => o.Text == "身份证");
                        if (shenfenzheng != null)
                        {
                            txtIDCardType.EditValue = shenfenzheng.Value;
                        }
                    }
                    txtIDCardNo.Text = data.Customer.IDCardNo;

                    txtMobile.EditValue = data.Customer.Mobile;
                    DateBirthday.EditValue = data.Customer.Birthday;
                    txtAdress.EditValue = data.Customer.Address;
                    txtDepartment.EditValue = data.Customer.Department;
                    txtRemarks1.EditValue = data.Customer.Remarks;
                    txtNation.EditValue = data.Customer.Nation;
                    txtVisitCard.EditValue = data.Customer.VisitCard;
                    //锁定如果是添加没有预约信息不清除职业健康信息
                    if (data.Id != Guid.Empty && checkEditSD.Checked != true)
                    {
                        //职业健康
                        txtRiskS.EditValue = data.RiskS;
                        if (data.OccHazardFactors != null && data.OccHazardFactors.Count > 0)
                        {
                            txtRiskS.Tag = data.OccHazardFactors;
                        }
                        txtWorkName.EditValue = data.WorkName;
                        txtTypeWork.EditValue = data.TypeWork;
                        txtTotalWorkAge.EditValue = data.TotalWorkAge;
                        txtInjuryAge.EditValue = data.InjuryAge;
                        txtCheckType.EditValue = data.PostState;
                        textEditClientname.EditValue = data.employerEnterpriseName;
                        textEditClientCode.EditValue = data.employerCreditCode;

                        textEditOther.EditValue = data.OtherTypeWork;
                    }
                    //else if (data.Id == Guid.Empty && checkEditSD.Checked == true)
                    //{
                    //    //暂时屏蔽
                    //      //data.CustomerItemGroup=  gridControlgroups.DataSource;
                    //}
                    comunit2.EditValue = data.WorkAgeUnit;
                    comunit.EditValue = data.InjuryAgeUnit;
                    textEditRegRemark.EditValue = data.Remarks;
                    if (data.PersonnelCategoryId.HasValue)
                    {
                        txtPersonnelCategory.EditValue = data.PersonnelCategoryId;
                    }
                }
                if (data.Id != Guid.Empty)
                {
                    // txtPersonnelCategory.EditValue = data.CustomerType;
                    if (curCustomRegInfo.PersonnelCategoryId.HasValue)
                    {
                        txtPersonnelCategory.EditValue = data.PersonnelCategoryId;
                    }
                    if (curCustomRegInfo.CustomerType.HasValue)
                        lookUpEditCustomerType.EditValue = data.CustomerType;

                    lookUpEditClientType.EditValue = data.PhysicalType;
                    if (data.RegisterState != (int)RegisterState.Yes)
                    {
                        if (data.BookingDate.HasValue)
                        { DateChekDate.EditValue = data.BookingDate; }
                        else
                        {
                            data.BookingDate =  _commonAppService.GetDateTimeNow().Now;
                        }

                    }
                    DateChekDate.EditValue = data.BookingDate;
                }
                else
                    DateChekDate.EditValue =  _commonAppService.GetDateTimeNow().Now;

                if (data.PhysicalType.HasValue)
                {
                    lookUpEditClientType.EditValue = data.PhysicalType;
                }
                if (data.NucleicAcidType.HasValue)
                {
                    comboBoxEdit1.EditValue = data.NucleicAcidType;
                }
                gridLookUpConceive.EditValue = data.ReadyPregnancybirth;
                txtClientRegID.EditValue = data.ClientRegId;
                if (data.ClientRegId.HasValue)
                {
                    #region 分组
                    //var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = data.ClientRegId.Value });
                    //if (!string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()) && gridLookUpSex.EditValue?.ToString() != ((int)Sex.GenderNotSpecified).ToString())
                    //{
                    //    list = list.Where(o => o.Sex == Convert.ToInt32(gridLookUpSex.EditValue) || o.Sex == (int)Sex.GenderNotSpecified)?.ToList();
                    //}

                    //txtTeamID.EditValue = null;
                    //txtTeamID.Properties.DataSource = list;

                    #endregion
                }
                if (data.ClientTeamInfo != null)
                {
                    txtTeamID.EditValue = data.ClientTeamInfo.Id;
                }
                if (data.ClientTeamInfo_Id != null)
                    txtTeamID.EditValue = data.ClientTeamInfo_Id;
                //txtTeamID.EditValue = data.ClientTeamInfo_Id;
                if (data.ReplaceSate.HasValue)
                {
                    if (data.ReplaceSate.Value == 2)
                    {//是替检
                        checkReplace.EditValue = true;
                    }
                    else
                    {
                        checkReplace.EditValue = false;
                    }
                }
                if (data.CustomerType.HasValue)
                    lookUpEditCustomerType.EditValue = data.CustomerType;
                checkReview.EditValue = data.ReviewSate;
                gridControlgroups.DataSource = data.CustomerItemGroup;
                //if (data != null)
                //{

                //        if (data.NucleicAcidType != null)
                //        {
                //            comboBoxEdit1.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
                //        }

                //}
                if (!string.IsNullOrWhiteSpace(data.ItemSuitName))
                    labItemSuitName.Text = "已选套餐：【" + data.ItemSuitName + "】";
                if (data.Customer != null && photo == null)
                {
                    if (data.Customer.CusPhotoBmId.HasValue && data.Customer.CusPhotoBmId != Guid.Empty)
                    {
                        var url = _pictureController.GetUrl(data.Customer.CusPhotoBmId.Value);
                        pictureCus.LoadAsync(url.Thumbnail);
                        //using (var stream = ImageHelper.GetUriImage(new Uri(url.Thumbnail)))
                        //{
                        //    //pictureCus.Image = Image.FromStream(stream);
                        //    pictureCus.LoadAsync(url.Thumbnail);
                        //}

                    }
                }
                //照片 照片可以不重新加载
                if (data != null && data.PhotoBmId.HasValue)
                {
                    var url = _pictureController.GetUrl(data.PhotoBmId.Value);
                    pictureCusReg.LoadAsync(url.Thumbnail);
                }
                if (data != null && data.SignBmId.HasValue)
                {
                    var url = _pictureController.GetUrl(data.SignBmId.Value);
                    pictureqm.LoadAsync(url.Thumbnail);
                }
                txtIntroducer.EditValue = data.Introducer;
                lookUpEditInfoSource.EditValue = data.InfoSource;
                txtQq.EditValue = data.Customer.Qq;
                txtJKZ.EditValue = data.JKZBM;
                txtCardNumber.EditValue = data.Customer.CardNumber;

                txtPrimaryName.EditValue = data.PrimaryName ?? "";

                txtFp.EditValue = data.FPNo;
                if (data.OrderUserId.HasValue)
                {
                    cglueJianChaYiSheng.EditValue = (long)data.OrderUserId;
                }
                //if (data.Customer != null)
                //{照片相关先注释
                //    if (!string.IsNullOrWhiteSpace(data.Customer.CusPhotoBM))
                //    {
                //        pictureCus.Image = strToString(data.Customer.CusPhotoBM);
                //    }
                //}


                #endregion
            }

        }

        /// <summary>
        /// 登记完更新的数据加载客户数据
        /// </summary>
        private void LoadCustomerDataforReg(QueryCustomerRegDto data, Image photo = null)
        {             
            IsReging = true;
            if (data != null)
            {           
                curCustomRegInfo = data;

              
          
                string strCheckSate = "";
                string strSFState = "";
                string strjbstate = "";
                string strCheckStates = "";
                if (data.RegisterState.HasValue)
                {
                    strCheckSate = EnumHelper.GetEnumDesc((RegisterState)data.RegisterState);
                    if (strCheckSate == "已登记")
                    {
                        strCheckSate = "<Color=Blue>" + strCheckSate + "</Color>";
                    }
                }
                else
                {
                    strCheckSate = EnumHelper.GetEnumDesc(RegisterState.No);
                }
                if (data.CostState.HasValue)
                {
                    strSFState = EnumHelper.GetEnumDesc((PayerCatType)data.CostState);
                    if (strSFState == "已收费")
                    {
                        strSFState = "<Color=Blue>" + strSFState + "</Color>";
                    }
                }
                if (data.SendToConfirm.HasValue)
                {

                    strjbstate = EnumHelper.GetEnumDesc((SendToConfirm)data.SendToConfirm);

                    if (strjbstate == "已交表")
                    {
                        strjbstate = "<Color=Blue>" + strjbstate + "</Color>";
                    }
                }
                if (data.CheckSate.HasValue)
                {

                    strCheckStates = EnumHelper.GetEnumDesc((PhysicalEState)data.CheckSate);
                    if (strCheckStates != "未体检")
                    {
                        strCheckStates = "<Color=Blue>" + strCheckStates + "</Color>";
                    }
                }
                labelCusInfo.Text = strCheckSate + " " + strSFState + " " + strjbstate + " " +
                    strCheckStates + " " + "人员编号：" + curCustomRegInfo.ClientRegNum + " 登记日期：" + curCustomRegInfo.LoginDate;

                #region 左侧
                if (photo != null)
                    pictureCus.Image = photo;
                if (data.Id == Guid.Empty && string.IsNullOrWhiteSpace(data.CustomerBM))
                {

                    var customBM = iIDNumberAppService.CreateArchivesNumBM();
                    data.CustomerBM = customBM;
                }
                txtCustoemrCode.EditValue = data.CustomerBM;
                if (data.Customer != null)
                {
                    if (string.IsNullOrWhiteSpace(data.Customer.ArchivesNum))
                        data.Customer.ArchivesNum = data.CustomerBM;
                    txtArchivesNum.EditValue = data.Customer.ArchivesNum;
                    txtName.EditValue = data.Customer.Name;
                    gridLookUpSex.EditValue = data.Customer.Sex;               
                    txtAge.EditValue = data.Customer.Age;
                    gridLookUpMarriageStatus.EditValue = data.Customer.MarriageStatus;
                    if (data.Customer.IDCardType.HasValue)
                        txtIDCardType.EditValue = data.Customer.IDCardType;
                    else
                    {
                        var shenfenzheng = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "身份证"); //Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType).FirstOrDefault(o => o.Text == "身份证");
                        if (shenfenzheng != null)
                        {
                            txtIDCardType.EditValue = shenfenzheng.Value;
                        }
                    }
                    txtIDCardNo.Text = data.Customer.IDCardNo;
                    txtMobile.EditValue = data.Customer.Mobile;
                    DateBirthday.EditValue = data.Customer.Birthday;
                    txtAdress.EditValue = data.Customer.Address;
                    txtDepartment.EditValue = data.Customer.Department;
                    txtRemarks1.EditValue = data.Customer.Remarks;
                    txtNation.EditValue = data.Customer.Nation;
                    txtVisitCard.EditValue = data.Customer.VisitCard;
                    //职业健康
                    txtRiskS.EditValue = data.RiskS;
                    if (data.OccHazardFactors != null && data.OccHazardFactors.Count > 0)
                    {
                        txtRiskS.Tag = data.OccHazardFactors;
                    }
                    txtWorkName.EditValue = data.WorkName;
                    txtTypeWork.EditValue = data.TypeWork;
                    txtTotalWorkAge.EditValue = data.TotalWorkAge;
                    txtInjuryAge.EditValue = data.InjuryAge;


                    txtCheckType.EditValue = data.PostState;

                    textEditClientname.EditValue = data.employerEnterpriseName;
                    textEditClientCode.EditValue = data.employerCreditCode;
                    textEditOther.EditValue = data.OtherTypeWork;
                    comunit2.EditValue = data.WorkAgeUnit;
                    comunit.EditValue = data.InjuryAgeUnit;
                    textEditRegRemark.EditValue = data.Remarks;
                    if (data.PersonnelCategoryId.HasValue)
                    {
                        txtPersonnelCategory.EditValue = data.PersonnelCategoryId;
                    }
                }
                if (data.Id != Guid.Empty)
                {
                    
                    if (curCustomRegInfo.PersonnelCategoryId.HasValue)
                    {
                        txtPersonnelCategory.EditValue = data.PersonnelCategoryId;
                    }
                    if (curCustomRegInfo.CustomerType.HasValue)
                        lookUpEditCustomerType.EditValue = data.CustomerType;

                    lookUpEditClientType.EditValue = data.PhysicalType;
                    if (data.RegisterState != (int)RegisterState.Yes)
                    {
                        if (data.BookingDate.HasValue)
                        { DateChekDate.EditValue = data.BookingDate; }
                        else
                        {
                            data.BookingDate = _commonAppService.GetDateTimeNow().Now;
                        }

                    }
                    DateChekDate.EditValue = data.BookingDate;
                }
                else
                    DateChekDate.EditValue = _commonAppService.GetDateTimeNow().Now;

                if (data.PhysicalType.HasValue)
                {
                    lookUpEditClientType.EditValue = data.PhysicalType;
                }
                if (data.NucleicAcidType.HasValue)
                {
                    comboBoxEdit1.EditValue = data.NucleicAcidType;
                }
                gridLookUpConceive.EditValue = data.ReadyPregnancybirth;
                
                if (data.ReplaceSate.HasValue)
                {
                    if (data.ReplaceSate.Value == 2)
                    {//是替检
                        checkReplace.EditValue = true;
                    }
                    else
                    {
                        checkReplace.EditValue = false;
                    }
                }
                if (data.CustomerType.HasValue)
                    lookUpEditCustomerType.EditValue = data.CustomerType;
                checkReview.EditValue = data.ReviewSate;
                gridControlgroups.DataSource = data.CustomerItemGroup;
               
              
                if (!string.IsNullOrWhiteSpace(data.ItemSuitName))
                    labItemSuitName.Text = "已选套餐：【" + data.ItemSuitName + "】";
              
                txtIntroducer.EditValue = data.Introducer;
                lookUpEditInfoSource.EditValue = data.InfoSource;
                txtQq.EditValue = data.Customer.Qq;
                txtJKZ.EditValue = data.JKZBM;
                txtCardNumber.EditValue = data.Customer.CardNumber;
                txtPrimaryName.EditValue = data.PrimaryName ?? "";
                txtFp.EditValue = data.FPNo;
                if (data.OrderUserId.HasValue)
                {
                    cglueJianChaYiSheng.EditValue = (long)data.OrderUserId;
                }
               


                #endregion
            }

        }
        /// <summary>
        /// 加载控件绑定的数据
        /// </summary>
        private void LoadControlBindData()
        {
            sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
            gridLookUpSex.Properties.DataSource = sexList;//性别
            marrySateList = MarrySateHelper.GetMarrySateModelsForItemInfo();
            gridLookUpMarriageStatus.Properties.DataSource = marrySateList;//婚否
            breedStateList = BreedStateHelper.GetBreedStateModels();
            gridLookUpConceive.Properties.DataSource = breedStateList;//孕育
            IBasicDictionaryAppService svr = new BasicDictionaryAppService();
            txtIDCardType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString())?.ToList();//Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType);
            var shenfenzheng = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString()).FirstOrDefault(o => o.Text.Contains("身份证"));// Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType).FirstOrDefault(o => o.Text == "身份证");
            if (shenfenzheng != null)
            {
                txtIDCardType.EditValue = shenfenzheng.Value;
            }
            //替检
            var PrimaryNamelist = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.PrimaryName.ToString())?.ToList();
            if (PrimaryNamelist != null && PrimaryNamelist.Count > 0)
                foreach (var PrimaryName in PrimaryNamelist)
                {
                    if (PrimaryName != null && !string.IsNullOrEmpty(PrimaryName.Text?.ToString()))
                    {
                        txtPrimaryName.Properties.Items.Add(PrimaryName.Text);
                    }
                }
            //体检类别
            var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            if (Variables.ISZYB == "2")
            {
                checktype = checktype.Where(o => o.Text.Contains("职业")).ToList();
            }
            lookUpEditClientType.Properties.DataSource = checktype;
            lookUpEditClientType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList().OrderBy(p=>p.OrderNum).ThenBy(p=>p.Value).FirstOrDefault().Value;

            txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
            lookUpEditCustomerType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();

            //复查状态
            reviewStates = ReviewSateTypeHelper.GetReviewStates();
            checkReview.Properties.DataSource = reviewStates;
            lookUpEditInfoSource.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ClientSource.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);
            //挂号科室
            var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
            comboBoxEdit1.Properties.DataSource = ghks;
            if (ghks != null && ghks.Count > 0)
            {
                comboBoxEdit1.EditValue = ghks[0].Value;
            }
            //单位、分组
            txtClientRegID.Properties.DataSource = clientRegs;
            //txtClientRegID.Properties.DataSource = clientTeams.GroupBy(o=>o.ClientRegId).Select(o=>o.First()).ToList();
            var result = DefinedCacheHelper.GetItemSuit().Where(o => o.Available == 1).ToList();
            //result = result.Where(p => p.IsendDate != 1 || (p.IsendDate == 1 && p.endDate >= System. _commonAppService.GetDateTimeNow().Now)).ToList();
            cuslookItemSuit.Properties.DataSource = result;

            //开单医生
            var kdys = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ForegroundFunctionControl.ToString() && o.Value == 150);
            if (kdys != null && kdys.Remarks != "")
            {
                var list = kdys.Remarks.Split('|');
                if (list.Length > 0)
                {
                    var user = DefinedCacheHelper.GetComboUsers().Where(o => list.Contains(o.Name)).ToList();
                    if (user.Count > 0)
                    {
                        cglueJianChaYiSheng.Properties.DataSource = user;
                        if (user.Any(o => o.Id == CurrentUser.Id))
                        {
                            cglueJianChaYiSheng.EditValue = CurrentUser.Id;
                        }
                        else
                        {
                            var cuuser = user.FirstOrDefault(o => o.Name == list[0]);
                            cglueJianChaYiSheng.EditValue = cuuser.Id;


                        }
                    }
                }


            }

            txtTeamID.EditValue = null;
            DateChekDate.EditValue =  _commonAppService.GetDateTimeNow().Now;//测试人员要求默认今天
            searchLookUpEditClient.Properties.DataSource = clientRegs;
            txtCheckState.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            txtDays.EditValue = 1;
            txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();

            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\CustomerReg.json";
            if (File.Exists(fp))  // 判断是否已有相同文件 
            {
                var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fp));
                foreach (var tj in Search)
                {
                    if (tj.Name == "CheckType")
                    {
                        lookUpEditClientType.EditValue = int.Parse(tj.Text);
                    }


                }
            }

            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetComboUsers();
        }
        /// <summary>
        /// 清空控件数据
        /// </summary>
        private void ClearControlData()
        {
            labelClientInfo.Text = "";
            isChangeItemGroup = false;
            dxErrorProvider.ClearErrors();
            txtVisitCard.Text = null;
            txtkdEmp.Text = null;
            //txtWX.Text = null;
            //txtFrom.Text = null;
            txtLasttime.Text = null;
            txtPersonnelCategory.EditValue = null;
            //txtCheckSate.Text = null;
            //txtSFState.Text = null;
            登记列表.Text = "登记列表";
            labelCusInfo.Text = "";
           txtCustoemrCode.EditValue = null;
            //gridLookUpCustomerType.EditValue = null;
            txtArchivesNum.EditValue = null;
            txtCardNumber.EditValue = null;
            txtName.EditValue = null;
            txtJKZ.EditValue = null;
            txtIntroducer.EditValue = null;
            lookUpEditInfoSource.EditValue = null;
            txtQq.EditValue = null;
            cuslookItemSuit.EditValue = null;
            gridLookUpSex.EditValue = null;
            txtAge.EditValue = null;//data.AgeUnit
            gridLookUpMarriageStatus.EditValue = null;
            txtIDCardNo.EditValue = null;
            txtMobile.EditValue = null;
            DateBirthday.EditValue = null;
            DateChekDate.EditValue = null;
            txtAdress.EditValue = null;
            txtFp.EditValue = null;
            txtDepartment.EditValue = null;
            txtRemarks1.EditValue = null;
            txtNation.EditValue = null;
            gridLookUpConceive.EditValue = null;
            txtClientRegID.EditValue = null;
            txtTeamID.EditValue = null;
            checkReplace.EditValue = false;
            checkReview.EditValue = null;
           
            labItemSuitName.Text = " ";
            //lookUpEditClientType.EditValue = null;
            curCustomRegInfo = null;
            pictureCus.Image = null;
            pictureCus.EditValue = null;
            pictureCusReg.Image = null;
            pictureCusReg.EditValue = null;
            pictureqm.Image = null;
            pictureqm.EditValue = null;
            lookUpEditCustomerType.EditValue = null;
            gridControlgroups.DataSource = null;
            labGroups.Text = "已选项目0项，正常0，加项0，减项0";
            if (checkEditSD.Checked != true)
            {
               
                //职业健康相关
                txtRiskS.EditValue = null;
                txtRiskS.Tag = null;
                txtWorkName.EditValue = null;
                txtTypeWork.EditValue = null;
                txtCheckType.EditValue = null;
                txtTotalWorkAge.EditValue = null;
                txtInjuryAge.EditValue = null;

                textEditClientname.EditValue = null;
                textEditClientCode.EditValue = null;

                textEditOther.EditValue = null;

                lookUpEditClientType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList().FirstOrDefault().Value;
                //加载默认查询条件
                string fp = System.Windows.Forms.Application.StartupPath + "\\CustomerReg.json";
                if (File.Exists(fp))  // 判断是否已有相同文件 
                {
                    var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fp));
                    foreach (var tj in Search)
                    {
                        if (tj.Name == "CheckType")
                        {
                            lookUpEditClientType.EditValue = int.Parse(tj.Text);
                        }


                    }
                }
            }
            textEditRegRemark.EditValue = null;
            txtPrimaryName.EditValue = null;
           

            if (shenfenzhengData != null)
                txtIDCardType.EditValue = shenfenzhengData.Value;
            labelControl1.Text = "";
            comboBoxEdit1.EditValue = null;
            if (comboBoxEdit1.Properties.DataSource != null)
            {
                var ghks = comboBoxEdit1.Properties.DataSource as List<BasicDictionaryDto>;
                if (ghks != null && ghks.Count > 0)
                {
                    comboBoxEdit1.EditValue = ghks[0].Value;
                }
            }


        }
        /// <summary>
        /// 从控件获取需保存的数据
        /// </summary>
        private QueryCustomerRegDto GetControlSaveData(bool isSave = false)
        {
            dxErrorProvider.ClearErrors();
            if (curCustomRegInfo == null)
            {
                curCustomRegInfo = new QueryCustomerRegDto();
                curCustomRegInfo.Customer = new QueryCustomerDto();
            }
            else if (curCustomRegInfo.Customer == null)
            {
                curCustomRegInfo.Customer = new QueryCustomerDto();
            }
            var input = curCustomRegInfo;//new QueryCustomerRegDto();
            //input.Customer = new QueryCustomerDto();
            //input.CustomerBM = txtCustoemrCode.EditValue?.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(txtCustoemrCode.Text.Trim()))
            {
                input.CustomerBM = txtCustoemrCode.Text.Trim();

            }
            if (string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                dxErrorProvider.SetError(txtCustoemrCode, string.Format(Variables.MandatoryTips, "体检号"));
                txtCustoemrCode.Focus();
                return null;
            }
            //input.Customer.ArchivesNum = txtArchivesNum.EditValue?.ToString().Trim();
            if (string.IsNullOrWhiteSpace(input.Customer.ArchivesNum))
            {//档案号为空时使用体检号作为档案号
                input.Customer.ArchivesNum = input.CustomerBM;
            }
            input.Customer.Name = txtName.EditValue?.ToString().Trim();
            if (string.IsNullOrWhiteSpace(input.Customer.Name))
            {
                dxErrorProvider.SetError(txtName, string.Format(Variables.MandatoryTips, "姓名"));
                txtName.Focus();
                return null;
            }
            if (!string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()))
            {
                input.Customer.Sex = (int)gridLookUpSex.EditValue;
            }
            else
            {
                dxErrorProvider.SetError(gridLookUpSex, string.Format(Variables.MandatoryTips, "性别"));
                gridLookUpSex.Focus();
                return null;
            }
            if (txtAge.EditValue != null)
            {
                input.Customer.Age = input.RegAge = Convert.ToInt32(txtAge.EditValue);
            }
            else
            {
                dxErrorProvider.SetError(txtAge, string.Format(Variables.MandatoryTips, "年龄"));
                txtAge.Focus();
                return null;
            }
            if (gridLookUpMarriageStatus.EditValue != null)
            {
                input.MarriageStatus = (int)gridLookUpMarriageStatus.EditValue;
            }
            else
            {
                input.MarriageStatus = null;
            }
            input.Customer.MarriageStatus = input.MarriageStatus;
            if (gridLookUpConceive.EditValue != null)
            {
                input.ReadyPregnancybirth = (int)gridLookUpConceive.EditValue;
            }
            else
                input.ReadyPregnancybirth = null;
            if (!string.IsNullOrWhiteSpace(txtIDCardType.EditValue?.ToString()))
                input.Customer.IDCardType = (int)txtIDCardType.EditValue;
            input.Customer.IDCardNo = txtIDCardNo.EditValue?.ToString().Trim();
            input.Customer.Mobile = txtMobile.EditValue?.ToString().Trim();
            input.Customer.VisitCard = txtVisitCard.EditValue?.ToString().Trim();
            if (DateBirthday.EditValue != null)
            {
                input.Customer.Birthday = DateBirthday.DateTime;
            }
            else
            {
                input.Customer.Birthday = null;
            }
            if (DateChekDate.EditValue == null)
                input.BookingDate = null;
            else
                input.BookingDate = DateChekDate.DateTime;
            input.Customer.Address = txtAdress.EditValue?.ToString().Trim();
            input.Customer.Department = txtDepartment.EditValue?.ToString().Trim();
            if (lookUpEditClientType.EditValue != null)
                input.PhysicalType = (int)lookUpEditClientType.EditValue;
            else
            {
                input.PhysicalType = null;
            }
            //if (txtPersonnelCategory.EditValue != null)
            //{
            //    input.Customer.CustomerType = (int)txtPersonnelCategory.EditValue;
            //    input.CustomerType = input.Customer.CustomerType;
            //}
            if (!string.IsNullOrWhiteSpace(lookUpEditCustomerType.EditValue?.ToString()))
            {
                input.CustomerType = int.Parse(lookUpEditCustomerType.EditValue.ToString());
            }
            else
            {
                input.CustomerType = null;
            }

            input.ClientType = ((int)ClientSate.Normal).ToString();//目前为正常 散检单位还没有
            if (txtClientRegID.EditValue != null)
            {
                input.ClientRegId = Guid.Parse(txtClientRegID.EditValue.ToString());

                input.ClientInfoId = clientRegs.FirstOrDefault(p => p.Id == input.ClientRegId)?.ClientInfo?.Id;
            }
            else
            {
                input.ClientRegId = null;
            }
            if (txtTeamID.EditValue != null)
            {
                input.ClientTeamInfo_Id = Guid.Parse(txtTeamID.EditValue.ToString());
                input.ClientTeamInfo = txtTeamID.GetSelectedDataRow() as ClientTeamInfoDto;
            }
            else
            {
                input.ClientTeamInfo_Id = null;
            }
            if (txtRemarks1.EditValue != null)
                input.Customer.Remarks = txtRemarks1.EditValue?.ToString().Trim();
            else
            {
                input.Customer.Remarks = null;
            }
            if (txtNation.EditValue != null)
                input.Customer.Nation = txtNation.EditValue?.ToString().Trim();
            else
            {
                input.Customer.Nation = null;
            }
            if (checkReview.EditValue == null)
                input.ReviewSate = null;
            else
                input.ReviewSate = (int)checkReview.EditValue;
            if (checkReplace.Checked)
            {
                input.ReplaceSate = 2;
            }
            else
            {
                input.ReplaceSate = 1;
            }
            if (txtPrimaryName.EditValue == null)
                input.PrimaryName = null;
            else
                input.PrimaryName = txtPrimaryName.EditValue.ToString();
            if (!string.IsNullOrWhiteSpace(txtPersonnelCategory.EditValue?.ToString()))
            {
                input.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
            }
            else
            {
                input.PersonnelCategoryId = null;
            }
            if (!isSave)
            {
                input.RegisterState = (int)RegisterState.Yes;
            }
            else
            {
                if (input.Id == Guid.Empty)
                    input.RegisterState = (int)RegisterState.No;
                else if (!input.RegisterState.HasValue)
                    input.RegisterState = (int)RegisterState.No;

            }
            //if (!isSave)改成api里面赋值了
            //{
            //    try
            //    {
            //        if (!input.LoginDate.HasValue)
            //        {
            //            ICommonAppService commonSvr = new CommonAppService();

            //            input.LoginDate = commonSvr.GetDateTimeNow().Now;
            //        }
            //    }
            //    catch (UserFriendlyException ex)
            //    {
            //        ShowMessageBox(ex);
            //    }
            //}
            //else
            //{
            //    if (input.Id == Guid.Empty)
            //        input.LoginDate = null;
            //}
            if (string.IsNullOrWhiteSpace(input.WebQueryCode))
                input.WebQueryCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToString();
            if (!input.CheckSate.HasValue)
                input.CheckSate = (int)PhysicalEState.Not;
            if (!input.SummSate.HasValue)
                input.SummSate = (int)SummSate.NotAlwaysCheck;
            if (!input.SummLocked.HasValue)
                input.SummLocked = (int)SDState.Unlocked;
            if (!input.PrintSate.HasValue)
                input.PrintSate = (int)PrintSate.NotToPrint;
            if (!input.BlindSate.HasValue)
                input.BlindSate = (int)BlindSate.Normal;
            if (!input.SendToConfirm.HasValue)
                input.SendToConfirm = (int)SendToConfirm.No;
            if (!input.GuidanceSate.HasValue)
                input.GuidanceSate = (int)PrintSate.NotToPrint;
            if (!input.BarState.HasValue)
                input.BarState = (int)PrintSate.NotToPrint;
            if (!input.RequestState.HasValue)
                input.RequestState = (int)PrintSate.NotToPrint;
            //input.Customer.CusPhotoBM = pictureToString(pictureCus.Image);
            input.Introducer = txtIntroducer.Text;
            if (!string.IsNullOrEmpty(lookUpEditInfoSource.EditValue?.ToString()))
            {
                input.InfoSource = (int)lookUpEditInfoSource.EditValue;
            }
            input.Customer.Qq = txtQq.Text;

            input.JKZBM = txtJKZ.Text;
            //预约备注
            input.Remarks = textEditRegRemark.EditValue?.ToString();
            input.Customer.CardNumber = txtCardNumber.Text;
            //职业健康
            if (txtRiskS.Tag != null)
            {
                input.OccHazardFactors = (List<ShowOccHazardFactorDto>)txtRiskS.Tag;
                input.RiskS = txtRiskS.EditValue.ToString();
            }
            else
            {
                if ((Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业"))) && !lookUpEditClientType.Text.ToString().Contains("放射"))
                {
                    dxErrorProvider.SetError(txtRiskS, string.Format(Variables.MandatoryTips, "危害因素"));
                    txtRiskS.Focus();
                    return null;
                }
            }
            if (txtWorkName.EditValue != null)
            {
                input.WorkName = txtWorkName.EditValue.ToString();
            }
            //else
            //{
            //    if (Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")))
            //    {
            //        dxErrorProvider.SetError(txtWorkName, string.Format(Variables.MandatoryTips, "车间"));
            //        txtWorkName.Focus();
            //        return null;
            //    }
            //}
            if (txtTypeWork.EditValue != null)
            {
                input.TypeWork = txtTypeWork.EditValue.ToString();
            }
            //else
            //{
            //    if (Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")))
            //    {
            //        dxErrorProvider.SetError(txtTypeWork, string.Format(Variables.MandatoryTips, "工种"));
            //        txtTypeWork.Focus();
            //        return null;
            //    }
            //}
            if (txtCheckType.EditValue != null)
            {
                input.PostState = txtCheckType.EditValue.ToString();
            }
            //else
            //{
            //    if (Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")))
            //    {
            //        dxErrorProvider.SetError(txtCheckType, string.Format(Variables.MandatoryTips, "检查类别"));
            //        txtCheckType.Focus();
            //        return null;
            //    }
            //}
            if (txtTotalWorkAge.EditValue != null && !string.IsNullOrEmpty(comunit2.EditValue?.ToString()))
            {
                input.TotalWorkAge = txtTotalWorkAge.EditValue.ToString();
                input.WorkAgeUnit = comunit2.Text;
            }
            //else
            //{
            //    if (Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")))
            //    {
            //        if (txtTotalWorkAge.EditValue == null)
            //        {
            //            dxErrorProvider.SetError(txtTotalWorkAge, string.Format(Variables.MandatoryTips, "总工龄"));
            //            txtTotalWorkAge.Focus();
            //            return null;
            //        }
            //        if (string.IsNullOrEmpty(comunit2.EditValue?.ToString()))
            //        {
            //            dxErrorProvider.SetError(comunit2, string.Format(Variables.MandatoryTips, "总工龄单位"));
            //            comunit2.Focus();
            //            return null;
            //        }
            //    }
            //}
            if (txtInjuryAge.EditValue != null && !string.IsNullOrEmpty(comunit.EditValue?.ToString()))
            {
                input.InjuryAge = txtInjuryAge.EditValue.ToString();
                input.InjuryAgeUnit = comunit.Text;
            }
            //if ( !string.IsNullOrEmpty(textEditClientname.EditValue?.ToString()))
            //{
                input.employerEnterpriseName = textEditClientname.EditValue?.ToString();
             
            //}
            //if (!string.IsNullOrEmpty(textEditClientCode.EditValue?.ToString()))
            //{
                input.employerCreditCode = textEditClientCode.EditValue?.ToString();

            input.OtherTypeWork= textEditOther.EditValue?.ToString();
            //}
            //else
            //{
            //    if (Variables.ISZYB == "2" || (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")))
            //    {
            //        if (txtInjuryAge.EditValue == null)
            //        {
            //            dxErrorProvider.SetError(txtInjuryAge, string.Format(Variables.MandatoryTips, "危害工龄"));
            //            txtInjuryAge.Focus();
            //            return null;
            //        }
            //        if (string.IsNullOrEmpty(comunit.EditValue?.ToString()))
            //        {
            //            dxErrorProvider.SetError(comunit, string.Format(Variables.MandatoryTips, "接害工龄单位"));
            //            comunit2.Focus();
            //            return null;
            //        }
            //    }
            //}           
            if (comboBoxEdit1.EditValue != null)
            {
                input.NucleicAcidType = (int?)comboBoxEdit1.EditValue;
            }
            if (cglueJianChaYiSheng.EditValue != null)
            {
                input.OrderUserId = (long?)cglueJianChaYiSheng.EditValue;
            }
            input.FPNo = txtFp.EditValue?.ToString().Trim();

            return input;
        }

        /// <summary>
        /// 查询今日登记未登记人数
        /// </summary>
        private void QueryRegNumbers()
        {
            try
            {
                labelControl3.Text = "";// Clear();
                    
                var numbers = customerSvr.QueryRegNumbers();
                var str = "今日数据：总人数：" + numbers.SumReg + "";
                labelControl3.Text = str;
                //labelControl3.MaskBox.AppendText(str);
                int count = 1;
                foreach (var data in numbers.datas)
                {
                    var  str1 =@"
" +  data.ClientName + ":总数：" + data.Sum
                        + "(男:" + data.Male
                        + "，女：" + data.Famale + @")
                        ";
                    count += 1;
                    //labelControl3.MaskBox.AppendText(str1);
                    str += str1;
                    //+ "，未说明的性别：" +data.NoSex;
                }
                //labelControl1.Text = str;
                labelControl3.Text = str; 
                //labelControl3.MinimumSize = new Size
                //{ Height = 23 * count, Width =0};
              
                
                



                //                str = string.Format(@"已登记：-人，-男-女。未登记：-人已登记：-人，-男-女。未登记：-人
                //已登记：-人，-男-女。未登记：-人
                //已登记：-人，-男-女。未登记：-人
                //已登记：-人，-男-女。未登记：-人
                //已登记：-人，-男-女。未登记：-人
                //已登记：-人，-男-女。未登记：-人");
                //                labelControl3.Text = str;
                //labelControl1.Text = string.Format(
                //    "今日数据：<br>已登记：{0}人，{1}男{2}女{3}未说明性别。<br>" +
                //    "体检中：{4}人，{5}男{6}女{7}未说明性别。<br>" +
                //    "已总检：{8}人，{9}男{10}女{11}未说明性别。<br>" +
                //    "已审核：{12}人，{13}男{14}女{15}未说明性别。<br>" +
                //    "已完成：{16}人，{17}男{18}女{19}未说明性别。<br>" +
                //     "未登记：{20}人，{21}男{22}女{23}未说明性别。",
                //    numbers.SumReg,    numbers.MaleReg,    numbers.FemaleReg, numbers.SumReg-numbers.MaleReg-numbers.FemaleReg,
                //    numbers.Tijianzhong,  numbers.TijianzhongMale,  numbers.TijianzhongFemale,    numbers.Tijianzhong- numbers.TijianzhongMale- numbers.TijianzhongFemale,
                //    numbers.Yizongjian,   numbers.YizongjianMale,   numbers.YizongjianFemale,   numbers.Yizongjian- numbers.YizongjianMale- numbers.YizongjianFemale,
                //    numbers.Yishenhe,    numbers.YishenheMale, numbers.YishenheFemale,    numbers.Yishenhe- numbers.YishenheMale- numbers.YishenheFemale,
                //    numbers.WanchengTijian,  numbers.WanchengTijianMale,  numbers.WanchengTijianFemale,   numbers.WanchengTijian- numbers.WanchengTijianMale- numbers.WanchengTijianFemale,
                //    numbers.NotReg,   numbers.MaleNotReg,   numbers.FemaleNotReg,   numbers.NotReg - numbers.MaleNotReg - numbers.FemaleNotReg);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        /// <summary>
        /// 设置界面按钮可用
        /// </summary>
        private void SetBtnEnabled()
        {
            if (curCustomRegInfo == null)
            {
                //butprintFroms.Enabled = false;
                dropDownButtonPrintGuidance.Enabled = false;
                butprintBars.Enabled = false;
                butPintFaB.Enabled = false;
                butCharge.Enabled = false;
                butHandform.Enabled = false;
                return;
            }
            if (curCustomRegInfo.Id == Guid.Empty)
            {
                //butprintFroms.Enabled = false;
                dropDownButtonPrintGuidance.Enabled = false;
                butprintBars.Enabled = false;
                butPintFaB.Enabled = false;
                butCharge.Enabled = false;
                butHandform.Enabled = false;
                //butAddSuit.Enabled = true;
                cuslookItemSuit.Enabled = true;
                butAddGroup.Enabled = true;
                butX.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                //修改总检才不能操作
                //if (curCustomRegInfo.SendToConfirm == (int)SendToConfirm.Yes || (curCustomRegInfo.SummSate != (int)SummSate.NotAlwaysCheck && curCustomRegInfo.SummSate.HasValue))
                if ((curCustomRegInfo.SummSate != (int)SummSate.NotAlwaysCheck && curCustomRegInfo.SummSate.HasValue))
                {
                    cuslookItemSuit.Enabled = false;
                    //butprintFroms.Enabled = false;
                    dropDownButtonPrintGuidance.Enabled = false;
                    butprintBars.Enabled = false;
                    butPintFaB.Enabled = false;
                    butCharge.Enabled = false;
                    butHandform.Enabled = false;
                    //butDetails.Enabled = false;
                    butOK.Enabled = false;
                    butCancel.Enabled = false;
                    butAddGroup.Enabled = butX.Enabled = butCopyGroups.Enabled = false;
                    btnSave.Enabled = false;
                    return;
                }
                if (curCustomRegInfo.ClientTeamInfo_Id != Guid.Empty || curCustomRegInfo.ClientTeamInfo_Id != null || !curCustomRegInfo.ClientTeamInfo_Id.HasValue)
                {
                    //butprintFroms.Enabled = true;
                    dropDownButtonPrintGuidance.Enabled = true;
                    butprintBars.Enabled = true;
                    butPintFaB.Enabled = true;
                    butCharge.Enabled = true;
                    butHandform.Enabled = true;
                    //butAddSuit.Enabled = true;
                    cuslookItemSuit.Enabled = true;
                    butAddGroup.Enabled = true;
                    butX.Enabled = true;
                    butCancel.Enabled = true;
                    butDetails.Enabled = true;
                    butCopyGroups.Enabled = true;
                }
                else
                {
                    var sfCredit = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 100);
                    if (sfCredit != null)
                    {
                        if (sfCredit.Remarks == "No")
                        {//开启了收费验证则需要判断未收费情况下不能打印
                            if (curCustomRegInfo.CostState != (int)PayerCatType.Charge)
                            {
                                //butprintFroms.Enabled = false;
                                dropDownButtonPrintGuidance.Enabled = false;
                                butprintBars.Enabled = false;
                                butPintFaB.Enabled = false;
                            }
                            else
                            {
                                //butprintFroms.Enabled = true;
                                dropDownButtonPrintGuidance.Enabled = true;
                                butprintBars.Enabled = true;
                                butPintFaB.Enabled = true;
                            }
                        }
                        else
                        {
                            //butprintFroms.Enabled = true;
                            dropDownButtonPrintGuidance.Enabled = true;
                            butprintBars.Enabled = true;
                            butPintFaB.Enabled = true;
                        }
                    }
                    else
                    {
                        //butprintFroms.Enabled = true;
                        dropDownButtonPrintGuidance.Enabled = true;
                        butprintBars.Enabled = true;
                        butPintFaB.Enabled = true;
                    }
                }
                //获取是否开启收费才能打导引单/条码 已经验证了
                if (curCustomRegInfo.CostState == (int)PayerCatType.PersonalCharge && curCustomRegInfo.ItemSuitBMId.HasValue)
                {
                    //butAddSuit.Enabled = false;
                    cuslookItemSuit.Enabled = false;
                }
                butCharge.Enabled = true;
                butHandform.Enabled = true;
                btnSave.Enabled = true;
            }
            butOK.Enabled = true;
        }
        #endregion

        #region 图片
        /// <summary>
        /// 图片string转为图片
        /// </summary>
        private Image strToString(string str)
        {
            byte[] imageBytes = Convert.FromBase64String(str);
            using (MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }
        /// <summary>
        /// 图片转为string
        /// </summary>
        private string pictureToString(Image image)
        {
            if (image == null)
            {
                return "";
            }
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                var result = Convert.ToBase64String(data);
                return result;
            }
        }



        #endregion
        /// <summary>
        /// 窗体显示后焦点在体检编码上
        /// </summary>
        private void CustomerReg_Shown(object sender, EventArgs e)
        {
            txtCustoemrCode.Focus();
            labelControl3.MinimumSize = new Size
            { Height = 0, Width = 0 };
            var katsts = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 10)?.Remarks;
            if (!string.IsNullOrEmpty(katsts) && katsts == "1")
            {
                #region 开启推送提示
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
                var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 1)?.Remarks;
                if ((!string.IsNullOrEmpty(HISjk) && HISjk == "1") || (!string.IsNullOrEmpty(DJTS) && DJTS == "1"))
                {
                    var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\推送提示";
                    Process KHMsg = new Process();
                    KHMsg.StartInfo.FileName = reportpath + "\\TSMess.exe";
                    KHMsg.Start();
                }
                #endregion
            }
        }
        /// <summary>
        /// 替检勾选后打开详情 提示填写原体检人名字
        /// </summary>
        private void checkReplace_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkReplace.Checked && string.IsNullOrWhiteSpace(curCustomRegInfo.PrimaryName))
            //{
            //    butDetails_Click(sender, e);
            //}
        }
        /// <summary>
        /// 身份证号填写后就设置年龄、性别和生日
        /// </summary>
        private void txtIDCardNo_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (txtIDCardType.Text.Contains("身份证"))
            {
                dxErrorProvider.ClearErrors();
                if (e.Value?.ToString() == null)
                {
                    txtAge.ReadOnly = false;
                    gridLookUpSex.ReadOnly = false;
                    DateBirthday.ReadOnly = false;
                    return;
                }
                else
                {
                    var data = VerificationHelper.GetByIdCard(e.Value?.ToString());
                    if (data != null)
                    {
                        txtAge.EditValue = data.Age;
                        gridLookUpSex.EditValue = (int)data.Sex;
                        DateBirthday.EditValue = data.Birthday;
                        txtAge.ReadOnly = true;
                        gridLookUpSex.ReadOnly = true;
                        DateBirthday.ReadOnly = true;
                        getCusInfo(e.Value?.ToString());
                    }
                    else
                    {
                        dxErrorProvider.SetError(txtIDCardNo, string.Format("身份证号输入错误"));
                        //txtIDCardNo.EditValue = null;
                        txtIDCardNo.Focus();
                        txtAge.ReadOnly = false;
                        gridLookUpSex.ReadOnly = false;
                        DateBirthday.ReadOnly = false;
                    }
                }
            }

        }
        /// <summary>
        /// 窗体按键盘事件 F2读取身份证
        /// </summary>
        private void CustomerReg_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.F2)
            {
                if (driver != null)
                {
                    var card = driver.ReadCardInfo();
                    Image photo = null;
                    if (card.Succeed)
                    {

                        DateTime.TryParseExact(card.Card.Birthday,
                              "yyyyMMdd",
                              CultureInfo.CurrentCulture,
                              DateTimeStyles.None,
                              out var dt);
                        var age =  _commonAppService.GetDateTimeNow().Now.Year - dt.Year;
                        var nowTime =  _commonAppService.GetDateTimeNow().Now;
                        if (nowTime.Month > DateBirthday.DateTime.Month)
                            age = nowTime.Year - dt.Year;
                        else if (nowTime.Month == dt.Month && nowTime.Day >= dt.Day)
                            age = nowTime.Year - dt.Year;
                        else
                            age = nowTime.Year - dt.Year - 1;
                        if (txtAge.EditValue?.ToString() != age.ToString())
                            txtAge.EditValue = age;

                        int? IDCardType = null;
                        var Address = "";
                        var GuoJi = "";
                        string mz = "";
                        if (card.Card.CertType == "I")
                        {// 外国人居留证
                            var idCard = (ResidencePermit)card.Card;
                            var waiguo = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "外国人居留证");
                            if (waiguo != null)
                                IDCardType = waiguo.Value;
                            GuoJi = idCard.NationCode;
                            //    curCustomRegInfo.Customer.IDCardType = waiguo.Value;
                            //curCustomRegInfo.Customer.GuoJi = idCard.NationCode;
                        }
                        else if (card.Card.CertType == "J")
                        {//港澳台居住证
                            var idCard = (ResidentialPass)card.Card;
                            var gangao = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "港澳台居住证");
                            if (gangao != null)
                                IDCardType = gangao.Value;
                            Address = idCard.Address;
                            //    curCustomRegInfo.Customer.IDCardType = gangao.Value;
                            //curCustomRegInfo.Customer.Address = idCard.Address;
                        }
                        else if (card.Card.CertType == string.Empty)
                        {//身份证
                            var idCard = (IdCard)card.Card;
                            Address = idCard.Address;
                            mz = idCard.Nation;
                            // MessageBox.Show(mz);
                            // MessageBox.Show(idCard.NationCode);
                            // curCustomRegInfo.Customer.Address = idCard.Address;
                        }
                        SearchCustomerDto searchCustomerDto = new SearchCustomerDto();
                        searchCustomerDto.IDCardNo = card.Card.IdCardNo;
                        int sexCode = 1;
                        if (card.Card.Sex.Contains("女"))
                        {
                            sexCode = 2;

                        }
                        var data = QueryCusDataIdNum(searchCustomerDto, card.Card.Name, sexCode, age, card.Card.Photo, Address);
                        if (data == null)
                        {
                            return;
                        }
                        if (data != null && data.CustomerBM != null && data.CustomerBM != "")
                        {
                            #region 要求预约信息也显示刷身份证信息
                            data.Customer.IDCardNo = card.Card.IdCardNo;
                            data.Customer.Name = card.Card.Name;
                            data.Customer.Sex = sexCode;
                            data.Customer.Nation = mz;
                            data.Customer.IDCardType = IDCardType;
                            data.Customer.GuoJi = GuoJi;
                            data.Customer.Address = Address;
                            data.Customer.Birthday = dt;
                            //var url = AppDomain.CurrentDomain.BaseDirectory + Guid.NewGuid().ToString("N");
                            //card.Card.Photo.Save(url);
                            photo = card.Card.Photo;
                            #endregion

                            LoadCustomerData(data, photo);
                            return;
                        }
                        else
                        {
                            // alertInfo.Show(this, "提示!", "没有该体检人预约信息！");
                            if ((txtName.Text.Trim() != "" && card.Card.Name != txtName.Text.TrimEnd()) ||
                            (txtIDCardNo.Text.Trim() != "" && card.Card.IdCardNo != txtIDCardNo.Text.TrimEnd()))
                            {
                                string ts = "姓名或身份证号不符！不能修改，如果需要修改，请清空当前姓名和身份证信息！";
                                MessageBox.Show(ts);
                                //if (DevExpress.XtraEditors.XtraMessageBox.Show(ts, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return;
                            }
                            bool isold = false;
                            if (data.Customer != null &&
                                (!string.IsNullOrEmpty(data.Customer.Mobile) || data.Customer.MarriageStatus.HasValue))
                            { isold = true; }
                            addCusByIdCard(card.Card.IdCardNo, isold);
                        }
                        curCustomRegInfo.Customer.IDCardNo = card.Card.IdCardNo;
                        curCustomRegInfo.Customer.Name = card.Card.Name;
                        curCustomRegInfo.Customer.Sex = sexCode;
                        curCustomRegInfo.Customer.Nation = mz;
                        curCustomRegInfo.Customer.IDCardType = IDCardType;
                        curCustomRegInfo.Customer.GuoJi = GuoJi;
                        curCustomRegInfo.Customer.Address = Address;
                        curCustomRegInfo.Customer.Birthday = dt;
                        //var url = AppDomain.CurrentDomain.BaseDirectory + Guid.NewGuid().ToString("N");
                        //card.Card.Photo.Save(url);
                        photo = card.Card.Photo;

                        //if (!IsReging || butAddReg.Enabled == true)
                        //{
                        LoadCustomerData(curCustomRegInfo, photo);
                        //}
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw ex;
                        //    ShowMessageBoxWarning(ex.Message);
                        //}

                    }
                    else
                    {

                        ShowMessageBoxWarning(card.Explain);
                    }
                    txtMobile.SelectAll();
                    txtMobile.Focus();
                }

            }
            if (e.KeyCode == Keys.F3)
            {
                if (cardriver != null)
                {
                    var card = cardriver.ReadCardInfo();
                    if (card != null && card.CardNo != "")
                    {
                        txtVisitCard.Text = card.CardNo;
                        txtVisitCard_KeyDown(sender, e);
                    }

                }

            }
        }
        /// <summary>
        /// 读取身份证信息按钮
        /// </summary>
        private void txtIDCardNo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            CustomerReg_KeyDown(sender, new KeyEventArgs(Keys.F2));
        }
        /// <summary>
        /// 生日填写后计算年龄
        /// </summary>
        private void DateBirthday_Leave(object sender, EventArgs e)
        {
            if (DateBirthday.EditValue != null)
            {
                //var age =  _commonAppService.GetDateTimeNow().Now.Year - DateBirthday.DateTime.Year;
                //if (txtAge.EditValue?.ToString() != age.ToString())
                //    txtAge.EditValue = age;
                DateTime birthday = DateBirthday.DateTime;


                DateTime nowTime =  _commonAppService.GetDateTimeNow().Now;
                if (nowTime.Year < DateBirthday.DateTime.Year)
                {
                    return;
                }
                int age = 0;
                if (nowTime.Month > DateBirthday.DateTime.Month)
                    age = nowTime.Year - birthday.Year;
                else if (nowTime.Month == birthday.Month && nowTime.Day >= birthday.Day)
                    age = nowTime.Year - birthday.Year;
                else
                    age = nowTime.Year - birthday.Year - 1;
                if (txtAge.EditValue?.ToString() != age.ToString())
                    txtAge.EditValue = age;
            }
        }
        /// <summary>
        /// 年龄填写后计算生日
        /// </summary>
        private void txtAge_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Text)) return;
            var year =  _commonAppService.GetDateTimeNow().Now.Year - Convert.ToInt32(txtAge.Text);
            var date = new DateTime(year, DateBirthday.DateTime.Month, DateBirthday.DateTime.Day);
            if (date != DateBirthday.DateTime)
                DateBirthday.EditValue = date;

        }
        /// <summary>
        /// 自定义列合计计算。去除减项项目，减项项目不参与计算
        /// </summary>
        private void gridView1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            string fieldName = (e.Item as GridSummaryItem).FieldName;
            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    break;
                case CustomSummaryProcess.Calculate:
                    break;
                case CustomSummaryProcess.Finalize:

                    if (e.IsTotalSummary)
                    {
                        var list = view.DataSource as List<TjlCustomerItemGroupDto>;
                        if (fieldName == GRPrice.FieldName)
                        {

                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.GRmoney);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == conItemPrice.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == conDiscountprice.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.PriceAfterDis);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == conTTPrice.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.TTmoney);
                            else
                                e.TotalValue = 0;
                        }

                    }
                    break;

            }
        }

        private void btnQueryCusRegList_Click(object sender, EventArgs e)
        {
            var input = new SearchCusRegListDto();
            if (!string.IsNullOrWhiteSpace(txtDays.EditValue?.ToString()))
                input.Day = Convert.ToInt32(txtDays.EditValue);
            if (txtCheckState.EditValue != null)
                input.CheckState = Convert.ToInt32(txtCheckState.EditValue);
            if (!string.IsNullOrWhiteSpace(searchLookUpEditClient.EditValue?.ToString()))
                input.ClientRegId = Guid.Parse(searchLookUpEditClient.EditValue?.ToString());
            if (checkEditdt.Checked == true)
            {
                if (!string.IsNullOrEmpty(dateEditStar.EditValue?.ToString()))
                {
                    input.Satr = DateTime.Parse(dateEditStar.DateTime.ToShortDateString());

                }
                if (!string.IsNullOrEmpty(dateEditEnd.EditValue?.ToString()))
                {
                    input.End = DateTime.Parse(dateEditEnd.DateTime.AddDays(1).ToShortDateString());

                }
            }
            var result = customerSvr.QueryRegList(input);
            gridControlDengJiList.DataSource = null;
            gridControlDengJiList.DataSource = result;
            QueryRegNumbers();
        }
        private void txtDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnQueryCusRegList.PerformClick();
        }
        private void gridViewDengjiList_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.Name == conCheckState.Name)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((ProjectIState)e.Value);
                var addminus = gridView1.GetRowCellValue(e.ListSourceRowIndex, colIsAddMinus);
                if (addminus != null)
                    if ((int)addminus == (int)AddMinusType.Minus)
                        e.DisplayText = EnumHelper.GetEnumDesc(AddMinusType.Minus);
            }
            if (e.Column.Name == conChargeState.Name)
            {
                e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
            }
            if (e.Column.FieldName == gridColumnRegisterState.FieldName)
                e.DisplayText = EnumHelper.GetEnumDesc((RegisterState)e.Value);

            if (e.Column.FieldName == gridColumnCostState.FieldName)
                e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);

            if (e.Column.FieldName == gridColumnSendToConfirm.FieldName)
                e.DisplayText = EnumHelper.GetEnumDesc((SendToConfirm)e.Value);

            if (e.Column.FieldName == gridColumnCheckSate.FieldName)
                e.DisplayText = EnumHelper.GetEnumDesc((ExaminationState)e.Value);
        }
        /// <summary>
        /// 左下角列表行双击
        /// </summary>
        private void gridViewDengjiList_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var customerbm = gridViewDengjiList.GetRowCellValue(e.RowHandle, gridColumnCustomerBM)?.ToString();
                var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = customerbm });
                LoadCustomerData(data);
            }
        }


        /// <summary>
        /// 照片双击
        /// </summary>
        private void pictureCus_DoubleClick(object sender, EventArgs e)
        {
            pictureCus.LoadImage();

            try
            {
                if (pictureCus.Image != null)
                {
                    ShowPic showPic = new ShowPic(txtCustoemrCode.Text.ToString(), txtName.Text.ToString(), gridLookUpSex.Text.ToString(), txtAge.Text.ToString(),
                        pictureCus.Image);
                    showPic.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var data = GetControlSaveData(true);
            if (data == null)
            {
                ShowMessageBoxWarning("没有要保存的数据");
                return;
            }
            btnSave.Enabled = false;
            //if (data.ClientRegId.Value != Guid.Empty)
            //{
            //    var FZSt = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = data.ClientRegId.Value });
            //    if (FZSt == 1)
            //    {
            //        ShowMessageBoxWarning("单位已封帐，不能执行该操作");
            //        return;
            //    }
            //}
            var IsChage = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.IsCharge, 10);
            if (IsChage != null && IsChage.Text == "0")
            {
                data = NoChargeState(data);
            }
            else
            {
                #region 保存前验证收费是否发生改变
                if (data.Id != Guid.Empty)
                {
                    if (checkChargeState(data) == false)
                    {
                        MessageBox.Show("收费状态发生改变，请刷新后再操作！");
                        return;
                    }
                }
                #endregion
            }
            #region 验证项目姓名状态                
            if (checkSexState(data) == false)
            {
                MessageBox.Show("所选组合和性别不匹配，请检查");
                return;
            }
            #endregion
            AutoLoading(() =>
            {
                SetPicToData();
                curCustomRegInfo = customerSvr.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                SetBtnEnabled();
                LoadCustomerData(curCustomRegInfo);
                QueryRegNumbers();
                //ShowMessageSucceed("保存成功。");
                txtCustoemrCode.Focus();
                IsReging = false;
            });
            btnSave.Enabled = true;
            butAddReg.Enabled = true;
        }
        /// <summary>
        /// 获取照片数据到当前客户
        /// </summary>
        private void SetPicToData()
        {
            var urlPic = pictureCus.GetLoadedImageLocation();
            //if (string.IsNullOrWhiteSpace(urlPic))
            //{
            if (pictureCus.Image != null)
            {
                var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                }
                pictureCus.Image.Save(url);
                urlPic = url;
            }
            //}
            if (!string.IsNullOrWhiteSpace(urlPic) && pictureCus.Image != null)
            {
                if (curCustomRegInfo.Customer.CusPhotoBmId.HasValue && curCustomRegInfo.Customer.CusPhotoBmId != Guid.Empty)
                {
                    _pictureController.Update(urlPic, curCustomRegInfo.Customer.CusPhotoBmId.Value);
                }
                else
                {
                    var picDto = _pictureController.Uploading(urlPic, "CusPhotoBm");
                    curCustomRegInfo.Customer.CusPhotoBmId = picDto.Id;
                }
            }
            else
            {
                
                    //_pictureController.Delete(curCustomRegInfo.Customer.CusPhotoBmId.Value);
                    curCustomRegInfo.Customer.CusPhotoBmId = null;
               
            }

            //登记照片

            var pictureCusRegPic = pictureCusReg.GetLoadedImageLocation();

            if (pictureCusReg.Image != null)
            {
                var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                }
                pictureCusReg.Image.Save(url);
                pictureCusRegPic = url;
            }

            if (!string.IsNullOrWhiteSpace(pictureCusRegPic) && pictureCusReg.Image != null)
            {
                if (curCustomRegInfo.PhotoBmId.HasValue && curCustomRegInfo.PhotoBmId != Guid.Empty)
                {
                    _pictureController.Update(pictureCusRegPic, curCustomRegInfo.PhotoBmId.Value);
                }
                else
                {
                    var picDto = _pictureController.Uploading(pictureCusRegPic, "CusPhotoBm");
                    curCustomRegInfo.PhotoBmId = picDto.Id;
                }
            }
            else
            {

                //_pictureController.Delete(curCustomRegInfo.Customer.CusPhotoBmId.Value);
                curCustomRegInfo.PhotoBmId = null;

            }
            //签名

            var signurlPic = pictureqm.GetLoadedImageLocation();

            if (pictureqm.Image != null)
            {
                var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                }
                pictureqm.Image.Save(url);
                signurlPic = url;
            }

            if (!string.IsNullOrWhiteSpace(signurlPic) && pictureqm.Image != null)
            {
                if (curCustomRegInfo.SignBmId.HasValue && curCustomRegInfo.SignBmId != Guid.Empty)
                {
                    _pictureController.Update(signurlPic, curCustomRegInfo.SignBmId.Value);
                }
                else
                {
                    var picDto = _pictureController.Uploading(signurlPic, "CusPhotoBm");
                    curCustomRegInfo.SignBmId = picDto.Id;
                }
            }
            else
            {

                //_pictureController.Delete(curCustomRegInfo.Customer.CusPhotoBmId.Value);
                curCustomRegInfo.SignBmId = null;

            }
        }

        private void renyuaLeibieView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == colFree.FieldName)
            //{
            //    var data = renyuaLeibieView.GetRowCellValue(e.ListSourceRowIndex, colIsFree);
            //    if (data != null)
            //    {
            //        if ((bool)data)
            //            e.DisplayText = "是";
            //        else
            //            e.DisplayText = "否";
            //    }
            //}
        }

        private void barButtonItemPreviewGuidance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ret = "";
            if (checkEditAdd.Checked == true)
            {
                //ret = PrintGuidanceNew.Print(curCustomRegInfo.Id, true, false, true);
                ret = PrintGuidanceNew.Print(curCustomRegInfo.Id, true, false);
            }
            else
            {
                //ret = PrintGuidanceNew.Print(curCustomRegInfo.Id, true, false);
                ret = PrintGuidanceNew.Print(curCustomRegInfo.Id, true);
            }
            //var ret=  PrintGuidanceNew.Print(curCustomRegInfo.Id, true, false);
            if (ret != "")
            {
                MessageBox.Show(ret);
            }
            txtCustoemrCode.Focus();
            txtCustoemrCode.SelectAll();
        }

        private void dropDownButtonPrintGuidance_Click(object sender, EventArgs e)
        {


            var ret = "";
            if (checkEditAdd.Checked == true)
            {
                // ret = PrintGuidanceNew.Print(curCustomRegInfo.Id,false,false,true);
                ret = PrintGuidanceNew.Print(curCustomRegInfo.Id, false, false);
            }
            else
            {
                ret = PrintGuidanceNew.Print(curCustomRegInfo.Id);
            }
            if (ret != "")
            {
                MessageBox.Show(ret);
            }
            //设置为已打印状态
            if (curCustomRegInfo != null)
            {
                curCustomRegInfo.GuidanceSate = (int)PrintSate.Print;
                if (curCustomRegInfo.CustomerItemGroup != null)
                {
                    foreach (var cusgroup in curCustomRegInfo.CustomerItemGroup)
                    {
                        cusgroup.GuidanceSate = (int)PrintSate.Print;
                    }
                }

            }
            //else
            //{
            //    var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = txtCustoemrCode.EditValue?.ToString() });
            //    LoadCustomerData(data);
            //}
        }
        /// <summary>
        /// 验证收费状态是否改变
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool checkChargeState(QueryCustomerRegDto input)
        {
            bool isok = true;
            if (input.Id != Guid.Empty)
            {
                EntityDto<Guid> cusName = new EntityDto<Guid>();
                cusName.Id = input.Id;
                CustomerRegCostDto customerReg = customerSvr.GetCustomerRegCost(cusName);
                if (input.CostState != customerReg.CostState)
                {
                    return false;
                }
                foreach (CustomerItemGroupDto cusgroup in customerReg.CustomerItemGroup)
                {
                    var ocusgroup = input.CustomerItemGroup.FirstOrDefault(p => p.ItemGroupName == cusgroup.ItemGroupName);
                    //if (ocusgroup != null && ocusgroup.PayerCat != cusgroup.PayerCat)
                    //{
                    //    return false;
                    //}
                    //需要测试收费没刷新会不会有问题
                    if (ocusgroup != null && ((ocusgroup.PayerCat == (int)PayerCatType.PersonalCharge && ocusgroup.PayerCat != cusgroup.PayerCat)
                        || (ocusgroup.PayerCat == (int)PayerCatType.NoCharge && cusgroup.PayerCat == (int)PayerCatType.PersonalCharge)))
                    {
                        return false;
                    }

                }

            }
            return isok;
        }
        /// <summary>
        /// 未关联收费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private QueryCustomerRegDto NoChargeState(QueryCustomerRegDto input)
        {


            foreach (TjlCustomerItemGroupDto cusgroup in input.CustomerItemGroup)
            {
                var ocusgroup = input.CustomerItemGroup.FirstOrDefault(p => p.ItemGroupName == cusgroup.ItemGroupName);
                if (ocusgroup.PayerCat == (int)PayerCatType.NoCharge)
                {
                    cusgroup.PayerCat = (int)PayerCatType.Charge;
                }
            }
            return input;


        }
        /// <summary>
        /// 验证性别和组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool checkSexState(QueryCustomerRegDto input)
        {
            bool isok = true;
            List<SimpleItemGroupDto> groups = Common.Helpers.CacheHelper.GetItemGroups().ToList();
            foreach (TjlCustomerItemGroupDto cusgroup in input.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus))
            {
                SearchItemGroupDto searchItem = new SearchItemGroupDto();
                searchItem.Id = cusgroup.ItemGroupBM_Id;

                if (groups.Count > 0)
                {
                    var gr = from c in groups where c.Id == cusgroup.ItemGroupBM_Id select c;
                    if (gr.Count() > 0)
                    {
                        var group = gr.ToList()[0];
                        if (group.Sex != (int)Sex.GenderNotSpecified && group.Sex != (int)Sex.Unknown && group.Sex != input.Customer.Sex)
                        {
                            return false;
                        }
                    }

                }
            }

            return isok;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //清空控件数据、清空当前登记人信息
            ClearControlData();
            butAddReg.Enabled = true;
            IsReging = false;
        }
        #region 排队接口
        public string SBCToDBC(string input)
        {
            char[] cc = input.ToCharArray();
            for (int i = 0; i < cc.Length; i++)
            {
                if (cc[i] == 12288)
                {
                    cc[i] = (char)32;
                    continue;
                }
                if (cc[i] > 65280 && cc[i] < 65375)
                {
                    cc[i] = (char)(cc[i] - 65248);
                }
            }
            return new string(cc);
        }




        private bool IsQuanJiao(string checkstr)
        {
            if (2 * checkstr.Length > Encoding.Default.GetByteCount(checkstr))
                return true;
            else return false;
        }
        Priority priority = new Priority(Btpd_base.PublicTool.SqlConnection);//优先权
        Btpd.BLL.NowTeam nowteam = new Btpd.BLL.NowTeam(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.DepartmentsInfo dal = new Btpd.BLL.DepartmentsInfo(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.LoginInfo login = new Btpd.BLL.LoginInfo(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.Binding binddal = new Btpd.BLL.Binding(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.DepartCall departcall = new Btpd.BLL.DepartCall(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.VnowTeam vnowteam = new Btpd.BLL.VnowTeam(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.UserInfo userinfo = new Btpd.BLL.UserInfo(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.Vmessage vmessage = new Btpd.BLL.Vmessage(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.DepartmentsInfo BllDepart = new Btpd.BLL.DepartmentsInfo(Btpd_base.PublicTool.SqlConnection);
        Btpd.BLL.Employee BLLEmp = new Btpd.BLL.Employee(Btpd_base.PublicTool.SqlConnection);
        string BChao = "";
        Btpd.BLL.PDConfig pdconfig = new Btpd.BLL.PDConfig(Btpd_base.PublicTool.SqlConnection);
        private string GetBTPD(string tjcode)
        {
            string strUserID = ConfigurationManager.AppSettings.Get(Variables.UserID);

            string strUserName = ConfigurationManager.AppSettings.Get(Variables.UserName);

            Btpd.BLL.Employee BLLEMP = new Btpd.BLL.Employee(Btpd_base.PublicTool.SqlConnection);
            List<Btpd.Model.Employee> dalemps = BLLEMP.GetList(" userid='" + strUserID + "'");
            if (dalemps.Count >= 1)
            {
                Glod.emp = dalemps[0];
            }
            //Glod.emp.UserID = strUserID;
            //Glod.emp.UserName = strUserName;
            string sqlConstr = ConfigurationManager.AppSettings.Get(Variables.tj);
            string KFDepartID = "";

            //排队人员全局变量
            Btpd.Model.Employee ementer = new Btpd.Model.Employee();
            ementer.UserID = "003";
            ementer.UserName = "admin";
            Btpd.Model.Glod.emp = ementer;
            //删除残余队列
            nowteam.UpdateSQL("delete FROM NowTeam WHERE DATEDIFF(day,TeamTime,GETDATE())>0");
            //如果没有语音初始化
            string strcoun = nowteam.SelectSql(" SELECT COUNT(1) from UserDepartmentNote where DATEDIFF(day,DateDay,GETDATE())=0 ");
            if (int.Parse(strcoun) < 1)
            {
                nowteam.UpdateSQL(" INSERT INTO UserDepartmentNote SELECT DepartmentsID,''," +
                    "GETDATE(),'1','','','','',GETDATE(), GETDATE() FROM DepartmentsInfo where ClassID!=0 ");
            }

            //SELECT  COUNT(1) FROM LoginInfo WHERE Enable=1
            strcoun = nowteam.SelectSql(" SELECT  COUNT(1) FROM LoginInfo WHERE Enable=1 ");
            if (int.Parse(strcoun) < 1)
            {
                MessageBox.Show("没有登录科室,将不能正常进行排队");
            }

            var strPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 110).Remarks;
            string pdinfo = "";
            if (strPD == "Y")
            {
                bool isHaveUSer = false;
                KFDepartID = "";
                StringBuilder dxid = new StringBuilder("IsEnable=0 and Externa='");

                if (string.IsNullOrEmpty(tjcode))
                {
                    return "体检号不能为空!";
                }
                if (IsQuanJiao(tjcode))
                {
                    tjcode = SBCToDBC(tjcode);

                }

                Btpd.cs_PublicPD BTCS_pu = new Btpd.cs_PublicPD();
                List<Btpd.Model.UserInfo> existmodel = userinfo.GetList("tjCode='" + tjcode + "'");

                string OutTime = pdconfig.GetModel("OutTime").ConfValue.Trim();//区域是否优先
                string PDCCKS = pdconfig.GetModel("PDCCKS").ConfValue.Trim();//区域是否优先
                                                                             //根据价格划分VIP
                int iqtdj001 = 1500;
                try
                {
                    iqtdj001 = Convert.ToInt16(pdconfig.GetModel("PDCCKS").ConfValue.Trim());
                }
                catch (Exception)
                {

                }

                Btpd.Model.UserInfo loginuser = new Btpd.Model.UserInfo();
                OpreateNote opre = new OpreateNote();
                Btpd.Model.UserInfo user = new Btpd.Model.UserInfo();
                OpreateNontEnter opreEnt = new OpreateNontEnter();
                string guid = "";
                Btpd.BLL.NowTeam BllNowTeam = new Btpd.BLL.NowTeam(Btpd_base.PublicTool.SqlConnection);
                if (existmodel.Count == 0)
                {
                    #region 第一次登记
                    SqlConnection tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                    tjcon.Open();//连接体检数据库
                    SqlCommand tjcom = new SqlCommand("select SELFBH,RFID,VIP,YYRXM,SEX,AGE,DXID,TeamOrder,ItemSuitIDName from tj_requireinfo where selfbh='" + tjcode + "' or RFID='" + tjcode + "' and datediff(DAY,TJRQ ,GETDATE())=0 ", tjcon);
                    SqlDataReader tjread = tjcom.ExecuteReader();
                    int coutn = 0;
                    int first = 0;
                    if (!tjread.HasRows)
                    {
                        return "数据异常,检测不到该用户信息!";
                    }

                    if (userinfo.GetModelList(" tjCode='" + tjcode + "'").Count > 0)
                    {
                        isHaveUSer = true;
                    }
                    while (tjread.Read())
                    {
                        if (first == 0)//只加载一次用户信息
                        {
                            loginuser.Name = tjread["YYRXM"].ToString();
                            loginuser.Sex = tjread["SEX"].ToString();
                            loginuser.Age = Convert.ToInt32(tjread["AGE"].ToString());
                            loginuser.tjCode = tjread["selfbh"].ToString();
                            loginuser.WdCode = tjread["RFID"].ToString();
                            loginuser.Vip = Convert.ToInt32(tjread["VIP"].ToString());
                            loginuser.TeamOrder = Convert.ToInt32(tjread["TeamOrder"].ToString().Trim());
                            loginuser.ItemSuitIDName = tjread["ItemSuitIDName"].ToString();
                            dxid.Append(tjread["DXID"].ToString() + "'"); first++; coutn++;
                        }
                        else
                        {
                            dxid.Append("or Externa='" + tjread["DXID"].ToString() + "'");//加载所有体检项目
                            coutn++;
                        }
                    }
                    tjread.Close();
                    tjcon.Close();
                    #region 记录操作日志

                    try
                    {
                        opreEnt.EmployeeID = Glod.emp.UserName;
                    }
                    catch (Exception)
                    {

                        opreEnt.EmployeeID = "";
                    }

                    opreEnt.UserID = loginuser.tjCode;
                    opreEnt.OperateTime = nowteam.GetNowTime();
                    opreEnt.IPAdress = opre.GetLocalIp();
                    #endregion
                    //插入项目组合检查状态
                    BllNowTeam.insertuser_item(tjcode, dxid.ToString(), "", "0");
                    List<Btpd.Model.Binding> binding = binddal.GetListDis(dxid.ToString() + " and IsEnable=0", 1);//获取
                    List<Btpd.Model.DepartmentsInfo> list = dal.GetModelList("ClassID='0'");
                    int listcount = list.Count;
                    int waitDeparts = binding.Count;
                    if (waitDeparts == 0)
                    {
                        return "无体检项目,请添加体检项目后再登记!";
                    }
                    StringBuilder tjdepart = new StringBuilder();
                    foreach (Btpd.Model.Binding depart in binding)
                    {
                        tjdepart.Append(depart.DepartmentsID + ",");
                        if (existmodel.Count == 0)
                        {

                        }
                        else
                        {
                            if (!existmodel[0].needDepart2.Contains(depart.DepartmentsID))
                            {
                                existmodel[0].NeedDepart += depart.DepartmentsID + ",";
                                existmodel[0].needDepart2 += depart.DepartmentsID + ",";

                            }
                        }
                    }
                    try
                    {
                        if (existmodel[0].NeedDepart == "")
                        {
                            return "体检结束";
                        }
                    }
                    catch (Exception)
                    {


                    }

                    if (existmodel.Count > 0)
                    {
                        guid = existmodel[0].UserID;
                    }
                    else
                    {
                        guid = Guid.NewGuid().ToString();
                    }

                    if (existmodel.Count == 0)
                    {
                        loginuser.NeedDepart = tjdepart.ToString();
                    }
                    else
                    {
                        loginuser.NeedDepart = tjdepart.ToString();
                    }
                    #endregion
                }
                else
                {
                    #region 同步基本信息

                    SqlConnection tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                    tjcon.Open();//连接体检数据库
                    SqlCommand tjcom = new SqlCommand("select SELFBH,RFID,VIP,YYRXM,SEX,AGE,DXID,TeamOrder,ItemSuitIDName from tj_requireinfo where selfbh='" + tjcode + "' or RFID='" + tjcode + "' and datediff(DAY,TJRQ ,GETDATE())=0 ", tjcon);
                    SqlDataReader tjread = tjcom.ExecuteReader();
                    int coutn = 0;
                    int first = 0;
                    if (!tjread.HasRows)
                    {
                        return "数据异常,检测不到该用户信息!";
                    }

                    if (userinfo.GetModelList(" tjCode='" + tjcode + "'").Count > 0)
                    {
                        isHaveUSer = true;
                    }
                    while (tjread.Read())
                    {
                        if (first == 0)//只加载一次用户信息
                        {
                            existmodel[0].Name = tjread["YYRXM"].ToString();
                            existmodel[0].Sex = tjread["SEX"].ToString();
                            existmodel[0].Age = Convert.ToInt32(tjread["AGE"].ToString());
                            existmodel[0].tjCode = tjread["selfbh"].ToString();
                            loginuser.WdCode = tjread["RFID"].ToString();
                            loginuser.Vip = Convert.ToInt32(tjread["VIP"].ToString());
                            existmodel[0].TeamOrder = Convert.ToInt32(tjread["TeamOrder"].ToString().Trim());
                            existmodel[0].ItemSuitIDName = tjread["ItemSuitIDName"].ToString();
                            first++;
                        }

                    }
                    tjread.Close();
                    tjcon.Close();
                    #endregion

                    #region 检查项目同步
                    try
                    {
                        bool isdisanfang = false;//true是从第三方获取的数据、false不是
                        StringBuilder strbitemid = new StringBuilder();
                        #region 如果数据库不是SQLSeVErSHI使用ODBC连接
                        SqlConnection tjcon2 = new SqlConnection(Btpd_base.PublicTool.SqlTj);
                        tjcon2.Open();//连接体检数据库
                        SqlCommand tjcom2 = new SqlCommand("select SELFBH,RFID,YYRXM,SEX,AGE,VIP,DXID,TeamOrder,Picture,ItemSuitIDName from tj_requireinfo where selfbh='" + tjcode.Trim() + "' ", tjcon2);
                        SqlDataReader tjread2 = tjcom2.ExecuteReader();
                        int coutn2 = 0;
                        int first2 = 0;
                        if (!tjread2.HasRows)
                        {
                            isdisanfang = true;
                        }
                        while (tjread2.Read())
                        {
                            strbitemid.Append(tjread2["DXID"].ToString() + ",");

                        }
                        tjread2.Close();
                        tjcon2.Close();

                        #endregion
                        if (isdisanfang == false)
                        {
                            BllNowTeam.deleteuser_item(tjcode, "", "");
                            string striteminfo = BllNowTeam.selectuser_item(tjcode);
                            for (int iss = 0; iss < strbitemid.ToString().Split(',').Length; iss++)
                            {
                                if (!striteminfo.Contains(strbitemid.ToString().Split(',')[iss]))
                                {
                                    List<Btpd.Model.Binding> lsbind = binddal.GetModelList(" Externa='" + strbitemid.ToString().Split(',')[iss] + "'");

                                    if (lsbind != null && lsbind.Count > 0)
                                    {
                                        BllNowTeam.insertuser_item2(tjcode, strbitemid.ToString().Split(',')[iss], lsbind[0].DepartmentsID, "0");
                                    }
                                }
                            }
                            string depneed = BllNowTeam.selectuser_itemlist(tjcode);

                            existmodel[0].NeedDepart = depneed + ",";
                            userinfo.Update(existmodel[0]);
                            if (depneed == "")
                            {
                                return "体检结束";
                            }


                        }
                        try
                        {
                            List<Btpd.Model.NowTeam> nowteamss = BllNowTeam.GetList(" userid='" + existmodel[0].UserID + "'");
                            foreach (Btpd.Model.NowTeam var in nowteamss)
                            {
                                if (var.DepartmentsID != "" && !existmodel[0].NeedDepart.Contains(BllDepart.GetModel(var.DepartmentsID).ClassID))
                                {
                                    var.DepartmentsID = "";
                                    BllNowTeam.Update(var);
                                    BTCS_pu.NewPD(loginuser, false, "");
                                }
                            }

                        }
                        catch (Exception)
                        {

                        }

                    }
                    catch (Exception)
                    {


                    }
                    #endregion
                }

                if (existmodel.Count == 0)
                {

                    loginuser.UserID = guid;
                    loginuser.needDepart2 = loginuser.NeedDepart;

                    bool b = userinfo.Add(loginuser);
                    Btpd.Model.NowTeam team = new Btpd.Model.NowTeam();
                    team.Userid = guid;
                    nowteam.Add(team);
                    user = userinfo.GetModel(guid);
                    Glod.IsPDStart = false;
                    opreEnt.OperateNote = "前台登记：调用排队接口273";
                    opre.insertOpre(opreEnt);
                    pdinfo = BTCS_pu.NewPD(loginuser, false, "");
                    Glod.IsPDStart = true;
                    team = nowteam.GetModelnew(team.Userid);
                    Btpd.Model.DepartmentsInfo DepEter = new Btpd.Model.DepartmentsInfo();
                    DepEter = BllDepart.GetModel(team.DepartmentsID);
                    string WaitTime = BLLEmp.GetDepartmentWaitTime(team.DepartmentsID, team.Teamtime);
                    if (DepEter.ClassID.Contains(PDCCKS) && Convert.ToInt16(WaitTime) > Convert.ToInt16(OutTime))
                    {
                        Glod.IsPDReg = false;
                        opreEnt.OperateNote = "前台登记：调用排队接口284，等待时间" + WaitTime + team.Teamtime;
                        opre.insertOpre(opreEnt);
                        pdinfo = BTCS_pu.NewPD(loginuser, true, "");
                        Glod.IsPDReg = true;
                    }

                }
                else
                {
                    loginuser = existmodel[0];
                    loginuser.UserID = existmodel[0].UserID;
                    loginuser.needDepart2 = existmodel[0].needDepart2;
                    loginuser.NeedDepart = existmodel[0].NeedDepart;

                    userinfo.Update(loginuser);
                    Btpd.BLL.NowTeam BLLNOW = new Btpd.BLL.NowTeam(Btpd_base.PublicTool.SqlConnection);
                    //List<Btpd.Model.NowTeam> teamszuot = BLLNOW.GetModelList("  UserID='" + loginuser.UserID + 
                    //    "' and time<'"+ _commonAppService.GetDateTimeNow().Now.ToString("yyyy-MM-dd")+" 00:00:02");
                    //foreach (Btpd.Model.NowTeam var in teamszuot)
                    //{
                    //    BLLNOW.Delete(var.ID);
                    //}
                    List<Btpd.Model.NowTeam> teams = BLLNOW.GetModelList("  UserID='" + loginuser.UserID + "'");
                    if (teams.Count > 0 && teams[0].DepartmentsID != "")
                    {
                        Btpd.Model.NowTeam team = teams[0];
                        team = nowteam.GetModelnew(team.Userid);
                        Btpd.Model.DepartmentsInfo dep = BllDepart.GetModel(team.DepartmentsID);
                        Btpd.Model.DepartmentsInfo DepEter = new Btpd.Model.DepartmentsInfo();
                        DepEter = BllDepart.GetModel(team.DepartmentsID);
                        if (DepEter == null)
                        {
                            MessageBox.Show("排队科室未登陆，请到智能排检系统--导诊台检查，科室登陆状态");
                        }
                        if ((dep == null || loginuser.NeedDepart.Contains(PDCCKS)) && teams.Count < 2)
                        {
                            team = nowteam.GetModelnew(team.Userid);
                            string WaitTime = BLLEmp.GetDepartmentWaitTime(team.DepartmentsID, team.Teamtime);
                            if (loginuser.NeedDepart.Contains(PDCCKS) && int.Parse(WaitTime) > 20)
                            {
                                Glod.IsPDReg = false;
                                opreEnt.OperateNote = "前台登记：调用排队接口319，等待时间" + WaitTime + team.Teamtime;
                                opre.insertOpre(opreEnt);
                                pdinfo = BTCS_pu.NewPD(loginuser, true, "");
                                Glod.IsPDReg = true;
                            }
                            else
                            {
                                pdinfo = "请到" + dep.Floor + "楼" + dep.Name + "等候";
                            }

                        }
                        else
                        {
                            pdinfo = "请到" + dep.Floor + "楼" + dep.Name + "等候";
                        }
                    }
                    else
                    {

                        Btpd.Model.NowTeam team = new Btpd.Model.NowTeam();
                        team = nowteam.GetModelnew(loginuser.UserID);
                        if (team == null)
                        {
                            team = new Btpd.Model.NowTeam();
                            team.Userid = loginuser.UserID;
                            nowteam.Add(team);
                            Glod.IsPDStart = false;
                            opreEnt.OperateNote = "前台登记：调用排队接口342，等待时间";
                            opre.insertOpre(opreEnt);
                            pdinfo = BTCS_pu.NewPD(loginuser, false, "");
                            Glod.IsPDStart = true;
                        }
                        if (team.DepartmentsID == "")
                        {
                            Glod.IsPDStart = false;
                            opreEnt.OperateNote = "前台登记：调用排队接口442，等待时间";
                            opre.insertOpre(opreEnt);
                            pdinfo = BTCS_pu.NewPD(loginuser, false, "");
                            Glod.IsPDStart = true;
                        }
                        team = nowteam.GetModelnew(team.Userid);
                        Btpd.Model.DepartmentsInfo DepEter = new Btpd.Model.DepartmentsInfo();
                        DepEter = BllDepart.GetModel(team.DepartmentsID);
                        string WaitTime = BLLEmp.GetDepartmentWaitTime(team.DepartmentsID, team.Teamtime);
                        if (DepEter == null)
                        {
                            MessageBox.Show("排队科室未登陆，请到智能排检系统--导诊台检查，科室登陆状态");
                            return "";
                        }
                        if (DepEter.ClassID.Contains(PDCCKS) && Convert.ToInt16(WaitTime) > Convert.ToInt16(OutTime))
                        {
                            Glod.IsPDReg = false;
                            opreEnt.OperateNote = "前台登记：调用排队接口354，等待时间" + WaitTime + team.Teamtime;
                            opre.insertOpre(opreEnt);
                            pdinfo = BTCS_pu.NewPD(loginuser, true, "");
                            Glod.IsPDReg = true;
                        }

                    }

                }
            }
            return pdinfo;
        }
        #endregion
        private void DateBirthday_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView4_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == colFree.FieldName)
            {
                if (e.Column.FieldName == colFree.FieldName)
                {
                    var data = gridView4.GetRowCellValue(e.ListSourceRowIndex, colIsFree);
                    if (data != null)
                    {
                        if ((bool)data)
                            e.DisplayText = "是";
                        else
                            e.DisplayText = "否";
                    }
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //var strCSPD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 11)?.Remarks;

                //字段判断拍照采集头像
              
                

                    #region MyRegion
                    var cameraHelper = new CusCamera();

                    if (cameraHelper.ShowDialog() == DialogResult.OK)
                    {
                        Image cuimgae = cameraHelper.CameraImage;
                        if (cuimgae != null)
                        {
                            pictureCusReg.Image = cuimgae;
                        }
                    }
                    #endregion
                
            }
            catch (Exception ex)
            {
                alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (xtraTabControl1.SelectedTabPageIndex == 1)
            //{
            txtDays.Focus();
            //}
        }

        private void txtVisitCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtVisitCard.Text))
            {

                var data = new QueryCustomerRegDto();
                //添加人员
                if (IsReging && butAddReg.Enabled == false)
                {
                    labelClientInfo.Text = "当前添加体检人状态";
                    labelClientInfo.Refresh();
                    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                    if (HISjk == "1")
                    {
                        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                        if (HISName == "北仑" || HISName == "南京飓风" || HISName == "获取基本信息")
                        {
                            //MessageBox.Show("进入接口");

                            InCarNumDto input = new InCarNumDto();
                            input.CardNum = txtVisitCard.Text;
                            input.HISName = HISName;
                            var cusinfo = customerSvr.geHisvard(input);
                            data.Customer = new QueryCustomerDto();
                            if (cusinfo.Name != null)
                            {
                                //MessageBox.Show("获取到数据");
                                data.Customer.Address = cusinfo.Address ?? "";
                                data.Customer.Age = cusinfo.Age;
                                data.Customer.Birthday = cusinfo.Birthday;
                                data.Customer.HospitalNum = cusinfo.HospitalNum ?? "";
                                data.Customer.IDCardNo = cusinfo.IDCardNo ?? "";
                                data.Customer.MarriageStatus = cusinfo.MarriageStatus;
                                data.Customer.MedicalCard = cusinfo.MedicalCard ?? "";
                                data.Customer.Mobile = cusinfo.Mobile ?? "";
                                data.Customer.Name = cusinfo.Name ?? "";
                                data.Customer.SectionNum = cusinfo.SectionNum ?? "";
                                data.Customer.Sex = cusinfo.Sex;
                                data.Customer.VisitCard = cusinfo.SectionNum ?? "";
                                data.HisPatientId = cusinfo.BLH;
                                data.HisSectionNum = cusinfo.SectionNum ?? "";
                                if (HISName == "获取基本信息")
                                { data.Customer.VisitCard = cusinfo.VisitCard ?? ""; }
                                if (HISName == "南京飓风")
                                {
                                    //data.Customer.ArchivesNum = cusinfo.BLH;
                                    // data.PhysicalType =int.Parse( cusinfo.ut_id);
                                    data.NucleicAcidType = int.Parse(cusinfo.ut_id);
                                }
                            }

                        }
                        else if (HISName.Contains( "江苏鑫亿"))
                        {
                            labelClientInfo.Text = "开始获取HIS体检人信息";
                            labelClientInfo.Refresh();
                            WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                            string ss = txtVisitCard.Text;
                            if (HISName.Contains("四院"))
                            { ss = ss + "四院"; }
                            var cusinfobody = neInterface.GetCustomer(ss);
                            if (cusinfobody != null && cusinfobody.Body != null && cusinfobody.Body.Count > 0)
                            {
                                labelClientInfo.Text = "读取HIS体检人信息成功";
                                labelClientInfo.Refresh();
                                var cusinfo = cusinfobody.Body.FirstOrDefault();
                                if (cusinfobody.Body.Count>1)
                                {
                                    HisSelect frm = new HisSelect();
                                    frm.cusList = cusinfobody.Body;
                                    if (frm.ShowDialog() == DialogResult.OK && frm.NowCusInfo != null)
                                    {
                                        cusinfo = frm.NowCusInfo;
                                    }

                                }

                              
                                data.Customer = new QueryCustomerDto();
                                if (cusinfo != null)
                                {
                                    //MessageBox.Show("获取到数据");
                                    data.Customer.Address = cusinfo.Address ?? "";
                                    //data.Customer.Age = cusinfo;
                                    if (DateTime.TryParse(cusinfo.Birthday, out DateTime bir))
                                    {
                                        data.Customer.Birthday = bir;
                                        #region 计算年龄
                                        DateTime nowTime = _commonAppService.GetDateTimeNow().Now;
                                        if (nowTime.Year < DateBirthday.DateTime.Year)
                                        {
                                            return;
                                        }
                                        int age = 0;
                                        if (nowTime.Month > DateBirthday.DateTime.Month)
                                            age = nowTime.Year - bir.Year;
                                        else if (nowTime.Month == bir.Month && nowTime.Day >= bir.Day)
                                            age = nowTime.Year - bir.Year;
                                        else
                                            age = nowTime.Year - bir.Year - 1;
                                        data.Customer.Age = age;
                                        #endregion
                                    }
                                    data.Customer.VisitCard = txtVisitCard.Text;
                                    // data.Customer.ArchivesNum = cusinfo.PatientId ?? "";
                                    data.Customer.SectionNum = cusinfo.PatientId ?? "";
                                    data.Customer.Name = cusinfo.PatientName ?? "";
                                    var sexBM = 1;
                                    if (cusinfo.PatientSex.Contains("女") || cusinfo.PatientSex.Contains("2"))
                                    {
                                        sexBM = 2;
                                    }
                                    data.Customer.Sex = sexBM;
                                    data.Customer.IDCardNo = cusinfo.IDCardNO;
                                    data.Customer.Address = cusinfo.Address;
                                    data.Customer.Nation = cusinfo.Nation;
                                    data.Customer.CardNumber = cusinfo.CardNo;
                                    data.Customer.Mobile = cusinfo.PhoneNum;
                                    if (!string.IsNullOrEmpty(cusinfo.ChargeType)
                                        && int.TryParse(cusinfo.ChargeType, out int ChargeType))
                                    {
                                        data.NucleicAcidType = ChargeType;
                                    }
                                    if (cusinfo.YB_FLAG == "1")
                                    {
                                        data.Remarks = "医保";
                                    }
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("HIS系统无此人信息");
                            }

                        }
                        labelClientInfo.Text = "加载体检人信息";
                        labelClientInfo.Refresh();
                        LoadCustomerData(data);

                    }

                }
                else
                {
                    labelClientInfo.Text = "当前非添加体检人状态";
                    labelClientInfo.Refresh();
                    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;


                    if (HISjk == "1" && HISName.Contains("江苏鑫亿" ))
                    {
                        labelClientInfo.Text = "开始获取HIS体检人信息";
                        labelClientInfo.Refresh();
                        string ss = txtVisitCard.Text;
                        if (HISName.Contains("四院"))
                        { ss = ss + "四院"; }
                        WindowsFormsApp1.XYNeInterface neInterface = new WindowsFormsApp1.XYNeInterface();
                        var cusinfobody = neInterface.GetCustomer(ss);
                        if (cusinfobody.Body.Count > 0)
                        {
                            var cusinfo = cusinfobody.Body.FirstOrDefault();

                            if (cusinfo != null)
                            {
                                labelClientInfo.Text = "加载HIS体检人基本信息";
                                labelClientInfo.Refresh();
                                var sexBM = 1;
                                if (cusinfo.PatientSex.Contains("女") || cusinfo.PatientSex.Contains("2"))
                                {
                                    sexBM = 2;
                                }
                              var VisitCard= txtVisitCard.Text;
                                butAddReg_Click(sender, e);
                                //添加
                                //MessageBox.Show("获取到数据");
                                if (curCustomRegInfo != null && curCustomRegInfo.Customer != null)
                                {
                                    curCustomRegInfo.Customer.Address = cusinfo.Address ?? "";
                                    //data.Customer.Age = cusinfo;
                                    if (DateTime.TryParse(cusinfo.Birthday, out DateTime bir))
                                    {
                                        curCustomRegInfo.Customer.Birthday = bir;
                                    }
                                    curCustomRegInfo.Customer.VisitCard = VisitCard;
                                    // data.Customer.ArchivesNum = cusinfo.PatientId ?? "";
                                    curCustomRegInfo.Customer.SectionNum = cusinfo.PatientId ?? "";
                                    curCustomRegInfo.Customer.Name = cusinfo.PatientName ?? "";

                                    curCustomRegInfo.Customer.Sex = sexBM;
                                    curCustomRegInfo.Customer.IDCardNo = cusinfo.IDCardNO;
                                    curCustomRegInfo.Customer.Address = cusinfo.Address;
                                    curCustomRegInfo.Customer.Nation = cusinfo.Nation;
                                    curCustomRegInfo.Customer.CardNumber = cusinfo.CardNo;
                                }
                                txtVisitCard.Text = VisitCard;
                                txtName.EditValue = cusinfo.PatientName ?? "";
                                gridLookUpSex.EditValue = sexBM;
                                if (!string.IsNullOrEmpty(cusinfo.IDCardNO))
                                {
                                    txtIDCardNo.EditValue = cusinfo.IDCardNO;
                                }
                                txtNation.EditValue = cusinfo.Nation;
                                txtCardNumber.EditValue = cusinfo.CardNo;
                                if (DateTime.TryParse(cusinfo.Birthday, out DateTime birday))
                                {
                                    DateBirthday.EditValue = birday;
                                }
                                if (cusinfo.YB_FLAG == "1")
                                {
                                    data.Remarks = "医保";
                                }

                            }
                        }

                    }
                    else
                    {
                        //MessageBox.Show("未进入接口");

                        if (HISjk == "1" && HISName == "获取基本信息")
                        {


                            InCarNumDto input = new InCarNumDto();
                            input.CardNum = txtVisitCard.Text;
                            input.HISName = HISName;
                            var cusinfo = customerSvr.geHisvard(input);
                            if (cusinfo != null && !string.IsNullOrEmpty(cusinfo.VisitCard))
                            {
                                data = QueryCustomerRegData(new SearchCustomerDto { WorkNumber = cusinfo.VisitCard, NotCheckState = (int)PhysicalEState.Complete });

                                if (data != null)
                                {
                                    if (data.Customer == null)
                                    {
                                        data.Customer = new QueryCustomerDto();
                                    }
                                    data.Customer.VisitCard = cusinfo.VisitCard;
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show("HIS系统无此人信息");
                                return;
                            }

                        }
                        else
                        {
                            data = QueryCustomerRegData(new SearchCustomerDto { VisitCard = txtVisitCard.EditValue.ToString(), NotCheckState = (int)PhysicalEState.Complete });
                        }
                            if (data == null)
                        {
                            data = new QueryCustomerRegDto();
                        }
                        if (data.Id == Guid.Empty || data.Id == null || (data.Customer.VisitCard != txtVisitCard.EditValue.ToString() 
                            && HISName != "获取基本信息"))
                        {

                            HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                            if (HISjk == "1")
                            {
                                // var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                                if (HISName == "北仑" || HISName == "南京飓风")
                                {
                                    //MessageBox.Show("进入接口");

                                    InCarNumDto input = new InCarNumDto();
                                    input.CardNum = txtVisitCard.Text;
                                    input.HISName = HISName;
                                    var cusinfo = customerSvr.geHisvard(input);

                                    if (cusinfo.Name != null)
                                    {
                                        butAddReg_Click(sender, e);
                                        //添加
                                        data = new QueryCustomerRegDto();
                                        data.Customer = new QueryCustomerDto();
                                        //MessageBox.Show("获取到数据");
                                        data.Customer.Address = cusinfo.Address ?? "";
                                        data.Customer.Age = cusinfo.Age;
                                        data.Customer.Birthday = cusinfo.Birthday;
                                        data.Customer.HospitalNum = cusinfo.HospitalNum ?? "";
                                        data.Customer.IDCardNo = cusinfo.IDCardNo ?? "";
                                        data.Customer.MarriageStatus = cusinfo.MarriageStatus;
                                        data.Customer.MedicalCard = cusinfo.MedicalCard ?? "";
                                        data.Customer.Mobile = cusinfo.Mobile ?? "";
                                        data.Customer.Name = cusinfo.Name ?? "";
                                        data.Customer.SectionNum = cusinfo.SectionNum ?? "";
                                        data.Customer.Sex = cusinfo.Sex;
                                        data.Customer.VisitCard = cusinfo.SectionNum ?? "";
                                        data.HisPatientId = cusinfo.BLH;
                                        data.HisSectionNum = cusinfo.SectionNum ?? "";

                                        if (HISName == "南京飓风")
                                        {
                                            //data.Customer.ArchivesNum = cusinfo.BLH;
                                            // data.PhysicalType =int.Parse( cusinfo.ut_id);
                                            data.NucleicAcidType = int.Parse(cusinfo.ut_id);
                                        }
                                        // return;
                                    }

                                }
                                else
                                {
                                    XtraMessageBox.Show("HIS系统无此人信息");
                                }
                            }

                        }
                        else
                        {

                            labelClientInfo.Text = "体检中存在该门诊号的预约记录";
                            labelClientInfo.Refresh();
                        }
                        LoadCustomerData(data);
                    }


                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureCus.Image != null)
                {
                    ShowPic showPic = new ShowPic(txtCustoemrCode.Text.ToString(), txtName.Text.ToString(), gridLookUpSex.Text.ToString(), txtAge.Text.ToString(),
                        pictureCus.Image);
                    showPic.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void txtVisitCard_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            CustomerReg_KeyDown(sender, new KeyEventArgs(Keys.F3));
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (curCustomRegInfo != null)
            {
                if (curCustomRegInfo.Id != Guid.Empty)
                {
                    EntityDto<Guid> cusName = new EntityDto<Guid>();
                    cusName.Id = curCustomRegInfo.Id;
                    CustomerRegCostDto customerReg = _chargeAppService.GetsfState(cusName);
                    if (customerReg.CostState != (int)PayerCatType.NoCharge)
                    {
                        MessageBox.Show("该体检人已收费不能删除！");
                        return;
                    }
                    if (curCustomRegInfo.RegisterState == (int)RegisterState.Yes)
                    {
                        ShowMessageBoxInformation(string.Format("{0}已登记，无法删除！", curCustomRegInfo.Customer.Name));
                        return;
                    }
                    if (curCustomRegInfo.CheckSate != (int)ExaminationState.Alr)
                    {
                        ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", curCustomRegInfo.Customer.Name));
                        return;
                    }
                    DialogResult dr = XtraMessageBox.Show("是否删除该体检人？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        curCustomRegInfo.RegisterState = (int)RegisterState.No;
                        try
                        {
                            //操作数据库
                            AutoLoading(() =>
                            {
                                List<EntityDto<Guid>> listGuid = new List<EntityDto<Guid>>();
                                listGuid.Add(cusName
                                    );
                                _clientReg.DelCustomerReg(listGuid);
                            });
                            //日志
                            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                            createOpLogDto.LogBM = curCustomRegInfo.CustomerBM;
                            createOpLogDto.LogName = curCustomRegInfo.Customer.Name;
                            createOpLogDto.LogText = "删除人员";
                            createOpLogDto.LogDetail = "";
                            createOpLogDto.LogType = (int)LogsTypes.ClientId;
                            _commonAppService.SaveOpLog(createOpLogDto);
                            curCustomRegInfo = null;
                            LoadCustomerData(curCustomRegInfo);
                            IsReging = false;
                            return;
                        }
                        catch (UserFriendlyException ex)
                        {
                            curCustomRegInfo.RegisterState = (int)RegisterState.Yes;
                            ShowMessageBox(ex);
                            return;
                        }
                    }
                    else
                        return;

                }
            }
            ShowMessageBoxInformation("当前数据未保存不能取消删除！");
        }

        private void gridControlDengJiList_Click(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEditClientType_EditValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(lookUpEditClientType.EditValue?.ToString()))
            {
                var input = new SearchItemSuitDto() { ItemSuitType = (int)ItemSuitType.Suit, ExaminationType = (int)lookUpEditClientType.EditValue };

                cuslookItemSuit.Properties.DataSource = QuerySuits(input, true);
            }

            if ((Variables.ISZYB == "1" && lookUpEditClientType.EditValue != null && (lookUpEditClientType.Text.ToString().Contains("职业") ||
                lookUpEditClientType.Text.ToString().Contains("放射"))) || Variables.ISZYB == "2")
            {
                //检查类型
                ChargeBM chargeBM = new ChargeBM();

                chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
                var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtCheckType.Properties.DataSource = lis1;
                //车间
                chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                var lis2 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtWorkName.Properties.DataSource = lis2;
                //工种
                chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
                var lis3 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtTypeWork.Properties.DataSource = lis3;
                groupzyb.Visibility = LayoutVisibility.Always;
                layjjz.Visibility = LayoutVisibility.Never;
                if (lookUpEditClientType.Text.ToString().Contains("放射"))
                {

                    labRiskS.Visibility = LayoutVisibility.Never;
                    layoutControlItem18.Visibility = LayoutVisibility.Never;
                }
                else
                {
                    labRiskS.Visibility = LayoutVisibility.Always;
                    layoutControlItem18.Visibility = LayoutVisibility.Always;
                }
            }
            else
            {
                groupzyb.Visibility = LayoutVisibility.Never;

                if (lookUpEditClientType.EditValue != null && (lookUpEditClientType.Text.ToString().Contains("食品")))
                {
                    //txtJKZ.Text = iIDNumberAppService.CreateJKZBM();
                    layjjz.Visibility = LayoutVisibility.Always;
                }
                else if (lookUpEditClientType.EditValue != null && (lookUpEditClientType.Text.ToString().Contains("从业")
                    || lookUpEditClientType.Text.ToString().Contains("健康证")))
                {
                    //txtJKZ.Text = iIDNumberAppService.CreateJKZBM();
                    layjjz.Visibility = LayoutVisibility.Always;

                    if (curCustomRegInfo == null ||
              (curCustomRegInfo?.RegisterState != (int)RegisterState.Yes &&
              (curCustomRegInfo?.CustomerItemGroup == null || curCustomRegInfo?.CustomerItemGroup.Count == 0)))
                    {

                        if (lookUpEditClientType.EditValue != null && !string.IsNullOrEmpty(gridLookUpSex.EditValue?.ToString()) &&
                            (lookUpEditClientType.Text.ToString().Contains("从业")
                            || lookUpEditClientType.Text.ToString().Contains("健康证")))
                        {


                            var input = new SearchItemSuitDto() { ItemSuitType = (int)ItemSuitType.Suit, ExaminationType = (int)lookUpEditClientType.EditValue };
                            var suitlist = QuerySuits(input);
                            if (suitlist.Count == 1)
                            {
                                cuslookItemSuit.EditValue = suitlist[0].Id;
                            }
                        }
                    }

                }
                else
                { layjjz.Visibility = LayoutVisibility.Never; }
            }
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            frmSelectHazard frmSelectHazard = new frmSelectHazard();
            if (txtRiskS.Tag != null && txtRiskS.Text != "")
            {
                var riskls = (List<ShowOccHazardFactorDto>)txtRiskS.Tag;
                frmSelectHazard.outOccHazardFactors = riskls;


            }
            if (frmSelectHazard.ShowDialog() == DialogResult.OK)
            {
                txtRiskS.Tag = null;
                var Hazard = frmSelectHazard.outOccHazardFactors;
                txtRiskS.Tag = Hazard;
                txtRiskS.Text = string.Join("，", Hazard.Select(o => o.Text).ToList()).TrimEnd('，');
            }
        }

        private void txtCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                if (string.IsNullOrWhiteSpace(gridLookUpSex.EditValue?.ToString()))
                {

                    dxErrorProvider.SetError(gridLookUpSex, string.Format(Variables.MandatoryTips, "性别"));
                    gridLookUpSex.Focus();
                    XtraMessageBox.Show("请输入性别");
                    return;
                }
                if (txtAge.EditValue == null)
                {

                    dxErrorProvider.SetError(txtAge, string.Format(Variables.MandatoryTips, "年龄"));
                    txtAge.Focus();
                    XtraMessageBox.Show("请输入年龄");
                    return;
                }

                CusCardDto cusCardDto = new CusCardDto();
                cusCardDto.Age = Convert.ToInt32(txtAge.EditValue);
                cusCardDto.CardNo = txtCardNumber.Text;
                cusCardDto.Sex = Convert.ToInt32(gridLookUpSex.EditValue);
                var carinfo = customerSvr.getSuitbyCardNum(cusCardDto);
                if (carinfo.Code == 0)
                {
                    XtraMessageBox.Show(carinfo.Err);
                }
                else
                {
                    labelControl1.Text = carinfo.CardCategory + carinfo.CardName + ",折扣:" + carinfo.DiscountRate;
                    if (carinfo.CardCategory.Contains("单位"))
                    {
                        txtClientRegID.EditValue = carinfo.ClientRegId;
                        txtTeamID.EditValue = carinfo.ClientTeamInfoId;
                    }
                    else
                    {
                        if (carinfo.ItemSuits.Count == 1)
                        {
                            txtIntroducer.EditValue = carinfo.SellName;

                            cuslookItemSuit.EditValue = carinfo.ItemSuits[0].Id;
                            GridLookUpEdit ss = new GridLookUpEdit();
                            ss = cuslookItemSuit;
                            cuslookItemSuit_EditValueChanged(ss, e);
                            //cuslookItemSuit.per
                            //btnSave.PerformClick();
                        }
                        else
                        {
                            SelectCardSuit selectCardSuit = new SelectCardSuit(carinfo.ItemSuits);
                            if (selectCardSuit.ShowDialog() == DialogResult.OK)
                            {
                                var SuitInfo = selectCardSuit.OutSuit;
                                cuslookItemSuit.EditValue = SuitInfo.Id;
                                GridLookUpEdit ss = new GridLookUpEdit();
                                ss = cuslookItemSuit;
                                cuslookItemSuit_EditValueChanged(ss, e);
                            }
                            //foreach (var data in carinfo.ItemSuits)
                            //{
                            //    if (gridLookUpSex.EditValue?.ToString() == "1")
                            //    {
                            //        foreach (var Mendata in carinfo.ItemSuits)
                            //        {
                            //            if (Mendata.ItemSuitName.Contains("男"))
                            //            {
                            //                cuslookItemSuit.EditValue = Mendata.Id;
                            //                break;
                            //            }
                            //        }
                            //    }

                            //     if (gridLookUpSex.EditValue?.ToString() == "2")
                            //    {
                            //        foreach (var womendata in carinfo.ItemSuits)
                            //        {
                            //            if (womendata.ItemSuitName.Contains("女"))
                            //            {
                            //                cuslookItemSuit.EditValue = womendata.Id;
                            //                break;
                            //            }
                            //        }


                            //    }

                            //    if (gridLookUpSex.EditValue?.ToString() == "9")
                            //    {
                            //        if (selectCardSuit.ShowDialog() == DialogResult.OK)
                            //        {
                            //            var SuitInfo = selectCardSuit.OutSuit;
                            //            cuslookItemSuit.EditValue = SuitInfo.Id;

                            //        }

                            //    }
                            //else if(cuslookItemSuit.EditValue==null)
                            //{
                            //    if (selectCardSuit.ShowDialog() == DialogResult.OK)
                            //    {
                            //        var SuitInfo = selectCardSuit.OutSuit;
                            //        cuslookItemSuit.EditValue = SuitInfo.Id;
                            //    }

                            //}
                            //}
                        }
                    }
                }
            }
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            //    if (HISjk == "1")
            //    {
            //        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
            //        if (HISName == "北仑" || HISName == "南京飓风")
            //        {
            //            //MessageBox.Show("进入接口");

            //            InCarNumDto input = new InCarNumDto();
            //            input.CardNum = txtVisitCard.Text;
            //            input.HISName = HISName;
            //            var cusinfo = customerSvr.geHisvard(input);
            //            if (cusinfo.Name != null)
            //            {

            //                if (HISName == "南京飓风" && comboBoxEdit1.EditValue != null && comboBoxEdit1.EditValue.ToString() != cusinfo.ut_id.ToString())
            //                {
            //                    MessageBox.Show("与挂号科室类别不符合！");
            //                    //lookUpEditClientType.EditValue = int.Parse(cusinfo.ut_id);

            //                }
            //            }

            //        }
            //    }
        }
        private void getCusInfo(string IDcard)
        {
            if (!string.IsNullOrEmpty(IDcard))
            {
                SearchOldRegDto searchOldRegDto = new SearchOldRegDto();
                searchOldRegDto.IDCardNo = IDcard;
                var cusinfo = customerSvr.GetCusInfoByIDCard(searchOldRegDto);
                if (cusinfo != null && !string.IsNullOrEmpty(cusinfo.IDCardNo))
                {
                    txtMobile.Text = cusinfo.Mobile;
                }
                if (cusinfo != null && !string.IsNullOrEmpty(cusinfo.IDCardNo) &&
                    string.IsNullOrEmpty(txtDepartment.EditValue?.ToString()))
                {
                    txtDepartment.EditValue = cusinfo.Department;
                }
            }


        }

        private void txtPersonnelCategory_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;


            }

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //frmZWcs frmZWcs = new frmZWcs();
            //frmZWcs.ShowDialog();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

            //调用程序：
            var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
            }
            pictureCus.Image.Save(url);
            string args = url;
            var path = AppDomain.CurrentDomain.BaseDirectory + "人证核验";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.Start();

            while (!KHMsg.HasExited) { } //如果exe还没关闭，则等待
            if (KHMsg.ExitCode != 1)
            {
                MessageBox.Show("认证核验失败！");
            }


        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {

                if (curCustomRegInfo != null)
                {
                    using (var frm = new ShowQuestionnaireEditor(curCustomRegInfo.CustomerBM))
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

        private void searchOrderNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(searchOrderNum.Text))
            {
                var data = QueryCustomerRegData(new SearchCustomerDto { OrderNum = searchOrderNum.EditValue?.ToString() });
                LoadCustomerData(data);
            }
        }

        private void lookUpEditCustomerType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void gridView4_ColumnFilterChanged(object sender, EventArgs e)
        {

        }

        private void gridView4_CustomColumnDisplayText_1(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

        }

        private void textEditRegRemark_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditRegRemark.Text.Contains("医保"))
            {
                textEditRegRemark.ForeColor = Color.Red;

            }
            else
            {
                textEditRegRemark.ForeColor = Color.Black;
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {

            try
            {
                frmsxqm frmsxqm = new frmsxqm();
                if (frmsxqm.ShowDialog() == DialogResult.OK)
                {
                    if (frmsxqm.imagesxqm != null)
                    {
                        //picqm.Image = frmsxqm.imagesxqm;
                        //Image image = picqm.Image;
                        var PicAddress = Convert.ToString(frmsxqm.imagesxqms);
                        pictureqm.Image = Image.FromFile(PicAddress);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void pictureqm_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (pictureqm.Image != null)
                {
                    ShowPic showPic = new ShowPic(txtCustoemrCode.Text.ToString(), txtName.Text.ToString(), gridLookUpSex.Text.ToString(), txtAge.Text.ToString(),
                        pictureqm.Image);
                    showPic.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
        }

        private void pictureCusReg_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (pictureCusReg.Image != null)
                {
                    ShowPic showPic = new ShowPic(txtCustoemrCode.Text.ToString(), txtName.Text.ToString(), gridLookUpSex.Text.ToString(), txtAge.Text.ToString(),
                        pictureCusReg.Image);
                    showPic.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                alertInfo.Show(this, "提示!", ex.Message.ToString());

            }
        }

        private void CustomerReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillProcess("TSMess");
        }

        private void gridControlgroups_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (layRS.Visibility == LayoutVisibility.Always)
            {
                layRS.Visibility = LayoutVisibility.Never;
                //laycus.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layRS.Visibility = LayoutVisibility.Always;
                //laycus.Visibility = LayoutVisibility.Never;
            }
        }

        private void layRS_DoubleClick(object sender, EventArgs e)
        {
            if (layRS.Visibility == LayoutVisibility.Always)
            {
                layRS.Visibility = LayoutVisibility.Never;
                //laycus.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layRS.Visibility = LayoutVisibility.Always;
                //laycus.Visibility = LayoutVisibility.Never;
            }
        }

        private void labelControl3_DoubleClick(object sender, EventArgs e)
        {
            if (layRS.Visibility == LayoutVisibility.Always)
            {
                layRS.Visibility = LayoutVisibility.Never;
                //laycus.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layRS.Visibility = LayoutVisibility.Always;
                //laycus.Visibility = LayoutVisibility.Never;
            }
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            if (curCustomRegInfo != null && curCustomRegInfo.Customer != null &&
                !string.IsNullOrEmpty(curCustomRegInfo.CustomerBM))
            {
              
                var path = AppDomain.CurrentDomain.BaseDirectory + "指纹识别";
                string filename = path+ "\\WindowsFormsApp1.exe";



                Process p = new Process();
                //  p.StartInfo.Arguments = "read";//读取体检号
               // p.StartInfo.Arguments = "GetImage0001";///根据体检号 获取图像显示在调用程序的窗体上
                 p.StartInfo.Arguments = "write"+ curCustomRegInfo.CustomerBM;///入体检号 0001是体检号
                p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                p.StartInfo.UseShellExecute = false;

                p.StartInfo.RedirectStandardOutput = true;

                p.StartInfo.FileName = filename;

                p.StartInfo.CreateNoWindow = false;
                p.Start();

                p.WaitForExit();

                string str = p.StandardOutput.ReadToEnd();
                str = str.Replace("load libFpDat_64.dll succ", "").Trim();//反馈的内容 不知道为什么多了load libFpDat_64.dll succ 所以 Replace掉就是正常返回的内容了

                //Console.WriteLine(str);
                //Console.ReadLine();

            }
            else
            {
                MessageBox.Show("当前无体检人信息无法保存指纹");
            }
            //Image img = Image.FromFile(filename);
            //if (img != null)
            //{
            //    this.BackgroundImage = img;

            //}

        }
        private void Zwsb_ShowMessage(string text)
        {
            this.Invoke(new Action(() =>
            {
                labelClientInfo.Text = text;
            }));

        }

        private void comboBoxEdit1_EditValueChanging(object sender, ChangingEventArgs e)
        {
            #region MyRegion
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;

                if (HISName.Contains("江苏鑫亿四院") && isok==true)
                {

                    if (string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()) &&
                        !string.IsNullOrEmpty(txtCustoemrCode.Text))
                    {
                        MessageBox.Show("个人体检不能修改挂号科室！");
                        e.Cancel = true;
                    }
                }
            }
            isok = false;
            #endregion
        }
        bool isok = false;
        private void comboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            isok = true;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            QueryRegNumbers();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            
                //这里在dll程序中调用exe，路径是获取dll所在路径
                var path = AppDomain.CurrentDomain.BaseDirectory + "人证核验";
                //使用进程
                Process myProcess = new Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                //传参，参数以空格分隔，如果某个参数为空，可以传入“”

                myProcess.Start();//启动
                myProcess.WaitForExit(60000);//等待exe程序处理完，超时1分钟
                //myProcess.WaitForExit();
                //while (!myProcess.HasExited)
                //{
                //  myProcess.StandardInput.Close();   //运行完毕关闭控制台输入
                string xmldata = myProcess.StandardOutput.ReadToEnd();//读取exe中内存流数据

                 
                    //自己实现的序列化
                    var ss = xmldata.Replace("\r\n", "$");
                    var pic = ss.Split('$').ToList();
                    string picnow = pic.FirstOrDefault(p => p.Contains("jpg"));

                    if (!string.IsNullOrEmpty(picnow))
                    {

                        System.Net.WebRequest webreq = System.Net.WebRequest.Create(picnow);
                        System.Net.WebResponse webres = webreq.GetResponse();
                        using (System.IO.Stream stream = webres.GetResponseStream())
                        {
                            Image image = Image.FromStream(stream);
                            pictureCusReg.Image = image;
                        }
                    }
                
                //}
            
        }
    }
}
