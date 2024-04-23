
using DevExpress.Utils;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OutInspects;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OutInspects;
using Sw.Hospital.HealthExaminationSystem.Application.OutInspects.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.DoctorStation;
using System;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.His.Common.Functional.Unit.CustomTools;
using Newtonsoft.Json.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using DevExpress.ExpressApp;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using gregn6Lib;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint.Dto;

namespace Sw.Hospital.HealthExaminationSystem.OutInspects
{
    public partial class OutCheckList : UserBaseForm
    {
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        private readonly IOutInspectsQueryAppService _OutInspectsQueryAppService;
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private readonly ICommonAppService _commonAppService;
        /// <summary>
        /// 查询建议字典--初始化读取，后续生成总检使用
        /// </summary>
        private List<SummarizeAdviceDto> _summarizeAdviceFull = new List<SummarizeAdviceDto>();

        // 建议字典
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        public ICustomerAppService customerSvr1 = new CustomerAppService();//体检预约
        private readonly GridppReport ReportMain = new GridppReport();
        private IBarPrintAppService barPrintAppService;

        public OutCheckList()
        {
            InitializeComponent();
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            _doctorStation = new DoctorStationAppService();
            _OutInspectsQueryAppService = new OutInspectsQueryAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
        }
        /// <summary>
        /// 自定义事件，和医生站实时交互
        /// </summary>
        public event EventHandler<DepartmentCustomerEventArgs> CustomerChanged;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private OutInspectsQueryAppService outInspectsQueryAppService = new OutInspectsQueryAppService();

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            AutoLoading(() =>
            {
                //总检
                gridView2.Columns[gridColumn23.FieldName].DisplayFormat.FormatType  = FormatType.Custom;
                gridView2.Columns[gridColumn23.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.SummSateFormatter);
                //体检SummSateFormatter
                gridView2.Columns[gridColumn22.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridView2.Columns[gridColumn22.FieldName].DisplayFormat.Format= new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);
                //打印
                gridView2.Columns[gridColumn25.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridView2.Columns[gridColumn25.FieldName].DisplayFormat.Format = new CustomFormatter(PrintTypeHelper.PrintType);
                //挂号科室
                var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
                repositoryItemLookUpEdit1.DataSource = ghks;
            }, Variables.LoadingForForm);
        }


        private void OutCheckList_Load(object sender, EventArgs e)
        {
            barPrintAppService = new BarPrintAppService();
            InitForm();
            //科室
          //  comboBoxEdit1.Properties.DataSource = _doctorStation.GetSearchClientInfoDto(new DeparmentCustomerSearch());
            //挂号科室
            var ghks = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.NucleicAcidtType.ToString())?.ToList();
            comboBoxEdit1.Properties.DataSource = ghks;
            customGridLookUpEdit1View.BestFitColumns();
          //  comboBoxEdit1.Properties.DataSource = CurrentUser.TbmDepartments.OrderBy(o => o.OrderNum).ToList();
            //体检状态
            lookUpEditExaminationStatus.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            lookUpEditExaminationStatus.ItemIndex = 0;
            //总检状态
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            lookUpEditSumStatus.ItemIndex = 0;
            //开始与结束时间
            dateEdit1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //条码状态
            //var lists = new List<EnumModel>();
            //lists.Add(new EnumModel { Id = 1, Name = "已打印" });
            //lists.Add(new EnumModel { Id = 2, Name = "未打印" });
            //lists.Add(new EnumModel { Id = 3, Name = "无需打印" });            
            comboBoxEdit2.Properties.DataSource = PrintSateHelper.GetSexModels();
          
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ListGuide s = new ListGuide();
            s.ShowDialog();
            simpleButton1.PerformClick();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            ////实例化接口

            OutCusInfoDto outinspectsQueryDto = new OutCusInfoDto();
            //开始时间
            if (!string.IsNullOrWhiteSpace(dateEdit1.DateTime.ToShortDateString()))
            {
                outinspectsQueryDto.startdate = DateTime.Parse(dateEdit1.DateTime.ToShortDateString());
            }
            //结束时间
            if (!string.IsNullOrWhiteSpace(dateEdit2.DateTime.ToShortDateString()))
            {
                outinspectsQueryDto.enddate = DateTime.Parse(dateEdit2.DateTime.AddDays(1).ToShortDateString());
            }
            //体检状态
            if (lookUpEditExaminationStatus.EditValue != null && lookUpEditExaminationStatus.Text != "全部")
            {
                outinspectsQueryDto.CheckSate = (int?)lookUpEditExaminationStatus.EditValue;
            }
            //体检号
            if (textEdit1.EditValue != null)
            {
                outinspectsQueryDto.CustomerBM = textEdit1.Text.Trim();
            }                 
                
