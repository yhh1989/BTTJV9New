using Abp.Application.Services.Dto;
using DevExpress.XtraEditors.Controls;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmClientTeamCharge : UserBaseForm
    {
        private IClientRegAppService _clientRegAppService { get; set; }
        private IChargeAppService _chargeAppService { get; set; }
        private ICustomerAppService _customerAppService { get; set; }
        public List<Guid> GuidList { get; set; }
        public UserViewDto user { get; set; }
        public int ZKdown { get; set; }
        public int JGdown { get; set; }
        private readonly ICommonAppService commonAppService;
        public string clientName = "";
        public string clientBM = "";
        private CreateClientXKSetDto createClientXKSetDto = new CreateClientXKSetDto();
        private IOAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        private CreatOAApproValcsDto creatOAApproValcsDto = new CreatOAApproValcsDto();

        private Guid NClientID = new Guid();
        public FrmClientTeamCharge()
        {
            InitializeComponent();
            this.ControlBox = false;
            _clientRegAppService = new ClientRegAppService();
            _chargeAppService = new ChargeAppService();
            _customerAppService = new CustomerAppService();
            commonAppService = new CommonAppService();
        }
        public FrmClientTeamCharge(List<Guid> list,string ClientName,string clientbm,Guid? ClientID) : this()
        {
            GuidList = list;
            clientName = ClientName;
            clientBM = clientbm;
            NClientID = ClientID.Value;
            _clientRegAppService = new ClientRegAppService();
            _chargeAppService = new ChargeAppService();
        }

        private void FrmClientTeamCharge_Load(object sender, EventArgs e)
        {
            createClientXKSetDto = oAApprovalAppService.getCreateClientXKSet();
            gridView1.Columns[gridColumnStatus.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[gridColumnStatus.FieldName].DisplayFormat.Format = new CustomFormatter(MarrySateHelper.MarrySateFormatter);
            gridView1.Columns[gridColumnSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[gridColumnSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
            //单选框赋值
            var PaymentMethod = PaymentMethodHelper.GetPaymentMethod();
            foreach (var item in PaymentMethod)
            {
                RadioGroupItem rg = new RadioGroupItem();
                rg.Description = item.Display;
                rg.Value = item.Id;
                if (item.Display != "限额")
                {
                    radioGroupJXFS.Properties.Items.Add(rg);
                }
                radioGroupZFFS.Properties.Items.Add(rg);
            }
            user = CurrentUser;
            labelControlUser.Text = user.Name;
           
            var clientRegs = _clientRegAppService.GetClientTeamInfosById(GuidList);
            Guid clientId = new Guid();
            foreach (var item in clientRegs)
            {
                item.AgeLimit = item.MinAge + "-" + item.MaxAge;
              //  clientId= item.
            }
            gridControl1.DataSource = clientRegs;


            //设置为团体收费审核必须审核后才能添加单位
            if (createClientXKSetDto != null && createClientXKSetDto.ZKType == 1)
            {
                labelControl8.Text = "单位最低折扣：";
                SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                searchOAApproValcsDto.ClientInfoId = NClientID;
                var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                if (zk.Count > 0)
                {
                    creatOAApproValcsDto = zk[0];
                  
                    labelControlZK.Text = string.IsNullOrWhiteSpace(creatOAApproValcsDto.DiscountRate.ToString()) ? "100%" : creatOAApproValcsDto.DiscountRate * 100 + "%"; 
                }
            }
            else
            {
                labelControlZK.Text = string.IsNullOrWhiteSpace(user.Discount) ? "100%" : Convert.ToDouble(user.Discount) * 100 + "%";
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            //第一列增加行号
            if (e.Info.IsRowIndicator)
            {
                //if (this.IsGroupRow(e.RowHandle) == false)
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = Convert.ToInt32(e.RowHandle + 1).ToString();
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focerowchanged();
        }
        private void focerowchanged()
        {
            dxErrorProvider.ClearErrors();
            try
            {
                //var id = Guid.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumnId).ToString());
                var dto = gridControl1.GetSelectedRowDtos<ClientTeamInfosDto>().FirstOrDefault();
                var newlist = _clientRegAppService.GetClientTeamRegByClientId(new SearchClientTeamInfoDto { Id = dto.Id });
                if (dto.ItemSuit != null)
                    textEditTC.Text = dto.ItemSuit.ItemSuitName;// dto.ItemSuitName;
                else
                    textEditTC.Text = "不限";// dto.ItemSuitName;
                decimal Total = 0;
                if (newlist != null && newlist.Count>0)
                {
                    foreach (var item in newlist)
                    {
                        Total = Total + item.ItemGroupMoney;
                    }
                }
                //textEditRS.Text = dto.PersonAmount == null ? "0" : dto.PersonAmount.ToString();
                textEditRS.Text = _customerAppService.GetNumber(new EntityDto<Guid> { Id = dto.Id }).ToString();
                gridControl2.DataSource = newlist.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder).ToList();
                if (dto.CostType == (int)PayerCatType.ClientCharge || dto.CostType == 0)
                    radioGroupZFFS.SelectedIndex = 0;
                if (dto.CostType == (int)PayerCatType.PersonalCharge)
                    radioGroupZFFS.SelectedIndex = 1;
                if (dto.CostType == 11)
                    radioGroupZFFS.SelectedIndex = 2;
                if (dto.CostType != (int)PayerCatType.ClientCharge && dto.CostType != (int)PayerCatType.PersonalCharge
                    && dto.CostType != (int)PayerCatType.FixedAmount && dto.CostType != 0)
                {
                    radioGroupZFFS.SelectedIndex = 0;
                }
                if (dto.JxType == (int)PayerCatType.ClientCharge)
                    radioGroupJXFS.SelectedIndex = 0;
                if (dto.JxType == (int)PayerCatType.PersonalCharge || dto.JxType == 0)
                    radioGroupJXFS.SelectedIndex = 1;
                if (dto.JxType == (int)PayerCatType.FixedAmount)
                    radioGroupJXFS.SelectedIndex = 2;
                textEditSW.EditValue = dto.SWjg??0;
                textQuotaMoney.EditValue = dto.QuotaMoney ?? 0;

                textEditZFXMHJ.Text = Total.ToString();
                //不管什么情况都实时计算
                //if (dto.TeamDiscountMoney != 0)
                //    textEditZFXMJG.Text = dto.TeamDiscountMoney.ToString(CultureInfo.InvariantCulture);
                //else
                textEditZFXMJG.Text = gridView2.Columns[4].SummaryItem.SummaryValue?.ToString();
                //if (dto.TeamDiscountMoney == 0)
                textEditFZHJ.EditValue = (Convert.ToDecimal(textEditZFXMJG.EditValue) * Convert.ToInt32(textEditRS.Text));
                //else
                //    textEditFZHJ.EditValue = (dto.TeamDiscountMoney * Convert.ToInt32(textEditRS.Text));
                //textEditZFXMPJZK.Text = dto.TeamDiscount == 0 ? "0" : dto.TeamDiscount.ToString();
                textEditJXXMPJZK.Text = dto.Jxzk == 0 ? "1" : dto.Jxzk.ToString(CultureInfo.InvariantCulture);
                textEditZFXMPJZK.Text = gridView2.Columns[2].SummaryItem.SummaryValue?.ToString();
                if (dto.TeamDiscount == 0 && Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue)!=0)
                    textEditPJZK.EditValue = Convert.ToDecimal(gridView2.Columns[4].SummaryItem.SummaryValue) / Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue);
                else
                    textEditPJZK.EditValue = dto.TeamDiscount == 0 ? 0 : dto.TeamDiscount;
                List<GroupMoneyDto> group = new List<GroupMoneyDto>();
                if (newlist != null)
                {
                    foreach (var item in newlist)
                    {
                        GroupMoneyDto groupdto = new GroupMoneyDto();
                        if (item.ItemGroup != null)
                        {
                            groupdto.MaxDiscount = item.ItemGroup.MaxDiscount;
                        }
                        groupdto.IsAddMinus = 0;
                        groupdto.ItemPrice = item.ItemGroupMoney;
                        groupdto.DiscountRate = item.Discount;
                        groupdto.PriceAfterDis = item.ItemGroupDiscountMoney;
                        group.Add(groupdto);
                        //group.Add(new GroupMoneyDto
                        //{
                        //    MaxDiscount = item.ItemGroup.MaxDiscount,
                        //    IsAddMinus = 0,
                        //    ItemPrice = item.ItemGroupMoney,
                        //    DiscountRate = item.Discount,
                        //    PriceAfterDis = item.ItemGroupDiscountMoney,
                        //});

                    }
                }
                if (creatOAApproValcsDto != null && creatOAApproValcsDto.DiscountRate!=null)
                { CurrentUser.Discount = creatOAApproValcsDto.DiscountRate.ToString(); }
                decimal decimals = _chargeAppService.MinMoney(new SeachChargrDto { ItemGroups = group, user = CurrentUser });
                labelControl4.EditValue = decimal.Round(decimals, 2, MidpointRounding.AwayFromZero);
            }
            catch (UserFriendlyException)
            {
                gridControl2.DataSource = null;
            }
        }
        private void textEditZFXMPJZK_Leave(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            //if (ZKdown == 1)
            //{
            //    ZKdown = 0; return;
            //}
            if (!string.IsNullOrWhiteSpace(textEditZFXMPJZK.EditValue.ToString().Trim()))
            {
                if (Convert.ToDouble(textEditZFXMPJZK.EditValue.ToString().Trim()) > 100)
                {
                    textEditZFXMPJZK.Text = "100";
                }
                if (decimal.Parse(textEditZFXMPJZK.EditValue.ToString().Trim()) < 0)
                {
                    dxErrorProvider.SetError(textEditZFXMPJZK, string.Format(Variables.Negative, "折扣率"));
                    textEditZFXMPJZK.Focus();
                    return;
                }
                //if (textEditZFXMPJZK.EditValue.ToString() == "0")
                //{
                //    textEditZFXMJG.EditValue = gridView2.Columns[4].SummaryItem.SummaryValue?.ToString();
                //    textEditFZHJ.EditValue = (Convert.ToDecimal(textEditZFXMJG.EditValue.ToString()) * Convert.ToInt32(textEditRS.Text));
                //    return;
                //}
                simpleButtonTB.Enabled = false;
                //折扣率
                decimal Decimal = decimal.Parse(textEditZFXMPJZK.EditValue.ToString());
                //未输入折扣率或输入打10折
                //if (Decimal == 1)
                //{
                //    textEditZFXMJG.EditValue.ToString() = gridView2.Columns[4].SummaryItem.SummaryValue?.ToString();
                //    textEditZFXMPJZK.Text = "100";
                //    if (!string.IsNullOrWhiteSpace(textEditRS.Text))
                //        textEditFZHJ.Text = decimal.Round((decimal.Parse(textEditZFXMJG.EditValue.ToString()) * int.Parse(textEditRS.Text)), 4, MidpointRounding.AwayFromZero).ToString();
                //    return;
                //}
                //获取单位分组项目
                var dto = gridControl2.GetDtoListDataSource<ClientTeamRegitemViewDto>().ToList();
                if (creatOAApproValcsDto != null)
                { CurrentUser.Discount = creatOAApproValcsDto.DiscountRate.ToString(); }
                //遍历分组项目 判断折扣率界限 得出分组项目折扣率、折扣后价格
                foreach (var item in dto)
                {
                    SeachChargrDto chargrDto = new SeachChargrDto();
                    //if (item.ItemGroup != null)//待完善
                    //   // chargrDto.ItemGroups = new List<GroupMoneyDto> { new GroupMoneyDto { ItemGroup = new GroupInfoDto { MaxDiscount = item.ItemGroup.MaxDiscount } } };
                    //else
                    //    chargrDto.ItemGroups = null;
                    chargrDto.ItemGroups = new List<GroupMoneyDto> { new GroupMoneyDto { MaxDiscount = item.ItemGroup.MaxDiscount } };
                    chargrDto.user = user;
                    chargrDto.ZKL = Decimal;
                    Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                    if (item.TbmItemGroupid == guid)
                    {
                        item.ItemGroupMoney = 0;
                        item.Discount = 1;
                        item.ItemGroupDiscountMoney = 0;
                    }
                    else
                    {
                        decimal decimals = _chargeAppService.MinGroupZKL(chargrDto);
                        item.Discount = decimals;
                        item.ItemGroupDiscountMoney = item.ItemGroupMoney * decimals;
                    }
                }
                gridControl2.DataSource = dto.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder).ToList();
                textEditZFXMJG.EditValue = decimal.Round(decimal.Parse(gridView2.Columns[4].SummaryItem.SummaryValue?.ToString()), 2, MidpointRounding.AwayFromZero);
                if (Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue) != 0)
                {
                    textEditPJZK.EditValue = Convert.ToDecimal(gridView2.Columns[4].SummaryItem.SummaryValue) / Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue);
                }
                    if (!string.IsNullOrWhiteSpace(textEditRS.Text))
                    textEditFZHJ.EditValue = decimal.Round((decimal.Parse(textEditZFXMJG.EditValue.ToString()) * int.Parse(textEditRS.Text)), 4, MidpointRounding.AwayFromZero).ToString();
            }
        }
        private void textEditZFXMJG_Leave(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            if (JGdown == 1)
            {
                JGdown = 0; return;
            }
            if (!string.IsNullOrWhiteSpace(textEditZFXMJG.EditValue.ToString().Trim()))
            {

                if (Convert.ToDecimal(textEditZFXMJG.EditValue.ToString().Trim()) >= decimal.Round(decimal.Parse(labelControl4.EditValue.ToString()), 2, MidpointRounding.AwayFromZero) || Convert.ToDecimal(textEditZFXMJG.EditValue.ToString().Trim()) == 0)
                {
                    if (Convert.ToDouble(textEditZFXMJG.EditValue) < 0)
                    {
                        if (Convert.ToDouble(textEditZFXMJG.EditValue) < 0)
                        {
                            dxErrorProvider.SetError(textEditZFXMJG, string.Format(Variables.Negative, "折后价格"));
                        }
                        return;
                    }
                    simpleButtonTB.Enabled = false;
                    textEditFZHJ.EditValue = (Convert.ToDecimal(textEditZFXMJG.EditValue.ToString()) * Convert.ToInt32(textEditRS.Text)).ToString();
                    var dto = gridControl2.GetDtoListDataSource<ClientTeamRegitemViewDto>().ToList();
                    List<GroupMoneyDto> group = new List<GroupMoneyDto>();
                    Guid index = new Guid();
                    foreach (var item in dto)
                    {
                        GroupMoneyDto groupdto = new GroupMoneyDto();
                        groupdto.Id = item.Id;
                        if (item.ItemGroup != null)
                        {
                            groupdto.MaxDiscount = item.ItemGroup.MaxDiscount;
                        }
                        groupdto.IsAddMinus = 0;
                        Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                        if (item.TbmItemGroupid == guid)
                        {
                            index = item.Id;
                            groupdto.ItemPrice = 0;
                            groupdto.DiscountRate = 1;
                            groupdto.PriceAfterDis = 0;
                        }
                        else
                        {
                            groupdto.ItemPrice = item.ItemGroupMoney;
                            groupdto.DiscountRate = item.Discount;
                            groupdto.PriceAfterDis = item.ItemGroupDiscountMoney;
                        }
                        group.Add(groupdto);
                    }
                    var list = _chargeAppService.MinCusGroupMoney(new SeachChargrDto
                    {
                        ItemGroups = group,
                        user = CurrentUser,
                        PayMoney = Convert.ToDecimal(textEditZFXMJG.EditValue.ToString().Trim()),
                        minMoney = 0,
                        minDiscountMoney = 0
                    });
                    var i = 0;
                    foreach (var item in list)
                    {
                        if (index != Guid.Empty && item.Id== index)
                        {
                            gridView2.SetRowCellValue(i, gridColumn8, Math.Round(item.ItemPrice, 2));
                        }
                      
                        gridView2.SetRowCellValue(i, gridView2.Columns[2], Math.Round(item.DiscountRate,4));
                        gridView2.SetRowCellValue(i, gridView2.Columns[4], Math.Round(item.PriceAfterDis, 2, MidpointRounding.AwayFromZero));
                        i++;
                        if (i > dto.Count)
                        {
                            return;
                        }
                    }
                    textEditZFXMPJZK.Text = gridView2.Columns[2].SummaryItem.SummaryValue?.ToString();
                    textEditPJZK.EditValue = Convert.ToDecimal(gridView2.Columns[4].SummaryItem.SummaryValue) / Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue);
                }
                else
                {
                    dxErrorProvider.SetError(textEditZFXMJG, string.Format(Variables.GreaterThanTips, "最低价格", "折后价格"));
                    textEditZFXMJG.Focus();
                    return;
                }

            }
        }

        private void simpleButtonBC_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();
            var dto = gridControl1.GetSelectedRowDtos<ClientTeamInfosDto>().FirstOrDefault();
            try
            {
                if (string.IsNullOrEmpty(textEditZFXMPJZK.EditValue?.ToString()))
                {
                    textEditZFXMPJZK.EditValue = 0;
                }
                var FZState = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = dto.ClientRegId });
                if (FZState == 1)
                {
                    ShowMessageBoxInformation("单位已封账不能更改收费设置");
                    return;
                }
                if (decimal.Parse(textEditZFXMPJZK.EditValue.ToString()) < 0)
                {
                    dxErrorProvider.SetError(textEditZFXMPJZK, string.Format(Variables.Negative, "折扣率"));
                    textEditZFXMPJZK.Focus();
                    return;
                }
                if (decimal.Parse(textEditZFXMJG.EditValue.ToString()) < 0)
                {
                    dxErrorProvider.SetError(textEditZFXMJG, string.Format(Variables.Negative, "价格"));
                    textEditZFXMJG.Focus();
                    return;
                }
                if (decimal.Parse(textEditJXXMPJZK.EditValue.ToString()) < 0)
                {
                    dxErrorProvider.SetError(textEditJXXMPJZK, string.Format(Variables.Negative, "折扣率"));
                    textEditJXXMPJZK.Focus();
                    return;
                }
                if (decimal.Parse(textEditJXXMPJZK.EditValue.ToString()) > 1)
                {
                    dxErrorProvider.SetError(textEditJXXMPJZK, string.Format(Variables.GreaterThanTips, "折扣率", "100%"));
                    textEditJXXMPJZK.Focus();
                    return;
                }
                if (Convert.ToDecimal(textEditZFXMJG.EditValue.ToString().Trim()) < decimal.Parse(labelControl4.EditValue.ToString()) && Convert.ToDecimal(textEditZFXMJG.EditValue.ToString().Trim()) != 0)
                {
                    dxErrorProvider.SetError(textEditZFXMJG, string.Format(Variables.GreaterThanTips, "最低价格", "折后价格"));
                    textEditZFXMJG.Focus();
                    return;
                }
                //textEditZFXMPJZK
                if (radioGroupJXFS.EditValue==null)
                {
                    MessageBox.Show("请选择加项方式");
                    return;
                }


                //团体收费审核
                //设置为团体收费审核必须审核后才能添加单位
                if (createClientXKSetDto != null && createClientXKSetDto.ZKType == 1)
                {
                   
                    SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                    searchOAApproValcsDto.ClientInfoId = NClientID;
                    var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                    if (zk.Count == 0)
                    {
                        MessageBox.Show("单位折扣方式为单位折扣审核，该单位尚未设置折扣，不能保存收费设置！");
                        return;
                    }
                    if (creatOAApproValcsDto.AppliState == (int)OAApState.NoAp)
                    {
                        MessageBox.Show("该单位存在未审核的折扣申请，不能保存收费设置！");
                        return;
                    }
                    if (creatOAApproValcsDto.AppliState == (int)OAApState.reAp)
                    {
                        MessageBox.Show("该单位折扣申请被拒绝，不能保存收费设置！");
                        return;
                    }
                }


                ClientTeamInfosDto client = _clientRegAppService.ClientTeamInfoUpdate(new ClientTeamInfosDto
                {
                    Id = dto.Id,
                    TeamMoney = decimal.Parse(textEditZFXMHJ.EditValue.ToString()),
                    TeamDiscount = decimal.Round(decimal.Parse(textEditPJZK.EditValue.ToString()), 4, MidpointRounding.AwayFromZero),
                    TeamDiscountMoney = decimal.Parse(textEditZFXMJG.EditValue.ToString().Trim()),
                    CostType = (int)radioGroupZFFS.EditValue,
                    JxType = (int)radioGroupJXFS.EditValue,
                    Jxzk = decimal.Parse(textEditJXXMPJZK.EditValue.ToString()),
                    SWjg = decimal.Parse(textEditSW.EditValue?.ToString()),
                    QuotaMoney = decimal.Parse(textQuotaMoney.EditValue?.ToString()),
                    

                        });
                if (client != null)
                {
                    dto.TeamDiscount = decimal.Parse(textEditZFXMPJZK.EditValue.ToString());
                    dto.TeamMoney = decimal.Parse(textEditZFXMHJ.EditValue.ToString());
                    dto.TeamDiscountMoney = decimal.Parse(textEditZFXMJG.EditValue.ToString().Trim());
                    dto.CostType = (int)radioGroupZFFS.EditValue;
                    dto.JxType = (int)radioGroupJXFS.EditValue;
                    dto.Jxzk = decimal.Parse(textEditJXXMPJZK.EditValue.ToString());
                    dto.SWjg = decimal.Parse(textEditSW.EditValue.ToString());
                    dto.QuotaMoney= decimal.Parse(textQuotaMoney.EditValue.ToString());
                    var Regdto = gridControl2.GetDtoListDataSource<ClientTeamRegitemViewDto>().ToList();
                    List<CreateClientTeamRegItemDto> regdto = new List<CreateClientTeamRegItemDto>();
                    foreach (var item in Regdto)
                    {
                        item.PayerCatType = client.CostType;
                        regdto.Add(new CreateClientTeamRegItemDto { Id = item.Id, Discount = item.Discount, ItemGroupDiscountMoney = item.ItemGroupDiscountMoney, PayerCatType = client.CostType, TbmItemGroupId= item.TbmItemGroupid });
                    }
                    gridControl2.DataSource = Regdto.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder).ToList();
                    _clientRegAppService.ClientTeamRegItemUpdate(regdto);
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = clientBM;
                    createOpLogDto.LogName = clientName;
                    createOpLogDto.LogText = "保存收费设置,分组：" + dto.TeamName;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    commonAppService.SaveOpLog(createOpLogDto);
                    ShowMessageBoxInformation("保存成功");

                    focerowchanged();

                    simpleButtonTB.Enabled = true;
                }
                simpleButtonTB_Click(sender, e);
               // simpleButtonTB.PerformClick();
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void simpleButtonTB_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var id = gridControl1.GetSelectedRowDtos<ClientTeamInfosDto>().FirstOrDefault().Id;
            var Regdto = gridControl2.GetDtoListDataSource<ClientTeamRegitemViewDto>().ToList();
            if (id != null)
            {
                   // _clientRegAppService.GetClientTeamById(new SearchClientTeamIdorTeamRegDto { Id = id, ClientTeamRegItem = Regdto });
                    _clientRegAppService.UpClientCostById(new SearchClientTeamIdorTeamRegDto { Id = id, ClientTeamRegItem = Regdto }); 
                    ShowMessageBoxInformation("同步成功");
            }
            });
        }

        private void textEditZFXMPJZK_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //private void textEditZFXMPJZK_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter && textEditZFXMPJZK.EditValue != null)
        //    {
        //        textEditZFXMPJZK_Leave(sender, e);
        //        ZKdown = 1;
        //        simpleButtonBC.Focus();
        //    }
        //}

        private void textEditZFXMJG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEditZFXMJG.EditValue != null)
            {
                textEditZFXMJG_Leave(sender, e);
                JGdown = 1;
                simpleButtonBC.Focus();
            }
        }

        private void textEditZFXMJG_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "Discount")
            {
                double total = Convert.ToDouble(gridColumn9.SummaryItem.SummaryValue);

                e.TotalValue = total * 100 + "%";
            }
        }

        private void sBClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textEditJXXMPJZK_Leave(object sender, EventArgs e)
        {
            if (creatOAApproValcsDto != null && textEditJXXMPJZK.EditValue!=null)
            { var zdzk = creatOAApproValcsDto.AddDiscountRate;

                var zk = decimal.Parse(textEditJXXMPJZK.EditValue.ToString());
                if (zk < zdzk)
                {
                    MessageBox.Show("加项折扣必须小于单位折扣设置加项折扣");
                    textEditJXXMPJZK.EditValue = zdzk;
                    return;
                }
            }
        }

        private void textEditZFXMPJZK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditZFXMPJZK.Text))
            {
                dxErrorProvider.ClearErrors();
                //if (ZKdown == 1)
                //{
                //    ZKdown = 0; return;
                //}
                if (!string.IsNullOrWhiteSpace(textEditZFXMPJZK.EditValue.ToString().Trim()))
                {
                    if (Convert.ToDouble(textEditZFXMPJZK.EditValue.ToString().Trim()) > 100)
                    {
                        textEditZFXMPJZK.Text = "100";
                    }
                    if (decimal.Parse(textEditZFXMPJZK.EditValue.ToString().Trim()) < 0)
                    {
                        dxErrorProvider.SetError(textEditZFXMPJZK, string.Format(Variables.Negative, "折扣率"));
                        textEditZFXMPJZK.Focus();
                        return;
                    }
                    //if (textEditZFXMPJZK.EditValue.ToString() == "0")
                    //{
                    //    textEditZFXMJG.EditValue = gridView2.Columns[4].SummaryItem.SummaryValue?.ToString();
                    //    textEditFZHJ.EditValue = (Convert.ToDecimal(textEditZFXMJG.EditValue.ToString()) * Convert.ToInt32(textEditRS.Text));
                    //    return;
                    //}
                    simpleButtonTB.Enabled = false;
                    //折扣率
                    decimal Decimal = decimal.Parse(textEditZFXMPJZK.EditValue.ToString());
                    //未输入折扣率或输入打10折
                    //if (Decimal == 1)
                    //{
                    //    textEditZFXMJG.EditValue.ToString() = gridView2.Columns[4].SummaryItem.SummaryValue?.ToString();
                    //    textEditZFXMPJZK.Text = "100";
                    //    if (!string.IsNullOrWhiteSpace(textEditRS.Text))
                    //        textEditFZHJ.Text = decimal.Round((decimal.Parse(textEditZFXMJG.EditValue.ToString()) * int.Parse(textEditRS.Text)), 4, MidpointRounding.AwayFromZero).ToString();
                    //    return;
                    //}
                    //获取单位分组项目
                    var dto = gridControl2.GetDtoListDataSource<ClientTeamRegitemViewDto>().ToList();
                    if (creatOAApproValcsDto != null)
                    { CurrentUser.Discount = creatOAApproValcsDto.DiscountRate.ToString(); }
                    //遍历分组项目 判断折扣率界限 得出分组项目折扣率、折扣后价格
                    foreach (var item in dto)
                    {
                        SeachChargrDto chargrDto = new SeachChargrDto();
                        //if (item.ItemGroup != null)//待完善
                        //   // chargrDto.ItemGroups = new List<GroupMoneyDto> { new GroupMoneyDto { ItemGroup = new GroupInfoDto { MaxDiscount = item.ItemGroup.MaxDiscount } } };
                        //else
                        //    chargrDto.ItemGroups = null;
                        chargrDto.ItemGroups = new List<GroupMoneyDto> { new GroupMoneyDto { MaxDiscount = item.ItemGroup.MaxDiscount } };
                        chargrDto.user = user;
                        chargrDto.ZKL = Decimal;
                        Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                        if (item.TbmItemGroupid == guid)
                        {
                            item.ItemGroupMoney = 0;
                            item.Discount = 1;
                            item.ItemGroupDiscountMoney = 0;
                        }
                        else
                        {
                            decimal decimals = _chargeAppService.MinGroupZKL(chargrDto);
                            item.Discount = decimals;
                            item.ItemGroupDiscountMoney = item.ItemGroupMoney * decimals;
                        }
                    }
                    gridControl2.DataSource = dto.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder).ToList();
                    textEditZFXMJG.EditValue = decimal.Round(decimal.Parse(gridView2.Columns[4].SummaryItem.SummaryValue?.ToString()), 2, MidpointRounding.AwayFromZero);
                    if (Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue) != 0)
                    {
                        textEditPJZK.EditValue = Convert.ToDecimal(gridView2.Columns[4].SummaryItem.SummaryValue) / Convert.ToDecimal(gridView2.Columns[3].SummaryItem.SummaryValue);
                    }
                    if (!string.IsNullOrWhiteSpace(textEditRS.Text))
                        textEditFZHJ.EditValue = decimal.Round((decimal.Parse(textEditZFXMJG.EditValue.ToString()) * int.Parse(textEditRS.Text)), 4, MidpointRounding.AwayFromZero).ToString();
                }
            }
        }
    }
}
