using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Data;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmSeleteItemGroup : UserBaseForm
    {
        public enum AlertFormLocation
        {
            /// <summary>
            /// An alert window appears at the top left corner of the screen.
            /// </summary>
            TopLeft = 0,

            /// <summary>
            /// An alert window appears at the top right corner of the screen.
            /// </summary>
            TopRight = 1,

            /// <summary>
            /// An alert window appears at the bottom left corner of the screen.
            /// </summary>
            BottomLeft = 2,

            /// <summary>
            /// An alert window appears at the bottom right corner of the screen.
            /// </summary>
            BottomRight = 3
        }

        private readonly IItemGroupAppService _itemGroupAppService;

        private readonly IBarSettingAppService _barSettingAppService;

        private readonly IChargeAppService _chargeAppService;

        private readonly IClientRegAppService _clientRegAppService;

        private readonly IOccTargetDiseaseAppService _occTargetDiseaseAppService;

        /// <summary>
        /// 当前选择分组  用于根据性别、年龄等过虑可选组合  团体预约界面调用
        /// </summary>
        public CreateClientTeamInfoesDto ClientTeamInfoesViewDto;

        /// <summary>
        /// 当前选择组合 登记界面调用 同时用于返回
        /// </summary>
        public List<TjlCustomerItemGroupDto> CurSelectItemGroup;

        /// <summary>
        /// 当前体检人  用于根据性别、年龄等过虑可选组合 登记界面调用
        /// </summary>
        public TjlCustomerDto CustomerDto;

        /// <summary>
        /// 当前体检预约人  用于根据性别、年龄等过虑可选组合 登记界面调用
        /// </summary>
        public QueryCustomerRegDto CustomerRegDto;

        /// <summary>
        /// 是否已登记 个人调用该窗体必填 true为已登记false为未登记
        /// </summary>
        public bool IsCheckSate;

        /// <summary>
        /// 当前是否个人体检 调用该窗体必填 true为个人false为团体
        /// </summary>
        public bool IsPersonal;

        public List<ClientTeamRegitemViewDto> LsisaddclientTeamRegitemViewDtos;

        //所有条码设置组合
        private List<BarItemDto> _lstbarItemDtos;

        /// <summary>
        /// 记录 团体 在这个界面增加的组合，如果为新增加的，减项直接减去
        /// </summary>
        private List<ClientTeamRegitemViewDto> lstClientTeamRegitemViewDto;

        /// <summary>
        /// 当前选择分组项目    团体预约界面调用 同时用于返回
        /// </summary>
        public List<ClientTeamRegitemViewDto> lstclientTeamRegitemViewDtos;

        /// <summary>
        /// 记录 个人 在这个界面增加的组合，如果为新增加的，减项直接减去
        /// </summary>
        private List<TjlCustomerItemGroupDto> lstselectItemGroup;

        //所有组合
        private List<SimpleItemGroupDto> lstsimpleItemGroupDtos;
        //职业健康可选项目
        private List<SimpleItemGroupDto> MayItemGrouplist;
        

        public FrmSeleteItemGroup()
        {
            _itemGroupAppService = new ItemGroupAppService();
            _chargeAppService = new ChargeAppService();
            _barSettingAppService = new BarSettingAppService();
            _clientRegAppService = new ClientRegAppService();
             _occTargetDiseaseAppService=new OccTargetDiseaseAppService();
            //grdvSelectItemGroup.Columns[PayerCat.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //grdvSelectItemGroup.Columns[PayerCat.FieldName].DisplayFormat.Format = new CustomFormatter(PayerCatTypeHelper.PayerCatTypeHelperFormatter);
            //grdvSelectItemGroup.Columns[IsAddMinus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //grdvSelectItemGroup.Columns[IsAddMinus.FieldName].DisplayFormat.Format = new CustomFormatter(AddMinusTypeHelper.AddMinusTypeHelperFormatter);
            InitializeComponent();
        }

        /// <summary>
        /// 个人登记界面调用
        /// </summary>
        /// <param name="curSelectItemGroup">当前选择组合</param>
        /// <param name="customerDto">当前体检人 用于根据性别、年龄等过虑可选组合</param>
        /// <param name="customerRegDto"></param>
        /// <param name="isPersonal"> 当前是否个人体检  true为个人false为团体</param>
        /// <param name="isCheckSate">是否已登记  true为已登记false为未登记</param>
        public FrmSeleteItemGroup(List<TjlCustomerItemGroupDto> curSelectItemGroup,
            TjlCustomerDto customerDto, QueryCustomerRegDto customerRegDto, bool isPersonal, bool isCheckSate)
        {
            _itemGroupAppService = new ItemGroupAppService();
            _chargeAppService = new ChargeAppService();
            _barSettingAppService = new BarSettingAppService();
            _clientRegAppService = new ClientRegAppService();
            _occTargetDiseaseAppService = new OccTargetDiseaseAppService();
            InitializeComponent();
            CurSelectItemGroup = curSelectItemGroup;
            CustomerDto = customerDto;
            IsPersonal = isPersonal;
            IsCheckSate = isCheckSate;
            CustomerRegDto = customerRegDto;
            if (CustomerDto!=null)
            {
                string cusInfor = "体检号：" + customerRegDto.CustomerBM + ",姓名：" +
                    customerDto.Name + ",性别：" +
                    SexHelper.CustomSexFormatter(customerDto.Sex) + ",年龄：" +
                    customerDto.Age;
                if (customerRegDto.ClientRegId.HasValue)
                {
                    cusInfor += ",单位：" + DefinedCacheHelper.GetClientRegNameComDto().FirstOrDefault(
                        p => p.Id == customerRegDto.ClientRegId)?.ClientName;
                }

                labelCusInfo.Text = cusInfor;
            }
            if (CustomerRegDto.ClientTeamInfo_Id == null)
            {
                btnTTPayment.Enabled = false;
                //btnGRPayment.Enabled = false;
            }
            else
            {
                var searchClientTeamInfoDto = new SearchClientTeamInfoDto();
                searchClientTeamInfoDto.Id = (Guid)CustomerRegDto.ClientTeamInfo_Id;
                LsisaddclientTeamRegitemViewDtos = _clientRegAppService.GetTeamRegItem(searchClientTeamInfoDto);

                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id= (Guid)CustomerRegDto.ClientTeamInfo_Id;
                var clientTeamInfo = _clientRegAppService.GetClientTeamInfos(input);
                if (clientTeamInfo != null && clientTeamInfo.CostType == (int)PayerCatType.FixedAmount)
                {
                    var ss = "，单位限额："+ clientTeamInfo.QuotaMoney;
                    labelCusInfo.Text += ss;
                }
            }
        }

        /// <summary>
        /// 团体界面调用
        /// </summary>
        /// <param name="_lstclientTeamRegitemViewDtos">当前选择分组项目</param>
        /// <param name="_ClientTeamInfoesViewDto">当前选择分组  用于根据性别、年龄等过虑可选组合</param>
        /// <param name="_isPersonal"> 当前是否个人体检  true为个人false为团体</param>
        /// <param name="_isCheckSate">是否已登记  true为已登记false为未登记</param>
        public FrmSeleteItemGroup(List<ClientTeamRegitemViewDto> _lstclientTeamRegitemViewDtos,
            CreateClientTeamInfoesDto _ClientTeamInfoesViewDto, bool _isPersonal, bool _isCheckSate)
        {
            InitializeComponent();
            _itemGroupAppService = new ItemGroupAppService();
            _chargeAppService = new ChargeAppService();
            _barSettingAppService = new BarSettingAppService();
            _clientRegAppService = new ClientRegAppService();
            _occTargetDiseaseAppService = new OccTargetDiseaseAppService();
            lstclientTeamRegitemViewDtos = _lstclientTeamRegitemViewDtos;
            ClientTeamInfoesViewDto = _ClientTeamInfoesViewDto;
            IsPersonal = _isPersonal;
            IsCheckSate = _isCheckSate;
            btnZJPyment.Enabled = false;
            btnTTPayment.Enabled = false;
            btnGRPayment.Enabled = false;
        }

        //右键组合详情
        private void 组合详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details();
        }

        //转组合详情
        private void Details()
        {
            var dto = grdOptionalItemGroup.GetSelectedRowDtos<SimpleItemGroupDto>().FirstOrDefault();
            if (dto == null)
            {
                return;
            }

            using (var frm = new FrmIntroduce(dto))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void grdvSelectItemGroup_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            var view = sender as GridView;
            var fieldName = (e.Item as GridSummaryItem).FieldName;
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
                        var list1 = view.DataSource as List<ClientTeamRegitemViewDto>;
                        if (fieldName == ItemGroupName.FieldName)
                        {
                            if (list != null)
                            {
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Count();
                            }
                            else if (list1 != null)
                            {
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Count();
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }

                        if (fieldName == Prices.SummaryItem.FieldName)
                        {
                            if (list != null)
                            {
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.ItemPrice);
                            }
                            else if (list1 != null)
                            {
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.ItemGroupMoney);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }

                        if (fieldName == PriceAfterDis.SummaryItem.FieldName)
                        {
                            if (list != null)
                            {
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.PriceAfterDis);
                            }
                            else if (list1 != null)
                            {
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.ItemGroupDiscountMoney);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }

                        if (fieldName == GRmoney.SummaryItem.FieldName)
                        {
                            if (list != null)
                            {
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.GRmoney);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }

                        if (fieldName == TTmoney.SummaryItem.FieldName)
                        {
                            if (list != null)
                            {
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus)
                                    .Sum(o => o.TTmoney);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }

                    break;
            }
        }

        private void grdvSelectItemGroup_CustomSummaryExists(object sender, CustomSummaryExistEventArgs e)
        {
        }

        // 回传事件
        //个人
        public event Action<List<TjlCustomerItemGroupDto>> SaveDataComplateForPersonal;

        //团体
        public event Action<List<ClientTeamRegitemViewDto>> SaveDataComplateForGroup;

        private void FrmSeleteItemGroup_Load(object sender, EventArgs e)
        {
            grdvSelectItemGroup.OptionsSelection.CheckBoxSelectorColumnWidth = 40;

            lookUpZYB.Properties.DataSource = IsZYBStateHelper.GetZYBStateModels();
            lookUpZYB.EditValue = 3;

            repositoryItemLookUpEdit1.DataSource= IsZYBStateHelper.GetZYBStateModels();


            setAlter();

            //if (customerRegDto.ClientInfoId == null)
            //{
            //    btnTTPayment.Enabled = false;
            //    btnGRPayment.Enabled = false;
            //}

            //初始化已选组合，根据单位或者个人绑定不同的数据
            InitializationGridView();

            //加载所有组合
            // lstsimpleItemGroupDtos = _itemGroupAppService.QuerySimples(new SearchItemGroupDto());
            lstsimpleItemGroupDtos= DefinedCacheHelper.GetItemGroups();
            //加载所有条码明细表
            _lstbarItemDtos = _barSettingAppService.GetBarItems();

            //加载可选组合
            BindgrdOptionalItemGroup();

            //加载已选组合
            LoadData();

            //职业健康加载必选可选项目
            OccShowITems();
            //decimal decimals = _chargeAppService.MinMoney(new SeachChargrDto { ItemGroups = group, user = CurrentUser });
            //labelControl4.EditValue = decimal.Round(decimals, 2, MidpointRounding.AwayFromZero);
            //Department DepartmentName  科室
            //ItemGroupNames ItemGroupName 组合
            //Prices ItemPrice 价格
            //DiscountRate DiscountRate 折扣率
            //PriceAfterDis PriceAfterDis 折扣价格
            //GRmoneys GRmoney 个付金额
            //TTmoney TTmoney 团付金额
            //PayerCat PayerCat 支付状态
            //IsAddMinus IsAddMinus 加减状态
            //RefundState RefundState 收费状态 

            //Department DepartmentName  科室
            //ItemGroupNames ItemGroupName 组合
            //Prices ItemGroupMoney 价格
            //DiscountRate Discount 折扣率
            //PriceAfterDis ItemGroupDiscountMoney 折扣价格
            //PayerCat PayerCatType 支付状态


            //if (IsPersonal)
            //{

            //    if (CustomerRegDto != null && CustomerRegDto.PhysicalType.HasValue)
            //    {
            //        var tjtyp = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ExaminationType.ToString() &&
            //         o.Value == CustomerRegDto.PhysicalType.Value);
            //        if (tjtyp != null && !tjtyp.Text.Contains("职业+健康") && (tjtyp.Text.Contains("职业") || tjtyp.Text.Contains("放射")))
            //        {
            //            lookUpZYB.EditValue = 1;

            //        }
            //        else
            //        {
            //            lookUpZYB.EditValue = 2;
            //        }


            //    }
            //}
            //else
            //{
            //    if (ClientTeamInfoesViewDto != null && ClientTeamInfoesViewDto.TJType.HasValue)
            //    {
            //        var tjtyp = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ExaminationType.ToString() &&
            //         o.Value == ClientTeamInfoesViewDto.TJType.Value);
            //        if (tjtyp != null && !tjtyp.Text.Contains("职业+健康") && (tjtyp.Text.Contains("职业") || tjtyp.Text.Contains("放射")))
            //        {
            //            lookUpZYB.EditValue = 1;

            //        }
            //        else
            //        {
            //            lookUpZYB.EditValue = 2;
            //        }

            //    }
            //}
        }
        /// <summary>
        /// 职业健康根据危害因素岗位类别显示项目
        /// </summary>
        private void OccShowITems()
        {
            var outitem = new OutIItemGroupsDto();
            bool iszyb = false;
            if (IsPersonal)
            {              
               
                if (CustomerRegDto.OccHazardFactors != null && CustomerRegDto.OccHazardFactors.Count > 0 && !string.IsNullOrEmpty(CustomerRegDto.PostState) && CurSelectItemGroup.Count==0)
                {
                    layoutControlItem2.Visibility = LayoutVisibility.Always;
                    InputRisksDto inputRisksDto = new InputRisksDto();
                    inputRisksDto.Risks = CustomerRegDto.OccHazardFactors.Select(o=>o.Id).ToList();
                    inputRisksDto.ChekType = CustomerRegDto.PostState;
                    outitem = _occTargetDiseaseAppService.getOccHazardFactors(inputRisksDto);
                    iszyb = true;
                }               
            }
            else
            {
                if (ClientTeamInfoesViewDto.ClientTeamRisk !=null &&  ClientTeamInfoesViewDto.ClientTeamRisk.Count>0 && !string.IsNullOrEmpty(ClientTeamInfoesViewDto.CheckType) && lstclientTeamRegitemViewDtos.Count == 0)
                {
                    layoutControlItem2.Visibility = LayoutVisibility.Always;
                    InputRisksDto inputRisksDto = new InputRisksDto();
                    inputRisksDto.Risks = ClientTeamInfoesViewDto.ClientTeamRisk;
                    inputRisksDto.ChekType = ClientTeamInfoesViewDto.CheckType;

                     outitem = _occTargetDiseaseAppService.getOccHazardFactors(inputRisksDto);
                    iszyb = true;

                }
            }
            if (iszyb)
            {
                if (outitem != null)
                {
                    //必选项目
                    if (outitem.MustItemGroups.Count > 0)
                    {
                        AddMax(outitem.MustItemGroups);
                    }
                    //可选项目
                    if (outitem.MayItemGroups.Count > 0)
                    {
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            MayItemGrouplist = outitem.MayItemGroups.ToList();
                            grdOptionalItemGroup.DataSource = outitem.MayItemGroups;

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 必选项目
        /// </summary>
        public void AddMax(List<SimpleItemGroupDto> lstOptionalItemGroup)
        {
            //获取选择dt           
            if (lstOptionalItemGroup.Count() == 0)
            {
                return;
            }

            //获取耗材 关联项目
            var lstAddCustomerItemGroupDto = new List<TjlCustomerItemGroupDto>();
            var lstclientTeamRegitemView = new List<ClientTeamRegitemViewDto>();
            if (IsPersonal) //添加是转换成个人选择组合
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();

                //判断是否重复
                lstOptionalItemGroup = DuplicateRemoval(lstOptionalItemGroup.ToList(), lstSelectItemGroup);
                if (lstOptionalItemGroup.Count > 0)
                {
                    //添加是转换成个人选择组合
                    lstAddCustomerItemGroupDto = GetAddData(lstOptionalItemGroup.ToList(), IsPersonal);

                    //移除可选项目
                    RemoveGroup(lstOptionalItemGroup.ToList());
                    var lstgroup = AddSelectItemGroup(lstAddCustomerItemGroupDto, lstSelectItemGroup);
                    //设置为职业病项目
                    foreach (var lgroup in lstgroup)
                    {
                        lgroup.IsZYB = 1;
                    }
                    grdSelectItemGroup.DataSource = lstgroup;
                    grdSelectItemGroup.RefreshDataSource();

                    //grdvSelectItemGroup.SelectRow(0);
                }
            }
            else //
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();

                //判断是否重复
                lstOptionalItemGroup = DuplicateRemoval(lstOptionalItemGroup.ToList(), lstSelectItemGroup);
                if (lstOptionalItemGroup.Count > 0)
                {
                    //添加是转换成个人选择组合
                    lstclientTeamRegitemView = GetAddTTData(lstOptionalItemGroup.ToList(), IsPersonal);

                    //移除可选项目
                    RemoveGroup(lstOptionalItemGroup.ToList());
                    var lstgroup = AddTTSelectItemGroup(lstclientTeamRegitemView, lstSelectItemGroup);
                    foreach (var lgroup in lstgroup)
                    {
                        lgroup.IsZYB = 1;
                    }
                    grdSelectItemGroup.DataSource = lstgroup;
                    grdSelectItemGroup.RefreshDataSource();

                    //grdvSelectItemGroup.SelectRow(0);
                }
            }
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MayItemGrouplist != null)
            {
                if (radioGroup1.SelectedIndex == 0)
                {
                    grdOptionalItemGroup.DataSource = MayItemGrouplist.ToList();
                }
                else
                {
                    //加载可选组合
                    BindgrdOptionalItemGroup();
                    //移除可选项目
                    RemoveGroup(MayItemGrouplist);
                }
            }

        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }

        private void schItemGroup_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            var isdel = false;
            var selectIndexes = grdvSelectItemGroup.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                for (var i = 0; i < selectIndexes.Length; i++)
                {
                    grdvSelectItemGroup.FocusedRowHandle = selectIndexes[i];
                    var bres = Del();
                    if (bres && i + 1 < selectIndexes.Length)
                    {
                        selectIndexes[i + 1] = selectIndexes[i + 1] - 1;
                    }

                    isdel = true;
                }
            }

            grdvSelectItemGroup.ClearSelection();
            if (isdel == false)
            {
                Del();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
          
            if (IsPersonal)
            {
                CurSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();

                #region 折后价格和团付个付价格不等
                var ErrPrice = CurSelectItemGroup.Where(p => p.PriceAfterDis != p.TTmoney + p.GRmoney).ToList();
                if (ErrPrice.Count > 0)
                {
                    var ErrPriceStr = string.Join(",", ErrPrice.Select(p => p.ItemGroupName)) + "折后价格和团付个付价格不等，请修改！";
                    MessageBox.Show(ErrPriceStr);
                    
                    return;
                } 
                #endregion
                SaveDataComplateForPersonal?.Invoke(CurSelectItemGroup);
            }
            else
            {
                lstclientTeamRegitemViewDtos = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                var remove = lstclientTeamRegitemViewDtos.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).ToList();
                foreach (var item in remove)
                {
                    lstclientTeamRegitemViewDtos.Remove(item);
                }
             
                SaveDataComplateForGroup?.Invoke(lstclientTeamRegitemViewDtos);
            }
            DialogResult = DialogResult.OK;
        }

        //取消
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {              
              
                if (!string.IsNullOrEmpty(txtDiscount.Text) && Convert.ToDecimal(txtDiscount.Text) > 100)
                {
                    alertInfo.Show(this, "提示!", "超出系统原价,请核实打折情况！");
                    return;
                }

                if (!string.IsNullOrEmpty(txtDiscount.Text) && Convert.ToDecimal(txtDiscount.Text) < 0)
                {
                    alertInfo.Show(this, "提示!", "折扣率不能为负数,请核实打折情况！");
                    return;
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text) && Convert.ToDecimal(txtDiscount.Text) < Convert.ToDecimal(CurrentUser.Discount)*100)
                {
                    alertInfo.Show(this, "提示!", "该用户的最大折扣率为：" + (Convert.ToDecimal(CurrentUser.Discount) * 100).ToString().TrimEnd('0').TrimEnd('.') + "%,已超过权限范围！");
                    return;
                }
                ZKSZ();
            }
        }
        private void ZKSZ(string isjg="")
        {

            
            if (CustomerRegDto != null && CustomerRegDto.Remarks == "医保" )
            {
                var YbYS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.YBDoct, 1)?.Remarks;
                if (string.IsNullOrEmpty(YbYS))
                {
                    MessageBox.Show("医保用户不能打折！");
                    return;
                }
                var ybdoct = YbYS.Split('|').ToList();
                if ( !ybdoct.Contains(CurrentUser.Name))
                {
                    MessageBox.Show("医保用户不能打折！");
                    return;
                }

            }

            //计算所有项目价格
            var seachChargrDto = new SeachChargrDto();
            seachChargrDto.user = CurrentUser;
            if (!string.IsNullOrEmpty(isjg))
            {
                seachChargrDto.PayMoney = Convert.ToDecimal(txtDiscountPrice.Text);
            }
            else
            {
                seachChargrDto.PayMoney = 0;
            }

            //获取所有选择行
            seachChargrDto = GetSelectDataRow(seachChargrDto);
            //去掉减项和已收费项目
           // seachChargrDto.ItemGroups = seachChargrDto.ItemGroups.Where(o=> o.IsAddMinus != (int)AddMinusType.Minus).ToList();
            if (seachChargrDto.ItemGroups.Count == 0)
            {
                MessageBox.Show("请选择需要打折的组合！");
                return;
            }
            var TTGroup= seachChargrDto.ItemGroups.Where(o => o.PayerCat == (int)PayerCatType.ClientCharge && o.IsAddMinus != (int)AddMinusType.Minus).ToList();
            if (TTGroup.Count > 0 && IsPersonal)
            {
                MessageBox.Show("不能修改团体金额，请重新选择！");
                return;
            }
            var GRGroup = seachChargrDto.ItemGroups.Where(o => o.PayerCat == (int)PayerCatType.PersonalCharge && o.IsAddMinus != (int)AddMinusType.Minus).ToList();
            if (GRGroup.Count > 0)
            {
                MessageBox.Show("已支付，不能修改价格，请重新选择！");
                return;
            }

            //获取选择项目最低折扣率，并计算价格
            var lstgroupMoney = _chargeAppService.CusGroupMoney(seachChargrDto);
            //var lstgroupMoney = _chargeAppService.MinCusGroupMoney(seachChargrDto);
            if (!decimal.TryParse(CurrentUser?.Discount,out decimal result))
            {
                MessageBox.Show("用户权限获取失败:"+ CurrentUser?.Discount);
                return;
            }         
            if (lstgroupMoney.Any(o => Convert.ToDecimal(o.DiscountRate) < Convert.ToDecimal(CurrentUser.Discount)))
            {
                
                    alertInfo.Show(this, "提示!", "该用户的最大折扣率为：" + (Convert.ToDecimal(CurrentUser.Discount) * 100).ToString().TrimEnd('0').TrimEnd('.')+ "%,已超过权限范围！");
                    return;
                
            }
            //除不尽处理找一个项目平衡
            if (isjg == "1")
            {
                var grousum = lstgroupMoney.Where(o=>o.IsAddMinus !=(int)AddMinusType.Minus).Sum(o => Math.Round(o.PriceAfterDis, 2));
                if (grousum != Convert.ToDecimal(txtDiscountPrice.Text))
                {
                    //找到还可以减钱的科室
                    var group = lstgroupMoney.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).FirstOrDefault(o => o.MaxDiscount  <o.DiscountRate && o.PriceAfterDis != 0);
                    if (group != null)
                    {
                        var minMoney = Convert.ToDecimal(txtDiscountPrice.Text) - grousum;
                        group.PriceAfterDis = Math.Round(group.PriceAfterDis, 2)  + minMoney;
                        if (group.PayerCat == (int)PayerCatType.ClientCharge)
                        {
                            group.TTmoney = Math.Round(group.TTmoney, 2) + minMoney;
                        }
                        else
                        { group.GRmoney = Math.Round(group.GRmoney, 2) + minMoney; }
                    }
                }
            }
            //根据返回值，设置gridview显示行
            SetSelectDataRow(lstgroupMoney);
        }

        private void txtDiscountPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                List<GroupMoneyDto> group = new List<GroupMoneyDto>();
                //if (newlist != null)
                //{
                //    foreach (var item in newlist)
                //    {
                //        GroupMoneyDto groupdto = new GroupMoneyDto();
                //        if (item.ItemGroup != null)
                //        {
                //            groupdto.MaxDiscount = item.ItemGroup.MaxDiscount;
                //        }
                //        groupdto.IsAddMinus = 0;
                //        groupdto.ItemPrice = item.ItemGroupMoney;
                //        groupdto.DiscountRate = item.Discount;
                //        groupdto.PriceAfterDis = item.ItemGroupDiscountMoney;
                //        group.Add(groupdto);
                     

                //    }
                //}
                //decimal decimals = _chargeAppService.MinMoney(new SeachChargrDto { ItemGroups = group, user = CurrentUser });
                //labelControl4.EditValue = decimal.Round(decimals, 2, MidpointRounding.AwayFromZero);

                ZKSZ("1");
               
            }
        }

        private void gridViewItemGround_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Add();
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Del();
            }
        }

        private void grdvSelectItemGroup_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (IsPersonal)
            {
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
                if (lstSelectItemGroup == null || lstSelectItemGroup.ToList().Count < 0)
                {
                    return;
                }

                if (e.Column.Name == "PayerCat")
                {
                    e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
                }

                if (e.Column.Name == "IsAddMinus")
                {
                    e.DisplayText = EnumHelper.GetEnumDesc((AddMinusType)e.Value);
                }

                if (e.Column.Name == "RefundState")
                {
                    if (e.Value != null)
                    {
                        e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
                    }
                    else
                    {
                        e.DisplayText = EnumHelper.GetEnumDesc(PayerCatType.NotRefund);
                    }
                }

                if (e.Column.Name == "CheckSate")
                {
                    if (lstSelectItemGroup != null && lstSelectItemGroup.Count > 0)
                    {
                        e.DisplayText =
                            EnumHelper.GetEnumDesc((ProjectIState)lstSelectItemGroup[e.ListSourceRowIndex].CheckState);
                    }
                }
            }
            else
            {
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                if (lstSelectItemGroup != null && lstSelectItemGroup.ToList().Count > 0)
                {
                    if (e.Column.Name == "PayerCat")
                    {
                        if (e.Value != null && Enum.IsDefined(typeof(PayerCatType), e.Value))
                        {
                            e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
                        }
                        else
                        {
                            e.DisplayText = string.Empty;
                        }
                    }

                    if (e.Column.Name == "PayerCatType")
                    {
                        if (e.Value != null && Enum.IsDefined(typeof(PayerCatType), e.Value))
                        {
                            e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
                        }
                    }

                    if (e.Column.Name == "IsAddMinus")
                    {
                        if (e.Value != null)
                        {
                            e.DisplayText = EnumHelper.GetEnumDesc((AddMinusType)e.Value);
                        }
                        else
                        {
                            e.DisplayText = EnumHelper.GetEnumDesc(AddMinusType.Normal);
                        }
                    }
                }
            }
        }

        private void btnTTPayment_Click(object sender, EventArgs e)
        {
            TTPayment();
        }

        private void btnGRPayment_Click(object sender, EventArgs e)
        {
            GRPayment();
        }

        private void btnZJPyment_Click(object sender, EventArgs e)
        {
            ZJPyment();
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            Refund();
        }

        private void 项目详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowItemInfo();
        }

        private void 转团付ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TTPayment();
        }

        private void 转个付ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GRPayment();
        }

        private void 赠检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZJPyment();
        }

        private void 退费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refund();
        }

        private void grdvSelectItemGroup_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var isadd = grdvSelectItemGroup.GetRowCellValue(e.RowHandle, IsAddMinus);
            if (isadd == null)
            {
                return;
            }

            if (Convert.ToInt32(isadd) == (int)AddMinusType.Add)
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else if (Convert.ToInt32(isadd) == (int)AddMinusType.Minus)
            {
                e.Appearance.ForeColor = Color.Green;
                e.Appearance.FontStyleDelta = FontStyle.Strikeout;
            }
        }

        private void ShowItemInfo()
        {
            alertInfo.Show(this, "提示!", "待开发");
        }

        private void Refund()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;

            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["RefundState"],
                PayerCatType.StayRefund);
            var ilst = grdvOptionalItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["RefundState"],
                    PayerCatType.StayRefund);
            }
        }

        private void ZJPyment()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            var row = columnView.GetFocusedRow() as TjlCustomerItemGroupDto;
            if (row != null)
            {
                if (row.MReceiptInfoPersonalId.HasValue || row.MReceiptInfoClientlId.HasValue)
                {
                    ShowMessageBoxWarning("该项目已经收费，请取消收费后再转赠检。");
                    return;
                }
            }

            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"],
                PayerCatType.GiveCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"],
                PayerCatType.GiveCharge);

            //修改价格
            //grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
            //grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], "0.00");

            var ilst = grdvSelectItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"],
                    PayerCatType.GiveCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"],
                    PayerCatType.GiveCharge);

                //修改价格
                //grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
                //grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], "0.00");
                //grdvSelectItemGroup.RefreshData();
               
            }
        }

        private void GRPayment()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            var row = columnView.GetFocusedRow() as TjlCustomerItemGroupDto;
            if (row != null)
            {
                if (row.MReceiptInfoPersonalId.HasValue || row.MReceiptInfoClientlId.HasValue)
                {
                    ShowMessageBoxWarning("该项目已经收费，不能转个人支付");
                    return;
                }
                var IsChage = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.IsCharge, 10);
                //关联收费判断收费状态，因为一些接口直接回传的状态没有存收费记录
                if (IsChage != null && IsChage.Text == "1")
                {
                    if (row.PayerCat.HasValue && row.PayerCat == (int)PayerCatType.PersonalCharge)
                    {
                        ShowMessageBoxWarning("该项目已经收费，不能转个人支付");
                        return;
                    }
                }
                //if (row.PayerCat==(int)PayerCatType.PersonalCharge)
                //{
                //    ShowMessageBoxWarning("该项目已经收费，不能转个人支付");
                //    return;
                //}
            }

            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"],
                PayerCatType.NoCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"],
                PayerCatType.NoCharge);

            //修改价格
            var PriceAfterDis =
                grdvSelectItemGroup.GetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PriceAfterDis"]);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], PriceAfterDis);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], "0.00");

            var ilst = grdvSelectItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"],
                    PayerCatType.NoCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"],
                    PayerCatType.NoCharge);

                //修改价格
                var PriceAfterDiss =
                    grdvSelectItemGroup.GetRowCellValue(item, grdvSelectItemGroup.Columns["PriceAfterDis"]);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], PriceAfterDiss);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], "0.00");
            }
        }

        private void TTPayment()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            var row = columnView.GetFocusedRow() as TjlCustomerItemGroupDto;
            if (row != null)
            {
                if (row.MReceiptInfoPersonalId.HasValue || row.MReceiptInfoClientlId.HasValue)
                {
                    ShowMessageBoxWarning("该项目已经收费，不能转团付");
                    return;
                }
                var IsChage = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.IsCharge, 10);
                //关联收费判断收费状态，因为一些接口直接回传的状态没有存收费记录
                if (IsChage != null && IsChage.Text == "1")
                {
                    if (row.PayerCat.HasValue && row.PayerCat== (int)PayerCatType.PersonalCharge)
                    {
                        ShowMessageBoxWarning("该项目已经收费，不能转团付");
                        return;
                    }
                }
            }
            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"],
                PayerCatType.ClientCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"],
                PayerCatType.ClientCharge);

            //修改价格
            var PriceAfterDis =
                grdvSelectItemGroup.GetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PriceAfterDis"]);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], PriceAfterDis);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], "0.00");

            var ilst = grdvSelectItemGroup.GetSelectedRows();

            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"],
                    PayerCatType.ClientCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"],
                    PayerCatType.ClientCharge);

                //修改价格
                var PriceAfterDiss =
                    grdvSelectItemGroup.GetRowCellValue(item, grdvSelectItemGroup.Columns["PriceAfterDis"]);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], PriceAfterDiss);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
            }
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

        private void InitializationGridView()
        {
            //待完善
            if (IsPersonal == false)
            {
                Department.FieldName = "DepartmentName";
                ItemGroupNames.FieldName = "ItemGroupName";
                Prices.FieldName = "ItemGroupMoney";
                DiscountRate.FieldName = "Discount";
                PriceAfterDis.FieldName = "ItemGroupDiscountMoney";
                PayerCat.FieldName = "PayerCatType";
                GRmoney.Visible = false;
                TTmoney.Visible = false;
                CheckSate.Visible = false;

                //IsAddMinus.Visible = false;
                RefundState.Visible = false;
            }

            if (IsPersonal == false || CustomerRegDto != null && CustomerRegDto.ClientInfoId == null)
            {
                btnTTPayment.Visible = false;
               btnGRPayment.Visible = true;
                转团付ToolStripMenuItem.Visible = false;
                转个付ToolStripMenuItem.Visible = true;
                txtDiscount.Visible = false;
                txtDiscountPrice.Visible = false;
            }
        }

        private void LoadData()
        {
            if (IsPersonal)
            {
                grdSelectItemGroup.DataSource = CurSelectItemGroup;
            }
            else
            {
                grdSelectItemGroup.DataSource = lstclientTeamRegitemViewDtos;
            }
        }

        public void BindgrdOptionalItemGroup()
        {
            var output = new List<SimpleItemGroupDto>();
            if (IsPersonal)
            {
                if (CustomerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    output = lstsimpleItemGroupDtos.ToList();
                }
                else
                {
                    // 待完善 性别、年龄 等条件过虑
                    var lstvar = from c in lstsimpleItemGroupDtos
                                 where c.Sex == CustomerDto.Sex || c.Sex == (int)Sex.GenderNotSpecified
                                 select c;
                    output = lstvar.ToList();
                }
            }
            else
            {
                if (CustomerDto == null || CustomerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    if (ClientTeamInfoesViewDto != null && (ClientTeamInfoesViewDto.Sex != (int)Sex.GenderNotSpecified ))
                    {
                        // 待完善 性别、年龄 等条件过虑
                        var lstvar = from c in lstsimpleItemGroupDtos
                                     where c.Sex == ClientTeamInfoesViewDto.Sex || c.Sex == (int)Sex.GenderNotSpecified
                                     select c;
                        output = lstvar.ToList();
                    }
                    else
                    {
                        output = lstsimpleItemGroupDtos.ToList();
                    }
                }
                else
                {
                    // 待完善 性别、年龄 等条件过虑
                    var lstvar = from c in lstsimpleItemGroupDtos
                                 where c.Sex == ClientTeamInfoesViewDto.Sex || c.Sex == (int)Sex.GenderNotSpecified
                                 select c;
                    output = lstvar.ToList();
                }
            }

            //去掉重复数据
            if (IsPersonal) //如果是个人
            {
                foreach (var item in CurSelectItemGroup)
                {
                    var vitem = from c in output where c.Id == item.ItemGroupBM_Id select c;
                    if (vitem.ToList().Count > 0)
                    {
                        output.Remove(vitem.ToList()[0]);
                    }
                }
            }
            else
            {
                foreach (var item in lstclientTeamRegitemViewDtos)
                {
                    var vitem = from c in output where c.Id == item.TbmItemGroupid select c;
                    if (vitem.ToList().Count > 0)
                    {
                        output.Remove(vitem.ToList()[0]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(schItemGroup.Text))
            {
                var strup = schItemGroup.Text.ToUpper();
                var lstvar = from c in output
                             where
                                 c.HelpChar.ToUpper().Contains(strup) || c.ItemGroupName.Contains(strup) ||
                                 c.Department.Name.Contains(strup)
                             select c;
                output = lstvar.ToList();
            }
            var ls = output.Where(o=>o.Department ==null).ToList();
            grdOptionalItemGroup.DataSource =
                output.OrderBy(n => n.Department.OrderNum).ThenBy(n => n.OrderNum)?.ToList();
            grdOptionalItemGroup.RefreshDataSource();
        }

        public void Add()
        {
            //获取选择dt
            var lstOptionalItemGroup = grdOptionalItemGroup.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (lstOptionalItemGroup.Count() == 0)
            {
                return;
            }
            #region 判断和已选组合细项是否冲突
            List<Guid> hasGroupIds = new List<Guid>();
            if (IsPersonal) //添加是转换成个人选择组合
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
                if (lstSelectItemGroup != null && lstSelectItemGroup.Count > 0)
                {
                    hasGroupIds = lstSelectItemGroup.Select(p => p.ItemGroupBM_Id).ToList();
                }
            }
            else
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                if (lstSelectItemGroup != null && lstSelectItemGroup.Count > 0)
                {
                    hasGroupIds = lstSelectItemGroup.Select(p => p.TbmItemGroupid.Value).ToList();
                }
            }
            var checkGroupids = lstOptionalItemGroup.Select(p => p.Id).ToList();
            if (hasGroupIds.Count > 0 && checkGroupids.Count > 0)
            {
                ConfiITemDto confiITem = new ConfiITemDto();
                confiITem.HasGroupIds = hasGroupIds;
                confiITem.CheckGroupIds = checkGroupids;
                var ts = _itemGroupAppService.getItemConf(confiITem);
                if (!string.IsNullOrEmpty(ts.StrTS))
                {
                    DialogResult dr = XtraMessageBox.Show(ts.StrTS + "。是否继续选中", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            #endregion
            #region 判断互斥组合

            var hcGrouplist = DefinedCacheHelper.GetBasicDictionary().Where(p => p.Type == BasicDictionaryType.HCGroup.ToString()).ToList();
            if (hcGrouplist != null && hcGrouplist.Count > 0)
            {
                #region 获取已选组合名称
                List<string> hasGroupNames = new List<string>();
                if (IsPersonal) //添加是转换成个人选择组合
                {
                    //获取选择组合
                    var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
                    if (lstSelectItemGroup != null && lstSelectItemGroup.Count > 0)
                    {
                        hasGroupNames = lstSelectItemGroup.Select(p => p.ItemGroupName).ToList();
                    }
                }
                else
                {
                    //获取选择组合
                    var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                    if (lstSelectItemGroup != null && lstSelectItemGroup.Count > 0)
                    {
                        hasGroupNames = lstSelectItemGroup.Select(p => p.ItemGroupName).ToList();
                    }
                } 
                #endregion

                foreach (var hcGroup in hcGrouplist)
                {
                    var groups = hcGroup.Text.Replace("，",",").Split(',').ToList();
                    //选中的组合
                    #region 判断选中组合是否包含互斥组合
                    var checkGroupNames = lstOptionalItemGroup.Select(p => p.ItemGroupName).ToList();
                    List<string> hcGroupNamelist = new List<string>();
                    foreach (var checkGroup in checkGroupNames)
                    {
                        if (groups.Contains(checkGroup))
                        {
                            hcGroupNamelist.Add(checkGroup);
                        }
                    } 
                    #endregion
                    //大于1则标识选中的组合中包含互斥组合
                    if (hcGroupNamelist.Count > 1)
                    {
                        var HCname = string.Join(",", hcGroupNamelist);
                        MessageBox.Show("选中组合存在互斥组合，"+ HCname + "请重新选择！");
                        return;
                    }
                    //等于1需要和已选中的组合进行比较
                    if (hcGroupNamelist.Count == 1)
                    {
                        foreach (var hasgroup in hasGroupNames)
                        {
                            if (groups.Contains(hasgroup) && hasgroup!= hcGroupNamelist.First())
                            {
                                MessageBox.Show("存在互斥组合：" +hasgroup +","+ hcGroupNamelist.First() + ",请重新选择！");
                                return;
                            }
                        }
                    }
                }
            }
            #endregion
            //获取耗材 关联项目
            var lstAddCustomerItemGroupDto = new List<TjlCustomerItemGroupDto>();
            var lstclientTeamRegitemView = new List<ClientTeamRegitemViewDto>();
            if (IsPersonal) //添加是转换成个人选择组合
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();

                //判断是否重复
                lstOptionalItemGroup = DuplicateRemoval(lstOptionalItemGroup.ToList(), lstSelectItemGroup);
                if (lstOptionalItemGroup.Count > 0)
                {
                    //添加是转换成个人选择组合
                    lstAddCustomerItemGroupDto = GetAddData(lstOptionalItemGroup.ToList(), IsPersonal);

                    //移除可选项目
                    RemoveGroup(lstOptionalItemGroup.ToList());
                    var lstgroup = AddSelectItemGroup(lstAddCustomerItemGroupDto, lstSelectItemGroup);
                    grdSelectItemGroup.DataSource = lstgroup;
                    grdSelectItemGroup.RefreshDataSource();

                    //grdvSelectItemGroup.SelectRow(0);
                }
            }
            else //
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();

                //判断是否重复
                lstOptionalItemGroup = DuplicateRemoval(lstOptionalItemGroup.ToList(), lstSelectItemGroup);
                if (lstOptionalItemGroup.Count > 0)
                {
                    //添加是转换成个人选择组合
                    lstclientTeamRegitemView = GetAddTTData(lstOptionalItemGroup.ToList(), IsPersonal);

                    //移除可选项目
                    RemoveGroup(lstOptionalItemGroup.ToList());
                    var lstgroup = AddTTSelectItemGroup(lstclientTeamRegitemView, lstSelectItemGroup);
                    grdSelectItemGroup.DataSource = lstgroup;
                    grdSelectItemGroup.RefreshDataSource();

                    //grdvSelectItemGroup.SelectRow(0);
                }
            }
        }

        private List<TjlCustomerItemGroupDto> AddSelectItemGroup(List<TjlCustomerItemGroupDto> customerItemGroupDto,
            List<TjlCustomerItemGroupDto> list)
        {
            if (lstselectItemGroup == null)
            {
                lstselectItemGroup = new List<TjlCustomerItemGroupDto>();
            }

            foreach (var item in customerItemGroupDto)
            {
                list.Insert(0, item);
                lstselectItemGroup.Add(item);
            }

            return list;
        }

        private List<ClientTeamRegitemViewDto> AddTTSelectItemGroup(List<ClientTeamRegitemViewDto> customerItemGroupDto,
            List<ClientTeamRegitemViewDto> list)
        {
            if (lstClientTeamRegitemViewDto == null)
            {
                lstClientTeamRegitemViewDto = new List<ClientTeamRegitemViewDto>();
            }

            foreach (var item in customerItemGroupDto)
            {
                list.Insert(0, item);
                lstClientTeamRegitemViewDto.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 去掉选择组合重复
        /// </summary>
        /// <param name="customerItemGroupDto">从可选组合选择的组合</param>
        /// <param name="list">已经选择的组合</param>
        /// <returns>List<SimpleItemGroupDto></returns>
        private List<SimpleItemGroupDto> DuplicateRemoval(List<SimpleItemGroupDto> lstSimpleItemGroupDto,
            List<TjlCustomerItemGroupDto> lstCustomerItemGroupDto)
        {
            var lstSimpleItemGroupDtonew = new List<SimpleItemGroupDto>(lstSimpleItemGroupDto.ToArray());
            var strbname = new StringBuilder();
            foreach (var item in lstSimpleItemGroupDto)
            {
                var vitem = from c in lstCustomerItemGroupDto where c.ItemGroupBM_Id == item.Id select c;
                if (vitem.ToList().Count > 0)
                {
                    lstSimpleItemGroupDtonew.Remove(item);
                    strbname.Append(item.ItemGroupName + "、");
                }
            }

            if (!string.IsNullOrEmpty(strbname.ToString()))
            {
                alertInfo.Show(this, "提示!", strbname.ToString().Trim('、') + " 组合已选择!");
            }

            return lstSimpleItemGroupDtonew;
        }

        /// <summary>
        /// 去掉选择组合重复
        /// </summary>
        /// <param name="customerItemGroupDto">从可选组合选择的组合</param>
        /// <param name="list">已经选择的组合</param>
        /// <returns>List<SimpleItemGroupDto></returns>
        private List<SimpleItemGroupDto> DuplicateRemoval(List<SimpleItemGroupDto> lstSimpleItemGroupDto,
            List<ClientTeamRegitemViewDto> lstCustomerItemGroupDto)
        {
            var lstSimpleItemGroupDtonew = new List<SimpleItemGroupDto>(lstSimpleItemGroupDto.ToArray());
            var strbname = new StringBuilder();
            var sexNoName= new StringBuilder();
            foreach (var item in lstSimpleItemGroupDto)
            {
                if (item.Sex != (int)Sex.GenderNotSpecified && item.Sex != (int)Sex.Unknown)
                {
                    if (IsPersonal && CustomerDto!=null)
                    {
                        if (CustomerDto.Sex != item.Sex)
                        {
                            lstSimpleItemGroupDtonew.Remove(item);
                            sexNoName.Append(item.ItemGroupName + "、");
                        }
                    }
                    else if(!IsPersonal && ClientTeamInfoesViewDto != null && ClientTeamInfoesViewDto.Sex !=(int)Sex.GenderNotSpecified)
                    {
                        if (ClientTeamInfoesViewDto.Sex != item.Sex)
                        {
                            lstSimpleItemGroupDtonew.Remove(item);
                            sexNoName.Append(item.ItemGroupName + "、");
                        }
                    }

                }           

                var vitem = from c in lstCustomerItemGroupDto where c.TbmItemGroupid == item.Id select c;
                if (vitem.ToList().Count > 0)
                {
                    lstSimpleItemGroupDtonew.Remove(item);
                    strbname.Append(item.ItemGroupName + "、");
                }
            }

            if (!string.IsNullOrEmpty(strbname.ToString()))
            {
                alertInfo.Show(this, "提示!", strbname.ToString().Trim('、') + " 组合已选择!");
            }
            if (!string.IsNullOrEmpty(sexNoName.ToString()))
            {
                alertInfo.Show(this, "提示!", sexNoName.ToString().Trim('、') + " 性别不符!");
            }

            return lstSimpleItemGroupDtonew;
        }

        private List<ClientTeamRegitemViewDto> GetAddTTData(List<SimpleItemGroupDto> lstSimpleItemGroupDto,
            bool isPersonal)
        {
            var lstgroupDto = new List<ClientTeamRegitemViewDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                
                var groupDto = new ClientTeamRegitemViewDto();
                groupDto.ClientTeamInfoId = ClientTeamInfoesViewDto.Id;
                groupDto.TbmDepartmentid = SimpleItemGroupDto.Department.Id;
                groupDto.DepartmentName = SimpleItemGroupDto.Department.Name;
                groupDto.DepartmentOrder = SimpleItemGroupDto.Department.OrderNum;
                groupDto.TbmItemGroupid = SimpleItemGroupDto.Id;
                groupDto.ItemGroupName = SimpleItemGroupDto.ItemGroupName;
                groupDto.ItemGroupOrder = SimpleItemGroupDto.OrderNum;
                groupDto.ItemGroupMoney = SimpleItemGroupDto.Price.Value;
                groupDto.Discount = ClientTeamInfoesViewDto.Jxzk ?? 1;
                groupDto.ItemGroupDiscountMoney = SimpleItemGroupDto.Price.Value * (ClientTeamInfoesViewDto.Jxzk ?? 1);
                if (!string.IsNullOrEmpty(lookUpZYB.EditValue?.ToString()))
                {
                    groupDto.IsZYB = (int)lookUpZYB.EditValue;

                }
                //加项 单位支付
                if (ClientTeamInfoesViewDto.JxType == (int)PayerCatType.FixedAmount)
                {
                    groupDto.PayerCatType = (int)PayerCatType.FixedAmount;
                }
                else
                {
                    groupDto.PayerCatType = (int)PayerCatType.ClientCharge;
                }

                //加项 固定金额 总金额在范围内 团体支付，超出个人支付
                if (ClientTeamInfoesViewDto.CostType == (int)PayerCatType.FixedAmount)
                {
                    var lstregitem = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                    var itemGroupSum = lstregitem.Sum(o => o.ItemGroupDiscountMoney);
                    itemGroupSum = itemGroupSum + groupDto.ItemGroupDiscountMoney;
                    if (itemGroupSum > ClientTeamInfoesViewDto.TeamMoney)
                    {
                        groupDto.PayerCatType = (int)PayerCatType.PersonalCharge;
                    }
                    else
                    {
                        groupDto.PayerCatType = (int)PayerCatType.ClientCharge;
                    }
                }

                //groupDto.IsAddMinus = (int)AddMinusType.Add;
                groupDto.IsAddMinus = (int)AddMinusType.Normal;
                lstgroupDto.Add(groupDto);
            }

            return lstgroupDto;
        }

        private List<TjlCustomerItemGroupDto> GetAddData(List<SimpleItemGroupDto> lstSimpleItemGroupDto,
            bool isPersonal)
        {
            var lstgroupDto = new List<TjlCustomerItemGroupDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                var groupDto = new TjlCustomerItemGroupDto();
                groupDto.CustomerRegBMId = CustomerRegDto.Id;
                groupDto.DepartmentId = SimpleItemGroupDto.Department.Id;
                groupDto.DepartmentName = SimpleItemGroupDto.Department.Name;
                groupDto.DepartmentOrder = SimpleItemGroupDto.Department.OrderNum;
                groupDto.ItemGroupBM_Id = SimpleItemGroupDto.Id;
                groupDto.ItemGroupName = SimpleItemGroupDto.ItemGroupName;
                groupDto.ItemGroupOrder = SimpleItemGroupDto.OrderNum;
                groupDto.SFType = Convert.ToInt32(SimpleItemGroupDto.ChartCode);
                groupDto.CheckState = (int)ProjectIState.Not;
                groupDto.SummBackSate = (int)SummSate.Audited;
                groupDto.BillingEmployeeBMId = CurrentUser.Id;
                groupDto.NucleicAcidState = SimpleItemGroupDto.NucleicAcidState;
                if (!string.IsNullOrEmpty(lookUpZYB.EditValue?.ToString()))
                {
                    groupDto.IsZYB = (int)lookUpZYB.EditValue;

                }
                if (CustomerRegDto.ClientTeamInfo_Id == null)
                {
                    var visitemsfState = from c in CurSelectItemGroup
                                         where c.PayerCat == (int)PayerCatType.ClientCharge
                                               || c.PayerCat == (int)PayerCatType.PersonalCharge
                                         select c;
                    if ((CustomerRegDto.CostState != null && visitemsfState.Count() > 0) || CustomerRegDto.ItemSuitBMId.HasValue)
                    {
                        groupDto.IsAddMinus = (int)AddMinusType.Add;
                    }
                    else
                    {
                        groupDto.IsAddMinus = (int)AddMinusType.Normal;
                    }
                }
                else
                {
                    var vishave = from c in LsisaddclientTeamRegitemViewDtos
                                  where c.ItemGroup.Id == SimpleItemGroupDto.Id
                                  select c;
                    
                    if (vishave.Count() > 0)
                    {
                        groupDto.IsAddMinus = (int)AddMinusType.Normal;
                    }
                    else
                    {
                        if (CustomerRegDto.CostState != (int)PayerCatType.NoCharge)
                        {
                            groupDto.IsAddMinus = (int)AddMinusType.Add;
                        }
                        else
                        {
                            groupDto.IsAddMinus = (int)AddMinusType.Normal;
                        }
                    }
                }

                groupDto.ItemPrice = SimpleItemGroupDto.Price.Value;
                if (CustomerRegDto.ClientTeamInfo != null&& CustomerRegDto.ClientTeamInfo.JxType!=null 
                    && (CustomerRegDto.ClientTeamInfo.JxType == (int)PayerCatType.ClientCharge 
                    || CustomerRegDto.ClientTeamInfo.JxType == (int)PayerCatType.PersonalCharge)
                    )
                {
                    if (CustomerRegDto.ClientTeamInfo.JxType == (int)PayerCatType.ClientCharge)
                    {
                        decimal jxzk = new decimal(1.00);
                        if (CustomerRegDto.ClientTeamInfo.Jxzk > 0)
                        {
                            jxzk = CustomerRegDto.ClientTeamInfo.Jxzk;
                        }
                        if (CustomerRegDto != null && CustomerRegDto.Remarks == "医保")
                        {
                            jxzk = 1;
                        }
                        groupDto.DiscountRate = jxzk;
                        groupDto.PriceAfterDis = SimpleItemGroupDto.Price.Value * jxzk;
                        groupDto.GRmoney = decimal.Parse("0.00");
                        groupDto.PayerCat = (int)PayerCatType.ClientCharge;
                        groupDto.TTmoney = SimpleItemGroupDto.Price.Value * jxzk;
                    }
                    else
                    {
                        decimal jxzk = new decimal(1.00);
                        if (CustomerRegDto.ClientTeamInfo.Jxzk > 0)
                        {
                            jxzk= CustomerRegDto.ClientTeamInfo.Jxzk;
                        }
                        if (CustomerRegDto != null && CustomerRegDto.Remarks == "医保")
                        {
                            jxzk = 1;
                        }
                        groupDto.DiscountRate = jxzk;
                        groupDto.PriceAfterDis = SimpleItemGroupDto.Price.Value * jxzk;
                        groupDto.GRmoney = SimpleItemGroupDto.Price.Value * jxzk;
                        groupDto.PayerCat = (int)PayerCatType.NoCharge;
                        groupDto.TTmoney = decimal.Parse("0.00");
                    }
                }
                else
                {
                    groupDto.DiscountRate = new decimal(1.00);
                    groupDto.PriceAfterDis = SimpleItemGroupDto.Price.Value;
                    groupDto.GRmoney = SimpleItemGroupDto.Price.Value;
                    groupDto.PayerCat = (int)PayerCatType.NoCharge;
                    groupDto.TTmoney = decimal.Parse("0.00");
                }
     
              
              
              
                groupDto.GuidanceSate = (int)PrintSate.NotToPrint;
                groupDto.BarState = (int)PrintSate.NotToPrint;
                groupDto.RequestState = (int)PrintSate.NotToPrint;
                groupDto.RefundState = (int)PayerCatType.NotRefund;
                var vitem = from c in _lstbarItemDtos where c.ItemGroupId == SimpleItemGroupDto.Id select c;
                if (vitem.ToList().Count > 0)
                {
                    groupDto.DrawSate = (int)BloodState.NOT;
                }
                else
                {
                    groupDto.DrawSate = (int)BloodState.NOTNEED;
                }

                lstgroupDto.Add(groupDto);
            }

            return lstgroupDto;
        }

        private List<ClientTeamRegitemViewDto> GetAddData(List<SimpleItemGroupDto> lstSimpleItemGroupDto)
        {
            var lstgroupDto = new List<ClientTeamRegitemViewDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                var groupDto = new ClientTeamRegitemViewDto();
                lstgroupDto.Add(groupDto);
            }

            return lstgroupDto;
        }

        public bool Del()
        {
            var bres = false;
            if (IsPersonal)
            {
                var item = grdSelectItemGroup.GetFocusedRowDto<TjlCustomerItemGroupDto>();
                if (item == null)
                {
                    return bres;
                }
                var hcdepr=DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == item.DepartmentId);
                //不能减项
                if (item.IsAddMinus == (int)AddMinusType.Minus)
                {
                    var dr = XtraMessageBox.Show("已经是减项,是否取消减项？", "确认", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        //更改为正常项目
                        var columnView = (ColumnView)grdSelectItemGroup.FocusedView;

                        //得到选中的行索引
                        var focusedhandle = columnView.FocusedRowHandle;
                        grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["IsAddMinus"],
                            AddMinusType.Normal);
                        grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["RefundState"],
                            PayerCatType.NotRefund);
                        grdvSelectItemGroup.RefreshData();
                        return bres;
                    }

                    return bres;

                    //alertInfo.Show(this, "提示!", item.ItemGroupName + " 已经是减项,不能重复减项!");
                    //return;
                }

                if (item.PayerCat == (int)PayerCatType.PersonalCharge || item.PayerCat == (int)PayerCatType.MixedCharge)
                {
                    //四院已收费不能减项目
                    #region MyRegion
                    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                    if (HISjk == "1")
                    {
                        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;

                        if (HISName.Contains("江苏鑫亿四院"))
                        {
                            MessageBox.Show("已收费不能减项，请到HIS退费后再操作！");
                            return false;
                        }
                    }
                    #endregion
                    alertInfo.Show(this, "提示!", item.ItemGroupName + " 已经收费,请到收费处进行退费!");
                    setPayerCatType();

                    //return;
                }
                Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                if (item.CheckState == (int)ProjectIState.Complete && hcdepr.Category != "耗材" && item.ItemGroupBM_Id != guid)
                {
                    alertInfo.Show(this, "提示!", item.ItemGroupName + " 已经检查,不能减项!");
                    return bres;
                }

                if (item.CheckState == (int)ProjectIState.Part)
                {
                    alertInfo.Show(this, "提示!", item.ItemGroupName + " 已经部分检查,不能减项!");
                    return bres;
                }

                if (CustomerRegDto.ClientTeamInfo == null)
                {
                    if ((CustomerRegDto.CostState == null || item.PayerCat == (int)PayerCatType.NoCharge) && !item.ItemSuitId.HasValue)
                    {
                        setRemove(item); //设置移除
                        bres = true;
                    }
                    else
                    {
                        setIsAddMinus(); //设置减项状态
                    }
                }
                else
                {
                    var vishave = from c in LsisaddclientTeamRegitemViewDtos
                                  where c.ItemGroup.Id == item.ItemGroupBM_Id
                                  select c;
                    if (vishave.Count() > 0)
                    {
                        setIsAddMinus(); //设置减项状态
                    }
                    else
                    {
                        //修改不包括单位人员
                        //var visitemsfState = from c in CurSelectItemGroup
                        //                     where c.PayerCat == (int)PayerCatType.ClientCharge
                        //                           || c.PayerCat == (int)PayerCatType.PersonalCharge
                        //                     select c;

                        var visitemsfState = from c in CurSelectItemGroup
                                             where  c.PayerCat == (int)PayerCatType.PersonalCharge
                                             select c;
                        if (visitemsfState.Count() > 0 || item.CheckState==2 || item.CheckState==3)
                        {
                            
                            setIsAddMinus(); //设置减项状态
                        }
                        else
                        {
                            setRemove(item); //设置移除
                            bres = true;
                        }
                    }
                }

                //如果套餐不为空或者已检
                //if ((item.ItemSuitId != null || isCheckSate == true))
                //{
                //    if (lstselectItemGroup != null && lstselectItemGroup.Contains(item))
                //    {
                //        setRemove(item);//设置移除
                //    }
                //    else
                //    {
                //        setIsAddMinus();//设置减项状态
                //    }
                //}
                //else
                //{
                //    setRemove(item);//设置移除
                //} 
            }
            else
            {
                var item = grdSelectItemGroup.GetFocusedRowDto<ClientTeamRegitemViewDto>();

                if (item == null)
                {
                    return bres;
                }

                //如果套餐不为空或者已检
                if (item.ItemSuitId != null || IsCheckSate)
                {
                    if (lstClientTeamRegitemViewDto != null && lstClientTeamRegitemViewDto.Contains(item))
                    {
                        setTTRemove(item); //设置移除
                        bres = true;
                    }
                    else
                    {
                        if (item.ProcessState != true)
                        {
                            if (item.IsAddMinus == (int)AddMinusType.Minus)
                            {
                                var dr = XtraMessageBox.Show("已经是减项,是否取消减项？", "确认", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question);
                                if (dr == DialogResult.OK)
                                {
                                    //更改为正常项目
                                    var columnView = (ColumnView)grdSelectItemGroup.FocusedView;

                                    //得到选中的行索引
                                    var focusedhandle = columnView.FocusedRowHandle;
                                    grdvSelectItemGroup.SetRowCellValue(focusedhandle,
                                        grdvSelectItemGroup.Columns["RefundState"], AddMinusType.Normal);
                                    grdvSelectItemGroup.RefreshData();
                                    return bres;
                                }

                                return bres;
                            }

                            setIsAddMinus(); //设置减项状态
                        }
                        else
                        {
                            alertInfo.Show(this, "提示!", item.ItemGroupName + " 已经检查,不能减项!");
                        }
                    }
                }
                else
                {
                    setTTRemove(item); //设置移除
                    bres = true;
                }
            }

            var iindex = grdvOptionalItemGroup.FocusedRowHandle;

            //加载可选组合
            BindgrdOptionalItemGroup();
            grdvOptionalItemGroup.FocusedRowHandle = iindex;
            return bres;
        }

        private void setRemove(TjlCustomerItemGroupDto item)
        {
            grdSelectItemGroup.RemoveDtoListItem(item);
            CurSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
            if (lstselectItemGroup != null)
            {
                lstselectItemGroup.Remove(item);
            }
        }

        private void setTTRemove(ClientTeamRegitemViewDto item)
        {
            grdSelectItemGroup.RemoveDtoListItem(item);
            lstclientTeamRegitemViewDtos = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
            if (lstClientTeamRegitemViewDto != null)
            {
                lstClientTeamRegitemViewDto.Remove(item);
            }
        }

        private void setIsAddMinus()
        {
            //更改为减项
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;

            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["IsAddMinus"],
                AddMinusType.Minus);
            grdvSelectItemGroup.RefreshData();
        }

        private void setPayerCatType()
        {
            //更改为减项
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;

            //得到选中的行索引
            var focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["RefundState"],
                PayerCatType.StayRefund);
            grdvSelectItemGroup.RefreshData();
        }

        private SeachChargrDto GetSelectDataRow(SeachChargrDto seachChargrDto)
        {
            seachChargrDto.ItemGroups = new List<GroupMoneyDto>();

            //获取所有选择列
            var iselectnum = grdvSelectItemGroup.GetSelectedRows();
            if (iselectnum.Length <= 0)
            {
                alertInfo.Show(this, "提示!", "请选择待打折组合");
            }

            foreach (var item in iselectnum)
            {
                var groupMoney = new GroupMoneyDto();
               
                if (IsPersonal) //个人
                {
                    var dataRow = (TjlCustomerItemGroupDto)grdvSelectItemGroup.GetRow(item);
                                     
                        var strItemGroupName = dataRow.ItemGroupName;
                    var vitemGroup = from c in lstsimpleItemGroupDtos
                                     where c.ItemGroupName == strItemGroupName
                                     select c;
                    if (vitemGroup.ToList().Count > 0)
                    {
                        groupMoney.MaxDiscount = vitemGroup.ToList()[0].MaxDiscount;
                    }
                    else
                    {
                        groupMoney.MaxDiscount = 0;
                    }

                    groupMoney.IsAddMinus = dataRow.IsAddMinus;
                    groupMoney.ItemPrice = dataRow.ItemPrice;
                    groupMoney.PriceAfterDis = dataRow.PriceAfterDis;
                    groupMoney.DiscountRate = txtDiscount.Text==""?0: Convert.ToDecimal(txtDiscount.Text) / 100;
                    groupMoney.PayerCat = dataRow.PayerCat.Value;
                    groupMoney.RefundState = dataRow.RefundState;
                    groupMoney.GRmoney = dataRow.GRmoney;
                    groupMoney.TTmoney = dataRow.TTmoney;
                }
                else
                {
                    var dataRow = (ClientTeamRegitemViewDto)grdvSelectItemGroup.GetRow(item);
                    var strItemGroupName = dataRow.ItemGroupName;
                    var vitemGroup = from c in lstsimpleItemGroupDtos
                                     where c.ItemGroupName == strItemGroupName
                                     select c;
                    if (vitemGroup.ToList().Count > 0)
                    {
                        groupMoney.MaxDiscount = vitemGroup.ToList()[0].MaxDiscount;
                    }
                    else
                    {
                        groupMoney.MaxDiscount = 1;
                    }

                    groupMoney.IsAddMinus = dataRow.IsAddMinus;
                    groupMoney.ItemPrice = dataRow.ItemGroupMoney;
                    groupMoney.PriceAfterDis = dataRow.ItemGroupDiscountMoney;
                    groupMoney.DiscountRate = txtDiscount.Text == "" ? 0 : Convert.ToDecimal(txtDiscount.Text) / 100;
                    groupMoney.PayerCat = dataRow.PayerCatType;
                    groupMoney.RefundState = 1;
                    groupMoney.GRmoney = dataRow.ItemGroupMoney;
                    groupMoney.TTmoney = dataRow.ItemGroupMoney;
                }

                seachChargrDto.ItemGroups.Add(groupMoney);
            }

            return seachChargrDto;
        }

        private void SetSelectDataRow(List<GroupMoneyDto> lstgroupMoney)
        {
            //获取所有选择列
            var iselectnum = grdvSelectItemGroup.GetSelectedRows();
            for (var i = 0; i < iselectnum.Length; i++)
            {
                //Department DepartmentName  科室
                //ItemGroupNames ItemGroupName 组合
                //Prices ItemPrice 价格
                //DiscountRate DiscountRate 折扣率
                //PriceAfterDis PriceAfterDis 折扣价格
                //GRmoneys GRmoney 个付金额
                //TTmoney TTmoney 团付金额
                //PayerCat PayerCat 支付状态
                //IsAddMinus IsAddMinus 加减状态
                //RefundState RefundState 收费状态 

                //Department DepartmentName  科室
                //ItemGroupNames ItemGroupName 组合
                //Prices ItemGroupMoney 价格
                //DiscountRate Discount 折扣率
                //PriceAfterDis ItemGroupDiscountMoney 折扣价格
                //PayerCat PayerCatType 支付状态

                if (IsPersonal == false)
                {
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["ItemGroupMoney"],
                        Math.Round(lstgroupMoney[i].ItemPrice,2));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["Discount"],
                         Math.Round(lstgroupMoney[i].DiscountRate,4));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i],
                        grdvSelectItemGroup.Columns["ItemGroupDiscountMoney"], lstgroupMoney[i].PriceAfterDis);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["GRmoney"],
                         Math.Round(lstgroupMoney[i].ItemPrice,2));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["TTmoney"],
                         Math.Round(lstgroupMoney[i].ItemPrice,2));
                }
                else
                {
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["ItemPrice"],
                         Math.Round(lstgroupMoney[i].ItemPrice,2));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["DiscountRate"],
                         Math.Round(lstgroupMoney[i].DiscountRate,4));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["PriceAfterDis"],
                         Math.Round(lstgroupMoney[i].PriceAfterDis,2));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["GRmoney"],
                         Math.Round(lstgroupMoney[i].PriceAfterDis,2));
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["TTmoney"], 0);
                }
            }
        }

        private void RemoveGroup(List<SimpleItemGroupDto> lstSimpleItemGroupDto)
        {
            //获取当前选择行
            var iindex = grdvOptionalItemGroup.GetFocusedDataSourceRowIndex();
            var lstOptionalItemGroup = grdOptionalItemGroup.GetDtoListDataSource<SimpleItemGroupDto>();
            foreach (var item in lstSimpleItemGroupDto)
            {
                if (lstOptionalItemGroup.Contains(item))
                {
                    grdOptionalItemGroup.RemoveDtoListItem(item);
                }
            }

            if (lstOptionalItemGroup.Count < iindex)
            {
                grdvOptionalItemGroup.SelectRow(iindex);
            }
            else
            {
                grdvOptionalItemGroup.SelectRow(lstOptionalItemGroup.Count - lstSimpleItemGroupDto.Count);
            }
        }

        private void schItemGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (grdvOptionalItemGroup.DataRowCount > 0)
                {
                    grdOptionalItemGroup.Focus();
                    grdvOptionalItemGroup.SelectRow(0);
                }
            }
        }

        private void grdvOptionalItemGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Add();
                schItemGroup.Focus();
                schItemGroup.SelectAll();
            }
        }
        bool isEd = true;
        private void grdvSelectItemGroup_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == TTmoney && isEd== true)
            {
                isEd = false;
                var TTJE = grdvSelectItemGroup.GetFocusedRowCellValue(TTmoney);
                var All = grdvSelectItemGroup.GetFocusedRowCellValue(PriceAfterDis);
                if (TTJE != null && All != null && decimal.TryParse(TTJE.ToString(), out decimal TTpay) && decimal.TryParse(All.ToString(), out decimal Allpay))
                {
                    var gr = Allpay - TTpay;
                   // grdvSelectItemGroup.SetFocusedRowCellValue(GRmoney, gr);
                    grdvSelectItemGroup.SetRowCellValue(grdvSelectItemGroup.FocusedRowHandle, GRmoney, gr);//已收
                    isEd = true;
                }
            }
            if (e.Column == GRmoney && isEd == true)
            {
                isEd = false;
                var GRJE = grdvSelectItemGroup.GetFocusedRowCellValue(GRmoney);
                var All = grdvSelectItemGroup.GetFocusedRowCellValue(PriceAfterDis);
                if (GRJE != null && All != null && decimal.TryParse(GRJE.ToString(), out decimal GRpay) && decimal.TryParse(All.ToString(), out decimal Allpay))
                {
                    var tt = Allpay - GRpay;
                    //grdvSelectItemGroup.SetFocusedRowCellValue(TTmoney, tt);
                    grdvSelectItemGroup.SetRowCellValue(grdvSelectItemGroup.FocusedRowHandle, TTmoney, tt);//已收
                    isEd = true;
                }
            }
        }
        #region 设置组合体检类别
        private void GroupZYBType()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            var row = columnView.GetFocusedRow() as TjlCustomerItemGroupDto;
        
            

            var ilst = grdvSelectItemGroup.GetSelectedRows();

            var zyb = (int)lookUpZYB.EditValue;
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, conIsZYB, zyb);

                grdvSelectItemGroup.RefreshData();
            }
        }
        #endregion
    }
}