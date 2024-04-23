using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Company;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class FrmInspectionTotalListAUTO : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;

        private readonly IInspectionTotalAppService _inspectionTotalService;
        /// <summary>
        /// 查询建议字典--初始化读取，后续生成总检使用
        /// </summary>
        private List<SummarizeAdviceDto> _summarizeAdviceFull = new List<SummarizeAdviceDto>();

        // 建议字典
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        public ICustomerAppService customerSvr1 = new CustomerAppService();//体检预约
        private TjlCustomerQuery cusrrTjlCustomerQuery = new TjlCustomerQuery();
        // 医生站
        private readonly IDoctorStationAppService _doctorStationAppService;
        
        public FrmInspectionTotalListAUTO()
        {
            InitializeComponent();
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
            _doctorStationAppService = new DoctorStationAppService();
            repositoryItemLookUpEditPhysicalType.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();

        }

        private void FrmInspectionTotalList_Load(object sender, EventArgs e)
        {
            InitForm();
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
            lookUpEditExaminationCategories.Properties.DisplayMember = "Text";
            lookUpEditExaminationCategories.Properties.ValueMember = "Value";
            if (Examination.Count > 0)
            {
                lookUpEditExaminationCategories.EditValue = Examination[0].Value;
            }
          
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\Inspection.json";
            if (File.Exists(fp))  // 判断是否已有相同文件 
            {
                var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fp));
                foreach (var tj in Search)
                {
                    if (tj.Name == "CheckType")
                    {
                        //if (Examination.Any(p => p.Value == int.Parse(tj.Text)))
                        //{
                            lookUpEditExaminationCategories.EditValue = tj.Text;
                            lookUpEditExaminationCategories.RefreshEditValue();
                   
 
                        //}
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

            LoadData();
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            AutoLoading(() =>
            {
                gridViewCustomerReg.IndicatorWidth = 40;
                var dataDto = _commonAppService.GetDateTimeNow();
                textEditDayNum.Text = "7";
                dateEditStartTime.DateTime = dataDto.Now.Date.AddDays(-1);
                dateEditEndTime.DateTime = dataDto.Now.Date;

                //gridViewCustomerReg.Columns[gridColumnXingBie.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                //gridViewCustomerReg.Columns[gridColumnXingBie.FieldName].DisplayFormat.Format =
                //    new CustomFormatter(SexHelper.CustomSexFormatter);
                //gridViewCustomerReg.Columns[gridColumnZhuangTai.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                //gridViewCustomerReg.Columns[gridColumnZhuangTai.FieldName].DisplayFormat.Format =
                //    new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);
                //gridViewCustomerReg.Columns[gridColumnSuoDing.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                //gridViewCustomerReg.Columns[gridColumnSuoDing.FieldName].DisplayFormat.Format =
                //    new CustomFormatter(CheckSateHelper.SummLockedFormatter);
                //gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                //gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.Format =
                //    new CustomFormatter(CheckSateHelper.ReceiveSateFormatter); 
                //gridViewCustomerReg.Columns[gridColumnZongJian.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                //gridViewCustomerReg.Columns[gridColumnZongJian.FieldName].DisplayFormat.Format =
                //    new CustomFormatter(CheckSateHelper.SummSateFormatter); 
                gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCustomerReg.Columns[gridColumnLingQu.FieldName].DisplayFormat.Format =
                    new CustomFormatter(CheckSateHelper.PrintSateFormatter);
                gridViewCustomerReg.Columns[conUploadState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCustomerReg.Columns[conUploadState.FieldName].DisplayFormat.Format =
                    new CustomFormatter(UploadStateHelper.UploadStateFormatter);

                
            }, Variables.LoadingForForm);
        }
        
        private void simpleButtonCheck_Click(object sender, EventArgs e)
        {
            //simpleButtonCheck.Enabled = false;
            //var dto = gridControl.GetFocusedRowDto<InspectionTotalListDto>();
            var dto = gridControl.GetFocusedRowDto<TjlCustomerRegForInspectionTotalSearchDto>();
            if (dto == null)
            {
                ShowMessageBoxWarning("请选中行！");
            }
            else
            {
                //var Info = _inspectionTotalService.GetCustomerRegList(new TjlCustomerQuery { CustomerRegID = dto.Id });
                //if (Info == null || Info.Count == 0)
                //    return;
                
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == dto.PhysicalType)?.Text;
                //if (tjlb!=null  && tjlb.Contains("职业+健康"))
                //{

                //    //var nowdate = ModelHelper.CustomMapTo2<TjlCustomerRegForInspectionTotalSearchDto, TjlCustomerRegForInspectionTotalDto>(
                //    //   dto);
                //    var nowdate = _inspectionTotalService.Transformation(dto);
                //    FrmInspectionTotalZYB frmInspectionTotal = new FrmInspectionTotalZYB(nowdate)
                //    {
                //        WindowState = FormWindowState.Maximized
                //    };
                //    frmInspectionTotal.SimpleButtonSaveClick += (s, t) =>
                //    {
                //        LoadData();
                //        for (int i = 0; i < gridViewCustomerReg.RowCount; i++)
                //        {
                //            //var rowData = (InspectionTotalListDto)gridViewCustomerReg.GetRow(i);
                //            var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(i);
                //            frmInspectionTotal.tjlCustomerQuery = cusrrTjlCustomerQuery;
                //            if (frmInspectionTotal._tjlCustomerRegDto.Id == rowData.Id)
                //            {
                //                gridViewCustomerReg.FocusedRowHandle = i;
                //                return;
                //            }
                //        }
                //    };
                //    frmInspectionTotal.Show();
                //    simpleButtonCheck.Enabled = true;
                //}
                //else
                //{

                    //var nowdate = ModelHelper.CustomMapTo2<TjlCustomerRegForInspectionTotalSearchDto, TjlCustomerRegForInspectionTotalDto>(
                    //   dto);
                    var nowdate = _inspectionTotalService.Transformation(dto);
                    FrmInspectionTotal frmInspectionTotal = new FrmInspectionTotal(nowdate)
                    {
                        WindowState = FormWindowState.Maximized
                    };
                    frmInspectionTotal.SimpleButtonSaveClick += (s, t) =>
                    {
                        LoadData();
                        for (int i = 0; i < gridViewCustomerReg.RowCount; i++)
                        {
                            //var rowData = (InspectionTotalListDto)gridViewCustomerReg.GetRow(i);
                            var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(i);
                            frmInspectionTotal.tjlCustomerQuery = cusrrTjlCustomerQuery;
                            if (frmInspectionTotal._tjlCustomerRegDto.Id == rowData.Id)
                            {
                                gridViewCustomerReg.FocusedRowHandle = i;
                                return;
                            }
                        }
                    };
                    frmInspectionTotal.Show();
                    //simpleButtonCheck.Enabled = true;
                //}
                    
            }
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
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
                        if (comDataType.Text.Contains("体检日期"))
                        { input.DateType = 1; }
                        else if (comDataType.Text.Contains("总检日期"))
                        { input.DateType = 2; }
                        else
                        { input.DateType = 0; }
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
                if (lookUpEditSumStatus.EditValue != null && lookUpEditSumStatus.Text!="全部")
                {
                    input.SumState = (int)lookUpEditSumStatus.EditValue;
                }
                if(lookUpEditExaminationStatus.EditValue !=null && lookUpEditExaminationStatus.Text !="全部")
                {
                    input.CheckState = (int)lookUpEditExaminationStatus.EditValue;
                }
                cusrrTjlCustomerQuery = input;
                var output = _inspectionTotalService.GetCustomerRegList(input).Where(o => o.RegisterState == (int)RegisterState.Yes).ToList();
                if (!string.IsNullOrEmpty(lookUpEditExaminationCategories.EditValue?.ToString()) && !lookUpEditExaminationCategories.EditValue.ToString().Contains("-1"))
                {
                    var IDlist = lookUpEditExaminationCategories.EditValue?.ToString().Split(',').ToList();
                    var IdS = new List<int>();
                    foreach (var id in IDlist)
                    {
                        if (int.TryParse(id, out int outID))
                        {
                            IdS.Add(outID);
                        }
                    }
                    cusrrTjlCustomerQuery.PhysicalTypelist = IdS;
                    output = output.Where(p => p.PhysicalType.HasValue && IdS.Contains(p.PhysicalType.Value)).ToList();
                }
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);
                var zjcoutn = output.Count(o => o.SummSate == (int)SummSate.HasInitialReview);
                var SHcoutn = output.Count(o => o.SummSate == (int)SummSate.Audited);
                var tjzWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Process && o.SummSate == (int)SummSate.NotAlwaysCheck);
                var twcWSh = output.Count(o => o.CheckSate == (int)PhysicalEState.Complete && o.SummSate == (int)SummSate.NotAlwaysCheck);
                labelControl1.Text = "总人数：" + sum + "，已审核:" + SHcoutn + "，已初审：" + zjcoutn + "体检完成未总检查：" + twcWSh + "，体检中未总检：" + tjzWSh;
                gridControl.DataSource = output;
                gridViewCustomerReg.BestFitColumns();
                
                //绑定诊断搜索下拉框
                var userDto = DefinedCacheHelper.GetComboUsers();
                txtsearchDoctor.Properties.DataSource = userDto;
                txtsearchDoctor.Properties.ValueMember = "Id";
                txtsearchDoctor.Properties.DisplayMember = "Name";
              
            });
        }

        //private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        //{
        //    DataNavigatorButtonClick(sender, e);
        //    LoadData();
        //}

        private void gridViewCustomerReg_RowClick(object sender, RowClickEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 2)
                //simpleButtonCheck.PerformClick();
        }

        private void gridViewCustomerReg_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void txtsearchDoctor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                txtsearchDoctor.EditValue = null;
            }
        }

        private void gridViewCustomerReg_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "CSEmployeeId" || e.Column.FieldName == "FSEmployeeId")
            {
                long id = 0;
                if (long.TryParse(e.DisplayText, out id))
                {
                    var userInfo = DefinedCacheHelper.GetComboUsers().Find(n => n.Id == long.Parse(e.DisplayText));
                    if (userInfo == null)
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = string.IsNullOrWhiteSpace(e.DisplayText) ? "" : userInfo.Name;
                    }
                }
            }
        }

        private void textEditName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textEditName.Text))
            {
                LoadData();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCustomerReg.GetSelectedRows();
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
                        var tjzt = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnZhuangTai);
                        if (tjzt == "体检完成")
                        {
                            //生成总检
                            var cusId = (Guid)gridViewCustomerReg.GetRowCellValue(index, conId);

                            var cusAge = (int)gridViewCustomerReg.GetRowCellValue(index, gridColumnNianLing);
                            //体检人信息
                            var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(index);
                            var cusSex = rowData.Customer.Sex;
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
                            var ad = DefinedCacheHelper.GetSummarizeAdvices().ToList();
                            _summarizeAdviceFull = ad.Where(o => o.SexState == cusSex || o.SexState == (int)Sex.GenderNotSpecified || o.SexState == (int)Sex.Unknown).
                  Where(o => o.MaxAge >= cusAge
                  && o.MinAge <= cusAge).ToList();
                            //匹配建议

                            _CustomerSummarizeList = MatchingAdvice(sum, cusId, _CustomerSummarizeList);

                            //保存总检

                            var nowdate = _inspectionTotalService.Transformation(rowData);
                            Save(cusId, _CustomerSummarizeList, _customerSummarizeDto, sum, nowdate);
                        }
                        else
                        {
                            var arm = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnBianHao);
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
            TjlCustomerSummarizeDto _customerSummarizeDto,string sum, TjlCustomerRegForInspectionTotalDto _tjlCustomerRegDto)
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
                    _TjlCustomerSummarize.CheckState = (int)SummSate.HasInitialReview;                 
                    var result = _inspectionTotalService.CreateSummarize(_TjlCustomerSummarize);
                    _customerSummarizeDto = result;
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
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
                    _customerSummarizeDto.CheckState = (int)SummSate.HasInitialReview;
                    var result = _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
                    _customerSummarizeDto = result;
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM; 
                    createOpLogDto.LogName = _tjlCustomerRegDto.Customer.Name;
                    createOpLogDto.LogText = "批量保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
              
                //更新患者体检信息表
                _tjlCustomerRegDto.SummSate = (int)SummSate.HasInitialReview;
                _tjlCustomerRegDto.SummLocked = 2;
                _tjlCustomerRegDto.CSEmployeeId = CurrentUser.Id;
                _inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);


            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
            }
        }
        //总检结论拼接
        //private string LoadStr(List<ATjlCustomerDepSummaryDto> _aTjlCustomerDepSummaryDto)
        //{
        //    //var dto = _customerRegDto.CustomerDepSummary.OrderBy(o => o.DepartmentOrder).ToList();
        //    var dto = _aTjlCustomerDepSummaryDto.OrderBy(o => o.DepartmentOrder).ToList();
        //    var str = "";
        //    var departmentName = string.Empty;
        //    var iCount = 1;
        //    for (var i = 0; i < dto.Count; i++)
        //    {
        //        if (string.IsNullOrWhiteSpace(dto[i].DagnosisSummary) && string.IsNullOrWhiteSpace(dto[i].CharacterSummary))
        //            continue;
        //        //字典中国屏蔽的科室诊断
        //        var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
        //        if (IsYC != null && IsYC.Remarks == "0")
        //        {
        //            var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
        //            if (IsYCgjc != null && IsYCgjc.Remarks != "")
        //            {
        //                string[] gjcls = IsYCgjc.Remarks.Split('|');
        //                bool isZC = false;
        //                foreach (string gjc in gjcls)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(dto[i].DagnosisSummary))
        //                    {
        //                        if (dto[i].DagnosisSummary.Replace(" ", "").Trim() == gjc)
        //                        {
        //                            isZC = true;
        //                            continue;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        if (dto[i].CharacterSummary.Replace(" ", "").Trim() == gjc)
        //                        {
        //                            isZC = true;
        //                            continue;
        //                        }
        //                    }
        //                }
        //                if (isZC)
        //                {
        //                    continue;
        //                }
        //            }
        //        }
        //        var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 3);
        //        string ZjFormat = ZjFormatd?.Remarks ?? "";

        //        if (departmentName != dto[i].DepartmentName)
        //        {
        //            if (ZjFormat != "")
        //            {
        //                ZjFormat = ZjFormat.Replace("【序号】", iCount.ToString()).Replace("【科室名称】", dto[i].DepartmentName);
        //            }
        //            else
        //            {
        //                str += $"{iCount}.{dto[i].DepartmentName}{ Environment.NewLine }";
        //            }
        //            departmentName = dto[i].DepartmentName;
        //            iCount++;

        //        }

        //        if (!string.IsNullOrWhiteSpace(dto[i].DagnosisSummary))
        //        {
        //            if (ZjFormat != "")
        //            {
        //                ZjFormat = ZjFormat.Replace("【科室小结】", dto[i].DagnosisSummary.TrimEnd((char[])"\r\n".ToCharArray()));
        //            }
        //            else
        //            {

        //                var spStr = dto[i].DagnosisSummary.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //                for (var j = 0; j < spStr.Count(); j++)
        //                    if (!string.IsNullOrWhiteSpace(spStr[j]))
        //                    {
        //                        str += $"{spStr[j]}{ Environment.NewLine }";
        //                    }
        //            }
        //        }
        //        else if (!string.IsNullOrWhiteSpace(dto[i].CharacterSummary))
        //        {
        //            if (ZjFormat != "")
        //            {
        //                ZjFormat += ZjFormat.Replace("【科室小结】", dto[i].CharacterSummary.TrimEnd((char[])"\r\n".ToCharArray()));
        //            }
        //            else
        //            {

        //                var spStr = dto[i].CharacterSummary.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //                for (var j = 0; j < spStr.Count(); j++)
        //                    if (!string.IsNullOrWhiteSpace(spStr[j]))
        //                    {
        //                        str += $"{spStr[j]}{ Environment.NewLine }";
        //                    }
        //            }
        //        }
        //        if (ZjFormat != "")
        //        {
        //            ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
        //            str += ZjFormat;
        //        }

        //    }

        //   return str;
        //}
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
                    //str += ZjFormat;
                    str += $"{ZjFormat}{ Environment.NewLine }"; ;
                }

            }
            return str;

            ////隐私项目
            //var ysdeparSum = _aTjlCustomerDepSummaryDto.Where(o => o.PrivacyCharacterSummary != null
            // && o.PrivacyCharacterSummary != "").ToList();
            //if (ysdeparSum.Count > 0)
            //{
            //    var Psum = ysdeparSum.Select(o => o.PrivacyCharacterSummary).ToList();

            //}
            //else
            //{

            //}
        }
        //匹配建议算法
        private List<TjlCustomerSummarizeBMDto> MatchingAdvice(string summ ,Guid cusRegId, List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList)
        {
            //建议汇总数据
            var StrContent = summ;
            //建议汇总数据
            var StrContent2 = summ;
            ////存储建议Id集合
            //List<Guid> IdList = new List<Guid>();
            ////遍历科室建议 
            //foreach (var Ditem in _summarizeAdviceFull.OrderByDescending(n => n.AdviceName.Length).ToList())
            //    if (!string.IsNullOrWhiteSpace(Ditem.AdviceName))
            //        if (!string.IsNullOrWhiteSpace(StrContent))
            //            if (StrContent.Contains(Ditem.AdviceName))
            //            {
            //                IdList.Add(Ditem.Id);
            //                StrContent = StrContent.Replace(Ditem.AdviceName, "");
            //            }
            //List<SummarizeAdviceDto> info = _summarizeAdviceAppService.GetSummForGuidList(IdList);
            ////按照汇总顺序重新排列诊断数据
            //foreach (var item in info)
            //{
            //    item.IndexOfNum = StrContent2.IndexOf(item.AdviceName);
            //}
            //info = info.OrderBy(n => n.IndexOfNum).ToList();
            // _CustomerSummarizeList = new List<TjlCustomerSummarizeBMDto>();
            ////清除已选诊断项目
            //foreach (var item in _CustomerSummarizeList)
            //{
            //    foreach (var itemInfo in info)
            //    {
            //        if (item.SummarizeAdviceId == itemInfo.Id)
            //        {
            //            info.Remove(itemInfo);
            //            break;
            //        }
            //    }
            //}
            ////将新加入的诊断项目进行序号重排，并转换记录表对象，放入集合
            //for (int i = 0; i < info.Count(); i++)
            //{
            //    info[i].OrderNum = _CustomerSummarizeList.Count() + 1;
            //    _CustomerSummarizeList.Add(SummarizeBMToJL(info[i], cusRegId));
            //}
            //return _CustomerSummarizeList;


            //存储建议Id集合
            List<Guid> IdList = new List<Guid>();         
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
                            if (!string.IsNullOrEmpty(ad))
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
                                        if (StrContent.Contains(str + Ditem.AdviceName))
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
                    }
                }        
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
                _CustomerSummarizeList.Add(SummarizeBMToJL(info[i], cusRegId));
              
            }
            return _CustomerSummarizeList;
        }
        //将下拉框选择的总检诊断编码数据转换为记录表数据
        public TjlCustomerSummarizeBMDto SummarizeBMToJL(SummarizeAdviceDto bmInfo,Guid cusregId)
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
            info.IsPrivacy = false;
            return info;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCustomerReg.GetSelectedRows();
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
                        var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(index);
                        
                      string err= UpdLoads(rowData.CustomerBM, rowData.Customer.Name, rowData.Customer.IDCardNo);
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
        public string UpdLoad(string CustomerRegID, string Name, string IDCard)
        {
            
            string rets = "";
            try
            {
                GsearchCustomerDto GsearchCustomer = new GsearchCustomerDto();  
                GsearchCustomer.CustomerBM = CustomerRegID;
                GsearchCustomer.Name = Name;
                GsearchCustomer.IdCardNo = IDCard;

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
                return rets;
            }
            catch (Exception ex)
            {
                return rets + ex.Message;

            }
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
                var  gwtype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.PublicHealthURL
                ).ToString()).FirstOrDefault(o => o.Value==6);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmIntReSult frmIntReSult = new FrmIntReSult();
            frmIntReSult.ShowDialog();
        }

        private void sleDW_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                sleDW.EditValue = null;
            }
        }

        private void lookUpEditSumStatus_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookUpEditSumStatus.EditValue = null;
            }
        }

        private void lookUpEditExaminationStatus_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookUpEditExaminationStatus.EditValue = null;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\Inspection.json";

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
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCustomerReg.GetSelectedRows();
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
                    int OKcusCount = 0;
                    int ErrcusCount = 0;
                    string ErrArch = "";
                    foreach (var index in selectIndexes)
                    {
                       
                           
                            //体检人信息
                            var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(index);
                        TdbInterfaceDocWhere tdbInterfaceWhere = new TdbInterfaceDocWhere();
                        tdbInterfaceWhere.inactivenum = rowData.CustomerBM;                      
                        InterfaceBack Back = new InterfaceBack();

                        Back = _doctorStationAppService.ConvertInterface(tdbInterfaceWhere);
                        if (!string.IsNullOrEmpty(Back.StrBui.ToString().Trim()))
                        {
                            errarm += Back.StrBui.ToString();
                            ErrcusCount += 1;
                            ErrArch += rowData.CustomerBM + ";";
                        }
                        else
                        {
                            OKcusCount += 1;

                        }


                        progressBarControl1.PerformStep();
                        //处理当前消息队列中的所有windows消息,不然进度条会不同步
                        System.Windows.Forms.Application.DoEvents();
                    }
                    if (errarm != "")
                    {
                        XtraMessageBox.Show("失败上传：" + ErrcusCount +"条数据，体检号：" + ErrArch+"\r\n异常详情：" + errarm
                            ) ;
                 
                    }
                    else
                    {
                        XtraMessageBox.Show("获取数据：" + OKcusCount + "条！");
                    }
                }
            });
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCustomerReg.GetSelectedRows();
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
                        var cusId = (Guid)gridViewCustomerReg.GetRowCellValue(index, conId);
                        EntityDto<Guid> entityDto = new EntityDto<Guid>();
                        entityDto.Id = cusId;
                        customerSvr1.UpCusUploadState(entityDto);
                        progressBarControl1.PerformStep();
                   
                        System.Windows.Forms.Application.DoEvents();
                    }
                    if (errarm != "")
                    {
                        XtraMessageBox.Show(errarm);
                    }
                    else
                    {
                        XtraMessageBox.Show("疾控状态上传成功！" + selectIndexes.Length + "条！");
                    }
                    simpleButtonQuery.PerformClick();
                }
            });
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCustomerReg.GetSelectedRows();
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
                        var tjzt = (string)gridViewCustomerReg.GetRowCellValue(index, gridColumnZhuangTai);
                        var rowData = (TjlCustomerRegForInspectionTotalSearchDto)gridViewCustomerReg.GetRow(index);

                        EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                        editCustomerRegStateDto.SummLocked = (int)SummLockedState.Unchecked;
                        if (rowData.CSEmployeeId.HasValue)
                        {

                            editCustomerRegStateDto.CSEmployeeId = rowData.CSEmployeeId;
                        }
                        else
                        {
                            editCustomerRegStateDto.CSEmployeeId = CurrentUser.Id;
                        }
                        if (rowData.FSEmployeeId.HasValue)
                        {

                            editCustomerRegStateDto.FSEmployeeId = rowData.FSEmployeeId;
                        }
                        else
                        {
                            editCustomerRegStateDto.FSEmployeeId = CurrentUser.Id;
                        }

                        editCustomerRegStateDto.SummSate = (int)SummSate.Audited;

                        
                        editCustomerRegStateDto.Id = rowData.Id;
                        _inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);


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

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            ExcelHelper.GridViewToExcel(gridViewCustomerReg, "总检列表", "总检列表");

        }
    }
}