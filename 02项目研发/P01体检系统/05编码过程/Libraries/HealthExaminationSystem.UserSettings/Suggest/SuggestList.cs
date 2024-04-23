using System;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Common;
using System.Collections.Generic;
using System.Data;
using System.IO;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System.Linq;
namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest
{
    public partial class SuggestList : UserBaseForm
    {
        private readonly ISummarizeAdviceAppService service = new SummarizeAdviceAppService();
        public FullSummarizeAdviceDto _Model { get; private set; }
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        private IIDNumberAppService iIDNumberAppService = new IDNumberAppService();
        private IWorkbook _workbook;
        public string currdepar = "";
        public SuggestList()
        {
            InitializeComponent();

            gvSummarizeAdvice.Columns[gvSummarizeAdviceCrisisSate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceCrisisSate.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvSummarizeAdvice.Columns[gvSummarizeAdviceSummState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceSummState.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvSummarizeAdvice.Columns[gvSummarizeAdviceDiagnosisSate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceDiagnosisSate.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);
            gvSummarizeAdvice.Columns[gvSummarizeAdviceHideOnGroupReport.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceHideOnGroupReport.FieldName].DisplayFormat.Format = new CustomFormatter(CommonFormat.YesOrNoFormatter);

            gvSummarizeAdvice.Columns[gvSummarizeAdviceSexState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceSexState.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            gvSummarizeAdvice.Columns[gvSummarizeAdviceMarrySate.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceMarrySate.FieldName].DisplayFormat.Format = new CustomFormatter(MarrySateHelper.CustomMarrySateFormatter);
            
            gvSummarizeAdvice.Columns[gvSummarizeAdviceDiagnosisAType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvSummarizeAdvice.Columns[gvSummarizeAdviceDiagnosisAType.FieldName].DisplayFormat.Format = new CustomFormatter((obj)=> CommonFormat.BasicDictionaryFormatter(BasicDictionaryType.DiagnosisAType, obj));

            if (_Model == null) _Model = new FullSummarizeAdviceDto();
            lueSexState.SetClearButton();
            lueMarrySate.SetClearButton();
            lueDiagnosisAType.SetClearButton();
            lueSexState.EditValue = (int)Sex.GenderNotSpecified;
            lueMarrySate.EditValue = (int)MarrySate.Unstated;
        }

        #region 事件
        private void SummarizeAdvice_Load(object sender, EventArgs e)
        {
            LoadControlData();
            Reload();
            currdepar = "";
        }
        private void sbReload_Click(object sender, EventArgs e)
        {
            currdepar = "";
            Reload();
        }
        private void sbAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void sbEdit_Click(object sender, EventArgs e)
        {
            //var dto = gridControl.GetFocusedRowDto<Edit>();
            //Edit(dto);
        }
        private void sbDel_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
            Del(dto);
        }
        private void gvSummarizeAdvice_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var dto = (FullSummarizeAdviceDto)gvSummarizeAdvice.GetRow(e.RowHandle);
                Edits(dto);
                //Edit(dto);
            }
        }
        private void gvSummarizeAdvice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dto = gvSummarizeAdvice.GetRow(e.FocusedRowHandle) as FullSummarizeAdviceDto;

            SetMemoEdit(dto);
        }

        private void SetMemoEdit(FullSummarizeAdviceDto dto = null)
        {
            if(dto == null)
            {
                meClientAdvice.Text = string.Empty;
                meDepartmentAdvice.Text = string.Empty;
                meDietGuide.Text = string.Empty;
                meHealthcareAdvice.Text = string.Empty;
                meKnowledge.Text = string.Empty;
                meSportGuide.Text = string.Empty;
                meSummAdvice.Text = string.Empty;
            }
            else
            {  
               _Model = dto;
                meClientAdvice.Text = dto.ClientAdvice;
                meDepartmentAdvice.Text = dto.DepartmentAdvice;
                meDietGuide.Text = dto.DietGuide;
                meHealthcareAdvice.Text = dto.HealthcareAdvice;
                meKnowledge.Text = dto.Knowledge;
                meSportGuide.Text = dto.SportGuide;
                meSummAdvice.Text = dto.SummAdvice;

                textEdit2.EditValue = dto.Id;
                lueDepartment.EditValue = dto.DepartmentId;
                teUid.Text = dto.Uid;
                textEdit1.Text = dto.AdviceName;
                teHelpChar.Text = dto.HelpChar;
                teAdvicevalue.Text = dto.Advicevalue;
                ceSummState.Checked = dto.SummState == 1;
                ceCrisisSate.Checked = dto.CrisisSate == 1;
                ceHideOnGroupReport.Checked = dto.HideOnGroupReport == 1;
                ceDiagnosisSate.Checked = dto.DiagnosisSate == 1;
                teDiagnosisExpain.Text = dto.DiagnosisExpain;
                lueDiagnosisAType.EditValue = dto.DiagnosisAType;
                meSummAdvice.Text = dto.SummAdvice;
                meDepartmentAdvice.Text = dto.DepartmentAdvice;
                meClientAdvice.Text = dto.ClientAdvice;
                meDietGuide.Text = dto.DietGuide;
                meSportGuide.Text = dto.SportGuide;
                meKnowledge.Text = dto.Knowledge;
                meHealthcareAdvice.Text = dto.HealthcareAdvice;
                lueSexState.EditValue = dto.SexState;
                lueMarrySate.EditValue = dto.MarrySate;
                teMinAge.Text = dto.MinAge?.ToString();
                teMaxAge.Text = dto.MaxAge?.ToString();
            }
        }

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            //Reload();
            Reloadf();
        }

        #endregion
        // 助记码
        private void teAdviceName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teHelpChar.Text))
                return;
            var name = teAdviceName.Text.Trim();
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
        #region 处理
        /// <summary>
        /// 加载控件数据
        /// </summary>
        private void LoadControlData()
        {
            var dept = DefinedCacheHelper.GetDepartments();
            treeList1.DataSource = dept;
            lueDepartment.Properties.DataSource = dept;
            lueSexState.Properties.DataSource = SexHelper.GetSexModelsForItemInfo();
            lueMarrySate.Properties.DataSource = MarrySateHelper.GetMarrySateModelsForItemInfo();
            lueDiagnosisAType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.DiagnosisAType);
            searchLookUpCriticalTypeState.Properties.DataSource = CriticalTypeStateHelper.GetList();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Reload()
        {
           

            AutoLoading(() =>
            {
              
                var output = service.PageFulls(new PageInputDto<SearchSummarizeAdvice>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                
                    Input = new SearchSummarizeAdvice
                    {
                        QueryText = teAdviceName.Text.Trim()
                         
                    }
                });
              
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridControl.DataSource = output.Result;               
                gvSummarizeAdvice.BestFitColumns();
               
            });


        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void Reloadf()
        {


            var deptId = currdepar;


            AutoLoading(() =>
            {
                if (deptId != null && deptId !="" && Guid.TryParse(deptId, out Guid deptguid))
                {
                    var output = service.PageFulls(new PageInputDto<SearchSummarizeAdvice>
                    {
                        TotalPages = TotalPages,
                        CurentPage = CurrentPage,

                        Input = new SearchSummarizeAdvice
                        {
                            QueryText = teAdviceName.Text.Trim(),
                            DepartmentId = deptguid

                        }
                    });

                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator);
                    gridControl.DataSource = output.Result;
                    gvSummarizeAdvice.BestFitColumns();
                }
                else
                {
                    var output = service.PageFulls(new PageInputDto<SearchSummarizeAdvice>
                    {
                        TotalPages = TotalPages,
                        CurentPage = CurrentPage,

                        Input = new SearchSummarizeAdvice
                        {
                            QueryText = teAdviceName.Text.Trim()

                        }
                    });

                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator);
                    gridControl.DataSource = output.Result;
                    gvSummarizeAdvice.BestFitColumns();
                }
            });


        }
        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            SummarizeAdviceEdit frm = new SummarizeAdviceEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                gridControl.AddDtoListItem(frm._Model);
                gridControl.RefreshDataSource();
            }
        }
        public SuggestList(FullSummarizeAdviceDto model) : this()
        {
            _Model = model;
            Reload();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        //public void Edit(FullSummarizeAdviceDto dto)
        //{
        //    if (dto == null)
        //    {
        //        ShowMessageBoxWarning("请选择建议。");
        //        return;
        //    }
        //    var temp = ModelHelper.DeepCloneByJson(dto);
        //    SummarizeAdviceEdit frm = new SummarizeAdviceEdit(temp);
        //    frm.ShowDialog();
        //    if (frm.DialogResult == DialogResult.OK)
        //    {
        //        ModelHelper.CustomMapTo(frm._Model, dto);
        //        gridControl.RefreshDataSource();
        //    }
        //}
        public void Edits(FullSummarizeAdviceDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择建议。");
                return;
            }
            //var temp = ModelHelper.DeepCloneByJson(dto);
            //SuggestList frm = new SuggestList(temp);                     
            //ModelHelper.CustomMapTo(frm._Model, dto);
            //gridControl.RefreshDataSource();
            
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        public void Del(FullSummarizeAdviceDto dto)
        {
            if (dto == null)
            {
                ShowMessageBoxWarning("请选择要删除的建议。");
                return;
            }
            var question = XtraMessageBox.Show("是否删除？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }

            AutoLoading(() =>
            {
                service.Del(new EntityDto<Guid> { Id = dto.Id });
                gridControl.RemoveDtoListItem(dto);
            }, Variables.LoadingDelete);
        }
        #endregion

        private void sbExport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = $"导出建议_{DateTime.Now.ToString("yyyyMMdd")}";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            string FileName = saveFileDialog.FileName;

            var ds = gridControl.DataSource;
            bool res = false;
            AutoLoading(() =>
            {
                var output = service.QueryFulls(new SearchSummarizeAdvice
                {
                    QueryText = teAdviceName.Text.Trim()
                });
                gridControl.DataSource = output;
                res = true;
            });
            if (!res)
            {
                return;
            }

            try
            {
                var ps = new DevExpress.XtraPrinting.PrintingSystem();
                var link = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
                ps.Links.Add(link);
                link.Links.Add(ExcelHelper.CreatePrintableLink(gridControl));
                link.Landscape = true;//横向
                                      //判断是否有标题，有则设置
                                      //link.CreateDocument(); //建立文档
                int count = 1;
                //在重复名称后加（序号）
                while (System.IO.File.Exists(FileName))
                {
                    if (FileName.Contains(")."))
                    {
                        int start = FileName.LastIndexOf("(");
                        int end = FileName.LastIndexOf(").") - FileName.LastIndexOf("(") + 2;
                        FileName = FileName.Replace(FileName.Substring(start, end), string.Format("({0}).", count));
                    }
                    else
                    {
                        FileName = FileName.Replace(".", string.Format("({0}).", count));
                    }
                    count++;
                }

                gridColumnSummAdvice.Visible = true;
                gridColumnDepartmentAdvice.Visible = true;
                gridColumnClientAdvice.Visible = true;
                gridColumnDietGuide.Visible = true;
                gridColumnSportGuide.Visible = true;
                gridColumnKnowledge.Visible = true;
                gridColumnHealthcareAdvice.Visible = true;

                if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
                {
                    var options = new DevExpress.XtraPrinting.XlsxExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    link.ExportToXlsx(FileName, options);
                }
                else
                {
                    var options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    link.ExportToXls(FileName, options);
                }
            }
            finally
            {
                gridColumnSummAdvice.Visible = false;
                gridColumnDepartmentAdvice.Visible = false;
                gridColumnClientAdvice.Visible = false;
                gridColumnDietGuide.Visible = false;
                gridColumnSportGuide.Visible = false;
                gridColumnKnowledge.Visible = false;
                gridColumnHealthcareAdvice.Visible = false;
                gridControl.DataSource = ds;
                gvSummarizeAdvice.BestFitColumns();
            }

            var question = DevExpress.XtraEditors.XtraMessageBox.Show("保存成功，是否打开文件？", "提示",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question);
            if (question == System.Windows.Forms.DialogResult.Yes)
                System.Diagnostics.Process.Start(FileName);//打开指定路径下的文件
            
            //var ds = gridControl.DataSource;
            //bool res = false;
            //AutoLoading(() =>
            //{
            //    var output = service.QueryFulls(new SearchSummarizeAdvice
            //    {
            //        QueryText = teAdviceName.Text.Trim()
            //    });
            //    gridControl.DataSource = output;
            //    res = true;
            //});
            //if (res)
            //{
            //    gridColumnSummAdvice.Visible = true;
            //    gridColumnDepartmentAdvice.Visible = true;
            //    gridColumnClientAdvice.Visible = true;
            //    gridColumnDietGuide.Visible = true;
            //    gridColumnSportGuide.Visible = true;
            //    gridColumnKnowledge.Visible = true;
            //    gridColumnHealthcareAdvice.Visible = true;
            //    ExcelHelper.ExportToExcel($"导出建议_{DateTime.Now.ToString("yyyyMMddHHmmss")}", gridControl);
            //    gridColumnSummAdvice.Visible = false;
            //    gridColumnDepartmentAdvice.Visible = false;
            //    gridColumnClientAdvice.Visible = false;
            //    gridColumnDietGuide.Visible = false;
            //    gridColumnSportGuide.Visible = false;
            //    gridColumnKnowledge.Visible = false;
            //    gridColumnHealthcareAdvice.Visible = false;
            //}
            //gridControl.DataSource = ds;
            //gvSummarizeAdvice.BestFitColumns();
        }
        //导出模板
        private void sbTemplate_Click(object sender, EventArgs e)
        {
            var strList = new List<string>() {
                "科室名称",
                "建议名称*",
                "建议依据",
                "建议内容*",
            };
            GridControlHelper.ExportByGridControl(strList, "总检建议模板");
        }
        //导入数据
        private void sbImput_Click(object sender, EventArgs e)
        {
            //var row= gvSummarizeAdvice.GetFocusedRow();
            //var row2 = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
            //if(row2==null)
            //{
            //    ShowMessageBoxWarning("请先选择科室。");
            //    return;
            //}
            openFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var datas= ExcelToDtoList(openFileDialog.FileName, "Sheet", true,Guid.Empty);
                if (datas == null)
                {
                    ShowMessageBoxInformation("请检查文件中必填项及建议编码是否重复。");
                    return;
                }
                else
                {
                    // var str = "您已选择【" + row2.Department.Name + "】，是否导入？";
                    var str = "是否导入？";
                    DialogResult dr = XtraMessageBox.Show(str, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        try
                        {
                            DateTime dtstar = System.DateTime.Now;

                            labelTS.Text = "共：" + datas.Count + "条数据";                          
                            labelTS.Refresh();
                            DataTable nchek = new DataTable();
                            nchek.Columns.Add("建议编码*");
                            nchek.Columns.Add("建议名称*");
                            nchek.Columns.Add("建议依据*");
                            nchek.Columns.Add("建议内容*");
                            nchek.Columns.Add("失败原因");
                            int i = 1;
                            AutoLoading(() =>
                            {
                                var service = new SummarizeAdviceAppService();
                             
                                foreach (var data in datas)
                                {
                                    if (string.IsNullOrEmpty(data.AdviceName))
                                    {
                                        labelTS.Text = i + "/" + datas.Count;
                                        labelTS.Refresh();
                                        i = i + 1;
                                        continue;
                                    }
                                    if (!data.DiagnosisSate.HasValue)
                                    { data.DiagnosisSate = 1; }
                                    if (!data.SummState.HasValue)
                                    { data.SummState = 1; }
                                    if (!data.CrisisSate.HasValue)
                                    { data.CrisisSate = 2; }
                                    if (!data.SexState.HasValue)
                                    { data.SexState = 9; }
                                    if (!data.MinAge.HasValue)
                                    { data.MinAge = 0; }
                                    if (!data.MaxAge.HasValue)
                                    { data.MaxAge = 0; }
                                    if (string.IsNullOrEmpty(data.Uid))
                                    {
                                        data.Uid=iIDNumberAppService.CreateAdviceBM();
                                    }
                                   
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(data.AdviceName) && string.IsNullOrEmpty(data.HelpChar))
                                        {
                                            var helpchar = _commonAppService.GetHansBrief(new ChineseDto { Hans = data.AdviceName });
                                            data.HelpChar = helpchar.Brief;
                                        }
                                    }
                                    catch (UserFriendlyException exception)
                                    {
                                        Console.WriteLine(exception);
                                    }
                                    //ninput = data;
                                    try
                                    {
                                        SummarizeAdviceInput input = new SummarizeAdviceInput()
                                        {
                                            SummarizeAdvice = data
                                            //SummarizeAdvice = ModelHelper.CustomMapTo2<FullSummarizeAdviceDto, CreateOrUpdateSummarizeAdviceDto>(data),
                                        };

                                        service.Add(input);
                                        labelTS.Text = i + "/" + datas.Count ;
                                        labelTS.Refresh();
                                        i = i + 1;
                                    }
                                    catch (UserFriendlyException ex)
                                    {

                                        DataRow rowdr = nchek.NewRow();
                                        rowdr["建议编码*"] = data.Uid;
                                        rowdr["建议名称*"] = data.AdviceName;
                                        rowdr["建议依据*"] = data.Advicevalue;
                                        rowdr["建议内容*"] = data.SummAdvice;
                                        rowdr["失败原因"] = ex.Description;
                                        nchek.Rows.Add(rowdr);

                                    }
                    }
                            });
                            DateTime dtsend = System.DateTime.Now;
                           
                            TimeSpan minuteSpan = new TimeSpan(dtsend.Ticks- dtstar.Ticks );
                            labelTS.Text = "共导入：" + i + "条数据，用时：" + minuteSpan.TotalMinutes.ToString("0.00") +"分钟";
                            labelTS.Refresh();
                            if (nchek.Rows.Count > 0)
                            {
                                var saveFileDialog = new SaveFileDialog();
                                saveFileDialog.FileName = "导入失败建议";
                                saveFileDialog.DefaultExt = "xls";
                                saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
                                if (saveFileDialog.ShowDialog() != DialogResult.OK
                                ) //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
                                    return;
                                var FileName = saveFileDialog.FileName;
                                ExcelHelper helper = new ExcelHelper(saveFileDialog.FileName);
                                helper.DataTableToExcel(nchek, "导入失败建议", true);
                                MessageBox.Show("共有：" + nchek.Rows.Count + "条数据导入失败！");
                            }
                        }
                        catch(UserFriendlyException ex)
                        {
                            ShowMessageBox(ex);
                        }
                        Reload();
                    }
                }
            }
        }
        //导入excel转换为总检建议dto
        private List<CreateOrUpdateSummarizeAdviceDto> ExcelToDtoList(string _fileName, string sheetName, bool isFirstRowColumn,Guid? departmentId)
        {
            MessageBox.Show("导入的数据会自动过滤掉建议名称为空或不能准确匹配科室名称的建议！");
            var result = new List<CreateOrUpdateSummarizeAdviceDto>();
            FileStream _fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            //if (_fileName.IndexOf(".xlsx", StringComparison.Ordinal) > 0)
            //{
                // 2007版本
                _workbook = new XSSFWorkbook(_fs);
            //}
            //else if (_fileName.IndexOf(".xls", StringComparison.Ordinal) > 0)
            //{
            //    // 2003版本
            //    _workbook = new HSSFWorkbook(_fs);
            //}

            ISheet sheet;
            if (sheetName != null)
            {
                // 如果没有找到指定的 SheetName 对应的 Sheet，则尝试获取第一个 Sheet
                sheet = _workbook.GetSheet(sheetName) ?? _workbook.GetSheetAt(0);
            }
            else
            {
                sheet = _workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                var firstRow = sheet.GetRow(0);
                // 一行最后一个 Cell 的编号，即总的列数
                int cellCount = firstRow.LastCellNum;
                //int startRow;
                //if (isFirstRowColumn)
                //{
                //    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                //    {
                //        var cell = firstRow.GetCell(i);
                //        var cellValue = cell?.StringCellValue;

                //        if (cellValue != null)
                //        {
                //            var column = new DataColumn(cellValue);
                //            data.Columns.Add(column);
                //        }
                //    }

                //    startRow = sheet.FirstRowNum + 1;
                //}
                //else
                //{
                //    startRow = sheet.FirstRowNum;
                //}
                // 最后一列的标号
                var rowCount = sheet.LastRowNum;
                var  _commonAppService = new CommonAppService();
                string ts = "";
                for (var i = 1; i <= rowCount; ++i)
                {
                    var row = sheet.GetRow(i);
                    // 没有数据的行默认是 NULL
                    if (row == null)
                        continue;
                    var rs = new CreateOrUpdateSummarizeAdviceDto();
                    if (departmentId.HasValue)
                    {
                        rs.DepartmentId = departmentId.Value;
                    }
                    
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {

                        if (sheet.GetRow(0).GetCell(j).ToString() == "建议编码*")
                        {
                            rs.Uid = row.GetCell(j).ToString();
                            //if (string.IsNullOrWhiteSpace(rs.Uid))
                            //    return null;
                        }
                        //var redata= result.Find(o => o.Uid == rs.Uid);
                        //if (redata != null)
                        //    return null;
                        if (sheet.GetRow(0).GetCell(j).ToString() == "建议名称*")
                        {
                            rs.AdviceName= row.GetCell(j)?.ToString();
                            if (string.IsNullOrEmpty(rs.AdviceName))
                                continue;
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "建议依据")
                        {
                            rs.Advicevalue = row.GetCell(j)?.ToString();
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "建议内容*")
                        {
                            rs.SummAdvice = row.GetCell(j)?.ToString();
                            if (string.IsNullOrEmpty(rs.SummAdvice))
                                rs.SummAdvice ="无";
                        }
                        if (sheet.GetRow(0).GetCell(j).ToString() == "科室名称")
                        {

                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()))
                            {
                                var departId= DefinedCacheHelper.GetDepartments().FirstOrDefault(p=>p.Name== row.GetCell(j).ToString())?.Id;
                                if (departId.HasValue)
                                {
                                    rs.DepartmentId = departId.Value;
                                }
                                else
                                {
                                    //MessageBox.Show(row.GetCell(j).ToString() +"没有匹配的科室，请检查模板！");
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }                     
                    }

                    result.Add(rs);
                }
               
            }
            return result;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            meClientAdvice.Text = string.Empty;
            meDepartmentAdvice.Text = string.Empty;
            meDietGuide.Text = string.Empty;
            meHealthcareAdvice.Text = string.Empty;
            meKnowledge.Text = string.Empty;
            meSportGuide.Text = string.Empty;
            meSummAdvice.Text = string.Empty;

            lueDepartment.EditValue = null;         
            textEdit1.Text = string.Empty;
            teHelpChar.Text = string.Empty;
            teAdvicevalue.Text = string.Empty;
            ceSummState.Checked =false;
            ceCrisisSate.Checked = false;
            ceHideOnGroupReport.Checked =false;
            ceDiagnosisSate.Checked = false;
            teDiagnosisExpain.Text = string.Empty;
            lueDiagnosisAType.EditValue = string.Empty;
            meSummAdvice.Text = string.Empty;
            meDepartmentAdvice.Text = string.Empty;
            meClientAdvice.Text = string.Empty;
            meDietGuide.Text = string.Empty;
            meSportGuide.Text = string.Empty;
            meKnowledge.Text = string.Empty;
            meHealthcareAdvice.Text = string.Empty;
            lueSexState.EditValue = string.Empty;
            lueMarrySate.EditValue = string.Empty;
            teMinAge.Text = "0";
            teMaxAge.Text = "120";

            teUid.Text = iIDNumberAppService.CreateAdviceBM();
            _Model = new FullSummarizeAdviceDto();


        }
        private bool Ok()
        {
            dxErrorProvider.ClearErrors();
            // 编码
            _Model.Uid = teUid.Text.Trim();
            if (string.IsNullOrEmpty(_Model.Uid))
            {
                //dxErrorProvider.SetError(teUid, string.Format(Variables.MandatoryTips, "编码"));
                //teUid.Focus();
                //return false;
                _Model.Uid = iIDNumberAppService.CreateAdviceBM();

            }
            // 科室
            _Model.DepartmentId = lueDepartment.EditValue == null ? Guid.Empty : (Guid)lueDepartment.EditValue;
            if (_Model.DepartmentId != Guid.Empty)
            {
                _Model.DepartmentId = (Guid)lueDepartment.EditValue;
                var deptdto = (TbmDepartmentDto)lueDepartment.GetSelectedDataRow();

                _Model.Department = ModelHelper.CustomMapTo2<TbmDepartmentDto, DepartmentSimpleDto>(deptdto);
            }
            else
            {
                dxErrorProvider.SetError(lueDepartment, string.Format(Variables.MandatoryTips, "科室"));
                lueDepartment.Focus();
                return false;
            }
            // 建议名称
            _Model.AdviceName = textEdit1.Text.Trim();
            if (string.IsNullOrEmpty(_Model.AdviceName))
            {
                dxErrorProvider.SetError(textEdit1, string.Format(Variables.MandatoryTips, "建议名称"));
                textEdit1.Focus();
                return false;
            }

            // 总检建议
            _Model.SummAdvice = meSummAdvice.Text;
            if (string.IsNullOrWhiteSpace(_Model.SummAdvice))
            {
                dxErrorProvider.SetError(meSummAdvice, string.Format(Variables.MandatoryTips, "建议内容"));
                xtraTabPage1.Select(); // 激活tab页
                meSummAdvice.Focus();
                return false;
            }

            // 助记码
            _Model.HelpChar = teHelpChar.Text.Trim();
            // 建议依据
            if (string.IsNullOrEmpty(teAdvicevalue.Text.Trim()))
            {
                _Model.Advicevalue = _Model.AdviceName;
            }
            else
            {
                _Model.Advicevalue = teAdvicevalue.Text.Trim();
            }
            // 阳性状态 1阳性2正常
            _Model.SummState = ceSummState.Checked ? 1 : 2;
            // 危急值状态 1危急值2正常
            _Model.CrisisSate = ceCrisisSate.Checked ? 1 : 2;
            // 团报隐藏 1隐藏2显示
            _Model.HideOnGroupReport = ceHideOnGroupReport.Checked ? 1 : 2;

            // 疾病状态 1疾病2正常
            _Model.DiagnosisSate = ceDiagnosisSate.Checked ? 1 : 2;
            // 疾病介绍
            _Model.DiagnosisExpain = teDiagnosisExpain.Text;
            // 疾病类别 字典
            _Model.DiagnosisAType = (int?)lueDiagnosisAType.EditValue;

            // 专科建议
            _Model.DepartmentAdvice = meDepartmentAdvice.Text;
            // 团体建议
            _Model.ClientAdvice = meClientAdvice.Text;
            // 饮食指导
            _Model.DietGuide = meDietGuide.Text;
            // 运动指导
            _Model.SportGuide = meSportGuide.Text;
            // 健康指导
            _Model.Knowledge = meKnowledge.Text;
            // 健康建议
            _Model.HealthcareAdvice = meHealthcareAdvice.Text;
            // 适用性别
            _Model.SexState = (int?)lueSexState.EditValue;
            // 适用婚别
            _Model.MarrySate = (int?)lueMarrySate.EditValue;
            // 最小年龄
            _Model.MinAge = int.TryParse(teMinAge.Text.Trim(), out int minAge) ? (int?)minAge : null;
            // 最大年龄
            _Model.MaxAge = int.TryParse(teMaxAge.Text.Trim(), out int maxAge) ? (int?)maxAge : null;

            bool res = false;
            AutoLoading(() =>
            {
                SummarizeAdviceInput input = new SummarizeAdviceInput()
                {
                    SummarizeAdvice = ModelHelper.CustomMapTo2<FullSummarizeAdviceDto, CreateOrUpdateSummarizeAdviceDto>(_Model),
                };
                FullSummarizeAdviceDto dto = null;
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                    gridControl.AddDtoListItem(dto);
                    gridControl.RefreshDataSource();
                   

                }
                else
                {
                    dto = service.Edit(input);
                    var dtoOld = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
                    ModelHelper.CustomMapTo(dto, dtoOld);
                    gridControl.RefreshDataSource();

                }
                dto.Department = _Model.Department;
                _Model = dto;
                res = true;
            });
            return res;
        }
        private bool Oks()
        {
            dxErrorProvider.ClearErrors();
            
            // 编码
            _Model.Uid= teUid.EditValue.ToString();
            if (string.IsNullOrEmpty(_Model.Uid))
            {
               //dxErrorProvider.SetError(teUid, string.Format(Variables.MandatoryTips, "编码"));
                //teUid.Focus();
                //return false;
                _Model.Uid = iIDNumberAppService.CreateAdviceBM();

            }
            // 科室
            _Model.DepartmentId = lueDepartment.EditValue == null ? Guid.Empty : (Guid)lueDepartment.EditValue;
            
            if (_Model.DepartmentId != Guid.Empty)
            {
                _Model.DepartmentId = (Guid)lueDepartment.EditValue;
                var deptdto = (TbmDepartmentDto)lueDepartment.GetSelectedDataRow();

                _Model.Department = ModelHelper.CustomMapTo2<TbmDepartmentDto, DepartmentSimpleDto>(deptdto);
            }
            else
            {
                dxErrorProvider.SetError(lueDepartment, string.Format(Variables.MandatoryTips, "科室"));
                lueDepartment.Focus();
                return false;
            }
            // 建议名称
            _Model.AdviceName = textEdit1.Text.Trim();
            if (string.IsNullOrEmpty(_Model.AdviceName))
            {
                dxErrorProvider.SetError(textEdit1, string.Format(Variables.MandatoryTips, "建议名称"));
                textEdit1.Focus();
                return false;
            }

            // 总检建议
            _Model.SummAdvice = meSummAdvice.Text;
            if (string.IsNullOrWhiteSpace(_Model.SummAdvice))
            {
                dxErrorProvider.SetError(meSummAdvice, string.Format(Variables.MandatoryTips, "建议内容"));
                xtraTabPage1.Select(); // 激活tab页
                meSummAdvice.Focus();
                return false;
            }

            // 助记码
            _Model.HelpChar = teHelpChar.Text.Trim();
            // 建议依据
            _Model.Advicevalue = teAdvicevalue.Text.Trim();
            // 阳性状态 1阳性2正常
            _Model.SummState = ceSummState.Checked ? 1 : 2;
            // 危急值状态 1危急值2正常
            _Model.CrisisSate = ceCrisisSate.Checked ? 1 : 2;
            // 团报隐藏 1隐藏2显示
            _Model.HideOnGroupReport = ceHideOnGroupReport.Checked ? 1 : 2;

            // 疾病状态 1疾病2正常
            _Model.DiagnosisSate = ceDiagnosisSate.Checked ? 1 : 2;
            // 疾病介绍
            _Model.DiagnosisExpain = teDiagnosisExpain.Text;
            // 疾病类别 字典
            _Model.DiagnosisAType = (int?)lueDiagnosisAType.EditValue;

            // 专科建议
            _Model.DepartmentAdvice = meDepartmentAdvice.Text;
            // 团体建议
            _Model.ClientAdvice = meClientAdvice.Text;
            // 饮食指导
            _Model.DietGuide = meDietGuide.Text;
            // 运动指导
            _Model.SportGuide = meSportGuide.Text;
            // 健康指导
            _Model.Knowledge = meKnowledge.Text;
            // 健康建议
            _Model.HealthcareAdvice = meHealthcareAdvice.Text;
            // 适用性别
            _Model.SexState = (int?)lueSexState.EditValue;
            // 适用婚别
            _Model.MarrySate = (int?)lueMarrySate.EditValue;
            // 最小年龄
            _Model.MinAge = int.TryParse(teMinAge.Text.Trim(), out int minAge) ? (int?)minAge : null;
            // 最大年龄
            _Model.MaxAge = int.TryParse(teMaxAge.Text.Trim(), out int maxAge) ? (int?)maxAge : null;
            _Model.Id = textEdit2.EditValue == null ? Guid.Empty : (Guid)textEdit2.EditValue;
            bool res = false;
            FullSummarizeAdviceDto dto = null;
            AutoLoading(() =>
            {
                SummarizeAdviceInput input = new SummarizeAdviceInput()
                {
                    SummarizeAdvice = ModelHelper.CustomMapTo2<FullSummarizeAdviceDto, CreateOrUpdateSummarizeAdviceDto>(_Model),
                };
               
                if (_Model.Id == Guid.Empty)
                {
                    dto = service.Add(input);
                }
                else
                {
                    dto = service.Edit(input);
                }
                dto.Department = _Model.Department;
                _Model = dto;
                res = true;
            });
            var dtoyl = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
            ModelHelper.CustomMapTo(dto, dtoyl);
            gridControl.RefreshDataSource();

        
            //gridControl.RefreshDataSource();
            return res;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           // var dto = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
            //_Model = dto;
            Ok();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var dto = gridControl.GetFocusedRowDto<FullSummarizeAdviceDto>();
            Edits(dto);
            Oks();            
        }
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var deptId = treeList1.FocusedNode.GetValue("Id").ToString();
            currdepar = deptId;
            var output = service.PageFulls(new PageInputDto<SearchSummarizeAdvice>
            {
                TotalPages = TotalPages,
                CurentPage = CurrentPage,
                Input= new SearchSummarizeAdvice
                {
                    DepartmentId=new Guid(deptId),                  

                }
                
            });

            TotalPages = output.TotalPages;
            CurrentPage = output.CurrentPage;
            InitialNavigator(dataNavigator);           
            gridControl.DataSource = output.Result;
            gvSummarizeAdvice.BestFitColumns();
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(teHelpChar.Text))
                return;
            var name = textEdit1.Text.Trim();
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

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
           // _Model.Uid = iIDNumberAppService.CreateAdviceBM();
            teUid.EditValue = iIDNumberAppService.CreateAdviceBM();
        }
    }

}
