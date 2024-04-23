using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using gregn6Lib;
using HealthExaminationSystem.Enumerations.Helpers;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BarSetting;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using WindowsFormsApp1.DAL.YX;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmBarPrint : UserBaseForm
    {
        #region 构造函数
        public FrmBarPrint()
        {
            InitializeComponent();
        }
        #endregion

        #region 申明变量
        /// <summary>
        /// 否弹出打印条码框
        /// </summary>
        public bool IsPrintShowDialog;

        private ICustomerAppService customerSvr; //体检预约 

        private IBarSettingAppService BarSettingAppService;

        private IBarPrintAppService barPrintAppService;

        private ICustomerAppService customerAppService;

        private IBarPrintInfoItemGroupAppService barPrintInfoItemGroupAppService;

        private readonly GridppReport ReportMain = new GridppReport();

        private CustomerRegEssentialInfoViewDto customerEssentialInfoViewDto;

        private IIDNumberAppService iIDNumberAppService;

        private List<CustomerBarPrintInfoItemDto> lstCustomerBarPrintInfoItemDto;

        /// <summary>
        /// 已打印条码id集合
        /// </summary>
        public List<int> lstiPrint;

        /// <summary>
        /// 打印体检人
        /// </summary>
        public CusNameInput cusNameInput;

        public CustomerRegsViewDto cusinfo;

        /// <summary>
        /// 绑定未打印数据
        /// </summary>
        private List<CustomerBarPrintInfoDto> lstCustomerBarPrintInfoDtoNot = new List<CustomerBarPrintInfoDto>();

        /// <summary>
        /// 绑定已打印数据
        /// </summary>
        private List<CustomerBarPrintInfoDto> lstCustomerBarPrintInfoDto = new List<CustomerBarPrintInfoDto>();
        private readonly ICommonAppService _commonAppService  = new CommonAppService();
        #endregion

        #region 系统事件
        #region 退出
        private void btnClose_Click(object sender, EventArgs e)
        {

            Close();
        }
        #endregion 退出

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                BarPrint();
                //if (IsPrintShowDialog == false)
                //    BindBarPrintData();
            }
            finally
            {
                stopwatch.Stop();
                var mm = stopwatch.ElapsedMilliseconds;
                this.Close();
            }
        }
        #endregion 打印

        #region 系统加载
        private void FrmBarPrint_Load(object sender, EventArgs e)
        {
            #region 实现变量
            customerSvr = new CustomerAppService();
            lstiPrint = new List<int>();
            BarSettingAppService = new BarSettingAppService();
            barPrintAppService = new BarPrintAppService();
            customerAppService = new CustomerAppService();
            barPrintInfoItemGroupAppService = new BarPrintInfoItemGroupAppService();
            iIDNumberAppService = new IDNumberAppService();
            lstCustomerBarPrintInfoItemDto = new List<CustomerBarPrintInfoItemDto>();
            #endregion

            //读取系统参数获取是否弹出打印条码框 待完善
            //IsPrintShowDialog = true;

            #region 设置未打印样式
            //设置复选框
            GrdvNoPrint.OptionsSelection.MultiSelect = true;
            GrdvNoPrint.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            GrdvNoPrint.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            GrdvNoPrint.OptionsView.ShowIndicator = false; //不显示指示器
            GrdvNoPrint.OptionsBehavior.ReadOnly = false;
            GrdvNoPrint.OptionsBehavior.Editable = false;
            var Barid = GrdvNoPrint.Columns.AddField("Id");
            Barid.Visible = false;
            Barid.Caption = "条码ID";
            var BarName = GrdvNoPrint.Columns.AddField("BarSettings.BarName");
            BarName.Visible = true;
            BarName.Caption = "条码名称";
           // BarName.SummaryItem.DisplayFormat = "共计{0}";
            BarName.SummaryItem.SummaryType = SummaryItemType.Count;
            var BarItemGroupName = GrdvNoPrint.Columns.AddField("BarName");
            BarItemGroupName.Visible = true;
            BarItemGroupName.Caption = "组合名称";         
        
            var Sampletype = GrdvNoPrint.Columns.AddField("BarSettings.Sampletype");
            Sampletype.Visible = true;
            Sampletype.Caption = "标本类型";
            var Remarks = GrdvNoPrint.Columns.AddField("BarSettings.Remarks");
            Remarks.Visible = true;
            Remarks.Caption = "容器";
            var BarPage = GrdvNoPrint.Columns.AddField("BarSettings.BarPage");
            BarPage.Visible = true;
            BarPage.Caption = "打印个数";
            var BarNUM = GrdvNoPrint.Columns.AddField("BarSettings.BarNUM");
            BarNUM.Visible = false;
            BarNUM.Caption = "打印方式";
            #endregion

            #region 设置已打印
            grdvPrint.OptionsSelection.MultiSelect = true;
            grdvPrint.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            grdvPrint.OptionsView.ShowIndicator = false; //不显示指示器
            grdvPrint.OptionsBehavior.ReadOnly = false;
            grdvPrint.OptionsBehavior.Editable = false;
            var BarNamep = grdvPrint.Columns.AddField("BarSettings.BarName");
            BarNamep.Visible = true;
            BarNamep.Caption = "条码名称";
            BarNamep.SummaryItem.SummaryType = SummaryItemType.Count;
            //BarNamep.SummaryItem.DisplayFormat = "共计{0}";
            var BarNumBM = grdvPrint.Columns.AddField("BarNumBM");
            BarNumBM.Visible = true;
            BarNumBM.Caption = "条码号";
          
            var BarItemGroupNamep = grdvPrint.Columns.AddField("BarName");
            BarItemGroupNamep.Visible = true;
            BarItemGroupNamep.Caption = "组合名称";
            var Sampletypep = grdvPrint.Columns.AddField("BarSettings.Sampletype");
            Sampletypep.Visible = true;
            Sampletypep.Caption = "标本类型";
            var Remarksp = grdvPrint.Columns.AddField("BarSettings.Remarks");
            Remarksp.Visible = true;
            Remarksp.Caption = "容器";
            var BarPagep = grdvPrint.Columns.AddField("BarPrintCount");
            BarPagep.Visible = true;
            BarPagep.Caption = "打印次数";
            var CollectionState = grdvPrint.Columns.AddField("CollectionState");
            CollectionState.Visible = true;
            CollectionState.Caption = "核收状态";
            #endregion

            //获取基本信息
            //cusNameInput = new CusNameInput();
            //  cusNameInput.Id = new Guid("60160C06-9C0E-4E4A-BBC7-046FE74818E3");
            cusNameInput.Theme = "1";

            try
            {
                customerEssentialInfoViewDto = customerSvr.GetCustomerRegEssentialInfoViewDto(cusNameInput);
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }

            //绑定已打印数据
            BindBarPrintData();
            if (IsPrintShowDialog == true)
            {
                GrdvNoPrint.SelectAll();
                BarPrint();

                this.DialogResult = DialogResult.OK;
            }

        }
        #endregion

        #region 作废
        private void btnvoid_Click(object sender, EventArgs e)
        {
            try
            {
                var lstcustomerBarPrintInfoDtos = new List<CustomerBarPrintInfoDto>();
                var lstcustomerBarPrintInfoDtosPrint = (List<CustomerBarPrintInfoDto>)grdvPrint.DataSource;

                //获取选择已打印
                var rownumberprint = grdvPrint.GetSelectedRows(); //获取选中行号；
                foreach (var item in rownumberprint)
                {
                    var customerBarPrintInfoDto = new CustomerBarPrintInfoDto();
                    var var = lstcustomerBarPrintInfoDtosPrint[item];
                    lstcustomerBarPrintInfoDtos.Add(var);
                }

                foreach (var item in lstcustomerBarPrintInfoDtos)
                {
                    var createBarPrintInfoItemGroupDto = new CreateBarPrintInfoItemGroupDto();
                    createBarPrintInfoItemGroupDto.BarPrintInfoid = item.Id;
                    barPrintInfoItemGroupAppService.DeleteBarPrintInfoItemGroupApp(createBarPrintInfoItemGroupDto);
                    barPrintAppService.DeleteBarPrintApp(item);
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = customerEssentialInfoViewDto.CustomerBM;
                    createOpLogDto.LogName = customerEssentialInfoViewDto.Customer.Name;
                    createOpLogDto.LogText = "作废条码：" + item.BarNumBM;
                    createOpLogDto.LogDetail = item.BarName;
                    createOpLogDto.LogType = (int)LogsTypes.PrintId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                #region 条码推送
                var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 3)?.Remarks;
                var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                {


                    //获取选择未打印
                    string BMList =string.Join(",", lstcustomerBarPrintInfoDtos.Select(p=>p.BarNumBM).ToList());


                    BMList = BMList.Trim(',');
                    NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                    jfyt.GetSqnasync(customerEssentialInfoViewDto.CustomerBM + "|" + DJTSCJ + "|" + BMList +"|作废" , true); //条码推送

                }
                #endregion
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }

            //绑定已打印数据
            BindBarPrintData();
        }
        #endregion
        #endregion

        #region 公共方法
        #region 绑定条码列
        private void BindBarPrintData()
        {
            lstCustomerBarPrintInfoDto = GetPrint();
            lstCustomerBarPrintInfoDtoNot = GetNotPrint();
        }
        #endregion

        #region 填充Grid++数据
        private int ReportMain_ReportFetchRecord()
        {
               int outBM = 1;

                var lstcustomerBarPrintInfoDtosall = new List<CustomerBarPrintInfoDto>();
                var lstcustomerBarPrintInfoDtos = (List<CustomerBarPrintInfoDto>)GrdNoPrint.DataSource;

                //获取选择未打印
                var rownumber = GrdvNoPrint.GetSelectedRows(); //获取选中行号；
                foreach (var item in rownumber)
                {
                    var var2 = lstcustomerBarPrintInfoDtos[item]; //根据行号获取相应行的数据；
                    lstcustomerBarPrintInfoDtosall.Add(var2);
                }

                var lstcustomerBarPrintInfoDtosPrint = (List<CustomerBarPrintInfoDto>)grdvPrint.DataSource;

                //获取选择已打印
                var rownumberprint = grdvPrint.GetSelectedRows(); //获取选中行号；
                foreach (var item in rownumberprint)
                {
                    var customerBarPrintInfoDto = new CustomerBarPrintInfoDto();
                    var var = lstcustomerBarPrintInfoDtosPrint[item];
                    lstcustomerBarPrintInfoDtosall.Add(var);
                }

                var commonAppService = new CommonAppService();

                if (IsPrintShowDialog == true && lstcustomerBarPrintInfoDtosall.Count <= 0)
                {
                    alertControl1.Show(this, "提示!", customerEssentialInfoViewDto.Customer.Name + " 没有可打条码!");
                }
                List<CreateCustomerBarPrintInfoDto> _CreateCustomerBarPrintInfoDto = new List<CreateCustomerBarPrintInfoDto>(); //条码记录暂存集合
                List<CreateBarPrintInfoItemGroupDto> _CreateBarPrintInfoItemGroupDto = new List<CreateBarPrintInfoItemGroupDto>();  //条码组合记录暂存集合
                List<CustomerBarPrintInfoDto> _UpdateBarPrintDto = new List<CustomerBarPrintInfoDto>();  //需要更新的条码记录暂存集合
                var NewTime = commonAppService.GetDateTimeNow().Now;
                var reportJsonForExamine = new ReportJson();
                reportJsonForExamine.Detail = new List<Detail>();
                foreach (var item in lstcustomerBarPrintInfoDtosall.OrderBy(n => n.BarSettings?.OrderNum).ToList())
                {
                    if (item.BarSettings == null)
                    {
                        MessageBox.Show(item.BarName + "条码设置已删除，不能打印！");
                        return 0;
                    }
                    var barnum = "";
                    if (string.IsNullOrEmpty(item.BarNumBM))
                    {
                        barnum = item.BarSettings.StrBar + CreateBarNum(item.BarSettings.BarNUM.Value);
                        item.BarNumBM = barnum;
                        item.BarPrintTime = NewTime;

                        #region 生成条码记录
                        try
                        {
                            var var = from p in lstCustomerBarPrintInfoItemDto where p.Id == item.Id select p;
                            var lstdto = var.ToList();
                            if (lstdto.Count > 0)
                            {
                                lstdto[0].BarNumBM = barnum;
                                lstdto[0].BarPrintTime = NewTime;

                                var createCustomerBar = new CreateCustomerBarPrintInfoDto();
                                createCustomerBar.BarName = item.BarName;
                                createCustomerBar.custtomerregid = cusNameInput.Id;
                                createCustomerBar.BarNumBM = item.BarNumBM;
                                createCustomerBar.BarPrintCount = item.BarPrintCount;
                                createCustomerBar.BarPrintTime = item.BarPrintTime;
                                createCustomerBar.BarSettingsId = item.BarSettings.Id;
                                createCustomerBar.Id = Guid.NewGuid();
                                //var cubar = barPrintAppService.AddBarPrintApp(createCustomerBar);
                                _CreateCustomerBarPrintInfoDto.Add(createCustomerBar);

                                //生成打印条码项目记录 CustomerBarPrintInfoItemDto
                                var varitemgroup = from p in lstCustomerBarPrintInfoItemDto where p.Id == item.Id select p;
                                var lstitemgroup = varitemgroup.ToList();
                                foreach (var itemgp in lstitemgroup[0].CustomerBarPrintInfo)
                                {
                                    var createBarPrintInfoItemGroupDto = new CreateBarPrintInfoItemGroupDto();
                                    createBarPrintInfoItemGroupDto.BarPrintInfoid = createCustomerBar.Id;
                                    createBarPrintInfoItemGroupDto.ItemGroupid = itemgp.ItemGroup.Id;
                                    createBarPrintInfoItemGroupDto.ItemGroupName = itemgp.ItemGroupName;
                                    createBarPrintInfoItemGroupDto.ItemGroupNameAlias = itemgp.ItemGroupNameAlias;
                                    //barPrintInfoItemGroupAppService.AddBarPrintInfoItemGroupApp(createBarPrintInfoItemGroupDto);
                                    _CreateBarPrintInfoItemGroupDto.Add(createBarPrintInfoItemGroupDto);
                                }
                            }
                        }
                        catch (UserFriendlyException exception)
                        {
                            ShowMessageBox(exception);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 更新记录
                        try
                        {
                            item.BarPrintTime = NewTime;
                            item.BarPrintCount = item.BarPrintCount + 1;
                            //barPrintAppService.UpdateBarPrintApp(item);
                            _UpdateBarPrintDto.Add(item);
                        }
                        catch (UserFriendlyException exception)
                        {
                            ShowMessageBox(exception);
                        }
                        #endregion
                    }

                    item.BarPrintTime = NewTime;

                    for (var i = 0; i < item.BarSettings.BarPage; i++)
                    {
                        var detail = new Detail();
                        detail.CustomerExaminationNumber = item.BarNumBM;
                        string strname = customerEssentialInfoViewDto.Customer.Name + " " +
                            SexHelper.CustomSexFormatter(customerEssentialInfoViewDto.Customer.Sex) + " " +
                            customerEssentialInfoViewDto.Customer.Age.ToString();
                        //  + " " + MarrySateHelper.CustomMarrySateFormatter(customerEssentialInfoViewDto.Customer.MarriageStatus);
                        detail.CustomerName = strname;
                        detail.ItemGroupName = item.BarName;
                        if (item.BarSettings.testType.HasValue)
                        {
                            int tevalue = item.BarSettings.testType.Value;
                            var textvaluels = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.TestType);
                            detail.TestType = textvaluels.FirstOrDefault(o => o.Value == tevalue).Text;
                        }
                        detail.Colour = item.BarSettings.TubeColor;
                    if (customerEssentialInfoViewDto.ClientInfo != null)
                    {
                        detail.ClientInfoName = customerEssentialInfoViewDto.ClientInfo.ClientName;
                       // detail.ParameterReceiving = "单位领取";
                    }
                    else
                    {
                       // ParameterReceiving = "自取";
                    }
                    detail.CustomerBM = customerEssentialInfoViewDto.CustomerBM;
                    detail.ClientRegNum = customerEssentialInfoViewDto.ClientRegNum;
                    detail.CustomerRegNum = customerEssentialInfoViewDto.CustomerRegNum;
                    detail.PrimaryName = customerEssentialInfoViewDto.PrimaryName;
                    reportJsonForExamine.Detail.Add(detail);
                    }

                }
            if (reportJsonForExamine.Detail.Count > 0)
            {
                var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonForExamine);
                ReportMain.LoadDataFromXML(reportJsonForExamineString);
            }
            else
            {
                outBM = 2;
            }



                //数据插入
                if (_CreateCustomerBarPrintInfoDto.Count > 0)
                    barPrintAppService.AddBarPrintApp(_CreateCustomerBarPrintInfoDto);
                if (_CreateBarPrintInfoItemGroupDto.Count > 0)
                    barPrintInfoItemGroupAppService.AddBarPrintInfoItemGroupApp(_CreateBarPrintInfoItemGroupDto);
                if (_UpdateBarPrintDto.Count > 0)
                    barPrintAppService.UpdateBarPrintApp(_UpdateBarPrintDto);
                return outBM;                      
        }
        private ReportJsonDto prinall(CusNameInput cusNameInput)
        {
            if (barPrintAppService == null)
            {
                barPrintAppService = new BarPrintAppService(); 
            }
           // cusNameInput.Theme = "1";
           //获取所有条码
            var lstcustomerBarPrintInfoDtosall = barPrintAppService.GetBarPrint(cusNameInput);

            var reportJsonForExamine = lstcustomerBarPrintInfoDtosall;            
            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonForExamine);
            ReportMain.LoadDataFromXML(reportJsonForExamineString);

            return lstcustomerBarPrintInfoDtosall;

        }
        #endregion

        #region 生成条码号
        public string CreateBarNum(int itype)
        {
            var barnum = "";
            switch (itype)
            {
                case 1:
                    barnum = customerEssentialInfoViewDto.CustomerBM;
                    break;
                case 2:
                    barnum = iIDNumberAppService.CreateBarBM();
                    break;
                case 3:
                    barnum = iIDNumberAppService.CreateBarBM();
                    break;
            }

            return barnum;
        }
        #endregion

        #region 绑定未打印数据
        public List<CustomerBarPrintInfoDto> GetNotPrint()
        {
            //生成未打印条码
            var lstbar = new List<CustomerBarPrintInfoDto>();
            try
            {
                //获取全部项目
                var lstcustomerItemGroupBarItems = customerAppService.GetLstCustomerItemGroupBarItemDto(cusNameInput);
                var fqxs = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o =>
      o.Type == BasicDictionaryType.BarPrintSet.ToString() && o.Value == 77)?.Remarks;
                if (!string.IsNullOrEmpty(fqxs) && fqxs == "1")
                {
                    lstcustomerItemGroupBarItems = lstcustomerItemGroupBarItems.Where(r => r.CheckState != (int)ProjectIState.GiveUp).ToList();
                }

                //获取已打印组合
                var lstBarPrintInfoItemGroupQueryDto =
                    barPrintInfoItemGroupAppService.GetLstBarPrintInfoItemGroupApp(cusNameInput);
                var searchBarItemDto = new SearchBarItemDto();
                var lstall = new List<Guid>();
                var lstprint = new List<Guid>();

                //获取未打印组合
                //1.获取已打印组合id
                foreach (var item in lstBarPrintInfoItemGroupQueryDto)
                {
                    lstprint.Add(item.ItemGroup.Id);
                }
                //2.获取所有组合id
                foreach (var item in lstcustomerItemGroupBarItems)
                {
                    if (!lstprint.Contains(item.ItemGroupBM_Id) && !lstall.Contains(item.ItemGroupBM_Id))
                    {
                        lstall.Add(item.ItemGroupBM_Id);
                    }
                }

                searchBarItemDto.AllItemGroupID = lstall;

                //根据未打印组合获取打印条码组合对象
                var lstbarItembViewDtos = BarSettingAppService.GetBarItemGroupFulls(searchBarItemDto);
                var lstBarSet = new List<CustomerBarPrintInfoDto>();
                var lstbarSettingDtos = new List<BarSettingDto>();

                //过滤重复找到需打印条码设置
                foreach (var item in lstbarItembViewDtos)
                {
                    if (item.BarSetting != null)
                    {
                        var itemsett = from c in lstbarSettingDtos where c.Id == item.BarSetting.Id select c;
                        if (itemsett.Count() <= 0)
                        {
                            lstbarSettingDtos.Add(item.BarSetting);
                        }
                    }

                }
                //遍历 需要打印的条码，并添加条码下的项目
                foreach (var item in lstbarSettingDtos)
                {
                    var customerBarPrintInfo = new CustomerBarPrintInfoDto();
                    customerBarPrintInfo.Id = Guid.NewGuid();
                    customerBarPrintInfo.BarSettings = item;
                    customerBarPrintInfo.BarPrintCount = 1;
                    var strbarItemGroupName = "";
                    var baritemGroup = from c in lstbarItembViewDtos where item.Id == c.BarSetting.Id orderby c.ItemGroup.OrderNum select c;
                    Guid nn = lstbarItembViewDtos[0].BarSetting.Id;
                    Guid mm = item.Id;
                    if (nn == mm)
                    {

                    }
                    //var baritemGroup = lstbarItembViewDtos.Select(r => (Guid)r.BarSetting.Id == (Guid)item.Id);
                    var customerBarPrintInfoItemDto = new CustomerBarPrintInfoItemDto();
                    customerBarPrintInfoItemDto.CustomerBarPrintInfo = new List<BarPrintInfoItemGroupDto>();
                    List<BarItembViewDto> lstbaritem = new List<BarItembViewDto>();
                    foreach (var baritem in baritemGroup)
                    {

                        var barPrintInfoItemGroupDto = new BarPrintInfoItemGroupDto();
                        barPrintInfoItemGroupDto.BarPrintInfo = customerBarPrintInfo;
                        barPrintInfoItemGroupDto.Id = Guid.NewGuid();
                        barPrintInfoItemGroupDto.ItemGroupName = baritem.ItemGroup.ItemGroupName;
                        barPrintInfoItemGroupDto.ItemGroup = baritem.ItemGroup;
                        barPrintInfoItemGroupDto.ItemGroupNameAlias = baritem.ItemGroupAlias;
                        strbarItemGroupName += baritem.ItemGroupAlias + "+";
                        customerBarPrintInfoItemDto.CustomerBarPrintInfo.Add(barPrintInfoItemGroupDto);
                    }

                    customerBarPrintInfo.BarName = strbarItemGroupName.TrimEnd('+');
                    customerBarPrintInfoItemDto.BarName = customerBarPrintInfo.BarName;
                    customerBarPrintInfoItemDto.Id = customerBarPrintInfo.Id;
                    customerBarPrintInfoItemDto.BarNumBM = customerBarPrintInfo.BarNumBM;
                    customerBarPrintInfoItemDto.BarPrintCount = customerBarPrintInfo.BarPrintCount;
                    customerBarPrintInfoItemDto.BarPrintTime = customerBarPrintInfo.BarPrintTime;
                    customerBarPrintInfoItemDto.BarSettings = customerBarPrintInfo.BarSettings;
                    lstCustomerBarPrintInfoItemDto.Add(customerBarPrintInfoItemDto);
                    lstbar.Add(customerBarPrintInfo);
                }

                GrdNoPrint.DataSource = lstbar.OrderBy(n => n.BarSettings.OrderNum).ToList();
                GrdvNoPrint.SelectAll();
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }

            return lstbar;
        }
        #endregion

        #region 绑定已打印数据
        public List<CustomerBarPrintInfoDto> GetPrint()
        {
            var customerBarPrintInfoDtos = new List<CustomerBarPrintInfoDto>();
            try
            {
                customerBarPrintInfoDtos = barPrintAppService.GetLstBarPrintApp(cusNameInput);

                //grdPrint.DataSource = customerBarPrintInfoDtos;
                grdPrint.DataSource = customerBarPrintInfoDtos.OrderBy(n => n.BarSettings?.OrderNum).ToList();
                grdvPrint.ClearSelection();
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }

            return customerBarPrintInfoDtos;
        }
        #endregion 绑定已打印数据

        #region 打印条码
        public void BarPrint()
        {
            ReportMain.Clear();
            //var strBarPrintName = CacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 10).Remarks;
            var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1000).Remarks;
            var StrPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 20).Remarks;
            //var GrdPath = GridppHelper.GetTemplate(strBarPrintName + "1.grf");
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 20)?.Remarks;
            if (!string.IsNullOrEmpty(printName) && string.IsNullOrEmpty(StrPrintName))
            {
                StrPrintName = printName;
            }
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            ReportMain.LoadFromURL(GrdPath);
            ReportMain.Printer.PrinterName = StrPrintName;
           var isok=  ReportMain_ReportFetchRecord();
            if (isok !=0)
            {
                if (isok == 1)
                {
                    //临时
                    ReportMain.Print(false);
                   // ReportMain.PrintPreview(false);
                }
                //更新条码打印状态
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = customerEssentialInfoViewDto.Id;
                chargeBM.Name = "条码";
                barPrintAppService.UpdateCustomerRegisterPrintState(chargeBM);

            }
            //给病理发送数据
            var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
            if (HISjk == "1")
            {
                var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                if (HISName == "东软")
                {
                    NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                    neInterface.GetCreExmAsyncbl(customerEssentialInfoViewDto.CustomerBM);

                }
            }
            #region 条码推送
            var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 3)?.Remarks;
            var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
            if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
            {
                var lstcustomerBarPrintInfoDtos = (List<CustomerBarPrintInfoDto>)GrdNoPrint.DataSource;
                var lstcustomerBarPrintInfoDtosPrint = (List<CustomerBarPrintInfoDto>)grdvPrint.DataSource;

                //获取选择未打印
                string BMList = "";
                var rownumber = GrdvNoPrint.GetSelectedRows(); //获取选中行号；
                foreach (var item in rownumber)
                {
                    var var2 = lstcustomerBarPrintInfoDtos[item]; //根据行号获取相应行的数据；
                    BMList += var2.BarNumBM + ",";
                }
                //获取选择已打印
                var rownumberprint = grdvPrint.GetSelectedRows(); //获取选中行号；
                foreach (var item in rownumberprint)
                {
                    var customerBarPrintInfoDto = new CustomerBarPrintInfoDto();
                    var var = lstcustomerBarPrintInfoDtosPrint[item];
                    BMList += var.BarNumBM + ",";
                }
                BMList = BMList.Trim(',');
                NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                jfyt.GetSqnasync(customerEssentialInfoViewDto.CustomerBM + "|" + DJTSCJ +"|" + BMList, true); //条码推送

            }
            #endregion

            //var reportFileName = CacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 10).Remarks;
            //var reportFileName = "条码-1列1.grf";
            //var printName = CacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 20).Remarks;

        }
        /// <summary>
        /// 单位批量打条码
        /// </summary>
        public void BarPrintAll(List<CusNameInput> cusNameInputls)
        { //苏州鑫
          
            foreach (var cusNameInput in cusNameInputls)
            {
                ReportMain.Clear();
                var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1000).Remarks;
                var StrPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 20).Remarks;
                //打印机设置
                var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 20)?.Remarks;
                if (!string.IsNullOrEmpty(printName) && string.IsNullOrEmpty(StrPrintName))
                {
                    StrPrintName = printName;
                }
                var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
                ReportMain.LoadFromURL(GrdPath);
                ReportMain.Printer.PrinterName = StrPrintName;                
              var tmlist=  prinall(cusNameInput);
                //ReportMain_ReportFetchRecord();
                ReportMain.Print(false);
                //更新条码打印状态
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Id = cusNameInput.Id;
                chargeBM.Name = "条码"; 
                barPrintAppService.UpdateCustomerRegisterPrintState(chargeBM);
                if (!string.IsNullOrEmpty(cusNameInput.CusRegBM))
                {
                    //给病理发送数据
                    var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
                    if (HISjk == "1")
                    {
                        var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                        if (HISName == "东软")
                        {
                       
                            NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                            neInterface.GetCreExmAsyncbl(cusNameInput.CusRegBM);
                            

                        }
                    }
                    #region 条码推送
                    var DJTS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 3)?.Remarks;
                    var DJTSCJ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.DJTS, 2)?.Remarks;
                    if (!string.IsNullOrEmpty(DJTS) && DJTS == "1" && !string.IsNullOrEmpty(DJTSCJ))
                    {
                        //江苏鑫亿条码存在漏打改成同步
                        //if (DJTSCJ.Contains("江苏鑫亿"))
                        //{                            

                        //    TestExamRequest examRequest = new TestExamRequest();
                        //    examRequest.PrintBar(cusNameInput.CusRegBM, "");
                        //}
                        //else
                        //{
                            //获取选择未打印
                            string BMList = "";
                            var dy = barPrintAppService.GetLstBarPrintApp(cusNameInput);
                            if (dy != null && dy.Count > 0)
                            {
                                foreach (var item in dy)
                                {

                                    BMList += item.BarNumBM + ",";
                                }
                            }
                            NeusoftInterface.JinFengYiTong jfyt = new NeusoftInterface.JinFengYiTong();
                            jfyt.GetSqnasync(cusNameInput.CusRegBM + "|" + DJTSCJ + "|" + BMList, true); //条码推送
                        //}
                    }
                    #endregion
                }
            }
           
            if (splashScreenManager.IsSplashFormVisible)
            splashScreenManager.CloseWaitForm();
        }
        #endregion
        #endregion
    }
}