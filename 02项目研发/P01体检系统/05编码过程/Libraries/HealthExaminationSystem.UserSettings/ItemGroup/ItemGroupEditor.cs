using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemGroup
{
    public partial class ItemGroupEditor : UserBaseForm
    {
        private readonly ICommonAppService common = new CommonAppService();
        private readonly IItemGroupAppService service = new ItemGroupAppService();
        private readonly ICustomerAppService _CustomerAppService= new CustomerAppService();
        public Guid DepartmentId { get; set; }
        public FullItemGroupDto _Model { get; private set; }
        List<ItemInfoSimpleDto> curItemls = new List<ItemInfoSimpleDto>();

        List<priceSynDto> priceSynDtos = new List<priceSynDto>();
        /// <summary>
        /// 新增
        /// </summary>
        public ItemGroupEditor()
        {
            InitializeComponent();

            //lueDepartment.SetClearButton();
            //lueSex.SetClearButton();
            lueSpecimenType.SetClearButton();
            lueChart.SetClearButton();

            lueSex.EditValue = (int)Sex.GenderNotSpecified;
            
            if (_Model == null) _Model = new FullItemGroupDto();
            var query = service.GetMaxOrderNum();
            txtOrderNum.EditValue = query + 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="model"></param>
        public ItemGroupEditor(FullItemGroupDto model) : this()
        {
            _Model = model;
            if(_Model != null) lueDepartment.Enabled = false;
            LoadData();
        }

        #region 事件
        private void ItemGroupEdit_Load(object sender, EventArgs e)
        {
            LoadControlData();
            //loadHis();
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

        private void gvItemInfoWait_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Add();
            }
        }

        private void gvItemInfoSelected_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                Del();
            }
        }

        // 助记码
        private void teItemGroupName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teHelpChar.Text))
            {
                if (teItemGroupName.Text == _Model.ItemGroupName)
                    return;
            }
            var name = teItemGroupName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var result = common.GetHansBrief(new ChineseDto { Hans = name });
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

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() =>
            {
                lueDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments();
                lueSpecimenType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.SpecimenType);
                lueChart.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChargeCategory);
                lookUpEditInspectionType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.InspectionType);
                //类别
                lookUpEditType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChckType);

            });
        }

        private void layoutControlGroup3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AutoLoading(() =>
            {
                LoadWaitData(true);
            });
        }

        // 切换科室先判断已经选择的项目
        private void lueDepartment_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.OldValue == null)
            {
                return;
            }

            var sels = gcSelected.GetDtoListDataSource<ItemInfoSimpleDto>();
            if(sels != null && sels.Count > 0 && (Guid)e.NewValue != sels[0].Department.Id)
            {
                ShowMessageBoxWarning("当前科室已经选择了项目。");
                e.Cancel = true;
            }
        }
        // 切换科室筛选项目
        private void lueDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadWaitData();
        }

        private void LoadWaitData(bool isReload = false)
        {
            //检查方法
            lookUpEditInspectionType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.InspectionType);
            //类别
            lookUpEditType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ChckType);

            var list = DefinedCacheHelper.GetItemInfos().Where(p=>p.IsActive!= (int)InvoiceState.Discontinuation).ToList();
            if (lueDepartment.EditValue != null)
            {
                list = list.Where(m => m.Department?.Id == (Guid)lueDepartment.EditValue ).ToList();
               var depart= lueDepartment.GetSelectedDataRow() as TbmDepartmentDto;
                if (depart != null && depart.Category.Contains("检验"))
                {
                    lookUpEditNotice.Properties.DataSource= DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.TestNotice);
                }
                else
                { lookUpEditNotice.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.InspectNotice); }
            }
            curItemls = list.ToList();
            gcWait.DataSource = list;
           //申请单类型


        }

        #endregion

        #region 处理

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        private void LoadControlData()
        {
            lueDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            lueSex.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lueSpecimenType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o=>o.Type== "SpecimenType").ToList();
            lueChart.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ChargeCategory").ToList(); //(BasicDictionaryType.ChargeCategory);
            if (DepartmentId != Guid.Empty)
            {
                lueDepartment.EditValue = DepartmentId;
            }
            LoadWaitData();
        }

        public void Add()
        {
            if(lueDepartment.EditValue == null)
            {
                ShowMessageBoxWarning("当前没有选择科室。");
                return;
            }
            lueDepartment.Enabled = false;
            var dtos = gcWait.GetSelectedRowDtos<ItemInfoSimpleDto>();
            if (dtos.Count == 0) return;
            gcSelected.AddDtoListItem(dtos, true);          
            var dt = gcSelected.GetDtoListDataSource<ItemInfoSimpleDto>();
            if (dt.Count > 0)
            {
                var a = dt.Where(o => o.Name.Contains("核酸")).ToList();
                if (a.Count() >0)
                {
                    foreach (var r in dt)
                    {
                        if (r.Name.Contains("核酸"))
                        {
                            checkEdit1.Checked = true;
                        }
                        

                    }
                }
                else
                {
                    checkEdit1.Checked = false;
                }

            }
            if (dt.Count() <= 0)
            {
                checkEdit1.Checked = false;
            }
        }
        public void Del()
        {
            var dtos = gcSelected.GetSelectedRowDtos<ItemInfoSimpleDto>();
            if (dtos.Count == 0) return;
            gcSelected.RemoveDtoListItem(dtos);
            if (gvItemInfoSelected.RowCount == 0) lueDepartment.Enabled = true;
            var dt = gcSelected.GetDtoListDataSource<ItemInfoSimpleDto>();
            if (dt.Count > 0)
            {
                var a = dt.Where(o => o.Name.Contains("核酸")).ToList();
                if (a.Count() > 0)
                {
                    foreach (var r in dt)
                    {
                        if (r.Name.Contains("核酸"))
                        {
                            checkEdit1.Checked = true;
                        }
                    }
                }
                else
                {
                    checkEdit1.Checked = false;
                }
            }
            if (dt.Count() <= 0)
            {
                checkEdit1.Checked = false;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            // 科室
            if (lueDepartment.EditValue != null)
            {
                _Model.DepartmentId = (Guid)lueDepartment.EditValue;
                var dept= (TbmDepartmentDto)lueDepartment.GetSelectedDataRow();
                DepartmentSimpleDto departmentSimple = new DepartmentSimpleDto();                
                departmentSimple.DepartmentBM = dept.DepartmentBM;
                departmentSimple.HelpChar = dept.HelpChar;
                departmentSimple.Id = dept.Id;
                departmentSimple.Name = dept.Name;
                departmentSimple.OrderNum = dept.OrderNum;
                departmentSimple.WBCode = dept.WBCode;
                _Model.Department = departmentSimple;
            }
            else
            {
                dxErrorProvider.SetError(lueDepartment, string.Format(Variables.MandatoryTips, "科室"));
                lueDepartment.Focus();
                return false;
            }
            // 项目组合名称
            _Model.ItemGroupName = teItemGroupName.Text.Trim();
            if (string.IsNullOrEmpty(_Model.ItemGroupName))
            {
                dxErrorProvider.SetError(teItemGroupName, string.Format(Variables.MandatoryTips, "组合名称"));
                teItemGroupName.Focus();
                return false;
            }
            // 项目列表
            _Model.ItemInfos = gcSelected.GetDtoListDataSource<ItemInfoSimpleDto>();
            if(_Model.ItemInfos == null || _Model.ItemInfos.Count == 0)
            {
                ShowMessageBoxWarning("未选择项目，组合没有意义！");
                return false;
            }
           

            // 助记码
            _Model.HelpChar = teHelpChar.Text.Trim();
            // 最小年龄
            _Model.MinAge = int.TryParse(teMinAge.Text.Trim(), out int minAge) ? (int?)minAge : null;
            // 最大年龄
            _Model.MaxAge = int.TryParse(teMaxAge.Text.Trim(), out int maxAge) ? (int?)maxAge : null;
            // 单价 最小折扣核算后的价格
            _Model.Price = decimal.TryParse(tePrice.EditValue?.ToString(), out decimal price) ? (decimal?)price : null;
            // 成本价
            _Model.CostPrice = decimal.TryParse(teCostPrice.EditValue?.ToString(), out decimal costPrice) ? (decimal?)costPrice : null;
            // 最大折扣
            _Model.MaxDiscount = decimal.TryParse(teMaxDiscount.EditValue?.ToString(), out decimal maxDiscount) ? (decimal?)maxDiscount : null;
            // 注意事项
            _Model.Notice = teNotice.Text;
            // 备注说明
            _Model.Remarks = teRemarks.Text;
            // 组合介绍
            _Model.ItemGroupExplain = teItemGroupExplain.Text;
            // 禁忌备注
            _Model.Taboo = teTaboo.Text;
            // 最大体检人数
            _Model.MaxCheckDay = int.TryParse(teMaxCheckDay.Text.Trim(), out int maxCheckDay) ? (int?)maxCheckDay : null;
            // 项目明细数量
            //_Model.ItemCount = _Model.ItemInfos.Count;
            // 适用性别
            _Model.Sex = (int?)lueSex.EditValue;
            // 收费类型
            if (lueChart.EditValue != null)
            {
                _Model.ChartCode = lueChart.EditValue.ToString();
                _Model.ChartName = lueChart.Text;
            }
            else
            {
                _Model.ChartCode = null;
                _Model.ChartName = null;
            }
            // 检验申请单类型
            if (!string.IsNullOrEmpty(lookUpEditNotice.EditValue?.ToString()))
            {
                _Model.NoticeCode = lookUpEditNotice.EditValue.ToString();
                _Model.NoticeName = lookUpEditNotice.Text;
            }
            else
            {
                _Model.NoticeCode = null;
                _Model.NoticeName = null;
            }
            // 检查方法
            if (!string.IsNullOrEmpty(lookUpEditInspectionType.EditValue?.ToString()))
            {
                 
                _Model.InspectionTypeCode = lookUpEditInspectionType.EditValue.ToString();
                _Model.InspectionTypeName = lookUpEditInspectionType.Text;
            }
            else
            {
                _Model.InspectionTypeCode = null;
                _Model.InspectionTypeCode = null;
            }
            // 检查方法
            if (!string.IsNullOrEmpty(lookUpEditType.EditValue?.ToString()))
            {
                _Model.CheckTypeCode = lookUpEditType.EditValue.ToString();
                _Model.CheckTypeName = lookUpEditType.Text;
            }
            else
            {
                _Model.CheckTypeCode = null;
                _Model.CheckTypeName = null;
            }
            // 收费项目 是否属于收费项目 0收费1不收费 默认 收费
            _Model.ISSFItemGroup = ceISSFItemGroup.Checked ? 0 : 1;
            // 标本类型 字典
            _Model.SpecimenType = (int?)lueSpecimenType.EditValue;
            // 是否用打条码  1是2不是
            _Model.BarState = ceBarState.Checked ? 1 : 2;
            // 是否隐私项目 1是2不是
            _Model.PrivacyState = cePrivacyState.Checked ? 1 : 2;
            // 是否抽血 1是2不是 
            _Model.DrawState = ceDrawState.Checked ? 1 : 2;
            // 餐前餐后 1餐前2餐后  
            _Model.MealState = ceMealState.Checked ? 1 : 2;
            // 是否妇检 1是2不是
            _Model.WomenState = ceWomenState.Checked ? 1 : 2;
            // 试管样式
            _Model.TubeType = teTubeType.Text.Trim();
            // 是否早餐
            _Model.Breakfast = ceBreakfast.Checked ? 1 : 2;
            // 自动VIP项目  1自动vip 0不自动 如果选该项目自动成为VIP
            _Model.AutoVIP = ceAutoVIP.Checked ? 1 : 0;
            // 外送状态 1外送 2自己做
            _Model.OutgoingState = ceOutgoingState.Checked ? 1 : 2;
            // 是否询问自愿（如妇科，乙肝）  1是 0否
            _Model.VoluntaryState = ceVoluntaryState.Checked ? 1 : 0;
            //是否启用
            _Model.IsActive = ceActiveState.Checked ? true : false;
            //是否核酸检测 1是 2不是
            _Model.NucleicAcidState = checkEdit1.Checked ? 1 : 2;
            _Model.OrderNum = Convert.ToInt32(txtOrderNum.EditValue);
            _Model.HISID = txtHISBM.Text;
            _Model.HISName = txtHISName.Text;
            _Model.ItemGroupBM = txtHISName.Text;
            _Model.ItemGroupBM = txtItemGroupBM.Text;
            _Model.GWBM = textEditGWB.Text;
            bool res = false;
            AutoLoading(() =>
            {
                ItemGroupInput input = new ItemGroupInput()
                {
                    ItemGroup = ModelHelper.CustomMapTo2<FullItemGroupDto, CreateOrUpdateItemGroup>(_Model),
                    ItemInfoIds = _Model.ItemInfos.Select(m => m.Id).ToList()
                };
                if (gridprice.DataSource != null)
                {
                    input.PriceSyn = gridprice.GetDtoListDataSource<TbmGroupRePriceSynchronizesDto>().ToList();
                }
                FullItemGroupDto dto = null;
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                }
                else
                {
                    dto = service.Edit(input);
                }
                dto.Department = _Model.Department; // (DepartmentSimpleDto)lueDepartment.GetSelectedDataRow()
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
            if (!string.IsNullOrEmpty(_Model.NoticeCode))
            {
                lookUpEditNotice.EditValue = _Model.NoticeCode;
            }
            if (!string.IsNullOrEmpty(_Model.InspectionTypeCode))
            {
                lookUpEditInspectionType.EditValue = _Model.InspectionTypeCode;
            }
            if (!string.IsNullOrEmpty(_Model.CheckTypeCode))
            {
                lookUpEditType.EditValue = _Model.CheckTypeCode;
            }
            textEditGWB.Text = _Model.GWBM;
            txtOrderNum.EditValue = _Model.OrderNum;
            // 科室
            lueDepartment.EditValue = _Model.DepartmentId;
            // 项目组合名称
            teItemGroupName.Text = _Model.ItemGroupName;
            // 助记码
            teHelpChar.Text = _Model.HelpChar;
            // 最小年龄
            teMinAge.Text = _Model.MinAge?.ToString();
            // 最大年龄
            teMaxAge.Text = _Model.MaxAge?.ToString();
            // 单价 最小折扣核算后的价格
            tePrice.Text = _Model.Price?.ToString();
            // 成本价
            teCostPrice.Text = _Model.CostPrice?.ToString();
            // 最大折扣
            teMaxDiscount.Text = _Model.MaxDiscount?.ToString();
            // 注意事项
            teNotice.Text = _Model.Notice;
            // 组合介绍
            teItemGroupExplain.Text = _Model.ItemGroupExplain;
            // 备注说明
            teRemarks.Text = _Model.Remarks;
            // 禁忌备注
            teTaboo.Text = _Model.Taboo;
            // 最大体检人数
            teMaxCheckDay.Text = _Model.MaxCheckDay?.ToString();
            //// 项目明细数量
            //_ItemGroup.ItemCount = _ItemGroup.ItemInfos.Count;
            // 适用性别
            lueSex.EditValue = _Model.Sex;
            // 收费类型
            lueChart.EditValue = _Model.ChartCode;
            // 收费项目 是否属于收费项目 0收费1不收费 默认 收费
            ceISSFItemGroup.Checked = _Model.ISSFItemGroup != 1;
            // 标本类型?
            lueSpecimenType.EditValue = _Model.SpecimenType;
            // 是否用打条码  1是2不是
            ceBarState.Checked = _Model.BarState == 1;
            // 是否隐私项目 1是2不是
            cePrivacyState.Checked = _Model.PrivacyState == 1;
            // 是否抽血 1是2不是 
            ceDrawState.Checked = _Model.DrawState == 1;
            // 餐前餐后 1餐前2餐后  
            ceMealState.Checked = _Model.MealState == 1;
            // 是否妇检 1是2不是
            ceWomenState.Checked = _Model.WomenState == 1;
            // 试管样式?
            teTubeType.Text = _Model.TubeType;
            // 是否早餐 1不是2是
            ceBreakfast.Checked = _Model.Breakfast == 1;
            // 自动VIP项目  1自动vip 0不自动 如果选该项目自动成为VIP
            ceAutoVIP.Checked = _Model.AutoVIP == 1;
            // 外送状态 1外送 2自己做
            ceOutgoingState.Checked = _Model.OutgoingState == 1;
            // 是否询问自愿（如妇科，乙肝）  1是 0否
            ceVoluntaryState.Checked = _Model.VoluntaryState == 1;
            //是否启用
            ceActiveState.Checked = _Model.IsActive;
            //是否核酸
            checkEdit1.Checked = _Model.NucleicAcidState == 1;
            txtHISBM.Text = _Model.HISID;
            txtHISName.Text = _Model.HISName;
            txtItemGroupBM.Text = _Model.ItemGroupBM;

            // 已选列表
            gcSelected.DataSource = _Model.ItemInfos;
            //已选物价
            gridprice.DataSource = _Model.GroupRePriceSynchronizes;
        }
        #endregion

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //BindgrdOptionalItemGroup();
        }
        public void BindgrdOptionalItemGroup()
        {
            if (searchControl1.Text != "")
           {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<ItemInfoSimpleDto>();
                output = curItemls.Where(o => o.Name.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();

                gcWait.DataSource = output;
            }
            else
            {
                gcWait.DataSource = curItemls;
            }

        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void gcSelected_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }


        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            var HisPrice = new HisPriceDtos();

            var result = (List<HisPriceDtos>)gridAllPrice.DataSource;
            var result1 = gridAllPrice.DataSource as List<HisPriceDtos>;
            var results = _CustomerAppService.InsertYXHis(result);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                LoadWaitData(true);
            });
        }
        public void AddPrice()
        {            //List<priceSynDto>
            lueDepartment.Enabled = false;
            var dtos = gridAllPrice.GetSelectedRowDtos<priceSynDto>();
            if (dtos.Count == 0) return;
            TbmGroupRePriceSynchronizesDto tbmGroupRePriceSynchronizes = new TbmGroupRePriceSynchronizesDto();
            tbmGroupRePriceSynchronizes.PriceSynchronize = dtos[0];
            tbmGroupRePriceSynchronizes.PriceSynchronizeId = dtos[0].Id;
            tbmGroupRePriceSynchronizes.Count = decimal.Parse(spNum.EditValue?.ToString());
            tbmGroupRePriceSynchronizes.chkit_costn= dtos[0].chkit_costn * Convert.ToDecimal(spNum.EditValue);
           // gridprice.AddDtoListItem(dtos, true);

            bool IsHave = false;
            var dataresult = gridprice.DataSource as List<TbmGroupRePriceSynchronizesDto>;
            if (dataresult != null)
            {
                var list = dataresult.Where(o => o.PriceSynchronizeId == dtos[0].Id).ToList();
                if (list.Count > 0)
                { IsHave = true; }               
            }
            if (!IsHave)
            {
                if (dataresult == null)
                {
                    dataresult = new List<TbmGroupRePriceSynchronizesDto>();
                }

                dataresult.Add(tbmGroupRePriceSynchronizes);
                gridprice.DataSource = dataresult;
                gridprice.RefreshDataSource();
                gridprice.Refresh();
                gridAllPrice.RemoveDtoListItem(dtos);
                getAllPrice();
            }

        }
        public void DelPrice()
        {
            var selectIndexes = gridView2.GetSelectedRows();
            var dtos = gridprice.GetSelectedRowDtos<TbmGroupRePriceSynchronizesDto>();
            if (dtos.Count == 0) return;
            //  gridprice.RemoveDtoListItem(dtos);
            gridprice.GetDtoListDataSource<TbmGroupRePriceSynchronizesDto>()?.RemoveAll(m => dtos.Any(i => i.PriceSynchronizeId == m.PriceSynchronizeId));
            gridprice.RefreshDataSource();
            gridAllPrice.AddDtoListItem(dtos[0].PriceSynchronize, true);
            getAllPrice();

        }
        public void getAllPrice()
        {
            var pricels= gridprice.DataSource as List<TbmGroupRePriceSynchronizesDto>;
            if (pricels.Count > 0)
            {
                tePrice.Text = pricels.Sum(o => o.chkit_costn)?.ToString("0.00");
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DelPrice();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AddPrice();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                AddPrice();
            }
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                DelPrice();
            }
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane1.SelectedPageIndex==1 && gridAllPrice.DataSource==null)
            {

                priceSynDtos = service.GetPriceSyns();
                gridAllPrice.DataSource = priceSynDtos;
            }
        }

        private void searchControl2_TextChanged(object sender, EventArgs e)
        {

            if (searchControl2.Text != "")
            {
                var strup = searchControl2.Text.ToUpper();
                var output = new List<priceSynDto>();
                output = priceSynDtos.Where(o => o.chkit_name.Contains(searchControl2.Text) || o.chkit_id2.Contains(strup)).ToList();

                gridAllPrice.DataSource = output;
            }
            else
            {
                gridAllPrice.DataSource = priceSynDtos;
            }
        }
        private void SetSelectDataRow(decimal Num)
        {
            //获取所有选择列
            var iselectnum = gridView2.GetSelectedRows();
            for (var i = 0; i < iselectnum.Length; i++)
            {
                var dem =decimal.Parse( gridView2.GetRowCellValue(iselectnum[i], comdj).ToString());
                var dtos = gridAllPrice.GetSelectedRowDtos<priceSynDto>();

                gridView2.SetRowCellValue(iselectnum[i], comCount, Num);
                gridView2.SetRowCellValue(iselectnum[i], comchkit_costn, dem* Num);
            }
            getAllPrice();
        }

        private void spNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                SetSelectDataRow(Convert.ToDecimal(spNum.EditValue));
            }
        }
    }
}
