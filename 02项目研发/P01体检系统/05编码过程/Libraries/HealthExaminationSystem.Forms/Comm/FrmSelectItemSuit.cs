using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmSelectItemSuit : UserBaseForm
    {
        #region 系统变量
        private List<TbmDepartmentDto> _departments; //科室数据
        private IItemSuitAppService itemSuitAppSvr; //套餐
        /// <summary>
        /// 套餐字典数据 系统加载时缓存加载，后期改成从缓存获取
        /// </summary>
        private List<SimpleItemSuitDto> ItemSuits;
        private List<SimpleItemSuitDto> ItemSuitsSel;
        /// <summary>
        /// 套餐字典明细数据 系统加载时缓存加载，后期改成从缓存获取
        /// </summary>
       // private List<ItemSuitItemGroupContrastFullDto> SelectSuitItemGroup; //当前选择的套餐下组合编码数据

        public SearchItemSuitDto ItemSuitDto = new SearchItemSuitDto();//查询套餐所需Dto
        /// <summary>
        /// 当前体检人  用于根据性别、年龄等过虑可选组合 登记界面调用
        /// </summary>
        public TjlCustomerDto GRInputcustomerDto;
        /// <summary>
        /// 当前是否个人体检 调用该窗体必填 true为个人false为团体
        /// </summary>
        public bool InputisPersonal;
        /// <summary>
        /// 当前选择分组  用于根据性别、年龄等过虑可选组合  团体预约界面调用
        /// </summary>
        public CreateClientTeamInfoesDto TTInputClientTeamInfoesViewDto;

        public List<ClientTeamRegitemViewDto> LsisaddclientTeamRegitemViewDtos;
        /// <summary>
        /// 是否1+X加项包 true为1+X加项包 false 为选择套餐
        /// </summary>
        public bool InputisOnePlus;

        /// <summary>
        /// 选择套餐 用于返回数据
        /// </summary>
        public SimpleItemSuitDto OutputsimpleItemSuitDto;
        /// <summary>
        /// 当前选择组合 用于返回数据同时用于传入 个人登记调用
        /// </summary>
        public List<TjlCustomerItemGroupDto> GROutputcurSelectItemGroup;
        private List<TjlCustomerItemGroupDto> GRcurSelectItemGroup;

        /// <summary>
        /// 当前选择分组项目 用于返回数据同时用于传入  团体预约界面调用 
        /// </summary>
        public List<ClientTeamRegitemViewDto> TTOutputlstclientTeamRegitemViewDtos = new List<ClientTeamRegitemViewDto>();

        /// <summary>
        /// 用于存储套餐与1+X项目
        /// </summary>
        List<ItemSuitGroupSimpDto> suitInfo = new List<ItemSuitGroupSimpDto>();
        //已选
        List<ItemSuitGroupSimpDto> YiXuan = new List<ItemSuitGroupSimpDto>();

        #endregion 系统变量
        #region 组合系统变量
        private  IItemGroupAppService _ItemGroupAppService;
        private  IChargeAppService chargeAppService;
        private  IBarSettingAppService barSettingAppService;
        private  IClientRegAppService clientRegAppService;
        /// <summary>
        /// 记录 个人 在这个界面增加的组合，如果为新增加的，减项直接减去
        /// </summary>
        private List<TjlCustomerItemGroupDto> lstselectItemGroup;

        /// <summary>
        /// 记录 团体 在这个界面增加的组合，如果为新增加的，减项直接减去
        /// </summary>
        private List<ClientTeamRegitemViewDto> lstClientTeamRegitemViewDto;

        //所有组合
        private List<SimpleItemGroupDto> lstsimpleItemGroupDtos;
        //所有条码设置组合
        private List<BarItemDto> lstbarItemDtos;
    

        #region 窗体调用时传值
        /// <summary>
        /// 当前选择组合 登记界面调用 同时用于返回
        /// </summary>
       // public List<TjlCustomerItemGroupDto> curSelectItemGroup;
        /// <summary>
        /// 当前体检人  用于根据性别、年龄等过虑可选组合 登记界面调用
        /// </summary>
        public TjlCustomerDto customerDto;
        /// <summary>
        /// 当前体检预约人  用于根据性别、年龄等过虑可选组合 登记界面调用
        /// </summary>
        public QueryCustomerRegDto customerRegDto;
        /// <summary>
        /// 当前是否个人体检 调用该窗体必填 true为个人false为团体
        /// </summary>
        public bool isPersonal;
        /// <summary>
        /// 是否已登记 个人调用该窗体必填 true为已登记false为未登记
        /// </summary>
        public bool isCheckSate;
        /// <summary>
        /// 当前选择分组  用于根据性别、年龄等过虑可选组合  团体预约界面调用
        /// </summary>
        public CreateClientTeamInfoesDto ClientTeamInfoesViewDto;
        /// <summary>
        /// 当前选择分组项目    团体预约界面调用 同时用于返回
        /// </summary>
        public List<ClientTeamRegitemViewDto> lstclientTeamRegitemViewDtos;
        public List<ClientTeamRegitemViewDto> lstclientRegitemDtos;
        // 回传事件
        //个人
        // public event Action<List<TjlCustomerItemGroupDto>> SaveDataComplateForPersonal;
        //团体
        // public event Action<List<ClientTeamRegitemViewDto>> SaveDataComplateForGroup;

        public List<ClientTeamRegitemViewDto> lsisaddclientTeamRegitemViewDtos;

        /// <summary>
        /// 当前选择组合 登记界面调用 同时用于返回
        /// </summary>
        public List<TjlCustomerItemGroupDto> CurSelectItemGroup;
        #endregion

        #endregion 系统变量     

        #region 构造函数
        /// <summary>
        /// 选择1+X、套餐、模板、组单 共用一个界面
        /// 1.预约管理 新建团体预约，可以选择是否按组单模式创建预约；组单可以多选，点击确定后，根据组单创建分组
        /// 2.1+X 可以多选 要去重
        /// </summary>
        public FrmSelectItemSuit()
        {
            InitializeComponent();
            itemSuitAppSvr = new ItemSuitAppService();
            _ItemGroupAppService = new ItemGroupAppService();
            chargeAppService = new ChargeAppService();
            barSettingAppService = new BarSettingAppService();
            clientRegAppService = new ClientRegAppService();
           
            //curSelectItemGroup = _curSelectItemGroup;
            //customerDto = _customerDto;
            //isPersonal = _isPersonal;
            //isCheckSate = _isCheckSate;
            //customerRegDto = _customerRegDto;
            if (customerRegDto.ClientTeamInfo_Id == null)
            {
                btnTTPayment.Enabled = false;
                btnGRPayment.Enabled = false;
            }
            else
            {
                SearchClientTeamInfoDto searchClientTeamInfoDto = new SearchClientTeamInfoDto();
                searchClientTeamInfoDto.Id = (Guid)customerRegDto.ClientTeamInfo_Id;
                lsisaddclientTeamRegitemViewDtos = clientRegAppService.GetTeamRegItem(searchClientTeamInfoDto);
            }
        }

        /// <summary>
        /// 个人套餐
        /// </summary>
        /// <param name="_GRInputcustomerDto"></param>
        public FrmSelectItemSuit(TjlCustomerDto _GRInputcustomerDto)
        {
            InitializeComponent();
            itemSuitAppSvr = new ItemSuitAppService();
            GRInputcustomerDto = _GRInputcustomerDto;
            InputisPersonal = true;
            InputisOnePlus = false;
        }

        /// <summary>
        /// 团体套餐
        /// </summary>
        /// <param name="_TTInputClientTeamInfoesViewDto"></param>
        public FrmSelectItemSuit(CreateClientTeamInfoesDto _TTInputClientTeamInfoesViewDto)
        {
            InitializeComponent();
            itemSuitAppSvr = new ItemSuitAppService();
            TTInputClientTeamInfoesViewDto = _TTInputClientTeamInfoesViewDto;
            InputisPersonal = false;
            InputisOnePlus = false;
        }
        /// <summary>
        /// 个人1+X
        /// </summary>
        /// <param name="_GRInputcustomerDto"></param>
        /// <param name="_GROutputcurSelectItemGroup"></param>
        public FrmSelectItemSuit(TjlCustomerDto _GRInputcustomerDto , List<TjlCustomerItemGroupDto> _GROutputcurSelectItemGroup)
        {
            InitializeComponent();
            itemSuitAppSvr = new ItemSuitAppService();
            GRInputcustomerDto = _GRInputcustomerDto;
            GROutputcurSelectItemGroup = _GROutputcurSelectItemGroup;
            GRcurSelectItemGroup = _GROutputcurSelectItemGroup.ToList();
            InputisPersonal = true;
            InputisOnePlus = true;
            isPersonal = true;
            customerDto = _GRInputcustomerDto;
            curSelectItemGroup = _GROutputcurSelectItemGroup.ToList();
        }
        /// <summary>
        /// 团体1+X
        /// </summary>
        /// <param name="_TTInputClientTeamInfoesViewDto"></param>
        /// <param name="_TTOutputlstclientTeamRegitemViewDtos"></param>
        public FrmSelectItemSuit(CreateClientTeamInfoesDto _TTInputClientTeamInfoesViewDto, List<ClientTeamRegitemViewDto> _TTOutputlstclientTeamRegitemViewDtos)
        {
            InitializeComponent();
            itemSuitAppSvr = new ItemSuitAppService();
            TTInputClientTeamInfoesViewDto = _TTInputClientTeamInfoesViewDto;
            TTOutputlstclientTeamRegitemViewDtos = _TTOutputlstclientTeamRegitemViewDtos;
            InputisPersonal = false;
            InputisOnePlus = true;
        }
        public static T Clone<T>(object realObject)
        {
            using (System.IO.Stream stream = new MemoryStream()) // 初始化一个 流对象
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T)); //将要序列化的对象序列化到xml文档（Formatter）
                serializer.Serialize(stream, realObject); //将序列后的对象写入到流中
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);// 反序列化得到新的对象
            }

        }
        #endregion 构造函数

        #region 人员登记 
        public List<TjlCustomerItemGroupDto> curSelectItemGroup; //当前选择组合

        /// <summary>
        /// 是否登记选择套餐进入
        /// </summary>
        public bool IsSuit;
        public Guid? curSelectSuitId; //当前选择套餐Id 可能用不到

        public string curSelectSuitName; //当前选择套餐名称

        public ClientTeamInfoDto curTeamInfo; //团检当前选择的分组 

        #endregion 待删除

        #region 系统事件
        private void FrmSelectItemSuit_Load(object sender, EventArgs e)
        {
            // 两个查询耗时约3秒
            // 第二个查询耗时约2.3秒
            // 查询为什么不能放入后台筛选，而要放到前面
            // 选择套餐项目分组为什么不能点击时查询

            //获取所有套餐信息及套餐明细表 待完善
            _ItemGroupAppService = new ItemGroupAppService();
            chargeAppService = new ChargeAppService();
            barSettingAppService = new BarSettingAppService();
            clientRegAppService = new ClientRegAppService();          
            ItemSuits = DefinedCacheHelper.GetItemSuit().Where(o => o.ItemSuitType == (int)ItemSuitType.OnePlusX).ToList();
            if (InputisOnePlus)
            {
                layoutControlItem1.Text = "选择加项包";
                Text = "选择加项包";
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            //个人
            if (InputisPersonal == true)
            {
                if (GRInputcustomerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                   // ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) select c).ToList();

                    ItemSuitsSel = ItemSuits.ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
                else
                {
                    // ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) && c.Available == 1 && (c.Sex == GRInputcustomerDto.Sex || c.Sex == (int)Sex.GenderNotSpecified) select c).ToList();
                    ItemSuitsSel = ItemSuits.ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
            }
            else
            {
                if (TTInputClientTeamInfoesViewDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    //ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) select c).ToList();
                    ItemSuitsSel = ItemSuits.ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
                else
                {
                    //ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) && c.Available == 1 && (c.Sex == TTInputClientTeamInfoesViewDto.Sex || c.Sex == (int)Sex.GenderNotSpecified) select c).ToList();
                    ItemSuitsSel = ItemSuits.ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }

            }


            #region 加载组合
            //加载所有组合          
            lstsimpleItemGroupDtos = DefinedCacheHelper.GetItemGroups();
             //加载所有条码明细表
             lstbarItemDtos = barSettingAppService.GetBarItems();
            //加载可选组合
            BindgrdOptionalItemGroup();
            #endregion
            updateSuit();
            FrmSeleteItemGroupLoad();

        }
        private void showadd()
        {
            //如果是加项包，设置可以多选

            //个人
            if (InputisPersonal == true)
            {
                if (GRInputcustomerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) select c).ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
                else
                {
                    ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) && c.Available == 1 && (c.Sex == GRInputcustomerDto.Sex || c.Sex == (int)Sex.GenderNotSpecified) select c).ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
            }
            else
            {
                if (TTInputClientTeamInfoesViewDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) select c).ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }
                else
                {
                    ItemSuitsSel = (from c in ItemSuits where c.ItemSuitType == (InputisOnePlus ? 3 : 1) && c.Available == 1 && (c.Sex == TTInputClientTeamInfoesViewDto.Sex || c.Sex == (int)Sex.GenderNotSpecified) select c).ToList();
                    grdItemSuitList.DataSource = ItemSuitsSel;
                }

            }
        }

        //套餐名称单击事件右侧表格加载详细内容
        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            //主表格选中行数据
            var row = gridView1.GetFocusedRow() as SimpleItemSuitDto;
            //回传数据赋值
            OutputsimpleItemSuitDto = row;

            //去缓存里查数据，尽量避免每次查找 待完善
            try
            {
                //var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto { Id = row.Id });
                //if (ret != null)
                //    if (ret.Count > 0)
                //        suitInfo = ret.First();
                string rowId = row.Id.ToString();
                //var ret = from c in SelectSuitItemGroup where c.ItemSuitID == rowId select c;
                var input = new EntityDto<Guid>();
                input.Id =Guid.Parse(rowId);
                var ret = itemSuitAppSvr.GetAllSuitItemGroup(input);
                suitInfo = ret;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

            if (InputisOnePlus)
            {

            }
            else
            {
                YiXuan = suitInfo;
            }
            grdItemSuitDetailed.DataSource = suitInfo;
        }

        //行打钩事件
        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //所勾选行 行号数组
            var SelIndexs = gridView1.GetSelectedRows();
            if (e.Action == System.ComponentModel.CollectionChangeAction.Refresh)
            {
                YiXuan = new List<ItemSuitGroupSimpDto>();
                foreach (var SelItem in SelIndexs)
                {
                    var RowInfo = this.gridView1.GetRow(SelItem) as SimpleItemSuitDto;
                    string rowId = RowInfo.Id.ToString();
                    //var ret = (from c in SelectSuitItemGroup where c.ItemSuitID == rowId select c).ToList();
                    var input = new EntityDto<Guid>();
                    input.Id = Guid.Parse(rowId);
                    var ret = itemSuitAppSvr.GetAllSuitItemGroup(input);
                    foreach (var retItem in ret)
                    {
                        var info = YiXuan.FirstOrDefault(n => n.ItemGroupId == retItem.ItemGroupId);
                        if (info == null)
                        {
                            var ChongFuInfo = TTOutputlstclientTeamRegitemViewDtos.FirstOrDefault(n => n.TbmItemGroupid == retItem.ItemGroupId);
                            if (ChongFuInfo == null)
                            {
                                YiXuan.Add(retItem);
                            }
                        }
                    }
                }
            }
            else
            {
                if (e.Action == System.ComponentModel.CollectionChangeAction.Add)
                {
                    foreach (var retItem in suitInfo)
                    {
                        var info = YiXuan.FirstOrDefault(n => n.ItemGroupId == retItem.ItemGroupId);
                        if (info == null)
                        {
                            var ChongFuInfo = TTOutputlstclientTeamRegitemViewDtos.FirstOrDefault(n => n.TbmItemGroupid == retItem.ItemGroupId);
                            if (ChongFuInfo == null)
                            {
                                YiXuan.Add(retItem);
                            }
                        }
                    }
                }
                else
                {
                    //取消选中
                    foreach (var item in suitInfo)
                    {
                        //if (YiXuan.Contains(item))
                        //{
                        //    YiXuan.Remove(item);
                        //}
                        var removels = YiXuan.Where(o => o.Id == item.Id).ToList();
                        if (removels.Count > 0)
                        {
                            var yxls = removels.First();
                            YiXuan.Remove(yxls);
                        }
                        
                    }
                }
            }
            updateSuit();
            FrmSeleteItemGroupLoad();
        }

        //套餐名称搜索 回车重新绑定套餐名称表格
        private void searSelectItemSuit_KeyDown(object sender, KeyEventArgs e)
        {
            var Str = searSelectItemSuit.EditValue == null ? "" : searSelectItemSuit.EditValue.ToString();

            var vitem = from c in ItemSuitsSel where c.ItemSuitName.Contains(Str) || c.HelpChar.ToUpper().Contains(Str.ToUpper()) select c;
            grdItemSuitList.DataSource = vitem;
        }

        // 回传事件
        //个人
        public event Action<SimpleItemSuitDto, List<TjlCustomerItemGroupDto>> SaveDataComplateForPersonal;
        //团体
        public event Action<SimpleItemSuitDto, List<ClientTeamRegitemViewDto>> SaveDataComplateForGroup;

        // 回传事件
        //个人
        public event Action<List<TjlCustomerItemGroupDto>> SaveGroupForPersonal;

        //团体
        public event Action<List<ClientTeamRegitemViewDto>> SaveGroupForGroup;

        public event Action<List<ClientTeamRegitemViewDto>> SaveDataComplateForGroup1;
        public event Action<List<TjlCustomerItemGroupDto>> SaveDataComplateForPersonal1;


        //模型转换
        public List<TjlCustomerItemGroupDto> ZhuanHuan(List<ClientTeamRegitemViewDto> Data, SimpleItemSuitDto suit)
        {
            List<TjlCustomerItemGroupDto> lstgroupDto = new List<TjlCustomerItemGroupDto>();
            foreach (var seleteItemGroupDto in Data)
            {
                if (InputisOnePlus)
                {
                    if (GROutputcurSelectItemGroup.Any(o => o.ItemGroupBM_Id == seleteItemGroupDto.TbmItemGroupid))
                        continue;
                }
                TjlCustomerItemGroupDto groupDto = new TjlCustomerItemGroupDto();
                groupDto.DepartmentId = seleteItemGroupDto.TbmDepartmentid.Value;
                groupDto.DepartmentName = seleteItemGroupDto.DepartmentName;
                groupDto.DepartmentOrder = seleteItemGroupDto.DepartmentOrder;
                groupDto.ItemGroupBM_Id = seleteItemGroupDto.TbmItemGroupid.Value;//这个没有id不行
                groupDto.ItemGroupName = seleteItemGroupDto.ItemGroupName;
                groupDto.ItemGroupOrder = seleteItemGroupDto.ItemGroupOrder;//不太一致
                //groupDto.SFType = Convert.ToInt32(seleteItemGroupDto.ItemGroup.ChartCode);//??为啥注释这个必须有
                groupDto.CheckState = (int)ProjectIState.Not;
                groupDto.SummBackSate = (int)SummSate.Audited;
                groupDto.BillingEmployeeBMId = CurrentUser.Id;
                if (InputisOnePlus)
                    groupDto.IsAddMinus = (int)AddMinusType.Add;
                else
                    groupDto.IsAddMinus = (int)AddMinusType.Normal;
                groupDto.ItemPrice = seleteItemGroupDto.ItemGroupMoney;
                groupDto.DiscountRate = seleteItemGroupDto.Discount;
                groupDto.PriceAfterDis = seleteItemGroupDto.ItemGroupDiscountMoney;
                groupDto.GRmoney = seleteItemGroupDto.ItemGroupDiscountMoney;//需要赋值为啥注释
                groupDto.PayerCat = (int)PayerCatType.NoCharge;
                groupDto.TTmoney = 0.00M;
                groupDto.GuidanceSate = (int)PrintSate.NotToPrint;
                groupDto.BarState = (int)PrintSate.NotToPrint;
                groupDto.RequestState = (int)PrintSate.NotToPrint;
                groupDto.RefundState = (int)PrintSate.NotToPrint;
                if (!InputisOnePlus)
                {
                    groupDto.ItemSuitId = suit.Id;
                    groupDto.ItemSuitName = suit.ItemSuitName;
                }

                //var vitem = from c in lstbarItemDtos
                //            where c.ItemGroupId == seleteItemGroupDto.Id
                //            select c;
                //if (vitem.ToList().Count > 0)
                //{
                //    groupDto.DrawSate = (int)BloodState.NOT;
                //}
                //else
                //{
                //    groupDto.DrawSate = (int)BloodState.NOTNEED;
                //}
                lstgroupDto.Add(groupDto);
            }
            return lstgroupDto;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {           
            saveGroup();
            DialogResult = DialogResult.OK;
        }
        private void saveGroup()
        {
            DialogResult = DialogResult.OK;
            if (isPersonal)
            {
                CurSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
               // SaveGroupForPersonal?.Invoke(CurSelectItemGroup);

                SaveDataComplateForPersonal?.Invoke(OutputsimpleItemSuitDto, CurSelectItemGroup);
            }
            else
            {
                lstclientTeamRegitemViewDtos = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                var remove = lstclientTeamRegitemViewDtos.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).ToList();
                foreach (var item in remove)
                {
                    lstclientTeamRegitemViewDtos.Remove(item);
                }

                // SaveGroupForGroup?.Invoke(lstclientTeamRegitemViewDtos);
                SaveDataComplateForGroup?.Invoke(OutputsimpleItemSuitDto, lstclientTeamRegitemViewDtos);
            }
        }
      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
       
        #endregion 系统事件

        #region 公共方法

        #region 人员登记处理  待删除


        public List<ItemSuitItemGroupContrastFullDto> QuerySuitGroups(Guid id)
        {
            FullItemSuitDto suitInfo = null;
            try
            {
                var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto { Id = id });
                if (ret != null)
                    if (ret.Count > 0)
                        suitInfo = ret.First();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

            return suitInfo.ItemSuitItemGroups;
        }
        /// <summary>
        /// 加项包转为记录
        /// </summary>
        private void XItemGroupConvertJiLi()
        {
            var str = new StringBuilder();
            var rowNumbers = gridView1.GetSelectedRows();
            var nameStr = "";
            foreach (var num in rowNumbers)
                nameStr += "【" + gridView1.GetRowCellValue(num, colSuitName) + "】";
            str.Append("您选择了加项包");
            str.Append(nameStr);
            str.Append("是否继续？");
            var dr = XtraMessageBox.Show(str.ToString(), "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                //var groups = new List<ItemSuitItemGroupContrastFullDto>();
                if (_departments == null)
                    _departments = DefinedCacheHelper.GetDepartments();
                foreach (var num in rowNumbers)
                {
                    var row = gridView1.GetRow(num);
                    var data = row as SimpleItemSuitDto;

                    //判断已选择组合和当前选择组合中没有选择该组合（去重
                    var selectItems = QuerySuitGroups(data.Id);
                    selectItems.RemoveAll(m => GROutputcurSelectItemGroup.Any(s => s.ItemGroupBM_Id == m.ItemGroupId));
                    foreach (var g in selectItems)
                    {
                        var group = new TjlCustomerItemGroupDto();
                        group.ItemGroupBM_Id = g.ItemGroupId;
                        group.ItemPrice = g.ItemPrice == null ? 0 : g.ItemPrice.Value;
                        group.ItemGroupName = g.ItemGroup.ItemGroupName;
                        if (InputisOnePlus == false)
                        {
                            group.DiscountRate = curTeamInfo.Jxzk;
                            if (curTeamInfo.JxType == (int)PayerCatType.ClientCharge)
                            {
                                if (g.ItemPrice.HasValue)
                                    group.TTmoney = g.ItemPrice.Value * curTeamInfo.Jxzk;
                                else
                                    group.TTmoney = 0M;
                                group.PriceAfterDis = group.TTmoney;
                                group.PayerCat = (int)PayerCatType.ClientCharge;
                            }
                            else if (curTeamInfo.JxType == (int)PayerCatType.PersonalCharge)
                            {
                                if (g.ItemPrice.HasValue)
                                    group.GRmoney = g.ItemPrice.Value * curTeamInfo.Jxzk;
                                else
                                    group.GRmoney = 0M;
                                group.PriceAfterDis = group.GRmoney;
                                group.PayerCat = (int)PayerCatType.NoCharge;
                            }
                        }
                        else
                        {
                            group.DiscountRate = g.Suitgrouprate == null ? 0 : g.Suitgrouprate.Value;
                            group.GRmoney = g.PriceAfterDis == null ? 0 : g.PriceAfterDis.Value;
                            group.PriceAfterDis = g.PriceAfterDis == null ? 0 : g.PriceAfterDis.Value;
                            group.PayerCat = (int)PayerCatType.NoCharge;
                            group.TTmoney = 0M;
                        }

                        group.IsAddMinus = (int)AddMinusType.Add; //是否加减项 正常项目
                        group.ItemGroupOrder = g.ItemGroup.OrderNum;
                        group.SFType = Convert.ToInt32(g.ItemGroup.ChartCode);

                        var depart = _departments.FirstOrDefault(o => o.Id == g.ItemGroup.DepartmentId);
                        if (depart != null)
                        {
                            group.DepartmentId = depart.Id;
                            group.DepartmentName = depart.Name;
                            group.DepartmentOrder = depart.OrderNum;
                        }

                        group.CheckState = (int)ProjectIState.Not;
                        group.SFType = Convert.ToInt32(g.ItemGroup.ChartCode);
                        GROutputcurSelectItemGroup.Add(group);
                    }
                }

                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 选择套餐组合数据转为登记 组合记录 存至当前人信息
        /// </summary>
        private void ItemGroupCovertJiLu(List<ItemSuitItemGroupContrastFullDto> grops, Guid suitId, string suitName)
        {
            var str = new StringBuilder();
            if (curSelectSuitId.HasValue)
            {
                if (curSelectSuitId != suitId)
                {
                    str.Append("您已经选择了【");
                    str.Append(curSelectSuitName);
                    str.Append("】套餐，");
                }
                else
                {
                    ShowMessageBoxInformation("您已经使用了该套餐，无需重复添加。");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(str?.ToString()))
                str.Append("确定切换套餐【");
            else
                str.Append("确定使用套餐【");

            str.Append(suitName);
            str.Append("】吗？");
            var dr = XtraMessageBox.Show(str.ToString(), "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            if (InputisOnePlus == false)
            {
                if (_departments == null)
                    _departments = DefinedCacheHelper.GetDepartments();
                if (GROutputcurSelectItemGroup != null)
                    GROutputcurSelectItemGroup.RemoveAll(o => o.PayerCat == (int)PayerCatType.NoCharge);
                else
                    GROutputcurSelectItemGroup = new List<TjlCustomerItemGroupDto>();
                foreach (var item in grops)
                {
                    var group = new TjlCustomerItemGroupDto();
                    group.ItemGroupBM_Id = item.ItemGroupId;
                    group.ItemPrice = item.ItemPrice == null ? 0 : item.ItemPrice.Value;
                    group.PriceAfterDis = item.PriceAfterDis == null ? 0 : item.PriceAfterDis.Value;
                    group.ItemGroupName = item.ItemGroup.ItemGroupName;
                    group.DiscountRate = item.Suitgrouprate == null ? 0 : item.Suitgrouprate.Value;
                    group.GRmoney = item.PriceAfterDis == null ? 0 : item.PriceAfterDis.Value;
                    group.IsAddMinus = (int)AddMinusType.Normal; //是否加减项 正常项目
                    group.ItemGroupOrder = item.ItemGroup.OrderNum;
                    group.PayerCat = (int)PayerCatType.NoCharge;
                    group.TTmoney = 0;
                    group.ItemSuitId = suitId;
                    group.ItemSuitName = suitName;
                    var depart = _departments.FirstOrDefault(o => o.Id == item.ItemGroup.DepartmentId);
                    if (depart != null)
                    {
                        group.DepartmentId = depart.Id;
                        group.DepartmentName = depart.Name;
                        group.DepartmentOrder = depart.OrderNum;
                    }

                    group.CheckState = (int)ProjectIState.Not;
                    GROutputcurSelectItemGroup.Add(group);
                }
            }
            else
            {
               // SelectSuitItemGroup = grops;
            }

            curSelectSuitName = suitName;
            curSelectSuitId = suitId;
            DialogResult = DialogResult.OK;
        }

        #endregion

        #endregion 公共方法



        #region 选择组合



        #region 系统事件

        #region 系统加载
        private void FrmSeleteItemGroupLoad()
        {
            grdvSelectItemGroup.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            setAlter();

            //if (customerRegDto.ClientInfoId == null)
            //{
            //    btnTTPayment.Enabled = false;
            //    btnGRPayment.Enabled = false;
            //}

            //初始化已选组合，根据单位或者个人绑定不同的数据
            InitializationGridView();
           
            //加载已选组合
            LoadData();     

        }
        /// <summary>
        /// 个人登记界面调用
        /// </summary>
        /// <param name="curSelectItemGroup">当前选择组合</param>
        /// <param name="customerDto">当前体检人 用于根据性别、年龄等过虑可选组合</param>
        /// <param name="customerRegDto"></param>
        /// <param name="isPersonal"> 当前是否个人体检  true为个人false为团体</param>
        /// <param name="isCheckSate">是否已登记  true为已登记false为未登记</param>
        public void SeleteItemGroup(List<TjlCustomerItemGroupDto> CurSelectItemGroup,
            TjlCustomerDto CustomerDto, QueryCustomerRegDto CustomerRegDto, bool IsPersonal, bool IsCheckSate)
        {
            clientRegAppService = new ClientRegAppService();
            curSelectItemGroup = CurSelectItemGroup;
            customerDto = CustomerDto;
            isPersonal = IsPersonal;
            isCheckSate = IsCheckSate;
            customerRegDto = CustomerRegDto;
            if (CustomerRegDto.ClientTeamInfo_Id == null)
            {
                btnTTPayment.Enabled = false;
                btnGRPayment.Enabled = false;
            }
            else
            {
                var searchClientTeamInfoDto = new SearchClientTeamInfoDto();
                searchClientTeamInfoDto.Id = (Guid)CustomerRegDto.ClientTeamInfo_Id;
                LsisaddclientTeamRegitemViewDtos = clientRegAppService.GetTeamRegItem(searchClientTeamInfoDto);
            }
        }

        /// <summary>
        /// 团体界面调用
        /// </summary>
        /// <param name="_lstclientTeamRegitemViewDtos">当前选择分组项目</param>
        /// <param name="_ClientTeamInfoesViewDto">当前选择分组  用于根据性别、年龄等过虑可选组合</param>
        /// <param name="_isPersonal"> 当前是否个人体检  true为个人false为团体</param>
        /// <param name="_isCheckSate">是否已登记  true为已登记false为未登记</param>
        public void SeleteItemGroup(List<ClientTeamRegitemViewDto> _lstclientTeamRegitemViewDtos,
            CreateClientTeamInfoesDto _ClientTeamInfoesViewDto, bool _isPersonal, bool _isCheckSate)
        {           
           
            lstclientTeamRegitemViewDtos = _lstclientTeamRegitemViewDtos;
            ClientTeamInfoesViewDto = _ClientTeamInfoesViewDto;
            lstclientRegitemDtos= _lstclientTeamRegitemViewDtos;
            InputisPersonal = _isPersonal;
            isCheckSate = _isCheckSate;
            btnZJPyment.Enabled = false;
            btnTTPayment.Enabled = false;
            btnGRPayment.Enabled = false;
        }


        #endregion 系统加载

        #region 组合查询
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }
        private void schItemGroup_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }
        #endregion 组合查询

        #region 按钮添加
        private void btnadd_Click(object sender, EventArgs e)
        {
            Add();
        }
        #endregion 按钮添加

        #region 按钮减项
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
        #endregion 按钮减项

        #region 确定按钮
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (isPersonal == true)
            {
                curSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
                SaveDataComplateForPersonal1?.Invoke(curSelectItemGroup);
            }
            else
            {
                lstclientTeamRegitemViewDtos = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
                List<ClientTeamRegitemViewDto> remove = lstclientTeamRegitemViewDtos.Where(o => o.IsAddMinus == (int)AddMinusType.Minus).ToList();
                foreach (var item in remove)
                {
                    lstclientTeamRegitemViewDtos.Remove(item);
                }

                SaveDataComplateForGroup1?.Invoke(lstclientTeamRegitemViewDtos);
            }

        }
        #endregion 确定按钮

        #region 关闭
        //取消
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 折扣率回车事件
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
                //计算所有项目价格
                SeachChargrDto seachChargrDto = new SeachChargrDto();
                seachChargrDto.user = CurrentUser;
                if (!string.IsNullOrEmpty(txtDiscountPrice.Text))
                {
                    seachChargrDto.PayMoney = Convert.ToDecimal(txtDiscountPrice.Text);
                }
                else
                {
                    seachChargrDto.PayMoney = 0;
                }

                //获取所有选择行
                seachChargrDto = GetSelectDataRow(seachChargrDto);
                //获取选择项目最低折扣率，并计算价格
                List<GroupMoneyDto> lstgroupMoney = chargeAppService.CusGroupMoney(seachChargrDto);
                //根据返回值，设置gridview显示行
                SetSelectDataRow(lstgroupMoney);
            }

        }

        #endregion

        #region 折扣价格回车事件
        private void txtDiscountPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }
        #endregion

        #region 双击添加
        private void gridViewItemGround_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Add();
            }
        }

        #endregion 双击添加

        #region 双击减项
        private void grdvSelectItemGroup_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Del();
            }
        }
        #endregion 双击减项

        #region 自定义显示列
        private void grdvSelectItemGroup_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (isPersonal == true)
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
                        e.DisplayText = EnumHelper.GetEnumDesc((ProjectIState)lstSelectItemGroup[e.ListSourceRowIndex].CheckState);
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
                            e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
                        else
                        {
                            e.DisplayText = string.Empty;
                        }
                    }
                    if (e.Column.Name == "PayerCatType")
                    {
                        if (e.Value != null && Enum.IsDefined(typeof(PayerCatType), e.Value))
                            e.DisplayText = EnumHelper.GetEnumDesc((PayerCatType)e.Value);
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
        #endregion 自定义显示列

        #region 转团体支付
        private void btnTTPayment_Click(object sender, EventArgs e)
        {
            TTPayment();
        }
        #endregion 转团体支付

        #region 转个人支付
        private void btnGRPayment_Click(object sender, EventArgs e)
        {
            GRPayment();
        }
        #endregion 转个人支付

        #region 转赠检
        private void btnZJPyment_Click(object sender, EventArgs e)
        {
            ZJPyment();
        }
        #endregion 转赠检

        #region 退费
        private void btnRefund_Click(object sender, EventArgs e)
        {
            Refund();
        }
        #endregion 退费

        #region 右键项目详情
        private void 项目详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowItemInfo();
        }
        #endregion 右键项目详情

        #region 右键转团付
        private void 转团付ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TTPayment();
        }
        #endregion 右键转团付

        #region 右键转个付
        private void 转个付ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GRPayment();
        }
        #endregion 右键转个付

        #region 右键赠检
        private void 赠检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZJPyment();
        }
        #endregion 右键赠检

        #region 右键退费
        private void 退费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refund();
        }
        #endregion 右键退费

        #region 加减项列样式
        private void grdvSelectItemGroup_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            var isadd = grdvSelectItemGroup.GetRowCellValue(e.RowHandle, IsAddMinus);
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
        }
        #endregion 加减项列样式
        #endregion 系统事件

        #region 公共方法

        #region 项目详情
        private void ShowItemInfo()
        {
            alertInfo.Show(this, "提示!", "待开发");
        }
        #endregion 项目详情

        #region 退费
        private void Refund()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["RefundState"], PayerCatType.StayRefund);
            int[] ilst = grdvOptionalItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["RefundState"], PayerCatType.StayRefund);
            }
        }
        #endregion 退费

        #region 转赠检
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
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.GiveCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.GiveCharge);
            //修改价格
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], "0.00");

            int[] ilst = grdvSelectItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.GiveCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.GiveCharge);
                //修改价格
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], "0.00");
            }
        }
        #endregion 转赠检

        #region 转个人支付
        private void GRPayment()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.NoCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.NoCharge);
            //修改价格
            var PriceAfterDis = grdvSelectItemGroup.GetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PriceAfterDis"]);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], PriceAfterDis);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], "0.00");

            int[] ilst = grdvSelectItemGroup.GetSelectedRows();
            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.NoCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.NoCharge);
                //修改价格
                var PriceAfterDiss = grdvSelectItemGroup.GetRowCellValue(item, grdvSelectItemGroup.Columns["PriceAfterDis"]);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], PriceAfterDiss);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], "0.00");
            }
        }
        #endregion 转个人支付

        #region 转团体支付
        private void TTPayment()
        {
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.ClientCharge);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.ClientCharge);
            //修改价格
            var PriceAfterDis = grdvSelectItemGroup.GetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["PriceAfterDis"]);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["TTmoney"], PriceAfterDis);
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["GRmoney"], "0.00");

            int[] ilst = grdvSelectItemGroup.GetSelectedRows();

            foreach (var item in ilst)
            {
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCat"], PayerCatType.ClientCharge);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["PayerCatType"], PayerCatType.ClientCharge);
                //修改价格
                var PriceAfterDiss = grdvSelectItemGroup.GetRowCellValue(item, grdvSelectItemGroup.Columns["PriceAfterDis"]);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["TTmoney"], PriceAfterDiss);
                grdvSelectItemGroup.SetRowCellValue(item, grdvSelectItemGroup.Columns["GRmoney"], "0.00");
            }

        }
        #endregion

        #region 设置弹出框
        private void setAlter()
        {
            //以毫秒为单位
            this.alertInfo.AutoFormDelay = 4000;
            //出现的效果方式
            this.alertInfo.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.FadeIn;
            //弹出的速度
            this.alertInfo.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Fast;
        }
        public enum AlertFormLocation
        {
            // 摘要: 
            //     An alert window appears at the top left corner of the screen.
            TopLeft = 0,
            //
            // 摘要: 
            //     An alert window appears at the top right corner of the screen.
            TopRight = 1,
            //
            // 摘要: 
            //     An alert window appears at the bottom left corner of the screen.
            BottomLeft = 2,
            //
            // 摘要: 
            //     An alert window appears at the bottom right corner of the screen.
            BottomRight = 3,
        }
        #endregion

        #region 初始化已选组合，根据单位或者个人绑定不同的数据
        private void InitializationGridView()
        {
            //待完善
            if (isPersonal == false)
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
            if (isPersonal == false || (customerRegDto != null && customerRegDto.ClientInfoId == null))
            {
                btnTTPayment.Visible = false;
                btnGRPayment.Visible = false;
                转团付ToolStripMenuItem.Visible = false;
                转个付ToolStripMenuItem.Visible = false;
                txtDiscount.Visible = false;
                txtDiscountPrice.Visible = false;
            }

        }
        #endregion 初始化已选组合，根据单位或者个人绑定不同的数据

        #region 加载已选组合
        private void updateSuit()
        {

            TTOutputlstclientTeamRegitemViewDtos = new List<ClientTeamRegitemViewDto>();
            //if (lstclientTeamRegitemViewDtos != null)
            //{
            //    lstclientTeamRegitemViewDtos = lstclientRegitemDtos.ToList();
            //}
            //if (GROutputcurSelectItemGroup != null)
            //{
            //    GROutputcurSelectItemGroup = GRcurSelectItemGroup.ToList();
            //}

            foreach (var item in YiXuan)
            {
                ClientTeamRegitemViewDto TeamRegItem = new ClientTeamRegitemViewDto();
                TeamRegItem.ItemGroupName = item.ItemGroupName;
                TeamRegItem.DepartmentName = item.DeptmentName;
                //
                TeamRegItem.TbmDepartmentid = item.DeptmentId;
                TeamRegItem.DepartmentOrder = item.dtpOrder;
                TeamRegItem.TbmItemGroupid = item.ItemGroupId;

                TeamRegItem.Discount = item.Suitgrouprate == null ? 1 : item.Suitgrouprate.Value;
                //
                TeamRegItem.ItemGroupOrder = item.OrderNum;
                TeamRegItem.ItemGroupMoney = item.ItemPrice == null ? 0 : item.ItemPrice.Value;
                TeamRegItem.Discount = item.Suitgrouprate.Value;
                TeamRegItem.ItemGroupDiscountMoney = item.PriceAfterDis.Value;
                TeamRegItem.PayerCatType = InputisPersonal ? 2 : 3;

                if (!InputisOnePlus)
                {
                    TeamRegItem.ItemSuitId = OutputsimpleItemSuitDto.Id;
                    TeamRegItem.ItemSuitName = OutputsimpleItemSuitDto.ItemSuitName;
                }
                if (InputisPersonal)
                {

                }
                else
                {
                    TeamRegItem.ClientTeamInfoId = TTInputClientTeamInfoesViewDto.Id;
                }
                if (GROutputcurSelectItemGroup != null)
                {
                    var cusgroup = GROutputcurSelectItemGroup.Where(o => o.ItemGroupBM_Id == TeamRegItem.TbmItemGroupid).ToList();
                    if (cusgroup.Count == 0)
                    {
                        TTOutputlstclientTeamRegitemViewDtos.Add(TeamRegItem);
                    }
                }
                else if (lstclientTeamRegitemViewDtos != null)
                {
                    var cusgroup = lstclientTeamRegitemViewDtos.Where(o => o.TbmItemGroupid== TeamRegItem.TbmItemGroupid).ToList();
                    if (cusgroup.Count == 0)
                    {
                        TTOutputlstclientTeamRegitemViewDtos.Add(TeamRegItem);
                    }

                }
                else
                {
                    TTOutputlstclientTeamRegitemViewDtos.Add(TeamRegItem);
                }
            }

            //个人 
            if (InputisPersonal)
            {
                //返回 OutputsimpleItemSuitDto -- GROutputcurSelectItemGroup
                if (InputisOnePlus)
                {
                    
                    if (GROutputcurSelectItemGroup != null)
                    {
                        GROutputcurSelectItemGroup.AddRange(ZhuanHuan(TTOutputlstclientTeamRegitemViewDtos, OutputsimpleItemSuitDto));
                    }
                    else
                        GROutputcurSelectItemGroup = ZhuanHuan(TTOutputlstclientTeamRegitemViewDtos, OutputsimpleItemSuitDto);

                }
                else
                {
                    GROutputcurSelectItemGroup = ZhuanHuan(TTOutputlstclientTeamRegitemViewDtos, OutputsimpleItemSuitDto);
                }

                //SaveDataComplateForPersonal?.Invoke(OutputsimpleItemSuitDto, GROutputcurSelectItemGroup);
                curSelectItemGroup = GROutputcurSelectItemGroup;
            }
            else
            {
                //返回 OutputsimpleItemSuitDto -- TTOutputlstclientTeamRegitemViewDtos
                //SaveDataComplateForGroup?.Invoke(OutputsimpleItemSuitDto, TTOutputlstclientTeamRegitemViewDtos);
                if (InputisOnePlus)
                {
                    if (lstclientTeamRegitemViewDtos != null)
                    {
                        lstclientTeamRegitemViewDtos.AddRange(TTOutputlstclientTeamRegitemViewDtos);
                    }
                    else
                        lstclientTeamRegitemViewDtos = TTOutputlstclientTeamRegitemViewDtos;

                }
                else
                {
                    lstclientTeamRegitemViewDtos = TTOutputlstclientTeamRegitemViewDtos;
                }

            }
        }
        private void LoadData()
        {
            if (isPersonal == true)
            {
                grdSelectItemGroup.DataSource = curSelectItemGroup;
            }
            else
            {
                grdSelectItemGroup.DataSource = lstclientTeamRegitemViewDtos;
            }
            grdSelectItemGroup.RefreshDataSource();
        }

        #endregion 加载已选组合

        #region 加载可选组合 
        public void BindgrdOptionalItemGroup()
        {
            List<SimpleItemGroupDto> output = new List<SimpleItemGroupDto>();
            if (isPersonal == true)
            {
                if (customerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    output = lstsimpleItemGroupDtos.ToList();
                }
                else
                {
                    // 待完善 性别、年龄 等条件过虑
                    var lstvar = from c in lstsimpleItemGroupDtos
                                 where (c.Sex == customerDto.Sex || c.Sex == (int)Sex.GenderNotSpecified)
                                 select c;
                    output = lstvar.ToList();
                }
            }
            else
            {
                if (customerDto == null || customerDto.Sex == (int)Sex.GenderNotSpecified)
                {
                    output = lstsimpleItemGroupDtos.ToList();
                }
                else
                {
                    // 待完善 性别、年龄 等条件过虑
                    var lstvar = from c in lstsimpleItemGroupDtos
                                 where (c.Sex == ClientTeamInfoesViewDto.Sex || c.Sex == (int)Sex.GenderNotSpecified)
                                 select c;
                    output = lstvar.ToList();
                }
            }
            //去掉重复数据
            if (isPersonal == true)//如果是个人
            {
                foreach (var item in curSelectItemGroup)
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
                string strup = schItemGroup.Text.ToUpper();
                var lstvar = from c in output
                             where
            c.HelpChar.ToUpper().Contains(strup) || c.ItemGroupName.Contains(strup) || c.Department.Name.Contains(strup)
                             select c;
                output = lstvar.ToList();
            }
            grdOptionalItemGroup.DataSource = output.OrderBy(n => n.Department.OrderNum).ThenBy(n => n.OrderNum)?.ToList();
            grdOptionalItemGroup.RefreshDataSource();
        }
        #endregion

        #region 添加组合
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
            if (InputisPersonal) //添加是转换成个人选择组合
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
                var ts = _ItemGroupAppService.getItemConf(confiITem);
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
            //获取耗材 关联项目
            var lstAddCustomerItemGroupDto = new List<TjlCustomerItemGroupDto>();
            var lstclientTeamRegitemView = new List<ClientTeamRegitemViewDto>();
            if (InputisPersonal) //添加是转换成个人选择组合
            {
                //获取选择组合
                var lstSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();

                //判断是否重复
                lstOptionalItemGroup = DuplicateRemoval(lstOptionalItemGroup.ToList(), lstSelectItemGroup);
                if (lstOptionalItemGroup.Count > 0)
                {
                    //添加是转换成个人选择组合
                    lstAddCustomerItemGroupDto = GetAddData(lstOptionalItemGroup.ToList(), InputisPersonal);

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
                    lstclientTeamRegitemView = GetAddTTData(lstOptionalItemGroup.ToList(), InputisPersonal);

                    //移除可选项目
                    RemoveGroup(lstOptionalItemGroup.ToList());
                    var lstgroup = AddTTSelectItemGroup(lstclientTeamRegitemView, lstSelectItemGroup);
                    grdSelectItemGroup.DataSource = lstgroup;
                    grdSelectItemGroup.RefreshDataSource();

                    //grdvSelectItemGroup.SelectRow(0);
                }
            }
        }
        #endregion 添加组合

        #region 添加组合到选择列
        private List<TjlCustomerItemGroupDto> AddSelectItemGroup(List<TjlCustomerItemGroupDto> customerItemGroupDto, List<TjlCustomerItemGroupDto> list)
        {
            if (lstselectItemGroup == null)
            {
                lstselectItemGroup = new List<TjlCustomerItemGroupDto>();
            }
            foreach (TjlCustomerItemGroupDto item in customerItemGroupDto)
            {
                list.Insert(0, item);
                lstselectItemGroup.Add(item);
            }
            return list;
        }
        #endregion

        #region 添加组合到选择列 团体
        private List<ClientTeamRegitemViewDto> AddTTSelectItemGroup(List<ClientTeamRegitemViewDto> customerItemGroupDto, List<ClientTeamRegitemViewDto> list)
        {
            if (lstClientTeamRegitemViewDto == null)
            {
                lstClientTeamRegitemViewDto = new List<ClientTeamRegitemViewDto>();
            }
            foreach (ClientTeamRegitemViewDto item in customerItemGroupDto)
            {
                list.Insert(0, item);
                lstClientTeamRegitemViewDto.Add(item);
            }
            return list;
        }
        #endregion

        #region 去掉重复
        /// <summary>
        /// 去掉选择组合重复
        /// </summary>
        /// <param name="customerItemGroupDto">从可选组合选择的组合</param>
        /// <param name="list">已经选择的组合</param>
        /// <returns>List<SimpleItemGroupDto></returns>
        private List<SimpleItemGroupDto> DuplicateRemoval(List<SimpleItemGroupDto> lstSimpleItemGroupDto, List<TjlCustomerItemGroupDto> lstCustomerItemGroupDto)
        {
            List<SimpleItemGroupDto> lstSimpleItemGroupDtonew = new List<SimpleItemGroupDto>(lstSimpleItemGroupDto.ToArray());
            StringBuilder strbname = new StringBuilder();
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
                this.alertInfo.Show(this, "提示!", strbname.ToString().Trim('、') + " 组合已选择!");
            }
            return lstSimpleItemGroupDtonew;
        }
        #endregion 去掉重复

        #region 去掉重复 团体
        /// <summary>
        /// 去掉选择组合重复
        /// </summary>
        /// <param name="customerItemGroupDto">从可选组合选择的组合</param>
        /// <param name="list">已经选择的组合</param>
        /// <returns>List<SimpleItemGroupDto></returns>
        private List<SimpleItemGroupDto> DuplicateRemoval(List<SimpleItemGroupDto> lstSimpleItemGroupDto, List<ClientTeamRegitemViewDto> lstCustomerItemGroupDto)
        {
            List<SimpleItemGroupDto> lstSimpleItemGroupDtonew = new List<SimpleItemGroupDto>(lstSimpleItemGroupDto.ToArray());
            StringBuilder strbname = new StringBuilder();
            foreach (var item in lstSimpleItemGroupDto)
            {
                var vitem = from c in lstCustomerItemGroupDto where c.TbmItemGroupid == item.Id select c;
                if (vitem.ToList().Count > 0)
                {
                    lstSimpleItemGroupDtonew.Remove(item);
                    strbname.Append(item.ItemGroupName + "、");
                }
            }
            if (!string.IsNullOrEmpty(strbname.ToString()))
            {
                this.alertInfo.Show(this, "提示!", strbname.ToString().Trim('、') + " 组合已选择!");
            }
            return lstSimpleItemGroupDtonew;
        }
        #endregion 去掉重复

        #region 添加是转换成团体选择组合
        private List<ClientTeamRegitemViewDto> GetAddTTData(List<SimpleItemGroupDto> lstSimpleItemGroupDto, bool isPersonal)
        {
            List<ClientTeamRegitemViewDto> lstgroupDto = new List<ClientTeamRegitemViewDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                ClientTeamRegitemViewDto groupDto = new ClientTeamRegitemViewDto();
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
                    List<ClientTeamRegitemViewDto> lstregitem = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
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
                groupDto.IsAddMinus = (int)AddMinusType.Add;
                lstgroupDto.Add(groupDto);
            }
            return lstgroupDto;
        }
        #endregion 添加是转换成个人选择组合
        #region 添加是转换成个人选择组合
        private List<TjlCustomerItemGroupDto> GetAddData(List<SimpleItemGroupDto> lstSimpleItemGroupDto, bool isPersonal)
        {
            List<TjlCustomerItemGroupDto> lstgroupDto = new List<TjlCustomerItemGroupDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                TjlCustomerItemGroupDto groupDto = new TjlCustomerItemGroupDto();
                groupDto.CustomerRegBMId = customerRegDto.Id;
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
                if (customerRegDto.ClientTeamInfo_Id == null)
                {
                    var visitemsfState = from c in curSelectItemGroup
                                         where c.PayerCat == (int)PayerCatType.ClientCharge
|| c.PayerCat == (int)PayerCatType.PersonalCharge
                                         select c;
                    if (customerRegDto.CostState != null && visitemsfState.Count() > 0)
                    {
                        groupDto.IsAddMinus = (int)AddMinusType.Add;
                    }
                    else
                    {
                        groupDto.IsAddMinus = (int)AddMinusType.Normal;
                    }
                }
                else
                {if (lsisaddclientTeamRegitemViewDtos != null)
                    {
                        var vishave = from c in lsisaddclientTeamRegitemViewDtos where c.ItemGroup.Id == SimpleItemGroupDto.Id select c;
                        if (vishave.Count() > 0)
                        {
                            groupDto.IsAddMinus = (int)AddMinusType.Normal;
                        }
                        else
                        {
                            if (customerRegDto.CostState != (int)PayerCatType.NoCharge)
                            {
                                groupDto.IsAddMinus = (int)AddMinusType.Add;
                            }
                            else
                            {
                                groupDto.IsAddMinus = (int)AddMinusType.Normal;
                            }
                        }
                    }
                }


                groupDto.ItemPrice = SimpleItemGroupDto.Price.Value;
                groupDto.DiscountRate = new decimal(1.00);
                groupDto.PriceAfterDis = SimpleItemGroupDto.Price.Value;
                groupDto.PayerCat = (int)PayerCatType.NoCharge;
                groupDto.TTmoney = decimal.Parse("0.00");
                groupDto.GRmoney = SimpleItemGroupDto.Price.Value;
                groupDto.GuidanceSate = (int)PrintSate.NotToPrint;
                groupDto.BarState = (int)PrintSate.NotToPrint;
                groupDto.RequestState = (int)PrintSate.NotToPrint;
                groupDto.RefundState = (int)PayerCatType.NotRefund;
                var vitem = from c in lstbarItemDtos where c.ItemGroupId == SimpleItemGroupDto.Id select c;
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
        #endregion 添加是转换成个人选择组合

        #region 添加时转换成团体选择组合
        private List<ClientTeamRegitemViewDto> GetAddData(List<SimpleItemGroupDto> lstSimpleItemGroupDto)
        {
            List<ClientTeamRegitemViewDto> lstgroupDto = new List<ClientTeamRegitemViewDto>();
            foreach (var SimpleItemGroupDto in lstSimpleItemGroupDto)
            {
                ClientTeamRegitemViewDto groupDto = new ClientTeamRegitemViewDto();
                lstgroupDto.Add(groupDto);
            }

            return lstgroupDto;
        }
        #endregion 添加时转换成团体选择组合

        #region 删除组合
        public bool Del()
        {
            var bres = false;
            if (InputisPersonal)
            {
                var item = grdSelectItemGroup.GetFocusedRowDto<TjlCustomerItemGroupDto>();
                if (item == null)
                {
                    return bres;
                }
                var hcdepr = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == item.DepartmentId);
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

                if (customerRegDto.ClientTeamInfo == null)
                {
                    if ((customerRegDto.CostState == null || item.PayerCat == (int)PayerCatType.NoCharge) && !item.ItemSuitId.HasValue)
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
                        var visitemsfState = from c in CurSelectItemGroup
                                             where c.PayerCat == (int)PayerCatType.ClientCharge
                                                   || c.PayerCat == (int)PayerCatType.PersonalCharge
                                             select c;
                        if (visitemsfState.Count() > 0)
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
                if (item.ItemSuitId != null || isCheckSate)
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
        #endregion

        #region 设置移除
        private void setRemove(TjlCustomerItemGroupDto item)
        {
            grdSelectItemGroup.RemoveDtoListItem(item);
            curSelectItemGroup = grdSelectItemGroup.GetDtoListDataSource<TjlCustomerItemGroupDto>();
            if (lstselectItemGroup != null)
            {
                lstselectItemGroup.Remove(item);
            }
        }
        #endregion 设置移除

        #region 设置移除
        private void setTTRemove(ClientTeamRegitemViewDto item)
        {
            grdSelectItemGroup.RemoveDtoListItem(item);
            lstclientTeamRegitemViewDtos = grdSelectItemGroup.GetDtoListDataSource<ClientTeamRegitemViewDto>();
            if (lstClientTeamRegitemViewDto != null)
            {
                lstClientTeamRegitemViewDto.Remove(item);
            }
        }
        #endregion 设置移除

        #region 设置减项状态
        private void setIsAddMinus()
        {

            //更改为减项
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["IsAddMinus"], AddMinusType.Minus);
            grdvSelectItemGroup.RefreshData();
        }
        #endregion 设置减项状态

        #region 设置带退费状态
        private void setPayerCatType()
        {

            //更改为减项
            var columnView = (ColumnView)grdSelectItemGroup.FocusedView;
            //得到选中的行索引
            int focusedhandle = columnView.FocusedRowHandle;
            grdvSelectItemGroup.SetRowCellValue(focusedhandle, grdvSelectItemGroup.Columns["RefundState"], PayerCatType.StayRefund);
            grdvSelectItemGroup.RefreshData();
        }
        #endregion 设置减项状态

        #region 生成选择组合集合
        private SeachChargrDto GetSelectDataRow(SeachChargrDto seachChargrDto)
        {
            seachChargrDto.ItemGroups = new List<GroupMoneyDto>();
            //获取所有选择列
            int[] iselectnum = grdvSelectItemGroup.GetSelectedRows();
            if (iselectnum.Length <= 0)
            {
                alertInfo.Show(this, "提示!", "请选择待打折组合");
            }
            foreach (var item in iselectnum)
            {
                GroupMoneyDto groupMoney = new GroupMoneyDto();


                if (isPersonal == true)//个人
                {
                    TjlCustomerItemGroupDto dataRow = (TjlCustomerItemGroupDto)grdvSelectItemGroup.GetRow(item);
                    string strItemGroupName = dataRow.ItemGroupName;
                    var vitemGroup = from c in lstsimpleItemGroupDtos where c.ItemGroupName == strItemGroupName select c;
                    if (vitemGroup.ToList().Count > 0)
                    {
                        groupMoney.MaxDiscount = vitemGroup.ToList()[0].MaxDiscount;
                    }
                    else
                    {
                        groupMoney.MaxDiscount = 1;
                    }

                    groupMoney.IsAddMinus = dataRow.IsAddMinus;
                    groupMoney.ItemPrice = dataRow.ItemPrice;
                    groupMoney.PriceAfterDis = dataRow.PriceAfterDis;
                    groupMoney.DiscountRate = Convert.ToDecimal(txtDiscount.Text) / 100;
                    groupMoney.PayerCat = dataRow.PayerCat.Value;
                    groupMoney.RefundState = dataRow.RefundState;
                    groupMoney.GRmoney = dataRow.GRmoney;
                    groupMoney.TTmoney = dataRow.TTmoney;
                }
                else
                {
                    ClientTeamRegitemViewDto dataRow = (ClientTeamRegitemViewDto)grdvSelectItemGroup.GetRow(item);
                    string strItemGroupName = dataRow.ItemGroupName;
                    var vitemGroup = from c in lstsimpleItemGroupDtos where c.ItemGroupName == strItemGroupName select c;
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
                    groupMoney.DiscountRate = Convert.ToDecimal(txtDiscount.Text) / 100;
                    groupMoney.PayerCat = dataRow.IsAddMinus.Value;
                    groupMoney.RefundState = 1;
                    groupMoney.GRmoney = dataRow.ItemGroupMoney;
                    groupMoney.TTmoney = dataRow.ItemGroupMoney;
                }


                seachChargrDto.ItemGroups.Add(groupMoney);

            }
            return seachChargrDto;
        }
        #endregion 生成选择组合集合

        #region 设置选择行数据
        private void SetSelectDataRow(List<GroupMoneyDto> lstgroupMoney)
        {
            //获取所有选择列
            int[] iselectnum = grdvSelectItemGroup.GetSelectedRows();
            for (int i = 0; i < iselectnum.Length; i++)
            {
                #region 个人绑定列
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
                #endregion
                #region 团体绑定列
                //Department DepartmentName  科室
                //ItemGroupNames ItemGroupName 组合
                //Prices ItemGroupMoney 价格
                //DiscountRate Discount 折扣率
                //PriceAfterDis ItemGroupDiscountMoney 折扣价格
                //PayerCat PayerCatType 支付状态
                #endregion
                if (isPersonal == false)
                {
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["ItemGroupMoney"], lstgroupMoney[i].ItemPrice);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["Discount"], lstgroupMoney[i].DiscountRate);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["ItemGroupDiscountMoney"], lstgroupMoney[i].PriceAfterDis);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["GRmoney"], lstgroupMoney[i].ItemPrice);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["TTmoney"], lstgroupMoney[i].ItemPrice);
                }
                else
                {
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["ItemPrice"], lstgroupMoney[i].ItemPrice);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["DiscountRate"], lstgroupMoney[i].DiscountRate);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["PriceAfterDis"], lstgroupMoney[i].PriceAfterDis);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["GRmoney"], lstgroupMoney[i].PriceAfterDis);
                    grdvSelectItemGroup.SetRowCellValue(iselectnum[i], grdvSelectItemGroup.Columns["TTmoney"], 0);

                }


            }
        }
        #endregion 设置选择行数据

        #region 移除当前行
        private void RemoveGroup(List<SimpleItemGroupDto> lstSimpleItemGroupDto)
        {
            //获取当前选择行
            int iindex = grdvOptionalItemGroup.GetFocusedDataSourceRowIndex();
            var lstOptionalItemGroup = grdOptionalItemGroup.GetDtoListDataSource<SimpleItemGroupDto>();
            foreach (SimpleItemGroupDto item in lstSimpleItemGroupDto)
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





        #endregion

        #endregion 公共方法

        //右键组合详情
        private void 组合详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Details();
        }
        //转组合详情
        private void Details()
        {
            var dto = grdOptionalItemGroup.GetSelectedRowDtos<SimpleItemGroupDto>().FirstOrDefault();
            if (dto == null) return;
            using (var frm = new FrmIntroduce(dto))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    return;
            }

        }

        private void grdvSelectItemGroup_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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
                        var list1 = view.DataSource as List<ClientTeamRegitemViewDto>;
                        if (fieldName == ItemGroupName.FieldName)
                        {

                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Count();
                            else if (list1 != null)
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Count();
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == Prices.SummaryItem.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice);
                            else if (list1 != null)
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemGroupMoney);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == PriceAfterDis.SummaryItem.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.PriceAfterDis);
                            else if (list1 != null)
                                e.TotalValue = list1.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemGroupDiscountMoney);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == GRmoney.SummaryItem.FieldName)
                        {
                            if (list != null)
                                e.TotalValue = list.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.GRmoney);
                            else
                                e.TotalValue = 0;
                        }
                        if (fieldName == TTmoney.SummaryItem.FieldName)
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

        private void grdvSelectItemGroup_CustomSummaryExists(object sender, DevExpress.Data.CustomSummaryExistEventArgs e)
        {

        }

        #endregion

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex==1)
            {
                //updateSuit();
                //FrmSeleteItemGroupLoad();

            }
        }
    }
}