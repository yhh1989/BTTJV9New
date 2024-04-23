using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using BTExpressions;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout.Utils;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using natrotech;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Crisis;
using Sw.Hospital.HealthExaminationSystem.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Dictionary;
using WindowsFormsApp1;
using static Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic.OccDayStaticList;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class DoctorDesk : UserBaseForm
    {
        private IPersonnelCategoryAppService _personnelCategoryAppService;
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        /// <summary>
        /// 获取图片
        /// </summary>
        private readonly PictureController _pictureController;
        /// <summary>
        /// 组合项目状态/项目状态
        /// </summary>
        private readonly List<ProjectIStateModel> _projectIStateModel;

        /// <summary>
        /// 当前登录人拥有科室id
        /// </summary>
        private readonly List<Guid> _departmentGuidSys;

        /// <summary>
        /// 获取身高等项目信息和生成小结格式
        /// </summary>
        private List<BasicDictionaryDto> _basicDictionarySys;

        /// <summary>
        /// 当前体检人体检项目
        /// </summary>
        private List<ATjlCustomerItemGroupDto> _currentCustomerItemGroupSys;

        /// <summary>
        /// 当前体检人
        /// </summary>
        private ATjlCustomerRegDto _currentInputSys;

        /// <summary>
        /// 所有科室用户
        /// </summary>
        private List<UserForComboDto> _currentUserdtoSys;

        /// <summary>
        /// 所有科室的图片
        /// </summary>
        private List<CustomerItemPicDto> _customerItemPicAllSys;

        /// <summary>
        /// 当前科室的图片
        /// </summary>
        private List<CustomerItemPicDto> _customerItemPicSys;
        private List<PersonnelCategoryViewDto> personnelCategoryViewDtos;
        public readonly IHistoryComparisonAppService _HistoryComparisonAppService;
        /// <summary>
        /// 当前图片是第几张
        /// </summary>
        private int _customerPicSys;

        /// <summary>
        /// 判断是否总检
        /// </summary>
        private bool _isShiFouZongJian = true;

        /// <summary>
        /// 多行文本中按键则不进入grid事件中
        /// </summary>
        private bool _istabDown = true;

        /// <summary>
        /// 项目字典（当前有的科室）
        /// </summary>
        private List<BTbmItemDictionaryDto> _itemDictionarySys;

        /// <summary>
        /// 项目参考值
        /// </summary>
        private List<SearchItemStandardDto> _itemStandardSys;

        /// <summary>
        /// 正在计算体脂的时候，修改后再进去事件则返回
        /// </summary>
        private bool _isTiZhiJiSuan;

        ///// <summary>
        ///// 健康建议
        ///// </summary>
        //private List<SearchTbmSummarizeAdviceDto> SummarizeAdviceSys = new List<SearchTbmSummarizeAdviceDto>();
        /// <summary>
        /// 计算型公式
        /// </summary>
        private List<ItemProcExpressDto> _itemProcExpressDtos;
        private List<string> connamels = new List<string>();
        public System.Drawing.Point mouseDownPoint;//存储鼠标焦点的全局变量
        public bool isSelected = false;
        private List<SexModel> sexList;//性别字典
        /// <summary>
        /// 数据是否修改
        /// </summary>
        private bool _valueUpdate = false;
        private readonly ICommonAppService _commonAppService;
        private List<DeptNameDto> deptNameDtos;
        private bool hsjzlr = false;
        #region 历史对比
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        private List<GridColumn> OldColumns = new List<GridColumn>();
        #endregion
        public DoctorDesk()
        {
            InitializeComponent();
            CustomColumns = new Dictionary<string, CustomColumnValue>();
            _HistoryComparisonAppService = new HistoryComparisonAppService();
            _personnelCategoryAppService = new PersonnelCategoryAppService();
            layDeptName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _commonAppService = new CommonAppService();
            _doctorStation = new DoctorStationAppService();
            _projectIStateModel = ProjectIStateHelper.GetProjectIStateModels();
            _pictureController = new PictureController();
            _calendarYearComparison = new HistoryComparisonAppService();
            customerSvr = new CustomerAppService();
            _departmentGuidSys = CurrentUser.TbmDepartments.Select(r => r.Id).ToList();
            _itemProcExpressDtos = _doctorStation.getItemProcExpress();
            var action = new Action(YiBuJiaZai);//定义委托
            action.BeginInvoke(asyncResult =>//第一个参数必须定义
            {
                action.EndInvoke(asyncResult);//执行操作
                asyncResult.AsyncWaitHandle.Close();//关闭

            }, null//第二个参数可以为null
            );
            #region 鼠标注册
            //this.pictureEditTuPianZhanShi.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);

            #endregion
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorDesk_Load(object sender, EventArgs e)
        {
            try
            {
                //人员列表显示
                var YbYS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusList, 1)?.Remarks;
                if (!string.IsNullOrEmpty(YbYS) && YbYS == "Y")
                {

                    dockCuslist.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                }
                else
                { dockCuslist.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide; }

                personnelCategoryViewDtos = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
                dockCuslist.Options.ShowCloseButton = false;

                _currentUserdtoSys = new List<UserForComboDto>();
                _currentUserdtoSys = DefinedCacheHelper.GetComboUsers();
                //绑定检查医生
                cglueJianChaYiSheng.Properties.DataSource = _currentUserdtoSys;
                //绑定审核医生
                cglueShenHeYiSheng.Properties.DataSource = _currentUserdtoSys;

                sexList = SexHelper.GetSexForPerson();// SexHelper.GetSexModelsForItemInfo();
                gridLookUpSex.Properties.DataSource = sexList;//性别
                #region 绑定历史数据检索条件
                searchLookUpDepartMent.Properties.DataSource = DefinedCacheHelper.GetDepartments();
                searchLookUpGroup.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
                searchLookUpItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();


                #endregion

                //加载默认查询条件
                string fpd = System.Windows.Forms.Application.StartupPath + "\\DoctorDesk.json";
                if (File.Exists(fpd))  // 判断是否已有相同文件 
                {
                    var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fpd));
                    foreach (var tj in Search)
                    {
                        if (tj.Name == "WeiJian")
                        {
                            if (tj.Text == "true")
                            {
                                checkEditRyWeiJianRenShu.Checked = true;
                            }
                            else
                            {
                                checkEditRyWeiJianRenShu.Checked = false;
                            }


                        }
                        else if (tj.Name == "checkHas")
                        {
                            if (tj.Text == "true")
                            {
                                checkHas.Checked = true;
                            }
                            else
                            {
                                checkHas.Checked = false;
                            }
                        }
                        else if (tj.Name == "checkState")
                        {
                            if (tj.Text == "true")
                            {
                                checkState.Checked = true;
                            }
                            else
                            {
                                checkState.Checked = false;
                            }
                        }
                        else if (tj.Name == "Day")
                        {
                            txtRYTianShu.Text = tj.Text;
                        }

                    }
                }
                var type = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.DepartChackUser.ToString())?.ToList();
                if (type != null && type.Count > 0)
                {
                    if (type[0].Remarks == "Y")
                    {
                        hsjzlr = true;

                        layDeptName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        string fp = System.Windows.Forms.Application.StartupPath + "\\DepartNameinfo.json";
                        if (File.Exists(fp))  // 判断是否已有相同文件 
                        {
                            deptNameDtos = JsonConvert.DeserializeObject<List<DeptNameDto>>(File.ReadAllText(fp));
                            deptNameDtos = deptNameDtos.Where(o => o.DepatId != null && o.DoctId != null).ToList();
                        }
                    }
                }
                //repositoryItemLookUpEdit1.DataSource = CrisisSateHelper.CrisisSateFormatter();


            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }

            colXMTuPian.Visible = false;
            colXMCanKaoZhi.Visible = false;
            colXMDanWei.Visible = false;
            colXMZD.Visible = false;
            memoEditZiDianZD.Visible = false;
            layoutControlItemZD.Visibility = LayoutVisibility.Never;
           
            var clientreg = DefinedCacheHelper.GetClientRegNameComDto();
            //单位、分组
            txtClientRegID.Properties.DataSource = clientreg;

            //体检类别
            var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            if (Variables.ISZYB == "2")
            {
                checktype = checktype.Where(o => o.Text.Contains("职业健康")).ToList();
            }
            lookUpEditClientType.Properties.DataSource = checktype;

            //splitContainerControl1.Collapsed = true;
            //splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            if (_departmentGuidSys.Count > 0)
            {
                chkcmbDepartment.Properties.DataSource = DefinedCacheHelper.GetDepartments().Where(p =>
                _departmentGuidSys.Contains(p.Id)).ToList();
                chkcmbDepartment.Properties.DisplayMember = "Name";
                chkcmbDepartment.Properties.ValueMember = "Id";
            }
            #region 自动列宽
            gridZuHeXiangMu.BestFitColumns();
            colXMCanKaoZhi.BestFit();//自动列宽
            #endregion
        }

        /// <summary>
        /// 查询组合项目/项目的对应说明
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatProjectIStates(object arg)
        {
            try
            {
                return _projectIStateModel.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return _projectIStateModel.Find(r => r.Id == (int)ProjectIState.Not).Display;
            }
        }

        /// <summary>
        /// 绑定体检人信息
        /// </summary>
        /// <param name="CustomerBM">体检号</param>
        public void LoadCurrentCustomerReg(string CustomerBM)
        {
            AutoLoading(() =>
            {
                //获取体检人信息
                try
                {
                    var Query = new QueryClass();
                    Query.CustomerBM = CustomerBM;
                    //Query.RegisterState = (int)RegisterState.Yes;
                    _currentInputSys = new ATjlCustomerRegDto();
                    _currentInputSys = _doctorStation.GetCustomerRegList(Query).FirstOrDefault(); //获取客户信息

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                if (_currentInputSys == null)
                {
                    //XtraMessageBox.Show("此患者没有进行登记", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    alertControl.Show(this, "温馨提示", "系统没有搜到此患者信息哦,请检查一下体检号是否输入有误!");
                    EliminateFrom();
                    return;
                }

                if (_currentInputSys.RegisterState == (int)RegisterState.No)
                {
                    XtraMessageBox.Show("该体检人未登记，不能体检！");
                    EliminateFrom();
                    return;
                }

                //体检人图像
                try
                {

                    if (_currentInputSys.Customer.CusPhotoBmId.HasValue &&
                    _currentInputSys.Customer.CusPhotoBmId != Guid.Empty
                    )
                    {
                        var url = new PictureController().GetUrl(_currentInputSys.Customer.CusPhotoBmId.Value);
                        pictureZhaoPian.LoadAsync(url.Thumbnail);
                    }
                    else
                    {
                        pictureZhaoPian.Image = null;
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                //体检人图像
                try
                {
                    var Query = new QueryClass();
                    Query.CustomerRegId = _currentInputSys.Id;
                    _customerItemPicAllSys = new List<CustomerItemPicDto>();
                    _customerItemPicAllSys = _doctorStation.GetCustomerItemPicDtos(Query);
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }
                _valueUpdate = false;
                if (!string.IsNullOrEmpty(_currentInputSys.RiskS))
                {
                    //职业健康
                    labzyb.Text = string.Format("检查类型：{0}，危害因素：{1},岗位：{2},工种:{3}",
                       _currentInputSys.PostState, _currentInputSys.RiskS, _currentInputSys.WorkName,
                       _currentInputSys.TypeWork);
                    layoutzyb.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    labzyb.Text = "";
                    layoutzyb.Visibility = LayoutVisibility.Never;
                }

                //工种
                //labGongZhong.Text = _currentInputSys.TypeWork;

                ////危害因素
                //labWeiHaiYinSu.Text = _currentInputSys.RiskS;
                //labdepart.Text= _currentInputSys.Customer
                //体检类别 //

                if (_currentInputSys.PhysicalType.HasValue)
                {
                    var checktype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
                    labClientType.Text = checktype.FirstOrDefault(p => p.Value == _currentInputSys.PhysicalType)?.Text;
                }
                memoEditRegRemark.Text = _currentInputSys.Remarks;

                labdepart.Text = _currentInputSys.Customer.Department;
                //档案号
                labelDangAnHao.Text = _currentInputSys.Customer.ArchivesNum;

                //体检号
                labelTiJianHao.Text = _currentInputSys.CustomerBM;
                labelIdNumber.Text = _currentInputSys.Customer.IDCardNo;

                //姓名
                textEditXingMing.Text = _currentInputSys.Customer.Name;
                //性别
                if (_currentInputSys.Customer.Sex != null)
                    labXingBie.Text = SexHelper.CustomSexFormatter(_currentInputSys.Customer.Sex);

                //年龄
                if (_currentInputSys.RegAge.HasValue)
                {
                    labNianLing.Text = _currentInputSys.RegAge?.ToString();
                }
                else
                {  //性别
                    if (_currentInputSys.Customer.Age.HasValue)
                    { labNianLing.Text = _currentInputSys.Customer.Age.ToString(); } }

                //单位
                if (_currentInputSys.ClientInfo != null && _currentInputSys.ClientInfo != null)
                    textEditDanWei.Text = _currentInputSys.ClientInfo.ClientName;
                else
                    textEditDanWei.Text = "";

                //婚否
                if (_currentInputSys.MarriageStatus != null)
                    labHunFou.Text = MarrySateHelper.CustomMarrySateFormatter(_currentInputSys.MarriageStatus);
                if (_currentInputSys.PersonnelCategoryId.HasValue)
                {
                    if (personnelCategoryViewDtos != null && personnelCategoryViewDtos.Count > 0)
                    {
                        var cusType = personnelCategoryViewDtos.FirstOrDefault(
                            p => p.Id == _currentInputSys.PersonnelCategoryId)?.Name;
                        labelCategory.Text = cusType;
                    }
                }

                ////客户类别
                //if (_currentInputSys.Customer.CustomerType != null)
                //    labHuanZheLeiBie.Text = CustomerTypeHelper.CustomerTypeFormatter(_currentInputSys.Customer.CustomerType);
                //else
                //    labHuanZheLeiBie.Text = CustomerTypeHelper.CustomerTypeFormatter((int)CustomerType.ordinary);

                ////准备孕或育
                //if (_currentInputSys.ReadyPregnancybirth != null)
                //    labDengJiShiJian.Text = EnumHelper.GetEnumDesc((BreedState)_currentInputSys.ReadyPregnancybirth);
                labDengJiShiJian.Text = _currentInputSys.LoginDate?.ToString();
                memoEditKeShiXiaoJie.Text = string.Empty;
                memoEditZhenDuan.Text = string.Empty;
                txtpsum.Text = string.Empty;
                memoEditRemark.Text = _currentInputSys.Customer.Remarks?.ToString();
                gridJianChaXiangMu.DataSource = null;
                tvJianChaXiangMu.Nodes.Clear();

                //绑定tree数据
                GetCustomerItemGroup();
                if (_currentInputSys.SummSate != (int)SummSate.NotAlwaysCheck && _currentInputSys.SummSate.HasValue)
                {
                    btnShengChengXiaoJie.Enabled = false;
                    btnBaoCunShuJu.Enabled = false;
                    btnXiuGaiXiaoJie.Enabled = false;
                    btnQingChu.Enabled = false;
                    _isShiFouZongJian = false;
                    btnGetData.Enabled = false;
                }
                else
                {
                    btnShengChengXiaoJie.Enabled = true;
                    btnBaoCunShuJu.Enabled = true;
                    btnXiuGaiXiaoJie.Enabled = true;
                    btnQingChu.Enabled = true;
                    _isShiFouZongJian = true;
                    btnGetData.Enabled = true;
                }
                //if (splitContainerControl1.Collapsed) BinDingSurplusPatientGrid();
            }, Variables.LoadingForCloud);
        }

        /// <summary>
        /// 获取体检项目
        /// </summary>
        public void GetCustomerItemGroup()
        {
            try
            {
                var Query = new QueryClass();
                Query.CustomerRegId = _currentInputSys.Id;
                //只显示本科室或异常科室
                _currentCustomerItemGroupSys = new List<ATjlCustomerItemGroupDto>();
                _currentCustomerItemGroupSys = _doctorStation.GetCustomerAllItemGroup(Query).Where(o => _departmentGuidSys.Contains(o.DepartmentId) || o.CustomerRegItem.Any(n => n.Symbol != "M" && n.Symbol != "" && n.ProcessState == 2 && n.Symbol != null)).ToList();
                if (_currentCustomerItemGroupSys.Count == 0)
                {
                    XtraMessageBox.Show("该体检人未登记本科室项目，请核实！");
                    return;
                }
                var Nopaygroup = _currentCustomerItemGroupSys.Where(o => o.PayerCat == (int)PayerCatType.NoCharge).ToList();
                _currentCustomerItemGroupSys = _currentCustomerItemGroupSys.Where(o => o.PayerCat != (int)PayerCatType.NoCharge).ToList();
                if (_currentCustomerItemGroupSys.Count == 0)
                {
                    XtraMessageBox.Show("该体检人本科室项目均未收费，不能体检");
                    return;
                }
                if (Nopaygroup.Count > 0)
                {
                    var nopaygroupname = string.Join(",", Nopaygroup.Select(o => o.ItemGroupName).ToList());
                    XtraMessageBox.Show(nopaygroupname + "未收费，没有显示");
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }

            BinDingDataTreeS(Guid.Empty);
        }
        bool isOk = false;
        /// <summary>
        /// 绑定tree数据
        /// </summary>
        public void BinDingDataTreeS(Guid DeparmentId)
        {
            isOk = false;
            //获取科室
            var dictDepartmentBM = new Dictionary<Guid, string>();
            foreach (var item in _currentCustomerItemGroupSys.OrderBy(o => o.DepartmentOrder))
            {
                if (!dictDepartmentBM.Keys.Contains(item.DepartmentId) && item.DepartmentName != "系统科室")
                {
                    dictDepartmentBM.Add(item.DepartmentId, item.DepartmentName);
                }
            }
            tvJianChaXiangMu.Nodes.Clear();
            //获取有权限的第一个科室
            var firstDeparmentName = string.Empty;
            //获取有权限的第一个科室只进一次
            bool IsFirst = true;
            #region MyRegion
            var DepartIds = chkcmbDepartment.Properties.GetCheckedItems().ToString().Split(',').Where(p =>
         p != "").ToList();
            List<Guid> NowDepartIds = new List<Guid>();
            if (DepartIds.Count > 0)
            {
                
                foreach (var departId in DepartIds)
                {
                    if (!string.IsNullOrEmpty(departId))
                    {
                        NowDepartIds.Add(Guid.Parse(departId));
                    }
                }                
            }
            #endregion
            //绑定树
            for (var i = 0; i < dictDepartmentBM.Count; i++)
            {
                //过滤科室

                if (NowDepartIds.Count > 0)
                {
                    if (!NowDepartIds.Contains(dictDepartmentBM.ElementAt(i).Key))
                    {
                        continue;
                    }
                }
                var nodeDepartment = new TreeNode();
                nodeDepartment.Tag = dictDepartmentBM.ElementAt(i).Key;//+","+ dictDepartmentBM.ElementAt(i).Value;
                nodeDepartment.Text = dictDepartmentBM.ElementAt(i).Value;
                if (!_departmentGuidSys.Contains(dictDepartmentBM.ElementAt(i).Key))
                {
                    nodeDepartment.ForeColor = Color.SlateGray;
                }
                else
                {
                    nodeDepartment.ExpandAll();
                    if (IsFirst)
                    {
                        firstDeparmentName = dictDepartmentBM.ElementAt(i).Value;
                        IsFirst = false;
                    }
                }
                nodeDepartment.Name = dictDepartmentBM.ElementAt(i).Value;
                foreach (var ItemGroup in _currentCustomerItemGroupSys
                    .Where(o => o.DepartmentId == dictDepartmentBM.ElementAt(i).Key).OrderBy(p=>p.CheckState).ThenBy(o => o.ItemGroupOrder))
                {
                    var nodeGroup = new TreeNode();
                    nodeGroup.Tag = ItemGroup.Id;
                    nodeGroup.Text = ItemGroup.ItemGroupName + "(" +
                                     ProjectIStateHelper.ProjectIStateFormatter(ItemGroup.CheckState) + ")";

                    if (!_departmentGuidSys.Contains(dictDepartmentBM.ElementAt(i).Key))
                    {
                        nodeGroup.ForeColor = Color.SlateGray;
                    }
                    //if (ItemGroup.CustomerRegItem.Where(o => o.CrisisSate == (int)CrisisSate.Abnormal).Count() > 0)
                    if (ItemGroup.CustomerRegItem.Where(o => o.CrisisSate == (int)CrisisSate.Abnormal).Count() > 0)
                    {
                        nodeGroup.BackColor = ColorTranslator.FromHtml("#FF8080");
                    }
                    else if (ItemGroup.CheckState == (int)ProjectIState.Part)
                    {
                        nodeGroup.BackColor = Color.LimeGreen;
                    }
                    else if (ItemGroup.CheckState == (int)ProjectIState.Complete)
                    {
                        nodeGroup.BackColor = Color.DeepSkyBlue;
                    }
                    if (ItemGroup.CustomerRegItem.Where(o => o.Symbol == SymbolHelper.SymbolFormatter(Symbol.High) || o.Symbol == SymbolHelper.SymbolFormatter(Symbol.Low) || o.Symbol == SymbolHelper.SymbolFormatter(Symbol.Abnormal)).Count() > 0)
                    {
                        nodeGroup.ForeColor = ColorTranslator.FromHtml("#FF0000");
                        nodeGroup.ForeColor = Color.Red;
                    }
                    //else if (ItemGroup.CustomerRegItem.Where(o => o.PositiveSate==(int)PositiveSate.Abnormal).Count() > 0)
                    //{
                    //    nodeGroup.ForeColor = ColorTranslator.FromHtml("#FF0000");
                    //    nodeGroup.ForeColor = Color.Red;
                    //}

                    nodeDepartment.Nodes.Add(nodeGroup);
                }
                tvJianChaXiangMu.Nodes.Add(nodeDepartment);
            }
            if (DeparmentId != Guid.Empty)
            {
                var name = dictDepartmentBM[DeparmentId];
                tvJianChaXiangMu.SelectedNode = tvJianChaXiangMu.Nodes[name];
                tvJianChaXiangMu.SelectedNode.EnsureVisible();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(firstDeparmentName))
                {
                    tvJianChaXiangMu.Focus();
                    tvJianChaXiangMu.SelectedNode = tvJianChaXiangMu.Nodes[firstDeparmentName];
                    tvJianChaXiangMu.SelectedNode.EnsureVisible();
                    var rowHandle = gridZuHeXiangMu.GetRowHandle(0);
                   
                        gridZuHeXiangMu.Focus();
                        gridZuHeXiangMu.ShowEditor();
                        gridZuHeXiangMu.FocusedRowHandle = rowHandle;

                        gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                        if (dockPanelZiDian.Visibility == DockVisibility.Visible)
                        {
                            Dictionaries();
                        }
                    isOk = true;



                }

            }

        }

        /// <summary>
        /// 绑定体检项目grid列表
        /// </summary>
        /// <param name="DataCustomerItemGroup"></param>
        public void BinDingGridJianChaXiangMu(List<ATjlCustomerItemGroupDto> DataCustomerItemGroup)
        {
            var SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
            var DataZhanShiLieList = new List<GridZhanShiLei>();
            var DepartmentId = Guid.Empty;
            if (tvJianChaXiangMu.SelectedNode.Parent == null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
            }
            else if (tvJianChaXiangMu.SelectedNode.Parent != null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
            }
            foreach (var itemGroup in DataCustomerItemGroup.OrderBy(o => o.DepartmentOrder).OrderBy(o => o.ItemGroupOrder))
            {

                var ycitems = itemGroup.CustomerRegItem.Where(o => o.Symbol != "M" && o.Symbol != "" || _departmentGuidSys.Contains(o.DepartmentId)).ToList();
                //暂时不做
                //var ycitems = itemGroup.CustomerRegItem.ToList();
                foreach (var item in ycitems.OrderBy(o => o.ItemOrder))
                {
                    var DataZhanShiLie = new GridZhanShiLei();
                    DataZhanShiLie.ZuHeId = itemGroup.Id;
                    DataZhanShiLie.ZuHeMingCheng = itemGroup.ItemGroupName;
                    DataZhanShiLie.ZuHeZhuangTai = itemGroup.CheckState;
                    DataZhanShiLie.ZuHeShunXu = itemGroup.ItemGroupOrder;
                    DataZhanShiLie.XiangMuId = item.Id;
                    DataZhanShiLie.XiangMuMingCheng = item.ItemName;
                    DataZhanShiLie.XiangMuWeiJiZhi = item.CrisisSate;
                    if (item.ItemName=="牙周")
                    {

                    }
                    DataZhanShiLie.WeiJiZhiLeibie = item.CrisisLever;
                    DataZhanShiLie.XiangMuBiaoShi = item.Symbol;
                    DataZhanShiLie.XiangMuBMId = item.ItemId;
                    DataZhanShiLie.ItemBM = item.ItemBM?.ItemBM;
                    DataZhanShiLie.ProcessState = item.ProcessState;
                   // DataZhanShiLie.XiangMuWeiJiZhi = item.CrisisSate;
                    DataZhanShiLie.DepartmentId = DepartmentId;
                    if (item.ItemBM != null && item.ItemBM.moneyType != (int)ItemType.Number && item.ItemBM.moneyType != (int)ItemType.Calculation)
                    {
                        if (item.ProcessState != (int)ProjectIState.Complete && string.IsNullOrWhiteSpace(item.ItemResultChar))
                        {

                            var itemDatalist = _itemStandardSys.Where(o =>
                                    o.ItemId == item.ItemId && o.PositiveSate == (int)PositiveSate.Normal &&
                                    o.MaxAge >= _currentInputSys.Customer.Age && o.MinAge <= _currentInputSys.Customer.Age &&
                                    (o.Sex == _currentInputSys.Customer.Sex.ToString() || o.Sex == SexGenderNotSpecified)).OrderBy(p => p.OrderNum)
                                .ToList();
                            var itemData = itemDatalist.FirstOrDefault();
                            //根据体检类别过滤参考值
                            if (itemDatalist.Count > 1)
                            {
                                var itemStander = itemDatalist.Where(p => p.PhysicalType == _currentInputSys.PhysicalType).FirstOrDefault();
                                if (itemStander != null)
                                {
                                    itemData = itemStander;
                                }
                                else
                                {
                                    var itemStanderEmp = itemDatalist.Where(p => p.PhysicalType == null).FirstOrDefault();
                                    if (itemStanderEmp != null)
                                    {
                                        itemData = itemStanderEmp;
                                    }
                                }

                            }
                            if (itemData != null)
                            {
                                DataZhanShiLie.XiangMuJieGuo = itemData.Summ;
                                item.ItemResultChar = itemData.Summ;
                                if (CurrentUser.TbmDepartments.Where(o => o.Id == DepartmentId).ToList().Count() > 0)
                                {
                                    itemGroup.IsUpdate = true;
                                }
                                if (item.ItemBM.moneyType == (int)ItemType.YinYang)
                                {
                                    DataZhanShiLie.XiangMuCanKaoZhi = itemData.Summ;
                                    item.Stand = itemData.Summ;

                                }
                            }
                        }
                        else
                        {
                            DataZhanShiLie.XiangMuJieGuo = item.ItemResultChar;
                            //诊断
                            if (!string.IsNullOrEmpty(item.ItemDiagnosis))
                            { DataZhanShiLie.XiangMuZD = item.ItemDiagnosis; }
                            if (item.ItemBM.moneyType == (int)ItemType.YinYang)
                            {
                                DataZhanShiLie.XiangMuCanKaoZhi = item.Stand;

                            }
                        }

                    }
                    else
                    {
                        DataZhanShiLie.XiangMuJieGuo = item.ItemResultChar;
                        if (string.IsNullOrWhiteSpace(item.Stand))
                        {
                            var itemData = _itemStandardSys.Where(o =>
                                    o.ItemId == item.ItemId && o.PositiveSate == (int)PositiveSate.Normal &&
                                    o.MaxAge >= _currentInputSys.Customer.Age && o.MinAge <= _currentInputSys.Customer.Age &&
                                    (o.Sex == _currentInputSys.Customer.Sex.ToString() || o.Sex == SexGenderNotSpecified)).OrderBy(p => p.OrderNum)
                                .FirstOrDefault();
                            if (itemData != null)
                            {
                                DataZhanShiLie.XiangMuCanKaoZhi = (itemData.MinValue + "-" + itemData.MaxValue).Replace(".00", "");
                                item.Stand = (itemData.MinValue + "-" + itemData.MaxValue).Replace(".00", "");
                            }
                        }
                        else
                        {
                            DataZhanShiLie.XiangMuCanKaoZhi = item.Stand;
                        }

                        if (string.IsNullOrWhiteSpace(item.Unit))
                        {
                            DataZhanShiLie.XiangMuDanWei = item.ItemBM?.Unit;
                            item.Unit = item.ItemBM?.Unit;
                        }
                        else
                        {
                            DataZhanShiLie.XiangMuDanWei = item.Unit;
                        }
                    }
                    //获取体重
                    //if (DataZhanShiLie.ItemBM == "0500002" && !string.IsNullOrWhiteSpace(DataZhanShiLie.XiangMuJieGuo))
                    //{
                    //    _tiZhong = double.Parse(DataZhanShiLie.XiangMuJieGuo);
                    //}
                    //if (DataZhanShiLie.ItemBM == "0500001" && !string.IsNullOrWhiteSpace(DataZhanShiLie.XiangMuJieGuo))
                    //{
                    //    _shenGao = double.Parse(DataZhanShiLie.XiangMuJieGuo);
                    //}

                    DataZhanShiLieList.Add(DataZhanShiLie);
                }
            }
            gridZuHeXiangMu.Columns[0].GroupIndex = 0;
            gridJianChaXiangMu.DataSource = null;
            gridJianChaXiangMu.DataSource = DataZhanShiLieList;

            gridZuHeXiangMu.BestFitColumns();
            colXMCanKaoZhi.BestFit();//自动列宽
           
        }

        /// <summary>
        /// 危急值列表绑定
        /// </summary>
        public void BinDingCriticalValueGrid()
        {
            AutoLoading(() =>
            {
                var Query = new QueryClassTwo();
                Query.DepartmentBMList = new List<Guid>();
                Query.DepartmentBMList = _departmentGuidSys;
                Query.CheckState = (int)ProjectIState.Not;
                Query.CrisisSate = (int)CrisisSate.Abnormal;
                gridViewWeiJiZhi.Columns["状态"].DisplayFormat.FormatType = FormatType.Custom;
                gridViewWeiJiZhi.Columns["状态"].DisplayFormat.Format =
                    new CustomFormatter(CrisisVisitSateHelper.CriticalTypeStateFormatter);

                gridViewCrical.Columns["分类"].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCrical.Columns["分类"].DisplayFormat.Format =
                    new CustomFormatter(CriticalTypeStateHelper.CriticalTypeStateFormatter);


                gridControlWeiJiZhi.DataSource =
                    _doctorStation.GetCriticalCusList(Query).OrderByDescending(o => o.登记时间);

            });
        }

        /// <summary>
        /// 绑定患者列表
        /// </summary>
        public void BinDingSurplusPatientGrid()

        {
            AutoLoading(() =>
            {
                //体检患者列表
                var Query = new QueryClassTwo();
                Query.DepartmentBMList = new List<Guid>();
                var DepartIds = chkcmbDepartment.Properties.GetCheckedItems().ToString().Split(',').Where(p =>
                p != "").ToList();

                if (DepartIds.Count > 0)
                {
                    List<Guid> NowDepartIds = new List<Guid>();
                    foreach (var departId in DepartIds)
                    {
                        if (!string.IsNullOrEmpty(departId))
                        {
                            NowDepartIds.Add(Guid.Parse(departId));
                        }
                    }
                    Query.DepartmentBMList = NowDepartIds;
                }
                else
                {
                    Query.DepartmentBMList = _departmentGuidSys;
                }
            #region 本科室部分检查算未检
            //var YbYS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusList, 3)?.Remarks;
 
            //    if (!string.IsNullOrEmpty(YbYS))
            //    {
            //        var departNamelist = YbYS.Replace("，", ",").Split(',').ToList();
            //        var deparids = DefinedCacheHelper.GetDepartments().Where(p => departNamelist.Contains(p.Name)).Select(
            //            p => p.Id).ToList();
            //        Query.DepartmentIDList = deparids;
            //    }
                #endregion
                if (checkEditdt.Checked == true)
                {
                    if (dateEditRYKaiShiShiJian.EditValue != null && dateEditRYJieShuShiJian.EditValue != null)
                    {
                        Query.LastModificationTimeBign = DateTime.Parse(dateEditRYKaiShiShiJian.EditValue.ToString());
                        Query.LastModificationTimeEnd = DateTime.Parse(dateEditRYJieShuShiJian.EditValue.ToString()).Date.AddDays(1);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txtRYTianShu.Text))
                {
                    int tian = int.Parse(txtRYTianShu.Text);
                    tian--;
                    Query.LastModificationTimeBign = Convert.ToDateTime(DateTime.Now.Date.AddDays(-tian).ToString("yyyy-MM-dd") + " 00:00:00");
                    Query.LastModificationTimeEnd = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                }
                else
                {
                    Query.LastModificationTimeBign = DateTime.Now.Date;
                    Query.LastModificationTimeEnd = DateTime.Now.Date.AddDays(1);
                }
              
                if (gridLookUpSex.EditValue != null)
                {
                    Query.Sex = gridLookUpSex.EditValue as int?;
                }
                if (!string.IsNullOrWhiteSpace(textEdit1.Text))
                {
                    Query.Name = textEdit1.Text;
                }
                if (txtClientRegID.EditValue != null)
                {
                    Query.ClientRegID = (Guid)txtClientRegID.EditValue;
                }
                if (txtClientRegID.EditValue != null)
                {
                    Query.ClientRegID = (Guid)txtClientRegID.EditValue;
                }
                if (lookUpEditClientType.EditValue != null)
                {
                    Query.PhysicalType = (int)lookUpEditClientType.EditValue;
                }
                //查询人员数据
                //var RtClass = _doctorStation.GetReturnClass(Query);
                //labYiJianRenShu.Text = RtClass.AlreadyInspect.ToString();
                //labWeiJianRenShu.Text = RtClass.NotInspect.ToString();
                //labYiDengJiShu.Text = RtClass.AlreadyRegister.ToString();
                if (checkHas.Checked == true)
                {
                    // Query.isJC = 1;
                    Query.CheckState = (int)ProjectIState.Complete;
                }
                if (checkEditRyWeiJianRenShu.Checked == true)
                {
                    // Query.isJC = 1;
                    Query.CheckState = (int)ProjectIState.Not;
                }
                if (checkEditPart.Checked == true)
                {
                    // Query.isJC = 1;
                    Query.CheckState = (int)ProjectIState.Part;
                }
                
                if (comboBoxEditTimeType.Text == "登记日期")
                {
                    Query.isJC = 0;
                }
                else if (comboBoxEditTimeType.Text.Contains("检查日期"))
                { Query.isJC = 1; }
                else if (comboBoxEditTimeType.Text == "体检日期")
                { Query.isJC = 2; }


                //if (checkEditRyWeiJianRenShu.Checked)
                //{
                //    gridControlDangTianHuanZhe.DataSource =
                //    _doctorStation.GetSameDayCusTomerReg(Query).Where(o => o.RegisterState != (int)RegisterState.No && o.CustomerItemGroup.Any(n=>_departmentGuidSys.Contains(n.DepartmentId) && n.CheckState == (int)PhysicalEState.Not)).OrderByDescending(m => m.LoginDate).ToList();
                //}
                //else
                //{
                var cusResult = _doctorStation.GetSameDayCusTomerReg(Query).Where(o => o.RegisterState != (int)RegisterState.No).OrderByDescending(m => m.LoginDate).ToList();
                if (checkState.Checked == true)
                {
                    cusResult = cusResult.Where(p => p.CheckSate != 3).ToList();
                }
                gridControlDangTianHuanZhe.DataSource = cusResult;
                //}
                var TJRY = gridControlDangTianHuanZhe.DataSource as List<TjlCustomerRegForDoctorDto>;
                labYiJianRenShu.Text = TJRY.Count(o => o.CheckSate == 2 || o.CheckSate == 3).ToString();
                labWeiJianRenShu.Text = TJRY.Count(o => o.CheckSate == 1).ToString();
                labYiDengJiShu.Text = TJRY.Count(o => o.RegisterState == 2).ToString();
                labsex.Text = "男：" + TJRY.Count(o => o.Customer.Sex == (int)Sex.Man).ToString() + " 女：" + TJRY.Count(o => o.Customer.Sex == (int)Sex.Woman).ToString();
            });
        }

        /// <summary>
        /// 绑定剩余患者列表
        /// </summary>
        public void BinDingGeRenTiJianLiShi()
        {
            if (_currentInputSys != null && _currentInputSys.Customer != null && _currentInputSys.Customer.Id != null)
                AutoLoading(() =>
                {
                    //绑定当前体检人历史
                    //    var Query = new QueryClassTwo();
                    //    Query.CustomerId = _currentInputSys.Customer.Id;
                    //    GridViewGeRenTiJianLiShi.DataSource =
                    //        _doctorStation.GetTjlCustomerRegDto(Query).OrderByDescending(o => o.LoginDate);
                });
        }

        /// <summary>
        /// 人员信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchHuanZheXinXi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(searchHuanZheXinXi.Text))
            {
                var txt = searchHuanZheXinXi.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt)) {
                    //if ( txt.Length > 10)
                    //{
                    //    var Bar = _doctorStation.GetCusBarPrintInfo(new QueryClass() { BarNumBM = txt });
                    //    LoadCurrentCustomerReg(Bar);
                    //}
                    //else
                    //{
                    LoadCurrentCustomerReg(txt);
                    //}
                }
            }
        }

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchControlZiDianChaXun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
                if (XMID != null && !string.IsNullOrWhiteSpace(XMID))
                {
                    var text = searchControlZiDianChaXun.Text.Trim();
                    gridControlZiDian.DataSource = _itemDictionarySys.Where(o =>
                        o.iteminfoBMId == Guid.Parse(XMID) &&
                        (o.HelpChar.Contains(text) || o.Word.Contains(text) || o.WBCode.Contains(text))).ToList();
                }
            }
        }

        /// <summary>
        /// 点击照片显示隐藏患者详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureZhaoPian_Click(object sender, EventArgs e)
        {
            //if (personneControl.Visible)
            //{
            //    personneControl.Visible = false;
            //    personneControl.CurrentInputSys = null;
            //}
            //else
            //{
            //    personneControl.Visible = true;
            //    personneControl.Focus();
            //    personneControl.CurrentInputSys = CurrentInputSys;
            //}
        }

        /// <summary>
        /// 弹出右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridJianChaXiangMu_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }

        /// <summary>
        /// 点击树绑定列表组合与项目数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvJianChaXiangMu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvJianChaXiangMu.SelectedNode != null)
            {
                var SelectId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                var SelectDepId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                var SelectDepText = tvJianChaXiangMu.SelectedNode.Text;
                var DataCustomerItemGroup = new List<ATjlCustomerItemGroupDto>();
                if (tvJianChaXiangMu.SelectedNode.Level == 0)
                {
                    DataCustomerItemGroup = _currentCustomerItemGroupSys.Where(o => o.DepartmentId == SelectId)
                        .OrderBy(n => n.ItemGroupOrder).ToList();
                    if (_departmentGuidSys.Contains(SelectId))
                    {


                        cglueJianChaYiSheng.EditValue = CurrentUser.Id; //绑定默认
                        cglueShenHeYiSheng.EditValue = CurrentUser.Id; //绑定默认
                        //设置默认检查医生
                        if (hsjzlr == true && deptNameDtos != null)
                        {
                            var userlsit = _currentUserdtoSys.Select(o => o.Id).ToList();
                            var depart = deptNameDtos.FirstOrDefault(o => o.DepatId == SelectId && o.DoctId != null && userlsit.Contains(o.DoctId.Value));
                            if (depart != null)
                            {
                                cglueJianChaYiSheng.EditValue = depart.DoctId;
                            }
                        }
                    }
                }
                else if (tvJianChaXiangMu.SelectedNode.Level == 1)
                {
                    SelectDepId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    SelectDepText = tvJianChaXiangMu.SelectedNode.Parent.Text;
                    DataCustomerItemGroup = _currentCustomerItemGroupSys.Where(o => o.Id == SelectId).ToList();
                    if (_departmentGuidSys.Contains(SelectDepId))
                    {
                        if (DataCustomerItemGroup[0].InspectEmployeeBMId != null)
                            cglueJianChaYiSheng.EditValue = DataCustomerItemGroup[0].InspectEmployeeBMId; //绑定检查人默认
                        else
                        {
                            cglueJianChaYiSheng.EditValue = CurrentUser.Id; //绑定检查人默认
                            //设置默认检查医生
                            if (hsjzlr == true && deptNameDtos != null)
                            {
                                var userlsit = _currentUserdtoSys.Select(o => o.Id).ToList();
                                var depart = deptNameDtos.FirstOrDefault(o => o.DepatId == SelectDepId && o.DoctId != null && userlsit.Contains(o.DoctId.Value));
                                if (depart != null)
                                {
                                    cglueJianChaYiSheng.EditValue = depart.DoctId;
                                }
                            }
                        }
                        if (DataCustomerItemGroup[0].CheckEmployeeBMId != null)
                            cglueShenHeYiSheng.EditValue = DataCustomerItemGroup[0].CheckEmployeeBMId; //绑定审核人
                        else
                            cglueShenHeYiSheng.EditValue = CurrentUser.Id; //绑定审核人默认
                    }
                }

                BinDingGridJianChaXiangMu(DataCustomerItemGroup);
                //禁用按钮
                if (CurrentUser.TbmDepartments.Where(o => o.Id == SelectDepId).ToList().Count() > 0 && _isShiFouZongJian)
                {
                    colXMJieGuo.OptionsColumn.AllowEdit = true;
                    btnShengChengXiaoJie.Enabled = true;
                    btnBaoCunShuJu.Enabled = true;
                    btnXiuGaiXiaoJie.Enabled = true;
                    btnQingChu.Enabled = true;
                    btnGetData.Enabled = true;
                }
                else
                {
                    colXMJieGuo.OptionsColumn.AllowEdit = false;
                    btnShengChengXiaoJie.Enabled = false;
                    btnBaoCunShuJu.Enabled = false;
                    btnXiuGaiXiaoJie.Enabled = false;
                    btnQingChu.Enabled = false;
                    btnGetData.Enabled = false;
                }
                gridColumnDictionaries.Visible = false;
                int VisibleIndex = 2;
                //诊断展示
                if (DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "DoctorStationDisplayColumn" && o.Value == 3).FirstOrDefault()?.Remarks != null)
                {
                    var GuidLieZhanShi = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "DoctorStationDisplayColumn" && o.Value == 3).FirstOrDefault()?.Remarks.Split(',');
                    if (GuidLieZhanShi.Contains(SelectDepText.ToString().ToUpper()))
                    {
                        layoutControlItemZD.Visibility = LayoutVisibility.Always;
                        memoEditZiDianZD.Visible = true;
                        colXMZD.Visible = true;
                        colXMZD.VisibleIndex = VisibleIndex;
                        VisibleIndex++;

                    }
                    else
                    {
                        layoutControlItemZD.Visibility = LayoutVisibility.Never;
                        colXMZD.Visible = false;
                        memoEditZiDianZD.Visible = false;
                    }

                }

                //参考值展示
                if (_basicDictionarySys.Where(o => o.Id == Guid.Parse(DoctorStationConstant.CANKAOZHIKESHI))
                        .FirstOrDefault() != null)
                {
                    var GuidLieZhanShi = _basicDictionarySys
                        .Where(o => o.Id == Guid.Parse(DoctorStationConstant.CANKAOZHIKESHI)).FirstOrDefault().Remarks
                        .Split(',');
                    if (GuidLieZhanShi.Contains(SelectDepText.ToString().ToUpper()))
                    {
                        colXMCanKaoZhi.Visible = true;
                        colXMCanKaoZhi.VisibleIndex = VisibleIndex;
                        VisibleIndex++;
                    }
                    else
                    {
                        colXMCanKaoZhi.Visible = false;
                    }

                }
                //图像展示
                if (_basicDictionarySys.Where(o => o.Id == Guid.Parse(DoctorStationConstant.TUXIANGXIANSHIKESHI))
                        .FirstOrDefault() != null)
                {
                    var GuidLieZhanShi = _basicDictionarySys
                        .Where(o => o.Id == Guid.Parse(DoctorStationConstant.TUXIANGXIANSHIKESHI)).FirstOrDefault()
                        .Remarks.Split(',');
                    if (GuidLieZhanShi.Contains(SelectDepText.ToString().ToUpper()))
                    {
                        colXMTuPian.Visible = true;
                        colXMTuPian.VisibleIndex = VisibleIndex;
                        VisibleIndex++;
                    }
                    else
                    {
                        colXMTuPian.Visible = false;
                    }
                }

                //单位
                if (_basicDictionarySys.Where(o => o.Id == Guid.Parse(DoctorStationConstant.DANWEIKESHI))
                        .FirstOrDefault() != null)
                {
                    var GuidLieZhanShi = _basicDictionarySys
                        .Where(o => o.Id == Guid.Parse(DoctorStationConstant.DANWEIKESHI)).FirstOrDefault().Remarks
                        .Split(',');
                    if (GuidLieZhanShi.Contains(SelectDepText.ToString().ToUpper()))
                    {
                        colXMDanWei.Visible = true;
                        colXMDanWei.VisibleIndex = VisibleIndex;
                        VisibleIndex++;
                    }
                    else
                    {
                        colXMDanWei.Visible = false;
                    }
                }
                // gridColumnDictionaries.Visible = true;
                colXMDanWei.VisibleIndex = VisibleIndex;
                memoEditKeShiXiaoJie.Text = string.Empty;
                memoEditZhenDuan.Text = string.Empty;
                txtpsum.Text = string.Empty;
                layPSum.Visibility = LayoutVisibility.Never;
                //科室小结
                if (_currentInputSys.CustomerDepSummary != null)
                {
                    var ipt = _currentInputSys.CustomerDepSummary.Where(o => o.DepartmentBMId == SelectDepId)
                        .FirstOrDefault();
                    if (ipt != null)
                    {
                        memoEditKeShiXiaoJie.Text += ipt.CharacterSummary;
                        memoEditZhenDuan.Text += ipt.DagnosisSummary;
                        if (!string.IsNullOrEmpty(ipt.PrivacyCharacterSummary))
                        {
                            layPSum.Visibility = LayoutVisibility.Always;
                            txtpsum.Text = ipt.PrivacyCharacterSummary;
                        }
                    }

                }
                //#region 光标定位在第一个项目
                //if (gridZuHeXiangMu.RowCount > 0)
                //{
                //    gridZuHeXiangMu.Focus();
                //    gridZuHeXiangMu.FocusedRowHandle = 0;
                //    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                //    gridZuHeXiangMu.ShowEditor();
                //}
                //#endregion
                BeginInvoke(new MethodInvoker(() =>
                {
                    gridZuHeXiangMu.Focus();
                    gridZuHeXiangMu.ShowEditor();
                    gridZuHeXiangMu.FocusedRowHandle = 0;

                    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                }));
            }
        }

        /// <summary>
        /// 双击体检人历史切换信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewGeRenJianChaShi_RowClick(object sender, RowClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            //{
            //    var CustomerBM = gridViewGeRenJianChaShi.GetFocusedRowCellValue(colLiShiTiJianHao.FieldName);
            //    //判断体检人和体检号是否存在
            //    if (_currentInputSys != null && !string.IsNullOrWhiteSpace(_currentInputSys.CustomerBM) && _currentInputSys.CustomerBM != CustomerBM.ToString())
            //    {
            //        LoadCurrentCustomerReg(CustomerBM.ToString());
            //    }
            //    else if (CustomerBM.ToString() != _currentInputSys.CustomerBM)
            //    {
            //        //获取修改数据
            //        var data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate).ToList();
            //        //判断是否存在未保存的修改数据
            //        if (data != null && data.Count() > 0)
            //        {
            //            var question = XtraMessageBox.Show("存在未保存信息是否保存后切换？", "询问",
            //         MessageBoxButtons.YesNo,
            //         MessageBoxIcon.Question,
            //         MessageBoxDefaultButton.Button2);
            //            if (question == DialogResult.Yes)
            //            {
            //                UpdateCurrentItem(true);
            //                searchHuanZheXinXi.Text = CustomerBM.ToString();
            //                LoadCurrentCustomerReg(CustomerBM.ToString());
            //            }
            //            else if (question == DialogResult.No)
            //            {
            //                searchHuanZheXinXi.Text = CustomerBM.ToString();
            //                LoadCurrentCustomerReg(CustomerBM.ToString());
            //            }
            //        }
            //        else
            //        {
            //            searchHuanZheXinXi.Text = CustomerBM.ToString();
            //            LoadCurrentCustomerReg(CustomerBM.ToString());
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 待检人员列表双击切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewDangTianHuanZhe_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
                var CustomerBM = gridViewDangTianHuanZhe.GetFocusedRowCellValue(colDangTianTiJianHao.FieldName);
                if (_currentInputSys == null || _currentInputSys.CustomerBM == null)
                {
                    searchHuanZheXinXi.Text = CustomerBM.ToString();
                    LoadCurrentCustomerReg(CustomerBM.ToString());
                }
                else if (CustomerBM != null && CustomerBM.ToString() != _currentInputSys.CustomerBM)
                {
                    //获取修改数据
                    var data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate).ToList();
                    //判断是否存在未保存的修改数据
                    if (data != null && data.Count() > 0 && _valueUpdate == true)
                    {
                        var question = XtraMessageBox.Show("存在未保存信息是否保存后切换？", "询问",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button2);
                        //判断是保存还是不保存
                        if (question == DialogResult.Yes)
                        {
                            UpdateCurrentItem(true);
                            searchHuanZheXinXi.Text = CustomerBM.ToString();
                            LoadCurrentCustomerReg(CustomerBM.ToString());
                        }
                        else if (question == DialogResult.No)
                        {
                            searchHuanZheXinXi.Text = CustomerBM.ToString();
                            LoadCurrentCustomerReg(CustomerBM.ToString());
                        }
                    }
                    else
                    {
                        searchHuanZheXinXi.Text = CustomerBM.ToString();
                        LoadCurrentCustomerReg(CustomerBM.ToString());
                    }

                }
            }
        }

        /// <summary>
        /// 生成科室小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShengChengXiaoJie_Click(object sender, EventArgs e)
        {
            btnShengChengXiaoJie.Enabled = false;
            ShengChengKeShiXiaoJie();
            if (customGridLookUpEditZhenDuan.Properties.DataSource != null)
            {
                customGridLookUpEditZhenDuan.Properties.DataSource = _itemDictionarySys.Where(o => !string.IsNullOrWhiteSpace(o.Period));

            }
            #region 危急值提示
            var crilist = _currentCustomerItemGroupSys.SelectMany(p => p.CustomerRegItem).Where(p => p.CrisisSate == (int)CrisisSate.Abnormal).Select
                 (p => new { p.ItemName, p.ItemResultChar, p.CrisisLever }).ToList();
            if (crilist.Count > 0)
            {
                foreach (var cri in crilist)
                {
                    var cricaltype = CriticalTypeStateHelper.CriticalTypeStateFormatter(cri.CrisisLever);

                    string ts = string.Format(
                        @"体检号：{0} 重要异常结果分类：{1}<br>
姓名：{2}    性别：{3}<br>
项目：{4}<br>
结果：{5}",
        _currentInputSys.CustomerBM, cricaltype, _currentInputSys.Customer.Name, _currentInputSys.Customer.Sex, cri.ItemName, cri.ItemResultChar
                        );
                    alertControl.Show(this, "危急值上报", ts);
                }
            }
            #endregion

            btnShengChengXiaoJie.Enabled = true;
            BeginInvoke(new MethodInvoker(() =>
            {
                searchHuanZheXinXi.Focus();
                searchHuanZheXinXi.SelectAll();
            }));
          
        }
        /// <summary>
        /// 生成科室小结
        /// </summary>
        private void ShengChengKeShiXiaoJie()
        {
            //var islock = _doctorStation.JudgeLocking(new QueryClass() { CustomerRegId = _currentInputSys.Id });
            //if (_currentInputSys != null && islock.IsLock == true)
            //{
            //    alertControl.Show(this, "系统提示", "患者已被：" + islock.LockUser + "锁定,已经不能修改啦!");
            //    //ShowMessageSucceed("患者已锁定不可更改!");
            //    return;
            //}

            //获取当前选择科室id
            var DepartmentId = Guid.Empty;
            if (tvJianChaXiangMu.SelectedNode == null)
            {
                alertControl.Show(this, "系统提示", "还没有选择科室呀,请先选择科室再生成小结!");
                //ShowMessageSucceed("请选择科室或项目!");
                return;
            }
            if (tvJianChaXiangMu.SelectedNode.Parent == null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
            }
            else if (tvJianChaXiangMu.SelectedNode.Parent != null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
            }
            //保存数据
          bool isft=  UpdateCurrentItem(false);
            if (isft == false)
            {
                return;
            }
            //if (_currentCustomerItemGroupSys.Where(o => o.DepartmentId == DepartmentId && o.CheckState != (int)ProjectIState.Complete).Count() != 0)
            //{
            //    XtraMessageBox.Show("当前科室存在未完成项目!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //生成小结
            if (_itemStandardSys == null ||
                _currentCustomerItemGroupSys.Where(o => o.DepartmentId == DepartmentId).Count() == 0)
            {
                alertControl.Show(this, "温馨提示", "此科室没有体检项目哦!");
                //XtraMessageBox.Show("患者没有体检数据或没有系统小结编码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //小结
            var outcreateCustomerDepSummary = new List<ATjlCustomerDepSummaryDto>();
            //体检项目
            var outlstaTjlCustomerItemGroupDto = new List<ATjlCustomerItemGroupDto>();
            ////专科建议
            //var outsearchTjlCustomerSummaryDtos = new List<SearchTjlCustomerSummaryDto>();
            CreateConclusionDto conclusion = new CreateConclusionDto();
            conclusion.CustomerBM = _currentInputSys.CustomerBM;
            conclusion.Department = new List<Guid>() { DepartmentId };
            _doctorStation.CreateConclusion(conclusion);
            QueryClass qCalss = new QueryClass();
            qCalss.CustomerRegId = _currentInputSys.Id;
            outcreateCustomerDepSummary = _doctorStation.GetCustomerDepSummary(qCalss);
            // outsearchTjlCustomerSummaryDtos = _doctorStation.GetCustomerSummary(qCalss);
            if (outcreateCustomerDepSummary != null && outcreateCustomerDepSummary.Count() > 0)
            {
                _currentInputSys.CustomerDepSummary = outcreateCustomerDepSummary;
                // _currentInputSys.CustomerSummary = outsearchTjlCustomerSummaryDtos;
                var curdepartsum = _currentInputSys.CustomerDepSummary
                     .FirstOrDefault(o => o.DepartmentBMId == DepartmentId);
                memoEditKeShiXiaoJie.Text = curdepartsum?.CharacterSummary;
                memoEditZhenDuan.Text = curdepartsum?.DagnosisSummary;
                if (!string.IsNullOrEmpty(curdepartsum?.PrivacyCharacterSummary))
                {

                    layPSum.Visibility = LayoutVisibility.Always;
                    txtpsum.Text = curdepartsum?.PrivacyCharacterSummary;
                }
                else
                {
                    layPSum.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                XtraMessageBox.Show("科室组合没有检查完，保存数据成功，但未生成小结!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            searchHuanZheXinXi.Text = "";
            searchHuanZheXinXi.Focus();
        }
        /// <summary>
        /// 修改当前选择科室小结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXiuGaiXiaoJie_Click(object sender, EventArgs e)
        {
            btnXiuGaiXiaoJie.Enabled = false;
            XiuGaiKeShiXiaoJie();
            btnXiuGaiXiaoJie.Enabled = true;
        }
        private void XiuGaiKeShiXiaoJie()
        {
            //获取当前选择科室id
            var DepartmentId = Guid.Empty;
            if (tvJianChaXiangMu.SelectedNode == null)
            {
                alertControl.Show(this, "系统提示", "还没有选择科室呀,请先选择科室再修改小结!");
                //ShowMessageSucceed("请选择科室或项目!");
                return;
            }
            if (tvJianChaXiangMu.SelectedNode.Parent == null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
            }
            else if (tvJianChaXiangMu.SelectedNode.Parent != null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
            }
            try
            {
                if (DepartmentId == null)
                {
                    alertControl.Show(this, "温馨提示", "请选择科室名称后再进行清除操作哦!");
                    // XtraMessageBox.Show("没有选择科室，请选择科室后点击!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var update = new UpdateClass();
                update.CustomerRegId = _currentInputSys.Id;
                update.DepartmentBM = DepartmentId;
                //update.CharacterSummary = memoEditKeShiXiaoJie.Text;
                update.CharacterSummary = memoEditZhenDuan.Text;
                update.DagnosisSummary = memoEditZhenDuan.Text;
                _doctorStation.UpdateCustomerDepSummary(update);


                var DepSummary = _currentInputSys.CustomerDepSummary
                    .Where(o => o.DepartmentBMId == DepartmentId).FirstOrDefault();
                if (DepSummary != null)
                {
                    DepSummary.CharacterSummary = memoEditKeShiXiaoJie.Text;
                    DepSummary.DagnosisSummary = memoEditZhenDuan.Text;
                }
                alertControl.Show(this, "温馨提示", "结论已经修改成功啦!");
                //XtraMessageBox.Show("修改完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaoCunShuJu_Click(object sender, EventArgs e)
        {
            btnBaoCunShuJu.Enabled = false;
            UpdateCurrentItem(true);
            if (customGridLookUpEditZhenDuan.Properties.DataSource != null)
            {
                customGridLookUpEditZhenDuan.Properties.DataSource = _itemDictionarySys.Where(o => !string.IsNullOrWhiteSpace(o.Period));
            }
            btnBaoCunShuJu.Enabled = true;
            searchHuanZheXinXi.Text = "";
            searchHuanZheXinXi.Focus();
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQingChu_Click(object sender, EventArgs e)
        {
            btnQingChu.Enabled = false;
            QingChuShuJu();
            btnQingChu.Enabled = true;
        }
        public void QingChuShuJu()
        {
            if (_currentInputSys == null)
            {
                alertControl.Show(this, "温馨提示", "没有找到这个患者呀!");
                //XtraMessageBox.Show("没有获取到患者信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tvJianChaXiangMu.SelectedNode == null)
            {
                alertControl.Show(this, "温馨提示", "请先选择科室再进行清除操作呀!");
                //ShowMessageSucceed("请选择科室或项目!");
                return;
            }
            var dr = XtraMessageBox.Show("是否清除本科室检查内容及科室小结?", "确认", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
                return;
            //获取当前选择科室id
            var DepartmentId = Guid.Empty;
            string departname = "";
            if (tvJianChaXiangMu.SelectedNode.Parent == null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                departname = tvJianChaXiangMu.SelectedNode.Text;
            }
            else if (tvJianChaXiangMu.SelectedNode.Parent != null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                departname = tvJianChaXiangMu.SelectedNode.Parent.Text;
            }
            var Query = new QueryClassTwo();
            Query.CustomerId = _currentInputSys.Id;
            Query.DepartmentBMList = new List<Guid> { DepartmentId };
            Query.CheckState = (int)ProjectIState.Not;
            Query.ProcessState = (int)ProjectIState.Not;
            try
            {
                _doctorStation.DeleteCustomerInspectInformation(Query);

                #region 重新获取图片
                var Querytp = new QueryClass();
                Querytp.CustomerRegId = _currentInputSys.Id;
                _customerItemPicAllSys = new List<CustomerItemPicDto>();
                _customerItemPicAllSys = _doctorStation.GetCustomerItemPicDtos(Querytp);
                #endregion

            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }

            foreach (var itemGroup in _currentCustomerItemGroupSys.Where(o => o.DepartmentId == DepartmentId)
                .ToList())
            {
                //组合状态
                itemGroup.CheckState = Query.CheckState;

                //开单医生
                itemGroup.BillingEmployeeBMId = null;

                //检查人
                itemGroup.InspectEmployeeBMId = null;

                //审核人
                itemGroup.CheckEmployeeBMId = null;

                //组合小结
                itemGroup.ItemGroupSum = string.Empty;

                //组合小结
                itemGroup.ItemGroupDiagnosis = string.Empty;
                foreach (var item in itemGroup.CustomerRegItem)
                {
                    //状态
                    item.ProcessState = Query.ProcessState;

                    //结果
                    item.ItemResultChar = string.Empty;

                    //项目标示
                    item.Symbol = string.Empty;

                    //阳性状态
                    item.PositiveSate = null;

                    //疾病状态
                    item.IllnessSate = null;

                    //危急值
                    item.CrisisSate = null;

                    //小结
                    item.ItemSum = null;

                    //小结
                    item.ItemDiagnosis = null;
                }
            }

            var depSummary =
                _currentInputSys.CustomerDepSummary.FirstOrDefault(o => o.DepartmentBMId == DepartmentId);
            if (depSummary != null)
            {
                depSummary.CharacterSummary = string.Empty;
                depSummary.DagnosisSummary = string.Empty;
                depSummary.SystemCharacter = string.Empty;
                depSummary.ExamineEmployeeBMId = null;
                memoEditKeShiXiaoJie.Text = string.Empty;
                memoEditZhenDuan.Text = string.Empty;
            }

            gridJianChaXiangMu.DataSource = null;
            //日志
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            createOpLogDto.LogBM = _currentInputSys.CustomerBM;
            createOpLogDto.LogName = _currentInputSys.Customer.Name;
            createOpLogDto.LogText = "医生站清除数据：" + departname;
            createOpLogDto.LogDetail = "";
            createOpLogDto.LogType = (int)LogsTypes.CheckId;
            _commonAppService.SaveOpLog(createOpLogDto);
            BinDingDataTreeS(DepartmentId);
            alertControl.Show(this, "温馨提示", "已经完成清除啦!");
            //XtraMessageBox.Show("清除完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 弃检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemQiJian_ItemClick(object sender, ItemClickEventArgs e)
        {

            var s = MessageBox.Show("确定要放弃吗？", "提示", MessageBoxButtons.OKCancel);
            if (s == DialogResult.OK)
            {
                if (_currentInputSys != null && _currentInputSys.SendToConfirm.HasValue
                    && _currentInputSys.SendToConfirm == (int)SendToConfirm.Yes)
                {
                    ShowMessageBoxInformation("已交表不能放弃!");
                    return;
                }
                var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
                var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
                if (group != null && group.ZuHeId != Guid.Empty)
                {

                    if (!_departmentGuidSys.Contains(group.DepartmentId))
                    {
                        XtraMessageBox.Show("您没有该科室的修改权限！");
                    }
                    if (Name == null)
                    { Name = group.ZuHeId; }
                }
                var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeZhuangTai.FieldName);
                if (CheckState != null && CheckState.ToString() == ((int)ProjectIState.GiveUp).ToString())
                {
                    alertControl.Show(this, "温馨提示", "这个项目已经弃检啦!");
                    //XtraMessageBox.Show("此数据已经弃检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (Name != null)
                {
                    var Query = new UpdateClass();
                    Query.CustomerItemGroupId = Guid.Parse(Name.ToString());
                    Query.CheckState = (int)ProjectIState.GiveUp;
                    Query.ProcessState = (int)ProjectIState.GiveUp;

                    //修改状态返回数据
                    var trGroup = new ATjlCustomerItemGroupDto();
                    try
                    {
                        trGroup = _doctorStation.GiveUpCheckItemGroup(Query);
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                        return;
                    }

                    if (trGroup != null && trGroup.Id != Guid.Empty)
                    {
                        ////更新数据源和列表数据
                        //var DataCustomerItemGroup = new List<ATjlCustomerItemGroupDto>();
                        //DataCustomerItemGroup.Add(trGroup);

                        //获取当前选择科室id
                        var DepartmentId = Guid.Empty;
                        if (tvJianChaXiangMu.SelectedNode.Parent == null)
                        {
                            DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                        }
                        else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                        {
                            DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                        }
                        _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CheckState = Query.CheckState;
                        foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CustomerRegItem)
                            item.ProcessState = Query.CheckState;
                        BinDingDataTreeS(DepartmentId);

                        //BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                    }
                    alertControl.Show(this, "温馨提示", "已经弃检成功啦!");
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = trGroup.ItemGroupName;
                    createOpLogDto.LogName = trGroup.CheckStateString;

                    //createOpLogDto.LogText = "待查项目：" + string.Join(",", Names);

                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    //XtraMessageBox.Show("修改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }

        /// <summary>
        /// 待检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDaiJian_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if (group != null && group.ZuHeId != Guid.Empty)
            {

                if (!_departmentGuidSys.Contains(group.DepartmentId))
                {
                    XtraMessageBox.Show("您没有该科室的修改权限！");
                }
                if (Name == null)
                { Name = group.ZuHeId; }
            }
            var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeZhuangTai.FieldName);
            if (CheckState != null && CheckState.ToString() == ((int)ProjectIState.Stay).ToString())
            {
                alertControl.Show(this, "温馨提示", "这个项目已经是待检啦,不要重复设置啊!");
                //XtraMessageBox.Show("此数据已为待检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CheckState != null &&
               (CheckState.ToString() == ((int)ProjectIState.Complete).ToString() ||
               CheckState.ToString() == ((int)ProjectIState.Part).ToString()))
            {
                alertControl.Show(this, "温馨提示", "这个项目已经已检查，不能设置为待查");
                //XtraMessageBox.Show("此数据已为待检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Name != null)
            {
                var Query = new UpdateClass();
                Query.CustomerItemGroupId = Guid.Parse(Name.ToString());
                Query.CheckState = (int)ProjectIState.Stay;
                Query.ProcessState = (int)ProjectIState.Stay;
                //修改状态返回数据
                var trGroup = new ATjlCustomerItemGroupDto();
                try
                {
                    trGroup = _doctorStation.GiveUpCheckItemGroup(Query);
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                if (trGroup != null && trGroup.Id != Guid.Empty)
                {

                    //更新数据源和列表数据
                    //var DataCustomerItemGroup = new List<ATjlCustomerItemGroupDto>();
                    //DataCustomerItemGroup.Add(trGroup);
                    //获取当前选择科室id
                    var DepartmentId = Guid.Empty;
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }
                    _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CheckState = Query.CheckState;
                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CustomerRegItem)
                        item.ProcessState = Query.CheckState;
                    BinDingDataTreeS(DepartmentId);
                    //BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据id获取项目值
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns></returns>
        public string GetBasicDictionary(Guid id)
        {
            foreach (var itemGroup in _currentCustomerItemGroupSys.Where(o =>
                o.CustomerRegItem.Where(n => n.ItemId == id).Count() > 0))
                foreach (var item in itemGroup.CustomerRegItem)
                    if (item.Id == id)
                        return item.ItemResultChar;
            return string.Empty;
        }

        /// <summary>
        /// 保存体检项目
        /// </summary>
        public bool UpdateCurrentItem(bool Is)
        {
            bool isOK = true;
            AutoLoading(() =>
            {
                if (_currentCustomerItemGroupSys == null)
                {
                    alertControl.Show(this, "系统提示", "这个患者没有项目哦!");
                    //ShowMessageSucceed("该患者没有项目数据!");
                    isOK = false;
                    return  ;
                }
                if (gridZuHeXiangMu.RowCount == 0)
                {
                    alertControl.Show(this, "系统提示", "该科室没有检查数据！");
                    //ShowMessageSucceed("该患者没有项目数据!");
                    isOK = false;
                    return  ;

                }
                //获取列表数据
                // var data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate).ToList();
                var ss = gridZuHeXiangMu.GetRowCellValue(0, conDepartmentId.FieldName);
                var DepartId = Guid.Parse(ss.ToString());
                var data = new List<ATjlCustomerItemGroupDto>();
                if (tvJianChaXiangMu.SelectedNode.Level == 1)
                {
                    var SelectId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate && o.DepartmentId == DepartId && o.Id == SelectId).ToList();
                }
                else
                {
                    data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate && o.DepartmentId == DepartId).ToList();
                }
                //组合信息
                var CustomerItemGroup = new List<UpdateCustomerItemGroupDto>();
                if (data == null || data.Count() == 0)
                {
                    if (Is)
                    {
                        alertControl.Show(this, "系统提示", "您没有修改项目结果所以不会保存啦!");
                        //ShowMessageSucceed("没有需要保存的数据!");

                    }
                    isOK = false;
                    return   ;
                    //生成小结
                    //CreateConclusion();
                }
                //var lockUser = _doctorStation.JudgeLocking(new QueryClass() { CustomerRegId = _currentInputSys.Id });
                //if (lockUser.IsLock == true)
                //{
                //    alertControl.Show(this, "系统提示", "该患者已经被：" + lockUser.LockUser + "总检锁定啦不能再修改数据!");
                //    //ShowMessageSucceed("患者已锁定不可更改!");
                //    return;
                //}

                //组合
                foreach (var itemGroup in data)
                {
                    if (itemGroup.CheckState == (int)ProjectIState.GiveUp)
                        continue;

                    var GroupDto = new UpdateCustomerItemGroupDto();
                    GroupDto.Id = itemGroup.Id; //id
                    if (itemGroup.CustomerRegItem.Where(o => (o.ItemResultChar == "" || o.ItemResultChar == null)
                    && o.ProcessState != (int)ProjectIState.GiveUp).Count() == 0)
                    {
                        GroupDto.CheckState = (int)ProjectIState.Complete; //状态
                    }
                    else if (itemGroup.CustomerRegItem.Where(o => (o.ItemResultChar != "" || o.ItemResultChar == null)
                      && o.ProcessState != (int)ProjectIState.GiveUp).Count() == 0)
                    {
                        GroupDto.CheckState = (int)ProjectIState.Not; //状态
                    }
                    //else if (itemGroup.CustomerRegItem.All(o => o.ProcessState != (int)ProjectIState.GiveUp))
                    //{
                    //    GroupDto.CheckState = (int)ProjectIState.GiveUp; //状态
                    //}
                    else
                    {
                        GroupDto.CheckState = (int)ProjectIState.Part; //状态
                    }
                    GroupDto.BillingEmployeeBMId = CurrentUser.Id; //开单医生
                    if (cglueJianChaYiSheng.EditValue != null) //检查人
                        GroupDto.InspectEmployeeBMId = long.Parse(cglueJianChaYiSheng.EditValue.ToString());
                    if (cglueShenHeYiSheng.EditValue != null) //审核人
                        GroupDto.CheckEmployeeBMId = long.Parse(cglueShenHeYiSheng.EditValue.ToString());
                    GroupDto.FirstDateTime = DateTime.Now;
                    GroupDto.CustomerRegItem = new List<UpdateCustomerRegItemDto>();

                    //项目
                    foreach (var item in itemGroup.CustomerRegItem)
                    {
                        var ItemDto = new UpdateCustomerRegItemDto();
                        ItemDto.Id = item.Id; //id
                        if (!string.IsNullOrWhiteSpace(item.ItemResultChar)
                        && item.ProcessState != (int)ProjectIState.GiveUp)
                        {
                            ItemDto.ProcessState = (int)ProjectIState.Complete; //状态
                        }
                        else if (item.ProcessState == (int)ProjectIState.GiveUp)
                        { ItemDto.ProcessState = (int)ProjectIState.GiveUp; //状态
                        }
                        else
                        {
                            ItemDto.ProcessState = (int)ProjectIState.Not; //状态
                        }
                        if (cglueJianChaYiSheng.EditValue != null) //检查人
                            ItemDto.InspectEmployeeBMId = long.Parse(cglueJianChaYiSheng.EditValue.ToString());
                        if (cglueShenHeYiSheng.EditValue != null) //审核人
                            ItemDto.CheckEmployeeBMId = long.Parse(cglueShenHeYiSheng.EditValue.ToString());
                        ItemDto.ItemResultChar = item.ItemResultChar; //结果                       
                        ItemDto.ItemDiagnosis = item.ItemDiagnosis;
                        ItemDto.ItemId = item.ItemId;
                        ItemDto.Stand = item.Stand;
                        ItemDto.Unit = item.Unit;
                        GroupDto.CustomerRegItem.Add(ItemDto);
                        #region 临界值提醒
                        var itemts = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p => p.Id == ItemDto.ItemId);
                        if (itemts != null && itemts.ISLJ.HasValue && itemts.ISLJ == 1 &&
                              decimal.TryParse(ItemDto.ItemResultChar, out decimal ValueNuber) &&
                             (ValueNuber < itemts.MinValue || ValueNuber > itemts.MaxValue))
                        {
                            MessageBox.Show(ItemDto.ItemName + "的结果超出项目设置的临界范围：" +
                                itemts.MinValue + "~" + itemts.MaxValue + "请确定结果是否正确！");
                            isOK = false;
                            return ;
                        }
                        #endregion


                    }
                    CustomerItemGroup.Add(GroupDto);
                }


                if (CustomerItemGroup != null && CustomerItemGroup.Count() > 0)
                {
                    _doctorStation.UpdateInspectionProject(CustomerItemGroup);
                    _currentCustomerItemGroupSys = new List<ATjlCustomerItemGroupDto>();
                    var Query = new QueryClass();
                    Query.CustomerRegId = _currentInputSys.Id;
                    _currentCustomerItemGroupSys = _doctorStation.GetCustomerItemGroup(Query);
                    //生成小结
                    CreateConclusion();
                    _currentCustomerItemGroupSys = _doctorStation.GetCustomerItemGroup(Query);
                    //获取当前选择科室id
                    var DepartmentId = Guid.Empty;
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }
                    //绑定检查项目树
                    BinDingDataTreeS(DepartmentId);
                    if (Is)
                        alertControl.Show(this, "系统提示", "结果已经保存成功啦!");
                }
                else
                {
                    if (Is)
                        alertControl.Show(this, "系统提示", "没有需要保存的数据哦!");
                }
            }, Variables.LoadingSaveing);
            //if (splitContainerControl1.Collapsed) BinDingSurplusPatientGrid();
            if (Is)
            {
                searchHuanZheXinXi.Focus();
            }
            return isOK;

        }
        /// <summary>
        /// 生成项目组合小结
        /// </summary>
        public void CreateConclusion()
        {
            //是否显示正常值
            string iszcz = "";
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 4)?.Remarks;
            if (!string.IsNullOrEmpty(isshow))
            {
                iszcz = isshow;
            }
            //是否显示结果
            string isXMJG = "";
            var isShowxmjg = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 9)?.Remarks;
            if (!string.IsNullOrEmpty(isShowxmjg))
            {
                isXMJG = isShowxmjg;
            }
            //获取当前选择科室id
            var DepartmentId = Guid.Empty;
            if (tvJianChaXiangMu.SelectedNode == null)
            {
                alertControl.Show(this, "系统提示", "还没有选择科室哦,请先选择科室再生成小结!");
                //ShowMessageSucceed("请选择科室或项目!");
                return;
            }
            if (tvJianChaXiangMu.SelectedNode.Parent == null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
            }
            else if (tvJianChaXiangMu.SelectedNode.Parent != null)
            {
                DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
            }
            var CurrentDepartment = CurrentUser.TbmDepartments.FirstOrDefault(o => o.Id == DepartmentId);
            //获取基本信息 限制条件
            int Age = 0;
            if (_currentInputSys.Customer.Age == null)
            {
                if (_currentInputSys.Customer.Birthday.HasValue)
                {
                    Age = DateTime.Parse(_currentInputSys.LoginDate.ToString()).Year - DateTime.Parse(_currentInputSys.Customer.Birthday.ToString()).Year;
                }
            }
            else
            {
                Age = (int)_currentInputSys.Customer.Age;
            }
            string sex = _currentInputSys.Customer.Sex.ToString();
            string SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
            var KeShiZhenDuan = string.Empty;
            //循环科室组合
            foreach (var ItemGroup in _currentCustomerItemGroupSys.Where(o => o.DepartmentId == CurrentDepartment.Id))
            {
                //判断项目组合是不是已查或部分已查
                if (ItemGroup.CheckState == (int)ProjectIState.Complete || ItemGroup.CheckState == (int)ProjectIState.Part)
                {
                    //循环当前分组项目
                    foreach (var Item in ItemGroup.CustomerRegItem.OrderBy(o => o.ItemOrder))
                    {
                        //判断项目是否异常
                        var IsItemAbnormal = false;
                        //判断院内院外(暂时去掉)
                        //if (Item.StandFrom == (int)ExamPlace.Hospital || Item.StandFrom == null)
                        if (true)
                        {
                            //判断是不是已经完成检查
                            if (Item.ProcessState == (int)ProjectIState.Complete)
                            {
                                //设置了大小区间参考值
                                var NumberItemStandardlist = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified)
                                && o.CheckType == (int)ResultJudgementState.BigOrSmall).OrderBy(p => p.OrderNum).ToList();
                               
                                //数值型判断
                                if ((NumberItemStandardlist.Count > 0 && decimal.TryParse(Item.ItemResultChar, out decimal charNuber)) || (Item.ItemTypeBM == (int)ItemType.Number || Item.ItemTypeBM == (int)ItemType.Calculation) && Item.ItemResultChar != null && Item.ItemResultChar != "")
                                {

                                    if (!decimal.TryParse(Item.ItemResultChar, out decimal CharNum))
                                    {
                                        MessageBox.Show(Item.ItemName + "项目类型设置的为数值型，但是结果" + Item.ItemResultChar + "为非数值，请检查项目设置，修改后请，更新缓存再登记才能生效！");
                                        continue;
                                    }
                                    var rtItemResultChar = Convert.ToDecimal(Item.ItemResultChar);//int.Parse(Item.ItemResultChar);
                                    #region 根据体检类别过滤参考值
                                    var ItemStandardDtolist = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.MinValue <= rtItemResultChar && o.MaxValue >= rtItemResultChar).OrderBy(p => p.OrderNum).ToList();
                                    var ItemStandardDto = ItemStandardDtolist.FirstOrDefault();
                                    //体检类别过滤
                                    if (ItemStandardDtolist.Count > 1)
                                    {
                                        var stander = ItemStandardDtolist.Where(p => p.PhysicalType == _currentInputSys.PhysicalType).FirstOrDefault();
                                        if (stander != null)
                                        {
                                            ItemStandardDto = stander;
                                        }
                                        else
                                        {
                                            var standerEmp = ItemStandardDtolist.Where(p => p.PhysicalType == null).FirstOrDefault();
                                            if (standerEmp != null)
                                            {
                                                ItemStandardDto = standerEmp;
                                            }
                                        }
                                    }
                                    #endregion
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        //判断是否异常
                                        if (ItemStandardDto.PositiveSate == (int)PositiveSate.Abnormal)
                                        {
                                            if (ItemStandardDto.IsNormal == (int)Symbol.High && isXMJG != "N")
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↑", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↑（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.Superhigh && isXMJG != "N")
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↑↑", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↑↑（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.Low && isXMJG != "N")
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↓", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↓（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                            else if (ItemStandardDto.IsNormal == (int)Symbol.UltraLow && isXMJG != "N")
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↓↓", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}({1} {2})↓↓（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                                }
                                                Item.PositiveSate = (int)PositiveSate.Abnormal;
                                                IsItemAbnormal = true;
                                            }
                                            //else if (ItemStandardDto.IsNormal == 4)
                                            //{
                                            //    Item.ItemSum = string.Format(@"{0}({1}↓↓{2})（正常值：{3}）", ItemStandardDto.Summ, Item.ItemResultChar, Item.Unit, Item.Stand);
                                            //    IsItemAbnormal = true;
                                            //}
                                            else
                                            {
                                                if (iszcz == "N")
                                                {
                                                    Item.ItemSum = string.Format(@"{0}", ItemStandardDto.Summ + Item.ItemResultChar);
                                                }
                                                else
                                                {
                                                    Item.ItemSum = string.Format(@"{0}（正常值：{1}）", ItemStandardDto.Summ + Item.ItemResultChar + Item.Unit, Item.Stand);
                                                }
                                                IsItemAbnormal = true;
                                            }
                                        }
                                        else
                                        {
                                            //if (iszcz == "N")
                                            //{
                                            Item.ItemSum = ItemStandardDto.Summ;
                                            //}
                                            //else
                                            //{
                                            //    Item.ItemSum = ItemStandardDto.Summ + "（正常值：" + Item.Stand + "）";
                                            //}
                                        }

                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        //取消判断
                                        //if (Item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.Superhigh) || Item.Symbol == SymbolHelper.SymbolFormatter((int)Symbol.UltraLow))
                                        //{
                                        //    Item.CrisisSate = (int)CrisisSate.Abnormal;
                                        //}
                                        //else
                                        //{
                                        //    Item.CrisisSate = (int)CrisisSate.Normal;
                                        //}
                                        Item.CrisisSate = (int)CrisisSate.Normal;
                                    }
                                    else
                                    {
                                        //Item.ItemSum = string.Format(@"{0}（正常值：{1}）", Item.ItemResultChar, Item.Stand);
                                        //Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                        //IsItemAbnormal = true;
                                        Item.ItemSum = "";
                                        Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);

                                    }
                                }
                                //说明形判断
                                else if (Item.ItemTypeBM == (int)ItemType.Explain)
                                {
                                    // var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                    #region 根据体检类别过滤参考值
                                    var ItemStandardDtolist = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).OrderBy(p => p.OrderNum).ToList();
                                    var ItemStandardDto = ItemStandardDtolist.FirstOrDefault();
                                    //体检类别过滤
                                    if (ItemStandardDtolist.Count > 1)
                                    {
                                        var stander = ItemStandardDtolist.Where(p => p.PhysicalType == _currentInputSys.PhysicalType).FirstOrDefault();
                                        if (stander != null)
                                        {
                                            ItemStandardDto = stander;
                                        }
                                        else
                                        {
                                            var standerEmp = ItemStandardDtolist.Where(p => p.PhysicalType == null).FirstOrDefault();
                                            if (standerEmp != null)
                                            {
                                                ItemStandardDto = standerEmp;
                                            }
                                        }
                                    }
                                    #endregion
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        Item.ItemSum = ItemStandardDto.Summ;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        if (ItemStandardDto.IsNormal != 4)
                                        {
                                            IsItemAbnormal = true;
                                        }
                                    }
                                    else
                                    {
                                        var ItemStandardDtoNull = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && Item.ItemResultChar.Contains(o.Summ) && o.CheckType == (int)ResultJudgementState.ContainText).OrderBy(p => p.OrderNum).FirstOrDefault();
                                        if (ItemStandardDtoNull != null)
                                        {
                                            //重度等级
                                            Item.IllnessLevel = ItemStandardDtoNull.IllnessLevel;
                                            Item.ItemSum = Item.ItemResultChar;
                                            Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDtoNull.IsNormal);
                                            if (ItemStandardDtoNull.IsNormal != 4)
                                            {
                                                IsItemAbnormal = true;
                                            }
                                        }
                                        else
                                        {
                                            var ckz = _itemStandardSys.Where(o => o.ItemId == Item.ItemId).OrderBy(p => p.OrderNum).FirstOrDefault();
                                            if (ckz != null)
                                            {   //重度等级
                                                Item.IllnessLevel = ckz.IllnessLevel;
                                                Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                                Item.ItemSum = Item.ItemResultChar;
                                                IsItemAbnormal = true;
                                            }
                                            else
                                            {
                                                Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Normal);
                                                Item.ItemSum = "";
                                            }

                                        }

                                    }
                                }
                                //阴阳形判断
                                else if (Item.ItemTypeBM == (int)ItemType.YinYang)
                                {
                                    //var ItemStandardDto = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).FirstOrDefault();
                                    #region 根据体检类别过滤参考值
                                    var ItemStandardDtolist = _itemStandardSys.Where(o => o.ItemId == Item.ItemId && o.MinAge <= Age && o.MaxAge >= Age && (o.Sex == sex || o.Sex == SexGenderNotSpecified) && o.Summ == Item.ItemResultChar && o.CheckType == (int)ResultJudgementState.Text).ToList();
                                    var ItemStandardDto = ItemStandardDtolist.FirstOrDefault();
                                    //体检类别过滤
                                    if (ItemStandardDtolist.Count > 1)
                                    {
                                        var stander = ItemStandardDtolist.Where(p => p.PhysicalType == _currentInputSys.PhysicalType).FirstOrDefault();
                                        if (stander != null)
                                        {
                                            ItemStandardDto = stander;
                                        }
                                        else
                                        {
                                            var standerEmp = ItemStandardDtolist.Where(p => p.PhysicalType == null).FirstOrDefault();
                                            if (standerEmp != null)
                                            {
                                                ItemStandardDto = standerEmp;
                                            }
                                        }
                                    }
                                    #endregion
                                    if (ItemStandardDto != null)
                                    {
                                        //重度等级
                                        Item.IllnessLevel = ItemStandardDto.IllnessLevel;
                                        Item.Symbol = ItemStandardDto.Summ;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(ItemStandardDto.IsNormal);
                                        if (ItemStandardDto.IsNormal != 4)
                                        {
                                            IsItemAbnormal = true;
                                        }
                                    }
                                    else
                                    {
                                        Item.ItemSum = Item.ItemResultChar;
                                        Item.Symbol = SymbolHelper.SymbolFormatter(Symbol.Abnormal);
                                        IsItemAbnormal = true;
                                    }
                                }
                            }
                        }
                        if (colXMZD.Visible == false)
                        {
                            Item.ItemDiagnosis = string.Empty;
                        }
                        if (IsItemAbnormal)
                        {
                            ////判断字典中是否搜索到
                            //var DictCon = true;
                            //foreach (var itemDict in _itemDictionarySys.Where(o => o.iteminfoBMId == Item.ItemId))
                            //{
                            //    if (Item.ItemResultChar.Contains(itemDict.Word))
                            //    {
                            //        if (string.IsNullOrWhiteSpace(Item.ItemDiagnosis))
                            //        {
                            //            Item.ItemDiagnosis = itemDict.Period;
                            //        }
                            //        else
                            //        {
                            //            Item.ItemDiagnosis += "," + itemDict.Period;
                            //        }
                            //        DictCon = false;
                            //    }
                            //}
                            //if (DictCon)
                            //{
                            //Item.ItemDiagnosis = Item.ItemName + Item.ItemSum.Trim();
                            //Item.ItemDiagnosis =  Item.ItemSum.Trim();
                            // }
                            if (memoEditZiDianZD.Visible == true)
                            {

                                if (string.IsNullOrEmpty(Item.ItemDiagnosis))
                                {
                                    Item.ItemDiagnosis = Item.ItemSum.Trim();

                                }
                            }
                            else
                            {
                                Item.ItemDiagnosis = Item.ItemSum.Trim();

                            }


                        }
                        else
                        {
                            // 正常都进入小结的科室
                            var strjcDepartment = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == "DepatSumSet" && o.Value == 2)?.Remarks;
                            if (strjcDepartment != null)
                            {
                                var departlist = DefinedCacheHelper.GetDepartments().Where(p => strjcDepartment.Contains(p.Name)).Select(p => p.Id).ToList();
                                if (strjcDepartment != null && departlist.Contains(Item.DepartmentId))
                                {
                                    if (!string.IsNullOrEmpty(Item.ItemSum))
                                    {
                                        Item.ItemDiagnosis = Item.ItemSum.Trim();
                                    }
                                }
                            }

                        }
                    }
                }
            }
            var currgrouplist = _currentCustomerItemGroupSys.Where(o => o.DepartmentId == CurrentDepartment.Id).ToList();
            _doctorStation.UpdateSectionSummary(currgrouplist);//保存项目组合和项目小结

        }
        /// <summary>
        /// 修改数据后（grid修改后需要重新绑定）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTiJianXiangMu_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

            if (_isTiZhiJiSuan)
                return;

            if (gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName) == null)
                return;

            var ZuHeId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName).ToString());
            var itemProID = Guid.Empty;
            if (_currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault() != null)
            {
                _valueUpdate = true;
                _currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault().IsUpdate = true;
                var xiangMuId = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
                foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault()
                    .CustomerRegItem)
                    if (item.Id == Guid.Parse(xiangMuId.ToString()))
                    {

                        var ZhiLeiBie = item.ItemBM.moneyType;
                        if (ZhiLeiBie == 1 && !string.IsNullOrWhiteSpace(e.Value?.ToString()))
                        {
                            if (!ZhuanHuanNumberic(e.Value.ToString()) && e.Column.Name == "colXMJieGuo")
                            {
                                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                                gridZuHeXiangMu.Focus();
                                gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                                gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                                gridZuHeXiangMu.ShowEditor();
                                gridZuHeXiangMu.SetRowCellValue(rowHandle, gridZuHeXiangMu.Columns[colXMJieGuo.FieldName], "");
                                alertControl.Show(this, "温馨提示", $"您输入的结果类型和系统设置不符合，应该输入{ItemTypeHelper.ItemTypeFormatter(ZhiLeiBie)}类型哦!");
                                //XtraMessageBox.Show("输入结果类型不正确，该结果应该为" + ItemTypeHelper.ItemTypeFormatter(ZhiLeiBie) + "!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                            itemProID = item.ItemBM.Id;
                        }

                        //item.ItemResultChar = e.Value?.ToString().Trim();
                        item.ItemResultChar = gridZuHeXiangMu.GetFocusedRowCellValue(colXMJieGuo.FieldName).ToString();
                        if (colXMZD.Visible == true)//如果是诊断模式
                        {
                            item.ItemDiagnosis = gridZuHeXiangMu.GetFocusedRowCellValue(colXMZD.FieldName)?.ToString();
                        }
                        break;
                    }
            }
            #region bmi计算
            //var xiangMuBianMa = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuBianMa.FieldName)?.ToString();
            //if (xiangMuBianMa == "0500001" && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            //{
            //    try
            //    {
            //        _shenGao = double.Parse(e.Value.ToString().Trim());
            //        if (_tiZhong != 0)
            //        {
            //            //判断体脂是否显示在grid中
            //            var IsGridTiZhi = true;
            //            var TiZhi = _tiZhong / (_shenGao * _shenGao) * 10000;
            //            for (var i = 0; i < gridJianChaXiangMu.Views[0].RowCount - 1; i++)
            //            {
            //                var row = gridJianChaXiangMu.Views[0].GetRow(i) as GridZhanShiLei;
            //                if (row != null && row.ItemBM == "20181112101709693")
            //                {
            //                    _isTiZhiJiSuan = true;
            //                    gridZuHeXiangMu.SetRowCellValue(i, gridZuHeXiangMu.Columns[colXMJieGuo.FieldName],
            //                        TiZhi.ToString("0.00"));
            //                    _isTiZhiJiSuan = false;
            //                    IsGridTiZhi = false;
            //                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == row.ZuHeId).FirstOrDefault().CustomerRegItem)
            //                    {
            //                        if (item.Id == row.XiangMuId)
            //                        {
            //                            item.ItemResultChar = TiZhi.ToString("0.00");
            //                            break;
            //                        }
            //                    }
            //                    break;
            //                }
            //            }
            //            if (IsGridTiZhi)
            //            {
            //                foreach (var itemGroup in _currentCustomerItemGroupSys)
            //                {
            //                    foreach (var item in itemGroup.CustomerRegItem)
            //                    {
            //                        if (item.ItemBM.ItemBM == "20181112101709693")
            //                        {
            //                            item.ItemResultChar = TiZhi.ToString("0.00");
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        alertControl.Show(this, "温馨提示", $"您输入的结果类型和系统设置不符合，“身高”应该输入数字类型哦!");
            //        //XtraMessageBox.Show("身高必须是数字类型结果!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
            //        gridZuHeXiangMu.Focus();
            //        gridZuHeXiangMu.FocusedRowHandle = rowHandle;
            //        gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
            //        gridZuHeXiangMu.ShowEditor();
            //    }
            //}
            //else if (xiangMuBianMa == "0500002" && !string.IsNullOrWhiteSpace(e.Value.ToString().Trim()))
            //{
            //    try
            //    {
            //        _tiZhong = double.Parse(e.Value.ToString());
            //        if (_shenGao != 0)
            //        {
            //            //判断体脂是否显示在grid中
            //            var IsGridTiZhi = true;
            //            var TiZhi = _tiZhong / (_shenGao * _shenGao) * 10000;
            //            for (var i = 0; i < gridJianChaXiangMu.Views[0].RowCount - 1; i++)
            //            {
            //                var row = gridJianChaXiangMu.Views[0].GetRow(i) as GridZhanShiLei;

            //                if (row != null && row.ItemBM == "20181112101709693")
            //                {

            //                    _isTiZhiJiSuan = true;
            //                    gridZuHeXiangMu.SetRowCellValue(i, gridZuHeXiangMu.Columns[colXMJieGuo.FieldName],
            //                        TiZhi.ToString("0.00"));
            //                    _isTiZhiJiSuan = false;
            //                    IsGridTiZhi = false;
            //                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == row.ZuHeId)
            //                       .FirstOrDefault().CustomerRegItem)
            //                        if (item.Id == row.XiangMuId)
            //                        {
            //                            item.ItemResultChar = TiZhi.ToString("0.00");
            //                            break;
            //                        }
            //                    break;
            //                }

            //            }
            //            if (IsGridTiZhi)
            //            {
            //                foreach (var itemGroup in _currentCustomerItemGroupSys)
            //                {
            //                    foreach (var item in itemGroup.CustomerRegItem)
            //                    {
            //                        if (item.ItemBM.ItemBM == "20181112101709693")
            //                        {
            //                            item.ItemResultChar = TiZhi.ToString("0.00");
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        alertControl.Show(this, "温馨提示", $"您输入的结果类型和系统设置不符合，“体重”应该输入数字类型哦!");
            //        //XtraMessageBox.Show("体重必须是数字类型结果!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
            //        gridZuHeXiangMu.Focus();
            //        gridZuHeXiangMu.FocusedRowHandle = rowHandle;
            //        gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
            //        gridZuHeXiangMu.ShowEditor();
            //    }
            //}
            #endregion
            //计算型处理
            //var xiangMuID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName)?.ToString();
            ItemComputation(itemProID);

        }
        /// <summary>
        /// 计算型项目处理
        /// </summary>
        private void ItemComputation(Guid ITemid)
        {

            if (_itemProcExpressDtos.Count() == 0)
            {
                return;
            }
            var itemProcExpress = _itemProcExpressDtos.Where(o => o.ItemInfoReRelations.Any(n => n.Id == ITemid)).ToList();
            for (int num = 0; num < itemProcExpress.Count(); num++)
            {
                string Formula = itemProcExpress[num].FormulaValue;
                var items = itemProcExpress[num].ItemInfoReRelations;
                int itemcout = items.Count;
                int itemNum = 0;
                if (string.IsNullOrWhiteSpace(Formula))
                {
                    continue;
                }
                for (var i = 0; i < gridJianChaXiangMu.Views[0].RowCount - 1; i++)
                {

                    var row = gridJianChaXiangMu.Views[0].GetRow(i) as GridZhanShiLei;
                    if (row != null)
                    {
                        var itemjg = items.Where(o => o.Id == row.XiangMuBMId).ToList();
                        if (itemjg.Count != 0)
                        {
                            var xiangMuBianMa = gridZuHeXiangMu.GetRowCellValue(i, colXMJieGuo.FieldName)?.ToString();
                            decimal jieguo = 0;
                            if (decimal.TryParse(xiangMuBianMa, out jieguo))
                            {
                                itemNum = itemNum + 1;
                                Formula = Formula.Replace("[" + row.XiangMuBMId.ToString() + "]", xiangMuBianMa);

                            }
                        }
                    }
                }
                //公式关联项目都有结果
                if (itemNum == itemcout)
                {
                    string jisuanjiguo = "";
                    if (Formula.Contains("MAX"))
                    {
                        string strv = Formula.Replace("MAX", "").Replace("(", "").Replace(")", "");
                        jisuanjiguo = CommonHelper.GetMaxNumByString(strv).ToString();
                        return;
                    }
                    if (Formula.Contains("MIN"))
                    {
                        string strv = Formula.Replace("MIN", "").Replace("(", "").Replace(")", "");
                        jisuanjiguo = CommonHelper.GetMinNumByString(strv).ToString();
                        return;
                    }
                    try
                    { Expression exp = new Expression(Formula);
                        exp.ConvertExpression();
                        jisuanjiguo = CommonHelper.decimalLocation(Convert.ToDouble(exp.Calc()), 2);
                        for (var i = 0; i < gridJianChaXiangMu.Views[0].RowCount - 1; i++)
                        {
                            var row = gridJianChaXiangMu.Views[0].GetRow(i) as GridZhanShiLei;

                            if (row != null && row.XiangMuBMId == itemProcExpress[num].ItemId)
                            {

                                _isTiZhiJiSuan = true;
                                gridZuHeXiangMu.SetRowCellValue(i, gridZuHeXiangMu.Columns[colXMJieGuo.FieldName],
                                    jisuanjiguo);
                                _isTiZhiJiSuan = false;
                                foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == row.ZuHeId)
                                   .FirstOrDefault().CustomerRegItem)
                                    if (item.Id == row.XiangMuId)
                                    {
                                        item.ItemResultChar = jisuanjiguo;
                                        break;
                                    }
                                break;
                            }

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 多行文本框tab到下一个结果框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemMemoEditJieGuo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                var coloumnName = gridZuHeXiangMu.FocusedColumn.Name;
                rowHandle = rowHandle + 1;
                if (rowHandle >= gridZuHeXiangMu.RowCount - 1)
                {
                    // btnShengChengXiaoJie.Focus();
                }
                else
                {
                    // gridZuHeXiangMu.Focus();
                    gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                    // gridZuHeXiangMu.ShowEditor();
                }
            }
            _istabDown = false;
        }

        /// <summary>
        /// 展示此项目体检图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridButtonChaKanTuPian_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (dockPanelTuPianZhanShi.Visibility == DockVisibility.Visible)
            {
                alertControl.Show(this, "温馨提示", "窗体已经打开了,不能重复打开哦!");
                //XtraMessageBox.Show("窗体已打开请勿重复打开!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _customerPicSys = 0;

            var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
            _customerItemPicSys = new List<CustomerItemPicDto>();

            _customerItemPicSys = _customerItemPicAllSys.Where(o => o.ItemBMID == Guid.Parse(XMID.ToString())).ToList();
            if (_customerItemPicSys != null && _customerItemPicSys.Count > 0)
            {
                var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                var result = _pictureController.GetUrl(Pic);
                if (result.RelativePath.Contains(".pdf"))
                {
                    string strnew = System.AppDomain.CurrentDomain.BaseDirectory +
                      "image";
                    if (!Directory.Exists(strnew))
                    {
                        Directory.CreateDirectory(strnew);
                    }
                    strnew = strnew + "\\" + Path.GetFileNameWithoutExtension(result.RelativePath) + ".pdf";
                    if (!File.Exists(strnew))
                    {
                        HttpDldFile.Download(result.RelativePath, strnew);
                    }
                    frmpdfShow frmpdf = new frmpdfShow();
                    frmpdf.strpdfPath = strnew;
                    frmpdf.ShowDialog();
                }
                else
                {
                    dockPanelTuPianZhanShi.Visibility = DockVisibility.Visible;
                    using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                    {

                        pictureEditTuPianZhanShi.Image = Image.FromStream(stream);
                    }
                    GC.Collect();
                    var ItemGroup = Guid.Parse(_customerItemPicSys[_customerPicSys].CustomerItemGroupID.ToString());
                    labelControlTuPianZongShu.Text = _customerItemPicSys.Count().ToString();
                    labelControlTuPianDangQianShu.Text = "1";
                    var ItemGroupData = _currentCustomerItemGroupSys.Where(o => o.Id == ItemGroup).FirstOrDefault();
                    if (ItemGroupData != null)
                        labelControlTuPianZuHeMingCheng.Text = ItemGroupData.ItemGroupName;
                }
            }
            else
            {
                dockPanelTuPianZhanShi.Visibility = DockVisibility.Visible;
                labelControlTuPianZongShu.Text = "0";
                labelControlTuPianDangQianShu.Text = "0";
                ;
            }
        }

        /// <summary>
        /// 下一张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEditTuPianXiaYiGe_Click(object sender, EventArgs e)
        {
            if (_customerItemPicSys != null && _customerItemPicSys.Count != 0)
            {
                if (_customerPicSys == _customerItemPicSys.Count - 1)
                    _customerPicSys = 0;
                else
                    _customerPicSys = _customerPicSys + 1;
                var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                var result = _pictureController.GetUrl(Pic);
                pictureEditTuPianZhanShi.Image = ImageHelper.GetUriImage(new Uri(result.RelativePath));

                var ItemGroup = Guid.Parse(_customerItemPicSys[_customerPicSys].CustomerItemGroupID.ToString());
                var ItemGroupData = _currentCustomerItemGroupSys.Where(o => o.Id == ItemGroup).FirstOrDefault();
                if (ItemGroupData != null)
                    labelControlTuPianZuHeMingCheng.Text = ItemGroupData.ItemGroupName;
                labelControlTuPianDangQianShu.Text = (_customerPicSys + 1).ToString();
            }
        }

        /// <summary>
        /// 上一张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureEditTuPianShangYiGe_Click(object sender, EventArgs e)
        {
            if (_customerItemPicSys != null && _customerItemPicSys.Count != 0)
            {
                if (_customerPicSys == 0)
                    _customerPicSys = _customerItemPicSys.Count - 1;
                else
                    _customerPicSys = _customerPicSys - 1;

                var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                var result = _pictureController.GetUrl(Pic);
                using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                {
                    pictureEditTuPianZhanShi.Image = Image.FromStream(stream);
                }

                var ItemGroup = Guid.Parse(_customerItemPicSys[_customerPicSys].CustomerItemGroupID.ToString());
                var ItemGroupData = _currentCustomerItemGroupSys.Where(o => o.Id == ItemGroup).FirstOrDefault();
                if (ItemGroupData != null)
                    labelControlTuPianZuHeMingCheng.Text = ItemGroupData.ItemGroupName;
                labelControlTuPianDangQianShu.Text = (_customerPicSys + 1).ToString();
            }
        }

        /// <summary>
        /// 勾选所有科室/去掉所有科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditTuPianSuoYou_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditTuPianSuoYou.Checked)
            {
                _customerPicSys = 0;
                _customerItemPicSys = new List<CustomerItemPicDto>();
                _customerItemPicSys = _customerItemPicAllSys;
                if (_customerItemPicSys != null && _customerItemPicSys.Count > 0)
                {
                    var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                    var result = _pictureController.GetUrl(Pic);
                    using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                    {
                        pictureEditTuPianZhanShi.Image = Image.FromStream(stream);
                    }

                    var ItemGroup = Guid.Parse(_customerItemPicSys[_customerPicSys].CustomerItemGroupID.ToString());
                    labelControlTuPianZongShu.Text = _customerItemPicSys.Count().ToString();
                    labelControlTuPianDangQianShu.Text = "1";
                    var ItemGroupData = _currentCustomerItemGroupSys.Where(o => o.Id == ItemGroup).FirstOrDefault();
                    if (ItemGroupData != null)
                        labelControlTuPianZuHeMingCheng.Text = ItemGroupData.ItemGroupName;
                }
            }
            else
            {
                _customerPicSys = 0;
                var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
                _customerItemPicSys = new List<CustomerItemPicDto>();
                _customerItemPicSys =
                    _customerItemPicAllSys.Where(o => o.ItemBMID == Guid.Parse(XMID.ToString())).ToList();
                if (_customerItemPicSys != null && _customerItemPicSys.Count > 0)
                {
                    var Pic = Guid.Parse(_customerItemPicSys[_customerPicSys].PictureBM.ToString());
                    var result = _pictureController.GetUrl(Pic);
                    using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                    {
                        pictureEditTuPianZhanShi.Image = Image.FromStream(stream);
                    }

                    var ItemGroup = Guid.Parse(_customerItemPicSys[_customerPicSys].CustomerItemGroupID.ToString());
                    labelControlTuPianZongShu.Text = _customerItemPicSys.Count().ToString();
                    labelControlTuPianDangQianShu.Text = "1";
                    var ItemGroupData = _currentCustomerItemGroupSys.Where(o => o.Id == ItemGroup).FirstOrDefault();
                    if (ItemGroupData != null)
                        labelControlTuPianZuHeMingCheng.Text = ItemGroupData.ItemGroupName;
                }
            }
        }

        /// <summary>
        /// 选择字典后追加到多行文本中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewZiDian_RowClick(object sender, RowClickEventArgs e)
        {
            var i = gridZuHeXiangMu.FocusedRowHandle;
            if (i == -1)
                return;
            var yZiDianWord = gridZuHeXiangMu.GetFocusedRowCellValue(colXMJieGuo.FieldName)?.ToString();
            var yZiDianzd = gridZuHeXiangMu.GetFocusedRowCellValue(colXMZD.FieldName)?.ToString();
            var TextJieGuo = memoEditZiDianZhanShi.Text;
            var textZD = memoEditZiDianZD.Text;
            if (TextJieGuo != yZiDianWord)
            {
                memoEditZiDianZhanShi.Text = yZiDianWord;
                this.memoEditZiDianZhanShi.Select(this.memoEditZiDianZhanShi.Text.Length, 0);

            }
            //诊断
            if (textZD != yZiDianzd)
            {
                memoEditZiDianZD.Text = yZiDianzd;
                this.memoEditZiDianZD.Select(this.memoEditZiDianZD.Text.Length, 0);

            }
            var ZiDianWord = gridViewZiDian.GetFocusedRowCellValue(colZiDianMingCheng.FieldName)?.ToString();
            var ZiDianZD = gridViewZiDian.GetFocusedRowCellValue(conPeriod.FieldName)?.ToString();
            if (!string.IsNullOrWhiteSpace(ZiDianWord) && !memoEditZiDianZhanShi.Text.Contains(ZiDianWord))
            {
                if (string.IsNullOrWhiteSpace(memoEditZiDianZhanShi.Text))
                {
                    memoEditZiDianZhanShi.Text = ZiDianWord;
                }
                else
                {
                    //List<BTbmItemDictionaryDto> ListDict = gridControlZiDian.DataSource as List<BTbmItemDictionaryDto>;
                    //if (ListDict.FirstOrDefault(o => memoEditZiDianZhanShi.Text.Contains(o.Word)) != null)
                    //{
                    var SexGenderNotSpecified = ((int)Sex.GenderNotSpecified).ToString();
                    var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
                    var itemData = _itemStandardSys.Where(o =>
                                    o.ItemId == Guid.Parse(XMID) && o.PositiveSate == (int)PositiveSate.Normal &&
                                    o.MaxAge >= _currentInputSys.Customer.Age && o.MinAge <= _currentInputSys.Customer.Age &&
                                    (o.Sex == _currentInputSys.Customer.Sex.ToString() || o.Sex == SexGenderNotSpecified))
                                .FirstOrDefault();
                    if (memoEditZiDianZhanShi.Text.Trim() == itemData?.Summ)
                    {
                        memoEditZiDianZhanShi.Text = ZiDianWord;
                    }
                    else
                    {


                        ZiDianWord = "," + ZiDianWord;
                        var start = memoEditZiDianZhanShi.SelectionStart;
                        memoEditZiDianZhanShi.Text = memoEditZiDianZhanShi.Text.Insert(start,
                            ZiDianWord);
                    }
                }
                this.memoEditZiDianZhanShi.Select(this.memoEditZiDianZhanShi.Text.Length, 0);

            }
            //诊断
            if (!string.IsNullOrWhiteSpace(ZiDianZD) && !memoEditZiDianZD.Text.Contains(ZiDianZD))
            {
                if (string.IsNullOrWhiteSpace(memoEditZiDianZD.Text))
                {
                    memoEditZiDianZD.Text = ZiDianZD;
                }
                else
                {

                    ZiDianZD = "," + ZiDianZD;
                    var start = memoEditZiDianZD.SelectionStart;
                    memoEditZiDianZD.Text = memoEditZiDianZD.Text.Insert(start,
                            ZiDianZD);

                }
                this.memoEditZiDianZD.Select(this.memoEditZiDianZD.Text.Length, 0);

            }
        }


        /// <summary>
        /// 弹出字典并绑定字典列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Dictionaries()
        {
            var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
            if (_itemDictionarySys.Where(o => o.iteminfoBMId == Guid.Parse(XMID)).ToList().Count > 0)
            {
                if (XMID != null && !string.IsNullOrWhiteSpace(XMID))
                {
                    gridControlZiDian.DataSource =
                        _itemDictionarySys.Where(o => o.iteminfoBMId == Guid.Parse(XMID)).OrderBy(o => o.ApplySate).ThenBy(o => o.OrderNum).ToList();
                    memoEditZiDianZhanShi.Text =
                        gridZuHeXiangMu.GetFocusedRowCellValue(colXMJieGuo.FieldName)?.ToString();

                    memoEditZiDianZD.Text =
                        gridZuHeXiangMu.GetFocusedRowCellValue(colXMZD.FieldName)?.ToString();
                }

                this.memoEditZiDianZD.Select(this.memoEditZiDianZD.Text.Length, 0);
                this.memoEditZiDianZhanShi.Select(this.memoEditZiDianZhanShi.Text.Length, 0);
            }
            else
            {
                gridControlZiDian.DataSource = null;
                memoEditZiDianZhanShi.Text = string.Empty;
                memoEditZiDianZD.Text = string.Empty;


            }
        }
        /// <summary>
        /// 字典内容更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void memoEditZiDianZhanShi_TextChanged(object sender, EventArgs e)
        {
            var i = gridZuHeXiangMu.FocusedRowHandle;
            if (i == -1)
                return;

            var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
            if (_itemDictionarySys.Where(o => o.iteminfoBMId == Guid.Parse(XMID)).ToList().Count > 0)
            {
                var ZiDianWord = gridZuHeXiangMu.GetFocusedRowCellValue(colXMJieGuo.FieldName)?.ToString();

                var TextJieGuo = memoEditZiDianZhanShi.Text;
                if (TextJieGuo != ZiDianWord)
                    gridZuHeXiangMu.SetRowCellValue(i, gridZuHeXiangMu.Columns[colXMJieGuo.FieldName], TextJieGuo);
            }
        }

        public void YiBuJiaZai()
        {
            //查询配置项
            if (_basicDictionarySys == null || _basicDictionarySys.Count == 0)
            {
                try
                {
                    var QueryDict = new QueryClass();
                    QueryDict.BasicDictionaryType = new List<string>();
                    QueryDict.BasicDictionaryType.Add(BasicDictionaryType.DoctorStationDisplayColumn.ToString());
                    _basicDictionarySys = new List<BasicDictionaryDto>();
                    _basicDictionarySys = _doctorStation.GetDictionaryDto(QueryDict);
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }
            }
            ////查询科室建议
            //if (SummarizeAdviceSys != null && SummarizeAdviceSys.Count() == 0)
            //{
            //    try
            //    {
            //        var QueryDict = new QueryClassTwo();
            //        QueryDict.DepartmentBMList = _departmentGuidSys;
            //        SummarizeAdviceSys = _doctorStation.GetSummarizeAdvice(QueryDict);
            //    }
            //    catch (UserFriendlyException ex)
            //    {
            //        ShowMessageBox(ex);
            //        return;
            //    }
            //}
            //项目字典
            if (_itemDictionarySys == null || _itemDictionarySys.Count == 0)
            {
                try
                {

                    var QueryDict = new QueryClassTwo();
                    QueryDict.DepartmentBMList = _departmentGuidSys;
                    _itemDictionarySys = new List<BTbmItemDictionaryDto>();
                    _itemDictionarySys = _doctorStation.GetItemDictionarylist(QueryDict);

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }
            }
            //获取项目参考值
            if (_itemStandardSys == null || _itemStandardSys.Count() == 0)
            {
                try
                {
                    _itemStandardSys = new List<SearchItemStandardDto>();
                    _itemStandardSys = _doctorStation.GetGenerateSummary();
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
            }
        }
        /// <summary>
        /// 判断是否
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns>, out double result</returns>
        public bool ZhuanHuanNumberic(string message)
        {
            //判断是否为整数字符串
            //是的话则将其转换为数字并将其设为out类型的输出值、返回true, 否则为false
            //result = -1;   //result 定义为out 用来输出值
            try
            {
                //当数字字符串的为是少于4时，以下三种都可以转换，任选一种
                //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()

                var result = Convert.ToDouble(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取lis和pacs数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            btnGetData.Enabled = false;
            HuoQuGetData();
            btnGetData.Enabled = true;
        }
        public void HuoQuGetData()
        {
            if (_currentInputSys != null)
            {
                //获取当前选择科室id
                var DepartmentId = Guid.Empty;
                TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                tdbInterfaceWhere.inactivenum = _currentInputSys.CustomerBM;

                if (tvJianChaXiangMu.SelectedNode == null)
                {
                    alertControl.Show(this, "温馨提示", "还没有选择科室哦,请选择科室后再获取数据!");
                    //ShowMessageSucceed("请选择科室或项目!");
                    return;
                }
                else
                {
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }
                    tdbInterfaceWhere.departmentID = DepartmentId;
                }
                InterfaceBack Back = new InterfaceBack();
                AutoLoading(() =>
                {
                    Back = _doctorStation.ConvertInterface(tdbInterfaceWhere);
                }, "正在读取接口数据并保存!");
                //if (Back.IdList != null && Back.IdList.Count > 0)
                //{
                if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                {

                    alertControl.Show(this, "温馨提示", Back.StrBui.ToString());
                    //XtraMessageBox.Show(Back.StrBui.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}

                #region 去掉生成小结
                //AutoLoading(() =>
                //{
                //    CreateConclusionDto conclusion = new CreateConclusionDto();
                //    conclusion.CustomerBM = _currentInputSys.CustomerBM;
                //    conclusion.Department = new List<Guid>() { DepartmentId };
                //    _doctorStation.CreateConclusion(conclusion);
                //}, "正在生成小结!"); 
                #endregion


                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                //    {
                //        XtraMessageBox.Show(Back.StrBui?.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                if (!string.IsNullOrEmpty(searchHuanZheXinXi.Text.Trim()))
                {
                    LoadCurrentCustomerReg(searchHuanZheXinXi.Text.Trim());
                }
            }
            if (customGridLookUpEditZhenDuan.Properties.DataSource != null)
            {
                customGridLookUpEditZhenDuan.Properties.DataSource = _itemDictionarySys.Where(o => !string.IsNullOrWhiteSpace(o.Period));

            }
        }
        /// <summary>
        /// 按天数查询人员列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRYTianShu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtRYTianShu.Text))
                {
                    dateEditRYKaiShiShiJian.EditValue = null;
                    dateEditRYJieShuShiJian.EditValue = null;
                    if (ZhuanHuanNumberic(txtRYTianShu.Text))
                    {
                        BinDingSurplusPatientGrid();
                    }
                }
            }
        }
        /// <summary>
        /// 获取未体检的人员列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditRyWeiJianRenShu_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditRyWeiJianRenShu.Checked == true)
            {
                checkHas.Checked = false;
                checkEditPart.Checked = false;
            }
        }
        /// <summary>
        /// 按时间段查询人员列表：开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditRYKaiShiShiJian_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 按时间段查询人员列表：开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditRYJieShuShiJian_EditValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReloadList_Click(object sender, EventArgs e)
        {
            BinDingSurplusPatientGrid();
            //BinDingCriticalValueGrid();
            BinDingGeRenTiJianLiShi();
            BinDingCriticalValueGrid();
        }
        private DepartmentCustomer departmentCustomer;
        /// <summary>
        /// 高级检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonGaoJiJianSuo_Click(object sender, EventArgs e)
        {
            //var cusListForm = new CusList();
            //if (cusListForm.ShowDialog() == DialogResult.OK)
            //{
            //    searchHuanZheXinXi.Text = cusListForm.curCustomerBM;
            //    LoadCurrentCustomerReg(cusListForm.curCustomerBM);
            //}
            //判断窗体是否关闭
            if (departmentCustomer == null || departmentCustomer.IsDisposed)
            {
                departmentCustomer = new DepartmentCustomer();
                departmentCustomer.CustomerChanged += (ss, ee) =>
                {
                    searchHuanZheXinXi.Text = ee.CustomerNumber;
                    LoadCurrentCustomerReg(ee.CustomerNumber);
                };
                departmentCustomer.Show(this);
                return;
            }
            //没有关闭的话激活一下
            departmentCustomer.Activate();
        }
        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridJianChaXiangMu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                ShengChengKeShiXiaoJie();
            }
            if (e.KeyCode == Keys.D && e.Control)
            {
                UpdateCurrentItem(true);
            }
        }
        /// <summary>
        /// 根据体检状态更改行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewDangTianHuanZhe_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                var data = (TjlCustomerRegForDoctorDto)gridViewDangTianHuanZhe.GetRow(e.RowHandle);

                //体检完成绿色，体检中蓝色，未见黑色
                //if (data.CheckSate == (int)PhysicalEState.Complete)
                //{
                //    e.Appearance.ForeColor = Color.Green;
                //}
                //if (data.CheckSate == (int)PhysicalEState.Process)
                //{
                //    e.Appearance.ForeColor = Color.Blue;
                //}
                //医生站人员列表组合部分检查人员列表显示部分检查的科室
                var YbYS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusList, 3)?.Remarks;
                bool hasKs = false;
                if (!string.IsNullOrEmpty(YbYS)  )
                {
                    var departNamelist = YbYS.Replace("，", ",").Split(',').ToList();
                    var deparids = DefinedCacheHelper.GetDepartments().Where(p => departNamelist.Contains(p.Name)).Select(
                        p => p.Id).ToList();
                    foreach (var deparid in _departmentGuidSys)
                    {
                        if (deparids.Contains(deparid))
                        {
                            hasKs = true;
                            break;
                        }
                    }



                }
                //组合有部分检查人员列表显示部分检查
                if (hasKs == true)
                {
                    var groupwj = data.CustomerItemGroup.Where(o => _departmentGuidSys.Contains(o.DepartmentId) && o.IsAddMinus != 3 && 
                    (o.CheckState == (int)ProjectIState.Not || o.CheckState == (int)ProjectIState.Part )).ToList();
                    if (groupwj.Count == 0)
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    else
                    {
                        var groupyj = data.CustomerItemGroup.Where(o => _departmentGuidSys.Contains(o.DepartmentId) && o.IsAddMinus != 3 
                        && o.CheckState != (int)PhysicalEState.Not).ToList();
                        if (groupyj.Count > 0)
                        {
                            e.Appearance.ForeColor = Color.Blue;
                        }
                    }
                }
                else
                {
                    var groupwj = data.CustomerItemGroup.Where(o => _departmentGuidSys.Contains(o.DepartmentId) && o.IsAddMinus != 3 && o.CheckState == (int)PhysicalEState.Not).ToList();
                    if (groupwj.Count == 0)
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    else
                    {
                        var groupyj = data.CustomerItemGroup.Where(o => _departmentGuidSys.Contains(o.DepartmentId) && o.IsAddMinus != 3 && o.CheckState != (int)PhysicalEState.Not).ToList();
                        if (groupyj.Count > 0)
                        {
                            e.Appearance.ForeColor = Color.Blue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断项目有没有图片，没有图片就不显示图片标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridZuHeXiangMu_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {


            if (e.Column == colXMTuPian && _customerItemPicAllSys != null)
            {
                var row = (GridZhanShiLei)gridZuHeXiangMu.GetRow(e.RowHandle);
                var ImgCount = _customerItemPicAllSys?.Where(o => o.ItemBMID == row.XiangMuId)?.ToList();
                if (ImgCount != null && ImgCount.Count != 0) //正式判断
                {
                    var item = new RepositoryItemButtonEdit();
                    item.Buttons.Clear();
                    item.Buttons.Add(new EditorButton(ButtonPredefines.Search));
                    item.TextEditStyle = TextEditStyles.HideTextEditor;
                    item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
                    item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;
                    e.RepositoryItem = item;
                }
            }
            #region 自动加载字典下拉
            // List < OccCustomerHazardSumDto >
            if (e.Column == colXMJieGuo && _itemDictionarySys != null)
            {
                var row = (GridZhanShiLei)gridZuHeXiangMu.GetRow(e.RowHandle);
                if (row != null && row.XiangMuBMId != Guid.Empty)
                {
                    var ImgCount = _itemDictionarySys.Where(o => o.iteminfoBMId == row.XiangMuBMId && o.ApplySate == 1)?.ToList();
                    if (ImgCount != null && ImgCount.Count != 0) //正式判断
                    {
                        var item = new RepositoryItemComboBox();
                     
                        var diclist = ImgCount.OrderBy(p => p.OrderNum).ToList();
                        foreach (var dic in diclist)
                        {
                            item.Items.Add(dic.Word);
                        }
                       item.Click += repositoryItemComboBoxjieguo_Click;
                        item.KeyDown += Item_KeyDown;
                        item.KeyUp += Item_KeyUp;
                        e.RepositoryItem = item;
                    }
                }

            }
            #endregion

        }

        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is ComboBoxEdit cb)
            {
                if (e.KeyCode == Keys.Down)
                {
                    cb.ShowPopup(); 
                }
            }
        }

        /// <summary>
        /// 回车下一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName) == null)
                    return;

                // var ZuHeId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName).ToString());
                //var DepartmentId = Guid.Empty;
                //if (tvJianChaXiangMu.SelectedNode.Parent == null)
                //{
                //    DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                //}
                //else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                //{
                //    DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                //}
                //CurrentUser.TbmDepartments.FirstOrDefault(o => o.Id == DepartmentId) != null && 
                //if (_currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault() != null)
                //{
                //    _currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault().IsUpdate = true;
                //}
                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                var coloumnName = gridZuHeXiangMu.FocusedColumn.Name;
                rowHandle = rowHandle + 1;
                if (rowHandle >= gridZuHeXiangMu.RowCount - 1)
                {
                    if (islast == true)
                    {
                        btnShengChengXiaoJie.Focus();
                    }
                }
                else
                {
                    //// gridZuHeXiangMu.Focus();
                    //gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                    //gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                    //// gridZuHeXiangMu.ShowEditor();
                }
            }
            _istabDown = false;
        }

        private void Item_Enter(object sender, EventArgs e)
        {
            if(sender is ComboBoxEdit cb)
            {
                cb.ShowPopup();
            }
        }

        /// <summary>
        /// 历年对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonHistoryContrast_Click(object sender, EventArgs e)
        {
            if (_currentInputSys != null && _currentInputSys.Customer != null && _currentInputSys.Customer.Id != Guid.Empty)
            {
                FrmHistoryYearComparison frmSeleteItemGroup = new FrmHistoryYearComparison();
                frmSeleteItemGroup.CustomerGuid = _currentInputSys.Customer.Id;
                frmSeleteItemGroup.Show();
            }
            else
            {
                XtraMessageBox.Show("没有人员信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 危急值信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCritical_Click(object sender, EventArgs e)
        {
            if (_currentInputSys != null)
            {
                DoctorCrisis Crisis = new DoctorCrisis();
                Crisis.CustomerBM = _currentInputSys.CustomerBM;
                Crisis.Show();
            }
            else
            {
                XtraMessageBox.Show("没有人员信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 初始化后定位到搜索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoctorDesk_Shown(object sender, EventArgs e)
        {
            searchHuanZheXinXi.Focus();
        }
        /// <summary>
        /// 清除窗体数据
        /// </summary>
        private void EliminateFrom()
        {
            gridJianChaXiangMu.DataSource = null;
            tvJianChaXiangMu.Nodes.Clear();
            memoEditKeShiXiaoJie.Text = string.Empty;
            memoEditZhenDuan.Text = string.Empty;
            //GridViewGeRenTiJianLiShi.DataSource = null;
            pictureZhaoPian.Image = null;
            //labHuanZheLeiBie.Text = string.Empty;
            labelDangAnHao.Text = string.Empty;
            labelTiJianHao.Text = string.Empty;
            textEditXingMing.Text = string.Empty;
            labXingBie.Text = string.Empty;
            labNianLing.Text = string.Empty;
            labHunFou.Text = string.Empty;
            labDengJiShiJian.Text = string.Empty;
            textEditDanWei.Text = string.Empty;
            labClientType.Text = string.Empty;
            labelCategory.Text = string.Empty;
            memoEditRemark.Text = "";
            memoEditRegRemark.Text = "";
            labdepart.Text = string.Empty;
            labelIdNumber.Text = string.Empty;

        }
        /// <summary>
        /// 选择诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customGridLookUpEditZhenDuan_EditValueChanged(object sender, EventArgs e)
        {
            if (customGridLookUpEditZhenDuan.EditValue != null && !string.IsNullOrWhiteSpace(customGridLookUpEditZhenDuan.EditValue.ToString()))
            {
                var start = memoEditZhenDuan.SelectionStart;
                var value = customGridLookUpEditZhenDuan.Text;
                memoEditZhenDuan.Text = memoEditZhenDuan.Text.Insert(start,
                    value);
                customGridLookUpEditZhenDuan.EditValue = null;
            }
        }
        /// <summary>
        /// 回车下一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemTextEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                if (isOk == true)
                {
                    isOk = false;
                    return;
                }
                if (gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName) == null)
                    return;               
                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                var coloumnName = gridZuHeXiangMu.FocusedColumn.Name;
                rowHandle = rowHandle + 1;
                //var ss = gridZuHeXiangMu.GetRowCellDisplayText(rowHandle-1,colXMJieGuo.FieldName)?.ToString();
                //if (ss != null)
                //{
                //    ss = ss.TrimEnd(System.Environment.NewLine.ToCharArray());
                //    gridZuHeXiangMu.SetFocusedRowCellValue(colXMJieGuo.FieldName, ss);
                //}

                if (rowHandle > gridZuHeXiangMu.DataRowCount - 1)
                {
                    //if (islast == true)
                    //{
                        btnShengChengXiaoJie.Focus();
                    //}
                  
                }
                else
                {
                    gridZuHeXiangMu.Focus();
                    gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                    gridZuHeXiangMu.ShowEditor();
                }
             

            }
            _istabDown = false;
          
        }
        /// <summary>
        /// 复制lable文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelTextCopy_Click(object sender, EventArgs e)
        {
            var labelControl = (LabelControl)sender;
            if (string.IsNullOrWhiteSpace(labelControl.Text))
            {
                return;
            }
            try
            {
                Clipboard.SetText(labelControl.Text);
            }
            catch (ExternalException)
            {

            }
            alertControl.Show(this, "复制提示", $"“{labelControl.Text}”已复制到剪贴板!");
        }
        /// <summary>
        /// 设置危急值（A类）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemInCriticalValue_ItemClick(object sender, ItemClickEventArgs e)
        {
            setWJZ(1);
        }
        private void setWJZ(int level)
        {
            var ItemId = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
            var itemGroupId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName)?.ToString());
            var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(colXMWeiJiZhi.FieldName);
            if (CheckState != null && CheckState.ToString() == ((int)CrisisSate.Abnormal).ToString())
            {
                var itemName = gridZuHeXiangMu.GetFocusedRowCellValue(colXMMingCheng.FieldName);
                alertControl.Show(this, "温馨提示", $"“{itemName}”此项目已是危急项目，请勿重复设置!");
                return;
            }
            if (ItemId != null)
            {
                string pass = "";
                frmIllContent frmIll = new frmIllContent();
                if (frmIll.ShowDialog() == DialogResult.OK)
                {
                    pass = frmIll.IllContent;
                }
                else
                {
                    return;
                }

                var Query = new UpdateClass();
                Query.CustomerItemId = Guid.Parse(ItemId.ToString());
                Query.CrisisSate = (int)CrisisSate.Abnormal;
                Query.CrisisLever = level;
                Query.CrisiChar = pass;
                Query.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStation.UpdateCrisisSate(Query);
                    if (trGroup.IsOK == false)
                    {
                        XtraMessageBox.Show(trGroup.ErrMess);
                        return;
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                if (trGroup.IsOK == true)
                {
                    var DepartmentId = Guid.Empty;
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }                    
                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == itemGroupId).First().CustomerRegItem)
                    {
                       
                        if (item.Id == Query.CustomerItemId)
                        {
                            item.CrisisSate = (int)CrisisSate.Abnormal;
                            item.PositiveSate = (int)PositiveSate.Abnormal;

                            item.CrisisLever = level;
                        }
                    }
                    BinDingDataTreeS(DepartmentId);
                    BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 取消危急值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemOutCriticalValue_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ItemId = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
            var itemGroupId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName)?.ToString());
            var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(colXMWeiJiZhi.FieldName);
            if (CheckState != null && CheckState.ToString() == ((int)CrisisSate.Normal).ToString())
            {
                var itemName = gridZuHeXiangMu.GetFocusedRowCellValue(colXMMingCheng.FieldName);
                alertControl.Show(this, "温馨提示", $"“{itemName}”此项目不为危急项目，请勿重复设置!");
                return;
            }

            if (ItemId != null)
            {
                var Query = new UpdateClass();
                Query.CustomerItemId = Guid.Parse(ItemId.ToString());
                Query.CrisisSate = (int)CrisisSate.Normal;

                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStation.UpdateCrisisSate(Query);
                    if (trGroup.IsOK == false)
                    {
                        XtraMessageBox.Show(trGroup.ErrMess);
                        return;
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                if (trGroup.IsOK == true)
                {
                    var DepartmentId = Guid.Empty;
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }
                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == itemGroupId).First().CustomerRegItem)
                    {
                        item.CrisisSate = (int)CrisisSate.Normal;
                        item.PositiveSate = (int)PositiveSate.Normal;

                    }
                    BinDingDataTreeS(DepartmentId);
                    BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            _doctorStation.BatchInsertDodyFat(new QueryClass());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _doctorStation.BatchInsertDodyFat(new QueryClass());
        }
        /// <summary>
        /// 字典弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEditDictionaries_Click(object sender, EventArgs e)
        {
            if (dockPanelZiDian.Visibility != DockVisibility.Visible)
                dockPanelZiDian.Visibility = DockVisibility.Visible;
            Dictionaries();
        }

        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            var cusListForm = new DepartmentCustomer();
            if (cusListForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        public void DeparmentCustomerGridViewRowClick()
        {

        }
        /// <summary>
        /// 点击结果更换字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemTextEditJieGuo_Click(object sender, EventArgs e)
        {
            Dictionaries();
        }

        private void simpleButton1_Click_3(object sender, EventArgs e)
        {
            _doctorStation.BatchInsertDodyFat(new QueryClass());
        }

        private void butWJ_Click(object sender, EventArgs e)
        {
            if (_currentInputSys != null && _currentInputSys.Customer != null && _currentInputSys.Customer.Id != Guid.Empty)
            {
                FrmHistory frmSeleteItemGroup = new FrmHistory(_currentInputSys.Id, Guid.Empty);

                frmSeleteItemGroup.Show();
            }
            else
            {
                XtraMessageBox.Show("没有人员信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //计算体脂
        //private void simpleButton1_Click_2(object sender, EventArgs e)
        //{
        //    _doctorStation.CalculationConstitution(new QueryClass());
        //}
        //插入项目
        //private void simpleButton1_Click_1(object sender, EventArgs e)
        //{
        //    _doctorStation.BatchInsertDodyFat(new QueryClass());
        //}
        #region 历年对比
        /// <summary>
        /// 体检人Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }
        /// <summary>
        /// 体检人预约信息
        /// </summary>
        private List<Application.HistoryComparison.Dto.SearchCustomerRegDto> CustomerRegListSys;
        /// <summary>
        /// 体检人组合记录
        /// </summary>
        private List<CustomerItemGroupDto> CustomerItemGroupSys;
        /// <summary>
        /// 体检人项目记录
        /// </summary>
        private List<SearchCustomerRegItemDto> CustomerRegItemSys;
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly HistoryComparisonAppService _calendarYearComparison;
        /// <summary>
        /// 记录已生成列名（列名是时间）
        /// </summary>
        private Dictionary<int, string> DictDatetime;
        /// <summary>
        /// 展示类
        /// </summary>
        private DataTable ExhibitionTable;
        /// <summary>
        /// 下拉框绑定科室
        /// </summary>
        private List<DistinctDepartment> DepartmentListSys;
        /// <summary>
        /// 下拉框绑定组合
        /// </summary>
        private List<DistinctItemGroup> ItemGroupListSys;
        /// <summary>
        /// 下拉框绑定项目
        /// </summary>
        private List<DistinctItem> ItemListSys;
 
        #endregion
        #region 病史信息
        private ICustomerAppService customerSvr;//体检预约
        private Guid customerregid;
        private Guid mclientregid;
        /// <summary>
        /// 医生站
        /// </summary>
       // private IDoctorStationAppService _doctorStation;




        private void FrmHistory_Load(object sender, EventArgs e)
        {
            //_doctorStation = new DoctorStationAppService();


            //加载1+X问卷
            if (customerregid != Guid.Empty)
            {
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                entityDto.Id = customerregid;

            }

        }




        private void TextShow(string sname, MemoEdit memoEdit, bool ischeck)
        {
            if (ischeck == true)
            {
                memoEdit.Text += sname + "；";
            }
            else
            {
                memoEdit.Text = memoEdit.Text.Replace(sname + "；", "");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private List<SaveCustomerQusTionDto> getSaveCustomerQusTion(FlowLayoutPanel flowLayoutPanel, List<SaveCustomerQusTionDto> quesList)
        {

            foreach (Control con in flowLayoutPanel.Controls)
            {
                DevExpress.XtraEditors.CheckEdit checkedit = (DevExpress.XtraEditors.CheckEdit)con;
                if (checkedit.Checked == true)
                {
                    SaveCustomerQusTionDto saveCustomerQusTionDto = new SaveCustomerQusTionDto();
                    saveCustomerQusTionDto.ClientRegId = mclientregid;
                    saveCustomerQusTionDto.CustomerRegId = customerregid;
                    saveCustomerQusTionDto.OneAddXQuestionnaireid = new Guid(con.Name);
                    quesList.Add(saveCustomerQusTionDto);
                }
            }
            return quesList;
        }
        #endregion

    
        private void pictureEditTuPianXiaYiGe_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabDTHuanZheLieBiao_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabDTHuanZheLieBiao.SelectedTabPageIndex == 1)
            {
                txtRYTianShu.Focus();
            }
        }

        private void dockPanelTuPianZhanShi_Click(object sender, EventArgs e)
        {

        }
        #region 图片鼠标事件     

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double scale = 1;
            if (pictureEditTuPianZhanShi.Height > 0)
            {
                scale = (double)pictureEditTuPianZhanShi.Width / (double)pictureEditTuPianZhanShi.Height;
            }
            pictureEditTuPianZhanShi.Width += (int)(e.Delta * scale);
            pictureEditTuPianZhanShi.Height += e.Delta;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Focus();
            pictureEditTuPianZhanShi.Cursor = Cursors.SizeAll;
        }

        //在MouseDown处获知鼠标是否按下，并记录下此时的鼠标坐标值；
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;  //注：全局变量mouseDownPoint前面已定义为Point类型  
                mouseDownPoint.Y = Cursor.Position.Y;
                isSelected = true;
            }
        }

        //在MouseUp处获知鼠标是否松开，终止拖动操作；
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;

        }

        private bool IsMouseInPanel()
        {
            if (this.panel_Picture.Left < PointToClient(Cursor.Position).X
                    && PointToClient(Cursor.Position).X < this.panel_Picture.Left
                    + this.panel_Picture.Width && this.panel_Picture.Top
                    < PointToClient(Cursor.Position).Y && PointToClient(Cursor.Position).Y
                    < this.panel_Picture.Top + this.panel_Picture.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //图片平移,在MouseMove处添加拖动函数操作
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected && IsMouseInPanel())//确定已经激发MouseDown事件，和鼠标在picturebox的范围内
            {
                this.pictureEditTuPianZhanShi.Left = this.pictureEditTuPianZhanShi.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureEditTuPianZhanShi.Top = this.pictureEditTuPianZhanShi.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        #endregion

        private void panel_Picture_SizeChanged(object sender, EventArgs e)
        {
            pictureEditTuPianZhanShi.Width = panel_Picture.Width - 6;
            pictureEditTuPianZhanShi.Height = panel_Picture.Height - 6;
        }

        private void gridLookUpSex_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void searchHuanZheXinXi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void checkHas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHas.Checked == true)
            {
                checkEditRyWeiJianRenShu.Checked = false;
                checkEditPart.Checked = false;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)

        {
            if (!string.IsNullOrWhiteSpace(textEdit1.Text))
            {
                //textEdit1.Text = string.Empty;
                BinDingSurplusPatientGrid();
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {

        }
        bool islast = false;       
        /// <summary>
        /// 回车下一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridZuHeXiangMu_KeyDown(object sender, KeyEventArgs e)
        {
           

            if (e.KeyCode == Keys.Enter)
            {
                islast = false;
                if (gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName) == null)
                {
                    return;
                }

                var ZuHeId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName).ToString());

                //var DepartmentId = Guid.Empty;
                //if (tvJianChaXiangMu.SelectedNode.Parent == null)
                //{
                //    DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                //}
                //else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                //{
                //    DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                //}
                //CurrentUser.TbmDepartments.FirstOrDefault(o => o.Id == DepartmentId) != null && 
                if (_currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault() != null)
                {
                    _currentCustomerItemGroupSys.Where(o => o.Id == ZuHeId).FirstOrDefault().IsUpdate = true;
                }

                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                var coloumnName = gridZuHeXiangMu.FocusedColumn.Name;
                rowHandle = rowHandle + 1;

                if (rowHandle >= gridZuHeXiangMu.DataRowCount)
                {


                    if (dockPanelZiDian.Visibility == DockVisibility.Visible)
                    {
                        dockPanelZiDian.Visibility = DockVisibility.Hidden;
                    }
                    gridZuHeXiangMu.CloseEditor();
                    //this.ActiveControl = btnShengChengXiaoJie;
                    //this.btnShengChengXiaoJie.Select();
                    //this.btnShengChengXiaoJie.Focus();
                    islast = true;
                }
                else
                {
                     gridZuHeXiangMu.Focus();
                    gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];

                    gridZuHeXiangMu.ShowEditor();
                    if (dockPanelZiDian.Visibility == DockVisibility.Visible)
                    {
                        Dictionaries();
                    }
                  
                }
            }
            #region 下拉展示列表注释
            //if (e.KeyCode == Keys.Down)
            //{
            //    var forcon = gridZuHeXiangMu.FocusedColumn;
            //    if (forcon.Caption == "结果")
            //    {

            //        GridView view = sender as GridView;
            //        //((RepositoryItemComboBox)view.FocusedColumn.ColumnEdit).Buttons[0].PerformClick();
            //        GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
            //        GridDataRowInfo rowInfo = vi.RowsInfo.GetInfoByHandle(gridZuHeXiangMu.FocusedRowHandle) as GridDataRowInfo;
            //        if (rowInfo != null)
            //        {


            //            ////repositoryItemComboBoxjieguo.ShowDropDown = ShowDropDown.SingleClick;

            //            ////获取RepositoryItemComboBox下拉单元格信息
            //            GridCellInfo cellInfo = rowInfo.Cells[2];

            //            //cellInfo.Editor
            //            repositoryItemComboBoxjieguo.Buttons[0].PerformClick();

            //            //var p1X = layoutControlBase.Location.X+ tabPane1.Location.X + gridSplitContainer1.Location.X;
            //            //var p1Y = layoutControlBase.Location.Y + tabPane1.Location.Y + gridSplitContainer1.Location.Y;

            //            ////将下拉单元格所在的相对坐标转换为屏幕的绝对坐标
            //            //Point p = this.PointToScreen(new Point(p1X + cellInfo.CellValueRect.X, p1Y + cellInfo.CellValueRect.Y));
            //            ////设置鼠标位置
            //            //if (view.IndicatorWidth > 0 && gridView1.ColumnPanelRowHeight > 0)
            //            //{
            //            //    MouseFlag.SetCursorPos(p.X + view.IndicatorWidth, p.Y + gridView1.ColumnPanelRowHeight);
            //            //}
            //            //else
            //            //{
            //            //    MouseFlag.SetCursorPos(p.X + 5, p.Y);
            //            //}
            //            ////单击RepositoryItemComboBox下拉单元格
            //            //MouseFlag.MouseLefDownEvent(0, 0, 0);
            //        }

            //    }
            //} 
            #endregion

            _istabDown = false;
        }
        private int getpontX (Control control,int x)
        {
            if (control.Parent != null)
            {
                return getpontX(control.Parent, control.Location.X);
            }
            else
            {
                return control.Location.X + x;
            }

        }
        private int getpontY(Control control, int Y)
        {
            if (control.Parent != null)
            {
                return getpontY(control.Parent, control.Location.Y);
            }
            else
            {
                return control.Location.Y + Y;
            }

        }

        private void hyperlinkLabelControl1_Click_1(object sender, EventArgs e)
        {
            frmDeptDoctName frmDeptDoctName = new frmDeptDoctName();

            if (frmDeptDoctName.ShowDialog() == DialogResult.OK)
            {
                if (hsjzlr == true)
                {
                    layDeptName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    string fp = System.Windows.Forms.Application.StartupPath + "\\DepartNameinfo.json";
                    if (File.Exists(fp))  // 判断是否已有相同文件 
                    {
                        deptNameDtos = JsonConvert.DeserializeObject<List<DeptNameDto>>(File.ReadAllText(fp));
                        deptNameDtos = deptNameDtos.Where(o => o.DepatId != null && o.DoctId != null).ToList();
                    }
                }
            }

        }

        private void txtClientRegID_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;              

            }
        }

        private void lookUpEditClientType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lookUpEditClientType_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditClientType.EditValue != null)
            {
                txtRYTianShu.Text = string.Empty;
                BinDingSurplusPatientGrid();

            }

        }

        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtClientRegID.EditValue != null)
            //{
            //    txtRYTianShu.Text = string.Empty;
            //    BinDingSurplusPatientGrid();

            //}
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(textEdit1.Text))
                {
                    //textEdit1.Text = string.Empty;
                    BinDingSurplusPatientGrid();
                }
            }
        }

        private void gridZuHeXiangMu_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //DataCustomerItemGroup = _currentCustomerItemGroupSys.Where(o => o.DepartmentId == SelectId)
            var Grvup = _currentCustomerItemGroupSys.Where(o => o.CustomerRegItem.Any( i => i.ProcessState==4)).ToList();
            if (Grvup.Count > 0)
            {
                var datas = (GridZhanShiLei)gridZuHeXiangMu.GetRow(e.RowHandle);
                if (e.Column == colXMMingCheng)
                {
                    if (datas.ProcessState == 4)
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
            }
        }

        private void hideContainerRight_Click(object sender, EventArgs e)
        {

        }

        private void gridViewWeiJiZhi_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
                var CustomerBM = gridViewWeiJiZhi.GetFocusedRowCellValue(colWeiJiZhiTiJianHao.FieldName);
                if (_currentInputSys == null || _currentInputSys.CustomerBM == null)
                {
                    searchHuanZheXinXi.Text = CustomerBM.ToString();
                    LoadCurrentCustomerReg(CustomerBM.ToString());
                }
                else if (CustomerBM != null && CustomerBM.ToString() != _currentInputSys.CustomerBM)
                {
                    //获取修改数据
                    var data = _currentCustomerItemGroupSys.Where(o => o.IsUpdate).ToList();
                    //判断是否存在未保存的修改数据
                    if (data != null && data.Count() > 0 && _valueUpdate == true)
                    {
                        var question = XtraMessageBox.Show("存在未保存信息是否保存后切换？", "询问",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button2);
                        //判断是保存还是不保存
                        if (question == DialogResult.Yes)
                        {
                            UpdateCurrentItem(true);
                            searchHuanZheXinXi.Text = CustomerBM.ToString();
                            LoadCurrentCustomerReg(CustomerBM.ToString());
                        }
                        else if (question == DialogResult.No)
                        {
                            searchHuanZheXinXi.Text = CustomerBM.ToString();
                            LoadCurrentCustomerReg(CustomerBM.ToString());
                        }
                    }
                    else
                    {
                        searchHuanZheXinXi.Text = CustomerBM.ToString();
                        LoadCurrentCustomerReg(CustomerBM.ToString());
                    }

                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            //  GridView  view = sender as GridView;
            var gridView = (GridView)gridControlWeiJiZhi.FocusedView;

           


            var ItemId = gridView.GetFocusedRowCellValue(conID.FieldName);
            if (ItemId == null)
            {
                XtraMessageBox.Show(this, "请选择行", "取消危急值", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.Button.Caption == "取消")
            {
                //var ItemId = gridViewCrical.GetFocusedRowCellValue(conItemID.FieldName);  
                if (ItemId != null)
                {
                    var Query = new UpdateClass();
                    Query.CustomerItemId = Guid.Parse(ItemId.ToString());
                    Query.CrisisSate = (int)CrisisSate.Normal;

                    //修改状态返回数据
                    OutStateDto trGroup = new OutStateDto();
                    try
                    {
                        trGroup = _doctorStation.UpdateCrisisSate(Query);
                        if (trGroup.IsOK == false)
                        {
                            XtraMessageBox.Show(trGroup.ErrMess);
                            return;
                        }
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                        return;
                    }
                    if (trGroup.IsOK==true)
                    {
                        BinDingCriticalValueGrid();
                    }
                  
                }
                else
                {
                    XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void repositoryItemTextEdit1_Click(object sender, EventArgs e)
        {
            Dictionaries();
        }

        private void memoEditZiDianZD_TextChanged(object sender, EventArgs e)
        {
            var i = gridZuHeXiangMu.FocusedRowHandle;
            if (i == -1)
                return;
            //XiangMuZD
            var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
            if (_itemDictionarySys.Where(o => o.iteminfoBMId == Guid.Parse(XMID)).ToList().Count > 0)
            {
                var ZiDianWord = gridZuHeXiangMu.GetFocusedRowCellValue(colXMZD.FieldName)?.ToString();

                var TextJieGuo = memoEditZiDianZD.Text;
                if (TextJieGuo != ZiDianWord)
                    gridZuHeXiangMu.SetRowCellValue(i, gridZuHeXiangMu.Columns[colXMZD.FieldName], TextJieGuo);
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private async void simpleButton5_Click(object sender, EventArgs e)
        {
            //startIdentify();
            //var connectionString = ConfigurationManager.ConnectionStrings["BT_SQL"].ConnectionString;
            //初始化
            //ZhiWenShiBie zwsb = new ZhiWenShiBie(connectionString);
            //zwsb.ShowMessage += Zwsb_ShowMessage;
            var path = AppDomain.CurrentDomain.BaseDirectory + "指纹识别";
            string filename = path + "\\WindowsFormsApp1.exe";
            Process p = new Process();

               p.StartInfo.Arguments = "read";//读取体检号
            // p.StartInfo.Arguments = "GetImage0001";///根据体检号 获取图像显示在调用程序的窗体上
           // p.StartInfo.Arguments = "write" + curCustomRegInfo.CustomerBM;///入体检号 0001是体检号
            p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.FileName = filename;

            p.StartInfo.CreateNoWindow = false;
            p.Start();

            p.WaitForExit();

            string str = p.StandardOutput.ReadToEnd();
            str = str.Replace("load libFpDat_64.dll succ", "").Trim();//反馈的内容 不知道为什么多了load libFpDat_64.dll succ 所以 Replace掉就是正常返回的内容了
            if (string.IsNullOrEmpty(str))
            {
                MessageBox.Show("该指纹未识别出体检号！");
            }
            else
            {

                #region 根据指纹识别体检号
                string tjcode = str;
                if (!string.IsNullOrEmpty(tjcode.ToString()))
                {
                    searchHuanZheXinXi.Text = tjcode.ToString();
                    LoadCurrentCustomerReg(tjcode.ToString());
                }
            }
           
            
            #endregion
        }
        private void Zwsb_ShowMessage(string text)
        {
            this.Invoke(new Action(() =>
            {
                labelts.Text = text;
            }));

        }
        public  void startIdentify()
        {


            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                //Console.WriteLine("请按压指纹仪，以识别指纹");
                labInfo.Text = "请按压指纹仪，以识别指纹";
                labInfo.Refresh();
                int featureId = -1;
                int nRet = FpSdk.Identify(out featureId);
                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    //Console.WriteLine("指纹识别成功 id为:{0}", featureId);
                    labInfo.Text =string.Format("指纹识别成功 id为:{0}", featureId);
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_BAD_QUALITY)
                {
                    //Console.WriteLine("指纹质量差");
                    labInfo.Text = string.Format("指纹质量差");
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    //Console.WriteLine("找不到指纹仪");
                    labInfo.Text = string.Format("找不到指纹仪");
                    labInfo.Refresh();
                }
                else
                {
                    //Console.WriteLine("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Text = string.Format("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Refresh();
                }

            }
            else
            {
               // Console.WriteLine("找不到指纹仪");
                labInfo.Text = string.Format("找不到指纹仪");
                labInfo.Refresh();
            }

            FpSdk.UnInit();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_currentInputSys.SummSate == (int)SummSate.Audited
                || _currentInputSys.SummSate == (int)SummSate.HasBeenEvaluated
                 )
            {
                XtraMessageBox.Show("已总检或审核不能修改数据！");
                return;
            }

            var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if(group!=null && group.DepartmentId!=Guid.Empty)
            {
                getHisItemValue(group.DepartmentId, null, null);
            }

        }
        /// <summary>
        /// 获取历史结果
        /// </summary>
        /// <param name="departId"></param>
        /// <param name="GroupId"></param>
        /// <param name="itemId"></param>
        private void getHisItemValue(Guid? departId, Guid? GroupId, Guid? itemId)
        {
            var cusItems = gridJianChaXiangMu.DataSource as List<GridZhanShiLei>;
            List<Guid> Itemids = new List<Guid>();

            if (departId.HasValue)
            {
                 Itemids = cusItems.Where(p => p.DepartmentId == departId).Select(
                  p => p.XiangMuBMId).ToList();
               //var cusGrouplist= _currentCustomerItemGroupSys.Where(o => o.DepartmentId == departId);
                for (int num=0;num< _currentCustomerItemGroupSys.Count;num ++)
                {
                    if (_currentCustomerItemGroupSys[num].DepartmentId == departId)
                    {
                        _currentCustomerItemGroupSys[num].IsUpdate = true;
                    }
                }

            }
            if (GroupId.HasValue)
            {
                Itemids = cusItems.Where(p => p.ZuHeId == GroupId).Select(
                 p => p.XiangMuBMId).ToList();
                _currentCustomerItemGroupSys.Where(o => o.Id == GroupId).FirstOrDefault().IsUpdate = true;
            }
            if (itemId.HasValue)
            {
                Itemids = cusItems.Where(p => p.XiangMuBMId == itemId).Select(
                 p => p.XiangMuBMId).ToList();
               
            }
            InSerchHIsDto input = new InSerchHIsDto();
            input.ItemIds = Itemids;
            input.CustomerId = _currentInputSys.CustomerId;
            input.IDCardNo = _currentInputSys.Customer.IDCardNo;
            input.CustomerRegId = _currentInputSys.Id;
            var ItemVulse = _calendarYearComparison.getHisValue(input);
            if (ItemVulse.Count > 0)
            {
                foreach (var item in cusItems)
                {
                    var nowvalue = ItemVulse.FirstOrDefault(p => p.ItemId == item.XiangMuBMId);
                    if (nowvalue != null && !string.IsNullOrEmpty(nowvalue.ItemValue))
                    {
                        item.XiangMuJieGuo = nowvalue.ItemValue;


                        #region MyRegion
                        foreach (var cusitem in _currentCustomerItemGroupSys.Where(o => o.Id == item.ZuHeId).FirstOrDefault()
                   .CustomerRegItem)
                            if (cusitem.ItemId == item.XiangMuBMId)
                            {

                                //item.ItemResultChar = e.Value?.ToString().Trim();
                                cusitem.ItemResultChar = nowvalue.ItemValue;
                                if (colXMZD.Visible == true)//如果是诊断模式
                                {
                                    cusitem.ItemDiagnosis = nowvalue.ItemValue;
                                }
                                break;
                            }
                        #endregion
                    }
                }
                //gridZuHeXiangMu.DataSource = cusItems;
                gridJianChaXiangMu.DataSource = cusItems;
                gridJianChaXiangMu.RefreshDataSource();
                gridJianChaXiangMu.Refresh();
            }
            else
            {
                XtraMessageBox.Show("未发现历史数据");
            }

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_currentInputSys.SummSate == (int)SummSate.Audited
              || _currentInputSys.SummSate == (int)SummSate.HasBeenEvaluated
               )
            {
                XtraMessageBox.Show("已总检或审核不能修改数据！");
                return;
            }

            var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if (group != null && group.ZuHeId != Guid.Empty)
            {
                getHisItemValue(null, group.ZuHeId, null);
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_currentInputSys.SummSate == (int)SummSate.Audited
             || _currentInputSys.SummSate == (int)SummSate.HasBeenEvaluated
              )
            {
                XtraMessageBox.Show("已总检或审核不能修改数据！");
                return;
            }

            var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if (group != null && group.XiangMuBMId != Guid.Empty)
            {
                _currentCustomerItemGroupSys.Where(o => o.Id == group.ZuHeId).FirstOrDefault().IsUpdate = true;
                getHisItemValue(null, group.ZuHeId, group.XiangMuBMId);
            }
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            BinDingSurplusPatientGrid();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            BinDingSurplusPatientGrid();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {           
            var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if (group != null && group.DepartmentId != Guid.Empty)
            {
               // getHisItemValue(group.DepartmentId, null, null);
                Open(group.XiangMuBMId, group.DepartmentId, group.XiangMuJieGuo);
            }
        }
        /// <summary>
        /// 打开字典设置
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="DepartmentId"></param>
        private void Open(Guid ItemId, Guid DepartmentId,string word)
        {
            var ItemInfoid = ItemId;
            var Departentid = DepartmentId;
            try
            {
                using (var frm = new DictionaryEdit((Guid)ItemInfoid, (Guid)Departentid, word))
                {
                    //if (frm.ShowDialog() == DialogResult.OK)
                    //{
                        frm.ShowDialog();
                    var QueryDict = new QueryClassTwo();
                    QueryDict.DepartmentBMList = _departmentGuidSys;
                    _itemDictionarySys = new List<BTbmItemDictionaryDto>();
                    _itemDictionarySys = _doctorStation.GetItemDictionarylist(QueryDict);
                    Dictionaries();
                      
                }
            }
            catch
            {
                XtraMessageBox.Show("请选择项目进行查看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void DoctorDesk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
                var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
                if (group != null && group.DepartmentId != Guid.Empty)
                {
                    // getHisItemValue(group.DepartmentId, null, null);
                    Open(group.XiangMuBMId, group.DepartmentId, group.XiangMuJieGuo);
                }
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            if (_currentInputSys == null)
            {
                XtraMessageBox.Show("请选择体检人");
            }
            else
            {
                var XMID = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
                var ncustomerItemPic = _customerItemPicAllSys.Where(o => o.ItemBMID == Guid.Parse(XMID.ToString())).ToList();
               var regid= _currentInputSys.Id;
                var cusGroup = _currentCustomerItemGroupSys.Where(p => p.CustomerRegItem.Any(o=> o.Id ==
                  Guid.Parse(XMID.ToString()))).FirstOrDefault();
                   
                var cusGroupId = cusGroup.Id;
                frmItemPic frmItemPic = new frmItemPic(regid, cusGroupId, Guid.Parse(XMID.ToString()),
           ncustomerItemPic);
                frmItemPic.ShowDialog();
                if (frmItemPic.DialogResult == DialogResult.OK)
                {
                    _customerItemPicAllSys = _customerItemPicAllSys.Where(p => p.ItemBMID != Guid.Parse(XMID.ToString())).ToList();
                    _customerItemPicAllSys.AddRange(frmItemPic.customerItemPicSys);
                }


            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            setWJZ(2);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            setWJZ(1);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ItemId = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
            var itemGroupId = Guid.Parse(gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName)?.ToString());
            var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(colXMWeiJiZhi.FieldName);
            if (CheckState != null && CheckState.ToString() == ((int)CrisisSate.Normal).ToString())
            {
                var itemName = gridZuHeXiangMu.GetFocusedRowCellValue(colXMMingCheng.FieldName);
                alertControl.Show(this, "温馨提示", $"“{itemName}”此项目不为危急项目，请勿重复设置!");
                return;
            }

            if (ItemId != null)
            {
                var Query = new UpdateClass();
                Query.CustomerItemId = Guid.Parse(ItemId.ToString());
                Query.CrisisSate = (int)CrisisSate.Normal;

                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStation.UpdateCrisisSate(Query);
                    if (trGroup.IsOK == false)
                    {
                        XtraMessageBox.Show(trGroup.ErrMess);
                        return;
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }

                if (trGroup.IsOK == true)
                {
                    var DepartmentId = Guid.Empty;
                    if (tvJianChaXiangMu.SelectedNode.Parent == null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                    }
                    else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                    {
                        DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                    }
                    foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == itemGroupId).First().CustomerRegItem)
                    {
                        item.CrisisSate = (int)CrisisSate.Normal;
                        item.PositiveSate = (int)PositiveSate.Normal;

                    }
                    BinDingDataTreeS(DepartmentId);
                    BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem6_ItemClick_1(object sender, ItemClickEventArgs e)
        {
           
            var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
            if (group != null && group.DepartmentId != Guid.Empty)
            {
                frmHistory frmCusHis = new frmHistory(
                    _currentInputSys.CustomerId, _currentInputSys.Customer.IDCardNo,
                    group.DepartmentId);
                frmCusHis.ShowDialog();


            }

        }

        private void barButtonItem7_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var s = MessageBox.Show("确定要放弃吗？", "提示", MessageBoxButtons.OKCancel);
            if (s == DialogResult.OK)
            {
                if (_currentInputSys != null && _currentInputSys.SendToConfirm.HasValue
                   && _currentInputSys.SendToConfirm == (int)SendToConfirm.Yes)
                {
                    ShowMessageBoxInformation("已交表不能放弃!");
                    return;
                }
                var Name = gridZuHeXiangMu.GetFocusedRowCellValue(colXMId.FieldName);
                var cusGroup =  gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName);
                var group = gridZuHeXiangMu.GetFocusedRow() as GridZhanShiLei;
                if (group != null && group.ZuHeId != Guid.Empty)
                {

                    if (!_departmentGuidSys.Contains(group.DepartmentId))
                    {
                        XtraMessageBox.Show("您没有该科室的修改权限！");
                    }
                    if (Name == null)
                    {
                        Name = group.XiangMuId;
                        cusGroup = group.ZuHeId;
                    }
                }
                var CheckState = gridZuHeXiangMu.GetFocusedRowCellValue(conProcessState.FieldName);
                if (CheckState != null && CheckState.ToString() == ((int)ProjectIState.GiveUp).ToString())
                {
                    alertControl.Show(this, "温馨提示", "这个项目已经弃检啦!");
                    //XtraMessageBox.Show("此数据已经弃检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (Name != null)
                {
                    var Query = new UpdateClass();
                    Query.CustomerItemGroupId = Guid.Parse(cusGroup.ToString());
                    Query.CustomerItemId = Guid.Parse(Name.ToString());
                    Query.CheckState = (int)ProjectIState.GiveUp;
                    Query.ProcessState = (int)ProjectIState.GiveUp;

                    //修改状态返回数据
                    var trGroup = new ATjlCustomerItemGroupDto();
                    try
                    {
                        trGroup = _doctorStation.GiveUpCheckItem(Query);
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                        return;
                    }

                    if (trGroup != null && trGroup.Id != Guid.Empty)
                    {
                        ////更新数据源和列表数据
                        //var DataCustomerItemGroup = new List<ATjlCustomerItemGroupDto>();
                        //DataCustomerItemGroup.Add(trGroup);

                        //获取当前选择科室id
                        var DepartmentId = Guid.Empty;
                        if (tvJianChaXiangMu.SelectedNode.Parent == null)
                        {
                            DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
                        }
                        else if (tvJianChaXiangMu.SelectedNode.Parent != null)
                        {
                            DepartmentId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                        }
                        // _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CheckState = Query.CheckState;
                        _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().IsUpdate =true;
                        foreach (var item in _currentCustomerItemGroupSys.Where(o => o.Id == Query.CustomerItemGroupId).First().CustomerRegItem)
                        {
                            if (item.Id == Query.CustomerItemId)
                            {
                                item.ProcessState = Query.CheckState;
                                
                            }
                        }
                        BinDingDataTreeS(DepartmentId);

                        //BinDingGridJianChaXiangMu(_currentCustomerItemGroupSys);
                    }
                    alertControl.Show(this, "温馨提示", "已经弃检成功啦!");
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = trGroup.ItemGroupName;
                    createOpLogDto.LogName = trGroup.CheckStateString;

                    //createOpLogDto.LogText = "待查项目：" + string.Join(",", Names);

                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    //XtraMessageBox.Show("修改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void but360_Click(object sender, EventArgs e)
        {
            if (_currentInputSys != null && !string.IsNullOrEmpty(_currentInputSys.Customer.SectionNum))
            {
                var InterUrl = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.InterUrl, 1)?.Remarks;
                if (string.IsNullOrEmpty(InterUrl))
                {
                    MessageBox.Show("请在字典360视图地址编码为1备注中维护接口地址");
                    return;
                }
                frmXZ frmXZ = new frmXZ();
                frmXZ.patientNo = _currentInputSys.Customer.SectionNum;
                frmXZ.Show();
            }
            else
            {
                MessageBox.Show("无患者号不能进入网页");
            }
        }

        private void simpleButton1_Click_4(object sender, EventArgs e)
        {
            try
            {

                if (_currentInputSys!=null &&  _currentInputSys.Customer.CusPhotoBmId.HasValue &&
                    _currentInputSys.Customer.CusPhotoBmId != Guid.Empty
                    )
                {
                    var urlPath = new PictureController().GetUrl(_currentInputSys.Customer.CusPhotoBmId.Value);
                    var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() ;
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                    }
                   var file= DownFile2(urlPath.Thumbnail, url );
                    if (file != "" && File.Exists(file))
                    {
                        string args = file;
                        var path = AppDomain.CurrentDomain.BaseDirectory + "\\人证核验";
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
                    else
                    {
                        MessageBox.Show("下载照片失败！");
                    }
                }
                else
                {
                    MessageBox.Show("无照片信息！");
                }

                
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }
        }
        //url  要下载的图片地址，全路径
        //file 要保存的地址 文件夹/文件名
        //ext  图片后缀
        //fileType 图片后缀
        public static string DownFile2(string url, string file)
        {
            WebClient wc = new WebClient();
            string reslut = "";
            try
            {

                Stream stream = wc.OpenRead(url);
                //如果图片没有后缀的，根据下面方法获取后缀名
                var fileType = wc.ResponseHeaders["Content-Type"].Split('/')[1];

                stream.Close();  //以及释放内存  
                wc.Dispose();
                //  只下载这几种类型png  gif  jpeg  bmp  jpg
                if (fileType != "png" && fileType != "gif" && fileType != "jpeg" && fileType != "bmp" && fileType != "jpg")
                {
                    reslut = "";
                    return reslut;
                }

                file += "." + fileType;
                //下载方法
                wc.DownloadFile(url, file);
                reslut = file;
            }
            catch (Exception e)
            {

                reslut = "";
            }
            //下载
            //读取下载下来的源文件HTML格式的字符
            //string mainData = File.ReadAllText(file, Encoding.UTF8);
            //return mainData;
            return reslut;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\DoctorDesk.json";

            List<Search> searchlist = new List<Search>();


           
            if (!string.IsNullOrWhiteSpace(txtRYTianShu.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "Day";
                search.Text = txtRYTianShu.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (checkEditRyWeiJianRenShu.Checked == true)
            {
                Search search = new Search();
                search.Name = "WeiJian";
                search.Text = "true";
                searchlist.Add(search);
            }
            else
            {
                Search search = new Search();
                search.Name = "WeiJian";
                search.Text = "false";
                searchlist.Add(search);
            }
            if (checkHas.Checked == true)
            {
                Search search = new Search();
                search.Name = "checkHas";
                search.Text = "true";
                searchlist.Add(search);
            }
            else
            {
                Search search = new Search();
                search.Name = "checkHas";
                search.Text = "false";
                searchlist.Add(search);
            }
            if (checkState.Checked == true)
            {
                Search search = new Search();
                search.Name = "checkState";
                search.Text = "true";
                searchlist.Add(search);
            }
            else
            {
                Search search = new Search();
                search.Name = "checkState";
                search.Text = "false";
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

        private void checkEditRyWeiJianRenShu_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void repositoryItemComboBoxjieguo_Click(object sender, EventArgs e)
        {
            Dictionaries();
        }

        private void gridZuHeXiangMu_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gridZuHeXiangMu_ShownEditor(object sender, EventArgs e)
        {

        }

        private void repositoryItemMemoEditJieguo_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            if (e.KeyChar == '\r')
            {
               
                //if (isOk == true)
                //{
                //    e.Handled = true;
                //    isOk = false;
                //    return;
                //}
                if (gridZuHeXiangMu.GetFocusedRowCellValue(colZuHeID.FieldName) == null)
                    return;
                var rowHandle = gridZuHeXiangMu.FocusedRowHandle;
                var coloumnName = gridZuHeXiangMu.FocusedColumn.Name;
                rowHandle = rowHandle + 1;
                //var ss = gridZuHeXiangMu.GetRowCellDisplayText(rowHandle-1,colXMJieGuo.FieldName)?.ToString();
                //if (ss != null)
                //{
                //    ss = ss.TrimEnd(System.Environment.NewLine.ToCharArray());
                //    gridZuHeXiangMu.SetFocusedRowCellValue(colXMJieGuo.FieldName, ss);
                //}

                if (rowHandle > gridZuHeXiangMu.DataRowCount - 1)
                {
                    //if (islast == true)
                    //{
                    btnShengChengXiaoJie.Focus();
                    //}

                }
                else
                {
                    gridZuHeXiangMu.Focus();
                    gridZuHeXiangMu.FocusedRowHandle = rowHandle;
                    gridZuHeXiangMu.FocusedColumn = gridZuHeXiangMu.Columns[colXMJieGuo.FieldName];
                    gridZuHeXiangMu.ShowEditor();
                }
                e.Handled = true;

            }
            _istabDown = false;

        }

        private void repositoryItemMemoEditJieguo_KeyDown(object sender, KeyEventArgs e)
        {
           

        }

        private void dockCuslist_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockCuslist.HideSliding();
            e.Cancel = true;
        }

        private void chkcmbDepartment_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
                editor.Text = "";
                chkcmbDepartment.Properties.Items.GetCheckedValues().Clear();
                chkcmbDepartment.EditValue = null;
                chkcmbDepartment.Text = "";
                chkcmbDepartment.Refresh();
                chkcmbDepartment.RefreshEditValue();
            }
        }
        #region 历史对比
        private void butsearchHis_Click(object sender, EventArgs e)
        {
            //小结和体检人不匹配提示
            if (_currentInputSys != null && _currentInputSys.Id != Guid.Empty)
            {
                SearchClass searchClass = new SearchClass();
                SearchHisClassDto searchHisClassDto = new SearchHisClassDto();
                searchHisClassDto.IDCardNo = _currentInputSys.Customer.IDCardNo;
                searchClass.CustomerId = _currentInputSys.CustomerId;
                if (!string.IsNullOrEmpty(searchLookUpDepartMent.EditValue?.ToString()))
                {
                    searchClass.DepartId = (Guid)searchLookUpDepartMent.EditValue;
                    searchHisClassDto.DepartName = searchLookUpDepartMent.Text;
                }
                if (!string.IsNullOrEmpty(searchLookUpGroup.EditValue?.ToString()))
                {
                    searchClass.GroupId = (Guid)searchLookUpGroup.EditValue;
                    searchHisClassDto.GroupName = searchLookUpGroup.Text;
                }
                if (!string.IsNullOrEmpty(searchLookUpItem.EditValue?.ToString()))
                {
                    searchClass.ItemId = (Guid)searchLookUpItem.EditValue;
                    searchHisClassDto.ItemName = searchLookUpItem.Text;
                }
                if (!string.IsNullOrEmpty(radioGroup2.EditValue?.ToString()) && radioGroup2.EditValue?.ToString() == "2")
                {
                    if (string.IsNullOrEmpty(searchHisClassDto.IDCardNo))
                    {
                        MessageBox.Show("体检人无身份证无法获取第三方历史数据！");
                        return;
                    }
                    else
                    {
                        var HisResult = _HistoryComparisonAppService.geHisvard(searchHisClassDto);
                        Reload(HisResult);
                        gridControlHisResult.DataSource = HisResult;
                    }

                }
                else
                {
                    var HisResult = _HistoryComparisonAppService.GetHistoryResultList(searchClass);
                    Reload(HisResult);
                    gridControlHisResult.DataSource = HisResult;
                }

            }

        }
        /// <summary>
        /// 刷新历史对比
        /// </summary>
        private void Reload(List<HistoryResultMainDto> input)
        {
            AutoLoading(() =>
            {

                //删除列
                if (CustomColumns != null && CustomColumns.Count > 0)
                {
                    foreach (var con in OldColumns)
                    {
                        if (gridViewHis.Columns.Contains(con))
                        {
                            gridViewHis.Columns.Remove(con);
                        }
                    }
                    OldColumns.Clear();
                    CustomColumns.Clear();
                }

                var itemname = input.SelectMany(p => p.historyResultDetailDtos).Select(p => new { p.CheckDate, p.CustomerBM }).Distinct().OrderBy(
                    p => p.CheckDate).ToList();

                foreach (var depar in itemname)
                {
                    var column = new GridColumn();
                    column.FieldName = $"gridColumnCustom{depar.CustomerBM}";
                    column.Name = $"{depar.CustomerBM}";
                    column.Caption = "(" + depar.CheckDate.Value.ToShortDateString() + ")";
                    if (!CustomColumns.Any(o => o.Key == column.Name))
                    {
                        var customColumn = new CustomColumnValue { Text = column.Name };
                        CustomColumns.Add(column.Name, customColumn);
                    }
                    column.Visible = true;
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    column.ColumnEdit = memoEdit;
                    //column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    column.SummaryItem.FieldName = "Name";
                    column.SummaryItem.DisplayFormat = "{0:c}";
                    column.SummaryItem.Tag = column.Name;
                    column.Width = 100;

                    gridViewHis.Columns.Add(column);
                    OldColumns.Add(column);
                }
                gridColumn41.VisibleIndex = gridViewHis.Columns.Count + 1;



            });

        }

        private void gridViewHis_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (e.ListSourceRowIndex < 0)
                {
                    return;
                }
                if (gridControlHisResult.DataSource is List<HistoryResultMainDto> rows)
                {
                    if (rows.Count > e.ListSourceRowIndex)
                    {

                        var itemValue = rows[e.ListSourceRowIndex].historyResultDetailDtos
                            ?.Where(r => r.CustomerBM == CustomColumns[e.Column.Name].Text)
                            .Select(r => new { r.ItemValue, r.Symbol }).ToList();
                        foreach (var i in itemValue)
                        {
                            if (i != null && i.ItemValue != null)
                            {
                                string bs = "";
                                if (i.Symbol == "H")
                                {
                                    //string name=

                                    bs = "↑";
                                }
                                else if (i.Symbol == "L")
                                {

                                    bs = "↓";
                                }
                                else if (i.Symbol == "HH")
                                {

                                    bs = "↑↑";
                                }
                                else if (i.Symbol == "LL")
                                {

                                    bs = "↓↓";
                                }
                                else
                                {

                                    bs = "";


                                }
                                e.DisplayText = i.ItemValue.ToString() + bs;
                            }

                        }



                    }
                }
            }
        }

        private void gridViewHis_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (CustomColumns.ContainsKey(e.Column.Name))
            {
                if (e.DisplayText.Contains("↑↑"))
                {

                    e.Appearance.BackColor = Color.Red;
                }
                else if (e.DisplayText.Contains("↓↓"))
                { e.Appearance.BackColor = Color.Blue; }
                else if (e.DisplayText.Contains("↑"))
                { e.Appearance.BackColor = Color.Salmon; }
                else if (e.DisplayText.Contains("↓"))
                { e.Appearance.BackColor = Color.SkyBlue; }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
        }

        #endregion

        private void layoutControl2_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel1_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanel1.HideSliding();
            e.Cancel = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEditPart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditPart.Checked == true)
            {
                checkHas.Checked = false;
                checkEditRyWeiJianRenShu.Checked = false;
            }
        }

        private void butRY_Click(object sender, EventArgs e)
        {
            #region 人脸识别
            //调用程序：
            var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
            }
            string args = "人脸识别";
            var path = AppDomain.CurrentDomain.BaseDirectory + "人证核验";
            Process KHMsg = new Process();
            KHMsg.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
            KHMsg.StartInfo.Arguments = args;
            KHMsg.StartInfo.RedirectStandardOutput = true;
            KHMsg.StartInfo.UseShellExecute = false;
            KHMsg.Start();
            //KHMsg.WaitForExit(60000);
            while (!KHMsg.HasExited)
            {
            } //如果exe还没关闭，则等待
            if (KHMsg.ExitCode != 1)
            {
                MessageBox.Show("人脸识别失败！");
            }
            else
            {
                string xmldata = KHMsg.StandardOutput.ReadToEnd();//读取exe中内存流数据

                //自己实现的序列化
                var ss = xmldata.Replace("\r\n", "$");
                var pic = ss.Split('$').ToList();
                var picnow = pic.FirstOrDefault(p => p.Contains("@"));
                if (!string.IsNullOrEmpty(picnow))
                {
                    picnow = picnow.Replace("@", "");
                    LoadCurrentCustomerReg(picnow);

                }
            }

            #endregion           
        }

        private void searchHuanZheXinXi_Click(object sender, EventArgs e)
        {
            searchHuanZheXinXi.SelectAll();
        }
    }


    public class GridZhanShiLei
    {
        /// <summary>
        /// 组合主键
        /// </summary>
        public Guid ZuHeId { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string ZuHeMingCheng { get; set; }

        /// <summary>
        /// 组合状态
        /// </summary>
        public int? ZuHeZhuangTai { get; set; }

        /// <summary>
        /// 组合顺序
        /// </summary>
        public int? ZuHeShunXu { get; set; }

        /// <summary>
        /// 项目主键
        /// </summary>
        public Guid XiangMuId { get; set; }

        /// <summary>
        /// 项目编码主键
        /// </summary>
        public Guid XiangMuBMId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string XiangMuMingCheng { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemBM { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string XiangMuZhuangTai { get; set; }

        /// <summary>
        /// 项目结果
        /// </summary>
        public string XiangMuJieGuo { get; set; }

        /// <summary>
        /// 项目结果
        /// </summary>
        public string XiangMuZD { get; set; }

        /// <summary>
        /// 项目参考值
        /// </summary>
        public string XiangMuCanKaoZhi { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>
        public string XiangMuDanWei { get; set; }

        /// <summary>
        /// 危急值状态 1危急值2正常
        /// </summary>
        public int? XiangMuWeiJiZhi { get; set; }

        /// <summary>
        /// 危急值类别A类B类
        /// </summary>
        public int? WeiJiZhiLeibie { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>
        public string XiangMuBiaoShi { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int? ProcessState { get; set; }

    }
    /// <summary>
    /// 鼠标操作相关类
    /// </summary>
    public class MouseFlag
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        [Flags]
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        /// <summary>
        /// 模拟鼠标单击事件
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="data"></param>
        public static void MouseLefDownEvent(int dx, int dy, uint data)
        {
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, dx, dy, data, UIntPtr.Zero);
        }

        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("User32")]
        public extern static bool GetCursorPos(ref Point lpPoint);

    }


}
 