            //登记序号
            if (textEdit2.EditValue != null)
            {
                outinspectsQueryDto.CustomerRegNum = Convert.ToInt32 ( textEdit2.Text.Trim());//不能用convert.int (int)也不行
            }     
            //挂号科室
            if (comboBoxEdit1.EditValue != null)
            {
                outinspectsQueryDto.NucleicAcidType = (int?)comboBoxEdit1.EditValue;//无法使用int.(parse)
            }
            //总检状态
            if (lookUpEditSumStatus.EditValue != null && lookUpEditSumStatus.Text != "全部")
            {
                outinspectsQueryDto.CheckSate = Convert.ToInt32(lookUpEditSumStatus.EditValue);
            }
            //条码打印
            if (comboBoxEdit2.EditValue != null && comboBoxEdit2.Text != "全部")
            {
                outinspectsQueryDto.BarState = (int?)comboBoxEdit2.EditValue;
            }
            var cuslist = _OutInspectsQueryAppService.OutinspectsQuery(outinspectsQueryDto);
            gridControl1.DataSource = cuslist;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            comboBoxEdit1.Text = ""; 
            comboBoxEdit2.Text = "";
            textEdit1.Text = "";
            textEdit2.Text = "";
            lookUpEditExaminationStatus.Text = "";
            lookUpEditSumStatus.Text = "";
            dateEdit1.Text = "";
            dateEdit2.Text = "";
           // OutCheckList outCheckList = new OutCheckList();
            

        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();            }
        }

 private void simpleButton4_Click(object sender, EventArgs e)
        {
            var strList = new List<string>() {
                 "登记序号",
                "姓名",
                "性别",
                "年龄",
                "移动电话",
                "身份证号",
            };
            JArray mb_jarray = new JArray();
            List<int> cellIndexs = new List<int>();
            GridControlHelper.DownloadTemplate(strList, "外出体检人员模板", mb_jarray, cellIndexs, "yyyy-MM-dd");

        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridView2.GetSelectedRows();
                if (selectIndexes.Length != 0)
                {
                    progressBarControl1.Properties.Maximum = selectIndexes.Length;
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                    System.Windows.Forms.Application.DoEvents();
                    string errarm = "";
                    foreach (var index in selectIndexes)
                    {
                        var tjzt = gridView2.GetRowCellValue(index, gridColumn22).ToString();
                        if (tjzt == "3")
                        {


                            //体检人信息
                            var rowData = (OutinspectsQueryDto)gridView2.GetRow(index);
                            var cusId = rowData.Id;
                            var cusAge = rowData.Age;
                            var cusSex = 1;
                            if (rowData.Sex.Contains("女"))
                            {
                                cusSex = 2;
                            }
                            //总检建议
                            var _customerSummarizeDto = _inspectionTotalService.GetSummarize(new TjlCustomerQuery
                            { CustomerRegID = cusId });
                            //体检人建议列表
                            var _CustomerSummarizeList = _inspectionTotalService.GetSummarizeBM(new TjlCustomerQuery
                            { CustomerRegID = cusId });


                            //获取科室小节
                            var _aTjlCustomerDepSummaryDto = _inspectionTotalService.GetCustomerDepSummaryList(new EntityDto<Guid>() { Id = cusId });
                            //生成总检汇总
                            string sum = LoadStr(_aTjlCustomerDepSummaryDto);
                            string advise = "";
                            if (sum == "")
                            {
                                advise = "* 本次体检所检项目未发现明显异常。";

                            }
                            _summarizeAdviceFull = DefinedCacheHelper.GetSummarizeAdvices().
                  Where(o => o.SexState == cusSex || o.SexState == cusSex ||
                  o.SexState == cusSex).
                  Where(o => o.MaxAge >= cusAge
                  && o.MinAge <= cusAge).ToList();
                            //匹配建议

                            _CustomerSummarizeList = MatchingAdvice(sum, cusId, _CustomerSummarizeList);

                            //保存总检

                            Save(cusId, _CustomerSummarizeList, _customerSummarizeDto, sum, rowData);
                        }
                        else
                        {
                            var arm = (string)gridView2.GetRowCellValue(index, gridColumn15);
                            errarm += arm + "体检未完成，不能自动总检" + Environment.NewLine;
                        }

                        //int num = index + 1;
                        //progressBarControl1.Text = num.ToString() +"/" + selectIndexes.Length.ToString();
                        //执行步长
                        progressBarControl1.PerformStep();
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                    }
                    if (errarm != "")
                    {
                        XtraMessageBox.Show(errarm);
                    }
                    else
                    {
                        XtraMessageBox.Show("成功生成汇总：" + selectIndexes.Length + "条！");
                    }
                }
            });
        }
        //保存方法
        private void Save(Guid cusRegId, List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList,
            TjlCustomerSummarizeDto _customerSummarizeDto, string sum, OutinspectsQueryDto _tjlCustomerRegDto)
        {
            try
            {

                //删除建议表（多条）
                _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = cusRegId });
                //插入建议表（多条）
                var SbStr = string.Empty;
                var _CustomerSummarizeBM = new List<TjlCustomerSummarizeBMDto>();
                foreach (var MatchingItem in _CustomerSummarizeList.OrderBy(l => l.SummarizeOrderNum))
                {
                    if (MatchingItem.SummarizeName.Trim() == string.Empty && MatchingItem.Advice.Trim() == string.Empty)
                    {
                        continue;
                    }
                    SbStr += "*" + MatchingItem.SummarizeName + "：" + MatchingItem.Advice;
                    //MatchingItem.Id = Guid.NewGuid();
                    _CustomerSummarizeBM.Add(MatchingItem);
                }
                _inspectionTotalService.CreateSummarizeBM(_CustomerSummarizeBM);

                if (_customerSummarizeDto == null)
                {
                    //插入建议汇总 //插入建议表（整体建议 单条）
                    var _TjlCustomerSummarize = new TjlCustomerSummarizeDto();
                    _TjlCustomerSummarize.CustomerRegID = cusRegId;
                    _TjlCustomerSummarize.ShEmployeeBMId = CurrentUser.Id;
                    _TjlCustomerSummarize.EmployeeBMId = CurrentUser.Id;
                    _TjlCustomerSummarize.CharacterSummary = sum;
                    _TjlCustomerSummarize.Advice = SbStr;
                    _TjlCustomerSummarize.ConclusionDate = _commonAppService.GetDateTimeNow().Now;
                    _TjlCustomerSummarize.CheckState = 1;
                    var result = _inspectionTotalService.CreateSummarize(_TjlCustomerSummarize);
                    _customerSummarizeDto = result;
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                    createOpLogDto.LogText = "批量保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                else
                {
                    _customerSummarizeDto.CustomerRegID = cusRegId;
                    _customerSummarizeDto.ShEmployeeBMId = CurrentUser.Id;
                    _customerSummarizeDto.EmployeeBMId = CurrentUser.Id;
                    _customerSummarizeDto.CharacterSummary = sum;
                    _customerSummarizeDto.Advice = SbStr;
                    _customerSummarizeDto.ConclusionDate = _commonAppService.GetDateTimeNow().Now;
                    _customerSummarizeDto.CheckState = 1;
                    var result = _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    _customerSummarizeDto = result;
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                    createOpLogDto.LogText = "批量保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }

                //更新患者体检信息表
                EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                editCustomerRegStateDto.SummLocked =2;
                editCustomerRegStateDto.CSEmployeeId = CurrentUser.Id;
                editCustomerRegStateDto.SummSate = (int)SummSate.HasInitialReview;
                editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                _inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);


            }
            catch (UserFriendlyException c)
            {
                XtraMessageBox.Show(c.Message);
            }
        }
        //总检结论拼接
        private string LoadStr(List<ATjlCustomerDepSummaryDto> _aTjlCustomerDepSummaryDto)
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
                    ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                    str += ZjFormat;
                }

            }

            return str;
        }
        //匹配建议算法
        private List<TjlCustomerSummarizeBMDto> MatchingAdvice(string summ, Guid cusRegId, List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList)
        {
            //建议汇总数据
            var StrContent = summ;
            //建议汇总数据
            var StrContent2 = summ;
            //存储建议Id集合
            List<Guid> IdList = new List<Guid>();
            //遍历科室建议 
            foreach (var Ditem in _summarizeAdviceFull.OrderByDescending(n => n.AdviceName.Length).ToList())
                if (!string.IsNullOrWhiteSpace(Ditem.AdviceName))
                    if (!string.IsNullOrWhiteSpace(StrContent))
                        if (StrContent.Contains(Ditem.AdviceName))
                        {
                            IdList.Add(Ditem.Id);
                            StrContent = StrContent.Replace(Ditem.AdviceName, "");
                        }
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
                _CustomerSummarizeList.Add(SummarizeBMToJL(info[i], cusRegId));
            }
            return _CustomerSummarizeList;
        }
        //将下拉框选择的总检诊断编码数据转换为记录表数据
        public TjlCustomerSummarizeBMDto SummarizeBMToJL(SummarizeAdviceDto bmInfo, Guid cusregId)
        {
            TjlCustomerSummarizeBMDto info = new TjlCustomerSummarizeBMDto();
            if (bmInfo == null)
                return info;
            info.Id = Guid.NewGuid(); //转换时赋ID会导致保存时报错，原因由于ID重复
            info.CustomerRegID = cusregId;
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
            return info;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridView2.GetSelectedRows();
                if (selectIndexes.Length != 0)
                {
                    progressBarControl1.Properties.Maximum = selectIndexes.Length;
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                    System.Windows.Forms.Application.DoEvents();
                    string errarm = "";
                    foreach (var index in selectIndexes)
                    {

                        //体检人信息
                        var rowData = (OutinspectsQueryDto)gridView2.GetRow(index);

                        string err = UpdLoads(rowData.CustomerBM, rowData.Name, rowData.IDCardNo);
                        if (err != "")
                        {
                            errarm += err;
                        }
                        //执行步长
                        progressBarControl1.PerformStep();
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                    }
                    if (errarm != "")
                    {
                        XtraMessageBox.Show(errarm);
                    }
                    else
                    {
                        XtraMessageBox.Show("成功上传公卫：" + selectIndexes.Length + "条！");
                    }
                }
            });
        }
        public string UpdLoads(string CustomerRegID, string Name, string IDCard)
        {

            string rets = "";
            try
            {
                GsearchCustomerDto GsearchCustomer = new GsearchCustomerDto();
                GsearchCustomer.CustomerBM = CustomerRegID;
                GsearchCustomer.Name = Name;
                GsearchCustomer.IdCardNo = IDCard;
                var DoctorNames = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.PublicHealthURL
                  ).ToString()).FirstOrDefault(o => o.Value == 8).Remarks;
                GsearchCustomer.DoctorName = DoctorNames;
                var DoctorJGDM = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.PublicHealthURL
                 ).ToString()).FirstOrDefault(o => o.Value == 9).Remarks;
                GsearchCustomer.DoctorJgdm = DoctorJGDM;
                var gwtype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.PublicHealthURL
               ).ToString()).FirstOrDefault(o => o.Value == 6);
                //上海万达，厦门置业
                if (gwtype != null && gwtype.Remarks == "上海万达")
                {
                    try
                    {
                        var res = customerSvr1.OutTJData(GsearchCustomer);
                        if (res.Code == 0)
                        {
                            rets = "一体机：" + res.ErrInfo;
                        }
                    }
                    catch (Exception ex)
                    {
                        rets += "一体机接口异常：" + ex.Message;
                    }
                }
                else
                {
                    try
                    {
                        var res = customerSvr1.OutDataSouce(GsearchCustomer);
                        if (res.Code == 0)
                        {
                            rets = "一体机：" + res.ErrInfo;
                        }
                    }
                    catch (Exception ex)
                    {
                        rets += "一体机接口异常：" + ex.Message;
                    }
                    try
                    {
                        var ress = customerSvr1.UpdateLisReport(GsearchCustomer);
                        if (ress.Code == 0)
                        {
                            rets += "LIS：" + ress.ErrInfo;
                        }
                    }
                    catch (Exception ex)
                    {
                        rets += "LIS接口异常：" + ex.Message;
                    }
                }
                return rets;
            }
            catch (Exception ex)
            {
                return rets + ex.Message;

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //OneBarPrintAll();
            BarPrintAll();
            simpleButton1.PerformClick();
        }
        /// 打印条码
        /// </summary>
        public void BarPrintAll()
        {



            ReportMain.Clear();
            var BarName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1001);
            if (BarName == null)
            {
                MessageBox.Show("请增加条码清单模板设置，字典设置-条码设置，编码1001");
                return;
            }
            if (string.IsNullOrWhiteSpace(BarName?.Remarks))
            {
                MessageBox.Show("字典设置-条码设置，编码1001,备注（模板名称）为空，请维护！");
                return;
            }
            var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1001).Remarks;
           // var StrPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 20).Remarks;
            //打印机设置
            var StrPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 90)?.Remarks;
            //if (!string.IsNullOrEmpty(printName) && string.IsNullOrEmpty(StrPrintName))
            //{
            //    StrPrintName = printName;
            //}
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            ReportMain.LoadFromURL(GrdPath);
            ReportMain.Printer.PrinterName = StrPrintName;
            prinall();
            ReportMain.Print(false);


            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
        }
        private void prinall()
        {
            if (barPrintAppService == null)
            {
                barPrintAppService = new BarPrintAppService();
            }
            var selectIndexes = gridView2.GetSelectedRows();

            if (selectIndexes.Length != 0)
            {
                List<CusNameInput> cusNameInputlist = new List<CusNameInput>();

                foreach (var index in selectIndexes)
                {
                    DetailDto detailDto = new DetailDto();
                    //体检人信息
                    var rowData = (OutinspectsQueryDto)gridView2.GetRow(index);
                    CusNameInput cusNameInput = new CusNameInput();
                    cusNameInput.Id = rowData.Id;
                    cusNameInput.Theme = "1";
                    cusNameInputlist.Add(cusNameInput);
                }
                var lstcustomerBarPrintInfoDtosall = barPrintAppService.GetAllBarPrint(cusNameInputlist);

                var reportJsonForExamine = lstcustomerBarPrintInfoDtosall;
                var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonForExamine);
                ReportMain.LoadDataFromXML(reportJsonForExamineString);
            }
        }
        /// <summary>
        /// 单位批量打条码
        /// </summary>
        public void OneBarPrintAll()
        {

            
                ReportMain.Clear();
            var BarName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1001);
            if (BarName == null)
            {
                MessageBox.Show("请增加条码清单模板设置，字典设置-条码设置，编码1001");
                return;
            }
            if (string.IsNullOrWhiteSpace(BarName?.Remarks))
            {
                MessageBox.Show("字典设置-条码设置，编码1001,备注（模板名称）为空，请维护！");
                return;
            }
                var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.BarPrintSet, 1001).Remarks;
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
               Oneprinall();                
                ReportMain.Print(false);
                
           
            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
        }
        private void Oneprinall()
        {
            if (barPrintAppService == null)
            {
                barPrintAppService = new BarPrintAppService();
            }
            var selectIndexes = gridView2.GetSelectedRows();

            if (selectIndexes.Length != 0)
            {
                var lstcustomerBarPrintInfoDtosall = new ReportJsonDto();
                lstcustomerBarPrintInfoDtosall.Detail = new List<DetailDto>();

                foreach (var index in selectIndexes)
                {
                    DetailDto detailDto = new DetailDto();
                        //体检人信息
                        var rowData = (OutinspectsQueryDto)gridView2.GetRow(index);

                    detailDto.CustomerExaminationNumber = rowData.CustomerBM.ToString();
                    string strname = rowData.Name + " " +
                       rowData.Sex + " " +
                        rowData.Age.ToString();
                    detailDto.CustomerName = strname;
                    detailDto.CustomerRegNum = rowData.CustomerRegNum;
                    lstcustomerBarPrintInfoDtosall.Detail.Add(detailDto);
                  //更新条码打印状态
                  ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Id = rowData.Id;
                    chargeBM.Name = "条码";
                    barPrintAppService.UpdateCustomerRegisterPrintState(chargeBM);
                }
              

                var reportJsonForExamine = lstcustomerBarPrintInfoDtosall;
                var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonForExamine);
                ReportMain.LoadDataFromXML(reportJsonForExamineString);
            }
            else
            {
                XtraMessageBox.Show("请选择需要人员！");
            }

           

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            OneBarPrintAll();
            simpleButton1.PerformClick();
        }
    }
}
