using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using Newtonsoft.Json;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonFormat;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet;
using Sw.Hospital.HealthExaminationSystem.UserSettings.SumHide;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class FrmInspectionTotal : UserBaseForm
    {
        private List<string> connamels = new List<string>();
        bool isfist = true;
        private readonly IPrintPreviewAppService _printPreviewAppService;
        private List<TjlCustomerRegForInspectionTotalDto> nextcus;
        private readonly ICommonAppService commonAppService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;
        public TjlCustomerQuery tjlCustomerQuery;
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        private OccTargetDiseaseByRisk _OccTargetDiseaseByRisk;
        private readonly IOccDiseaseAppService _OccDiseaseAppService;

        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        public bool isShow=false;
        private bool ry = false;
        private bool iszyb = false;
        public FrmInspectionTotal()
        {

            InitializeComponent();
            _printPreviewAppService = new PrintPreviewAppService();
            _tjlCustomerRegDto = null;

            _doctorStationAppService = new DoctorStationAppService();
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
            _pictureController = new PictureController();
            _PersonnelIndividuationConfigAppService = new PersonnelIndividuationConfigAppService();
            _calendarYearComparison = new HistoryComparisonAppService();
            customerSvr = new CustomerAppService();
            nextcus = new List<TjlCustomerRegForInspectionTotalDto>();
            commonAppService = new CommonAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            _OccTargetDiseaseByRisk = new OccTargetDiseaseByRisk();
            _OccDiseaseAppService = new OccDiseaseAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            ry = true;
        }
        public FrmInspectionTotal(TjlCustomerRegForInspectionTotalDto tjlCustomerRegDto,bool isyb=false)
        {

            InitializeComponent();
            _printPreviewAppService = new PrintPreviewAppService();
            _tjlCustomerRegDto = tjlCustomerRegDto;
            iszyb = isyb;
            _doctorStationAppService = new DoctorStationAppService();
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
            _pictureController = new PictureController();
            _PersonnelIndividuationConfigAppService = new PersonnelIndividuationConfigAppService();
            _calendarYearComparison = new HistoryComparisonAppService();
            customerSvr = new CustomerAppService();
            nextcus = new List<TjlCustomerRegForInspectionTotalDto>();
            commonAppService = new CommonAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            _OccTargetDiseaseByRisk = new OccTargetDiseaseByRisk();
            _OccDiseaseAppService = new OccDiseaseAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
        }
        //下一位体检完成
        private void getNext(int SumState=0)
        {
            if (nextcus.Count == 0)
            { 
                var input = new TjlCustomerQuery();
                if (tjlCustomerQuery != null)
                {
                    input = tjlCustomerQuery;
                }
                if (SumState != 0)
                {
                    input.SumState = SumState;
                }
                nextcus = _inspectionTotalService.GetReCustomerReg(input).ToList();
            }
            if (nextcus.Count > 0)
            {
                string str = nextcus[0].Customer.Name + ",单位：" + nextcus[0].ClientInfo?.ClientName;
                if (nextcus[0].PhysicalType.HasValue)
                {
                    var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                    var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                    var tjlb = Clientcontract.FirstOrDefault(o => o.Value == nextcus[0].PhysicalType)?.Text;
                    if (tjlb != null)
                    {
                        str += ",体检类别：" + tjlb;
                    }

                }
                DialogResult dr = XtraMessageBox.Show("是否切换下一位？\r\n" + str, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    _tjlCustomerRegDto = nextcus[0];
                    if (ry==true)
                    {
                        LoadData();
                        LoadDataState();
                        LoadSummarizeData();
                        LoadCrisisRegItem();

                        isfist = false;

                        var outresult = _OccHazardFactorAppService.getSimpOccHazardFactors();
                        repositoryItemLookUpEditrisk.DataSource = outresult;
                        nextcus.Remove(nextcus[0]);
                    }
                    else
                    {
                        object obj = new object();
                        EventArgs e = new EventArgs();
                        FrmInspectionTotal_Load(obj, e);
                        nextcus.Remove(nextcus[0]);
                    }
                }
            }
        }
        private void FrmInspectionTotal_Load(object sender, EventArgs e)
        {
            InitFormnew();         
            if (ry == true)
            {

                Text = "职业总检";
            }
            else
            {
                Text = $@"{Text} - {_tjlCustomerRegDto.Customer.Name}";
                if (_tjlCustomerRegDto.ClientInfo != null)
                {
                    Text = $@"{Text} - {_tjlCustomerRegDto.ClientInfo.ClientName}";
                }

                //InitForm();
                LoadData();
               
                LoadSummarizeData();
                LoadDataState();
                LoadCrisisRegItem();
            }
            //加载历史对比固定列
            for (int n = 0; n < gridViewContrast.Columns.Count; n++)
            {
                connamels.Add(gridViewContrast.Columns[n].Name);
            }
            isfist = false;

            var outresult = _OccHazardFactorAppService.getSimpOccHazardFactors();
            repositoryItemLookUpEditrisk.DataSource = outresult;
            if (isShow == true)
            {

                laysum.Visibility = LayoutVisibility.Never;

                laySave.Visibility = LayoutVisibility.Never;

                laySH.Visibility = LayoutVisibility.Never;

                layCancel.Visibility = LayoutVisibility.Never;

                layUP.Visibility = LayoutVisibility.Never;
                layReport.Visibility = LayoutVisibility.Never;

                layDy.Visibility = LayoutVisibility.Never;
                layHQ.Visibility = LayoutVisibility.Never;
                laygx.Visibility = LayoutVisibility.Never;
                layth.Visibility = LayoutVisibility.Never;
                laywz.Visibility = LayoutVisibility.Never;

            }
            string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 17)?.Remarks;
            if (!string.IsNullOrEmpty(strwjshow) && strwjshow == "Y" && isShow==false)
            {
                tabNavigationPage8.PageVisible = false;
                tabNavigationPage11.PageVisible = false;
               // conDescription.Visible = false;
                conMedicalAdvice.Visible = false;

                tabPane2.SelectedPageIndex = 2;

            }
        }

        #region 总检查询人员
        private void InitFormnew()
        {
            AutoLoading(() =>
            {
                gridViewCustomerReg.IndicatorWidth = 40;
                var dataDto = _commonAppService.GetDateTimeNow();
                textEditDayNum.Text = "7";
                dateEditStartTime.DateTime = dataDto.Now.Date.AddDays(-1);
                dateEditEndTime.DateTime = dataDto.Now.Date;
                gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.Format =
                    new CustomFormatter(CheckSateHelper.PrintSateFormatter);
                gridViewCustomerReg.Columns[conUploadState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCustomerReg.Columns[conUploadState.FieldName].DisplayFormat.Format =
                    new CustomFormatter(UploadStateHelper.UploadStateFormatter);


            }, Variables.LoadingForForm);
            gridLookJK.Properties.DataSource = UploadStateHelper.GetUploadStateModels();

            sleDW.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            lookUpEditSumStatus.EditValue = (int)SummSate.NotAlwaysCheck;

            // 加载体检类别数据
            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();

            // 加载体检状态数据
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            lookUpEditExaminationStatus.EditValue = (int)PhysicalEState.Complete;
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);
            lookUpEditExaminationCategories.Properties.DataSource = Examination;
            if (Examination.Count > 0)
            {
                lookUpEditExaminationCategories.EditValue = Examination[0].Value;
            }
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\ZInspection.json";
            if (File.Exists(fp))  // 判断是否已有相同文件 
            {
                var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fp));
                foreach (var tj in Search)
                {
                    if (tj.Name == "CheckType")
                    {
                        if (Examination.Any(p => p.Value == int.Parse(tj.Text)))
                        {
                            lookUpEditExaminationCategories.EditValue = int.Parse(tj.Text);
                        }
                    }
                    else if (tj.Name == "CheckState")
                    {
                        lookUpEditExaminationStatus.EditValue = int.Parse(tj.Text);
                    }
                    else if (tj.Name == "SumState")
                    {
                        lookUpEditSumStatus.EditValue = int.Parse(tj.Text);
                    }
                    else if (tj.Name == "Day")
                    {
                        textEditDayNum.Text = tj.Text;
                    }
                    else if (tj.Name == "Doctor")
                    {
                        txtsearchDoctor.EditValue = long.Parse(tj.Text);
                    }
                }
            }
            #region 报告模板
            // 加载打印模板
            var MBNamels = DefinedCacheHelper
                .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            string[] mbls = MBNamels.Split('|');
            List<string> list = new List<string>();
            list.Add("根据体检类别匹配");
            if (Variables.ISZYB != "2")
            {
                foreach (string mb in mbls)
                {
                    if (mb != "")
                    {
                        list.Add(mb);
                    }
                }
                // 加载对比报告模板
                var db = DefinedCacheHelper
                    .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1)?.Remarks;
                if (!string.IsNullOrEmpty(db))
                {
                    list.Add(db);

                }
            }
            // 加载职业健康报告模板
            var zybmb = DefinedCacheHelper
                .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2)?.Remarks;
            if (!string.IsNullOrEmpty(zybmb))
            {
                list.Add(zybmb);

            }
            listBoxControlTemplates.Properties.DataSource = list;
            if (list.Count > 0)
            {
                listBoxControlTemplates.EditValue = list[0];
            }
            #endregion
        }
        public void LoadDatanew()
        {
            AutoLoading(() =>
            {
                //var input = new PageInputDto<TjlCustomerQuery>
                var input = new TjlCustomerQuery()
                {
                    Name = textEditName.Text.Trim()
                };
                //if (checkEditNeiWai.Checked)
                //{
                //    input.IsGetNeiWaiKe = false;
                //}
                if (textEditName.Text.Trim() == string.Empty)
                {
                    if (txtsearchDoctor.EditValue != null && !string.IsNullOrWhiteSpace(txtsearchDoctor.EditValue.ToString()))
                    {
                        long[] DocIdArr = new long[1];
                        DocIdArr[0] = (long)txtsearchDoctor.EditValue;
                        input.arrEmployeeName_Id = DocIdArr;
                        if (comboEmp.Text == "总检医生")
                        { input.EmployeeNameType = 1; }
                    }
                    if (checkEditIsData.Checked)
                    {
                        if (dateEditStartTime.EditValue != null)
                            input.BeginDate = dateEditStartTime.DateTime;
                        if (dateEditEndTime.EditValue != null)
                            input.EndDate = dateEditEndTime.DateTime;
                        if (input.BeginDate > input.EndDate)
                        {
                            ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                            return;
                        }
                    }
                    else
                    {
                        if (textEditDayNum.Text.Trim() != string.Empty)
                        {
                            //int dayNum = Convert.ToInt32(textEditDayNum.Text.Trim());
                            //input.BeginDate = DateTime.Now.AddDays(-dayNum).Date;
                            //input.EndDate = DateTime.Now.Date;

                            int tian = int.Parse(textEditDayNum.Text.Trim());
                            tian--;
                            input.BeginDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-tian).ToString("yyyy-MM-dd") + " 00:00:00");
                            input.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 23:59:59");
                        }
                    }
                }


                //var output = _inspectionTotalService.PageFulls(input);
                //TotalPages = output.TotalPages;
                //CurrentPage = output.CurrentPage;
                //InitialNavigator(dataNavigator);

                //var output = _inspectionTotalService.GetInspectionTotalList(input);
                var examinationCategory = lookUpEditExaminationCategories.EditValue as int?;
                if (examinationCategory.HasValue && examinationCategory != -1)
                {
                    input.PhysicalType = examinationCategory;
                }
                if (sleDW.EditValue != null)
                {
                    input.ClientrRegID = (Guid)sleDW.EditValue;
                }
                if (lookUpEditSumStatus.EditValue != null && lookUpEditSumStatus.Text != "全部")
                {
                    input.SumState = (int)lookUpEditSumStatus.EditValue;
                }
                if (lookUpEditExaminationStatus.EditValue != null && lookUpEditExaminationStatus.Text != "全部")
                {
                    input.CheckState = (int)lookUpEditExaminationStatus.EditValue;
                }

                if (gridLookJK.EditValue != null && gridLookJK.Text != "全部")
                {
                    input.UpZybState = (int)gridLookJK.EditValue;
                }

                var output = _inspectionTotalService.GetCustomerRegList(input).Where(o => o.RegisterState == (int)RegisterState.Yes).ToList();
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);
                var zjcoutn = output.Count(o => o.SummSate == (int)SummSate.HasInitialReview);
                var SHcoutn = output.Count(o => o.SummSate == (int)SummSate.Audited);
                var tjzWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Process && o.SummSate == (int)SummSate.NotAlwaysCheck);
                var twcWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Complete && o.SummSate == (int)SummSate.NotAlwaysCheck);
                labelControl2.Text = "总人数：" + sum + "，已审核:" + SHcoutn + "，已总检：" + zjcoutn + "，体检完成未总检：" + twcWSh + "，体检中未总检：" + tjzWSh;
                gridControl.DataSource = output;
                gridViewCustomerReg.BestFitColumns();

                //绑定诊断搜索下拉框
                var userDto = DefinedCacheHelper.GetComboUsers();
                txtsearchDoctor.Properties.DataSource = userDto;
                txtsearchDoctor.Properties.ValueMember = "Id";
                txtsearchDoctor.Properties.DisplayMember = "Name";

            });
        }
        #region 仓储

        // 通用
        private readonly ICommonAppService _commonAppService;

        // 医生站
        private readonly IDoctorStationAppService _doctorStationAppService;

        // 总检
        private readonly IInspectionTotalAppService _inspectionTotalService;

        // 用户个性配置
        private readonly IPersonnelIndividuationConfigAppService _PersonnelIndividuationConfigAppService;

        //图像处理控制器
        private PictureController _pictureController;

        // 建议字典
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;

        #endregion 
        #endregion

        #region 全局对象
        /// <summary>
        /// 体检信息对象
        /// <para>列表页传进来</para>
        /// <para>用于初始化绑定</para>
        /// </summary>
        public  TjlCustomerRegForInspectionTotalDto _tjlCustomerRegDto;

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        private List<ATjlCustomerDepSummaryDto> _aTjlCustomerDepSummaryDto;

        /// <summary>
        /// 体检人项目集合
        /// </summary>
        private List<CustomerRegInspectItemDto> _customerRegItemList;

        /// <summary>
        /// 查询建议字典--初始化读取，后续生成总检使用
        /// </summary>
        private List<SummarizeAdviceDto> _summarizeAdviceFull = new List<SummarizeAdviceDto>();

        /// <summary>
        /// 总检后的总结结论
        /// </summary>
        private TjlCustomerSummarizeDto _customerSummarizeDto = new TjlCustomerSummarizeDto();

        /// <summary>
        /// 总检后的职业健康总检
        /// </summary>
        private InputOccCusSumDto _OcccustomerSummarizeDto = new InputOccCusSumDto();

        /// <summary>
        /// 诊断子窗体
        /// </summary>
        private TotalDiagnosis _totalDiagnosis;

        /// <summary>
        /// 保存匹配到的科室建议
        /// </summary>
        //private List<SummarizeAdviceDto> _matchingList = new List<SummarizeAdviceDto>();
        private List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();

        /// <summary>
        /// 当前患者所有检查图像
        /// </summary>
        List<CustomerItemPicDto> CustomerItemPicAllSys = new List<CustomerItemPicDto>();

        /// <summary>
        /// 当前患者当前所选项目图像
        /// </summary>
        List<CustomerItemPicDto> CustomerItemPicSys = new List<CustomerItemPicDto>();
        //项目图像数
        int CustomerPicSys = 0;

        private List<Guid> ListDeparmentNoSum;
        #endregion

        #region 公用方法

        /// <summary>
        /// 克隆对象/引用类型对象复制出一个新的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            var ret = default(T);
            if (obj != null)
            {
                var cloner = new XmlSerializer(typeof(T));
                var stream = new MemoryStream();
                cloner.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                ret = (T)cloner.Deserialize(stream);
            }

            return ret;
        }

        #endregion 

        //事件抛出列表窗体
        public event EventHandler SimpleButtonSaveClick;

        //界面初始化
        private void InitForm()
        {
            labelJianChaBianHao.Text = _tjlCustomerRegDto.CustomerBM;
            if (_tjlCustomerRegDto.ClientInfo != null)
            {
                labelControlDanWei.Text = _tjlCustomerRegDto.ClientInfo.ClientName;
            }
            else
            {
                labelControlDanWei.Text = "个人";
            }
            labelControlShiJian.Text = _tjlCustomerRegDto.LoginDate.ToString();
            labelXingMing.Text = _tjlCustomerRegDto.Customer.Name;
      
            labelXingBie.Text = SexHelper.CustomSexFormatter(_tjlCustomerRegDto.Customer.Sex);
            labelNianLing.Text = _tjlCustomerRegDto.Customer.Age.ToString();
            string csD = _tjlCustomerRegDto.CSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().Find(n => n.Id == _tjlCustomerRegDto.CSEmployeeId).Name;
            string fsD = _tjlCustomerRegDto.FSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().Find(n => n.Id == _tjlCustomerRegDto.FSEmployeeId).Name;
            labelShenHeYiSheng.Text = csD + "-" + fsD;
            //下拉列表禁用编辑
            customyiSheng.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;          
            customGridZhenDuan2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //职业健康
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            if (tjlb!=null && (Variables.ISZYB == "1" && 
                ((tjlb.Contains("职业") && !string.IsNullOrEmpty(_tjlCustomerRegDto.RiskS))
                 || tjlb.Contains("放射"))
               ) || Variables.ISZYB == "2")
            {
                labZYB.Text = string.Format("体检类型：{0} 危害因素：{1} 岗位： {2} 工种：{3}", _tjlCustomerRegDto.PostState,
                    _tjlCustomerRegDto.RiskS, _tjlCustomerRegDto.WorkName, _tjlCustomerRegDto.TypeWork);
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                entityDto.Id = _tjlCustomerRegDto.Id;
                var Occ = _inspectionTotalService.GetOccSumByCusReg(entityDto);
                _OccTargetDiseaseByRisk = Occ;
                //repositoryItemzyb.DataSource = Occ.OccDiseases;
                //repositoryItemzyb.DisplayMember = "Text";
                //repositoryItemzyb.ValueMember = "Id";
                //repositoryItemjjz.DataSource = Occ.Contraindications;
                //repositoryItemjjz.DisplayMember = "Text";
                //repositoryItemjjz.ValueMember = "Id";


                //医学建议
                ChargeBM chargeyxjy = new ChargeBM();
                chargeyxjy.Name = ZYBBasicDictionaryType.YXJY.ToString();
                var lisyxjy = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeyxjy).ToList();
                if (tjlb.Contains("放射"))
                {
                    lisyxjy = lisyxjy.Where(p => p.Remarks.Contains("放射")).ToList();
                }
                else
                {
                    lisyxjy = lisyxjy.Where(p => !p.Remarks.Contains("放射")).ToList();
                }
                //searOccAdvice.Properties.DataSource = lis1;
                repositoryItemComboYXJY.Items.Clear();
                foreach (var yj in lisyxjy)
                {
                    repositoryItemComboYXJY.Items.Add(yj.Text);
                }

                //处理意见
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = ZYBBasicDictionaryType.Opinions.ToString();
                var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).ToList();
                if (tjlb.Contains("放射"))
                {
                    lis1 = lis1.Where(p => p.Remarks.Contains("放射")).ToList();
                }
                else
                {
                    lis1 = lis1.Where(p => !p.Remarks.Contains("放射")).ToList();
                }
                //searOccAdvice.Properties.DataSource = lis1;
                repositoryItemclyj.Items.Clear();
                foreach (var yj in lis1)
                {
                    repositoryItemclyj.Items.Add(yj.Text);
                }
                //职业健康结论

                chargeBM.Name = ZYBBasicDictionaryType.Conclusion.ToString();
                var lis2 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            
                repositoryItemjl.DataSource = lis2;
                repositoryItemjl.DisplayMember = "Text";
                repositoryItemjl.ValueMember = "Text";
                //tabNavigationPage3.Show();
                tabNavigationPage3.PageVisible = true;
            }
            else
            {
                tabNavigationPage3.PageVisible = false;

            }
            if (tjlb.Contains("职业+健康"))
            {
                labelControlState.Text = CheckSateHelper.SummSateFormatter(_tjlCustomerRegDto.OccSummSate);
            }
            else
            {
                labelControlState.Text = CheckSateHelper.SummSateFormatter(_tjlCustomerRegDto.SummSate);
            }
        }
      
        //加载表格数据
        private void LoadData()
        {
            //splashScreenManager.SetWaitFormDescription("加载所有医生...");

            var userDto = DefinedCacheHelper.GetComboUsers();
            customyiSheng.Properties.DataSource = userDto;
            customyiSheng.EditValue = CurrentUser.Id;

            //splashScreenManager.SetWaitFormDescription("加载体检人信息...");
            //_customerRegDto = _inspectionTotalService.GetCustomerRegInfo(new EntityDto<Guid>() { Id = _tjlCustomerRegDto.Id });
            _aTjlCustomerDepSummaryDto = _inspectionTotalService.GetCustomerDepSummaryList(new EntityDto<Guid>() { Id = _tjlCustomerRegDto.Id });


            //splashScreenManager.SetWaitFormDescription("加载建议字典...");
            //查询建议字典--初始化读取，后续生成总检使用
            //var idlist = _aTjlCustomerDepSummaryDto.Select(r => r.DepartmentBMId).ToList();
            //_summarizeAdviceFull = _summarizeAdviceAppService.GetSummAll(new InputSearchSumm() { Age = _tjlCustomerRegDto.Customer.Age, SexState = _tjlCustomerRegDto.Customer.Sex });
            int setnot = (int)Sex.GenderNotSpecified;
            int setUn = (int)Sex.Unknown;
            _summarizeAdviceFull = DefinedCacheHelper.GetSummarizeAdvices().
                Where(o => o.SexState == setnot || o.SexState == setUn ||
                o.SexState == _tjlCustomerRegDto.Customer.Sex).
                Where(o => o.MaxAge >= _tjlCustomerRegDto.Customer.Age
                && o.MinAge <= _tjlCustomerRegDto.Customer.Age).ToList();
            //绑定诊断搜索下拉框           
            customGridZhenDuan2.Properties.DataSource = _summarizeAdviceFull;
            customGridZhenDuan2.EditValue = "Id";

            //splashScreenManager.SetWaitFormDescription("加载体检人图像信息...");

            CustomerItemPicAllSys = _doctorStationAppService.GetCustomerItemPicDtos(new QueryClass() { CustomerRegId = _tjlCustomerRegDto.Id });

            //splashScreenManager.SetWaitFormDescription("加载体检人检查项目结果...");
            //var stopwatch = new Stopwatch();  ///------------计时器--------------
            //stopwatch.Start();
            // var customerRegItem = _inspectionTotalService.GetCustomerRegItemList(new QueryClass { CustomerRegId = _tjlCustomerRegDto.Id }).Where(o => o.CustomerItemGroupBM != null).ToList();

            //stopwatch.Stop();
            //var ttt = stopwatch.ElapsedMilliseconds;
            var customerRegInsItem = _inspectionTotalService.GetCusInspectItemList(new QueryClass { CustomerRegId = _tjlCustomerRegDto.Id }).ToList();


            //gridControlCustomerRegItem.DataSource = customerRegInsItem.OrderBy(n => n.DepartmentOrderNum)
            //    .ThenBy(n => n.ItemGroupOrder).ThenBy(n => n.ItemOrder);


            _customerRegItemList = customerRegInsItem.OrderBy(n => n.DepartmentOrderNum)
                .ThenBy(n => n.ItemGroupOrder).ThenBy(n => n.ItemOrder).ToList();
            if (checkEdit1.Checked == true)
            {
             var itemlist = _customerRegItemList.Where(o => o.Symbol != "" && o.Symbol != "M" && o.Symbol != null).ToList();
                if (labZY.Checked == true && iszyb==false)
                {
                    itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                }
                gridControlCustomerRegItem.DataSource = itemlist;
            }
            else
            {
                //过滤健康检项目
                var  itemlist = _customerRegItemList.ToList();
                if (labZY.Checked == true)
                {
                    itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                }
                gridControlCustomerRegItem.DataSource = itemlist;
            }           
           var Nockek= _customerRegItemList.Where(o => o.DepartmentId != Guid.Parse("B54823B8-23C5-4107-9747-6E6CBB486022")
            && o.GroupCheckState == (int)ProjectIState.Not).Select(o => o.ItemGroupName).Distinct().ToList();
            if (Nockek.Count>=0)
            {
                chekNo.Text = "未检项目("+ Nockek.Count+ "个)";
                chekNo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                chekNo.Text = "未检项目";
            }
            #region 未小结，未检提示

            var Str = string.Empty;
            //杜智菁修改 o.ProcessState!= (int)ProjectIState.GiveUp
            var sumls = _aTjlCustomerDepSummaryDto.Select(o => o.DepartmentBMId).ToList();
            var nosum = _customerRegItemList.Where(o => !sumls.Contains(o.DepartmentId) && o.ProcessState != (int)ProjectIState.GiveUp).Select(o => o.DepartmentName).Distinct().ToList();
            Str = string.Join(",", nosum);
            var nocheckGroups = _customerRegItemList.Where(o => o.DepartmentId != Guid.Parse("B54823B8-23C5-4107-9747-6E6CBB486022")
             && o.GroupCheckState == (int)ProjectIState.Not).Select(o => o.ItemGroupName).Distinct().ToList();
            string Strno = string.Join(",", nocheckGroups);
            if (!string.IsNullOrWhiteSpace(Str))
            {
                // ShowMessageSucceed(Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空。");
                alertInfo.Show(this, "提示!", Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空。");
                //字典控制
                var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 20)?.Remarks;
                if (IsYC != null && IsYC == "Y")
                {
                    ShowMessageSucceed(Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空。");
                }
                // XtraMessageBox.Show(Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空，是否生成汇总及建议？。", "是否强制下总检");

                // XtraMessageBox.Show(Strno + "未检。", "是否强制下总检");


            }
            if (!string.IsNullOrWhiteSpace(Strno))
            { var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 24)?.Remarks;
                if (IsYC != null && IsYC == "Y")
                {
                    ShowMessageSucceed(Strno + "未检。");
                }
                    alertInfo.Show(this, "提示!", Strno + "未检。");
            }
            #endregion
            //职业健康labZYB
        }

        //拼接危急值项目
        private void LoadCrisisRegItem()
        {
            var RegItems = _customerRegItemList.Where(n => n.CrisisSate == (int)CrisisSate.Abnormal);
            var SbStr = string.Empty;
            var IsCrisis = false;
            var depId = new List<Guid>();
            foreach (var item in RegItems)
            {
                if (depId.Contains(item.DepartmentId))
                    continue;
                depId.Add(item.DepartmentId);
                IsCrisis = true;
                SbStr += "【" + item.DepartmentName + "】,";
            }
            SbStr += "存在危急值，请注意！";
            if (IsCrisis)
                ShowMessageSucceed(SbStr);
        }       

        //总检结论拼接
        private void LoadStr()
        {
           
            //var dto = _customerRegDto.CustomerDepSummary.OrderBy(o => o.DepartmentOrder).ToList();
            var dto = _aTjlCustomerDepSummaryDto.OrderBy(o => o.DepartmentOrder).ToList();
            var str = "";
            var departmentName = string.Empty;
            var iCount = 1;           
            for (var i = 0; i < dto.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(dto[i].DagnosisSummary) && string.IsNullOrWhiteSpace(dto[i].CharacterSummary))
                    continue;
                //字典中国屏蔽的科室诊断
                var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
                if (IsYC != null && IsYC.Remarks == "0")
                {
                    var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
                    if (IsYCgjc != null && IsYCgjc.Remarks != "")
                    {
                        string[] gjcls = IsYCgjc.Remarks.Split('|');
                        bool isZC = false;
                        foreach (string gjc in gjcls)
                        {
                            if (!string.IsNullOrWhiteSpace(dto[i].DagnosisSummary))
                            {
                                if (dto[i].DagnosisSummary.Replace(" ", "").Trim() == gjc)
                                {
                                    isZC = true;
                                    continue;
                                }

                            }
                            else
                            {
                                if (dto[i].CharacterSummary.Replace(" ", "").Trim() == gjc)
                                {
                                    isZC = true;
                                    continue;
                                }
                            }
                        }
                        if (isZC)
                        {
                            continue;
                        }
                    }
                }
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 3);

                string ZjFormat = ZjFormatd?.Remarks ?? "";
                //设置总检格式和健康体检分开
                var zyzs = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 110)?.Remarks;
                if (!string.IsNullOrEmpty(zyzs))
                {
                    ZjFormat = zyzs;
                }
                if (departmentName != dto[i].DepartmentName)
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【序号】", iCount.ToString()).Replace("【科室名称】", dto[i].DepartmentName);
                    }
                    else
                    {
                        str += $"{iCount}.{dto[i].DepartmentName}{ Environment.NewLine }";
                    }
                    departmentName = dto[i].DepartmentName;
                    iCount++;

                }

                if (!string.IsNullOrWhiteSpace(dto[i].DagnosisSummary))
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【科室小结】", dto[i].DagnosisSummary.TrimEnd((char[])"\r\n".ToCharArray()));
                    }
                    else
                    {

                        var spStr = dto[i].DagnosisSummary.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        for (var j = 0; j < spStr.Count(); j++)
                            if (!string.IsNullOrWhiteSpace(spStr[j]))
                            {
                                str += $"{spStr[j]}{ Environment.NewLine }";
                            }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(dto[i].CharacterSummary))
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat += ZjFormat.Replace("【科室小结】", dto[i].CharacterSummary.TrimEnd((char[])"\r\n".ToCharArray()));
                    }
                    else
                    {

                        var spStr = dto[i].CharacterSummary.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        for (var j = 0; j < spStr.Count(); j++)
                            if (!string.IsNullOrWhiteSpace(spStr[j]))
                            {
                                str += $"{spStr[j]}{ Environment.NewLine }";
                            }
                    }
                }
                if (ZjFormat != "")
                {
                    ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】"," ");
                    //str += ZjFormat;
                    str += $"{ZjFormat}{ Environment.NewLine }"; ;
                }

            }
            
            memoEditHuiZong.Text = str;
            richEditControl1.Text= str; 
            //隐私项目
            var ysdeparSum = _aTjlCustomerDepSummaryDto.Where(o => o.PrivacyCharacterSummary != null
             && o.PrivacyCharacterSummary != "").ToList();
            if (ysdeparSum.Count > 0)
            {
                var Psum = ysdeparSum.Select(o=>o.PrivacyCharacterSummary).ToList();                
                txtPrivacy2.Text=string.Join(Environment.NewLine, Psum);
                txtPrivacy1.Text = txtPrivacy2.Text;
                layPrivacy1.Visibility = LayoutVisibility.Always;
                layPrivacy2.Visibility = LayoutVisibility.Always;
                layPrivacyTS1.Visibility = LayoutVisibility.Always;
                layPrivacyTS2.Visibility = LayoutVisibility.Always;
            }           
            else
            {
                layPrivacy1.Visibility = LayoutVisibility.Never;
                layPrivacy2.Visibility = LayoutVisibility.Never;
                layPrivacyTS1.Visibility = LayoutVisibility.Never;
                layPrivacyTS2.Visibility = LayoutVisibility.Never;
            }
        }

        //组合总检结论拼接
        private void GroupLoadStr()
        {

            //var dto = _customerRegDto.CustomerDepSummary.OrderBy(o => o.DepartmentOrder).ToList();
            // var dto = _aTjlCustomerDepSummaryDto.OrderBy(o => o.DepartmentOrder).ToList();
            var dto = _customerRegItemList.Select(p => new
            {
                p.DepartmentName,
                p.DepartmentOrderNum,
                p.ItemGroupName,
                p.ItemGroupOrder,
                p.ItemGroupSum,
                p.ItemGroupDiagnosis
            }).Distinct().ToList();
            var str = "";
            var departmentName = string.Empty;
            var iCount = 1;
            for (var i = 0; i < dto.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(dto[i].ItemGroupDiagnosis) && string.IsNullOrWhiteSpace(dto[i].ItemGroupSum))
                    continue;
                //字典中屏蔽的科室诊断
                var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
                if (IsYC != null && IsYC.Remarks == "0")
                {
                    var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
                    if (IsYCgjc != null && IsYCgjc.Remarks != "")
                    {
                        string[] gjcls = IsYCgjc.Remarks.Split('|');
                        bool isZC = false;
                        foreach (string gjc in gjcls)
                        {
                            if (!string.IsNullOrWhiteSpace(dto[i].ItemGroupDiagnosis))
                            {
                                if (dto[i].ItemGroupDiagnosis.Replace(" ", "").Trim() == gjc)
                                {
                                    isZC = true;
                                    continue;
                                }

                            }
                            else
                            {
                                if (dto[i].ItemGroupSum.Replace(" ", "").Trim() == gjc)
                                {
                                    isZC = true;
                                    continue;
                                }
                            }
                        }
                        if (isZC)
                        {
                            continue;
                        }
                    }
                }
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 3);

                string ZjFormat = ZjFormatd?.Remarks ?? "";
               
                //设置总检格式和健康体检分开
                var zyzs = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 110)?.Remarks;
                if (!string.IsNullOrEmpty(zyzs))
                {
                    ZjFormat = zyzs;
                }
              
                if (departmentName != dto[i].DepartmentName)
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【序号】", iCount.ToString()).Replace("【科室名称】", dto[i].DepartmentName);
                    }
                    else
                    {
                        str += $"{iCount}.{dto[i].DepartmentName}{ Environment.NewLine }";
                    }
                    departmentName = dto[i].DepartmentName;
                    iCount++;

                }

                if (!string.IsNullOrWhiteSpace(dto[i].ItemGroupDiagnosis))
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【科室小结】", dto[i].ItemGroupDiagnosis.TrimEnd((char[])"\r\n".ToCharArray()));
                    }
                    else
                    {

                        var spStr = dto[i].ItemGroupDiagnosis.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        for (var j = 0; j < spStr.Count(); j++)
                            if (!string.IsNullOrWhiteSpace(spStr[j]))
                            {
                                str += $"{spStr[j]}{ Environment.NewLine }";
                            }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(dto[i].ItemGroupSum))
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat += ZjFormat.Replace("【科室小结】", dto[i].ItemGroupSum.TrimEnd((char[])"\r\n".ToCharArray()));
                    }
                    else
                    {

                        var spStr = dto[i].ItemGroupSum.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        for (var j = 0; j < spStr.Count(); j++)
                            if (!string.IsNullOrWhiteSpace(spStr[j]))
                            {
                                str += $"{spStr[j]}{ Environment.NewLine }";
                            }
                    }
                }
                if (ZjFormat != "")
                {
                    ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                    //str += ZjFormat;
                    str += $"{ZjFormat}{ Environment.NewLine }"; ;
                }

            }

            memoEditHuiZong.Text = str;
            richEditControl1.Text = str;
            //隐私项目
            var ysdeparSum = _aTjlCustomerDepSummaryDto.Where(o => o.PrivacyCharacterSummary != null
             && o.PrivacyCharacterSummary != "").ToList();
            if (ysdeparSum.Count > 0)
            {
                var Psum = ysdeparSum.Select(o => o.PrivacyCharacterSummary).ToList();
                txtPrivacy2.Text = string.Join(Environment.NewLine, Psum);
                txtPrivacy1.Text = txtPrivacy2.Text;
                layPrivacy1.Visibility = LayoutVisibility.Always;
                layPrivacy2.Visibility = LayoutVisibility.Always;
                layPrivacyTS1.Visibility = LayoutVisibility.Always;
                layPrivacyTS2.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layPrivacy1.Visibility = LayoutVisibility.Never;
                layPrivacy2.Visibility = LayoutVisibility.Never;
                layPrivacyTS1.Visibility = LayoutVisibility.Never;
                layPrivacyTS2.Visibility = LayoutVisibility.Never;
            }
        }
        //项目总检结论拼接
        private string ItemLoadStr1(bool isZJJ)
        {
            string aqllstr = "";

            var dtoNowlist = _customerRegItemList.ToList();
            var dtoNow = dtoNowlist.Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M").OrderBy(p => p.CrisisLever).
              ThenBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();


            //if (dtoNow.Any(p => p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")))
            //{
            //    dtoNow = dtoNowlist.Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M" || (p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压"))).OrderBy(p => p.CrisisLever).
            //  ThenBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM.OrderNum).ToList();

            //}
            if (isZJJ == true)
            {
                dtoNow = dtoNow.Where(p => !p.IsZYB.HasValue || p.IsZYB != 1).ToList();
            }

            //重要异常
            var Crisisdt = dtoNow.Where(p => p.CrisisSate == 1).
                OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            //血压特殊处理
            bool isxy = false;
            List<CustomerRegInspectItemDto> CrisisOther = new List<CustomerRegInspectItemDto>();
            if (Crisisdt.Count > 0)
            {
                if (Crisisdt.Any(p => p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")))
                {
                    Crisisdt = dtoNow.Where(p => p.CrisisSate == 1 || p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")).
                OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
                    isxy = true;
                }
                var CrisisdtNew = GetCustomerRegisterItem(Crisisdt);
                aqllstr += ItemLoadstrFj(CrisisdtNew);
            }
            //普通异常
            var nomaldt = dtoNow.Where(p => p.CrisisSate != 1 ).//|| (p.CrisiChar != null && p.CrisiChar != "")
                OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            if (isxy == true)
            {
                nomaldt = nomaldt.Where(p => !p.ItemName.Contains("收缩压") && !p.ItemName.Contains("舒张压")).
                    OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList(); ;
            }
            if (nomaldt.Count > 0)
            {
                var nomaldtNew = GetCustomerRegisterItem(nomaldt);
                aqllstr += ItemLoadstrFj(nomaldtNew);
            }
            return aqllstr;

        }
        //项目总检结论拼接
        private string ItemLoadStr(bool isZJJ)
        {
            string aqllstr = "";

            var dtoNowlist = _customerRegItemList.ToList();
            var dtoNow = dtoNowlist.Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M").OrderBy(p => p.CrisisLever).
              ThenBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            if (isZJJ == true)
            {
                dtoNow = dtoNow.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
            }

            //重要异常
            var Crisisdt = dtoNow.Where(p => p.CrisisSate == 1).
                OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            if (Crisisdt.Count > 0)
            {
                var CrisisdtNew = GetCustomerRegisterItem(Crisisdt);
                aqllstr += ItemLoadstrFj(CrisisdtNew);
            }
            //普通异常
            var nomaldt = dtoNow.Where(p => p.CrisisSate != 1).
                OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            if (nomaldt.Count > 0)
            {
                var nomaldtNew = GetCustomerRegisterItem(nomaldt);
                aqllstr += ItemLoadstrFj(nomaldtNew);
            }
            return aqllstr;

        }



        #region 项目汇总相关
        /// <summary>
        /// 合并报告单
        /// </summary>
        /// <returns></returns>
        private List<CustomerRegInspectItemDto> GetCustomerRegisterItem(List<CustomerRegInspectItemDto> inputlist)
        {
            //var cuslist = inputlist.ToList() ;
            var json = JsonConvert.SerializeObject(inputlist);
            var cuslist = JsonConvert.DeserializeObject<List<CustomerRegInspectItemDto>>(json);

            List<string> ReportBMList = new List<string>();

            List<Guid> RemoveBMList = new List<Guid>();
            foreach (var input in cuslist)
            {
                if (!string.IsNullOrEmpty(input.ReportBM))
                {
                    var ReportBMlist = cuslist.Where(p => p.ReportBM == input.ReportBM).Select(p => p.ItemName).ToList();
                    input.ItemName = string.Join("$", ReportBMlist);
                    if (ReportBMList.Contains(input.ReportBM))
                    {
                        RemoveBMList.Add(input.Id);
                    }
                    ReportBMList.Add(input.ReportBM);
                }

            }
            var inputlistnew = cuslist.Where(p => !RemoveBMList.Contains(p.Id)).ToList();
            return inputlistnew;
        }

        private string ItemLoadstrFj(List<CustomerRegInspectItemDto> dtoNow)
        {
            var str = "";

            var deparsum = dtoNow.GroupBy(p => new { p.DepartmentId, p.DepartmentOrderNum, p.DepartmentName }).Select(p => new
            { p.Key.DepartmentOrderNum, p.Key.DepartmentName, p.Key.DepartmentId }).Distinct().OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.DepartmentName).ToList();
            for (var num = 0; num < deparsum.Count; num++)
            {
                bool HasDepat = false;
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 11);
            
                string allZjFormat = ZjFormatd?.Remarks ?? "";
                //设置总检格式和健康体检分开
                var zyzs = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 110)?.Remarks;
                if (!string.IsNullOrEmpty(zyzs))
                {
                    allZjFormat = zyzs;
                }
                var dto = dtoNow.Where(p => p.DepartmentId == deparsum[num].DepartmentId).
                    OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
                var departformat = "";
                if (allZjFormat.Contains("|"))
                {
                    departformat = allZjFormat.Substring(0, allZjFormat.IndexOf("|"));
                }
                if (departformat != "")
                {
                    departformat = departformat.Replace("【科室序号】", (num + 1).ToString()).Replace("【科室名称】", deparsum[num].DepartmentName).Replace("|", "");
                    departformat = departformat.Replace("【中文科室序号】", CommonFormat.NumToChinese((num + 1).ToString()));
                    // str += departformat;
                }
                else
                {
                    departformat = $"{(num + 1).ToString()}.{ deparsum[num].DepartmentName}{ Environment.NewLine }";
                    // str += $"{(num + 1).ToString()}.{ deparsum[num].Name}{ Environment.NewLine }";
                }
                var ItemFormat = allZjFormat.Substring(allZjFormat.IndexOf("|") + 1, allZjFormat.Length - allZjFormat.IndexOf("|") - 1);

                int xh = 1;
                for (int i = 0; i < dto.Count; i++)
                {
                    var ZjFormat = ItemFormat;

                    var itemnow = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p => p.Id == dto[i].ItemId);
                    if (itemnow != null && itemnow.IsSummary.HasValue && itemnow.IsSummary == 2)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis) && string.IsNullOrWhiteSpace(dto[i].ItemSum))
                        continue;
                    //字典中屏蔽的科室诊断
                    var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
                    if (IsYC != null && IsYC.Remarks == "0")
                    {
                        var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
                        if (IsYCgjc != null && IsYCgjc.Remarks != "")
                        {
                            string[] gjcls = IsYCgjc.Remarks.Split('|');
                            bool isZC = false;
                            foreach (string gjc in gjcls)
                            {
                                if (!string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis))
                                {
                                    if (dto[i].ItemDiagnosis.Replace(" ", "").Trim() == gjc)
                                    {
                                        isZC = true;
                                        continue;
                                    }

                                }
                                else
                                {
                                    if (dto[i].ItemDiagnosis.Replace(" ", "").Trim() == gjc)
                                    {
                                        isZC = true;
                                        continue;
                                    }
                                }
                            }
                            if (isZC)
                            {
                                continue;
                            }
                        }
                    }



                    //ZjFormat = ZjFormat.Replace("【序号】", (i + 1).ToString());
                    //ZjFormat = ZjFormat.Replace("【中文序号】", CommonFormat.NumToChinese((i + 1).ToString()));

                    ZjFormat = ZjFormat.Replace("【项目序号】", (xh).ToString());

                    //因为彩超等小结在诊断字段所以诊断有数据就取诊断
                    var itemSum = dto[i].ItemSum;
                    if (!string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis))
                    {
                        itemSum = dto[i].ItemDiagnosis;
                    }
                    if (!string.IsNullOrWhiteSpace(itemSum))
                    {
                        if (ZjFormat != "")
                        {
                            if (itemSum.Contains(dto[i].ItemName))
                            {
                                ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                            }
                            else if (dto[i].Symbol == "P")
                            {

                                if (dto[i].ItemName.Contains("$"))
                                {
                                    ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName.Replace("$", "、") + ": \r\n" + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                }
                                else
                                {
                                    ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName + ":" + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                }
                            }
                            else
                            {
                                ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                            }
                        }
                        else
                        {
                            if (itemSum.Contains(dto[i].ItemName))
                            {
                                ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                            }
                            else if (dto[i].Symbol == "P")
                            {

                                var itemstr = dto[i].ItemName + ":" + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                str += $"{itemstr}{ Environment.NewLine }";
                            }
                            else
                            {
                                var itemstr = dto[i].ItemName + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                str += $"{itemstr}{ Environment.NewLine }";
                            }
                        }
                    }
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                        if (HasDepat == false)
                        {
                            str += departformat;
                            HasDepat = true;
                        }
                        str += ZjFormat;
                        //str += $"{ZjFormat}{ Environment.NewLine }"; ;
                        xh = xh + 1;
                    }
                }
                str += $"{ Environment.NewLine }";
            }

            return str;
        }

        /// <summary>
        /// 项目汇总
        /// </summary>
        /// <param name="dtoNow"></param>
        /// <returns></returns>
        private string ItemLoadstrFj1(List<CustomerRegInspectItemDto> dtoNow)
        {
            var str = "";
            var sumHidelist = DefinedCacheHelper.GetBasicDictionary().Where(p => p.Type == BasicDictionaryType.SumHide.ToString()).OrderByDescending(p => p.Remarks.Length).ToList();

            var deparsum = dtoNow.GroupBy(p => new { p.DepartmentId, p.DepartmentOrderNum, p.DepartmentName }).Select(p => new
            { p.Key.DepartmentOrderNum, p.Key.DepartmentName, p.Key.DepartmentId }).Distinct().OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.DepartmentName).ToList();
            string DeparHH = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 18)?.Remarks;
            bool isNomal = false;
            if (dtoNow.Any(p => p.CrisisSate == (int)CrisisSate.Normal))
            {
                isNomal = true;
            }

            for (var num = 0; num < deparsum.Count; num++)
            {

                bool HasDepat = false;
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 11);
                string allZjFormat = ZjFormatd?.Remarks ?? "";
                var dto = dtoNow.Where(p => p.DepartmentId == deparsum[num].DepartmentId).
                    OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
                var departformat = "";
                if (allZjFormat.Contains("|"))
                {
                    departformat = allZjFormat.Substring(0, allZjFormat.IndexOf("|"));
                }
                if (departformat != "")
                {
                    departformat = departformat.Replace("【科室序号】", (num + 1).ToString()).Replace("【科室名称】", deparsum[num].DepartmentName).Replace("|", "");
                    departformat = departformat.Replace("【中文科室序号】", CommonFormat.NumToChinese((num + 1).ToString()));
                    // str += departformat;
                }
                else
                {
                    departformat = $"{(num + 1).ToString()}.{ deparsum[num].DepartmentName}{ Environment.NewLine }";
                    // str += $"{(num + 1).ToString()}.{ deparsum[num].Name}{ Environment.NewLine }";
                }
                var ItemFormat = allZjFormat.Substring(allZjFormat.IndexOf("|") + 1, allZjFormat.Length - allZjFormat.IndexOf("|") - 1);

                int xh = 1;
                //List<Guid> fhlist = new List<Guid>();
                for (int i = 0; i < dto.Count; i++)
                {
                    ////复合判断只显示一次项目结果
                    //if (fhlist.Contains(dto[i].ItemGroupBMId.Value))
                    //{
                    //    continue;
                    //}
                    var ZjFormat = ItemFormat;

                    var itemnow = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p => p.Id == dto[i].ItemId);
                    if (itemnow != null && itemnow.IsSummary.HasValue && itemnow.IsSummary == 2)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis) && string.IsNullOrWhiteSpace(dto[i].ItemSum))
                        continue;
                    //字典中屏蔽的科室诊断
                    var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
                    if (IsYC != null && IsYC.Remarks == "0")
                    {
                        var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
                        if (IsYCgjc != null && IsYCgjc.Remarks != "")
                        {
                            string[] gjcls = IsYCgjc.Remarks.Split('|');
                            bool isZC = false;
                            foreach (string gjc in gjcls)
                            {
                                if (!string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis))
                                {
                                    if (dto[i].ItemDiagnosis.Replace(" ", "").Trim() == gjc)
                                    {
                                        isZC = true;
                                        continue;
                                    }

                                }
                                else
                                {
                                    if (dto[i].ItemDiagnosis.Replace(" ", "").Trim() == gjc)
                                    {
                                        isZC = true;
                                        continue;
                                    }
                                }
                            }
                            if (isZC)
                            {
                                continue;
                            }
                        }
                    }



                    //ZjFormat = ZjFormat.Replace("【序号】", (i + 1).ToString());
                    //ZjFormat = ZjFormat.Replace("【中文序号】", CommonFormat.NumToChinese((i + 1).ToString()));

                    ZjFormat = ZjFormat.Replace("【项目序号】", (xh).ToString());

                    //因为彩超等小结在诊断字段所以诊断有数据就取诊断

                    var itemSum = dto[i].ItemSum;
                    if (!string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis))
                    {
                        itemSum = dto[i].ItemDiagnosis;
                    }
                    ////重要异常只显示重要异常诊断
                    //if (!string.IsNullOrEmpty(dto[i].CrisiChar) && dto[i].CrisisSate == (int)CrisisSate.Abnormal && isNomal == false)
                    //{
                    //    itemSum = dto[i].CrisiChar;
                    //}
                    //else if (!string.IsNullOrEmpty(dto[i].CrisiChar) && dto[i].CrisisSate == (int)CrisisSate.Abnormal && isNomal == true)
                    //{
                    //    //显示除重要异常的其他异常
                    //    itemSum = itemSum.Replace(dto[i].CrisiChar, "");
                    //}
                    if (!string.IsNullOrWhiteSpace(itemSum))
                    {
                        #region 过滤总检结论
                        foreach (var sumhide in sumHidelist)
                        {
                            itemSum = itemSum.Replace(sumhide.Remarks + "\r\n", "");
                            itemSum = itemSum.Replace(sumhide.Remarks, "");
                        }
                        if (itemSum.Replace("\r", "").Replace("\n", "") == "")
                        {
                            continue;
                        }
                        #endregion
                        if (ZjFormat != "")
                        {

                            if (itemSum.Contains(dto[i].ItemName))
                            {
                                ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                            }
                            else if (dto[i].Symbol == "P")
                            {

                                if (dto[i].ItemName.Contains("$"))
                                {
                                    ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName.Replace("$", "、") + ": \r\n" + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                }
                                else
                                {
                                    ////复合判断不显示项目名称
                                    //if (dto[i].DiagnSate == 1)
                                    //{ ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray())); }
                                    //else
                                    //{
                                        string hh = "";
                                        if (!string.IsNullOrEmpty(DeparHH))
                                        {
                                            var deparlist = DeparHH.Split('|').ToList();
                                            if (deparlist.Contains(dto[i].DepartmentName))
                                            {
                                                hh = "\r\n";
                                            }

                                        }

                                        ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName + ":" + hh + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                    //}
                                }
                            }
                            else
                            { //复合判断或结果中包含项目名称则不显示项目名称
                                //if (dto[i].DiagnSate == 1 || itemSum.Contains(dto[i].ItemName))
                                //{
                                //    ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                //}
                                //else
                                //{
                                    ZjFormat = ZjFormat.Replace("【项目小结】", dto[i].ItemName + itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                //}
                            }
                        }
                        else
                        {
                            if (itemSum.Contains(dto[i].ItemName))
                            {
                                ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                            }
                            else if (dto[i].Symbol == "P")
                            {

                                var itemstr = dto[i].ItemName + ":" + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                str += $"{itemstr}{ Environment.NewLine }";
                            }
                            else
                            {
                                var itemstr = dto[i].ItemName + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                str += $"{itemstr}{ Environment.NewLine }";
                            }
                        }
                    }
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                        if (HasDepat == false)
                        {
                            str += departformat;
                            HasDepat = true;
                        }
                        str += ZjFormat;
                        //str += $"{ZjFormat}{ Environment.NewLine }"; ;
                        xh = xh + 1;
                    }
                    //if (dto[i].DiagnSate == 1)
                    //{ fhlist.Add(dto[i].ItemGroupBMId.Value); }
                }
                str += $"{ Environment.NewLine }";

            }

            return str;
        }
        #endregion

        //匹配建议算法
        private void MatchingAdvice()
        {
            //建议汇总数据
            var StrContent = memoEditHuiZong.Text;
            //建议汇总数据
            var StrContent2 = memoEditHuiZong.Text;
            //隐私总检汇总
            var PStrContent = txtPrivacy2.Text;
            //存储建议Id集合
            List<Guid> IdList = new List<Guid>();
            //存储建议Id集合
            List<Guid> PIdList = new List<Guid>();
           //匹配多条建议
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 5)?.Remarks;
            //遍历科室建议 
            var field非异常诊断前缀 = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 7)?.Remarks;
            // 过滤数据
            var field已匹配建议 = new ConcurrentDictionary<string, SummarizeAdviceDto>();
            List<string> AdviceName = new List<string>();
            foreach (var Ditem in _summarizeAdviceFull.OrderByDescending(n => n.AdviceName.Length).ToList())
                if (!string.IsNullOrWhiteSpace(Ditem.AdviceName))
                {
                    if (isshow != null && isshow == "1")
                    {
                        if (AdviceName.Contains(Ditem.AdviceName))
                        { continue; }
                        string adviName = Ditem.AdviceName;
                        if (!string.IsNullOrEmpty(Ditem.Advicevalue))
                        {
                            adviName = Ditem.Advicevalue;
                        }                      
                        var adviNames = adviName.Split('|');
                        foreach (string ad in adviNames)
                        {
                            if (!string.IsNullOrEmpty(ad) )
                            {
                                if (!string.IsNullOrWhiteSpace(StrContent))
                                    if (StrContent.Contains(ad))
                                    {
                                        //排除非异常前缀如 未见脂肪干
                                        if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                        {
                                            var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                            bool havalue = false;
                                            foreach (var str in field非异常诊断前缀列表)
                                            {
                                                if (StrContent.Contains(str + ad))
                                                { havalue = true; }
                                            }
                                            if (havalue == false)
                                            {
                                                IdList.Add(Ditem.Id);
                                                AdviceName.Add(Ditem.AdviceName);
                                              
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            IdList.Add(Ditem.Id);
                                            AdviceName.Add(Ditem.AdviceName);
                                            break;
                                        }
                                    }
                                if (!string.IsNullOrWhiteSpace(PStrContent))
                                    if (PStrContent.Contains(ad))
                                    { //排除非异常前缀如 未见脂肪干
                                        if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                        {
                                            var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                            bool havalue = false;
                                            foreach (var str in field非异常诊断前缀列表)
                                            {
                                                if (PStrContent.Contains(str+ ad))
                                                {
                                                    havalue = true;
                                                }
                                            }
                                            if (havalue == false)
                                            {
                                                PIdList.Add(Ditem.Id);
                                                AdviceName.Add(Ditem.AdviceName);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            PIdList.Add(Ditem.Id);
                                            AdviceName.Add(Ditem.AdviceName);
                                            break;
                                        }
                                    }
                            }
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(StrContent))
                            if (StrContent.Contains(Ditem.AdviceName))
                            {
                                //排除非异常前缀如 未见脂肪干
                                if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                {
                                    var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    bool havalue = false;
                                    foreach (var str in field非异常诊断前缀列表)
                                    {
                                        if (StrContent.Contains(str +Ditem.AdviceName) )
                                        { havalue = true; }
                                    }
                                    if (havalue == false)
                                    {
                                        IdList.Add(Ditem.Id);
                                        StrContent = StrContent.Replace(Ditem.AdviceName, "");
                                    }
                                }
                                else
                                {
                                    IdList.Add(Ditem.Id);
                                    StrContent = StrContent.Replace(Ditem.AdviceName, "");
                                }
                            }
                        if (!string.IsNullOrWhiteSpace(PStrContent))
                            if (PStrContent.Contains(Ditem.AdviceName))
                            {//排除非异常前缀如 未见脂肪干
                                if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                {
                                    var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    bool havalue = false;
                                    foreach (var str in field非异常诊断前缀列表)
                                    {
                                        if (PStrContent.Contains(str + Ditem.AdviceName))
                                        { havalue = true; }
                                    }
                                    if (havalue == false)
                                    {
                                        PIdList.Add(Ditem.Id);
                                        PStrContent = PStrContent.Replace(Ditem.AdviceName, "");
                                    }
                                }
                                else
                                {
                                    PIdList.Add(Ditem.Id);
                                    PStrContent = PStrContent.Replace(Ditem.AdviceName, "");
                                }
                            }
                    }
                }
            IdList.AddRange(PIdList);
            List<SummarizeAdviceDto> info = _summarizeAdviceAppService.GetSummForGuidList(IdList);
            //按照汇总顺序重新排列诊断数据
            foreach (var item in info)
            {
                if (isshow != null && isshow == "1")
                {
                    string adviName = item.AdviceName;
                    if (!string.IsNullOrEmpty(item.Advicevalue))
                    {
                        adviName = item.Advicevalue;
                    }
                    var adviNames = adviName.Split('|');
                    foreach (var ad in adviNames)
                    { item.IndexOfNum = StrContent2.IndexOf(ad); }
                }
                else
                {
                    item.IndexOfNum = StrContent2.IndexOf(item.AdviceName);
                }
            }
            info = info.OrderBy(n => n.IndexOfNum).ToList();
            _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();
            //清除已选诊断项目
            foreach (var item in _CustomerSummarizeList)
            {
                foreach (var itemInfo in info)
                {
                    if (item.SummarizeAdviceId == itemInfo.Id)
                    {
                        info.Remove(itemInfo);
                        break;
                    }
                }
            }
            //将新加入的诊断项目进行序号重排，并转换记录表对象，放入集合
            for (int i = 0; i < info.Count(); i++)
            {
               
                info[i].OrderNum = _CustomerSummarizeList.Count() + 1;
                if (PIdList.Count >= 0 && PIdList.Contains(info[i].Id))
                {
                    _CustomerSummarizeList.Add(SummarizeBMToJL(info[i],true));
                }
                else
                {
                    _CustomerSummarizeList.Add(SummarizeBMToJL(info[i]));
                }
            }
           
        }

        //根据状态加载按钮显隐
        private void LoadDataState()
        {
            simpleButtonShenHe.Enabled = true;
            BtnSave.Enabled = true;
            simpleButtonShengCheng.Enabled = true;
            simpleButtonQingChu.Enabled = true;
            butUpdate.Enabled = true;
            //职业健康            
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            if (tjlb!=null && tjlb.Contains("职业+健康"))
            {
                if (_customerSummarizeDto != null && !_customerSummarizeDto.occCheckState.HasValue)
                {
                    _customerSummarizeDto.occCheckState = (int)SummSate.NotAlwaysCheck;
                }
                if (_customerSummarizeDto != null && (_customerSummarizeDto.occCheckState == (int)SummSate.HasInitialReview || _customerSummarizeDto.occCheckState == (int)SummSate.Audited))
                {
                    //是否有权限修改别人的总检
                    bool IsTrue = false;
                    try
                    {
                        //判断当前用户有没有特殊权限
                        var UserInfo = _PersonnelIndividuationConfigAppService.GetUserById(new EntityDto<long> { Id = CurrentUser.Id });
                        if (UserInfo.IndividuationConfig != null)
                        {
                            var individuationConfig = UserInfo.IndividuationConfig;
                            if (individuationConfig.IsActive && individuationConfig.AdvancedAlwaysCheck)
                                IsTrue = true;
                        }
                    }
                    catch (UserFriendlyException c)
                    {
                        ShowMessageBox(c);
                    }


                    //判断是不是总检人
                    if (CurrentUser.Id == _customerSummarizeDto.occEmployeeBMId || _customerSummarizeDto.occShEmployeeBMId == CurrentUser.Id || IsTrue)
                    {
                        if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.Audited)
                        {
                            BtnSave.Enabled = false;
                            simpleButtonShenHe.Enabled = false;

                            customGridZhenDuan2.Enabled = false;

                            simpleButtonXinZeng2.Enabled = false;
                            simpleButtonShengCheng.Enabled = false;
                            butUpdate.Enabled = false;
                        }
                        else
                        {
                            BtnSave.Enabled = true;
                            simpleButtonShenHe.Enabled = true;

                            customGridZhenDuan2.Enabled = true;

                            simpleButtonXinZeng2.Enabled = true;
                            simpleButtonShengCheng.Enabled = true;
                            butUpdate.Enabled = true;
                        }
                    }
                    else
                    {
                        //不是总检人按钮全灰
                        BtnSave.Enabled = false;
                        simpleButtonShenHe.Enabled = false;
                        simpleButtonQingChu.Enabled = false;
                        simpleButtonShengCheng.Enabled = false;
                        butUpdate.Enabled = false;
                        customGridZhenDuan2.Enabled = false;

                        simpleButtonXinZeng2.Enabled = false;
                    }
                }
                else if (_tjlCustomerRegDto.SummLocked == (int)SummLockedState.Alr)
                {
                    //已锁定
                    //BtnSave.Enabled = false;
                    //simpleButtonShengCheng.Enabled = false;
                    //simpleButtonQingChu.Enabled = false;
                    //LoadStr();
                }
                else if (_tjlCustomerRegDto.SummLocked == (int)SummLockedState.Unchecked && (_tjlCustomerRegDto.OccSummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.OccSummSate == null))
                {
                    //未锁定，正常总检
                    //更新患者体检信息表
                    //_tjlCustomerRegDto.SummLocked = 1;
                    //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);
                    //解锁建议下拉框        
                    customGridZhenDuan2.Enabled = true;
                    simpleButtonXinZeng2.Enabled = true;
                    //拼接总结
                    //LoadStr();
                }

            }
            else
            {
                if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview || _tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
                {
                    //是否有权限修改别人的总检
                    bool IsTrue = false;
                    try
                    {
                        //判断当前用户有没有特殊权限
                        var UserInfo = _PersonnelIndividuationConfigAppService.GetUserById(new EntityDto<long> { Id = CurrentUser.Id });
                        if (UserInfo.IndividuationConfig != null)
                        {
                            var individuationConfig = UserInfo.IndividuationConfig;
                            if (individuationConfig.IsActive && individuationConfig.AdvancedAlwaysCheck)
                                IsTrue = true;
                        }
                    }
                    catch (UserFriendlyException c)
                    {
                        ShowMessageBox(c);
                    }


                    //判断是不是总检人
                    if (CurrentUser.Id == _tjlCustomerRegDto.CSEmployeeId || _tjlCustomerRegDto.FSEmployeeId == CurrentUser.Id || IsTrue)
                    {
                        if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
                        {
                            BtnSave.Enabled = false;
                            simpleButtonShenHe.Enabled = false;

                            customGridZhenDuan2.Enabled = false;

                            simpleButtonXinZeng2.Enabled = false;
                            simpleButtonShengCheng.Enabled = false;
                            butUpdate.Enabled = false;
                        }
                        else
                        {
                            BtnSave.Enabled = true;
                            simpleButtonShenHe.Enabled = true;

                            customGridZhenDuan2.Enabled = true;

                            simpleButtonXinZeng2.Enabled = true;
                            simpleButtonShengCheng.Enabled = true;
                            butUpdate.Enabled = true;
                        }
                    }
                    else
                    {
                        //不是总检人按钮全灰
                        BtnSave.Enabled = false;
                        simpleButtonShenHe.Enabled = false;
                        simpleButtonQingChu.Enabled = false;
                        simpleButtonShengCheng.Enabled = false;
                        butUpdate.Enabled = false;
                        customGridZhenDuan2.Enabled = false;

                        simpleButtonXinZeng2.Enabled = false;
                    }
                }
                else if (_tjlCustomerRegDto.SummLocked == (int)SummLockedState.Alr)
                {
                    //已锁定
                    //BtnSave.Enabled = false;
                    //simpleButtonShengCheng.Enabled = false;
                    //simpleButtonQingChu.Enabled = false;
                    //LoadStr();
                }
                else if (_tjlCustomerRegDto.SummLocked == (int)SummLockedState.Unchecked && (_tjlCustomerRegDto.SummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.SummSate == null))
                {
                    //未锁定，正常总检
                    //更新患者体检信息表
                    //_tjlCustomerRegDto.SummLocked = 1;
                    //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);
                    //解锁建议下拉框        
                    customGridZhenDuan2.Enabled = true;
                    simpleButtonXinZeng2.Enabled = true;
                    //拼接总结
                    //LoadStr();
                }
            }

            InitForm();
        }

        /// <summary>
        /// 加载总检数据
        /// </summary>
        private void LoadSummarizeData()
        {
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;

            _customerSummarizeDto = _inspectionTotalService.GetSummarize(new TjlCustomerQuery
            { CustomerRegID = _tjlCustomerRegDto.Id });
            _CustomerSummarizeList = _inspectionTotalService.GetSummarizeBM(new TjlCustomerQuery
            { CustomerRegID = _tjlCustomerRegDto.Id });
            if (tjlb.Contains("职业+健康"))
            { _CustomerSummarizeList= _CustomerSummarizeList.Where(p => p.IsZYB == 1).ToList(); }
            //职业健康 
            //显示所有职业病
            var Issy = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 121)?.Remarks;


            if (tjlb!=null && (Variables.ISZYB == "1" &&  ((tjlb.Contains("职业") && !string.IsNullOrEmpty(_tjlCustomerRegDto.RiskS)) ||
               tjlb.Contains("放射"))) ||Variables.ISZYB == "2")
            {
                //放射职业隐藏危害因素
                if (tjlb.Contains("放射") || (!string.IsNullOrEmpty(Issy) && Issy=="1"))
                {
                    if (tjlb.Contains("放射"))
                    {
                        laybtAdd.Visibility = LayoutVisibility.Always;
                        laybtDel.Visibility = LayoutVisibility.Always;
                        conriskName.Visible = false;
                    }
                    else
                    {
                        laybtAdd.Visibility = LayoutVisibility.Never;
                        laybtDel.Visibility = LayoutVisibility.Never;
                        conriskName.Visible = true;
                    }

                    var OccDiseaseDtos = new Occdieaserucan();
                    OccDiseaseDtos.IsActive = 1;
                    var ConDictionary = _OccDiseaseAppService.GetAllOccDisease(OccDiseaseDtos);

                    repositoryItemzyb.DataSource = ConDictionary;
                    repositoryItemzyb.DisplayMember = "Text";
                    repositoryItemzyb.ValueMember = "Id";

                    var showOccDictionary = new ShowOccDictionary();
                    showOccDictionary.Type = "Contraindication";
                    showOccDictionary.IsActive = 3;
                    var result = _OccDisProposalNewAppService.GetAll(showOccDictionary);

                    repositoryItemjjz.DataSource = result;
                    repositoryItemjjz.DisplayMember = "Text";
                    repositoryItemjjz.ValueMember = "Id";

                }
                else
                {
                    laybtAdd.Visibility = LayoutVisibility.Never;
                    laybtDel.Visibility = LayoutVisibility.Never;
                    conriskName.Visible = true;
                }
                EntityDto<Guid> inputzyb = new EntityDto<Guid>();
                inputzyb.Id = _tjlCustomerRegDto.Id;
                _OcccustomerSummarizeDto = _inspectionTotalService.GetCusOccSumByRegId(inputzyb);
                if (_OcccustomerSummarizeDto != null && _OcccustomerSummarizeDto.CreatOccCustomerSumDto != null)
                {
                  
                    //labelControl2.Text = "GBZ_188-2014_职业健康监护技术规范";
                  
                    gridHazardSum.DataSource = _OcccustomerSummarizeDto.OccCustomerHazardSumDto;
                 
                }
                else
                {
                    if (_OcccustomerSummarizeDto != null &&  _OcccustomerSummarizeDto.OccCustomerHazardSumDto != null
                        && _OcccustomerSummarizeDto.OccCustomerHazardSumDto?.Count > 0)
                    {
                        gridHazardSum.DataSource = _OcccustomerSummarizeDto.OccCustomerHazardSumDto;
                    }
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = ZYBBasicDictionaryType.OccSum.ToString();
                    var occsumset = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).FirstOrDefault(p => p.OrderNum == 1);

                   
                }
            }
            if (tjlb.Contains("职业+健康"))
            {
                memoEditHuiZong.Text = _customerSummarizeDto?.occCharacterSummary;
            }
            else
            {
                memoEditHuiZong.Text = _customerSummarizeDto?.CharacterSummary;
            }
            this.richEditControl1.Text = memoEditHuiZong.Text;
            //隐私诊断
            if (!string.IsNullOrEmpty(_customerSummarizeDto?.PrivacyCharacterSummary))
            {
                txtPrivacy2.Text = _customerSummarizeDto?.PrivacyCharacterSummary;
                txtPrivacy1.Text = txtPrivacy2.Text;

                layPrivacy1.Visibility = LayoutVisibility.Always;
                layPrivacy2.Visibility = LayoutVisibility.Always;
                layPrivacyTS1.Visibility = LayoutVisibility.Always;
                layPrivacyTS2.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layPrivacy1.Visibility = LayoutVisibility.Never;
                layPrivacy2.Visibility = LayoutVisibility.Never;
                layPrivacyTS1.Visibility = LayoutVisibility.Never;
                layPrivacyTS2.Visibility = LayoutVisibility.Never;
            }
           
            treeListZhenDuan.DataSource = _CustomerSummarizeList.OrderBy(l => l.SummarizeOrderNum).ToList();
            //treeListZhenDuan.BestFitColumns();

            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id = _tjlCustomerRegDto.Id;
            var review = _inspectionTotalService.GetCusReViewDto(input);

            if (review.Count > 0)
            {
                layoutControlItem44.Visibility= DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                gridReview.DataSource = review;
                gridReview.Visible = true;
            }
            else
            {
                layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                gridReview.DataSource = null;
               gridReview.Visible = false;
            }
        }

        //人员下拉框情况按钮事件
        private void customyiSheng_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                customyiSheng.EditValue = null;
        }    
        //生成总检
        private void simpleButtonShengCheng_Click(object sender, EventArgs e)
        {
            //去除掉未缴费与减项得体检项目
            var ItemOk = _customerRegItemList.Where(o => o.Id != Guid.Empty && o.GroupCheckState != (int)ProjectIState.GiveUp);
            //项目去重 拿到去重后得科室
            //var DicInfo = ItemOk.Where((x, i) => _customerRegItemList.FindIndex(z => z.DepartmentId == x.DepartmentId) == i).ToList();
            var DicInfo = ItemOk.Distinct(new CustomerRegComparerNew());
            #region 未小结，未检提示

            var Str = string.Empty;
            ListDeparmentNoSum = new List<Guid>();
            foreach (var item in DicInfo)
            {
                //采血科室不纳入判断范围，后期改为字典
                if (item.DepartmentId == Guid.Parse("B54823B8-23C5-4107-9747-6E6CBB486022"))
                    continue;
                var info = _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == item.DepartmentId);
                if (info == null)
                {
                    Str += "【" + item.DepartmentName + "】,";
                    ListDeparmentNoSum.Add(item.DepartmentId);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(info.CharacterSummary) && string.IsNullOrWhiteSpace(info.DagnosisSummary))
                    {
                        Str += "【" + item.DepartmentName + "】,";
                        ListDeparmentNoSum.Add(item.DepartmentId);
                    }
                }
            }
           
            var nocheckGroups = _customerRegItemList.Where(o => o.DepartmentId != Guid.Parse("B54823B8-23C5-4107-9747-6E6CBB486022")
             && o.GroupCheckState == (int)ProjectIState.Not).Select(o => o.ItemGroupName).Distinct().ToList();
          string   Strno = string.Join(",", nocheckGroups);
            if (!string.IsNullOrWhiteSpace(Str))
            {
                //ShowMessageSucceed(Str + "未填写科室小结。");

                            
                var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 20)?.Remarks;
                if (IsYC != null && IsYC == "Y")
                {
                    var IsTrue = XtraMessageBox.Show(Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空。", "是否强制下总检", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                }

                alertInfo.Show(this, "提示!", Str + "未填写科室小结,汇总中会缺少该科室信息，报告中结论将为空。");
            }

            if (!string.IsNullOrWhiteSpace(Strno))
            {
                var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 20)?.Remarks;
                if (IsYC != null && IsYC == "Y")
                {
                    var IsTrueno = XtraMessageBox.Show(Strno + "未检。", "是否强制下总检", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrueno)
                        return;
                }

                    alertInfo.Show(this, "提示!", Strno + "未检。");
            }
            #endregion
            if (this.memoEditHuiZong.Text.Trim() == string.Empty)
            {
                //重新加载检查汇总
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                var basicDictionary = DefinedCacheHelper.GetBasicDictionary();
                var field总检基础字典数据 = basicDictionary.Where(r => r.Type == BasicDictionaryType.CusSumSet.ToString()).ToList();
                var xmck = field总检基础字典数据.Find(r => r.Value == 16)?.Remarks;
                if (xmck != null && xmck == "Y")
                {
                    bool isOk = false;
                    if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                    {
                        var str = ItemLoadStr(true);
                        memoEditHuiZong.Text = str;
                        richEditControl1.Text = str;
                        //field总检汇总字符串生成器.AppendLine(ItemLoadStr(true));
                    }
                    else
                    {
                        var str = ItemLoadStr(true);
                        memoEditHuiZong.Text = str;
                        richEditControl1.Text = str;

                       // field总检汇总字符串生成器.AppendLine(ItemLoadStr(false));
                    }
                }
                else if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                {
                    GroupLoadStr();
                }
                else
                {
                    LoadStr();
                }
            }
            if (memoEditHuiZong.Text.Trim() == "")
            {
                memoEditHuiZong.Text = "* 本次体检所检项目未发现明显异常。";

            }
            //匹配建议
            MatchingAdvice();
            if (_CustomerSummarizeList.Count() > 0)
            {
                //tabPane1.SelectedPageIndex = 1;
                if (tabNavigationPage8.PageVisible != false)
                {
                    tabPane2.SelectedPageIndex = 1;
                }
                _CustomerSummarizeList = MakeReview(_CustomerSummarizeList);               
                treeListZhenDuan.DataSource = _CustomerSummarizeList.OrderBy(l => l.SummarizeOrderNum).ToList();
                treeListZhenDuan.RefreshDataSource();
               // treeListZhenDuan.BestFitColumns();
            }
            


            //return;
            ////打开诊断窗口
            //if (_totalDiagnosis == null || _totalDiagnosis.IsDisposed)
            //{
            //    //匹配建议
            //    MatchingAdvice();
            //    if (_matchingList.Count() > 0)
            //    {
            //        _totalDiagnosis = new TotalDiagnosis(_matchingList);
            //        _totalDiagnosis.SimpleButtonZhenduanClick += (s, t) =>
            //        {
            //            tabPane1.SelectedPageIndex = 1;
            //            gridColumnXuhao.FieldName = "OrderNum";
            //            gridColumnMingCheng.FieldName = "AdviceName";
            //            gridColumnNeiRong.FieldName = "SummAdvice";
            //            gridControl3.DataSource = _matchingList.OrderBy(l => l.OrderNum).ToList();
            //            gridControl3.RefreshDataSource();
            //            _totalDiagnosis.Close();
            //            _totalDiagnosis.Dispose();
            //        };
            //        _totalDiagnosis.TopMost = true;
            //        _totalDiagnosis.Show();
            //    }
            //    else
            //    {
            //        //ShowMessageSucceed("无科室建议。");
            //        _totalDiagnosis = new TotalDiagnosis(_matchingList);
            //        _totalDiagnosis.SimpleButtonZhenduanClick += (s, t) =>
            //        {
            //            tabPane1.SelectedPageIndex = 1;
            //            gridColumnXuhao.FieldName = "OrderNum";
            //            gridColumnMingCheng.FieldName = "AdviceName";
            //            gridColumnNeiRong.FieldName = "SummAdvice";
            //            gridControl3.DataSource = _matchingList.OrderBy(l => l.OrderNum).ToList();
            //            gridControl3.RefreshDataSource();
            //            _totalDiagnosis.Close();
            //            _totalDiagnosis.Dispose();
            //        };
            //        _totalDiagnosis.TopMost = true;
            //        _totalDiagnosis.Show();
            //    }
            //}
            //else
            //{
            //    _totalDiagnosis.SummarizeList = _matchingList;
            //    _totalDiagnosis.gridControl1.DataSource = _matchingList.OrderBy(l => l.OrderNum).ToList();
            //    _totalDiagnosis.gridControl1.RefreshDataSource();
            //    _totalDiagnosis.Activate();
            //}
        }
        #region 复查相关
        private List<TjlCustomerSummarizeBMDto> MakeReview(List<TjlCustomerSummarizeBMDto> input)
        {
            try
            {
                //获取默认回访数据
                repositoryItemCheckedComboBoxEdit2.ValueMember = " Id";
                repositoryItemCheckedComboBoxEdit2.DisplayMember = "ItemGroupName";
                repositoryItemCheckedComboBoxEdit2.DataSource = DefinedCacheHelper.GetItemGroups();

                var review = _inspectionTotalService.GetIllReViewDto(input);
                if (review != null && review.Count > 0)
                {
                    gridReview.Visible = true;
                    layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    gridReview.DataSource = review;
                    var advice = review.Select(o => o.SummarizeAdviceId).ToList();
                    foreach (var ad in input)
                    {
                        if (advice.Contains(ad.SummarizeAdviceId))
                        {
                            ad.isReview = 1;
                        }
                    }

                }
                else
                {
                    gridReview.Visible = false;
                    gridReview.DataSource = null;
                   layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                
                return input;
            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
                return null;
            }
        }
        private void SaveReview()
        {
            try
            {
                //获取默认回访数据
                var review = gridReview.DataSource as List<CusReViewDto>;
                if (review!=null && review.Count > 0)
                {
                    _inspectionTotalService.SaveCusReViewDto(review);
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "保存复查";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                else
                {
                    if (gridReview.Visible==true)
                    {
                        EntityDto<Guid> entityDto = new EntityDto<Guid>();
                        entityDto.Id = _tjlCustomerRegDto.Id;
                        _inspectionTotalService.DelCusReViewDto(entityDto);
                    }
                }
            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
            }

        }
        #endregion

        //保存按钮
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (customyiSheng.EditValue == null)
            {
                ShowMessageSucceed("请选择总检医生。");
                return;
            }
            //职业健康
            //var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            //var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            //var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            //if (tjlb != null && (Variables.ISZYB == "1" && tjlb.Contains("职业") && !string.IsNullOrEmpty(_tjlCustomerRegDto.RiskS)) || Variables.ISZYB == "2")
            //{   //危害因素职业病
            //    _OcccustomerSummarizeDto.OccCustomerHazardSumDto = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
            //    if (_OcccustomerSummarizeDto.OccCustomerHazardSumDto != null &&
            //        (_OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() == 0
            //        || _OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() > 1))
            //    {
            //        MessageBox.Show("必须选择一个主结论！");
            //        return;
            //    }
            //}
            Save();

            LoadDataState();
            //SimpleButtonSaveClick?.Invoke(sender, e);
            //ShowMessageSucceed("总检完成。");
            alertInfo.Show(this, "提示!", "总检完成。");
            var zjxyg = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 131);
            if (zjxyg == null || zjxyg?.Remarks == "Y")
            {
                if (laySH.Visibility == LayoutVisibility.Never)
                {
                    getNext((int)SummSate.NotAlwaysCheck);
                }
            }
        }

        //保存方法
        private void Save()
        {
            try
            {

                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;

                //删除建议表（多条）
                _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id , IsZYB=1 });
                //插入建议表（多条）
                var SbStr = string.Empty;
                var _CustomerSummarizeBM = new List<TjlCustomerSummarizeBMDto>();
                foreach (var MatchingItem in _CustomerSummarizeList.OrderBy(l => l.SummarizeOrderNum))
                {
                    if (MatchingItem.SummarizeName.Trim() == string.Empty && MatchingItem.Advice.Trim() == string.Empty)
                    {
                        continue;
                    }
                    SbStr += "*" + MatchingItem.SummarizeName + "："+ MatchingItem.Advice;
                    //MatchingItem.Id = Guid.NewGuid();
                    if (tjlb.Contains("职业+健康"))
                    { MatchingItem.IsZYB = 1; }
                        _CustomerSummarizeBM.Add(MatchingItem);
                }
                string fcxm = "";
                _inspectionTotalService.CreateSummarizeBM(_CustomerSummarizeBM);
                //总检保存复查项目
                var review = gridReview.DataSource as List<CusReViewDto>;
                if (review != null && review.Count > 0)
                {
                   var fcxmls = review.SelectMany(o=>o.ItemGroup).Select(o=>o.ItemGroupName).Distinct().ToList();
                    string grouname = string.Join(",", fcxmls);
                    fcxm= grouname.TrimEnd(',');
                }
              
                if (_customerSummarizeDto == null)
                {
                    //插入建议汇总 //插入建议表（整体建议 单条）
                    var _TjlCustomerSummarize = new TjlCustomerSummarizeDto();
                    _TjlCustomerSummarize.CustomerRegID = _tjlCustomerRegDto.Id;
              
                    if (tjlb.Contains("职业+健康"))
                    {
                        _TjlCustomerSummarize.occShEmployeeBMId = (long)customyiSheng.EditValue;
                        _TjlCustomerSummarize.occEmployeeBMId = (long)customyiSheng.EditValue;
                        _TjlCustomerSummarize.occCharacterSummary = memoEditHuiZong.Text;
                        _TjlCustomerSummarize.occAdvice = SbStr;
                        _TjlCustomerSummarize.occConclusionDate = _commonAppService.GetDateTimeNow().Now;
                        _TjlCustomerSummarize.occCheckState = (int)SummSate.HasInitialReview;
                    }
                    else
                    {
                        _TjlCustomerSummarize.ShEmployeeBMId = (long)customyiSheng.EditValue;
                        _TjlCustomerSummarize.EmployeeBMId = (long)customyiSheng.EditValue;
                        _TjlCustomerSummarize.CharacterSummary = memoEditHuiZong.Text;
                        _TjlCustomerSummarize.PrivacyCharacterSummary = txtPrivacy2.Text;
                        _TjlCustomerSummarize.Advice = SbStr;
                        _TjlCustomerSummarize.ConclusionDate = _commonAppService.GetDateTimeNow().Now;
                    }

                    _TjlCustomerSummarize.CheckState = 1;
                    //if (fcxm != "")
                    //{
                    _TjlCustomerSummarize.ReviewContent = fcxm;

                    //}
                    var result = _inspectionTotalService.CreateSummarize(_TjlCustomerSummarize);
                    _customerSummarizeDto = result;
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                else
                {
                    _customerSummarizeDto.CustomerRegID = _tjlCustomerRegDto.Id;



                    if (tjlb.Contains("职业+健康"))
                    {
                        _customerSummarizeDto.occShEmployeeBMId = (long)customyiSheng.EditValue;
                        _customerSummarizeDto.occEmployeeBMId = (long)customyiSheng.EditValue;
                        _customerSummarizeDto.occCharacterSummary = memoEditHuiZong.Text;
                        _customerSummarizeDto.occAdvice = SbStr;
                        _customerSummarizeDto.occConclusionDate = _commonAppService.GetDateTimeNow().Now;
                        _customerSummarizeDto.occCheckState = (int)SummSate.HasInitialReview;
                    }
                    else
                    {

                        _customerSummarizeDto.ShEmployeeBMId = (long)customyiSheng.EditValue;
                        _customerSummarizeDto.EmployeeBMId = (long)customyiSheng.EditValue;
                        _customerSummarizeDto.CharacterSummary = memoEditHuiZong.Text;
                        _customerSummarizeDto.Advice = SbStr;
                        _customerSummarizeDto.ConclusionDate = _commonAppService.GetDateTimeNow().Now;
                    }

                    _customerSummarizeDto.CheckState = 1;
                    //if (fcxm != "")
                    //{
                    _customerSummarizeDto.ReviewContent = fcxm;
                    //}
                    var result = _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    _customerSummarizeDto = result;
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                //保存职业健康总检
              var occisOK=  SaveOcc();
                if (occisOK == false)
                {
                    return;
                }
                //保存复查
                SaveReview();
                //更新患者体检信息表
                if (tjlb.Contains("职业+健康"))
                {
                    _tjlCustomerRegDto.OccSummSate = (int)SummSate.HasInitialReview;
                }
                else
                {
                    _tjlCustomerRegDto.SummSate = (int)SummSate.HasInitialReview;
                }
                _tjlCustomerRegDto.SummLocked = 2;
                _tjlCustomerRegDto.CSEmployeeId = (long)customyiSheng.EditValue;
                _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);


               
                //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                //editCustomerRegStateDto.SummSate = (int)SummSate.HasInitialReview;
                //editCustomerRegStateDto.SummLocked = 2;
                //editCustomerRegStateDto.CSEmployeeId = (long)customyiSheng.EditValue;
                //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);


            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
            }
        }
        //保存职业健康方法
        private bool SaveOcc()
        {
            //职业健康
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            if (tjlb!=null && (Variables.ISZYB == "1" && (tjlb.Contains("职业") || tjlb.Contains("放射"))) || Variables.ISZYB == "2")
            {
                if (_OcccustomerSummarizeDto == null)
                {
                    _OcccustomerSummarizeDto = new InputOccCusSumDto();
                    _OcccustomerSummarizeDto.CraetOccCustomerOccDiseasesDto = new List<CraetOccCustomerOccDiseasesDto>();
                    _OcccustomerSummarizeDto.CreatOccCustomerContraindicationDto = new List<CreatOccCustomerContraindicationDto>();
                }
                if (_OcccustomerSummarizeDto.CreatOccCustomerSumDto == null)
                {
                    _OcccustomerSummarizeDto.CreatOccCustomerSumDto = new CreatOccCustomerSumDto();
                   
                }
                if (_OcccustomerSummarizeDto.CraetOccCustomerOccDiseasesDto == null)
                {
                    _OcccustomerSummarizeDto.CraetOccCustomerOccDiseasesDto = new List<CraetOccCustomerOccDiseasesDto>();
                }
                if (_OcccustomerSummarizeDto.CreatOccCustomerContraindicationDto == null)
                {
                    _OcccustomerSummarizeDto.CreatOccCustomerContraindicationDto = new List<CreatOccCustomerContraindicationDto>();
                }
                if (_OcccustomerSummarizeDto.OccCustomerHazardSumDto == null)
                {
                    _OcccustomerSummarizeDto.OccCustomerHazardSumDto = new List<OccCustomerHazardSumDto>();
                }
                _OcccustomerSummarizeDto.CreatOccCustomerSumDto.CustomerRegBMId = _tjlCustomerRegDto.Id;
                _OcccustomerSummarizeDto.CreatOccCustomerSumDto.CustomerSummarizeId = _customerSummarizeDto.Id;
                _OcccustomerSummarizeDto.CreatOccCustomerSumDto.Standard = "GBZ_188-2014_职业健康监护技术规范";
        
              

                //危害因素职业病
                _OcccustomerSummarizeDto.OccCustomerHazardSumDto = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
                if (_OcccustomerSummarizeDto.OccCustomerHazardSumDto != null &&
                    (_OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() == 0
                    || _OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() > 1))
                {
                    MessageBox.Show("必须选择一个主结论！");
                    return false;
                }
                else
                {
                    ;
                    _OcccustomerSummarizeDto = _inspectionTotalService.SaveOccSum(_OcccustomerSummarizeDto);
                    return true;
                }
            }
            return true;
        }
        //审核总检按钮
        private void simpleButtonShenHe_Click(object sender, EventArgs e)
        {
            if (customyiSheng.EditValue == null)
            {
                ShowMessageSucceed("请选择总检医生。");
                return;
            }
            //职业健康
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            if (tjlb != null && (Variables.ISZYB == "1" && tjlb.Contains("职业") && !string.IsNullOrEmpty(_tjlCustomerRegDto.RiskS)) || Variables.ISZYB == "2")
            {   //危害因素职业病
                _OcccustomerSummarizeDto.OccCustomerHazardSumDto = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
                if (_OcccustomerSummarizeDto.OccCustomerHazardSumDto != null &&
                    (_OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() == 0
                    || _OcccustomerSummarizeDto.OccCustomerHazardSumDto.Where(p => p.IsMain == 1).Count() > 1))
                {
                    MessageBox.Show("必须选择一个主结论！");
                    return;
                }
            }
            if (tjlb.Contains("职业+健康"))
            {
                if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.Audited)
                {
                    ShowMessageSucceed("已审核总检。");
                    return;
                }
                else if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.HasInitialReview)
                {
                    Save();
                }
                else if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.OccSummSate == null)
                {
                    Save();
                }
            }
            else
            {
                if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
                {
                    ShowMessageSucceed("已审核总检。");
                    return;
                }
                else if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview)
                {
                    Save();
                }
                else if (_tjlCustomerRegDto.SummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.SummSate == null)
                {
                    Save();
                }
            }

            if (_customerSummarizeDto != null)
            {
                //更新建议汇总
                //职业健康            
                if (tjlb.Contains("职业+健康"))
                {
                    _customerSummarizeDto.occShEmployeeBMId = (long)customyiSheng.EditValue;
                    _customerSummarizeDto.occEmployeeBMId = (long)customyiSheng.EditValue;
                    _customerSummarizeDto.occCheckState = (int)SummSate.HasInitialReview;
                    _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                }
                else
                {

                    _customerSummarizeDto.ShEmployeeBMId = (long)customyiSheng.EditValue;
                    _customerSummarizeDto.EmployeeBMId = (long)customyiSheng.EditValue;
                    _customerSummarizeDto.CheckState = 2;
                    _inspectionTotalService.CreateSummarize(_customerSummarizeDto);

                }
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                createOpLogDto.LogText = "保存总检审核";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.SumId;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            if (_tjlCustomerRegDto != null)
            {
                if (tjlb.Contains("职业+健康"))
                {
                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = (long)customyiSheng.EditValue;
                    _tjlCustomerRegDto.OccSummSate = (int)SummSate.Audited;
                    //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);
                    OccEditCustomerRegStateDto editCustomerRegStateDto = new OccEditCustomerRegStateDto();
                    editCustomerRegStateDto.SummLocked = _tjlCustomerRegDto.SummLocked;
                    editCustomerRegStateDto.CSEmployeeId = _tjlCustomerRegDto.CSEmployeeId;
                    editCustomerRegStateDto.FSEmployeeId = (long)customyiSheng.EditValue;
                    editCustomerRegStateDto.OccSummSate = (int)SummSate.Audited;
                    editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    _inspectionTotalService.UpdateOccTjlCustomerRegState(editCustomerRegStateDto);
                }
                else
                {
                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = (long)customyiSheng.EditValue;
                    _tjlCustomerRegDto.SummSate = (int)SummSate.Audited;
                    //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);
                    EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    editCustomerRegStateDto.SummLocked = _tjlCustomerRegDto.SummLocked;
                    editCustomerRegStateDto.CSEmployeeId = _tjlCustomerRegDto.CSEmployeeId;
                    editCustomerRegStateDto.FSEmployeeId = (long)customyiSheng.EditValue;
                    editCustomerRegStateDto.SummSate = (int)SummSate.Audited;

                    editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    _inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);
                }
            }
            LoadDataState();
            // ShowMessageSucceed("总检审核完成。");
            alertInfo.Show(this, "提示!", "总检审核完成。");
            //SimpleButtonSaveClick?.Invoke(sender, e);
            var zjxyg = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 131);
            if (zjxyg == null || zjxyg?.Remarks == "Y")
            {
                getNext();
            }
        }

        //清除总检按钮
        private void simpleButtonQingChu_Click(object sender, EventArgs e)
        {
            //职业健康            
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            if (tjlb.Contains("职业+健康"))
            {
                if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.Audited)
                {
                    var IsTrue = MessageBox.Show("是否要清除总检审核。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新建议汇总
                    if (_customerSummarizeDto != null)
                    {
                        _customerSummarizeDto.CheckState = 1;
                        _customerSummarizeDto.ShEmployeeBMId = null;
                        _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    }

                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _tjlCustomerRegDto.OccSummSate = (int)SummSate.HasInitialReview;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.SummSate = (int)SummSate.HasInitialReview;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    LoadDataState();
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    // ShowMessageSucceed("已清除总检审核。");
                    alertInfo.Show(this, "提示!", "已清除总检审核。");
                }
                else if (_tjlCustomerRegDto.OccSummSate == (int)SummSate.HasInitialReview)
                {
                    var IsTrue = MessageBox.Show("是否要清除总检。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新建议汇总
                    if (_customerSummarizeDto != null)
                    {
                        _customerSummarizeDto.CheckState = 1;
                        _customerSummarizeDto.EmployeeBMId = null;
                        _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    }

                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _tjlCustomerRegDto.OccSummSate = (int)SummSate.NotAlwaysCheck;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    LoadDataState();
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    ShowMessageSucceed("已清除总检。");
                }
                else
                {
                    var IsTrue = MessageBox.Show("是否要清除总检数据。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新体检信息 （清除总检）
                    _tjlCustomerRegDto.OccSummSate = (int)SummSate.NotAlwaysCheck;
                    _tjlCustomerRegDto.SummLocked = 2;
                    _tjlCustomerRegDto.CSEmployeeId = null;
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    //editCustomerRegStateDto.SummLocked = 2;
                    //editCustomerRegStateDto.CSEmployeeId = null;
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    //删除建议表（多条）
                    _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id , IsZYB=1 });
                    //删除建议汇总表 （单条）
                    _inspectionTotalService.DelTjlCustomerSummarize(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id, IsZYB=1 });
                    //删除职业总检
                    if (tabNavigationPage3.PageVisible == true)
                    {
                        _inspectionTotalService.DelOccSum(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id });
                    }
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "清除总检数据";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    _customerSummarizeDto = null;
                    //重新加载界面
                    LoadDataState();
                    //清空建议列表
                    //清空职业总检
                    if (tabNavigationPage3.PageVisible == true)
                    {
                        EntityDto<Guid> inputzyb = new EntityDto<Guid>();
                        inputzyb.Id = _tjlCustomerRegDto.Id;
                        var _OcccustomerSummarizeDto = _inspectionTotalService.GetCusOccSumByRegId(inputzyb);

                        gridHazardSum.DataSource = _OcccustomerSummarizeDto.OccCustomerHazardSumDto;
                    }
                    memoEditHuiZong.Text = string.Empty;
                    this.richEditControl1.Text = string.Empty;
                    txtPrivacy1.Text = string.Empty;
                    txtPrivacy2.Text = string.Empty;
                    treeListZhenDuan.DataSource = null;
                    treeListZhenDuan.RefreshDataSource();
                    _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();
                    tabPane2.SelectedPageIndex = 0;
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    ShowMessageSucceed("已清除总检。");
                }
            }
            else
            {
                if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
                {
                    var IsTrue = MessageBox.Show("是否要清除总检审核。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新建议汇总
                    if (_customerSummarizeDto != null)
                    {
                        _customerSummarizeDto.CheckState = 1;
                        _customerSummarizeDto.ShEmployeeBMId = null;
                        _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    }

                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _tjlCustomerRegDto.SummSate = (int)SummSate.HasInitialReview;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.SummSate = (int)SummSate.HasInitialReview;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    LoadDataState();
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    // ShowMessageSucceed("已清除总检审核。");
                    alertInfo.Show(this, "提示!", "已清除总检审核。");
                }
                else if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview)
                {
                    var IsTrue = MessageBox.Show("是否要清除总检。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新建议汇总
                    if (_customerSummarizeDto != null)
                    {
                        _customerSummarizeDto.CheckState = 1;
                        _customerSummarizeDto.EmployeeBMId = null;
                        _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    }

                    //更新患者体检信息表
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _tjlCustomerRegDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    LoadDataState();
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    ShowMessageSucceed("已清除总检。");
                }
                else
                {
                    var IsTrue = MessageBox.Show("是否要清除总检数据。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (!IsTrue)
                        return;
                    //更新体检信息 （清除总检）
                    _tjlCustomerRegDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    _tjlCustomerRegDto.SummLocked = 2;
                    _tjlCustomerRegDto.CSEmployeeId = null;
                    _tjlCustomerRegDto.FSEmployeeId = null;
                    _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);

                    //EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                    //editCustomerRegStateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                    //editCustomerRegStateDto.SummLocked = 2;
                    //editCustomerRegStateDto.CSEmployeeId = null;
                    //editCustomerRegStateDto.FSEmployeeId = null;
                    //editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                    //_inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);

                    //删除建议表（多条）
                    _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id });
                    //删除建议汇总表 （单条）
                    _inspectionTotalService.DelTjlCustomerSummarize(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id });
                    //删除职业总检
                    if (tabNavigationPage3.PageVisible == true)
                    {
                        _inspectionTotalService.DelOccSum(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id });
                    }
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "清除总检数据";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    _customerSummarizeDto = null;
                    //重新加载界面
                    LoadDataState();
                    //清空建议列表
                    //清空职业总检
                    if (tabNavigationPage3.PageVisible == true)
                    {
                        EntityDto<Guid> inputzyb = new EntityDto<Guid>();
                        inputzyb.Id = _tjlCustomerRegDto.Id;
                        var _OcccustomerSummarizeDto = _inspectionTotalService.GetCusOccSumByRegId(inputzyb);

                        gridHazardSum.DataSource = _OcccustomerSummarizeDto.OccCustomerHazardSumDto;
                    }
                    memoEditHuiZong.Text = string.Empty;
                    this.richEditControl1.Text = string.Empty;
                    txtPrivacy1.Text = string.Empty;
                    txtPrivacy2.Text = string.Empty;
                    treeListZhenDuan.DataSource = null;
                    treeListZhenDuan.RefreshDataSource();
                    _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();
                    if (tabNavigationPage8.PageVisible != false)
                    {
                        tabPane2.SelectedPageIndex = 0;
                    }
                    //SimpleButtonSaveClick?.Invoke(sender, e);
                    ShowMessageSucceed("已清除总检。");
                }
            }
        }

        //总检退回按钮
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            var _inspectionReturn = new InspectionReturn(_tjlCustomerRegDto, _customerRegItemList);
            _inspectionReturn.TopMost = true;
            _inspectionReturn.Show();
        }

        //主表格样式设置
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    var data = (TjlCustomerRegItemDto)gridViewCustomerRegItems.GetRow(e.RowHandle);

            //    //H偏高 HH超高L偏低 LL 超低M正常P异常
            //    switch (data.Symbol)
            //    {
            //        case "H":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //        case "HH":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //        case "L":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //        case "LL":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //        case "P":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //        case "↑":
            //            e.Appearance.ForeColor = Color.Red; // 改变行字体颜色
            //            break;
            //    }

            //    //e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
            //}

            //DevExpress.Utils.AppearanceDefault appNotPass1 = new DevExpress.Utils.AppearanceDefault(Color.Black, Color.Salmon, Color.Empty, Color.SeaShell, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            //DevExpress.Utils.AppearanceDefault appNotPass2 = new DevExpress.Utils.AppearanceDefault(Color.Black, Color.Yellow, Color.Empty, Color.SeaShell, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
        }

        //报告预览
        private void simpleButtonYuLan_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能预览报告！");
                //return;
            }
            var printReport = new PrintReportNew();
            //printReport.StrReportTemplateName = "000";
            var cusNameInput = new CusNameInput();
            cusNameInput.Id = _tjlCustomerRegDto.Id;
            printReport.cusNameInput = cusNameInput;
            var MBNamels = DefinedCacheHelper
              .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            string[] mbls = MBNamels.Split('|');
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            //var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
            //if (tjlb != null && tjlb.Contains("职业"))
            //{
            //    var zybmc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2);
            //    if (zybmc == null)
            //    {
            //        MessageBox.Show("请在字典，个人报告设置，中维护编码为2的职业健康模板名称");
            //        return;
            //    }
            // var   strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2).Remarks;
            //    printReport.Print(true, "", strBarPrintName);
            //}
            //else
            //{
            //    printReport.Print(true, "", mbls[0]);
            //}

            if (string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()))
            {
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                if (tjlb != null && tjlb.Contains("职业"))
                {
                    var zybmc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2);
                    if (zybmc == null)
                    {
                        MessageBox.Show("请在字典，个人报告设置，中维护编码为2的职业健康模板名称");
                        return;
                    }
                    var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2).Remarks;
                    printReport.Print(true, "", strBarPrintName);
                }
                else
                {
                    printReport.Print(true, "", mbls[0]);
                }
            }

            else if (listBoxControlTemplates.EditValue?.ToString() == "根据体检类别匹配")
            {
                printReport.Print(true, "", "", "0");
            }
            else
            { printReport.Print(true, "", listBoxControlTemplates.EditValue.ToString(), "0"); }
            //printReport.Print();
            //printReport.PrintingEmdicalCertificate(true);
        }

        //图片列按钮事件
        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (dockPanelImg.Visibility == DockVisibility.Visible)
            {
                XtraMessageBox.Show("窗体已打开请勿重复打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CustomerPicSys = 0;
            var RowData = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
            CustomerItemPicSys = new List<CustomerItemPicDto>();
            CustomerItemPicSys = CustomerItemPicAllSys.Where(o => o.ItemBMID == RowData.Id).ToList();
            if (CustomerItemPicSys != null && CustomerItemPicSys.Count > 0)
            {
                Guid PicGuid = CustomerItemPicSys[CustomerPicSys].PictureBM.Value;
                var PicInfo = _pictureController.GetUrl(PicGuid);
                if (PicInfo.RelativePath.Contains(".pdf"))
                {
                    string strnew = System.AppDomain.CurrentDomain.BaseDirectory +
                        "image";
                    if (!Directory.Exists(strnew))
                    {
                        Directory.CreateDirectory(strnew);
                    }
                    strnew = strnew + "\\" + Path.GetFileNameWithoutExtension(PicInfo.RelativePath) + ".pdf";
                    if (!File.Exists(strnew))
                    {
                        HttpDldFile.Download(PicInfo.RelativePath, strnew);
                    }
                    frmpdfShow frmpdf = new frmpdfShow();
                    frmpdf.strpdfPath = strnew;
                    frmpdf.ShowDialog();
                }
                else
                {
                    pictureEditImg.Image = ImageHelper.GetUriImage(new Uri(PicInfo.RelativePath));
                    labelControlZongShu.Text = CustomerItemPicSys.Count().ToString();
                    labelControlDangQian.Text = "1";
                    labelControlZuHeMingCheng.Text = RowData.ItemGroupName;
                    dockPanelImg.Visibility = DockVisibility.Visible;
                }


            }
            else
            {
                dockPanelImg.Visibility = DockVisibility.Visible;
                labelControlZongShu.Text = "0";
                labelControlDangQian.Text = "0";
                labelControlZuHeMingCheng.Text = "";
            }
        }

        //图片上一张
        private void pictureEditLeft_Click(object sender, EventArgs e)
        {
            if (CustomerItemPicSys != null && CustomerItemPicSys.Count != 0)
            {
                if (CustomerPicSys == 0)
                    CustomerPicSys = CustomerItemPicSys.Count - 1;
                else
                    CustomerPicSys = CustomerPicSys - 1;

                var Pic = Guid.Parse(CustomerItemPicSys[CustomerPicSys].PictureBM.ToString());
                var result = _pictureController.GetUrl(Pic);
                using (var stream = ImageHelper.GetUriImageStream(new Uri(result.RelativePath)))
                {
                    pictureEditImg.Image = Image.FromStream(stream);
                }

                var ItemGroup = Guid.Parse(CustomerItemPicSys[CustomerPicSys].CustomerItemGroupID.ToString());

                labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
            }
        }

        //图片下一张
        private void pictureEditRight_Click(object sender, EventArgs e)
        {
            if (CustomerItemPicSys != null && CustomerItemPicSys.Count != 0)
            {
                if (CustomerPicSys == CustomerItemPicSys.Count - 1)
                    CustomerPicSys = 0;
                else
                    CustomerPicSys = CustomerPicSys + 1;
                var Pic = Guid.Parse(CustomerItemPicSys[CustomerPicSys].PictureBM.ToString());
                var result = _pictureController.GetUrl(Pic);
                pictureEditImg.Image = ImageHelper.GetUriImage(new Uri(result.RelativePath));

                var ItemGroup = Guid.Parse(CustomerItemPicSys[CustomerPicSys].CustomerItemGroupID.ToString());
                labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
            }
        }

        //所有图片复选框
        private void checkEditAllImg_CheckedChanged(object sender, EventArgs e)
        {

        }

        //窗体关闭事件
        private void FrmInspectionTotal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //因为芜湖需要刷新暂时先这样处理
            var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 171)?.Remarks;
            if (IsYC != null && IsYC == "Y")
            {
                
            }
            else
            {
                SimpleButtonSaveClick?.Invoke(sender, e);
            }
            //SimpleButtonSaveClick?.Invoke(sender, e);
        }

        //改变图片按钮列显示状态
        private void gridViewCustomerRegItems_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnImg)
            {

                var RowData = (CustomerRegInspectItemDto)gridViewCustomerRegItems.GetRow(e.RowHandle);
                var ImgCount = CustomerItemPicAllSys.Where(o => o.ItemBMID == RowData.Id)?.ToList();
                //if (RowData.ItemName == "脉搏")
                if (ImgCount == null || ImgCount.Count == 0) //正式判断
                {
                    var item = new RepositoryItemButtonEdit();
                    //item.ButtonsStyle = BorderStyles.UltraFlat;
                    //item.TextEditStyle = TextEditStyles.HideTextEditor;
                    //item.Buttons.Clear();
                    //EditorButton bt = new EditorButton();
                    //bt.Kind = ButtonPredefines.Glyph;
                    ////bt.Appearance.BackColor = Color.FromArgb(233, 234, 237);
                    //bt.Caption = e.CellValue.ToString();
                    //bt.IsDefaultButton = false;
                    //item.Buttons.Add(bt);
                    e.RepositoryItem = item;
                }
                else
                {

                }

            }
        }
        //打印报告按钮事件
        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            if (Variables.ISReg == "0")
            {
                //XtraMessageBox.Show("试用版本，不能打印报告！");
                //return;
            }
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;

            string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 300)?.Remarks;
            if (!string.IsNullOrEmpty(strwjshow) && strwjshow=="Y")
            {
                //职业健康 
                if (tjlb.Contains("职业+健康"))
                {
                    if (_tjlCustomerRegDto.OccSummSate != (int)SummSate.Audited)
                    {
                        MessageBox.Show("未审核不能打印报告！");
                        return;
                    }
                }
                else
                {
                    if (_tjlCustomerRegDto.SummSate != (int)SummSate.Audited)
                    {
                        MessageBox.Show("未审核不能打印报告！");
                        return;
                    }
                }
            }
            var printReport = new PrintReportNew();
            var cusNameInput = new CusNameInput();
            cusNameInput.Id = _tjlCustomerRegDto.Id;
            printReport.cusNameInput = cusNameInput;
            var MBNamels = DefinedCacheHelper
             .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            string[] mbls = MBNamels.Split('|');
            
           


            if (string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()))
            {
                if (tjlb != null && tjlb.Contains("职业"))
                {
                    var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2).Remarks;
                    printReport.Print(true, "", strBarPrintName);
                }
                else
                {
                    printReport.Print(false, "", mbls[0]);
                }
            }
            else if (listBoxControlTemplates.EditValue?.ToString() == "根据体检类别匹配")
            {
                printReport.Print(false, "", "", "0");
            }
            else
            { printReport.Print(false, "", listBoxControlTemplates.EditValue?.ToString(), "0"); }

            string mb = "";
            if (!string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()) && listBoxControlTemplates.EditValue.ToString().Contains("职业"))
            { mb = "职业"; }
            _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM  { Id = _tjlCustomerRegDto.Id, Name= mb });
            //日志
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
            createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
            createOpLogDto.LogText = "打印体检报告" ;
            createOpLogDto.LogDetail = "";
            createOpLogDto.LogType = (int)LogsTypes.PrintId;
            _commonAppService.SaveOpLog(createOpLogDto);
        }

        //切换选中行单元格事件
        private void gridViewCustomerRegItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (isfist == false)
            {
                var colName = gridViewCustomerRegItems.FocusedColumn.Caption;
                if (gridViewCustomerRegItems.IsGroupRow(gridViewCustomerRegItems.FocusedRowHandle))
                {
                    var childRowHandle = gridViewCustomerRegItems.GetChildRowHandle(gridViewCustomerRegItems.FocusedRowHandle, 0);
                    colName = gridViewCustomerRegItems.IsGroupRow(childRowHandle) ? gridColumn3.Caption : gridColumn5.Caption;
                }
                if (colName == "科室名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    if (dto != null)
                    {
                        labelKeShi.Text = dto.DepartmentName;
                        labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                        labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                        yc12.Text = "科室小结：";
                        //memoEditXiaoJie.Text =
                        //    _customerRegDto.CustomerDepSummary.FirstOrDefault(n =>
                        //        n.DepartmentBMId == dto.DepartmentBM.Id) == null
                        //        ? "未填写内容"
                        //        : _customerRegDto.CustomerDepSummary
                        //            .FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id).CharacterSummary;

                        memoEditXiaoJie.Text =
                            _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentId) == null ? "未填写内容"
                                : _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentId).CharacterSummary;
                    }
                }
                else if (colName == "组合名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    labelKeShi.Text = dto.DepartmentName;
                    labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                    labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                    yc12.Text = "组合小结：";
                    memoEditXiaoJie.Text = dto.ItemGroupSum;
                }
                else if (colName == "项目名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    if (dto != null)
                    {
                        labelKeShi.Text = dto.DepartmentName;
                        labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                        labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                        yc12.Text = "项目小结：";
                        var sbStr = string.Format("{0}{1}{2}{3}{4}", dto.ItemSum, Environment.NewLine, "-------------------------------", Environment.NewLine, dto.ItemDiagnosis);
                        memoEditXiaoJie.Text = sbStr;
                    }
                }
            }
        }
        //切换选中列单元格事件
        private void gridViewCustomerRegItems_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (isfist == false)
            {
                var colName = gridViewCustomerRegItems.FocusedColumn.Caption;
                if (gridViewCustomerRegItems.IsGroupRow(gridViewCustomerRegItems.FocusedRowHandle))
                {
                    var childRowHandle = gridViewCustomerRegItems.GetChildRowHandle(gridViewCustomerRegItems.FocusedRowHandle, 0);
                    colName = gridViewCustomerRegItems.IsGroupRow(childRowHandle) ? gridColumn3.Caption : gridColumn5.Caption;
                }
                if (colName == "科室名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    if (dto != null)
                    {
                        labelKeShi.Text = dto.DepartmentName;
                        labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                        labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                        yc12.Text = "科室小结：";
                        //memoEditXiaoJie.Text =
                        //    _customerRegDto.CustomerDepSummary.FirstOrDefault(n =>
                        //        n.DepartmentBMId == dto.DepartmentBM.Id) == null
                        //        ? "未填写内容"
                        //        : _customerRegDto.CustomerDepSummary
                        //            .FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id).CharacterSummary;

                        memoEditXiaoJie.Text =
                            _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentId) == null ? "未填写内容"
                                : _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentId).CharacterSummary;
                    }
                }
                else if (colName == "组合名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    if (dto == null)
                        return;
                    labelKeShi.Text = dto.DepartmentName;
                    labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                    labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                    yc12.Text = "组合小结：";
                    memoEditXiaoJie.Text = dto.ItemGroupSum;
                }
                else if (colName == "项目名称")
                {
                    var dto = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
                    if (dto != null)
                    {
                        labelKeShi.Text = dto.DepartmentName;
                        labelJianChaYiSheng.Text = dto.InspectEmployeeBMName;
                        labelJieLunYiSheng.Text = dto.CheckEmployeeBMName;
                        yc12.Text = "项目小结：";
                        var sbStr = string.Format("{0}{1}{2}{3}{4}", dto.ItemSum, Environment.NewLine, "-------------------------------", Environment.NewLine, dto.ItemDiagnosis);
                        memoEditXiaoJie.Text = sbStr;
                    }
                }
            }
        }
        //项目结果行列样式控制事件
        private void gridViewCustomerRegItems_RowCellStyle(object sender, RowCellStyleEventArgs e)
       {
            if (e.Column == gridColumnJieGuo)
            {
                var data = (CustomerRegInspectItemDto)gridViewCustomerRegItems.GetRow(e.RowHandle);
                if (data == null)
                {
                    return;
                }
                if (data.Symbol != SymbolHelper.SymbolFormatter(Symbol.Normal) && !string.IsNullOrWhiteSpace(data.Symbol))
                {
                    if (data.Symbol == SymbolHelper.SymbolFormatter(Symbol.Low) 
                        || data.Symbol == SymbolHelper.SymbolFormatter(Symbol.UltraLow))
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            var Grvup = _customerRegItemList.Where(o => o.ProcessState == 4).ToList();
            if (Grvup.Count > 0)
            {
                var datas = (CustomerRegInspectItemDto)gridViewCustomerRegItems.GetRow(e.RowHandle);
                if (e.Column == gridColumn6)
                {
                    if (datas.ProcessState == 4)
                    {
                        e.Appearance.BackColor = Color.Red;
                    }                                     
                }                
            }
        } 
        //tab页切换事件
        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane2.SelectedPageIndex == 0)
            {
                //panelControl1.Visible = false;
                this.memoEditHuiZong.Text = richEditControl1.Text;
            }
            else
            {
               // panelControl1.Visible = true;
                //this.richEditControl1.Enabled = false;
                this.richEditControl1.Text = memoEditHuiZong.Text;
            }
        }

        //将下拉框选择的总检诊断编码数据转换为记录表数据
        public TjlCustomerSummarizeBMDto SummarizeBMToJL(SummarizeAdviceDto bmInfo,bool Ispr=false)
        {
            TjlCustomerSummarizeBMDto info = new TjlCustomerSummarizeBMDto();
            if (bmInfo == null)
                return info;
            info.Id = Guid.NewGuid(); //转换时赋ID会导致保存时报错，原因由于ID重复
            info.CustomerRegID = _tjlCustomerRegDto.Id;
            info.CustomerReg = null;
            info.SummarizeName = bmInfo.AdviceName;
            info.SummarizeAdvice = null;
            info.SummarizeType = 1;
            info.Advice = bmInfo.SummAdvice;
            info.SummarizeOrderNum = bmInfo.OrderNum;
            if (bmInfo.IsTestInfo == 1)
            {
                info.SummarizeAdviceId = null;
            }
            else
            {
                info.SummarizeAdviceId = bmInfo.Id;
            }
            info.IsPrivacy = Ispr;
            return info;
        }

        //将传入的集合重新排序
        public List<TjlCustomerSummarizeBMDto> OrderByList(List<TjlCustomerSummarizeBMDto> list)
        {
            var ParentInfo = list.Where(n => n.ParentAdviceId == null || n.ParentAdviceId == Guid.Empty)?.OrderBy(l => l.SummarizeOrderNum).ToList();
            for (int i = 0; i < ParentInfo.Count(); i++)
            {
                ParentInfo[i].SummarizeOrderNum = i + 1;
                var ChildrenInfo = list.Where(n => n.ParentAdviceId == ParentInfo[i].Id)?.OrderBy(l => l.SummarizeOrderNum).ToList();
                if (ChildrenInfo != null && ChildrenInfo.Count() > 0)
                {
                    for (int j = 0; j < ChildrenInfo.Count(); j++)
                    {
                        ChildrenInfo[j].SummarizeOrderNum = j + 1;
                    }
                }
            }

            //list = list.OrderBy(l => l.SummarizeOrderNum).ToList();
            //for (int i = 0; i < list.Count(); i++)
            //{
            //    list[i].SummarizeOrderNum = i + 1;
            //}
            return list.OrderBy(l => l.SummarizeOrderNum).ToList();
        }
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
            alertControl.Show(this, "复制提示", $"“{labelControl.Text}”已复制到剪贴板！");
        }

        //诊断内容列，多行文本框控件鼠标双击文本全选
        private void repositoryItemMemoEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                MemoEdit edit = sender as MemoEdit;
                edit.Select(0, edit.Text.Length);
            }
        }      

        private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                //上
                var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                if (list == null)
                    return;
                var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
                if (currentItem == null)
                    return;
                if (currentItem == list.FirstOrDefault())
                    return;
                var currentOrder = currentItem.SummarizeOrderNum;
                list = list.OrderBy(n => n.ParentAdviceId).ToList();
                var currentIndex = list.IndexOf(currentItem);
                if (currentItem.ParentAdviceId != null && currentItem.ParentAdviceId != Guid.Empty && currentItem.SummarizeOrderNum == 1)
                    return;
                if (list[currentIndex - 1].ParentAdviceId == null || list[currentIndex - 1].ParentAdviceId == Guid.Empty)
                {
                    list[currentIndex].SummarizeOrderNum = list[currentIndex - 1].SummarizeOrderNum;
                    list[currentIndex - 1].SummarizeOrderNum = currentOrder;
                    treeListZhenDuan.DataSource = OrderByList(list);
                   // treeListZhenDuan.BestFitColumns();
                    treeListZhenDuan.RefreshDataSource();
                    treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex - 1];
                }
                else
                {
                    var ParentIndex = list.IndexOf(list.FirstOrDefault(n => n.Id == list[currentIndex - 1].ParentAdviceId));
                    list[currentIndex].SummarizeOrderNum = list[ParentIndex].SummarizeOrderNum;
                    list[currentIndex - 1].SummarizeOrderNum = list[currentIndex].SummarizeOrderNum + 1;
                    treeListZhenDuan.DataSource = OrderByList(list);
                   // treeListZhenDuan.BestFitColumns();
                    treeListZhenDuan.RefreshDataSource();
                    treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[ParentIndex + 1];
                }
                treeListZhenDuan.ExpandAll();
            }
            else if (e.Button.Index == 1)
            {
                //下
                var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                if (list == null)
                    return;
                var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
                if (currentItem == null)
                    return;
                if (currentItem == list.Where(n => n.ParentAdviceId == null || n.ParentAdviceId == Guid.Empty).OrderBy(n => n.SummarizeOrderNum).LastOrDefault())
                    return;
                var currentOrder = currentItem.SummarizeOrderNum;
                list = list.OrderBy(n => n.ParentAdviceId).ToList();
                var currentIndex = list.IndexOf(currentItem);
                if (currentIndex + 1 >= list.Count)
                    return;
                list[currentIndex].SummarizeOrderNum = list[currentIndex + 1].SummarizeOrderNum;
                list[currentIndex + 1].SummarizeOrderNum = currentOrder;
                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex + 1];
                treeListZhenDuan.ExpandAll();
            }
            else if (e.Button.Index == 2)
            {
                //置顶
                var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                if (list == null)
                    return;
                var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
                if (currentItem == null)
                    return;
                if (currentItem == list.FirstOrDefault())
                    return;
                var currentOrder = currentItem.SummarizeOrderNum;
                list = list.OrderBy(n => n.ParentAdviceId).ToList();
                var currentIndex = list.IndexOf(currentItem);

                list[currentIndex].SummarizeOrderNum = 0;
                list[0].SummarizeOrderNum = 1;
                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[0];
                treeListZhenDuan.ExpandAll();
            }
            else if (e.Button.Index == 3)
            {
                //删除
                //MessageBox.Show("删除");
                var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
                if (currentItem == null)
                    return;

                var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                if (list == null)
                    return;
                //list = list.OrderBy(n => n.ParentAdviceId).ToList();
                var currentIndex = list.IndexOf(currentItem);
                //将删除行的所有子级行变为普通行
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ParentAdviceId == currentItem.Id)
                    {
                        list[i].ParentAdviceId = null;
                        list[i].SummarizeName = list[i].SummarizeName;
                    }
                }

                _CustomerSummarizeList.Remove(currentItem);


                treeListZhenDuan.DataSource = OrderByList(_CustomerSummarizeList);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                if (currentIndex > 1)
                {
                    treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex - 1];
                }
                treeListZhenDuan.ExpandAll();
            }
            else if (e.Button.Index == 4)
            {
                //设为子级
                var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                if (list == null)
                    return;
                var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
                if (currentItem == null)
                    return;
                if (currentItem == list.OrderBy(n => n.ParentAdviceId).FirstOrDefault())
                    return;
                //list = list.OrderBy(n => n.ParentAdviceId).ToList();
                var currentIndex = list.IndexOf(currentItem);
                if (currentItem.ParentAdviceId != Guid.Empty && currentItem.ParentAdviceId != null)
                {
                    //已经是子级，再次点击 取消子级
                    currentItem.ParentAdviceId = null;
                    currentItem.SummarizeName = currentItem.SummarizeName;
                    currentItem.SummarizeOrderNum = list.Count + 1;

                    treeListZhenDuan.DataSource = OrderByList(list);
                   // treeListZhenDuan.BestFitColumns();
                    treeListZhenDuan.RefreshDataSource();
                    treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[treeListZhenDuan.GetNodeList().Count - 1];
                }
                else
                {
                    //判断当前行是父级 还是 普通行
                    var ParentInfo = list.FirstOrDefault(n => n.ParentAdviceId == currentItem.Id);
                    if (ParentInfo != null)
                    {
                        //父级
                        list = list.OrderBy(n => n.ParentAdviceId).ToList();
                        currentIndex = list.IndexOf(currentItem);
                        if (list[currentIndex - 1].ParentAdviceId != Guid.Empty && list[currentIndex - 1].ParentAdviceId != null)
                        {
                            //上一行也是子级，将取它得父级ID
                            list[currentIndex].ParentAdviceId = list[currentIndex - 1].ParentAdviceId;
                            list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                        }
                        else
                        {
                            //上一行是父级，直接取ID
                            list[currentIndex].ParentAdviceId = list[currentIndex - 1].Id;
                            list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                        }
                        //将当前行所有的子级 的 父级ID 更新为 新的父级（当前行的父级）
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].ParentAdviceId == list[currentIndex].Id)
                            {
                                list[i].ParentAdviceId = list[currentIndex].ParentAdviceId;
                            }
                        }
                    }
                    else
                    {
                        if (currentItem.SummarizeAdviceId != null && currentItem.SummarizeAdviceId != Guid.Empty)
                        {
                            list = list.OrderBy(n => n.ParentAdviceId).ToList();
                            currentIndex = list.IndexOf(currentItem);
                        }
                        //普通
                        if (list[currentIndex - 1].ParentAdviceId != Guid.Empty && list[currentIndex - 1].ParentAdviceId != null)
                        {
                            //上一行也是子级，将取它得父级ID
                            list[currentIndex].ParentAdviceId = list[currentIndex - 1].ParentAdviceId;
                            list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                        }
                        else
                        {
                            //上一行是父级，直接取ID
                            list[currentIndex].ParentAdviceId = list[currentIndex - 1].Id;
                            list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                        }
                    }



                    treeListZhenDuan.DataSource = OrderByList(list);
                   // treeListZhenDuan.BestFitColumns();
                    treeListZhenDuan.RefreshDataSource();
                    treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex];
                }
                treeListZhenDuan.ExpandAll();
            }
        }

        private void customGridZhenDuan2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                customGridZhenDuan2.EditValue = null;
        }

        private void customGridZhenDuan2_EditValueChanged(object sender, EventArgs e)
        {
            if (customGridZhenDuan2.GetSelectedDataRow() != null)
            {
                var RowData = (SummarizeAdviceDto)customGridZhenDuan2.GetSelectedDataRow();
                bool IsHave = false;
                foreach (var item in _CustomerSummarizeList)
                {
                    if (item.SummarizeAdviceId == RowData.Id)
                    {
                        IsHave = true;
                    }
                }
                if (!IsHave)
                {
                    var Num = 0;
                    if (treeListZhenDuan.FocusedNode != null)
                    {
                        Num = treeListZhenDuan.GetNodeIndex(treeListZhenDuan.FocusedNode);
                    }
                    RowData.OrderNum = Num + 1;
                    _CustomerSummarizeList.Add(SummarizeBMToJL(RowData));
                    treeListZhenDuan.DataSource = OrderByList(_CustomerSummarizeList);
                   // treeListZhenDuan.BestFitColumns();
                    treeListZhenDuan.RefreshDataSource();
                    if (Num != 0)
                    {
                        treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[Num + 1];
                    }
                    treeListZhenDuan.FocusedColumn = treeListColumnNeiRong;
                    treeListZhenDuan.ShowEditor();
                }
                treeListZhenDuan.ExpandAll();
                customGridZhenDuan2.EditValue = null;
            }
        }

        private void simpleButtonXinZeng2_Click(object sender, EventArgs e)
        {
            var Num = 0;
            if (treeListZhenDuan.FocusedNode != null)
            {
                Num = treeListZhenDuan.GetNodeIndex(treeListZhenDuan.FocusedNode);
            }

            SummarizeAdviceDto newRow = new SummarizeAdviceDto();
            newRow.Id = Guid.NewGuid();
            newRow.OrderNum = Num + 1;
            newRow.AdviceName = string.Empty;
            newRow.SummAdvice = string.Empty;
            newRow.IsTestInfo = 1;
            _CustomerSummarizeList.Add(SummarizeBMToJL(newRow));
            treeListZhenDuan.DataSource = OrderByList(_CustomerSummarizeList);
           // treeListZhenDuan.BestFitColumns();
            treeListZhenDuan.RefreshDataSource();
            if (Num != 0)
            {
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[Num + 1];
            }
            treeListZhenDuan.FocusedColumn = treeListColumnNeiRong;
            treeListZhenDuan.ShowEditor();
            treeListZhenDuan.ExpandAll();
        }

        private void treeListZhenDuan_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == treeListColumnXuHao)
            {
                if (treeListZhenDuan.GetRow(e.Node.Id) is TjlCustomerSummarizeBMDto row)
                {
                    if (row.ParentAdviceId.HasValue)
                    {
                        var parent = _CustomerSummarizeList.Find(r => r.Id == row.ParentAdviceId);
                        e.DisplayText = $"{parent.SummarizeOrderNum}.{row.SummarizeOrderNum}";
                    }
                }
            }
        }
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetInterfaceData_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto != null)
            {
                var alldepart = _customerRegItemList.Select(o => o.DepartmentId).Distinct().ToList();
                if (alldepart == null )
                {
                    alertControl.Show(this, "温馨提示", "没有可获取的数据!");
                    return;
                }
                var departls = new List<Guid>();
                AutoLoading(() =>
                {
                    //获取当前科室id
                    var DepartmentId = Guid.Empty;
                
                   //获取所有科室接口数据
                    
                    foreach (var item in alldepart)
                    {
                        TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                        tdbInterfaceWhere.inactivenum = _tjlCustomerRegDto.CustomerBM;
                        tdbInterfaceWhere.departmentID = item;
                        InterfaceBack Back = new InterfaceBack();

                        Back = _doctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                        if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                        {
                            alertControl.Show(this, "温馨提示", Back.StrBui.ToString());
                        }
                        else
                        {
                            departls.Add(item);

                        }
                    }
                }, "正在读取接口数据并保存!");
                if (departls.Count > 0)
                {
                    AutoLoading(() =>
                    {

                        CreateConclusionDto conclusion = new CreateConclusionDto();
                        conclusion.CustomerBM = _tjlCustomerRegDto.CustomerBM;
                        conclusion.Department = departls;
                        _doctorStationAppService.CreateConclusion(conclusion);
                    }, "正在生成小结!");
                }
                LoadData();
            }
        }
        int iswj = 0;
        private void tabPane2_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane2.SelectedPageIndex == 0)
            {
                //tabNavigationPage2.Show();
                //tabNavigationPage1.Hide();
                tabPane1.SelectedPageIndex = 1;
                this.memoEditHuiZong.Text = richEditControl1.Text;
                txtPrivacy2.Text = txtPrivacy1.Text;
            }
            else
            {
                //panelControl1.Visible = true;
                tabPane1.SelectedPageIndex = 0;
                //this.richEditControl1.Enabled = false;
                this.richEditControl1.Text = memoEditHuiZong.Text;
                txtPrivacy1.Text = txtPrivacy2.Text;
            }

            if (tabPane2.SelectedPageIndex == 2)
            {
                if (_tjlCustomerRegDto != null)
                {
                    if (_tjlCustomerRegDto != null && _tjlCustomerRegDto.Customer != null && _tjlCustomerRegDto.Customer.Id != Guid.Empty)
                    {
                        CustomerGuid = _tjlCustomerRegDto.Customer.Id;
                        CustomerRegListSys = new List<Application.HistoryComparison.Dto.SearchCustomerRegDto>();
                        CustomerItemGroupSys = new List<CustomerItemGroupDto>();
                        CustomerRegItemSys = new List<SearchCustomerRegItemDto>();
                        DictDatetime = new Dictionary<int, string>();
                        CustomerRegListSys = _calendarYearComparison.GetCustomerRegList(new SearchClass() { CustomerId = CustomerGuid });
                        foreach (var itemReg in CustomerRegListSys)
                        {
                            var GroupList = itemReg.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus && o.PayerCat != (int)PayerCatType.NoCharge);
                            CustomerItemGroupSys.AddRange(GroupList);
                            foreach (var itemGroup in GroupList)
                            {
                                CustomerRegItemSys.AddRange(itemGroup.CustomerRegItem);
                            }
                        }
                        DepartmentListSys = CustomerItemGroupSys.Select(r => new DistinctDepartment { Id = r.DepartmentId, Name = r.DepartmentName, OrderNum = r.DepartmentOrder }).Distinct().ToList();
                        ItemGroupListSys = CustomerItemGroupSys.Select(r => new DistinctItemGroup { Id = (Guid)r.ItemGroupBM_Id, Name = r.ItemGroupName, OrderNum = r.ItemGroupOrder, DepartmentId = r.DepartmentId }).Distinct().ToList();
                        ItemListSys = CustomerRegItemSys.Select(r => new DistinctItem { Id = r.ItemId, Name = r.ItemName, OrderNum = r.ItemOrder, DepartmentId = r.DepartmentId, ItemGroupId = r.ItemGroupBMId }).Distinct().ToList();
                        checkedComboBoxEditDepartment.Properties.DataSource = DepartmentListSys.OrderBy(o => o.OrderNum);
                        checkedComboBoxEditItemGroup.Properties.DataSource = ItemGroupListSys.OrderBy(o => o.OrderNum);
                        checkedComboBoxEditItem.Properties.DataSource = ItemListSys.OrderBy(o => o.OrderNum);

                    }
                    else
                    {
                        XtraMessageBox.Show("没有人员信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (tabPane2.SelectedPageIndex ==3)
            {
                //if (_tjlCustomerRegDto != null)
                //{
                //    simpleButton3.Enabled = true;
                //    if (iswj == 0)
                //    {
                //        iswj = 1;
                //        LoadDataWJ();
                //    }
                //    customerregid = _tjlCustomerRegDto.Id;
                //    FrmHistory_Load(sender, e);
                //}
                //else
                //{
                //    simpleButton3.Enabled = false;
                //}
            }
        }
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

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addconlumns()
        {
            int ord = 4;
            ExhibitionTable = new DataTable();
            ExhibitionTable.Columns.Add("DepartmentId", typeof(String));
            ExhibitionTable.Columns.Add("DepartmentName", typeof(String));
            ExhibitionTable.Columns.Add("ItemGroupId", typeof(String));
            ExhibitionTable.Columns.Add("ItemGroupName", typeof(String));
            ExhibitionTable.Columns.Add("ItemId", typeof(String));
            ExhibitionTable.Columns.Add("ItemName", typeof(String));
            ExhibitionTable.Columns.Add("Stand", typeof(String));

            foreach (var item in CustomerRegListSys.OrderByDescending(o => o.LoginDate))
            {
                var Columns = new DevExpress.XtraGrid.Columns.GridColumn();
                Columns.Name = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                Columns.FieldName = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.Caption = DateTime.Parse(item.LoginDate.ToString()).ToString("yyyy-MM-dd");
                Columns.VisibleIndex = ord;
                Columns.MaxWidth = 200;
                Columns.MinWidth = 100;
                Columns.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                gridViewContrast.Columns.Add(Columns);
                ExhibitionTable.Columns.Add(Columns.Caption, typeof(String));
                DictDatetime.Add(ord, Columns.Name);
                ord++;
            }
        }
        /// <summary>
        /// 历史对比移除多余列
        /// </summary>
        private void removeColumns()
        {
            DictDatetime = new Dictionary<int, string>();
            for (int n = 0; n < gridViewContrast.Columns.Count; n++)
            {
                if (!connamels.Contains(gridViewContrast.Columns[n].Name))
                {
                    gridViewContrast.Columns.Remove(gridViewContrast.Columns[n]);
                }
            }

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelect_Click(object sender, EventArgs e)
        {
            removeColumns();
            addconlumns();
            ExhibitionTable.Rows.Clear();
            //科室，组合，项目所有勾选的科室记录
            List<DistinctDepartment> dictDepartment = new List<DistinctDepartment>();
            //科室，组合，项目所有勾选的组合记录
            List<DistinctItemGroup> dictItemGroup = new List<DistinctItemGroup>();
            //科室，组合，项目所有勾选的项目记录
            List<DistinctItem> dictItem = new List<DistinctItem>();
            //查询包含的时间列，不包含的会隐藏
            List<string> LoginDateList = new List<string>();
            //判断是否勾选了科室下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditDepartment.EditValue.ToString()))
            {
                //获取勾选的科室数组
                var dictDepartmentList = checkedComboBoxEditDepartment.EditValue.ToString().Replace(" ", "").Split(',').ToList();
                //记录勾选的科室信息
                dictDepartment.AddRange(DepartmentListSys.Where(o => dictDepartmentList.Contains(o.Id.ToString())));
                //记录勾选的组合信息
                dictItemGroup.AddRange(ItemGroupListSys.Where(o => dictDepartmentList.Contains(o.DepartmentId.ToString())));
                //记录勾选的项目信息
                dictItem.AddRange(ItemListSys.Where(o => dictDepartmentList.Contains(o.DepartmentId.ToString())));
            }
            //判断是否勾选了组合下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditItemGroup.EditValue.ToString()))
            {
                //获取勾选的组合数组
                var dictItemGroupList = checkedComboBoxEditItemGroup.EditValue.ToString().Replace(" ", "").Split(',').ToList();
                //循环勾选的组合数据
                foreach (var ItemGroup in ItemGroupListSys.Where(o => dictItemGroupList.Contains(o.Id.ToString())))
                {
                    //获取此组合的信息
                    var dictItemGroupFirst = ItemGroupListSys.FirstOrDefault(o => o.Id == ItemGroup.Id);
                    //判断此组合的科室是否存在于科室记录中，若不存在则科室
                    if (dictDepartment.FirstOrDefault(o => o.Id == ItemGroup.DepartmentId) == null)
                    {
                        dictDepartment.Add(DepartmentListSys.FirstOrDefault(o => o.Id == ItemGroup.DepartmentId));
                    }
                    //判断此组合是否存在于组合记录中，若不存在则增加组合和项目
                    if (!dictItemGroup.Contains(dictItemGroupFirst))
                    {
                        dictItemGroup.Add(ItemGroup);
                        dictItem.AddRange(ItemListSys.Where(o => o.ItemGroupId == ItemGroup.Id));
                    }
                }
            }
            //判断是否勾选了项目下拉框
            if (!string.IsNullOrWhiteSpace(checkedComboBoxEditItem.EditValue.ToString()))
            {
                //获取勾选的项目数组
                var dictItemList = checkedComboBoxEditItem.EditValue.ToString().Replace(" ", "").Split(',').ToList();
                //循环勾选的项目数据
                foreach (var item in ItemListSys.Where(o => dictItemList.Contains(o.Id.ToString())))
                {
                    //判断此项目科室记录是否存在，若不存在则增加科室信息
                    if (dictDepartment.FirstOrDefault(o => o.Id == item.DepartmentId) == null)
                    {
                        dictDepartment.Add(DepartmentListSys.FirstOrDefault(o => o.Id == item.DepartmentId));
                    }
                    //判断此项目组合记录是否存在，若不存在则增加组合信息
                    if (dictItemGroup.FirstOrDefault(o => o.Id == item.ItemGroupId) == null)
                    {
                        dictItemGroup.Add(ItemGroupListSys.FirstOrDefault(o => o.Id == item.ItemGroupId));
                    }
                    //判断此项目是否存在项目记录，若不存在则增加项目信息
                    if (!dictItem.Contains(item))
                    {
                        dictItem.Add(item);
                    }
                }
            }
            //判断科室记录中是否存在数据，若没有则表示都没有勾选
            if (dictDepartment.Count != 0)
            {
                //循环勾选科室
                foreach (var itemDepartment in dictDepartment)
                {
                    var Deparment = CustomerItemGroupSys.Where(o => o.DepartmentId == itemDepartment.Id).FirstOrDefault();
                    //循环科室对应组合
                    foreach (var itemGroup in dictItemGroup.Where(o => o.DepartmentId == itemDepartment.Id))
                    {
                        var CustomerId = CustomerRegListSys.OrderByDescending(o => o.LoginDate).FirstOrDefault()?.Id;
                        //循环组合对应项目   
                        foreach (var item in dictItem.Where(o => o.ItemGroupId == itemGroup.Id))
                        {
                            //添加项目
                            DataRow row = ExhibitionTable.NewRow();
                            row["DepartmentId"] = itemGroup.DepartmentId;
                            row["DepartmentName"] = itemDepartment.Name;
                            row["ItemGroupId"] = itemGroup.Id;
                            row["ItemGroupName"] = itemGroup.Name.Trim();
                            row["ItemId"] = item.Id;
                            row["ItemName"] = item.Name;
                            row["Stand"] = CustomerRegItemSys.FirstOrDefault(o => o.ItemId == item.Id && o.CustomerRegId == CustomerId)?.Stand ?? "";
                            foreach (var itemList in CustomerRegItemSys.Where(o => o.ItemId == item.Id))
                            {
                                var Login = CustomerRegListSys.FirstOrDefault(o => o.Id == itemList.CustomerRegId);
                                if (Login != null && Login.LoginDate != null)
                                {
                                    var LoginDate = DateTime.Parse(Login.LoginDate.ToString()).ToString("yyyy-MM-dd");
                                    row[LoginDate] = itemList.ItemResultChar;
                                    if (!LoginDateList.Contains(LoginDate))
                                    {
                                        LoginDateList.Add(LoginDate);
                                    }
                                }
                            }
                            ExhibitionTable.Rows.Add(row);
                        }
                        //添加组合诊断
                        DataRow rowGroupSum = ExhibitionTable.NewRow();
                        rowGroupSum["DepartmentId"] = itemGroup.DepartmentId;
                        rowGroupSum["DepartmentName"] = itemDepartment.Name;
                        rowGroupSum["ItemGroupId"] = itemGroup.Id;
                        rowGroupSum["ItemGroupName"] = itemGroup.Name.Trim();
                        rowGroupSum["ItemName"] = "组合诊断";
                        foreach (var item in CustomerItemGroupSys.Where(o => o.ItemGroupBM_Id == itemGroup.Id))
                        {
                            var Login = CustomerRegListSys.FirstOrDefault(o => o.Id == item.CustomerRegBMId);
                            if (Login != null && Login.LoginDate != null)
                            {
                                var LoginDate = DateTime.Parse(Login.LoginDate.ToString()).ToString("yyyy-MM-dd");
                                rowGroupSum[LoginDate] = item.ItemGroupDiagnosis;
                            }
                        }
                        ExhibitionTable.Rows.Add(rowGroupSum);
                    }
                    //添加科室诊断
                    DataRow rowDeparmentSum = ExhibitionTable.NewRow();
                    rowDeparmentSum["DepartmentId"] = itemDepartment.Id;

                    rowDeparmentSum["DepartmentName"] = itemDepartment.Name;
                    rowDeparmentSum["ItemName"] = "科室诊断";
                    foreach (var itemCustomer in CustomerRegListSys)
                    {
                        var LoginDate = DateTime.Parse(itemCustomer.LoginDate.ToString()).ToString("yyyy-MM-dd");
                        rowDeparmentSum[LoginDate] = itemCustomer.CustomerDepSummary.FirstOrDefault(o => o.DepartmentBMId == itemDepartment.Id)?.DagnosisSummary;
                    }
                    ExhibitionTable.Rows.Add(rowDeparmentSum);
                }
            }
            //显示隐藏列
            foreach (var item in DictDatetime)
            {
                if (LoginDateList.Contains(item.Value) == false)
                {
                    gridViewContrast.Columns[item.Value].Visible = false;
                }
                else
                {
                    gridViewContrast.Columns[item.Value].Visible = true;
                    gridViewContrast.Columns[item.Value].VisibleIndex = item.Key;
                }
            }
            gridControlContrast.DataSource = ExhibitionTable;


        }
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
            //if (customerregid != Guid.Empty)
            //{
            //    EntityDto<Guid> entityDto = new EntityDto<Guid>();
            //    entityDto.Id = customerregid;
            //    InitialUpdate(entityDto);
            //}

        }
        //private void LoadDataWJ()
        //{
        //    var closeWait = false;
        //    if (!splashScreenManager.IsSplashFormVisible)
        //    {
        //        splashScreenManager.ShowWaitForm();
        //        closeWait = true;
        //    }
        //    splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
        //    try
        //    {
        //        var result = customerSvr.getOneAddXQuestionnaires();
        //        splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
        //        layoutControlGroupBase.BeginUpdate();
        //        foreach (var ques in result)
        //        {
        //            var checkEdit = new CheckEdit
        //            {
        //                Text = ques.Name
        //            };
        //            checkEdit.Name = ques.Id.ToString("N");
        //            if (ques.Category == "家族史")
        //            {
        //                checkEdit.Width = 150;
        //                flowfamily.Controls.Add(checkEdit);
        //            }
        //            else if (ques.Category == "现病史")
        //            {
        //                checkEdit.Width = 150;
        //                flownow.Controls.Add(checkEdit);
        //            }
        //            else if (ques.Category == "既往史")
        //            {
        //                checkEdit.Width = 150;
        //                flownowill.Controls.Add(checkEdit);
        //            }
        //            else if (ques.Category == "手术史")
        //            {
        //                checkEdit.Width = 200;
        //                flowShoushu.Controls.Add(checkEdit);
        //            }
        //            else if (ques.Category == "体检史")
        //            {
        //                checkEdit.Width = 150;
        //                flowTijian.Controls.Add(checkEdit);
        //            }
        //            else if (ques.Category == "月经史")
        //            {
        //                checkEdit.Width = 100;
        //                flowYujing.Controls.Add(checkEdit);
        //            }
        //            checkEdit.CheckedChanged += rad_CheckedChanged;
        //        }

        //        layoutControlGroupBase.EndUpdate();
        //    }
        //    catch (UserFriendlyException e)
        //    {
        //        ShowMessageBox(e);
        //    }
        //    finally
        //    {
        //        if (closeWait)
        //        {
        //            if (splashScreenManager.IsSplashFormVisible)
        //                splashScreenManager.CloseWaitForm();
        //        }
        //    }
        //}
        //private void InitialUpdate(EntityDto<Guid> input)
        //{
        //    var closeWait = false;
        //    if (!splashScreenManager.IsSplashFormVisible)
        //    {
        //        splashScreenManager.ShowWaitForm();
        //        closeWait = true;
        //    }
        //    splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
        //    try
        //    {

        //        var modules = customerSvr.getCustomerQuestionDtos(input);
        //        splashScreenManager.SetWaitFormDescription(Variables.LoadingEditor);
        //        foreach (var module in modules)
        //        {
        //            var name = module.OneAddXQuestionnaireid.Value.ToString("N");
        //            if (module.QuestionType == "家族史")
        //            {
        //                var item = flowfamily.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //            else if (module.QuestionType == "现病史")
        //            {
        //                var item = flownow.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //            else if (module.QuestionType == "既往史")
        //            {
        //                var item = flownowill.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //            else if (module.QuestionType == "手术史")
        //            {
        //                var item = flowShoushu.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //            else if (module.QuestionType == "体检史")
        //            {
        //                var item = flowTijian.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //            else if (module.QuestionType == "月经史")
        //            {
        //                var item = flowYujing.Controls.Find(name, false);
        //                ((CheckEdit)item[0]).Checked = true;
        //            }
        //        }

        //    }
        //    catch (UserFriendlyException e)
        //    {
        //        ShowMessageBox(e);
        //    }
        //    finally
        //    {
        //        if (closeWait)
        //        {
        //            if (splashScreenManager.IsSplashFormVisible)
        //                splashScreenManager.CloseWaitForm();
        //        }
        //    }
        //}

        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DevExpress.XtraEditors.CheckEdit checkedit = (DevExpress.XtraEditors.CheckEdit)sender;
            //    string Pname = checkedit.Parent.Name;
            //    MemoEdit memoEdit = new MemoEdit();
            //    switch (Pname)
            //    {
            //        case "flowfamily":
            //            memoEdit = memJiazu;
            //            break;
            //        case "flownow":
            //            memoEdit = memXianbing;
            //            break;
            //        case "flownowill":
            //            memoEdit = memJIwangshi;
            //            break;
            //        case "flowShoushu":
            //            memoEdit = memshoushushi;
            //            break;
            //        case "flowTijian":
            //            memoEdit = memTijianshi;
            //            break;
            //        case "flowYujing":
            //            memoEdit = memYujingshi;
            //            break;

            //    }
            //    TextShow(checkedit.Text, memoEdit, checkedit.Checked);
            //    string strNum = "";
            //    if (memJiazu.Text != "")
            //    {
            //        strNum = "家族史：" + memJiazu.Text + "\r\n";
            //    }
            //    if (memXianbing.Text != "")
            //    {
            //        strNum += "现病史：" + memXianbing.Text + "\r\n";
            //    }
            //    if (memJIwangshi.Text != "")
            //    {
            //        strNum += "既往史：" + memJIwangshi.Text + "\r\n";
            //    }
            //    if (memshoushushi.Text != "")
            //    {
            //        strNum += "手术史：" + memshoushushi.Text + "\r\n";
            //    }
            //    if (memTijianshi.Text != "")
            //    {
            //        strNum += "体检史：" + memTijianshi.Text + "\r\n";
            //    }
            //    if (memYujingshi.Text != "")
            //    {
            //        strNum += "月经史：" + memYujingshi.Text + "\r\n";
            //    }
            //    txtHistory.Text = strNum;

            //}
            //catch (Exception ex)
            //{
            //    string ss = ex.ToString();
            //}

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

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    List<SaveCustomerQusTionDto> input = new List<SaveCustomerQusTionDto>();
            //    List<SaveCustomerQusTionDto> ls1 = getSaveCustomerQusTion(flowfamily, input);
            //    List<SaveCustomerQusTionDto> ls2 = getSaveCustomerQusTion(flownow, ls1);
            //    List<SaveCustomerQusTionDto> ls3 = getSaveCustomerQusTion(flownowill, ls2);
            //    List<SaveCustomerQusTionDto> ls4 = getSaveCustomerQusTion(flowShoushu, ls3);
            //    List<SaveCustomerQusTionDto> ls5 = getSaveCustomerQusTion(flowTijian, ls4);
            //    List<SaveCustomerQusTionDto> ls6 = getSaveCustomerQusTion(flowYujing, ls5);

            //    _doctorStationAppService.SaveCustomerQuestion(ls6);
            //    XtraMessageBox.Show("保存成功！");
            //}
            //catch (UserFriendlyException ex)
            //{
            //    ShowMessageBox(ex);
            //}

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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
               var itemlist = _customerRegItemList.Where(o => o.Symbol != "" && o.Symbol != "M" && o.Symbol != null).ToList();
                if (labZY.Checked == true)
                {
                    itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                }
                gridControlCustomerRegItem.DataSource = itemlist;
            }
            else
            {
                var itemlist = _customerRegItemList.ToList();
                if (labZY.Checked == true && iszyb==false)
                {
                    itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                }

                gridControlCustomerRegItem.DataSource = itemlist;

            }
        }
        //复查
        private void repositoryItemButtonEditItems_Click(object sender, EventArgs e)
        {
            try
            {
                // var XMID = gridReview.GetFocusedRowCellValue(colXMXiangMuWaiJian.FieldName)?.ToString();
                frmCheckItem frmCheckItem = new frmCheckItem();

                var review = gridReview.GetFocusedRowDto<CusReViewDto>();
                frmCheckItem.cusGroupDtos = review.ItemGroup.ToList();
                if (frmCheckItem.ShowDialog() == DialogResult.OK)
                {
                    //review.ReviewDay = 11;
                    review.ItemGroup = frmCheckItem.cusGroupOut;
                    //  review.ItemGroupNames = frmCheckItem.groups;

                    gridView3.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //复查
        private void repositoryItembtdelet_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 0)
                {
                   
                    frmCheckItem frmCheckItem = new frmCheckItem();                   
                    frmCheckItem.isadd = "1";

                    if (frmCheckItem.ShowDialog() == DialogResult.OK)
                    {
                        var ReViewdt= gridReview.DataSource as List<CusReViewDto>;
                        CusReViewDto cusreview = new CusReViewDto();
                        cusreview.CustomerRegId = _tjlCustomerRegDto.Id;
                        cusreview.ReviewDate = frmCheckItem.reviewSetOut.ReviewDate;
                        cusreview.ReviewDay = frmCheckItem.reviewSetOut.ReviewDay;
                        cusreview.SummarizeAdviceId = frmCheckItem.reviewSetOut.SummarizeAdviceId;
                        cusreview.ItemGroup = frmCheckItem.cusGroupOut;
                        cusreview.Remart = frmCheckItem.reviewSetOut.Remart;
                        ReViewdt.Add(cusreview);
                        gridView3.UpdateCurrentRow();
                        gridReview.RefreshDataSource();
                    }

                }
                else if (e.Button.Index == 1)
                {
                    //删除
                    //MessageBox.Show("删除");
                    var currentItem = gridReview.GetFocusedRowDto<CusReViewDto>();
                    if (currentItem == null)
                        return;

                    var list = gridReview.DataSource as List<CusReViewDto>;
                    if (list == null)
                        return;
                    var currentIndex = list.IndexOf(currentItem);
                    list.Remove(currentItem);
                    gridReview.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class CustomerRegComparer : IEqualityComparer<TjlCustomerRegItemDto>
        {
            public bool Equals(TjlCustomerRegItemDto x, TjlCustomerRegItemDto y)
            {
                if (x == null)
                    return y == null;
                return x.DepartmentId == y.DepartmentId;
            }

            public int GetHashCode(TjlCustomerRegItemDto obj)
            {
                if (obj == null)
                    return 0;
                return obj.DepartmentId.GetHashCode();
            }


        }

        public class CustomerRegComparerNew : IEqualityComparer<CustomerRegInspectItemDto>
        {
            public bool Equals(CustomerRegInspectItemDto x, CustomerRegInspectItemDto y)
            {
                if (x == null)
                    return y == null;
                return x.DepartmentId == y.DepartmentId;
            }

            public int GetHashCode(CustomerRegInspectItemDto obj)
            {
                if (obj == null)
                    return 0;
                return obj.DepartmentId.GetHashCode();
            }


        }

        private void richEditControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }
        /// <summary>
        /// 右键隐藏轻微异常诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                var selectText = richEditControl1.SelectedText;
                if (_tjlCustomerRegDto != null && selectText != "")
                {
                    TbmSumHideDto hideDto = new TbmSumHideDto();
                    hideDto.SumWord = selectText;
                    hideDto.ClientType = "";
                    hideDto.IsNormal = 2;
                    if (string.IsNullOrWhiteSpace(_tjlCustomerRegDto.ClientType))
                    {
                        DialogResult dr = XtraMessageBox.Show("'" + selectText + "'为轻微诊断，以后不想显示在汇总中？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else
                    {

                        FrmSumHide hide = new FrmSumHide();
                        var result = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
                        hide.category = result.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType).Text;
                        hide.categoryInt = _tjlCustomerRegDto.PhysicalType.ToString();
                        hide.sum = selectText;

                        if (hide.ShowDialog() == DialogResult.OK)
                        {
                            hideDto.ClientType = hide.categoryInt;
                        }
                        else
                        {
                            return;
                        }
                    }

                    var sum = _inspectionTotalService.AddSumHide(hideDto);
                    BtnUpDepartSum_Click(sender, e);
                    XtraMessageBox.Show("更新成功，请清除总检后，重新生成总检！");
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }
        /// <summary>
        /// 右建隐藏正常结论
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var selectText = richEditControl1.SelectedText;
                if (_tjlCustomerRegDto != null && selectText != "")
                {
                    DialogResult dr = XtraMessageBox.Show("'" + selectText + "'为正常结论，以后不想显示在汇总中？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        TbmSumHideDto hideDto = new TbmSumHideDto();
                        hideDto.SumWord = selectText;
                        hideDto.ClientType = "";
                        hideDto.IsNormal = 1;
                        _inspectionTotalService.AddSumHide(hideDto);
                        BtnUpDepartSum_Click(sender, e);
                        XtraMessageBox.Show("更新成功，请清除总检后，重新生成总检！");
                    }
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void btncopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //确保文本在文本框中已经选定 
            if (richEditControl1.SelectionLength > 0)
                // 复制文本到剪贴板
                richEditControl1.Copy();
        }

        private void btncut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 确保当前文本框中有选定
            if (richEditControl1.SelectedText != "")
                // 剪切选定的文本至剪贴板
                richEditControl1.Cut();
        }

        private void btnPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 判断剪贴板中是否有文本
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                // 判断文本框中是否有文本选定了
                if (richEditControl1.SelectionLength > 0)
                {
                    // 询问是否覆盖选定的文本
                    if (MessageBox.Show("你想覆盖选定的文本吗?", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        // 移动选定文本的位置，即之前选定文本的起始+选定文本的长度
                        richEditControl1.SelectionStart = richEditControl1.SelectionStart + richEditControl1.SelectionLength;
                }
                // 将剪贴板中的文本粘贴至文本框
                richEditControl1.Paste();
            }
        }

        private void BtnUpDepartSum_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var cusdepart = _customerRegItemList.Select(o => o.DepartmentId).Distinct().ToList(); 
                CreateConclusionDto conclusion = new CreateConclusionDto();
                conclusion.CustomerBM = _tjlCustomerRegDto.CustomerBM;
                conclusion.Department = cusdepart;
                _doctorStationAppService.CreateConclusion(conclusion);
            }, "正在生成小结!");
            LoadData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          FrmSumHideList hide = new FrmSumHideList();
            hide.ShowDialog();

        }
        #region 图片鼠标事件     
        public System.Drawing.Point mouseDownPoint;//存储鼠标焦点的全局变量
        public bool isSelected = false;
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double scale = 1;
            if (pictureEditImg.Height > 0)
            {
                scale = (double)pictureEditImg.Width / (double)pictureEditImg.Height;
            }
            pictureEditImg.Width += (int)(e.Delta * scale);
            pictureEditImg.Height += e.Delta;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureEditImg.Focus();
            pictureEditImg.Cursor = Cursors.SizeAll;
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
                this.pictureEditImg.Left = this.pictureEditImg.Left + (Cursor.Position.X - mouseDownPoint.X);
                this.pictureEditImg.Top = this.pictureEditImg.Top + (Cursor.Position.Y - mouseDownPoint.Y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
            }

        }
        #endregion

        private void panel_Picture_SizeChanged(object sender, EventArgs e)
        {
            pictureEditImg.Width = panel_Picture.Width - 6;
            pictureEditImg.Height = panel_Picture.Height - 6;
        }

        private void chekNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chekNo.Checked == true)
            {
               var cusgroupls =  _customerRegItemList.Where(o => o.DepartmentId != Guid.Parse("B54823B8-23C5-4107-9747-6E6CBB486022")
            && o.GroupCheckState == (int)ProjectIState.Not).ToList();
                if (cusgroupls.Count==0)
                {
                   // MessageBox.Show("所有体检项目均已检查，无未检项目！");
                    XtraMessageBox.Show("所有体检项目均已检查，无未检项目！");
                }
                else
                {
                    var itemlist = cusgroupls.ToList();
                    if (labZY.Checked == true)
                    {
                        itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                    }
                    gridControlCustomerRegItem.DataSource = itemlist;
                }

            }
            else
            {
                var itemlist = _customerRegItemList.ToList();
                if (labZY.Checked == true && iszyb==false)
                {
                    itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                }
                gridControlCustomerRegItem.DataSource = itemlist;

            }
        }

        private void simpleButtonXinZeng2_Click_1(object sender, EventArgs e)
        {

        }

        private void butUp_Click(object sender, EventArgs e)
        {
            //上
            var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
            if (list == null)
                return;
            var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
            if (currentItem == null)
                return;
            if (currentItem == list.FirstOrDefault())
                return;
            var currentOrder = currentItem.SummarizeOrderNum;
            list = list.OrderBy(n => n.ParentAdviceId).ToList();
            var currentIndex = list.IndexOf(currentItem);
            if (currentItem.ParentAdviceId != null && currentItem.ParentAdviceId != Guid.Empty && currentItem.SummarizeOrderNum == 1)
                return;
            if (list[currentIndex - 1].ParentAdviceId == null || list[currentIndex - 1].ParentAdviceId == Guid.Empty)
            {
                list[currentIndex].SummarizeOrderNum = list[currentIndex - 1].SummarizeOrderNum;
                list[currentIndex - 1].SummarizeOrderNum = currentOrder;
                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex - 1];
            }
            else
            {
                var ParentIndex = list.IndexOf(list.FirstOrDefault(n => n.Id == list[currentIndex - 1].ParentAdviceId));
                list[currentIndex].SummarizeOrderNum = list[ParentIndex].SummarizeOrderNum;
                list[currentIndex - 1].SummarizeOrderNum = list[currentIndex].SummarizeOrderNum + 1;
                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[ParentIndex + 1];
            }
            treeListZhenDuan.ExpandAll();
        }

        private void butDown_Click(object sender, EventArgs e)
        {
            //下
            var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
            if (list == null)
                return;
            var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
            if (currentItem == null)
                return;
            if (currentItem == list.Where(n => n.ParentAdviceId == null || n.ParentAdviceId == Guid.Empty).OrderBy(n => n.SummarizeOrderNum).LastOrDefault())
                return;
            var currentOrder = currentItem.SummarizeOrderNum;
            list = list.OrderBy(n => n.ParentAdviceId).ToList();
            var currentIndex = list.IndexOf(currentItem);
            if (currentIndex + 1 >= list.Count)
                return;
            list[currentIndex].SummarizeOrderNum = list[currentIndex + 1].SummarizeOrderNum;
            list[currentIndex + 1].SummarizeOrderNum = currentOrder;
            treeListZhenDuan.DataSource = OrderByList(list);
           // treeListZhenDuan.BestFitColumns();
            treeListZhenDuan.RefreshDataSource();
            treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex + 1];
            treeListZhenDuan.ExpandAll();
        }

        private void butTop_Click(object sender, EventArgs e)
        {
            //置顶
            var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
            if (list == null)
                return;
            var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
            if (currentItem == null)
                return;
            if (currentItem == list.FirstOrDefault())
                return;
            var currentOrder = currentItem.SummarizeOrderNum;
            list = list.OrderBy(n => n.ParentAdviceId).ToList();
            var currentIndex = list.IndexOf(currentItem);

            list[currentIndex].SummarizeOrderNum = 0;
            list[0].SummarizeOrderNum = 1;
            treeListZhenDuan.DataSource = OrderByList(list);
           // treeListZhenDuan.BestFitColumns();
            treeListZhenDuan.RefreshDataSource();
            treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[0];
            treeListZhenDuan.ExpandAll();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
            if (currentItem == null)
                return;

            var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
            if (list == null)
                return;
            //list = list.OrderBy(n => n.ParentAdviceId).ToList();
            var currentIndex = list.IndexOf(currentItem);
            //将删除行的所有子级行变为普通行
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ParentAdviceId == currentItem.Id)
                {
                    list[i].ParentAdviceId = null;
                    list[i].SummarizeName = list[i].SummarizeName;
                }
            }

            _CustomerSummarizeList.Remove(currentItem);


            treeListZhenDuan.DataSource = OrderByList(_CustomerSummarizeList);
           // treeListZhenDuan.BestFitColumns();
            treeListZhenDuan.RefreshDataSource();
            if (currentIndex > 1)
            {
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex - 1];
            }
            treeListZhenDuan.ExpandAll();
        }

        private void butChiled_Click(object sender, EventArgs e)
        {
            //设为子级
            var list = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
            if (list == null)
                return;
            var currentItem = treeListZhenDuan.GetFocusedRow() as TjlCustomerSummarizeBMDto;
            if (currentItem == null)
                return;
            if (currentItem == list.OrderBy(n => n.ParentAdviceId).FirstOrDefault())
                return;
            //list = list.OrderBy(n => n.ParentAdviceId).ToList();
            var currentIndex = list.IndexOf(currentItem);
            if (currentItem.ParentAdviceId != Guid.Empty && currentItem.ParentAdviceId != null)
            {
                //已经是子级，再次点击 取消子级
                currentItem.ParentAdviceId = null;
                currentItem.SummarizeName = currentItem.SummarizeName;
                currentItem.SummarizeOrderNum = list.Count + 1;

                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[treeListZhenDuan.GetNodeList().Count - 1];
            }
            else
            {
                //判断当前行是父级 还是 普通行
                var ParentInfo = list.FirstOrDefault(n => n.ParentAdviceId == currentItem.Id);
                if (ParentInfo != null)
                {
                    //父级
                    list = list.OrderBy(n => n.ParentAdviceId).ToList();
                    currentIndex = list.IndexOf(currentItem);
                    if (list[currentIndex - 1].ParentAdviceId != Guid.Empty && list[currentIndex - 1].ParentAdviceId != null)
                    {
                        //上一行也是子级，将取它得父级ID
                        list[currentIndex].ParentAdviceId = list[currentIndex - 1].ParentAdviceId;
                        list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                    }
                    else
                    {
                        //上一行是父级，直接取ID
                        list[currentIndex].ParentAdviceId = list[currentIndex - 1].Id;
                        list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                    }
                    //将当前行所有的子级 的 父级ID 更新为 新的父级（当前行的父级）
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].ParentAdviceId == list[currentIndex].Id)
                        {
                            list[i].ParentAdviceId = list[currentIndex].ParentAdviceId;
                        }
                    }
                }
                else
                {
                    if (currentItem.SummarizeAdviceId != null && currentItem.SummarizeAdviceId != Guid.Empty)
                    {
                        list = list.OrderBy(n => n.ParentAdviceId).ToList();
                        currentIndex = list.IndexOf(currentItem);
                    }
                    //普通
                    if (list[currentIndex - 1].ParentAdviceId != Guid.Empty && list[currentIndex - 1].ParentAdviceId != null)
                    {
                        //上一行也是子级，将取它得父级ID
                        list[currentIndex].ParentAdviceId = list[currentIndex - 1].ParentAdviceId;
                        list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                    }
                    else
                    {
                        //上一行是父级，直接取ID
                        list[currentIndex].ParentAdviceId = list[currentIndex - 1].Id;
                        list[currentIndex].SummarizeName = list[currentIndex].SummarizeName;
                    }
                }



                treeListZhenDuan.DataSource = OrderByList(list);
               // treeListZhenDuan.BestFitColumns();
                treeListZhenDuan.RefreshDataSource();
                treeListZhenDuan.FocusedNode = treeListZhenDuan.GetNodeList()[currentIndex];
            }
            treeListZhenDuan.ExpandAll();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
        
        }

        private void repositoryItemButtonEdit4_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
          
        }

        private void SearOccDis_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void searJJZ_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        private void cuslookItemSuit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmCheckItem frmCheckItem = new frmCheckItem();
            frmCheckItem.isadd = "1";

            if (frmCheckItem.ShowDialog() == DialogResult.OK)
            {
                gridReview.Visible = true;
                layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                var ReViewdt = gridReview.DataSource as List<CusReViewDto>;
                CusReViewDto cusreview = new CusReViewDto();
                cusreview.CustomerRegId = _tjlCustomerRegDto.Id;
                cusreview.ReviewDate = frmCheckItem.reviewSetOut.ReviewDate;
                cusreview.ReviewDay = frmCheckItem.reviewSetOut.ReviewDay;
                cusreview.SummarizeAdviceId = frmCheckItem.reviewSetOut.SummarizeAdviceId;
                cusreview.ItemGroup = frmCheckItem.cusGroupOut;
                cusreview.Remart = frmCheckItem.reviewSetOut.Remart;
                if (ReViewdt == null)
                {
                    ReViewdt = new List<CusReViewDto>();
                }
                ReViewdt.Add(cusreview);
                gridReview.DataSource = ReViewdt;
                gridView3.UpdateCurrentRow();
                gridReview.RefreshDataSource();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //去除掉未缴费与减项得体检项目
            var ItemOk = _customerRegItemList.Where(o => o.Id != Guid.Empty && o.GroupCheckState != (int)ProjectIState.GiveUp);
            //项目去重 拿到去重后得科室
            //var DicInfo = ItemOk.Where((x, i) => _customerRegItemList.FindIndex(z => z.DepartmentId == x.DepartmentId) == i).ToList();
            var DicInfo = ItemOk.Distinct(new CustomerRegComparerNew());
            if (string.IsNullOrEmpty(memoEditHuiZong.Text.Trim()))
            {
                MessageBox.Show("还未生成总检无需更新汇总");
                return;
            }
                //重新加载检查汇总
             LoadStr();
            
            if (memoEditHuiZong.Text.Trim() == "")
            {
                memoEditHuiZong.Text = "* 本次体检所检项目未发现明显异常。";

            }
            //匹配建议
            MatchingAdvice();
            if (_CustomerSummarizeList.Count() > 0)
            {
                if (tabNavigationPage8.PageVisible != false)
                {
                    //tabPane1.SelectedPageIndex = 1;
                    tabPane2.SelectedPageIndex = 1;
                    _CustomerSummarizeList = MakeReview(_CustomerSummarizeList);
                    var oldad = new List<TjlCustomerSummarizeBMDto>();
                    if (treeListZhenDuan.DataSource != null)
                    {
                        oldad = treeListZhenDuan.DataSource as List<TjlCustomerSummarizeBMDto>;
                        var Adnames = oldad.Select(o => o.SummarizeName);
                        _CustomerSummarizeList = _CustomerSummarizeList.Where(o => !Adnames.Contains(o.SummarizeName)).ToList();
                        oldad.AddRange(_CustomerSummarizeList);
                    }
                    for (int i = 0; i < oldad.Count(); i++)
                    {

                        oldad[i].SummarizeOrderNum = i + 1;

                    }

                    treeListZhenDuan.DataSource = oldad.OrderBy(l => l.SummarizeOrderNum).ToList();
                    treeListZhenDuan.RefreshDataSource();
                }
                // treeListZhenDuan.BestFitColumns();
            }


        }
        //匹配建议算法
        private void AllMatchingAdvice()
        {
            //建议汇总数据
            var StrContent = memoEditHuiZong.Text;
            //建议汇总数据
            var StrContent2 = memoEditHuiZong.Text;
            //隐私总检汇总
            var PStrContent = txtPrivacy2.Text;
            //存储建议Id集合
            List<Guid> IdList = new List<Guid>();
            //存储建议Id集合
            List<Guid> PIdList = new List<Guid>();
            //匹配多条建议
            var isshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 5)?.Remarks;
            //遍历科室建议 
            List<string> AdviceName = new List<string>();
            foreach (var Ditem in _summarizeAdviceFull.OrderByDescending(n => n.AdviceName.Length).ToList())
                if (!string.IsNullOrWhiteSpace(Ditem.AdviceName))
                {
                    if (isshow != null && isshow == "1")
                    {
                        if (AdviceName.Contains(Ditem.AdviceName))
                        { continue; }
                        string adviName = Ditem.AdviceName;
                        if (!string.IsNullOrEmpty(Ditem.Advicevalue))
                        {
                            adviName = Ditem.Advicevalue;
                        }
                        var adviNames = adviName.Split('|');
                        foreach (string ad in adviNames)
                        {
                            if (!string.IsNullOrEmpty(ad))
                            {
                                if (!string.IsNullOrWhiteSpace(StrContent))
                                    if (StrContent.Contains(ad))
                                    {
                                        IdList.Add(Ditem.Id);
                                        AdviceName.Add(Ditem.AdviceName);
                                        break;
                                    }
                                if (!string.IsNullOrWhiteSpace(PStrContent))
                                    if (PStrContent.Contains(ad))
                                    {
                                        PIdList.Add(Ditem.Id);
                                        AdviceName.Add(Ditem.AdviceName);
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(StrContent))
                            if (StrContent.Contains(Ditem.AdviceName))
                            {
                                IdList.Add(Ditem.Id);
                                StrContent = StrContent.Replace(Ditem.AdviceName, "");
                            }
                        if (!string.IsNullOrWhiteSpace(PStrContent))
                            if (PStrContent.Contains(Ditem.AdviceName))
                            {
                                PIdList.Add(Ditem.Id);
                                PStrContent = PStrContent.Replace(Ditem.AdviceName, "");
                            }
                    }
                }
            IdList.AddRange(PIdList);
            List<SummarizeAdviceDto> info = _summarizeAdviceAppService.GetSummForGuidList(IdList);
            //按照汇总顺序重新排列诊断数据
            foreach (var item in info)
            {
                item.IndexOfNum = StrContent2.IndexOf(item.AdviceName);
            }
            info = info.OrderBy(n => n.IndexOfNum).ToList();
            _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();
            //清除已选诊断项目
            foreach (var item in _CustomerSummarizeList)
            {
                foreach (var itemInfo in info)
                {
                    if (item.SummarizeAdviceId == itemInfo.Id)
                    {
                        info.Remove(itemInfo);
                        break;
                    }
                }
            }
            //将新加入的诊断项目进行序号重排，并转换记录表对象，放入集合
            for (int i = 0; i < info.Count(); i++)
            {

                info[i].OrderNum = _CustomerSummarizeList.Count() + 1;
                if (PIdList.Count >= 0 && PIdList.Contains(info[i].Id))
                {
                    _CustomerSummarizeList.Add(SummarizeBMToJL(info[i], true));
                }
                else
                {
                    _CustomerSummarizeList.Add(SummarizeBMToJL(info[i]));
                }
            }

        }

        private void richEditControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                this.memoEditHuiZong.Text = richEditControl1.Text;
           
        }

        private void memoEditHuiZong_KeyPress(object sender, KeyPressEventArgs e)
        {
            richEditControl1.Text = this.memoEditHuiZong.Text;
        }
        private void setWJZ(int leve)
        {
            var cusGroup = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();
            if (cusGroup != null && cusGroup.ProcessState != null &&
                cusGroup.CrisisSate == (int)CrisisSate.Abnormal)
            {
                var itemName = cusGroup.ItemName;
                alertInfo.Show(this, "温馨提示", $"“{itemName}”此项目已是危急项目，请勿重复设置!");
                return;
            }

            if (cusGroup.Id != null)
            {
                var Query = new UpdateClass();
                Query.CustomerItemId = cusGroup.Id;
                Query.CrisisSate = (int)CrisisSate.Abnormal;
                Query.CrisisLever = leve;
                Query.CrisiChar = "";
                Query.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStationAppService.UpdateCrisisSate(Query);
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
                    var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
                    //检查结果
                    var customerRegInsItem = _inspectionTotalService.GetCusInspectItemList(new QueryClass { CustomerRegId = _tjlCustomerRegDto.Id }).ToList();

                    var itemlist = customerRegInsItem.ToList();
                    if (labZY.Checked == true)
                    {
                        itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                    }
                    gridControlCustomerRegItem.DataSource = itemlist;
                    gridControlCustomerRegItem.RefreshDataSource();
                    gridControlCustomerRegItem.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setWJZ(1);

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setWJZ(2);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var cusGroup = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();

            if (cusGroup != null && cusGroup.ProcessState != null &&
            cusGroup.CrisisSate == (int)CrisisSate.Normal)
            {
                var itemName = cusGroup.ItemName;
                alertInfo.Show(this, "温馨提示", $"“{itemName}”此项目不为危急项目，请勿重复设置!");
                return;
            }

            if (cusGroup.Id != null)
            {
                var Query = new UpdateClass();
                Query.CustomerItemId = cusGroup.Id;
                Query.CrisisSate = (int)CrisisSate.Normal;

                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStationAppService.UpdateCrisisSate(Query);
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
                    var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
                    //检查结果
                    var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                            .GetCustomerRegisterItem(input);
                    var itemlist = customerRegisterItemTask.Result;
                    if (labZY.Checked == true)
                    {
                        itemlist = itemlist.Where(p => p.CustomerItemGroupBM.IsZYB.HasValue && p.CustomerItemGroupBM.IsZYB == 1).ToList();
                    }
                    gridControlCustomerRegItem.DataSource = itemlist;
                    gridControlCustomerRegItem.RefreshDataSource();
                    gridControlCustomerRegItem.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlCustomerRegItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu2.ShowPopup(MousePosition);
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            OccdieaseConsultation occdieaseConsultation = new OccdieaseConsultation(_tjlCustomerRegDto.CustomerBM);
            occdieaseConsultation.ShowDialog();
        }

        private void gridViewHazardSum_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            // List < OccCustomerHazardSumDto >
            if (e.Column == conzyb && _OccTargetDiseaseByRisk != null && _OccTargetDiseaseByRisk.OccDiseases != null)
            {
                var row = (OccCustomerHazardSumDto)gridViewHazardSum.GetRow(e.RowHandle);
                if (row!=null && row.OccHazardFactorsId.HasValue)
                {
                    var ImgCount = _OccTargetDiseaseByRisk.OccDiseases?.Where(o => o.OccHazardFactorsId == row.OccHazardFactorsId)?.ToList();
                    if (ImgCount != null && ImgCount.Count != 0) //正式判断
                    {
                        var item = new RepositoryItemCheckedComboBoxEdit();
                        item.DataSource = ImgCount;
                        item.DisplayMember = "Text";
                        item.ValueMember = "Id";
                        e.RepositoryItem = item;
                    }
                }
              
            }
            if (e.Column == conjjz && _OccTargetDiseaseByRisk != null && _OccTargetDiseaseByRisk.Contraindications != null)
            {
                var row = (OccCustomerHazardSumDto)gridViewHazardSum.GetRow(e.RowHandle);
                if (row != null && row.OccHazardFactorsId.HasValue)
                {
                    var ImgCount = _OccTargetDiseaseByRisk.Contraindications?.Where(o => o.OccHazardFactorsId == row.OccHazardFactorsId)?.ToList();
                    if (ImgCount != null && ImgCount.Count != 0) //正式判断
                    {
                        var item = new RepositoryItemCheckedComboBoxEdit();
                        item.DataSource = ImgCount;
                        item.DisplayMember = "Text";
                        item.ValueMember = "Id";
                        e.RepositoryItem = item;
                    }
                }
            }

        }

        private void repositoryItemButtonEdit2_ButtonClick_1(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "编辑")
            {
                var row = (OccCustomerHazardSumDto)gridViewHazardSum.GetFocusedRow();
                Edit(row);
            }
            else if (e.Button.Caption == "清除")
            {
                var row = (OccCustomerHazardSumDto)gridViewHazardSum.GetFocusedRow();
                row.Advise = "";
                row.Conclusion = "";
                row.Description = "";
                row.MedicalAdvice = "";
                row.OccCustomerOccDiseases = null;
                row.OccCustomerOccDiseasesIds = "";
                row.OccDictionarys = null;
                row.OccDictionarysIDs = "";
                row.Opinions = "";

                gridHazardSum.RefreshDataSource();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        public void Edit(OccCustomerHazardSumDto dto)
        {
            var riskname = gridViewHazardSum.GetFocusedRowCellDisplayText(conriskName);

            var temp = ModelHelper.DeepCloneByJson(dto);
            frmCusRiskSum frm = new frmCusRiskSum(_tjlCustomerRegDto, temp, riskname);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ModelHelper.CustomMapTo(frm._cusHazar, dto);
                gridHazardSum.RefreshDataSource();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var griddatda = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
            if (griddatda == null)
            {

                griddatda = new List<OccCustomerHazardSumDto>();

            }
            OccCustomerHazardSumDto occCustomerHazardSumDto = new OccCustomerHazardSumDto();
            occCustomerHazardSumDto.CustomerRegBMId = _tjlCustomerRegDto.Id;
            griddatda.Add(occCustomerHazardSumDto);
            gridHazardSum.DataSource = griddatda;
            gridHazardSum.RefreshDataSource();
            gridHazardSum.Refresh();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var currentItem = gridViewHazardSum.GetFocusedRow() as OccCustomerHazardSumDto;
            if (currentItem == null)
                return;

            var dataresult = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
            dataresult.Remove(currentItem);
            gridHazardSum.DataSource = dataresult;
            gridHazardSum.RefreshDataSource();
            gridHazardSum.Refresh();
        }

        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {
            //var currentItem = gridViewHazardSum.GetFocusedRow() as OccCustomerHazardSumDto;
            //if (currentItem == null)
            //    return;
            //if (currentItem.IsMain == 1)
            //{
            //    var dataresultlist = gridHazardSum.DataSource as List<OccCustomerHazardSumDto>;
            //    foreach (var dataresult in dataresultlist)
            //    {
            //        if (dataresult.OccHazardFactorsId != currentItem.OccHazardFactorsId)
            //        {
            //            dataresult.IsMain = 0;
            //        }
            //    }
            //    gridHazardSum.DataSource = dataresultlist;
            //    gridHazardSum.RefreshDataSource();
            //    gridHazardSum.Refresh();
            //}
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            LoadDatanew();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\ZInspection.json";

            List<Search> searchlist = new List<Search>();


            if (!string.IsNullOrWhiteSpace(lookUpEditExaminationCategories.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "CheckType";
                search.Text = lookUpEditExaminationCategories.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(lookUpEditExaminationStatus.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "CheckState";
                search.Text = lookUpEditExaminationStatus.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(lookUpEditSumStatus.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "SumState";
                search.Text = lookUpEditSumStatus.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(textEditDayNum.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "Day";
                search.Text = textEditDayNum.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(txtsearchDoctor.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "Doctor";
                search.Text = txtsearchDoctor.EditValue?.ToString();
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

        private void butInterfase_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {

                //体检人信息
                var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetFocusedRow();
                if (rowData == null)
                {
                    MessageBox.Show("请选择需要上传的体检人！");
                    return;
                }
                TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                tdbInterfaceWhere.inactivenum = rowData.CustomerBM;
                InterfaceBack Back = new InterfaceBack();

                Back = _doctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                {
                    MessageBox.Show("上传异常！" + Back.StrBui.ToString().Trim());
                }
                else
                {
                    MessageBox.Show("上传成功！");
                }
            });
        }

        private void gridViewCustomerReg_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {

                var dto = gridControl.GetFocusedRowDto<TjlCustomerRegForInspectionTotalSearchDto>();
                if (dto == null)
                {
                    ShowMessageBoxWarning("请选中行！");
                }
                else
                {

                    var nowdate = _inspectionTotalService.Transformation(dto);
                    _tjlCustomerRegDto = nowdate;
                    //Text = $@"{Text} - {_tjlCustomerRegDto.Customer.Name}";
                    //if (_tjlCustomerRegDto.ClientInfo != null)
                    //{
                    //    Text = $@"{Text} - {_tjlCustomerRegDto.ClientInfo.ClientName}";
                    //}

                    LoadData();
                    LoadDataState();
                    LoadSummarizeData();
                    LoadCrisisRegItem();
                  
                    isfist = false;

                    var outresult = _OccHazardFactorAppService.getSimpOccHazardFactors();
                    repositoryItemLookUpEditrisk.DataSource = outresult;
                 

                }
            }
        }

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(searchControl1.Text))
            {
                TjlCustomerQuery input = new TjlCustomerQuery();
                input.Code = searchControl1.Text.Trim();
                var dto = _inspectionTotalService.GetCustomerRegList(input).FirstOrDefault();
                if (dto == null)
                {
                    ShowMessageBoxWarning("没有该体检人！");
                }
                else
                {

                    var nowdate = _inspectionTotalService.Transformation(dto);
                    _tjlCustomerRegDto = nowdate;
                   // Text = "总检";
                  

                    LoadData();
                    LoadDataState();
                    LoadSummarizeData();
                    LoadCrisisRegItem();

                    isfist = false;

                    var outresult = _OccHazardFactorAppService.getSimpOccHazardFactors();
                    repositoryItemLookUpEditrisk.DataSource = outresult;


                }
            }
        }

        private void lookUpEditExaminationStatus_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookUpEditExaminationStatus.EditValue = null;
            }
        }

        private void sleDW_Click(object sender, EventArgs e)
        {
           
        }

        private void sleDW_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                sleDW.EditValue = null;
            }
        }

        private void txtsearchDoctor_Click(object sender, EventArgs e)
        {
           
        }

        private void txtsearchDoctor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtsearchDoctor.EditValue = null;
            }
        }

        private void lookUpEditSumStatus_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookUpEditSumStatus.EditValue = null;
            }
        }

        private void dockPanel1_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanel1.HideSliding();
            //Point screenPoint = Control.MousePosition;
            //SetCursorPos(screenPoint.X, screenPoint.Y - 100);
            //treeListZhenDuan.Select();

            //treeListZhenDuan.Focus();
            e.Cancel = true;
        }

        private void butYX_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto == null)
            {
                MessageBox.Show("请选择体检人！");
                return;
            }
            var cusItem = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();

            if (cusItem != null && cusItem.Id != Guid.Empty)
            {
                DialogResult dr = XtraMessageBox.Show("确定把" + cusItem.ItemName
                    + "设置为阳性吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (cusItem.ProcessState != (int)ProjectIState.Complete)
                {
                    MessageBox.Show("项目结果未检，不能设置阳性");
                    return;
                }
                //修改状态返回数据
                var Query = new UpCusIllStateDto();
                Query.Id = cusItem.Id;
                Query.cusItemId = cusItem.Id;
                Query.CustomerBM = _tjlCustomerRegDto.CustomerBM;
                var iteminfo = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p=>p.Id== cusItem.ItemId);
                if (iteminfo!=null && iteminfo.moneyType == 1 || iteminfo.moneyType == 2)
                {
                    //MessageBox.Show("数值型项目不可设置阳性！");
                    //return;
                    frmsym _frmsym = new frmsym();
                    _frmsym.ShowDialog();
                    if (_frmsym.DialogResult == DialogResult.OK)
                    {
                        Query.Symbol = _frmsym.sym;
                    }
                    else
                    {
                        MessageBox.Show("数值型，请选择偏高偏低");
                        return;
                    }
                }
                else
                {
                    if (cusItem.Symbol == "P")
                    {
                        MessageBox.Show("项目已是阳性无需设置！");
                        return;
                    }
                    Query.Symbol = "P";
                }


                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStationAppService.UpdateIllState(Query);
                    if (trGroup.IsOK == false)
                    {
                        XtraMessageBox.Show(trGroup.ErrMess);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (trGroup.IsOK == true)
                {
                    
                    var customerRegInsItem = _inspectionTotalService.GetCusInspectItemList(new QueryClass { CustomerRegId = _tjlCustomerRegDto.Id }).ToList();

                    _customerRegItemList = customerRegInsItem.OrderBy(n => n.DepartmentOrderNum)
                        .ThenBy(n => n.ItemGroupOrder).ThenBy(n => n.ItemOrder).ToList();
                    if (checkEdit1.Checked == true)
                    {
                        var itemlist = _customerRegItemList.Where(o => o.Symbol != "" && o.Symbol != "M" && o.Symbol != null).ToList();
                        if (labZY.Checked == true)
                        {
                            itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                        }
                        gridControlCustomerRegItem.DataSource = itemlist;
                     
                    }
                    else
                    {
                        //过滤健康检项目
                        var itemlist = _customerRegItemList.ToList();
                        if (labZY.Checked == true)
                        {
                            itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                        }
                        gridControlCustomerRegItem.DataSource = itemlist;
                    }
                    gridControlCustomerRegItem.RefreshDataSource();
                    gridControlCustomerRegItem.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto == null)
            {
                MessageBox.Show("请选择体检人！");
                return;
            }
            var cusItem = gridControlCustomerRegItem.GetFocusedRowDto<CustomerRegInspectItemDto>();

            if (cusItem != null && cusItem.Id != Guid.Empty)
            {
                DialogResult dr = XtraMessageBox.Show("确定取消" + cusItem.ItemName
                    + "阳性吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (cusItem.ProcessState != (int)ProjectIState.Complete)
                {
                    MessageBox.Show("项目结果未检，不能取消阳性");
                    return;
                }
                //修改状态返回数据
                var Query = new UpCusIllStateDto();
                Query.Id = cusItem.Id;
                Query.cusItemId = cusItem.Id;
                Query.CustomerBM = _tjlCustomerRegDto.CustomerBM;
                var iteminfo = DefinedCacheHelper.GetItemInfos().FirstOrDefault(p => p.Id == cusItem.ItemId);
                if (iteminfo != null && iteminfo.moneyType == 1 || iteminfo.moneyType == 2)
                {
                    MessageBox.Show("数值型项目不可取消阳性！");
                    return;
                }
                else
                {
                    if (cusItem.Symbol != "P")
                    {
                        MessageBox.Show("项目不是阳性无需取消！");
                        return;
                    }
                }


                //修改状态返回数据
                OutStateDto trGroup = new OutStateDto();
                try
                {
                    trGroup = _doctorStationAppService.CancelIllState(Query);
                    if (trGroup.IsOK == false)
                    {
                        XtraMessageBox.Show(trGroup.ErrMess);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (trGroup.IsOK == true)
                {

                    var customerRegInsItem = _inspectionTotalService.GetCusInspectItemList(new QueryClass { CustomerRegId = _tjlCustomerRegDto.Id }).ToList();

                    _customerRegItemList = customerRegInsItem.OrderBy(n => n.DepartmentOrderNum)
                        .ThenBy(n => n.ItemGroupOrder).ThenBy(n => n.ItemOrder).ToList();
                    if (checkEdit1.Checked == true)
                    {
                        var itemlist = _customerRegItemList.Where(o => o.Symbol != "" && o.Symbol != "M" && o.Symbol != null).ToList();
                        if (labZY.Checked == true)
                        {
                            itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                        }
                        gridControlCustomerRegItem.DataSource = itemlist;

                    }
                    else
                    {
                        //过滤健康检项目
                        var itemlist = _customerRegItemList.ToList();
                        if (labZY.Checked == true)
                        {
                            itemlist = itemlist.Where(p => !p.IsZYB.HasValue || p.IsZYB != 2).ToList();
                        }
                        gridControlCustomerRegItem.DataSource = itemlist;
                    }
                    gridControlCustomerRegItem.RefreshDataSource();
                    gridControlCustomerRegItem.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            frmSumSet frmSumSet = new frmSumSet();
            frmSumSet.ShowDialog();
        }
    }
}