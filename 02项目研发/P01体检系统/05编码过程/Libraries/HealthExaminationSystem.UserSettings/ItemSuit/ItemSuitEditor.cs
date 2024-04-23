using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemSuit
{
    public partial class ItemSuitEditor : UserBaseForm
    {
        private readonly IItemSuitAppService service = new ItemSuitAppService();
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        private IChargeAppService _chargeAppService = new ChargeAppService();
        public FullItemSuitDto _Model { get; private set; }
        private List<SimpleItemGroupDto> groupls = new List<SimpleItemGroupDto>();

        public ItemSuitEditor()
        {
            InitializeComponent();

            lueSex.SetClearButton();
            lueMaritalStatus.SetClearButton();
            lueExaminationType.SetClearButton();
            lueItemSuitType.SetClearButton();

            lueSex.EditValue = (int)Sex.GenderNotSpecified;
            lueMaritalStatus.EditValue = (int)MarrySate.Unstated;
            lueItemSuitType.EditValue = (int)ItemSuitType.Suit;

            if (_Model == null) _Model = new FullItemSuitDto();           
        }
        
        public ItemSuitEditor(FullItemSuitDto model) : this()
        {
            _Model = model;
            LoadData();
        }

        #region 事件
        private void ItemSuitEdit_Load(object sender, EventArgs e)
        {
            LoadControlData();
        }

        private void sbOk_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void sbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void sbDel_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void gvItemGroupWait_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Add();
            }
        }

        private void gvItemGroupSelected_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Del();
            }
        }

        private void gvItemGroupSelected_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gvItemGroupSelectedSuitgrouprate)
            {
                var dto = gvItemGroupSelected.GetRow(e.RowHandle) as ItemSuitItemGroupContrastFullDto;
                if (dto.ItemGroup.MaxDiscount != null && dto.Suitgrouprate < dto.ItemGroup.MaxDiscount)
                {
                    dto.Suitgrouprate = dto.ItemGroup.MaxDiscount;
                }
                dto.PriceAfterDis = (dto.ItemPrice ?? 0) * (dto.Suitgrouprate ?? 0);
                CalcPrice();
            }
            else if (e.Column == gvItemGroupSelectedPriceAfterDis)
            {
                var dto = gvItemGroupSelected.GetRow(e.RowHandle) as ItemSuitItemGroupContrastFullDto;
                dto.Suitgrouprate = (dto.ItemPrice == null || dto.ItemPrice == 0) ? 0 : (dto.PriceAfterDis ?? 0) / (dto.ItemPrice);
                dto.Suitgrouprate = (dto.Suitgrouprate.Value * 100) / 100; // 进一法
                dto.Suitgrouprate = Math.Round(dto.Suitgrouprate.Value, 4);
                CalcPrice();
            }
        }

        // 行变化
        private void gvItemGroupSelected_RowCountChanged(object sender, EventArgs e)
        {
            CalcPrice();
        }

        // 助记码
        private void teItemSuitName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teHelpChar.Text))
                return;
            var name = teItemSuitName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    teHelpChar.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                teHelpChar.Text = string.Empty;
            }
        }

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() =>
            {
                lueExaminationType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ExaminationType);
            });
        }

        private void layoutControlGroup4_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() =>
            {
                gcWait.DataSource = DefinedCacheHelper.GetItemGroups();
            });
        }

        #endregion
        /// <summary>
        /// 加载控件数据
        /// </summary>
        private void LoadControlData()
        {
            lueSex.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lueMaritalStatus.Properties.DataSource = MarrySateHelper.GetMarrySateModelsForItemInfo();
            lueItemSuitType.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ItemSuitType));
           var exatype=  DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ExaminationType);
            if (Variables.ISZYB == "2")
            {
                exatype = exatype.Where(o=>o.Text.Contains("职业健康")).ToList();
            }
            lueExaminationType.Properties.DataSource = exatype;
            gcWait.DataSource = DefinedCacheHelper.GetItemGroups().ToList();
            groupls = DefinedCacheHelper.GetItemGroups().ToList();                                
        }
        /// <summary>
        /// 添加选择
        /// </summary>
        public void Add()
        {
            var dtos = gcWait.GetSelectedRowDtos<SimpleItemGroupDto>();
            if (dtos.Count == 0) return;
            //gcSelected.AddDtoListItem(dtos, true);
            var itemSuitItemGroups = dtos.Select(m => new ItemSuitItemGroupContrastFullDto
            {
                ItemGroup = m,
                ItemGroupId = m.Id,
                ItemPrice = m.Price,
                Suitgrouprate = 1,
                PriceAfterDis = m.Price,
            }).ToList();

            var list = gcSelected.GetDtoListDataSource<ItemSuitItemGroupContrastFullDto>();
            if (list == null)
            {
                list = new List<ItemSuitItemGroupContrastFullDto>();
                gcSelected.DataSource = list;
            }
            itemSuitItemGroups.RemoveAll(m => list.Any(s => s.ItemGroup.Id == m.ItemGroup.Id));
            list.AddRange(itemSuitItemGroups);
            gcSelected.RefreshDataSource();
        }
        /// <summary>
        /// 移除选择
        /// </summary>
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<ItemSuitItemGroupContrastFullDto>();
            if (dtos.Count == 0) return;
            //gcSelected.RemoveDtoListItem(dtos);
            gcSelected.GetDtoListDataSource<ItemSuitItemGroupContrastFullDto>()?.RemoveAll(m => dtos.Any(i => i.ItemGroupId == m.ItemGroupId));
            gcSelected.RefreshDataSource();
        }
        /// <summary>
        /// 计算价格
        /// </summary>
        private void CalcPrice()
        {
            var list = gcSelected.GetDtoListDataSource<ItemSuitItemGroupContrastFullDto>();
            var itemPrice = list.Sum(m => m.ItemPrice);
            var priceAfterDis = list.Sum(m => m.PriceAfterDis);
            tePrice.Text = priceAfterDis.ToString(); // 单价
            teCostPrice.Text = itemPrice.ToString(); // 成本价
            teCjPrice.Text = (priceAfterDis - itemPrice).ToString(); // 结果差价
        }
        /// <summary>
        /// 确定
        /// </summary>
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            // 套餐名称
            _Model.ItemSuitName = teItemSuitName.Text.Trim();
            if (string.IsNullOrEmpty(_Model.ItemSuitName))
            {
                dxErrorProvider.SetError(teItemSuitName, string.Format(Variables.MandatoryTips, "套餐名称"));
                teItemSuitName.Focus();
                return false;
            }
            // 组合列表
            _Model.ItemSuitItemGroups = gcSelected.GetDtoListDataSource<ItemSuitItemGroupContrastFullDto>();
            if (_Model.ItemSuitItemGroups == null || _Model.ItemSuitItemGroups.Count == 0)
            {
                ShowMessageBoxWarning("未选择项目组合，套餐没有意义！");
                return false;
            }
            if (checkEditEndDate.Checked == true && string.IsNullOrEmpty(DateEndDate.EditValue?.ToString()))
            {
                ShowMessageBoxWarning("请设置有效期！");
                return false;

            }
            // 助记码
            _Model.HelpChar = teHelpChar.Text.Trim();
            // 适用性别
            _Model.Sex = (int?)lueSex.EditValue;
            // 最小年龄
            _Model.MinAge = int.TryParse(teMinAge.Text.Trim(), out int minAge) ? (int?)minAge : null;
            // 最大年龄
            _Model.MaxAge = int.TryParse(teMaxAge.Text.Trim(), out int maxAge) ? (int?)maxAge : null;
            // 单价
            _Model.Price = decimal.TryParse(tePrice.EditValue?.ToString(), out decimal price) ? (decimal?)price : null;
            // 成本价
            _Model.CostPrice = decimal.TryParse(teCostPrice.EditValue?.ToString(), out decimal costPrice) ? (decimal?)costPrice : null;
            // 结果差价
            _Model.CjPrice = decimal.TryParse(teCjPrice.EditValue?.ToString(), out decimal cjPrice) ? (decimal?)cjPrice : null;
            // 体检类别字典
            _Model.ExaminationType = (int?)lueExaminationType.EditValue;
            //  1基础套餐2组单3加项
            if (lueItemSuitType.EditValue == null)
            {
                _Model.ItemSuitType = null;
            }
            else
            {
                _Model.ItemSuitType = (int)lueItemSuitType.EditValue;
            }
            // 是否启用
            _Model.Available = ceAvailable.Checked ? 1 : 2;
            // 注意事项
            _Model.Notice = meNotice.Text;
            // 备注说明
            _Model.Remarks = meRemarks.Text;

            // 婚姻状况
            _Model.MaritalStatus = (int?)lueMaritalStatus.EditValue;
            // 危害因素
            _Model.RiskName = meRiskName.Text;
            // 是否备孕 1备孕2不备孕
            _Model.ConceiveStatus = ceConceiveStatus.Checked ? 1 : 2;
            _Model.GWBM = textEditGWB.Text;
            if (checkEditEndDate.Checked == true)
            {
                _Model.IsendDate = 1;
                _Model.endDate = DateEndDate.DateTime;
            }
            else
            { _Model.IsendDate = 0; }

            bool res = false;
            AutoLoading(() =>
            {
                ItemSuitInput input = new ItemSuitInput
                {
                    ItemSuit = ModelHelper.CustomMapTo2<FullItemSuitDto, CreateOrUpdateItemSuitDto>(_Model),
                    ItemSuitItemGroups = new List<ItemSuitItemGroupContrastDto>()
                };
                input.ItemSuitItemGroups.AddRange(_Model.ItemSuitItemGroups);
                FullItemSuitDto dto = null;
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                }
                else
                {
                    dto = service.Edit(input);
                }
                dto.ItemSuitItemGroups = _Model.ItemSuitItemGroups;
                _Model = dto;
                res = true;
            }, Variables.LoadingSaveing);
            return res;
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (_Model == null)
            {               
                return;
            }
            if (_Model.Id != null)
            {
                layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            textEditGWB.Text = _Model.GWBM;
            // 套餐名称
            teItemSuitName.Text = _Model.ItemSuitName;
            // 助记码
            teHelpChar.Text = _Model.HelpChar;
            // 适用性别
            lueSex.EditValue = _Model.Sex;
            // 最小年龄
            teMinAge.Text = _Model.MinAge?.ToString();
            // 最大年龄
            teMaxAge.Text = _Model.MaxAge?.ToString();
            // 单价
            tePrice.Text = _Model.Price?.ToString();
            // 成本价
            teCostPrice.Text = _Model.CostPrice?.ToString();
            // 结果差价
            teCjPrice.Text = _Model.CjPrice?.ToString();
            // 体检类别字典
            lueExaminationType.EditValue = _Model.ExaminationType;
            if (_Model.IsendDate.HasValue && _Model.IsendDate == 1)
            {
                checkEditEndDate.Checked = true;
                if (_Model.endDate.HasValue)
                {
                    DateEndDate.EditValue = _Model.endDate;
                }
            }
            else
            {
                checkEditEndDate.Checked = false;
            }
            //  1基础套餐2组单3加项
            if (_Model.ItemSuitType == null)
            {
                lueItemSuitType.EditValue = null;
            }
            else
            {
                lueItemSuitType.EditValue = (ItemSuitType)_Model.ItemSuitType;
            }
            // 是否启用
            ceAvailable.Checked = _Model.Available == 1;
            // 注意事项
            meNotice.Text = _Model.Notice;
            // 备注说明
            meRemarks.Text = _Model.Remarks;

            // 婚姻状况
            lueMaritalStatus.EditValue = _Model.MaritalStatus;
            // 危害因素
            meRiskName.Text = _Model.RiskName;
            // 是否备孕 1备孕2不备孕
            ceConceiveStatus.Checked = _Model.ConceiveStatus == 1;

            // 已选组合列表
            gcSelected.DataSource = _Model.ItemSuitItemGroups;
        }

        private void tePrice_Leave(object sender, EventArgs e)
        {
            SeachChargrDto seachChargrDto = new SeachChargrDto();
            var groups = gcSelected.GetDtoListDataSource<ItemSuitItemGroupContrastFullDto>();
            var ItemGroupsls = new List<GroupMoneyDto>();
            var guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            Guid mlid = new Guid();
            foreach (ItemSuitItemGroupContrastFullDto group in groups)
            {
                if (group.ItemGroupId == guid)
                {
                    mlid = group.Id;
                    GroupMoneyDto groupMoneyDto = new GroupMoneyDto();
                    groupMoneyDto.Id = group.Id;
                    groupMoneyDto.IsAddMinus = 1;
                    groupMoneyDto.MaxDiscount = group.ItemGroup.MaxDiscount;
                    groupMoneyDto.ItemPrice = 0;
                    groupMoneyDto.PriceAfterDis = 0;
                    groupMoneyDto.DiscountRate = 1;
                    ItemGroupsls.Add(groupMoneyDto);
                }
                else
                {
                   
                    GroupMoneyDto groupMoneyDto = new GroupMoneyDto();
                    groupMoneyDto.Id = group.Id;
                    groupMoneyDto.IsAddMinus = 1;
                    groupMoneyDto.MaxDiscount = group.ItemGroup.MaxDiscount;
                    groupMoneyDto.ItemPrice = group.ItemPrice.Value;
                    groupMoneyDto.PriceAfterDis = group.ItemPrice.Value;
                    groupMoneyDto.DiscountRate = group.PriceAfterDis.Value;
                    ItemGroupsls.Add(groupMoneyDto);
                }
            }
            decimal my = ItemGroupsls.Sum(o => o.PriceAfterDis);
            seachChargrDto.ItemGroups = ItemGroupsls;
            seachChargrDto.user = CurrentUser;
            seachChargrDto.PayMoney = Convert.ToDecimal(tePrice.EditValue.ToString().Trim());
            seachChargrDto.minMoney = 0;
            seachChargrDto.minDiscountMoney = 0;
            var list = _chargeAppService.MinCusGroupMoney(seachChargrDto);           
            var i = 0;
            foreach (var item in list)
            {
                //gvItemGroupSelected.SetRowCellValue(i, gvItemGroupSelectedSuitgrouprate, item.DiscountRate);
                //gvItemGroupSelected.SetRowCellValue(i, gvItemGroupSelectedPriceAfterDis, decimal.Round(item.PriceAfterDis, 4, MidpointRounding.AwayFromZero));
                //gvItemGroupSelected.SetRowCellValue(i, gvItemGroupSelectedSuitgrouprate, item.DiscountRate);
                if (mlid != Guid.Empty && item.Id == mlid)
                {
                    groups[i].ItemPrice = Math.Round(item.ItemPrice, 2);
                }
                groups[i].Suitgrouprate= Math.Round(item.DiscountRate, 4);
                groups[i].PriceAfterDis = Math.Round(item.PriceAfterDis, 2); 
                i++;               
            }
            teCostPrice.EditValue = groups.Sum(o=>o.ItemPrice);
            gcSelected.RefreshDataSource();



        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }
        public void BindgrdOptionalItemGroup()
        {
            if (searchControl1.Text != "")
            {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<SimpleItemGroupDto>();
                output = groupls.Where(o => o.ItemGroupName.Contains(searchControl1.Text) || o.HelpChar.ToUpper().Contains(strup)).ToList();
                gcWait.DataSource = output;
            }
            else
            {
                gcWait.DataSource = groupls;
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new CopyItemSuit())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var griddatda = gcSelected.DataSource as List<ItemSuitItemGroupContrastFullDto>;
                    if (griddatda == null)
                    {
                        griddatda = new List<ItemSuitItemGroupContrastFullDto>();
                    }
                    var result1 = service.GetAllSuitItemGroups(new EntityDto<Guid>
                    {
                        
                        Id = frm.occpast.Id
                    });
                    if (result1 != null)
                    {
                        gcSelected.DataSource = result1;
                        gcSelected.RefreshDataSource();
                        gcSelected.Refresh();
                    }
                }
            }        
        }

        private void DateEndDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
                 

            }
        }
    }
}
