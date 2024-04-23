using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterItemPicture.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegisterSummarize.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterDepartmentSummaryAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Runtime.InteropServices;
using System.IO;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using static Sw.Hospital.HealthExaminationSystem.UserSettings.OccDayStatic.OccDayStaticList;
using Sw.His.Common.Functional.Unit.CustomTools;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.CommonFormat;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.DoctorStation;
using NeusoftInterface;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto.CustomerRegisterItemAppServiceDto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto.CustomerRegisterAppServiceDot;
using DevExpress.XtraTreeList;
using DevExpress.Utils.Drawing;
using DevExpress.XtraTreeList.Nodes;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.UserSettings.Suggest;
using DevExpress.XtraRichEdit.API.Native;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.UserSettings.BasicDictionary;
using System.Text.RegularExpressions;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class FrmInspectionTotalNew86 : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private readonly IPrintPreviewAppService _printPreviewAppService;
        // 用户个性配置
        private readonly IPersonnelIndividuationConfigAppService _PersonnelIndividuationConfigAppService;
        private List<CustomerRegisterItemPictureDto> _currentItemPictures;

        private CustomerRegisterSummarizeDto customerRegisterSummarizeDto;
        public OutCusListDto _tjlCustomerRegDto;
        public string TS = "";
        public string Wjz = "";
        public readonly IHistoryComparisonAppService _HistoryComparisonAppService;
        private Dictionary<string, CustomColumnValue> CustomColumns { get; set; }
        private List<GridColumn> OldColumns = new List<GridColumn>();
        private List<CustomerRegInspectGroupDto> _CustomerRegInspectGroupDto = new List<CustomerRegInspectGroupDto>();
        private readonly IDoctorStationAppService _doctorStationAppService;
        // 建议字典
        private readonly ISummarizeAdviceAppService _summarizeAdviceAppService;
        private CustomerRegister1Dto NowCusRegInf = new CustomerRegister1Dto();

        /// <summary>
        /// 查询建议字典--初始化读取，后续生成总检使用
        /// </summary>
        private List<SummarizeAdviceDto> _summarizeAdviceFull = new List<SummarizeAdviceDto>();
        private string selectrxt = "";
        List<CustomerRegisterItemDto> currCusRegGroupItems = new List<CustomerRegisterItemDto>();
        List<CustomerRegisterDepartmentSummaryDto> currCusDeptSum = new List<CustomerRegisterDepartmentSummaryDto>();

        public FrmInspectionTotalNew86()
        {
            _summarizeAdviceAppService = new SummarizeAdviceAppService();
            customerRegisterSummarizeDto = new CustomerRegisterSummarizeDto();
            _PersonnelIndividuationConfigAppService = new PersonnelIndividuationConfigAppService();
            _printPreviewAppService = new PrintPreviewAppService();
            _inspectionTotalService = new InspectionTotalAppService();
            _commonAppService = new CommonAppService();
            _HistoryComparisonAppService = new HistoryComparisonAppService();

            CustomColumns = new Dictionary<string, CustomColumnValue>();
            _doctorStationAppService = new DoctorStationAppService();
            InitializeComponent();
        }

        private void FrmInspectionTotalNew_Load(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                gridViewCusReg.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
                gridViewCusReg.Columns[conSummSate.FieldName].DisplayFormat.Format =
                    new CustomFormatter(SummSateHelper.SummSateFormatter);

                //人员列表显示
                var YbYS = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusList, 2)?.Remarks;
                if (!string.IsNullOrEmpty(YbYS) && YbYS == "Y")
                {

                    dockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                }
                else
                { dockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide; }
                //初始化窗体
                InitForm();
                //加载默认查询条件
                string fp = System.Windows.Forms.Application.StartupPath + "\\NInspection.json";
                if (File.Exists(fp))  // 判断是否已有相同文件 
                {
                    var Search = JsonConvert.DeserializeObject<List<Search>>(File.ReadAllText(fp));
                    foreach (var tj in Search)
                    {
                        if (tj.Name == "CheckType")
                        {
                            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
                            if (Examination.Any(p => p.Value == int.Parse(tj.Text)))
                            {
                                comCheckType.EditValue = int.Parse(tj.Text);
                            }
                        }
                        else if (tj.Name == "CheckState")
                        {
                            comState.EditValue = int.Parse(tj.Text);
                        }
                        else if (tj.Name == "SumState")
                        {
                            comSumSate.EditValue = int.Parse(tj.Text);
                        }
                        else if (tj.Name == "Day")
                        {
                            textDay.Text = tj.Text;
                        }

                    }
                }
            }, Variables.LoadingForForm);
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {

            var dataDto = _commonAppService.GetDateTimeNow();
            //天数
            textDay.Text = "7";
            //体检日期
            dateLoginStart.DateTime = dataDto.Now.Date.AddDays(-1);
            dateLoginStart.DateTime = dataDto.Now.Date;
            dateLoginEnd.DateTime = dataDto.Now.Date.AddDays(-1);
            dateLoginEnd.DateTime = dataDto.Now.Date;

            //总检日期

            //         dateSumStar.DateTime = dataDto.Now.Date.AddDays(-1);
            //         dateSumStar.DateTime = dataDto.Now.Date;
            //         dateSumEnd.DateTime = dataDto.Now.Date.AddDays(-1);
            //dateSumEnd.DateTime = dataDto.Now.Date;

            searchLookUpClientReg.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            //SummSate

            comSumSate.Properties.DataSource = SummSateHelper.GetSelectList();
            comSumSate.EditValue = (int)SummSate.NotAlwaysCheck;

            // 加载体检类别数据
            var Examination = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            BasicDictionaryDto all = new BasicDictionaryDto();

            // 加载体检状态数据

            comState.Properties.DataSource = PhysicalEStateHelper.YYGetList();
            comState.EditValue = (int)PhysicalEState.Complete;
            all.Value = -1;
            all.Text = "全部";
            Examination.Add(all);

            comCheckType.Properties.DataSource = Examination;
            if (Examination.Count > 0)
            {
                comCheckType.EditValue = Examination[0].Value;
            }
            //审核医生

            //绑定诊断搜索下拉框
            var userDto = DefinedCacheHelper.GetComboUsers();

            txtsearchDoctor.Properties.DataSource = userDto;
            txtsearchDoctor.Properties.ValueMember = "Id";
            txtsearchDoctor.Properties.DisplayMember = "Name";
            repositoryItemLookUpEdit8.DataSource = userDto.ToList();
            repositoryItemLookUpEdit9.DataSource = userDto.ToList();

            //repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetDepartments();
            //repositoryItemLookUpEdit2.DataSource = DefinedCacheHelper.GetComboUsers();
            repositoryItemLookUpEdit3.DataSource = DefinedCacheHelper.GetDepartments();
            repositoryItemLookUpEdit4.DataSource = DefinedCacheHelper.GetItemGroups();
            repositoryItemLookUpEdit5.DataSource = DefinedCacheHelper.GetItemInfos();
            repositoryItemLookUpEdit6.DataSource = SexHelper.GetSexForPerson();
            gridViewCusReg.Columns["PrintSate"].DisplayFormat.FormatType = FormatType.Custom;
            gridViewCusReg.Columns["PrintSate"].DisplayFormat.Format =
                new CustomFormatter(PrintSateHelper.PrintSateFormatter);



            //绑定诊断搜索下拉框           
            //customGridZhenDuan2.Properties.DataSource = DefinedCacheHelper.GetSummarizeAdvices();
            //customGridZhenDuan2.EditValue = "Id";
            //ProjectIStateH
            repositoryItemLookUpEdit7.DataSource = ProjectIStateHelper.GetProjectIStateModels();

            #region 绑定历史数据检索条件
            searchLookUpDepartMent.Properties.DataSource = DefinedCacheHelper.GetDepartments();
            searchLookUpGroup.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            searchLookUpItem.Properties.DataSource = DefinedCacheHelper.GetItemInfos();


            #endregion
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
            // 参数控制显示上传列
             

        }

        private void butSearch_Click(object sender, EventArgs e)
        {


            LoadData();
        }
        public void LoadData(string cusRegBM = "")
        {
            AutoLoading(() =>
            {
                gridCusReg.DataSource = null;

                // gridView1.FocusedRowHandle = -1;
                var input = new InSearchCusDto();
                if (cusRegBM != "")
                {
                    input.CusNameBM = cusRegBM;
                    input.Code = cusRegBM;
                }
                else
                {
                    if (!string.IsNullOrEmpty(textName.Text.Trim()))
                    {
                        input.CusNameBM = textName.Text.Trim();
                    }

                    //if (textName.Text.Trim() == string.Empty)
                    //{

                    if (checkEditIsData.Checked)
                    {
                        if (comDataType.Text.Contains("体检日期"))
                        { input.DateType = 1; }
                        else if (comDataType.Text.Contains("总检日期"))
                        { input.DateType = 2; }
                        else
                        { input.DateType = 0; }

                        if (dateLoginStart.EditValue != null)
                            input.LoginStar = dateLoginStart.DateTime;
                        if (dateLoginEnd.EditValue != null)
                            input.LoginEnd = dateLoginEnd.DateTime.AddDays(1);
                        if (input.LoginStar > input.LoginEnd)
                        {
                            ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                            return;
                        }
                    }
                    //else if (checkEdit2.Checked)
                    //{
                    //    if (dateSumStar.EditValue != null)
                    //        input.SumStar = dateSumStar.DateTime;
                    //    if (dateSumEnd.EditValue != null)
                    //        input.SumEnd = dateSumEnd.DateTime.AddDays(1);
                    //    if (input.SumStar > input.SumEnd)
                    //    {
                    //        ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                    //        return;
                    //    }
                    //}
                    else
                    {

                        if (textDay.Text.Trim() != string.Empty)
                        {
                            //int dayNum = Convert.ToInt32(textEditDayNum.Text.Trim());
                            //input.BeginDate = DateTime.Now.AddDays(-dayNum).Date;
                            //input.EndDate = DateTime.Now.Date;

                            int tian = int.Parse(textDay.Text.Trim());
                            tian--;
                            input.LoginStar = DateTime.Now.Date.AddDays(-tian);
                            input.LoginEnd = DateTime.Now.Date.AddDays(1);
                        }
                    }
                    var examinationCategory = comCheckType.EditValue as int?;
                    if (examinationCategory.HasValue && examinationCategory != -1)
                    {
                        input.CheckType = examinationCategory;
                    }



                    if (comSumSate.EditValue != null && comSumSate.Text != "全部")
                    {
                        input.SumSate = (int)comSumSate.EditValue;
                    }

                    if (comState.EditValue != null && comState.Text != "全部")
                    {
                        input.Sate = (int)comState.EditValue;
                    }
                    //}

                    if (searchLookUpClientReg.EditValue != null && searchLookUpClientReg.EditValue != "")
                    {
                        input.ClientRegId = (Guid)searchLookUpClientReg.EditValue;
                    }
                    if (radioIsOK.EditValue != null && radioIsOK.EditValue?.ToString() != "全部")
                    {
                        input.Qualified = radioIsOK.EditValue?.ToString();
                    }
                    if (txtsearchDoctor.EditValue != null && !string.IsNullOrWhiteSpace(txtsearchDoctor.EditValue.ToString()))
                    {
                        long[] DocIdArr = new long[1];
                        DocIdArr[0] = (long)txtsearchDoctor.EditValue;
                        input.arrEmployeeName_Id = DocIdArr;
                        if (comboEmp.Text == "总检医生")
                        { input.EmployeeNameType = 1; }
                    }
                }
                var output = _inspectionTotalService.GetOutCus(input).ToList();
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);
                labelControl1.Text = "总人数：" + sum + "未总检:" + not + "已总检：" + (sum - not);
                gridCusReg.DataSource = output;


                //if (output.Count > 0)
                //{
                //    gridView1.FocusedRowHandle = 0;
                //}
                if (cusRegBM != "")
                {
                    loadfistdata();
                }
            });
        }
        /// <summary>
        /// 绑定tree数据
        /// </summary>
        public void BinDingDataTreeS(List<CustomerRegisterItemDto>  currcusItems,Guid DeparmentId)
        {
           // isOk = false;
            //获取科室
            var dictDepartmentBM = new Dictionary<Guid, string>();
            foreach (var item in currcusItems.OrderBy(o => o.DepartmentBM.OrderNum))
            {
                if (!dictDepartmentBM.Keys.Contains(item.DepartmentId) && item.DepartmentBM.Name != "系统科室")
                {
                    dictDepartmentBM.Add(item.DepartmentId, item.DepartmentBM.Name);
                }
            }
            tvJianChaXiangMu.Nodes.Clear();
            //获取有权限的第一个科室
            var firstDeparmentName = string.Empty;
            //获取有权限的第一个科室只进一次
            bool IsFirst = true;          
            //绑定树
            for (var i = 0; i < dictDepartmentBM.Count; i++)
            {
                //过滤科室               
                var nodeDepartment = new TreeNode();
                nodeDepartment.Tag = dictDepartmentBM.ElementAt(i).Key;//+","+ dictDepartmentBM.ElementAt(i).Value;
                nodeDepartment.Text = dictDepartmentBM.ElementAt(i).Value;
               
                    nodeDepartment.ExpandAll();
                if (IsFirst)
                {
                    firstDeparmentName = dictDepartmentBM.ElementAt(i).Value;
                    IsFirst = false;
                }
              
                nodeDepartment.Name = dictDepartmentBM.ElementAt(i).Value;
                var itemgroups = currcusItems.Select(p => new
                {
                    ItemgGroupID = p.ItemGroupBMId,
                    ItemGroupName = p.ItemGroupBM.ItemGroupName,
                    CheckState = p.CustomerItemGroupBM.CheckState,
                    DepartmentId=p.DepartmentId,
                    GroupOrder = p.ItemGroupBM.OrderNum
                }).Where(o => o.DepartmentId == dictDepartmentBM.ElementAt(i).Key)
                .Distinct().OrderBy(o => o.GroupOrder).ToList();
                foreach (var ItemGroup in itemgroups)
                {
                    var nodeGroup = new TreeNode();
                    nodeGroup.Tag = ItemGroup.ItemgGroupID;
                    nodeGroup.Text = ItemGroup.ItemGroupName + "(" +
                                     ProjectIStateHelper.ProjectIStateFormatter(ItemGroup.CheckState) + ")";
                                        
                    if (currcusItems.Where(p=>p.ItemGroupBMId== ItemGroup.ItemgGroupID).
                        Where(o => o.CrisisSate == (int)CrisisSate.Abnormal).Count() > 0)
                    {
                        nodeGroup.BackColor = ColorTranslator.FromHtml("#FF8080");
                    }
                    else if (currcusItems.Where(p => p.ItemGroupBMId == ItemGroup.ItemgGroupID)
                        .Where(o => o.Symbol == SymbolHelper.SymbolFormatter(Symbol.High) || o.Symbol == SymbolHelper.SymbolFormatter(Symbol.Low) || o.Symbol == SymbolHelper.SymbolFormatter(Symbol.Abnormal)).Count() > 0)
                    {
                        nodeGroup.ForeColor = ColorTranslator.FromHtml("#FF0000");
                        nodeGroup.ForeColor = Color.Red;
                    }                    

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

                }

            }

        }
        private void comCheckType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comCheckType.EditValue = null;
            }
        }

        private void comState_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comState.EditValue = null;
            }
        }

        private void comSumSate_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                comSumSate.EditValue = null;
            }
        }

        private void searchLookUpClientReg_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                searchLookUpClientReg.EditValue = null;
            }
        }
        //加载第一条数据
        private void loadfistdata()
        {
            #region 总检解除锁定
            //if (NowCusRegInf != null && NowCusRegInf.Id != Guid.Empty && NowCusRegInf.SummLocked == (int)SummLockedState.Alr
            //    && NowCusRegInf.SummLockEmployeeBMId == CurrentUser.Id)
            //{
            //    // UpdateCuslockSate(LockStateDto dto)

            //    LockStateDto LockState = new LockStateDto();
            //    LockState.Id = NowCusRegInf.Id;
            //    LockState.SummLocked = (int)SummLockedState.Unchecked;
            //    LockState.SummLockEmployeeBMId = null;
            //    _inspectionTotalService.UpdateCuslockSate(LockState);
            //}
            #endregion

            // 加载人员对应总检数据
            //gridControl2.DataSource = null;
            gridControl3.DataSource = null;
            tvJianChaXiangMu.Nodes.Clear();
            txtCusBM.ResetText();
            labName.ResetText();
            labSex.ResetText();
            labAge.ResetText();
            labSumState.ResetText();
            labClient.ResetText();
            //labSuit.ResetText();
            //labType.ResetText();
            labLoginDate.ResetText();
            _currentItemPictures = null;
            customerRegisterSummarizeDto = null;
            _tjlCustomerRegDto = null;
            memoEditHuiZong.ResetText();
            memoEditZhenDuan.ResetText();
            richAD.ResetText();
            TS = "";
            Wjz = "";
            gridView3.ActiveFilter.Remove(gridColumn34);
            //treeListZhenDuan.DataSource = null;
            if (gridViewCusReg.IsDataRow(gridViewCusReg.FocusedRowHandle))
            {
                if (gridViewCusReg.GetFocusedRowCellValue(conId) is Guid id)
                {
                    TS = "";
                    Wjz = "";




                    var input = new EntityDto<Guid>(id);
                    AutoLoading(() =>
                    {
                        //科室小结
                        var customerRegisterDepartmentSummaryTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterDepartmentSummaryAppService
                                .GetCustomerRegisterDepartmentSummary(input);
                        currCusDeptSum = customerRegisterDepartmentSummaryTask.Result;

                         //检查结果
                         var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                                .GetCustomerRegisterItem(input);

                        currCusRegGroupItems = customerRegisterItemTask.Result;
                        //基本信息
                        var customerRegisterTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterAppService.GetCustomerRegisterById(
                                    input);
                        //图片
                        var itemPictureTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemPictureAppService
                                    .GetItemPictureByCustomerRegisterId(input);
                        //总检
                        var customerRegisterSummarizeTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterSummarizeAppService
                                    .GetSummarizeByCustomerRegisterId(input);
                        //建议
                        var customerRegisterSummarizeSuggestTask = DefinedCacheHelper.DefinedApiProxy
                                .CustomerRegisterSummarizeAppService.GetSummarizeSuggestByCustomerRegisterId(input);

                        #region 冲突提示
                        SumAdviseDto sumAdviseDto = new SumAdviseDto();
                        sumAdviseDto.Sex = customerRegisterTask.Result.Sex;
                        sumAdviseDto.Age = customerRegisterTask.Result.RegAge;
                        sumAdviseDto.CharacterSummary = customerRegisterSummarizeTask.Result?.CharacterSummary;
                        sumAdviseDto.Advice = customerRegisterSummarizeSuggestTask.Result;
                        var strMess = _inspectionTotalService.MatchSumConflict(sumAdviseDto);
                        if (strMess.OutMess != "")
                        {
                            XtraMessageBox.Show(strMess.OutMess);
                        }
                        #endregion
                        //屏蔽职业检数据
                        var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                        gridControl3.DataSource = cusgrouplist;
                        //组合小结
                        _CustomerRegInspectGroupDto = _inspectionTotalService.GetCusGroupList(new QueryClass { CustomerRegId = id }).ToList();

                        BinDingDataTreeS(customerRegisterItemTask.Result, Guid.Empty);

                        ////筛选显示异常
                        //if (checkEdit4.Checked)
                        //{
                        //    gridView3.ActiveFilter.Add(gridColumn30,
                        //        new ColumnFilterInfo("[Symbol] IS NOT NULL AND [Symbol] <> '' AND [Symbol] <> 'M'"));
                        //}
                        //else
                        //{
                        //    gridView3.ActiveFilter.Remove(gridColumn30);
                        //}
                        ////筛选显示已检
                        //if (checkEdit3.Checked)
                        //{
                        //    gridView3.ActiveFilter.Add(gridColumn31,
                        //        new ColumnFilterInfo("[ProcessState] IS NOT NULL AND [ProcessState] <> 2  AND [ProcessState] <> 3"));
                        //}
                        //else
                        //{
                        //    gridView3.ActiveFilter.Add(gridColumn31,
                        //        new ColumnFilterInfo("[ProcessState] IS NOT NULL AND [ProcessState] == 2  or  [ProcessState] == 3"));
                        //}
                        //未检个数
                        var NoCount = customerRegisterItemTask.Result.Where(o => o.ProcessState != null && o.ProcessState != (int)ProjectIState.Complete
                            && o.ProcessState != (int)ProjectIState.Part).Count();
                        if (NoCount > 0)
                        {
                            checkEdit3.Text = "未检项目(" + NoCount + "个)";
                            checkEdit3.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            checkEdit3.Text = "未检项目";
                            checkEdit3.ForeColor = System.Drawing.Color.Black;
                        }

                        var customerRegister = customerRegisterTask.Result;
                        NowCusRegInf = customerRegister;
                        if (customerRegister != null && customerRegister.Id != Guid.Empty && (customerRegister.SummLocked == null || customerRegister.SummLocked == (int)SummLockedState.Unchecked))
                        {
                            #region 总检锁定
                            var IsSD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 21)?.Remarks;
                            if (!string.IsNullOrEmpty(IsSD) && IsSD == "Y")
                            {
                                // UpdateCuslockSate(LockStateDto dto)
                                LockStateDto LockState = new LockStateDto();
                                LockState.SummLocked = (int)SummLockedState.Alr;
                                LockState.SummLockEmployeeBMId = CurrentUser.Id;
                                LockState.Id = customerRegister.Id;
                                _inspectionTotalService.UpdateCuslockSate(LockState);
                            }
                            #endregion
                        }
                        txtCusBM.Text = customerRegister.CustomerBM;
                        labName.Text = customerRegister.Name;
                        labSex.Text = SexHelper.CustomSexFormatter(customerRegister.Sex);
                        labAge.Text = customerRegister.RegAge?.ToString();
                        labSumState.Text = SummSateHelper.SummSateFormatter(customerRegister.SummSate);
                        labClient.Text = customerRegister.ClientName;
                        //labSuit.Text = customerRegister.ItemSuitName;
                        //labelFPNo.Text = customerRegister.FPNo;


                        if (customerRegister.PhysicalType.HasValue)
                        {
                            var checkstate = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ExaminationType.ToString()
                         && o.Value == customerRegister.PhysicalType)?.Text;
                            //labType.Text = checkstate;
                        }
                        labLoginDate.Text = customerRegister.LoginDate?.ToString("d");

                        _currentItemPictures = itemPictureTask.Result;

                        var customerRegisterSummarize = customerRegisterSummarizeTask.Result;
                        if (customerRegisterSummarize != null)
                        {
                            // 绑定总检汇总数据
                            customerRegisterSummarizeDto = customerRegisterSummarize;
                            memoEditHuiZong.Text = customerRegisterSummarize.CharacterSummary;

                        }
                        else
                        {
                            memoEditHuiZong.Text = "";
                            richAD.Text = "";
                            customerRegisterSummarizeDto = null;
                        }
                        //绑定建议
                        _tjlCustomerRegDto = (OutCusListDto)gridViewCusReg.GetFocusedRow();
                        #region 赋值
                        _tjlCustomerRegDto.SummSate = NowCusRegInf.SummSate;
                        _tjlCustomerRegDto.CSEmployeeId = NowCusRegInf.CSEmployeeId;
                        _tjlCustomerRegDto.FSEmployeeId = NowCusRegInf.FSEmployeeId;
                        _tjlCustomerRegDto.SummLockEmployeeBMId = NowCusRegInf.SummLockEmployeeBMId;
                        _tjlCustomerRegDto.SummLocked = NowCusRegInf.SummLocked;
                        #endregion
                        //加载建议
                        int setnot = (int)Sex.GenderNotSpecified;
                        int setUn = (int)Sex.Unknown;
                        //根据性别过滤建议
                        //       string sql = "[SexState] ==" + setnot + " or [SexState] ==" + setUn + " or [SexState]==" + _tjlCustomerRegDto.Sex + "";
                        //       gridView5.ActiveFilter.Add(conSex,
                        //   new ColumnFilterInfo(sql));
                        //       gridView5.ActiveFilter.Add(conMaxAge,
                        //    new ColumnFilterInfo("[MaxAge] >=" + _tjlCustomerRegDto.Age + ""));
                        //       gridView5.ActiveFilter.Add(conMinAge,
                        //new ColumnFilterInfo("[MinAge] <=" + _tjlCustomerRegDto.Age + ""));
                        // treeListZhenDuan.DataSource = customerRegisterSummarizeSuggestTask.Result.Where(p => p.IsZYB != 1).OrderBy(r => r.SummarizeOrderNum).ToList();
                       var rows= customerRegisterSummarizeSuggestTask.Result.Where(p => p.IsZYB != 1).OrderBy(r => r.SummarizeOrderNum).ToList();
                        richAD.Text= AdDrTOStr(rows);
                        string csD = _tjlCustomerRegDto.CSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().FirstOrDefault(n => n.Id == _tjlCustomerRegDto.CSEmployeeId)?.Name;
                        string fsD = _tjlCustomerRegDto.FSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().FirstOrDefault(n => n.Id == _tjlCustomerRegDto.FSEmployeeId)?.Name;
                        //labelShenHeYiSheng.Text = csD + "-" + fsD;
                        LoadDataState();
                        #region 提示
                        //未检
                        var NoGroup = customerRegisterItemTask.Result.Where(o => o.ProcessState == (int)ProjectIState.Not || o.ProcessState == (int)ProjectIState.Part).ToList();

                        var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        //未小结科室
                        var Depatlist = customerRegisterDepartmentSummaryTask.Result.Select(o => o.DepartmentId).Distinct().ToList();
                        var Dpar = customerRegisterItemTask.Result.Where(o =>
                        !Depatlist.Contains(o.DepartmentId) && o.ProcessState != (int)ProjectIState.GiveUp).ToList();

                        if (Dpar.Count > 0)
                        {
                            var depNoIds = Dpar.Select(o => new { o.DepartmentId, o.ProcessState }).Distinct().ToList();
                            var depNo = depNoIds.GroupBy(o => o.DepartmentId).Select(o => new CustomerRegisterDepartmentSummaryDto
                            {
                                CustomerRegId = input.Id,
                                DepartmentId = o.Key,
                                DagnosisSummary = o.Where(r => r.ProcessState == (int)ProjectIState.GiveUp).Count() == 0 ? "未小结" :
                                o.Where(r => r.ProcessState != (int)ProjectIState.GiveUp).Count() > 0 ? "未小结（部分放弃）" : "未小结（全部放弃）",
                            }
                              ).Distinct().ToList();
                            customerRegisterDepartmentSummaryTask.Result.AddRange(depNo);
                            var depart = Dpar.Select(o => o.DepartmentId).ToList();
                            var deparNames = DefinedCacheHelper.GetDepartments().Where(o => depart.Contains(o.Id)).Select(o => o.Name).ToList();
                            if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                            {
                                var groupsIds = _CustomerRegInspectGroupDto.Where(p => p.IsZYB != 1).Select(p => p.DepartmentName).Distinct().ToList();
                                deparNames = deparNames.Where(p => groupsIds.Contains(p)).ToList();
                            }


                            TS += string.Join(",", deparNames).TrimEnd(',') + "，未生成科室小结，总检汇总中将不显示该科室信息\r\n";
                        }

                        if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                        {
                            var groupsIds = _CustomerRegInspectGroupDto.Where(p => p.IsZYB != 1).Select(p => p.ItemGroupBM_Id).Distinct().ToList();

                            NoGroup = NoGroup.Where(p => groupsIds.Contains(p.ItemGroupBMId)).ToList();
                        }
                        if (NoGroup.Count > 0)
                        {
                            var grouIDs = NoGroup.Select(o => o.ItemGroupBMId).Distinct().ToList();
                            var NoGroupName = DefinedCacheHelper.GetItemGroups().Where(o => grouIDs.Contains(o.Id)).Select(o => o.ItemGroupName).ToList();
                            TS += string.Join(",", NoGroupName).TrimEnd(',') + "，未检或部分检查\r\n";
                        }
                        //危急值
                        var CrisisGroup = customerRegisterItemTask.Result.Where(n => n.CrisisSate == (int)CrisisSate.Abnormal).ToList();
                        if (CrisisGroup.Count > 0)
                        {
                            var ITemIds = CrisisGroup.Select(o => o.ItemId).ToList();

                            var ItemNames = DefinedCacheHelper.GetItemInfos().Where(o => ITemIds.Contains(o.Id)).Select(o => o.Name).ToList();
                            Wjz = string.Join(",", ItemNames).TrimEnd(',') + "，存在重要异常结果\r\n";
                        }
                        if (TS != "")
                        {
                            alertInfo.Show(this, "提示!", TS);
                        }
                        if (Wjz != "")
                        {
                            // alertInfo.Show(this, "提示!", Wjz);
                            ShowMessageSucceed(Wjz);
                        }
                        #endregion
                        //过滤职业科室小结
                        //var deparIDlist = cusgrouplist.Select(p => p.DepartmentId).Distinct().ToList();
                        //gridControl2.DataSource = customerRegisterDepartmentSummaryTask.Result.Where(
                        //    p => deparIDlist.Contains(p.DepartmentId.Value)).ToList();
                        #region 复查显示
                        EntityDto<Guid> reviewinput = new EntityDto<Guid>();
                        reviewinput.Id = _tjlCustomerRegDto.Id;
                        var review = _inspectionTotalService.GetCusReViewDto(input);
                        if (review.Count > 0)
                        {
                            layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            gridReview.DataSource = review;
                            gridReview.Visible = true;
                        }
                        else
                        {
                            layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            gridReview.Visible = false;
                        }
                        #endregion
                        if (_tjlCustomerRegDto.PhysicalType.HasValue)
                        {
                            // var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                            //var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                            if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("健康证")))
                            {
                                tabbedControlGroup2.SelectedTabPageIndex = 1;
                                layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                //layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                radioQualified.EditValue = customerRegisterSummarizeDto?.Qualified;
                                richEditOpinion.Text = customerRegisterSummarizeDto?.Opinion;
                            }
                            else
                            {
                                tabbedControlGroup2.SelectedTabPageIndex = 0;
                                //layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
                        }
                    });
                }
            }

        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            return;
        }


        

        private void gridControl3_DataSourceChanged(object sender, EventArgs e)
        {
            if (gridControl3.DataSource != null)
            {
                gridView3.BestFitColumns();
            }
        }

        private void gridView3_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            e.Handled = true;
            if (e.Column == gridColumn9)
            {
                var v1 = DefinedCacheHelper.GetDepartments().Find(r => r.Id == (Guid)e.Value1);
                var v2 = DefinedCacheHelper.GetDepartments().Find(r => r.Id == (Guid)e.Value2);
                //if (v1.Id == Guid.Parse("F7567B9B-1581-48C0-8474-460C84827E56"))
                //{
                //}
                //if (v1.Id == Guid.Parse("62d15d0c-e526-4faf-bdb7-c3f39e573689"))
                //{
                //}
                if (v1 == null || v2 == null)
                {
                    return;

                }
                if (e.SortOrder == ColumnSortOrder.Ascending)
                {
                    if (v1.OrderNum > v2.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1.OrderNum < v2.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
                else if (e.SortOrder == ColumnSortOrder.Descending)
                {
                    if (v1.OrderNum < v2.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1.OrderNum > v2.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
            }
            else if (e.Column == gridColumn10)
            {
                e.Handled = true;
                var v1 = DefinedCacheHelper.GetItemGroups().Find(r => r.Id == (Guid)e.Value1);
                var v2 = DefinedCacheHelper.GetItemGroups().Find(r => r.Id == (Guid)e.Value2);

                if (e.SortOrder == ColumnSortOrder.Ascending)
                {
                    if (v1?.OrderNum > v2?.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1?.OrderNum < v2?.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
                else if (e.SortOrder == ColumnSortOrder.Descending)
                {
                    if (v1?.OrderNum < v2?.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1?.OrderNum > v2?.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
            }
            else if (e.Column == gridColumn11)
            {
                e.Handled = true;
                var v1 = DefinedCacheHelper.GetItemInfos().Find(r => r.Id == (Guid)e.Value1);
                var v2 = DefinedCacheHelper.GetItemInfos().Find(r => r.Id == (Guid)e.Value2);
                if (e.SortOrder == ColumnSortOrder.Ascending)
                {
                    if (v1?.OrderNum > v2?.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1?.OrderNum < v2?.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
                else if (e.SortOrder == ColumnSortOrder.Descending)
                {
                    if (v1?.OrderNum < v2?.OrderNum)
                    {
                        e.Result = 1;
                    }
                    else if (v1?.OrderNum > v2?.OrderNum)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = 0;
                    }
                }
            }
        }

        private void gridView3_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn13)
            {
                if (e.CellValue is Guid id)
                {
                    if (_currentItemPictures != null)
                    {
                        //var button = new RepositoryItemButtonEdit();
                        repositoryItemButtonEdit5.TextEditStyle = TextEditStyles.HideTextEditor;
                        if (_currentItemPictures.Any(r => r.ItemBMID == id))
                        {
                            var item = new RepositoryItemButtonEdit();
                            item.Buttons.Clear();
                            item.Buttons.Add(new EditorButton(ButtonPredefines.Search));
                            item.TextEditStyle = TextEditStyles.HideTextEditor;
                            item.ButtonClick -= GridButtonChaKanTuPian_ButtonClick;
                            item.ButtonClick += GridButtonChaKanTuPian_ButtonClick;

                            var pictureArg = new PictureArg();
                            pictureArg.CurrentItemId = id;
                            pictureArg.Pictures = _currentItemPictures.Where(r => r.ItemBMID == id).ToList();
                            item.Buttons[0].Tag = pictureArg;
                            e.RepositoryItem = item;
                        }

                    }
                }
            }
        }

        private void Button_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // 显示图片
            if (e.Button.Tag is PictureArg arg)
            {
                using (var frm = new InspectionTotalPicture(arg))
                {
                    frm.ShowDialog(this);
                }
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked)
            {
                //gridView3.ActiveFilter.Add(gridColumn30,
                //    new ColumnFilterInfo("[Symbol] IS NOT NULL AND [Symbol] <> '' AND [Symbol] <> 'M'"));

                if (currCusRegGroupItems != null && currCusRegGroupItems.Count > 0)
                {
                    var res = currCusRegGroupItems.Where(p => p.Symbol != null && p.Symbol != ""
                      && p.Symbol != "M").ToList();
                    gridControl3.DataSource = res;
                }
            }
            else
            {
                gridView3.ActiveFilter.Remove(gridColumn30);
                gridControl3.DataSource = currCusRegGroupItems;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                //gridView3.ActiveFilter.Add(gridColumn31,
                //    new ColumnFilterInfo("[ProcessState] IS NOT NULL AND [ProcessState] <> 2  AND [ProcessState] <> 3"));

                if (currCusRegGroupItems != null && currCusRegGroupItems.Count > 0)
                {
                    var res = currCusRegGroupItems.Where(p => p.ProcessState != null && p.ProcessState != 2
                      && p.ProcessState != 3).ToList();
                    gridControl3.DataSource = res;
                }





            }
            else
            {
                gridView3.ActiveFilter.Remove(gridColumn31);
                gridControl3.DataSource = currCusRegGroupItems;
            }
        }

        private void simpleButtonShengCheng_Click(object sender, EventArgs e)
        {
            makeSum();
        }
        private void makeSum(int isOK = 1)
        {

            if (_tjlCustomerRegDto == null)
            {
                XtraMessageBox.Show("请选择体检人！");
                return;
            }
            // 生成汇总
            var basicDictionary = DefinedCacheHelper.GetBasicDictionary();
            var field总检基础字典数据 = basicDictionary.Where(r => r.Type == BasicDictionaryType.CusSumSet.ToString()).ToList();
            if (field总检基础字典数据.Count == 0)
            {
                ShowMessageBoxError("缺少总检配置，无法生成总检!");
                return;
            }
            //项目小结生成汇总
            var xmck = field总检基础字典数据.Find(r => r.Value == 16)?.Remarks;
            if (xmck == null)
            {
                xmck = "";
            }
            var field汇总格式化字符串 = "";
            if (xmck != "Y")
            {
                var field总检汇总配置 = field总检基础字典数据.Find(r => r.Value == 3);
                if (field总检汇总配置 == null)
                {
                    ShowMessageBoxError("缺少总检汇总配置，无法生成总检!");
                    return;
                }

                field汇总格式化字符串 = field总检汇总配置.Remarks;
                if (string.IsNullOrWhiteSpace(field汇总格式化字符串))
                {
                    ShowMessageBoxError("缺少总检汇总配置，无法生成总检!");
                    return;
                }
            }
            if (TS != "")
            {
                //var IsTrueno = XtraMessageBox.Show(TS, "是否强制下总检", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                //if (!IsTrueno)
                //    return;

                alertInfo.Show(this, "提示!", TS);
            }
            if (Wjz != "")
            {
                MessageBox.Show(Wjz);
                // alertInfo.Show(this, "提示!", Wjz);
                // ShowMessageSucceed(Wjz);
            }
            var field匹配多条建议 = false;
            var field匹配多条建议规则 = field总检基础字典数据.Find(r => r.Value == 5);
            if (field匹配多条建议规则 != null && field匹配多条建议规则.Remarks == 1.ToString())
            {
                field匹配多条建议 = true;
            }

            //if (gridControl2.DataSource is List<CustomerRegisterDepartmentSummaryDto> field科室小结汇总)
            //{
                //小结和体检人不匹配提示
                //if (field科室小结汇总.Count > 0 && field科室小结汇总[0].CustomerRegId != _tjlCustomerRegDto.Id)
                //{
                //    MessageBox.Show("当前体检人和科室信息不匹配请刷新后重试！");
                //    return;
                //}
                var field总检汇总字符串生成器 = new StringBuilder();
                var index = 1;
                var field科室列表 = DefinedCacheHelper.GetDepartments();

                // var isShowxmjg = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 10)?.Remarks;
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;

                if (xmck != null && xmck == "Y")
                {
                    bool isOk = false;
                    if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                    {
                        field总检汇总字符串生成器.AppendLine(ItemLoadStr(true));
                    }
                    else
                    {
                        field总检汇总字符串生成器.AppendLine(ItemLoadStr(false));
                    }
                }
                else if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                {

                    field总检汇总字符串生成器.AppendLine(GroupLoadStr());
                }
                else
                {
                    //显示组合序号
                    #region 按科室小结生成总检
                    var ZJGS = field总检基础字典数据.Find(r => r.Value == 8)?.Remarks;
                    if (!string.IsNullOrWhiteSpace(ZJGS) && ZJGS == "Y")
                    {
                        foreach (var customerRegisterDepartmentSummaryDto in currCusDeptSum)
                        {
                            var field科室 = field科室列表.Find(r => r.Id == customerRegisterDepartmentSummaryDto.DepartmentId && customerRegisterDepartmentSummaryDto.OriginalDiag != null && customerRegisterDepartmentSummaryDto.OriginalDiag != "");
                            if (field科室 != null)
                            {
                                field总检汇总字符串生成器.AppendLine(field汇总格式化字符串.Replace("【序号】", index.ToString()).Replace("【科室名称】", field科室.Name)
                                                .Replace("【科室小结】", customerRegisterDepartmentSummaryDto.OriginalDiag)).Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                                index++;
                            }
                        }
                        var sumstr = field总检汇总字符串生成器.ToString().Replace("&", "");

                        var sumlist = sumstr.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        var newSum = "";
                        for (int i = 0; i < sumlist.Count; i++)
                        {
                            newSum += (i + 1) + "、" + sumlist[i] + "\r\n";
                        }
                        field总检汇总字符串生成器.Clear();
                        field总检汇总字符串生成器.Append(newSum);
                    }
                    else
                    {
                        //字典中屏蔽的科室诊断
                        var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 1);
                        if (IsYC != null && IsYC.Remarks == "0")
                        {
                            var IsYCgjc = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 2);
                            if (IsYCgjc != null && IsYCgjc.Remarks != "")
                            {
                                var gjcls = IsYCgjc.Remarks.Split('|').ToList();
                            currCusDeptSum = currCusDeptSum.Where(p => !gjcls.Contains(p.DagnosisSummary.Replace(" ", ""))).ToList();

                            }
                        }

                        foreach (var customerRegisterDepartmentSummaryDto in currCusDeptSum)
                        {
                            var field科室 = field科室列表.Find(r => r.Id == customerRegisterDepartmentSummaryDto.DepartmentId);
                            if (field科室 != null)
                            {
                                field总检汇总字符串生成器.AppendLine(field汇总格式化字符串.Replace("【序号】", index.ToString()).Replace("【科室名称】", field科室.Name)
                                                .Replace("【科室小结】", customerRegisterDepartmentSummaryDto.DagnosisSummary)).Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                                index++;

                            }
                        }
                    }
                    #endregion
                }
                //异步处理更新汇总有问题所以控制是否异步
                if (isOK == 1)
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        memoEditHuiZong.Text = field总检汇总字符串生成器.ToString();
                        if (memoEditHuiZong.Text.Trim() == "")
                        {
                            memoEditHuiZong.Text = "* 本次体检所检项目未发现明显异常。";
                            string strSum = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 24)?.Remarks;
                            if (!string.IsNullOrEmpty(strSum))
                            {
                                memoEditHuiZong.Text = strSum;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(memoEditHuiZong.Text))
                        {
                            // 匹配建议
                            var field总检建议列表 = DefinedCacheHelper.GetSummarizeAdvices().Where(o => o.AdviceName != "").OrderBy(p => p.OrderNum);
                            // 过滤数据
                            var field已匹配建议 = new ConcurrentDictionary<string, SummarizeAdviceDto>();
                            Parallel.ForEach(field总检建议列表, o =>
                            {
                                var field非异常诊断前缀 = field总检基础字典数据.Find(r => r.Value == 7)?.Remarks;

                                string adName = "";
                                if (string.IsNullOrEmpty(o.Advicevalue))
                                {
                                    adName = o.AdviceName;
                                }
                                else
                                { adName = o.Advicevalue; }
                                var field建议依据集合 = adName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var s in field建议依据集合)
                                {
                                    if (memoEditHuiZong.Text.Contains(s))
                                    {
                                        //排除非异常前缀如 未见脂肪干
                                        if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                        {
                                            var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                            bool havalue = false;
                                            foreach (var str in field非异常诊断前缀列表)
                                            {
                                                if (memoEditHuiZong.Text.Contains(str + s))
                                                { havalue = true; }
                                            }
                                            if (havalue == false)
                                            {
                                                field已匹配建议.TryAdd(s, o);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            field已匹配建议.TryAdd(s, o);
                                            break;
                                        }
                                    }
                                }
                            });

                            if (field已匹配建议.Count != 0)
                            {
                                //按汇总顺序排序
                                foreach (var item in field已匹配建议)
                                {
                                    if (item.Value != null)
                                    {
                                        item.Value.OrderNum = memoEditHuiZong.Text.IndexOf(item.Key);
                                    }
                                }
                                if (!field匹配多条建议)
                                {
                                    var field待删除建议 = (from keyValuePair in field已匹配建议 from keyValuePair1 in field已匹配建议 where keyValuePair1.Key != keyValuePair.Key where keyValuePair1.Key.Contains(keyValuePair.Key) select keyValuePair.Key).ToList();
                                    foreach (var s in field待删除建议)
                                    {
                                        field已匹配建议.TryRemove(s, out var value);
                                    }
                                }

                                if (gridViewCusReg.GetFocusedRow() is OutCusListDto row)
                                {
                                    var rows = new List<CustomerRegisterSummarizeSuggestDto>();

                                    foreach (var keyValuePair in field已匹配建议)
                                    {


                                        rows.Add(SummarizeBMToJL(keyValuePair.Value));
                                    }

                                    rows = rows.OrderBy(r => r.SummarizeOrderNum).ToList();
                                    foreach (var customerRegisterSummarizeSuggestDto in rows)
                                    {
                                        customerRegisterSummarizeSuggestDto.SummarizeOrderNum = rows.IndexOf(customerRegisterSummarizeSuggestDto) + 1;
                                    }
                                    rows = HBSum(rows);
                                    rows = MakeReview(rows);

                                    //treeListZhenDuan.DataSource = rows;
                                    richAD.Text = AdDrTOStr(rows);

                                }
                            }
                        }
                    }));
                }
                else if (isOK == 2)
                {
                    memoEditHuiZong.Text = field总检汇总字符串生成器.ToString();
                    if (memoEditHuiZong.Text.Trim() == "")
                    {
                        memoEditHuiZong.Text = "* 本次体检所检项目未发现明显异常。";
                        string strSum = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 22)?.Remarks;
                        if (!string.IsNullOrEmpty(strSum))
                        {
                            memoEditHuiZong.Text = strSum;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(memoEditHuiZong.Text))
                    {
                        // 匹配建议
                        var field总检建议列表 = DefinedCacheHelper.GetSummarizeAdvices().Where(o => o.AdviceName != "").OrderBy(p => p.OrderNum);
                        // 过滤数据
                        var field已匹配建议 = new ConcurrentDictionary<string, SummarizeAdviceDto>();
                        Parallel.ForEach(field总检建议列表, o =>
                        {
                            var field非异常诊断前缀 = field总检基础字典数据.Find(r => r.Value == 7)?.Remarks;

                            string adName = "";
                            if (string.IsNullOrEmpty(o.Advicevalue))
                            {
                                adName = o.AdviceName;
                            }
                            else
                            { adName = o.Advicevalue; }
                            var field建议依据集合 = adName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var s in field建议依据集合)
                            {
                                if (memoEditHuiZong.Text.Contains(s))
                                {
                                    //排除非异常前缀如 未见脂肪干
                                    if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                                    {
                                        var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                        bool havalue = false;
                                        foreach (var str in field非异常诊断前缀列表)
                                        {
                                            if (memoEditHuiZong.Text.Contains(str + s))
                                            { havalue = true; }
                                        }
                                        if (havalue == false)
                                        {
                                            field已匹配建议.TryAdd(s, o);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        field已匹配建议.TryAdd(s, o);
                                        break;
                                    }
                                }
                            }
                        });

                        if (field已匹配建议.Count != 0)
                        {
                            //按汇总顺序排序
                            foreach (var item in field已匹配建议)
                            {
                                if (item.Value != null)
                                {
                                    item.Value.OrderNum = memoEditHuiZong.Text.IndexOf(item.Key);
                                }
                            }
                            if (!field匹配多条建议)
                            {
                                var field待删除建议 = (from keyValuePair in field已匹配建议 from keyValuePair1 in field已匹配建议 where keyValuePair1.Key != keyValuePair.Key where keyValuePair1.Key.Contains(keyValuePair.Key) select keyValuePair.Key).ToList();
                                foreach (var s in field待删除建议)
                                {
                                    field已匹配建议.TryRemove(s, out var value);
                                }
                            }

                            if (gridViewCusReg.GetFocusedRow() is OutCusListDto row)
                            {
                                var rows = new List<CustomerRegisterSummarizeSuggestDto>();

                                foreach (var keyValuePair in field已匹配建议)
                                {


                                    rows.Add(SummarizeBMToJL(keyValuePair.Value));
                                }

                                rows = rows.OrderBy(r => r.SummarizeOrderNum).ToList();
                                foreach (var customerRegisterSummarizeSuggestDto in rows)
                                {
                                    customerRegisterSummarizeSuggestDto.SummarizeOrderNum = rows.IndexOf(customerRegisterSummarizeSuggestDto) + 1;
                                }
                                rows = HBSum(rows);
                                rows = MakeReview(rows);

                                //treeListZhenDuan.DataSource = rows;
                                richAD.Text = AdDrTOStr(rows);
                            }
                        }
                    }
                }
                else if (isOK == 0)
                { memoEditHuiZong.Text = field总检汇总字符串生成器.ToString(); }
            //}

            #region 冲突提示

            SumAdviseDto sumAdviseDto = new SumAdviseDto();
            sumAdviseDto.Sex = _tjlCustomerRegDto.Sex;
            sumAdviseDto.Age = _tjlCustomerRegDto.Age;
            sumAdviseDto.CharacterSummary = memoEditHuiZong.Text;
            //var cusRegSum = treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
            var cusRegSum = getStrToDR(richAD.Text);
            if (cusRegSum != null && cusRegSum.Count > 0)
            {
                sumAdviseDto.Advice = cusRegSum;

            }

            var strMess = _inspectionTotalService.MatchSumConflict(sumAdviseDto);
            if (strMess.OutMess != "")
            {
                XtraMessageBox.Show(strMess.OutMess);
            }

            #endregion
        }
        /// <summary>
        /// 建议列表转文本
        /// </summary>
        /// <param name="reglist"></param>
        /// <returns></returns>
        private string AdDrTOStr(List<CustomerRegisterSummarizeSuggestDto> reglist)
        {
            string strall = "";
            foreach (var sumlist in reglist)
            {
                string str ="*" + sumlist.SummarizeName + "：\r" +
                    sumlist.Advice;
                strall += str + "\r\n";
            }
            return strall;
        }
        /// <summary>
        /// 文本转建议列表
        /// </summary>
        /// <param name="strsum"></param>
        /// <returns></returns>
        private List<CustomerRegisterSummarizeSuggestDto> getStrToDR(string strsum)
        {
           
            List<CustomerRegisterSummarizeSuggestDto> cusSumList = new List<CustomerRegisterSummarizeSuggestDto>();

            //var sumlist = strsum.Split('*').ToList();
            int Num = 1;
            #region 正则处理办法

            string pattern = @"\*(.*?)[:：]\r\n(.*?)(?=\n\*|$)";
            //Regex regex = new Regex(pattern, RegexOptions.Singleline);
            //MatchCollection matches = regex.Matches(strsum);

            Dictionary<string, string> suggestions = new Dictionary<string, string>();

            var matches = Regex.Matches(strsum, pattern, RegexOptions.Singleline);

             
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                suggestions.Add(match.Groups[1].Value, match.Groups[2].Value);
                                
            }
           

            //foreach (var suggestion in suggestions)
            //{
            //    Console.WriteLine($"名称: {suggestion.Key}, 内容: {suggestion.Value}");

            //}
            #endregion

            foreach (var suggestion in suggestions)
            {
                if (suggestion.Key.Trim() == "")
                {
                    continue;
                }
                
                CustomerRegisterSummarizeSuggestDto cussum = new CustomerRegisterSummarizeSuggestDto();
                
                //if (sumads.Count >= 2)
                //{
                    var sumadlist = DefinedCacheHelper.GetSummarizeAdvices();
                    SummarizeAdviceDto bmInfo = sumadlist.FirstOrDefault(p=>p.AdviceName==
                   Regex.Replace(suggestion.Key,@"^\s+|\s+$", string.Empty));

                    if (bmInfo != null)
                    {
 

                        bmInfo.SummAdvice = Regex.Replace(suggestion.Value,
                            @"^\s+|\s+$", string.Empty);
                        bmInfo.OrderNum = Num;
                        cussum = SummarizeBMToJL(bmInfo);
                        cusSumList.Add(cussum);
                    Num = Num + 1;
                }
                    else
                    {
                        SummarizeAdviceDto newRow = new SummarizeAdviceDto();
                        newRow.Id = Guid.NewGuid();
                        newRow.OrderNum = Num ;
                        newRow.AdviceName = Regex.Replace(suggestion.Key,
                            @"^\s+|\s+$", string.Empty);
                        //@"^[\r\n]+"
                        newRow.SummAdvice = Regex.Replace(suggestion.Value, @"^\s+|\s+$", string.Empty);
                        newRow.IsTestInfo = 1;
                      var adsum=  SummarizeBMToJL(newRow);
                        cusSumList.Add(adsum);
                    Num = Num + 1;
                }
                //}
                //else
                //{
                //    MessageBox.Show(sum + "，建议格式不正确，请检查格式，建议名称用*...：标识");
                //}
            }
            return cusSumList;
        }

        //组合总检结论拼接
        private string GroupLoadStr()
        {

            //var dto = _customerRegDto.CustomerDepSummary.OrderBy(o => o.DepartmentOrder).ToList();
            // var dto = _aTjlCustomerDepSummaryDto.OrderBy(o => o.DepartmentOrder).ToList();
            var dto = _CustomerRegInspectGroupDto.Where(p => p.IsZYB != 1).Select(p => new
            {
                p.DepartmentName,
                p.DepartmentOrderNum,
                p.ItemGroupName,
                p.ItemGroupOrder,
                p.ItemGroupSum,
                p.ItemGroupDiagnosis
            }).Distinct().OrderBy(p => p.DepartmentOrderNum).ThenBy(p => p.ItemGroupOrder).ToList();
            var str = "";
            var departmentName = string.Empty;
            var iCount = 1;
            var groupName = string.Empty;
            var gCount = 1;
            var deparsum = dto.GroupBy(p => p.ItemGroupName).ToList();
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
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 11);

                string ZjFormat = ZjFormatd?.Remarks ?? "";

                if (departmentName != dto[i].DepartmentName)
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【科室序号】", iCount.ToString()).Replace("【科室名称】", dto[i].DepartmentName).Replace("|", "");
                        ZjFormat = ZjFormat.Replace("【中文科室序号】", CommonFormat.NumToChinese(iCount.ToString()));

                    }
                    else
                    {
                        str += $"{iCount}.{dto[i].DepartmentName}{ Environment.NewLine }";
                    }
                    departmentName = dto[i].DepartmentName;
                    gCount = 1;
                    iCount++;

                }
                else
                {

                    ZjFormat = ZjFormat.Substring(ZjFormat.IndexOf("|") + 1, ZjFormat.Length - ZjFormat.IndexOf("|") - 1);
                }
                ZjFormat = ZjFormat.Replace("【序号】", (i + 1).ToString());
                ZjFormat = ZjFormat.Replace("【中文序号】", CommonFormat.NumToChinese((i + 1).ToString()));
                if (groupName != dto[i].ItemGroupName)
                {

                    ZjFormat = ZjFormat.Replace("【组合序号】", gCount.ToString()).Replace("【组合名称】", dto[i].ItemGroupName);
                    ZjFormat = ZjFormat.Replace("【中文组合序号】", CommonFormat.NumToChinese(gCount.ToString()));
                    groupName = dto[i].ItemGroupName;
                    gCount++;

                }

                if (!string.IsNullOrWhiteSpace(dto[i].ItemGroupDiagnosis))
                {
                    if (ZjFormat != "")
                    {
                        ZjFormat = ZjFormat.Replace("【组合小结】", dto[i].ItemGroupDiagnosis.TrimEnd((char[])"\r\n".ToCharArray()));
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
                        ZjFormat = ZjFormat.Replace("【组合小结】", dto[i].ItemGroupSum.TrimEnd((char[])"\r\n".ToCharArray()));
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

            return str;


        }
        //项目总检结论拼接
        private string ItemLoadStr(bool isZJJ)
        {
            string aqllstr = "";

            var dtoNowlist = gridControl3.DataSource as List<CustomerRegisterItemDto>;
            var dtoNow = dtoNowlist.Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M").OrderBy(p => p.CrisisLever).
              ThenBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList();


            //if (dtoNow.Any(p => p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")))
            //{
            //    dtoNow = dtoNowlist.Where(p => p.Symbol != null && p.Symbol != "" && p.Symbol != "M" || (p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压"))).OrderBy(p => p.CrisisLever).
            //  ThenBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM.OrderNum).ToList();

            //}
            if (isZJJ == true)
            {
                dtoNow = dtoNow.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
            }

            //重要异常
            var Crisisdt = dtoNow.Where(p => p.CrisisSate == 1).
                OrderBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList();
            //血压特殊处理
            bool isxy = false;
            List<CustomerRegisterItemDto> CrisisOther = new List<CustomerRegisterItemDto>();
            if (Crisisdt.Count > 0)
            {
                if (Crisisdt.Any(p => p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")))
                {
                    Crisisdt = dtoNow.Where(p => p.CrisisSate == 1 || p.ItemName.Contains("收缩压") || p.ItemName.Contains("舒张压")).
                OrderBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList();
                    isxy = true;
                }
                var CrisisdtNew = GetCustomerRegisterItem(Crisisdt);
                aqllstr += "*重要异常\r\n" + ItemLoadstrFj(CrisisdtNew, true, false);
            }
            //普通异常
            var nomaldt = dtoNow.Where(p => p.CrisisSate != 1 || (p.CrisiChar != null && p.CrisiChar != "")).
                OrderBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList();
            if (isxy == true)
            {
                nomaldt = nomaldt.Where(p => !p.ItemName.Contains("收缩压") && !p.ItemName.Contains("舒张压")).
                    OrderBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList(); ;
            }
            if (nomaldt.Count > 0)
            {
                var nomaldtNew = GetCustomerRegisterItem(nomaldt);
                aqllstr += ItemLoadstrFj(nomaldtNew, false, true);
            }
            return aqllstr;

        }
        /// <summary>
        /// 合并报告单
        /// </summary>
        /// <returns></returns>
        private List<CustomerRegisterItemDto> GetCustomerRegisterItem(List<CustomerRegisterItemDto> inputlist)
        {
            //var cuslist = inputlist.ToList() ;
            var json = JsonConvert.SerializeObject(inputlist);
            var cuslist = JsonConvert.DeserializeObject<List<CustomerRegisterItemDto>>(json);

            List<string> ReportBMList = new List<string>();

            List<Guid> RemoveBMList = new List<Guid>();
            cuslist = cuslist.OrderBy(p => p.CrisisSate == null ? 2 : p.CrisisSate).ToList();
            //去掉重复项目，多部一报告
            List<Guid> itemIDs = new List<Guid>();
            foreach (var input in cuslist)
            {
                if (!string.IsNullOrEmpty(input.ReportBM))
                {
                    var ReportBMlist = cuslist.Where(p => p.ReportBM == input.ReportBM).Select(p => p.ItemName).Distinct().ToList();
                    input.ItemName = string.Join("$", ReportBMlist);
                    if (ReportBMList.Contains(input.ReportBM))
                    {
                        RemoveBMList.Add(input.Id);
                    }
                    if (!itemIDs.Contains(input.ItemId))
                    {
                        ReportBMList.Add(input.ReportBM);
                    }
                    itemIDs.Add(input.ItemId);
                }

            }
            var inputlistnew = cuslist.Where(p => !RemoveBMList.Contains(p.Id)).ToList();
            return inputlistnew;
        }
        /// <summary>
        /// 项目汇总
        /// </summary>
        /// <param name="dtoNow"></param>
        /// <returns></returns>
        private string ItemLoadstrFj(List<CustomerRegisterItemDto> dtoNow, bool isZYYC = false, bool isNomal = false)
        {
            bool hzxs = false;
            var hzxsXMMC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 25);
            if (hzxsXMMC != null && hzxsXMMC.Remarks == "Y")
            {
                hzxs = true;
            }
            var str = "";
            var sumHidelist = DefinedCacheHelper.GetBasicDictionary().Where(p => p.Type == BasicDictionaryType.SumHide.ToString()).OrderByDescending(p => p.Remarks.Length).ToList();

            var deparsum = dtoNow.GroupBy(p => new { p.DepartmentId, p.DepartmentBM?.OrderNum, p.DepartmentBM?.Name }).Select(p => new
            { p.Key.OrderNum, p.Key.Name, p.Key.DepartmentId }).Distinct().OrderBy(p => p.OrderNum).ThenBy(p => p.Name).ToList();
            //换行的科室？不知道哪里用
            string DeparHH = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 18)?.Remarks;
            
            //显示组合名称的科室
            string ShowGroupDepart = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 31)?.Remarks;
            List<string> GroupNamelist = new List<string>();
            List<string> ShowGroupDepartlist = new List<string>();
            if (ShowGroupDepart != null && ShowGroupDepart != "")
            {
                ShowGroupDepartlist = ShowGroupDepart.Replace("，","").Replace(",","").Split('|').ToList();
            }
            int ZXH = 1;
            bool isDepart = true;
            var iszxh = false;
            for (var num = 0; num < deparsum.Count; num++)
            {


                bool HasDepat = false;
                var ZjFormatd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 11);
                string allZjFormat = ZjFormatd?.Remarks ?? "";
                if (allZjFormat.Contains("总序号"))
                { iszxh = true; }
                var dto = dtoNow.Where(p => p.DepartmentId == deparsum[num].DepartmentId).
                    OrderBy(p => p.DepartmentBM?.OrderNum).ThenBy(p => p.ItemGroupBM?.OrderNum).ToList();
                var departformat = "";
                if (allZjFormat.Contains("|"))
                {
                    departformat = allZjFormat.Substring(0, allZjFormat.IndexOf("|"));
                }
                if (departformat != "")
                {
                    if (isZYYC == false)
                    {
                        departformat = departformat.Replace("【科室序号】", (num + 1).ToString()).Replace("【科室名称】", deparsum[num].Name).Replace("|", "");
                        departformat = departformat.Replace("【中文科室序号】", CommonFormat.NumToChinese((num + 1).ToString()));
                    }
                    else
                    {
                        departformat = "";
                    }
                    // str += departformat;
                }
                else
                {

                    if (isZYYC == false && !allZjFormat.Contains("总序号"))
                    { departformat = $"{(num + 1).ToString()}.{ deparsum[num].Name}{ Environment.NewLine }"; }
                    else
                    {
                        departformat = "";
                        isDepart = false;
                    }

                    // str += $"{(num + 1).ToString()}.{ deparsum[num].Name}{ Environment.NewLine }";
                }
                var ItemFormat = allZjFormat.Substring(allZjFormat.IndexOf("|") + 1, allZjFormat.Length - allZjFormat.IndexOf("|") - 1);

                int xh = 1;
                List<Guid> fhlist = new List<Guid>();
                for (int i = 0; i < dto.Count; i++)
                {

                    //复合判断只显示一次项目结果
                    if (fhlist.Contains(dto[i].ItemGroupBMId.Value))
                    {
                        continue;
                    }
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

                    
                    ZjFormat = ZjFormat.Replace("【项目序号】", (xh).ToString());
                   
                    //因为彩超等小结在诊断字段所以诊断有数据就取诊断

                    var itemSum = dto[i].ItemSum;
                    if (!string.IsNullOrWhiteSpace(dto[i].ItemDiagnosis))
                    {
                        itemSum = dto[i].ItemDiagnosis;
                    }
                    //重要异常只显示重要异常诊断
                    if (!string.IsNullOrEmpty(dto[i].CrisiChar) && dto[i].CrisisSate == (int)CrisisSate.Abnormal && isNomal == false)
                    {
                        itemSum = dto[i].CrisiChar.Replace("|", "\r\n");
                    }
                    else if (!string.IsNullOrEmpty(dto[i].CrisiChar) && dto[i].CrisisSate == (int)CrisisSate.Abnormal && isNomal == true)
                    {
                        //显示除重要异常的其他异常
                        var CrisiCharlist = dto[i].CrisiChar.Replace("\r\n", "|").Split('|');
                        foreach (var CrisiChar in CrisiCharlist)
                        {
                            if (!string.IsNullOrEmpty(CrisiChar))
                            {
                                //itemSum = itemSum.Replace(dto[i].CrisiChar, "");
                                itemSum = itemSum.Replace(CrisiChar, "");
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(itemSum))
                    {
                        #region 过滤总检结论
                        List<string> sumHlist = new List<string>();
                        foreach (var sumhide in sumHidelist)
                        {
                            //itemSum = itemSum.Replace(sumhide.Remarks + "\r\n", "");
                            //itemSum = itemSum.Replace(sumhide.Remarks, "");
                            #region 修改隐藏诊断
                            //某个体检类别不隐藏
                            var sumhidelist = sumhide.Code.Replace(",", "|").Replace("，", "|").Replace("；", "|").Replace(";", "|").Split('|').ToList();
                            if (_tjlCustomerRegDto.PhysicalType.HasValue && sumhidelist.Contains(_tjlCustomerRegDto.PhysicalType.Value.ToString()))
                            {
                                continue;

                            }
                            var itemSumnew = itemSum.Replace("\r\n", "&").Replace("。", "&").Replace(";", "&").Replace("；", "&").Replace("，", "&").Replace(",", "&");
                            var itemSumlist = itemSumnew.Split('&').ToList();
                            if (sumhide.Remarks.Contains("\r\n") || sumhide.Remarks.Contains("。") || sumhide.Remarks.Contains(";")
                                || sumhide.Remarks.Contains("；") || sumhide.Remarks.Contains("，") || sumhide.Remarks.Contains(","))
                            {

                                itemSum = itemSum.Replace(sumhide.Remarks + "\r\n", "");
                                itemSum = itemSum.Replace(sumhide.Remarks, "");
                            }
                            else
                            {
                                foreach (var itemSumN in itemSumlist)
                                {


                                    if (itemSumN.Contains(sumhide.Remarks))
                                    {

                                        sumHlist.Add(itemSumN);
                                    }

                                }
                            }
                            #endregion
                        }
                        foreach (var sumHM in sumHlist)
                        {
                            itemSum = itemSum.Replace(sumHM + "\r\n", "");
                            itemSum = itemSum.Replace(sumHM + "。", "");
                            itemSum = itemSum.Replace(sumHM + ";", "");
                            itemSum = itemSum.Replace(sumHM + "；", "");
                            itemSum = itemSum.Replace(sumHM + "，", "");
                            itemSum = itemSum.Replace(sumHM + ",", "");
                            itemSum = itemSum.Replace(sumHM, "");
                        }
                        #endregion
                        if (itemSum.Replace("\r", "").Replace("\n", "") == "")
                        {
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(itemSum))
                        {




                            if (ZjFormat != "")
                            {
                                var xmxj = "";

                                if ((dto[i].Symbol == "P" && itemSum.Contains(dto[i].ItemName + ":")) || (dto[i].Symbol != "P" && itemSum.Contains(dto[i].ItemName)))
                                {
                                    xmxj = itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                     
                                }
                                else if (dto[i].Symbol == "P")
                                {
                                    //合并报告名称处理
                                    if (dto[i].ItemName.Contains("$"))
                                    {
                                        
                                        if (hzxs == true || (dto[i].ItemBM != null && dto[i].ItemBM.IsSummaryName.HasValue && dto[i].ItemBM.IsSummaryName == 1))
                                        {
                                            xmxj = dto[i].ItemName.Replace("$", "、") + ": \r\n" + itemSum.TrimEnd((char[])"\r\n".ToCharArray());

                                        }
                                        else
                                        {
                                            xmxj = itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                        }
                                    }
                                    else
                                    {
                                        //复合判断不显示项目名称
                                        if (dto[i].DiagnSate == 1)
                                        {
                                            xmxj = itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                            //  ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                        }
                                        else
                                        {
                                            string hh = "";
                                            if (!string.IsNullOrEmpty(DeparHH))
                                            {
                                                var deparlist = DeparHH.Split('|').ToList();
                                                if (deparlist.Contains(dto[i].DepartmentBM.Name))
                                                {
                                                    hh = "\r\n";
                                                }

                                            }

                                            if (hzxs == true || (dto[i].ItemBM != null && dto[i].ItemBM.IsSummaryName.HasValue && dto[i].ItemBM.IsSummaryName == 1))
                                            {
                                                xmxj = dto[i].ItemName + ":" + hh + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                            }
                                            else
                                            { xmxj = hh + itemSum.TrimEnd((char[])"\r\n".ToCharArray()); }


                                        }
                                    }
                                }
                                else
                                { //复合判断或结果中包含项目名称则不显示项目名称
                                    if (dto[i].DiagnSate == 1 || itemSum.Contains(dto[i].ItemName))
                                    {
                                        xmxj = itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                    }
                                    else
                                    {

                                        if (hzxs == true || (dto[i].ItemBM != null && dto[i].ItemBM.IsSummaryName.HasValue && dto[i].ItemBM.IsSummaryName == 1))
                                        {
                                            xmxj = dto[i].ItemName + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                        }
                                        else
                                        {
                                            xmxj = itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                        }

                                    }
                                }
                                if (ZjFormat.Contains("【总序号】"))
                                {
                                    //获取数字后一位
                                    var ZjFormatls = ZjFormat.Replace("【总序号】", "&");
                                    string strq = "";
                                    if (ZjFormatls.Length >= 2)
                                    {
                                        strq = ZjFormatls.Substring(ZjFormatls.IndexOf("&") + 1, 1);
                                        if (strq == "【")
                                        {//如果后一位是标签则不取
                                            strq = "";
                                        }
                                    }

                                    var itemlist = xmxj.Replace("\n", "&").Replace("\t", "&").Replace("\r", "&").Replace("\r\n", "&").Split('&').Where(p => p != "" && p != "\r\n").ToList();

                                    if (itemlist.Count > 1)
                                    {
                                        ZjFormat = ZjFormat.Replace("【总序号】", (ZXH).ToString());
                                        ZXH = ZXH + 1;
                                        xmxj = "";
                                        int count = 0;
                                        foreach (var isum in itemlist)
                                        {
                                            if (count >= 1)
                                            {

                                                xmxj += (ZXH).ToString() + strq + isum.Trim() + "\r\n";
                                                if (count != itemlist.Count - 1)
                                                {
                                                    ZXH = ZXH + 1;
                                                }
                                            }
                                            else
                                            {
                                                xmxj += isum.Trim() + "\r\n";
                                            }
                                            count = count + 1;
                                        }
                                    }
                                    else
                                    {
                                        ZjFormat = ZjFormat.Replace("【总序号】", (ZXH).ToString());
                                    }
                                }
                                ZjFormat = ZjFormat.Replace("【项目小结】", xmxj.Trim());
                            }
                            else
                            {
                                if (itemSum.Contains(dto[i].ItemName))
                                {
                                    ZjFormat = ZjFormat.Replace("【项目小结】", itemSum.TrimEnd((char[])"\r\n".ToCharArray()));
                                }
                                else if (dto[i].Symbol == "P")
                                {
                                    var itemname = "";

                                    if (hzxs == true || (dto[i].ItemBM != null && dto[i].ItemBM.IsSummaryName.HasValue && dto[i].ItemBM.IsSummaryName == 1))
                                    {
                                        itemname = dto[i].ItemName + ":";
                                    }
                                    var itemstr = itemname + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                    str += $"{itemstr}{ Environment.NewLine }";
                                }
                                else
                                {
                                    var itemname = "";
                                    if (hzxs == true || (dto[i].ItemBM != null && dto[i].ItemBM.IsSummaryName.HasValue && dto[i].ItemBM.IsSummaryName == 1))
                                    {
                                        itemname = dto[i].ItemName;
                                    }
                                    var itemstr = itemname + itemSum.TrimEnd((char[])"\r\n".ToCharArray());
                                    str += $"{itemstr}{ Environment.NewLine }";
                                }
                            }
                        }
                    }
                    if (ZjFormat != "" && !string.IsNullOrWhiteSpace(itemSum))
                    {
                        
                        ZjFormat = ZjFormat.Replace("【换行】", Environment.NewLine).Replace("【空格】", " ");
                        
                        if (HasDepat == false)
                        {
                            str += departformat;
                            HasDepat = true;
                        }
                        if (ShowGroupDepartlist.Contains(dto[i].DepartmentBM.Name)
                            && !GroupNamelist.Contains(dto[i].ItemGroupBM.ItemGroupName))
                        {
                            ZjFormat =  "\r\n【" + dto[i].ItemGroupBM.ItemGroupName +"】"  + ZjFormat;

                            GroupNamelist.Add(dto[i].ItemGroupBM.ItemGroupName);
                        }
                        str += ZjFormat;
                        
                        xh = xh + 1;
                        ZXH = ZXH + 1;
                    }
                    if (dto[i].DiagnSate == 1)
                    { fhlist.Add(dto[i].ItemGroupBMId.Value); }
                }
                if (isDepart == true)
                {
                    str += $"{ Environment.NewLine }";
                }

            }
            //重要异常区分
            if (iszxh == true && isZYYC == true)
            {
                str = str + "\r\n";
            }
            #region 再处理下隐藏诊断
            var yc = sumHidelist.Where(p => p.Remarks.Contains("\r\n")).ToList();
            foreach (var hidsum in yc)
            {
                //str = str.Replace("\r\n" + hidsum.Remarks+ "\r\n" , "");
                str = str.Replace(hidsum.Remarks + "\r\n", "");
                //str = str.Replace("\r\n" + hidsum.Remarks   , "");
                str = str.Replace(hidsum.Remarks, "");
            }
            #endregion
            return str;
        }
       
        //将下拉框选择的总检诊断编码数据转换为记录表数据
        public CustomerRegisterSummarizeSuggestDto SummarizeBMToJL(SummarizeAdviceDto bmInfo, bool Ispr = false)
        {
            CustomerRegisterSummarizeSuggestDto info = new CustomerRegisterSummarizeSuggestDto();
            if (bmInfo == null)
                return info;
            info.Id = Guid.NewGuid(); //转换时赋ID会导致保存时报错，原因由于ID重复
            info.CustomerRegID = _tjlCustomerRegDto.Id;
            info.SummarizeName = bmInfo.AdviceName;
            info.SummarizeType = 1;
            info.IsSC = 1;
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
        public List<CustomerRegisterSummarizeSuggestDto> OrderByList(List<CustomerRegisterSummarizeSuggestDto> list)
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
            return list.OrderBy(l => l.SummarizeOrderNum).ToList();
        }
     
        private void simpleButtonYuLan_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto != null)
            {
                //if (Variables.ISReg == "0")
                //{
                //	XtraMessageBox.Show("试用版本，不能预览报告！");
                //	return;
                //}
                var printReport = new PrintReportNew();
                //printReport.StrReportTemplateName = "000";
                var cusNameInput = new CusNameInput();
                cusNameInput.Id = _tjlCustomerRegDto.Id;
                printReport.cusNameInput = cusNameInput;
                var MBNamels = DefinedCacheHelper
                  .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
                string[] mbls = MBNamels.Split('|');

                if (mbls.Length > 0)
                {
                    int iszyb = 0;
                    string mb = mbls[0];
                    //从业体检模板绑定
                    if (_tjlCustomerRegDto.PhysicalType.HasValue)
                    {
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        if (tjlb != null && tjlb.Contains("从业") || tjlb.Contains("健康证"))
                        {
                            var jkzmb = mbls.Where(p => p.Contains("健康卡") || p.Contains("健康证")).ToList();
                            if (jkzmb.Count > 0)
                            {
                                mb = jkzmb[0];
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()))
                    {
                        printReport.Print(true, "", mb, "0", 2);
                    }
                    else if (listBoxControlTemplates.EditValue?.ToString() == "根据体检类别匹配")
                    {
                        printReport.Print(true, "", "", "0", 2);
                    }
                    else
                    { printReport.Print(true, "", listBoxControlTemplates.EditValue.ToString(), "0", 2); }
                }

            }
        }

        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto != null)
            {
                //if (Variables.ISReg == "0")
                //{
                //	XtraMessageBox.Show("试用版本，不能打印报告！");
                //	return;
                //}
                string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 300)?.Remarks;
                var sumtstate = _tjlCustomerRegDto.SummSate;

                if (!string.IsNullOrEmpty(strwjshow) && strwjshow == "Y")
                {
                    if (sumtstate != (int)SummSate.Audited)
                    {
                        MessageBox.Show("未审核不能打印报告！");
                        return;
                    }
                }
                var printReport = new PrintReportNew();
                var cusNameInput = new CusNameInput();
                cusNameInput.Id = _tjlCustomerRegDto.Id;
                printReport.cusNameInput = cusNameInput;
                var MBNamels = DefinedCacheHelper
                 .GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
                string[] mbls = MBNamels.Split('|');
                if (mbls.Length > 0)
                {
                    string mb = mbls[0];
                    //从业体检模板绑定
                    if (_tjlCustomerRegDto.PhysicalType.HasValue)
                    {
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        if (tjlb!=null && tjlb.Contains("从业") || tjlb.Contains("健康证"))
                        {
                            var jkzmb = mbls.Where(p => p.Contains("健康卡") || p.Contains("健康证")).ToList();
                            if (jkzmb.Count > 0)
                            {
                                mb = jkzmb[0];
                            }
                        }


                    }
                    if (string.IsNullOrEmpty(listBoxControlTemplates.EditValue?.ToString()))
                    {
                        printReport.Print(false, "", mb, "0", 2);
                    }
                    else if (listBoxControlTemplates.EditValue?.ToString() == "根据体检类别匹配")
                    {
                        printReport.Print(false, "", "", "0", 2);
                    }
                    else
                    { printReport.Print(false, "", listBoxControlTemplates.EditValue?.ToString(), "0", 2); }
                }
                //挪到报表打印事件
                //_printPreviewAppService.UpdateCustomerRegisterPrintState(new EntityDto<Guid> { Id = id });
                ////日志
                //CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                //createOpLogDto.LogBM = gridView1.GetFocusedRowCellValue(gridColumn1).ToString();
                //createOpLogDto.LogName = gridView1.GetFocusedRowCellValue(gridColumn2).ToString();
                //createOpLogDto.LogText = "打印体检报告";
                //createOpLogDto.LogDetail = "";
                //createOpLogDto.LogType = (int)LogsTypes.PrintId;
                //_commonAppService.SaveOpLog(createOpLogDto);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //if (gridControl2.DataSource is List<CustomerRegisterDepartmentSummaryDto> field科室小结汇总)
            //{
            //    //小结和体检人不匹配提示
            //    if (field科室小结汇总.Count > 0 && field科室小结汇总[0].CustomerRegId != _tjlCustomerRegDto.Id)
            //    {
            //        MessageBox.Show("当前体检人和科室信息不匹配请刷新后重试！");
            //        return;
            //    }
            //}
           

            if (!Save())
            {
                return;
            }

            LoadDataState();

            alertInfo.Show(this, "提示!", "总检完成。");

        }

        private void simpleButtonShenHe_Click(object sender, EventArgs e)
        {
            //if (gridControl2.DataSource is List<CustomerRegisterDepartmentSummaryDto> field科室小结汇总)
            //{
            //    //小结和体检人不匹配提示
            //    if (field科室小结汇总.Count > 0 && field科室小结汇总[0].CustomerRegId != _tjlCustomerRegDto.Id)
            //    {
            //        MessageBox.Show("当前体检人和科室信息不匹配请刷新后重试！");
            //        return;
            //    }
            //}
           
            if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
            {
                ShowMessageSucceed("已审核总检。");
                return;
            }
            else if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview)
            {
                if (!Save())
                {
                    return;
                }
            }
            else if (_tjlCustomerRegDto.SummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.SummSate == null)
            {
                if (!Save())
                {
                    return;
                }
            }

            if (customerRegisterSummarizeDto != null)
            {
                //更新建议汇总
                customerRegisterSummarizeDto.ShEmployeeBMId = CurrentUser.Id;// (long)customyiSheng.EditValue;
                if (!customerRegisterSummarizeDto.EmployeeBMId.HasValue)
                {
                    customerRegisterSummarizeDto.EmployeeBMId = CurrentUser.Id;// (long)customyiSheng.EditValue;
                }
                customerRegisterSummarizeDto.CheckState = 2;
                _inspectionTotalService.SaveSummarize(customerRegisterSummarizeDto);
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                createOpLogDto.LogText = "保存总检审核";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.SumId;
                _commonAppService.SaveOpLog(createOpLogDto);
            }
            if (_tjlCustomerRegDto != null)
            {
                //更新患者体检信息表
                _tjlCustomerRegDto.SummSate = (int)SummSate.Audited;
                //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);
                EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                editCustomerRegStateDto.SummLocked = (int)SummLockedState.Unchecked;
                editCustomerRegStateDto.CSEmployeeId = _tjlCustomerRegDto.CSEmployeeId;
                editCustomerRegStateDto.FSEmployeeId = CurrentUser.Id;// (long)customyiSheng.EditValue;
                editCustomerRegStateDto.SummSate = (int)SummSate.Audited;

                editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                _inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);
                #region 健康证
                if (radioQualified.EditValue != null && radioQualified.EditValue?.ToString() == "合格")
                {
                    var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                    var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                    if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("食品") || tjlb.Contains("健康证"))) //健康证
                    {

                        EntityDto<Guid> entityDto = new EntityDto<Guid>();
                        entityDto.Id = _tjlCustomerRegDto.Id;
                        _printPreviewAppService.UpdateCustomerRegisterHGZState(entityDto);

                    }
                }
                #endregion

                //报告
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "东软")
                    {
                        NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                        neInterface.GetRepAsync(_tjlCustomerRegDto.CustomerBM);

                        //健康芜湖上传报告
                        if (_tjlCustomerRegDto.InfoSource.HasValue && _tjlCustomerRegDto.InfoSource == 2)
                        {
                            HealthInWuHuInterface healthInWuHuInterface = new HealthInWuHuInterface();
                            healthInWuHuInterface.SendRepot(_tjlCustomerRegDto.CustomerBM);
                        }
                    }
                }




            }
            LoadDataState();

            // ShowMessageSucceed("总检审核完成。");
            alertInfo.Show(this, "提示!", "总检审核完成。");

        }
        //保存方法
        private bool Save()
        {
            try
            {


                //删除建议表（多条）
                _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id, IsZYB = 2 });
                //插入建议表（多条）
                var SbStr = string.Empty;
                var _CustomerSummarizeBM = new List<TjlCustomerSummarizeBMDto>();
                var _CustomerSummarizeList = getStrToDR(richAD.Text);// treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
                var sumchar = memoEditHuiZong.Text;
                #region 空建议默认生成一条建议

                if (string.IsNullOrEmpty(sumchar))
                {
                    string strSum = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 24)?.Remarks;
                    if (!string.IsNullOrEmpty(strSum))
                    {
                        sumchar = strSum;
                        memoEditHuiZong.Text = strSum;
                        if (_CustomerSummarizeList == null)
                        {
                            _CustomerSummarizeList = new List<CustomerRegisterSummarizeSuggestDto>();
                        }
                        if (_CustomerSummarizeList.Count == 0)
                        {
                            string strAd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 23)?.Remarks;
                            if (!string.IsNullOrEmpty(strAd))
                            {
                                var adsum = DefinedCacheHelper.GetSummarizeAdvices().FirstOrDefault(p => p.AdviceName == strAd);
                                if (adsum != null)
                                {
                                    _CustomerSummarizeList.Add(SummarizeBMToJL(adsum));
                                    richAD.Text = AdDrTOStr(_CustomerSummarizeList);
                                    //treeListZhenDuan.DataSource = _CustomerSummarizeList;
                                    //treeListZhenDuan.RefreshDataSource();
                                    //treeListZhenDuan.Refresh();
                                }
                            }
                        }
                    }


                    //var nrsum=
                }
                #endregion
                foreach (var MatchingItem in _CustomerSummarizeList.OrderBy(l => l.SummarizeOrderNum))
                {
                    if (MatchingItem.SummarizeName.Trim() == string.Empty && MatchingItem.Advice.Trim() == string.Empty)
                    {
                        continue;
                    }
                    SbStr += "*" + MatchingItem.SummarizeName + "：" + MatchingItem.Advice;
                    TjlCustomerSummarizeBMDto tjlCustomerSummarizeBMDto = new TjlCustomerSummarizeBMDto();
                    tjlCustomerSummarizeBMDto.Id = MatchingItem.Id;
                    tjlCustomerSummarizeBMDto.IsPrivacy = MatchingItem.IsPrivacy;
                    tjlCustomerSummarizeBMDto.CustomerRegID = MatchingItem.CustomerRegID;
                    tjlCustomerSummarizeBMDto.isReview = MatchingItem.isReview;
                    tjlCustomerSummarizeBMDto.Advice = MatchingItem.Advice;
                    tjlCustomerSummarizeBMDto.ParentAdviceId = MatchingItem.ParentAdviceId;
                    tjlCustomerSummarizeBMDto.SummarizeAdviceId = MatchingItem.SummarizeAdviceId;
                    tjlCustomerSummarizeBMDto.SummarizeName = MatchingItem.SummarizeName;
                    tjlCustomerSummarizeBMDto.SummarizeOrderNum = MatchingItem.SummarizeOrderNum;
                    tjlCustomerSummarizeBMDto.SummarizeType = MatchingItem.SummarizeType;
                    tjlCustomerSummarizeBMDto.IsSC = MatchingItem.IsSC;
                    tjlCustomerSummarizeBMDto.IsZYB = 2;
                    _CustomerSummarizeBM.Add(tjlCustomerSummarizeBMDto);
                }
                string fcxm = "";
                _inspectionTotalService.CreateSummarizeBM(_CustomerSummarizeBM);
                //总检保存复查项目
                var review = gridReview.DataSource as List<CusReViewDto>;
                if (review != null && review.Count > 0)
                {
                    var fcxmls = review.SelectMany(o => o.ItemGroup).Select(o => o.ItemGroupName).Distinct().ToList();
                    string grouname = string.Join(",", fcxmls);
                    fcxm = grouname.TrimEnd(',');
                }
                if (customerRegisterSummarizeDto == null)
                {
                    //插入建议汇总 //插入建议表（整体建议 单条）
                    var _TjlCustomerSummarize = new CustomerRegisterSummarizeDto();
                    _TjlCustomerSummarize.CustomerRegID = _tjlCustomerRegDto.Id;
                    _TjlCustomerSummarize.ShEmployeeBMId = CurrentUser.Id;// (long)customyiSheng.EditValue;
                    _TjlCustomerSummarize.EmployeeBMId = CurrentUser.Id;//(long)customyiSheng.EditValue;
                    _TjlCustomerSummarize.CharacterSummary = sumchar;
                    _TjlCustomerSummarize.PrivacyCharacterSummary = "";
                    _TjlCustomerSummarize.Advice = SbStr;
                    var nowdate= _commonAppService.GetDateTimeNow().Now;
                    _TjlCustomerSummarize.ExamineDate = nowdate;
                    _TjlCustomerSummarize.ConclusionDate = nowdate;
                    _TjlCustomerSummarize.CheckState = 1;

                    if (fcxm != "")
                    {
                        _TjlCustomerSummarize.ReviewContent = fcxm;

                    }
                    if (_tjlCustomerRegDto.PhysicalType.HasValue)
                    {
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("健康证")))
                        {
                            if (radioQualified.EditValue == null)
                            {
                                MessageBox.Show("请选择是否合格！");
                                return false;
                            }
                            _TjlCustomerSummarize.Qualified = radioQualified.EditValue?.ToString();
                            _TjlCustomerSummarize.Opinion = richEditOpinion.Text;
                        }
                    }
                    var result = _inspectionTotalService.SaveSummarize(_TjlCustomerSummarize);

                    #region 冲突提示
                    SumAdviseDto sumAdviseDto = new SumAdviseDto();
                    sumAdviseDto.Sex = _tjlCustomerRegDto.Sex;
                    sumAdviseDto.Age = _tjlCustomerRegDto.Age;
                    sumAdviseDto.CharacterSummary = _TjlCustomerSummarize?.CharacterSummary;
                    if (_CustomerSummarizeBM != null && _CustomerSummarizeBM.Count > 0)
                    {
                        sumAdviseDto.Advice = new List<CustomerRegisterSummarizeSuggestDto>();
                        foreach (var ad in _CustomerSummarizeBM)
                        {
                            CustomerRegisterSummarizeSuggestDto input = new CustomerRegisterSummarizeSuggestDto();
                            input.Advice = ad.Advice;
                            input.SummarizeName = ad.SummarizeName;
                            sumAdviseDto.Advice.Add(input);
                        }

                    }

                    var strMess = _inspectionTotalService.MatchSumConflict(sumAdviseDto);
                    if (strMess.OutMess != "")
                    {
                        XtraMessageBox.Show(strMess.OutMess);
                    }
                    #endregion
                    customerRegisterSummarizeDto = result;
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                    createOpLogDto.LogText = "保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);

                }
                else
                {
                    if (_tjlCustomerRegDto.PhysicalType.HasValue)
                    {
                        var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                        var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                        if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("健康证")))
                        {
                            if (radioQualified.EditValue == null)
                            {
                                MessageBox.Show("请选择是否合格！");
                                return false;
                            }
                            customerRegisterSummarizeDto.Qualified = radioQualified.EditValue?.ToString();
                            customerRegisterSummarizeDto.Opinion = richEditOpinion.Text;
                        }
                    }

                    customerRegisterSummarizeDto.CustomerRegID = _tjlCustomerRegDto.Id;

                    customerRegisterSummarizeDto.ShEmployeeBMId = CurrentUser.Id;// (long)customyiSheng.EditValue;
                    //审核不修改总检医生
                    if (!customerRegisterSummarizeDto.EmployeeBMId.HasValue)
                    {
                        customerRegisterSummarizeDto.EmployeeBMId = CurrentUser.Id;// (long)customyiSheng.EditValue;

                    }
                    if (!customerRegisterSummarizeDto.ExamineDate.HasValue)
                    {
                        customerRegisterSummarizeDto.ExamineDate = _commonAppService.GetDateTimeNow().Now;
                    }
                    customerRegisterSummarizeDto.CharacterSummary = sumchar;
                    customerRegisterSummarizeDto.Advice = SbStr;
                    customerRegisterSummarizeDto.ConclusionDate = _commonAppService.GetDateTimeNow().Now;
                    customerRegisterSummarizeDto.CheckState = 1;
                    if (fcxm != "")
                    {
                        customerRegisterSummarizeDto.ReviewContent = fcxm;
                    }
                    var result = _inspectionTotalService.SaveSummarize(customerRegisterSummarizeDto);
                    customerRegisterSummarizeDto = result;
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                    createOpLogDto.LogText = "保存总检";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                //保存复查
                SaveReview();
                //更新患者体检信息表
                INCusSumSateDto iNCusSumSateDto = new INCusSumSateDto();
                iNCusSumSateDto.FSEmployeeId = _tjlCustomerRegDto.FSEmployeeId;
                iNCusSumSateDto.Id = _tjlCustomerRegDto.Id;

                //上面是不修改的字段
                iNCusSumSateDto.SummSate = (int)SummSate.HasInitialReview;
                iNCusSumSateDto.SummLocked = 2;
                iNCusSumSateDto.CSEmployeeId = customerRegisterSummarizeDto.EmployeeBMId;

                _inspectionTotalService.UpdateCusSumSate(iNCusSumSateDto);
                //更新当前人
                _tjlCustomerRegDto.FSEmployeeId = iNCusSumSateDto.FSEmployeeId;
                _tjlCustomerRegDto.SummSate = iNCusSumSateDto.SummSate;
                _tjlCustomerRegDto.CSEmployeeId = iNCusSumSateDto.CSEmployeeId;
                _tjlCustomerRegDto.SummLocked = iNCusSumSateDto.SummLocked;





            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
            }
            return true;
        }
        #region 复查相关
        private List<CustomerRegisterSummarizeSuggestDto> MakeReview(List<CustomerRegisterSummarizeSuggestDto> input)
        {
            try
            {
                //获取默认回访数据
                repositoryItemCheckedComboBoxEdit2.ValueMember = " Id";
                repositoryItemCheckedComboBoxEdit2.DisplayMember = "ItemGroupName";
                repositoryItemCheckedComboBoxEdit2.DataSource = DefinedCacheHelper.GetItemGroups();

                var review = _inspectionTotalService.GetIllReViewNewDto(input);
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
                if (review != null && review.Count > 0)
                {
                    _inspectionTotalService.SaveCusReViewDto(review);
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                    createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                    createOpLogDto.LogText = "保存复查";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.SumId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                else
                {
                    if (gridReview.Visible == true)
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
        //根据状态加载按钮显隐
        private void LoadDataState()
        {
            simpleButtonShenHe.Enabled = true;
            BtnSave.Enabled = true;
            simpleButtonShengCheng.Enabled = true;
            simpleButtonQingChu.Enabled = true;
            butUpdate.Enabled = true;
       
            //customGridZhenDuan2.Enabled = true;
             
 
            if (_tjlCustomerRegDto == null)
            {
                return;
            }
            //细化总检权限，已总检的总检之间不能互相修改，已审核的审核之间不能互相修改
            //判断是总检状态
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
            //已初审权限处理
            if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview)
            {
                //总检状态是当前操作者所有按钮可操作(某种原因没有总检医生的也可以操作)
                if (!_tjlCustomerRegDto.CSEmployeeId.HasValue || CurrentUser.Id == _tjlCustomerRegDto.CSEmployeeId || IsTrue)
                {
                    BtnSave.Enabled = true;
                    simpleButtonShenHe.Enabled = true;
                    //customGridZhenDuan2.Enabled = true;
                   
                    simpleButtonShengCheng.Enabled = true;
                    butUpdate.Enabled = true;
                }
                else
                {
                    //通过按钮是否显示判断是否有审核权限
                    //没有审核权限则所有按钮不能操作
                    if (simpleButtonShenHe.Visible == false)
                    {
                        BtnSave.Enabled = false;
                        simpleButtonShenHe.Enabled = false;
                        simpleButtonQingChu.Enabled = false;
                        simpleButtonShengCheng.Enabled = false;
                        butUpdate.Enabled = false;

                       
                    }
                    //有审核权限保存总检不能操作其他可操作
                    else
                    {
                        BtnSave.Enabled = false;
                        simpleButtonShenHe.Enabled = true;
                        //customGridZhenDuan2.Enabled = true;
                        
                        simpleButtonShengCheng.Enabled = true;
                        butUpdate.Enabled = true;
                    }

                }

            }
            //如果是审核状态
            else if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
            {
                //高级权限或者是当前审核医生(某种原因没有审核医生的也能操作)
                if (!_tjlCustomerRegDto.FSEmployeeId.HasValue || _tjlCustomerRegDto.FSEmployeeId == CurrentUser.Id || IsTrue)
                {
                    BtnSave.Enabled = false;
                    simpleButtonShenHe.Enabled = false;
                 
                    simpleButtonShengCheng.Enabled = false;
                    butUpdate.Enabled = false;
                    simpleButtonQingChu.Enabled = true;
                }
                else
                {
                    //不是总检人按钮全灰
                    BtnSave.Enabled = false;
                    simpleButtonShenHe.Enabled = false;
                    simpleButtonQingChu.Enabled = false;
                    simpleButtonShengCheng.Enabled = false;
                    butUpdate.Enabled = false;
                    //customGridZhenDuan2.Enabled = false;

                   
                }

            }
            #region 暂时注释
            //if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview || _tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
            //{



            //    //判断是不是总检人
            //    if (CurrentUser.Id == _tjlCustomerRegDto.CSEmployeeId || _tjlCustomerRegDto.FSEmployeeId == CurrentUser.Id || IsTrue)
            //    {
            //        if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
            //        {
            //            BtnSave.Enabled = false;
            //            simpleButtonShenHe.Enabled = false;
            //            simpleButtonAddAd.Enabled = false;
            //            simpleButtonShengCheng.Enabled = false;
            //            butUpdate.Enabled = false;
            //            //已审核但是不是审核医生无权清除
            //            if (!IsTrue && _tjlCustomerRegDto.SummSate == 4 && _tjlCustomerRegDto.FSEmployeeId.HasValue && _tjlCustomerRegDto.FSEmployeeId != CurrentUser.Id)
            //            {
            //                simpleButtonQingChu.Enabled = false;
            //            }
            //            else
            //            { simpleButtonQingChu.Enabled = true; }
            //        }
            //        else
            //        {
            //            BtnSave.Enabled = true;
            //            simpleButtonShenHe.Enabled = true;

            //            customGridZhenDuan2.Enabled = true;

            //            simpleButtonAddAd.Enabled = true;
            //            simpleButtonShengCheng.Enabled = true;
            //            butUpdate.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        //不是总检人按钮全灰
            //        BtnSave.Enabled = false;
            //        simpleButtonShenHe.Enabled = false;
            //        simpleButtonQingChu.Enabled = false;
            //        simpleButtonShengCheng.Enabled = false;
            //        butUpdate.Enabled = false;
            //        //customGridZhenDuan2.Enabled = false;

            //        simpleButtonAddAd.Enabled = false;
            //    }
            //} 
            
            #endregion
             if (NowCusRegInf.SummLocked == (int)SummLockedState.Alr && NowCusRegInf.SummLockEmployeeBMId != CurrentUser.Id)
            {
                //已锁定                
                simpleButtonShengCheng.Enabled = false;
                BtnSave.Enabled = false;
                simpleButtonShenHe.Enabled = false;
                butUpdate.Enabled = false;
                simpleButtonQingChu.Enabled = false;
                var userName = DefinedCacheHelper.GetComboUsers().FirstOrDefault(p => p.Id == NowCusRegInf.SummLockEmployeeBMId);
                MessageBox.Show(_tjlCustomerRegDto.Name + "已被总检人：" + userName?.Name + "锁定，您无权修改！");
            }
            else if (_tjlCustomerRegDto.SummLocked==null || _tjlCustomerRegDto.SummLocked == (int)SummLockedState.Unchecked && (_tjlCustomerRegDto.SummSate == (int)SummSate.NotAlwaysCheck || _tjlCustomerRegDto.SummSate == null))
            {
               
                //解锁建议下拉框        
                //customGridZhenDuan2.Enabled = true;
             
              
            }
            var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
            //基本信息
            var customerRegisterTask =
                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterAppService.GetCustomerRegisterById(
                    input);
            labSumState.Text = SummSateHelper.SummSateFormatter(customerRegisterTask.Result.SummSate);
            // InitForm();

           
        }

        #region 复合诊断
        private List<CustomerRegisterSummarizeSuggestDto> HBSum(List<CustomerRegisterSummarizeSuggestDto> input)
        {
            var HBZDlist = _summarizeAdviceAppService.SearchSumHB();
            foreach (var HBZD in HBZDlist)
            {
                bool isHB = true;
                foreach (var sum in HBZD.Advices)
                {
                    if (!input.Any(p => p.SummarizeAdviceId == sum.Id))
                    {
                        isHB = false;
                    }
                }
                if (isHB == true)
                {


                    var sum = DefinedCacheHelper.GetSummarizeAdvices().FirstOrDefault(p => p.Id == HBZD.SummarizeAdviceId);
                    if (sum != null)
                    {
                        CustomerRegisterSummarizeSuggestDto customerRegister = new CustomerRegisterSummarizeSuggestDto();
                        var sumId = HBZD.Advices.FirstOrDefault().Id;
                        var suminfo = input.FirstOrDefault(p => p.SummarizeAdviceId == sumId);
                        var index = input.IndexOf(suminfo);
                        customerRegister.Id = Guid.NewGuid();
                        customerRegister.Advice = sum.SummAdvice;
                        customerRegister.CustomerRegID = suminfo.CustomerRegID;
                        customerRegister.IsPrivacy = suminfo.IsPrivacy;
                        customerRegister.IsZYB = suminfo.IsZYB;
                        customerRegister.isReview = suminfo.isReview;
                        customerRegister.IsSC = suminfo.IsSC;
                        customerRegister.SummarizeAdviceId = sum.Id;
                        customerRegister.SummarizeName = sum.AdviceName;
                        customerRegister.SummarizeOrderNum = sum.OrderNum;
                        customerRegister.SummarizeType = suminfo.SummarizeType;

                        input.Insert(index, customerRegister);
                        var sumIDlist = HBZD.Advices.Select(p => p.Id).ToList();
                        input.RemoveAll(p => p.SummarizeAdviceId != null && sumIDlist.Contains(p.SummarizeAdviceId.Value));

                    }
                }
            }
            int num = 1;
            foreach (var sum in input)
            {
                sum.SummarizeOrderNum = num;
                num = num + 1;
            }
            return input;
        }
        #endregion
        private void simpleButtonQingChu_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto.SummSate == (int)SummSate.Audited)
            {
                var IsTrue = MessageBox.Show("是否要清除总检审核。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (!IsTrue)
                    return;
                //if (gridControl2.DataSource is List<CustomerRegisterDepartmentSummaryDto> field科室小结汇总)
                //{
                //    //小结和体检人不匹配提示
                //    if (field科室小结汇总.Count > 0 && field科室小结汇总[0].CustomerRegId != _tjlCustomerRegDto.Id)
                //    {
                //        MessageBox.Show("当前体检人和科室信息不匹配请刷新后重试！");
                //        return;
                //    }
                //}
                //更新建议汇总
                if (customerRegisterSummarizeDto != null)
                {
                    customerRegisterSummarizeDto.CheckState = 1;
                    customerRegisterSummarizeDto.ShEmployeeBMId = null;
                    _inspectionTotalService.SaveSummarize(customerRegisterSummarizeDto);
                }




                //更新患者体检信息表
                INCusSumSateDto iNCusSumSateDto = new INCusSumSateDto();
                iNCusSumSateDto.CSEmployeeId = _tjlCustomerRegDto.CSEmployeeId;
                iNCusSumSateDto.Id = _tjlCustomerRegDto.Id;
                iNCusSumSateDto.SummLocked = _tjlCustomerRegDto.SummLocked;
                //上面是不修改的字段

                iNCusSumSateDto.FSEmployeeId = null;
                string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 6)?.Remarks;
                if (strwjshow != null && strwjshow.Contains("Y"))
                {
                    iNCusSumSateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                }
                else
                {
                    iNCusSumSateDto.SummSate = (int)SummSate.HasInitialReview;
                }

                _inspectionTotalService.UpdateCusSumSate(iNCusSumSateDto);
                //更新当前人
                _tjlCustomerRegDto.FSEmployeeId = iNCusSumSateDto.FSEmployeeId;
                _tjlCustomerRegDto.SummSate = iNCusSumSateDto.SummSate;
                _tjlCustomerRegDto.CSEmployeeId = iNCusSumSateDto.CSEmployeeId;
                _tjlCustomerRegDto.SummLocked = iNCusSumSateDto.SummLocked;



                LoadDataState();
                alertInfo.Show(this, "提示!", "已清除总检审核。");
            }
            else if (_tjlCustomerRegDto.SummSate == (int)SummSate.HasInitialReview)
            {
                var IsTrue = MessageBox.Show("是否要清除总检。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (!IsTrue)
                    return;
                //更新建议汇总
                if (customerRegisterSummarizeDto != null)
                {
                    customerRegisterSummarizeDto.CheckState = 1;
                    customerRegisterSummarizeDto.EmployeeBMId = null;
                    _inspectionTotalService.SaveSummarize(customerRegisterSummarizeDto);
                }
                //更新患者体检信息表
                INCusSumSateDto iNCusSumSateDto = new INCusSumSateDto();
                iNCusSumSateDto.CSEmployeeId = _tjlCustomerRegDto.CSEmployeeId;
                iNCusSumSateDto.Id = _tjlCustomerRegDto.Id;
                iNCusSumSateDto.SummLocked = _tjlCustomerRegDto.SummLocked;


                //上面是不修改的字段

                iNCusSumSateDto.FSEmployeeId = null;
                iNCusSumSateDto.SummSate = (int)SummSate.NotAlwaysCheck;

                _inspectionTotalService.UpdateCusSumSate(iNCusSumSateDto);
                //更新当前人
                _tjlCustomerRegDto.FSEmployeeId = iNCusSumSateDto.FSEmployeeId;
                _tjlCustomerRegDto.SummSate = iNCusSumSateDto.SummSate;
                _tjlCustomerRegDto.CSEmployeeId = iNCusSumSateDto.CSEmployeeId;
                _tjlCustomerRegDto.SummLocked = iNCusSumSateDto.SummLocked;

                LoadDataState();
                ShowMessageSucceed("已清除总检。");
            }
            else
            {
                var IsTrue = MessageBox.Show("是否要清除总检数据。", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (!IsTrue)
                    return;



                //更新患者体检信息表
                INCusSumSateDto iNCusSumSateDto = new INCusSumSateDto();
                iNCusSumSateDto.Id = _tjlCustomerRegDto.Id;

                //上面是不修改的字段

                iNCusSumSateDto.SummSate = (int)SummSate.NotAlwaysCheck;
                iNCusSumSateDto.SummLocked = 2;
                iNCusSumSateDto.CSEmployeeId = null;
                iNCusSumSateDto.FSEmployeeId = null;

                _inspectionTotalService.UpdateCusSumSate(iNCusSumSateDto);
                //更新当前人
                _tjlCustomerRegDto.FSEmployeeId = iNCusSumSateDto.FSEmployeeId;
                _tjlCustomerRegDto.SummSate = iNCusSumSateDto.SummSate;
                _tjlCustomerRegDto.CSEmployeeId = iNCusSumSateDto.CSEmployeeId;
                _tjlCustomerRegDto.SummLocked = iNCusSumSateDto.SummLocked;


                //删除建议表（多条）
                _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id, IsZYB = 2 });
                //删除建议汇总表 （单条）
                _inspectionTotalService.DelTjlCustomerSummarize(new TjlCustomerQuery() { CustomerRegID = _tjlCustomerRegDto.Id, IsZYB = 2 });

                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = _tjlCustomerRegDto.CustomerBM;
                createOpLogDto.LogName = _tjlCustomerRegDto.Name;
                createOpLogDto.LogText = "清除总检数据";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.SumId;
                _commonAppService.SaveOpLog(createOpLogDto);
                customerRegisterSummarizeDto = null;
                //重新加载界面
                LoadDataState();
                //清空建议列表

                memoEditHuiZong.Text = string.Empty;
                richAD.Text = string.Empty;
                //treeListZhenDuan.DataSource = null;
                //treeListZhenDuan.RefreshDataSource();


                //撤销报告
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1).Remarks;
                if (HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
                    if (HISName == "东软")
                    {
                        NeusoftInterface.NeInterface neInterface = new NeusoftInterface.NeInterface();
                        neInterface.CancelRepAsync(_tjlCustomerRegDto.CustomerBM, CurrentUser.EmployeeNum, CurrentUser.Name);

                    }
                }
                ShowMessageSucceed("已清除总检。");
            }

        }

      

        private void butUpdate_Click(object sender, EventArgs e)
        {


            //simpleButtonShengCheng.PerformClick();

            var IsYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 22)?.Remarks;
            if (!string.IsNullOrEmpty(IsYC) && IsYC == "Y")
            {

                makeSum(0);

            }
            else
            {
                //获取原有建议
                var OldSum = getStrToDR(richAD.Text);// treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;

                if (OldSum == null)
                {
                    OldSum = new List<CustomerRegisterSummarizeSuggestDto>();
                }
                makeSum(2);
                //获取最新生成的建议
                var _CustomerSummarizeList = getStrToDR(richAD.Text); //treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;

                if (_CustomerSummarizeList.Count() > 0)
                {

                    var Adnames = OldSum.Select(o => o.SummarizeName);
                    _CustomerSummarizeList = _CustomerSummarizeList.Where(o => !Adnames.Contains(o.SummarizeName)).ToList();
                    OldSum.AddRange(_CustomerSummarizeList);
                    OldSum = OldSum.OrderBy(l => l.SummarizeOrderNum).ToList();
                    for (int i = 0; i < OldSum.Count(); i++)
                    {

                        OldSum[i].SummarizeOrderNum = i + 1;

                    }
                    richAD.Text = AdDrTOStr(OldSum.OrderBy(l => l.SummarizeOrderNum).ToList());
                    //treeListZhenDuan.DataSource = OldSum.OrderBy(l => l.SummarizeOrderNum).ToList();
                    //treeListZhenDuan.RefreshDataSource();

                }
            }

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }      
    
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //确保文本在文本框中已经选定 
            //  if (memoEditHuiZong.selec > 0)
            // 复制文本到剪贴板
            memoEditHuiZong.Copy();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ////  // 确保当前文本框中有选定
            //  if (memoEditHuiZong.SelectedText != "")
            // 剪切选定的文本至剪贴板
            memoEditHuiZong.Cut();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
            alertInfo.Show(this, "复制提示", $"“{labelControl.Text}”已复制到剪贴板！");
        }

        private void butASum_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //加载默认查询条件
            string fp = System.Windows.Forms.Application.StartupPath + "\\NInspection.json";

            List<Search> searchlist = new List<Search>();


            if (!string.IsNullOrWhiteSpace(comCheckType.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "CheckType";
                search.Text = comCheckType.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(comState.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "CheckState";
                search.Text = comState.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(comSumSate.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "SumState";
                search.Text = comSumSate.EditValue?.ToString();
                searchlist.Add(search);
            }
            if (!string.IsNullOrWhiteSpace(textDay.EditValue?.ToString()))
            {
                Search search = new Search();
                search.Name = "Day";
                search.Text = textDay.EditValue?.ToString();
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

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void butsearchHis_Click(object sender, EventArgs e)
        {


            //小结和体检人不匹配提示
            if (_tjlCustomerRegDto != null && _tjlCustomerRegDto.Id != Guid.Empty)
            {
                SearchClass searchClass = new SearchClass();
                SearchHisClassDto searchHisClassDto = new SearchHisClassDto();
                searchHisClassDto.IDCardNo = _tjlCustomerRegDto.IDCardNo;
                searchClass.CustomerId = _tjlCustomerRegDto.CustomerId;
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
        /// 历史对比刷新
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

        private void gridView7_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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

        private void gridView7_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void hideContainerBottom_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel1_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            dockPanel1.HideSliding();
            //Point screenPoint = Control.MousePosition;
            //SetCursorPos(screenPoint.X, screenPoint.Y-100);
            //treeListZhenDuan.Select();

            //treeListZhenDuan.Focus();
            e.Cancel = true;

        }

        private void dockPanel1_ClosedPanel(object sender, DockPanelEventArgs e)
        {

        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        private static void SetPos()
        {
            int dx = 1000;
            int dy = 100;
            SetCursorPos(dx, dy);
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

        private void memoEditHuiZong_MouseUp(object sender, MouseEventArgs e)
        {
            var selectText = memoEditHuiZong.Document.GetText(this.memoEditHuiZong.Document.Selection);
            if (selectText.Length > 0)
            {
                //var advicelist = DefinedCacheHelper.GetSummarizeAdvices().Where(
                //    p => p.AdviceName.Contains(selectText) || p.Advicevalue.Contains(selectText)).ToList();
                //gridSelectAdvice.DataSource = advicelist;
                //richTextBoxAdvice.Text = selectText.Trim();
                var selectAdvice = selectText.Trim();

                if (!string.IsNullOrEmpty(selectAdvice))
                {
                    var advicelist = DefinedCacheHelper.GetSummarizeAdvices().Where(
                        p => p.AdviceName.Contains(selectAdvice) || p.Advicevalue.Contains(selectAdvice)).
                        OrderBy(p => p.OrderNum).ToList();
                    //gridSelectAdvice.DataSource = advicelist;

                }

            }
        }

       
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks== 1 && e.RowHandle >= 0)
            {
                #region 清除上一个人的锁定状态
                //if (NowCusRegInf != null && NowCusRegInf.Id != Guid.Empty && NowCusRegInf.SummLocked == (int)SummLockedState.Alr
                //    && NowCusRegInf.SummSate != (int)SummSate.Audited && NowCusRegInf.SummLockEmployeeBMId == CurrentUser.Id)
                //{
                //    #region 总检解除锁定
                //    // UpdateCuslockSate(LockStateDto dto)
                //    LockStateDto LockState = new LockStateDto();
                //    LockState.Id = NowCusRegInf.Id;
                //    LockState.SummLocked = (int)SummLockedState.Unchecked;
                //    LockState.SummLockEmployeeBMId = null;
                //    _inspectionTotalService.UpdateCuslockSate(LockState);
                //    #endregion
                //}
                #endregion
                // 加载人员对应总检数据
                //gridControl2.DataSource = null;
                gridControl3.DataSource = null;
                tvJianChaXiangMu.Nodes.Clear();
                txtCusBM.ResetText();
                labName.ResetText();
                labSex.ResetText();
                labAge.ResetText();
                labSumState.ResetText();
                labClient.ResetText();
                //labSuit.ResetText();
                //labType.ResetText();
                labLoginDate.ResetText();
                _currentItemPictures = null;
                customerRegisterSummarizeDto = null;
                _tjlCustomerRegDto = null;
                memoEditHuiZong.ResetText();
                TS = "";
                Wjz = "";
                gridView3.ActiveFilter.Remove(gridColumn34);
                //treeListZhenDuan.DataSource = null;

                if (gridViewCusReg.IsDataRow(gridViewCusReg.FocusedRowHandle))
                {
                    if (gridViewCusReg.GetFocusedRowCellValue(conId) is Guid id)
                    {
                        TS = "";
                        Wjz = "";

                        var input = new EntityDto<Guid>(id);
                        CustomerRegister1Dto cureglins = null;
                        AutoLoading(() =>
                        {
                            //科室小结
                            var customerRegisterDepartmentSummaryTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterDepartmentSummaryAppService
                                .GetCustomerRegisterDepartmentSummary(input);
                            currCusDeptSum = customerRegisterDepartmentSummaryTask.Result;

                            //检查结果
                            var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                                .GetCustomerRegisterItem(input);

                            currCusRegGroupItems = customerRegisterItemTask.Result;
                            //基本信息
                            var customerRegisterTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterAppService.GetCustomerRegisterById(
                                    input);
                            //图片
                            var itemPictureTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemPictureAppService
                                    .GetItemPictureByCustomerRegisterId(input);
                            //总检
                            var customerRegisterSummarizeTask =
                                DefinedCacheHelper.DefinedApiProxy.CustomerRegisterSummarizeAppService
                                    .GetSummarizeByCustomerRegisterId(input);
                            //建议
                            var customerRegisterSummarizeSuggestTask = DefinedCacheHelper.DefinedApiProxy
                                .CustomerRegisterSummarizeAppService.GetSummarizeSuggestByCustomerRegisterId(input);

                            #region 冲突提示
                            SumAdviseDto sumAdviseDto = new SumAdviseDto();
                            sumAdviseDto.Sex = customerRegisterTask.Result.Sex;
                            sumAdviseDto.Age = customerRegisterTask.Result.RegAge;
                            sumAdviseDto.CharacterSummary = customerRegisterSummarizeTask.Result?.CharacterSummary;
                            sumAdviseDto.Advice = customerRegisterSummarizeSuggestTask.Result;
                            var strMess = _inspectionTotalService.MatchSumConflict(sumAdviseDto);
                            if (strMess.OutMess != "")
                            {
                                XtraMessageBox.Show(strMess.OutMess);
                            }
                            #endregion
                            //屏蔽职业检数据
                            var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                            gridControl3.DataSource = cusgrouplist;
                            //组合小结
                            _CustomerRegInspectGroupDto = _inspectionTotalService.GetCusGroupList(new QueryClass { CustomerRegId = id }).ToList();

                            BinDingDataTreeS(customerRegisterItemTask.Result, Guid.Empty);

                            ////筛选显示异常
                            //if (checkEdit4.Checked)
                            //{
                            //    gridView3.ActiveFilter.Add(gridColumn30,
                            //        new ColumnFilterInfo("[Symbol] IS NOT NULL AND [Symbol] <> '' AND [Symbol] <> 'M'"));
                            //}
                            //else
                            //{
                            //    gridView3.ActiveFilter.Remove(gridColumn30);
                            //}
                            ////筛选显示已检
                            //if (checkEdit3.Checked)
                            //{
                            //    gridView3.ActiveFilter.Add(gridColumn31,
                            //        new ColumnFilterInfo("[ProcessState] IS NOT NULL AND [ProcessState] <> 2  AND [ProcessState] <> 3"));
                            //}
                            //else
                            //{
                            //    gridView3.ActiveFilter.Add(gridColumn31,
                            //        new ColumnFilterInfo("[ProcessState] IS NOT NULL AND [ProcessState] == 2  or  [ProcessState] == 3"));
                            //}
                            //未检个数
                            var NoCount = customerRegisterItemTask.Result.Where(o => o.ProcessState != null && o.ProcessState != (int)ProjectIState.Complete
                            && o.ProcessState != (int)ProjectIState.Part).Count();
                            if (NoCount > 0)
                            {
                                checkEdit3.Text = "未检项目(" + NoCount + "个)";
                                checkEdit3.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                checkEdit3.Text = "未检项目";
                                checkEdit3.ForeColor = System.Drawing.Color.Black;
                            }

                            var customerRegister = customerRegisterTask.Result;
                            NowCusRegInf = customerRegister;
                            if (customerRegister != null && customerRegister.Id != Guid.Empty && (customerRegister.SummLocked == null || customerRegister.SummLocked == (int)SummLockedState.Unchecked))
                            {
                                #region 总检锁定
                                var IsSD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 21)?.Remarks;
                                if (!string.IsNullOrEmpty(IsSD) && IsSD == "Y")
                                {
                                    // UpdateCuslockSate(LockStateDto dto)
                                    LockStateDto LockState = new LockStateDto();
                                    LockState.SummLocked = (int)SummLockedState.Alr;
                                    LockState.SummLockEmployeeBMId = CurrentUser.Id;
                                    LockState.Id = customerRegister.Id;
                                    _inspectionTotalService.UpdateCuslockSate(LockState);
                                }
                                #endregion
                            }
                            cureglins = customerRegister;
                            txtCusBM.Text = customerRegister.CustomerBM;
                            labName.Text = customerRegister.Name;
                            labSex.Text = SexHelper.CustomSexFormatter(customerRegister.Sex);
                            labAge.Text = customerRegister.RegAge?.ToString();
                            labSumState.Text = SummSateHelper.SummSateFormatter(customerRegister.SummSate);
                            labClient.Text = customerRegister.ClientName;
                            //labSuit.Text = customerRegister.ItemSuitName;
                            //labelFPNo.Text = customerRegister.FPNo;


                            if (customerRegister.PhysicalType.HasValue)
                            {
                                var checkstate = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ExaminationType.ToString()
                                 && o.Value == customerRegister.PhysicalType)?.Text;
                                //labType.Text = checkstate;
                            }
                            labLoginDate.Text = customerRegister.LoginDate?.ToString("d");

                            _currentItemPictures = itemPictureTask.Result;

                            var customerRegisterSummarize = customerRegisterSummarizeTask.Result;
                            if (customerRegisterSummarize != null)
                            {
                                // 绑定总检汇总数据
                                customerRegisterSummarizeDto = customerRegisterSummarize;
                                memoEditHuiZong.Text = customerRegisterSummarize.CharacterSummary;

                            }
                            else
                            {
                                memoEditHuiZong.Text = "";
                                richAD.Text = "";
                                customerRegisterSummarizeDto = null;
                            }
                            //绑定建议
                            _tjlCustomerRegDto = (OutCusListDto)gridViewCusReg.GetFocusedRow();
                            #region 赋值
                            _tjlCustomerRegDto.SummSate = NowCusRegInf.SummSate;
                            _tjlCustomerRegDto.CSEmployeeId = NowCusRegInf.CSEmployeeId;
                            _tjlCustomerRegDto.FSEmployeeId = NowCusRegInf.FSEmployeeId;
                            _tjlCustomerRegDto.SummLockEmployeeBMId = NowCusRegInf.SummLockEmployeeBMId;
                            _tjlCustomerRegDto.SummLocked = NowCusRegInf.SummLocked;
                            #endregion
                            //加载建议
                            int setnot = (int)Sex.GenderNotSpecified;
                            int setUn = (int)Sex.Unknown;
                            //根据性别过滤建议
                     //       string sql = "[SexState] ==" + setnot + " or [SexState] ==" + setUn + " or [SexState]==" + _tjlCustomerRegDto.Sex + "";
                     //       gridView5.ActiveFilter.Add(conSex,
                     //   new ColumnFilterInfo(sql));
                     //       gridView5.ActiveFilter.Add(conMaxAge,
                     //    new ColumnFilterInfo("[MaxAge] >=" + _tjlCustomerRegDto.Age + ""));
                     //       gridView5.ActiveFilter.Add(conMinAge,
                     //new ColumnFilterInfo("[MinAge] <=" + _tjlCustomerRegDto.Age + ""));

                            //treeListZhenDuan.DataSource = customerRegisterSummarizeSuggestTask.Result.Where(p => p.IsZYB != 1).OrderBy(r => r.SummarizeOrderNum).ToList();
                            richAD.Text = AdDrTOStr(customerRegisterSummarizeSuggestTask.Result.Where(p => p.IsZYB != 1).OrderBy(r => r.SummarizeOrderNum).ToList());
                            string csD = _tjlCustomerRegDto.CSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().Find(n => n.Id == _tjlCustomerRegDto.CSEmployeeId).Name;
                            string fsD = _tjlCustomerRegDto.FSEmployeeId == null ? "" : DefinedCacheHelper.GetComboUsers().Find(n => n.Id == _tjlCustomerRegDto.FSEmployeeId).Name;
                            //labelShenHeYiSheng.Text = csD + "-" + fsD;
                            LoadDataState();
                            #region 提示
                            //未小结科室
                            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                            var Depatlist = customerRegisterDepartmentSummaryTask.Result.Select(o => o.DepartmentId).Distinct().ToList();
                            var Dpar = customerRegisterItemTask.Result.Where(o => !Depatlist.Contains(o.DepartmentId)).ToList();

                            if (Dpar.Count > 0)
                            {
                                var depNoIds = Dpar.Select(o => new { o.DepartmentId, o.ProcessState }).Distinct().ToList();
                                var depNo = depNoIds.GroupBy(o => o.DepartmentId).Select(o => new CustomerRegisterDepartmentSummaryDto
                                {
                                    CustomerRegId = input.Id,
                                    DepartmentId = o.Key,
                                    DagnosisSummary = o.Where(r => r.ProcessState == (int)ProjectIState.GiveUp).Count() == 0 ? "未小结" :
                                        o.Where(r => r.ProcessState != (int)ProjectIState.GiveUp).Count() > 0 ? "未小结（部分放弃）" : "未小结（全部放弃）",
                                }
                                  ).Distinct().ToList();
                                customerRegisterDepartmentSummaryTask.Result.AddRange(depNo);
                                var depart = Dpar.Select(o => o.DepartmentId).ToList();
                                var deparNames = DefinedCacheHelper.GetDepartments().Where(o => depart.Contains(o.Id)).Select(o => o.Name).ToList();
                                if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                                {
                                    var departNames = _CustomerRegInspectGroupDto.Where(p => p.IsZYB != 1).Select(p => p.DepartmentName).Distinct().ToList();
                                    deparNames = deparNames.Where(p => departNames.Contains(p)).ToList();
                                }

                                TS += string.Join(",", deparNames).TrimEnd(',') + "，未生成科室小结，总检汇总中将不显示该科室信息\r\n";
                            }
                            //未检
                            var NoGroup = customerRegisterItemTask.Result.Where(o => o.ProcessState == (int)ProjectIState.Not || o.ProcessState == (int)ProjectIState.Part).ToList();


                            if (_tjlCustomerRegDto.PhysicalType.HasValue && tjlb != null && tjlb.Contains("职业+健康"))
                            {
                                var groupsIds = _CustomerRegInspectGroupDto.Where(p => p.IsZYB != 1).Select(p => p.ItemGroupBM_Id).ToList();
                                NoGroup = NoGroup.Where(p => groupsIds.Contains(p.ItemGroupBMId)).ToList();
                            }

                            if (NoGroup.Count > 0)
                            {
                                var grouIDs = NoGroup.Select(o => o.ItemGroupBMId).Distinct().ToList();
                                var NoGroupName = DefinedCacheHelper.GetItemGroups().Where(o => grouIDs.Contains(o.Id)).Select(o => o.ItemGroupName).ToList();
                                TS += string.Join(",", NoGroupName).TrimEnd(',') + "，未检或部分检查\r\n";
                            }
                            //危急值
                            var CrisisGroup = customerRegisterItemTask.Result.Where(n => n.CrisisSate == (int)CrisisSate.Abnormal).ToList();
                            if (CrisisGroup.Count > 0)
                            {
                                var ITemIds = CrisisGroup.Select(o => o.ItemId).ToList();

                                var ItemNames = DefinedCacheHelper.GetItemInfos().Where(o => ITemIds.Contains(o.Id)).Select(o => o.Name).ToList();
                                Wjz = string.Join(",", ItemNames).TrimEnd(',') + "，存在重要异常结果\r\n";
                            }
                            if (TS != "")
                            {
                                alertInfo.Show(this, "提示!", TS);
                            }
                            if (Wjz != "")
                            {
                                // alertInfo.Show(this, "提示!", Wjz);
                                MessageBox.Show(Wjz);
                                //ShowMessageSucceed(Wjz);
                            }
                            #endregion
                            //过滤职业科室小结
                            //var deparIDlist = cusgrouplist.Select(p => p.DepartmentId).Distinct().ToList();
                            //gridControl2.DataSource = customerRegisterDepartmentSummaryTask.Result.Where(
                            //    p => deparIDlist.Contains(p.DepartmentId.Value)).ToList();

                            if (_tjlCustomerRegDto.PhysicalType.HasValue)
                            {
                                // var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                                // var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                                if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("健康证")))
                                {
                                    tabbedControlGroup2.SelectedTabPageIndex = 1;
                                    layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    //layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    radioQualified.EditValue = customerRegisterSummarizeDto?.Qualified;
                                    richEditOpinion.Text = customerRegisterSummarizeDto?.Opinion;
                                }
                                else
                                {
                                    tabbedControlGroup2.SelectedTabPageIndex = 0;
                                    //layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                    layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                }
                            }
                            #region 复查显示
                            EntityDto<Guid> reviewinput = new EntityDto<Guid>();
                            reviewinput.Id = _tjlCustomerRegDto.Id;
                            var review = _inspectionTotalService.GetCusReViewDto(input);
                            if (review.Count > 0)
                            {
                                layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                gridReview.DataSource = review;
                                gridReview.Visible = true;
                            }
                            else
                            {
                                layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                gridReview.Visible = false;
                            }
                            #endregion
                        });
                        //参数控制体检完成，未总检自动生成总检
                        var AutoSum = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 14);

                        if (AutoSum != null && AutoSum.Remarks == "Y" && cureglins != null)
                        {

                            if (cureglins.SummSate == (int)SummSate.NotAlwaysCheck &&
                                cureglins.CheckSate == (int)PhysicalEState.Complete)
                            {
                                //未总检但有总检数据只更新汇总(yhh修改有汇总数据不更新汇总0609)
                                if (customerRegisterSummarizeDto != null)
                                {
                                    //butUpdate.PerformClick();
                                }
                                else
                                {

                                    Task.Run(() => makeSum());
                                }

                            }
                        }
                    }
                }
            }
        }

        private void butInterfase_Click(object sender, EventArgs e)
        {

            //水平进度条

            var selectIndexes = gridViewCusReg.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                string errarm = "";
                int OKcusCount = 0;
                int ErrcusCount = 0;
                string ErrArch = "";
                AutoLoading(() =>
                {
                    foreach (var index in selectIndexes)
                    {
                        var rowData = (OutCusListDto)gridViewCusReg.GetRow(index);

                        if (rowData != null)
                        {


                            //体检人信息

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
                        }

                    }
                });
                if (errarm != "")
                {
                    XtraMessageBox.Show("失败上传：" + ErrcusCount + "条数据，体检号：" + ErrArch + "\r\n异常详情：" + errarm
                        );

                }
                else
                {
                    XtraMessageBox.Show("获取数据：" + OKcusCount + "条！");
                }
            }

        }

     
        private void GridButtonChaKanTuPian_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            // 显示图片
            if (e.Button.Tag is PictureArg arg)
            {
                using (var frm = new InspectionTotalPicture(arg))
                {
                    frm.ShowDialog(this);
                }
            }

        }

        private void pictureEditRight_Click(object sender, EventArgs e)
        {
            //if (CustomerItemPicSys != null && CustomerItemPicSys.Count != 0)
            //{
            //    if (CustomerPicSys == CustomerItemPicSys.Count - 1)
            //        CustomerPicSys = 0;
            //    else
            //        CustomerPicSys = CustomerPicSys + 1;
            //    var Pic = Guid.Parse(CustomerItemPicSys[CustomerPicSys].PictureBM.ToString());
            //    var result = _pictureController.GetUrl(Pic);
            //    pictureEditImg.Image = ImageHelper.GetUriImage(new Uri(result.RelativePath));

            //    var ItemGroup = Guid.Parse(CustomerItemPicSys[CustomerPicSys].CustomerItemGroupID.ToString());
            //    labelControlDangQian.Text = (CustomerPicSys + 1).ToString();
            //}
        }

        private void txtCusBM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtCusBM.Text))
            {
                var txt = txtCusBM.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt))
                {

                    LoadData(txt);

                }
            }
        }

        private void textName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textName.Text))
            {
                var txt = textName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt))
                {

                    LoadData();

                }
            }
        }

        private void textDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textDay.Text))
            {
                var txt = textName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txt))
                {

                    LoadData();

                }
            }
        }

        private void dockPanel2_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            dockPanel2.HideSliding();
            //Point screenPoint = Control.MousePosition;
            //SetCursorPos(screenPoint.X, screenPoint.Y - 100);
            //treeListZhenDuan.Select();

            //treeListZhenDuan.Focus();
            e.Cancel = true;
        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 0)
                {

                    frmCheckItem frmCheckItem = new frmCheckItem();
                    frmCheckItem.isadd = "1";

                    if (frmCheckItem.ShowDialog() == DialogResult.OK)
                    {
                        var ReViewdt = gridReview.DataSource as List<CusReViewDto>;
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

        private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
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

        private void treeListZhenDuan_DataSourceChanged(object sender, EventArgs e)
        {
            var _CustomerSummarizeList = getStrToDR(richAD.Text);// treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
            if (_CustomerSummarizeList != null)
            {
                var word = _CustomerSummarizeList.Where(p => p.IsSC == 1).Select(p => p.Advice + p.SummarizeName).ToList();
                var wordstr = string.Join("", word);
                //labelControl7.Text = "建议字数：" + wordstr.Length.ToString();
            }
        }

        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void repositoryItemCheckEdit3_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void repositoryItemCheckEdit3_Click(object sender, EventArgs e)
        {


        }

        private void repositoryItemCheckEdit3_MouseUp(object sender, MouseEventArgs e)
        {


        }

        private void treeListZhenDuan_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

            var _CustomerSummarizeList = getStrToDR(richAD.Text); //treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
            var word = _CustomerSummarizeList.Where(p => p.IsSC == 1).Select(p => p.Advice + p.SummarizeName).ToList();
            var wordstr = string.Join("", word);
            //labelControl7.Text = "建议字数：" + wordstr.Length.ToString();
            //labelControl7.Refresh();
        }

        private void treeListZhenDuan_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

            //var _CustomerSummarizeList = treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
            //var word = _CustomerSummarizeList.Where(p => p.IsSC == 1).Select(p => p.Advice + p.SummarizeName).ToList();
            //var wordstr = string.Join("", word);
            //labelControl7.Text = "建议字数：" + wordstr.Length.ToString();
            //labelControl7.Refresh();
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu2.ShowPopup(MousePosition);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            setWJZ(1);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setWJZ(2);
        }
        int newleve = 1;
        Guid GroupId;

        private void setWJZ(int leve)
        {
            newleve = leve;

            var cusGroup = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();
            GroupId = cusGroup.Id;
            //if (cusGroup != null && cusGroup.ProcessState != null &&
            //    cusGroup.CrisisSate == (int)CrisisSate.Abnormal)
            //{
            //    var itemName = cusGroup.ItemName;
            //    alertInfo.Show(this, "温馨提示", $"“{itemName}”此项目已是危急项目，请勿重复设置!");
            //    return;
            //}

            if (cusGroup.Id != null)
            {
                string pass = "";
                ZYYC(cusGroup.CrisiChar, selectrxt, cusGroup.CrisiContent, cusGroup.ItemGroupBM.ItemGroupName);
                dockZYYC.Show();
                #region MyRegion

                //frmIllContent frmIll = new frmIllContent();
                //if (!string.IsNullOrEmpty(selectrxt))
                //{
                //    frmIll.IllContent = selectrxt;
                //}
                //frmIll.OldeContent = cusGroup.CrisiChar;

                //if (frmIll.ShowDialog() == DialogResult.OK)
                //{
                //    pass = frmIll.IllContent;
                //}
                //else
                //{
                //    return;
                //}



                //var Query = new UpdateClass();
                //Query.CustomerItemId = cusGroup.Id;
                //Query.CrisisSate = (int)CrisisSate.Abnormal;
                //Query.CrisisLever = leve;
                //Query.CrisiChar = pass;
                //Query.CrisisVisitSate = (int)CrisisVisitSate.Yes;
                //修改状态返回数据
                //OutStateDto trGroup = new OutStateDto();
                //try
                //{
                //    trGroup = _doctorStationAppService.UpdateCrisisSate(Query);
                //    if (trGroup.IsOK == false)
                //    {
                //        XtraMessageBox.Show(trGroup.ErrMess);
                //        return;
                //    }
                //}
                //catch (UserFriendlyException ex)
                //{
                //    ShowMessageBox(ex);
                //    return;
                //}

                //if (trGroup.IsOK == true)
                //{
                //    var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
                //    //检查结果
                //    var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                //            .GetCustomerRegisterItem(input);
                //    //过滤职业检项目
                //    gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                //    gridControl3.RefreshDataSource();
                //    gridControl3.Refresh();
                //} 
                #endregion
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region 重要异常
        private void ZYYC(string OldeContent, string IllContent, string Oldsm, string GroupName)
        {
            memoSM.Text = "";
            textIll.Text = "";
            dockZYYC.Text = GroupName + "-" + "重要异常";
            if (!string.IsNullOrEmpty(Oldsm))
            {
                memoSM.Text = Oldsm;
            }

            if (!string.IsNullOrEmpty(OldeContent))
            {
                textIll.Text = OldeContent;
            }
            if (!string.IsNullOrEmpty(IllContent))
            {
                if (string.IsNullOrEmpty(textIll.Text))
                { textIll.Text = IllContent; }
                else
                {
                    var cotentlist = textIll.Text.Split('|').ToList();
                    if (!cotentlist.Contains(IllContent))
                    {
                        textIll.Text += "\r\n" + IllContent;
                    }
                }
            }
        }
        private void butOK_Click(object sender, EventArgs e)
        {
            var cusGroup = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();

            var Query = new UpdateClass();
            Query.CustomerItemId = cusGroup.Id;
            Query.CrisisSate = (int)CrisisSate.Abnormal;
            Query.CrisisLever = newleve;
            Query.CrisiChar = textIll.Text;
            Query.CrisiContent = memoSM.Text;
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
                var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                        .GetCustomerRegisterItem(input);
                //过滤职业检项目
                gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                gridControl3.RefreshDataSource();
                gridControl3.Refresh();
                memoSM.Text = "";
                textIll.Text = "";
                dockZYYC.Hide();
            }
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            dockZYYC.Hide();
            memoSM.Text = "";
            textIll.Text = "";
        }
        #endregion
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var cusGroup = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();

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
                    //屏蔽职业检数据
                    gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.RefreshDataSource();
                    gridControl3.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void hideContainerLeft_Click(object sender, EventArgs e)
        {

        }

        private void treeListZhenDuan_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {

        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            //GridHitInfo hitInfo = gridView2.CalcHitInfo(e.ControlMousePosition);

            //if (hitInfo.RowHandle < 0 || hitInfo.Column == null || hitInfo.HitTest != GridHitTest.RowCell)
            //{
            //    toolTipController1.HideHint();
            //    return;
            //}

            ////var row = gridView2.GetDataRow(hitInfo.RowHandle);    //如果是DiscountRate列 就显示自定义的tooltip
            //var row = gridView2.GetRow(hitInfo.RowHandle) as CustomerRegisterDepartmentSummaryDto;
            //if (hitInfo.Column.FieldName == "DagnosisSummary" && row != null)
            //{
            //    var rate = row.DagnosisSummary;
            //    e.Info = new ToolTipControlInfo("提示", rate);

            //}
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                //水平进度条
                progressBarControl1.Properties.Minimum = 0;


                var selectIndexes = gridViewCusReg.GetSelectedRows();
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

                        // var tjzt = (string)gridViewCusReg.GetRowCellValue(index, conCheckSate);

                        var tjztsate = gridViewCusReg.GetRowCellDisplayText(index, conCheckSate);
                        var sumsate = gridViewCusReg.GetRowCellDisplayText(index, conSummSate);
                        //var tjzt = CheckSateHelper.PhysicalEStateFormatter(tjztsate);
                        //体检完成未总检才能自动总检

                        if (tjztsate == "3" && sumsate == "未总检")
                        {
                            //生成总检
                            var cusId = (Guid)gridViewCusReg.GetRowCellValue(index, conId);

                            var cusAge = (int)gridViewCusReg.GetRowCellValue(index, conAge);
                            //体检人信息
                            var rowData = (OutCusListDto)gridViewCusReg.GetRow(index);
                            var cusSex = rowData.Sex;
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
                            //string advise = "";
                            if (sum == "")
                            {

                                sum = "* 本次体检所检项目未发现明显异常。";
                                string strSum = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 22)?.Remarks;
                                if (!string.IsNullOrEmpty(strSum))
                                {
                                    sum = strSum;
                                }
                                if (rowData.PhysicalType.HasValue)
                                {
                                    var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == "ExaminationType");
                                    var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;
                                    if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("健康证")))
                                    {

                                        _customerSummarizeDto.Qualified = "合格";
                                        //_customerSummarizeDto.Opinion = richEditOpinion.Text;
                                    }
                                }

                            }
                            var ad = DefinedCacheHelper.GetSummarizeAdvices().ToList();
                            _summarizeAdviceFull = ad.Where(o => o.SexState == cusSex || o.SexState == (int)Sex.GenderNotSpecified || o.SexState == (int)Sex.Unknown).
                  Where(o => o.MaxAge >= cusAge
                  && o.MinAge <= cusAge).ToList();
                            //匹配建议

                            _CustomerSummarizeList = MatchingAdvice(sum, cusId, _CustomerSummarizeList);

                            //保存总检

                            //var nowdate = _inspectionTotalService.Transformation(rowData);
                            Save(cusId, _CustomerSummarizeList, _customerSummarizeDto, sum, rowData);
                        }
                        else
                        {
                            if (tjztsate != "3")
                            {
                                var arm = (string)gridViewCusReg.GetRowCellValue(index, conCustomerBM);
                                errarm += arm + "体检未完成，不能自动总检" + Environment.NewLine;
                            }
                            if (sumsate != "1")
                            {
                                var arm = (string)gridViewCusReg.GetRowCellValue(index, conCustomerBM);
                                errarm += arm + "已总检，不能重复总检" + Environment.NewLine;
                            }
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

        }
        //保存方法
        private void Save(Guid cusRegId, List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList,
            TjlCustomerSummarizeDto _customerSummarizeDto, string sum, OutCusListDto _tjlCustomerRegDto)
        {
            try
            {

                //删除建议表（多条）
                _inspectionTotalService.DelTjlCustomerSummarizeBM(new TjlCustomerQuery() { CustomerRegID = cusRegId, IsZYB = 2 });
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
                    _TjlCustomerSummarize.CheckState = (int)SummSate.Audited;
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
                    _customerSummarizeDto.CheckState = (int)SummSate.Audited;
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
                //_tjlCustomerRegDto.SummSate = (int)SummSate.Audited;
                //_tjlCustomerRegDto.SummLocked = 2;
                //_tjlCustomerRegDto.CSEmployeeId = CurrentUser.Id;
                //_inspectionTotalService.UpdateTjlCustomerRegDto(_tjlCustomerRegDto);


                EditCustomerRegStateDto editCustomerRegStateDto = new EditCustomerRegStateDto();
                editCustomerRegStateDto.SummLocked = _tjlCustomerRegDto.SummLocked;
                editCustomerRegStateDto.CSEmployeeId = CurrentUser.Id;//(long)customyiSheng.EditValue;
                editCustomerRegStateDto.FSEmployeeId = CurrentUser.Id;//(long)customyiSheng.EditValue;
                editCustomerRegStateDto.SummSate = (int)SummSate.Audited;

                editCustomerRegStateDto.Id = _tjlCustomerRegDto.Id;
                _inspectionTotalService.UpdateTjlCustomerRegState(editCustomerRegStateDto);


            }
            catch (UserFriendlyException c)
            {
                ShowMessageBox(c);
            }
        }

        //匹配建议算法
        private List<TjlCustomerSummarizeBMDto> MatchingAdvice(string summ, Guid cusRegId, List<TjlCustomerSummarizeBMDto> _CustomerSummarizeList)
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
            info.IsPrivacy = false;
            return info;
        }

        private void labName_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmInspectionTotalNew_FormClosed(object sender, FormClosedEventArgs e)
        {

            //解除所有锁定
            var IsSD = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.CusSumSet, 21)?.Remarks;
            if (!string.IsNullOrEmpty(IsSD) && IsSD == "Y")
            {
                LockStateDto LockState = new LockStateDto();
                LockState.SummLocked = 3;// (int)SummLockedState.Unchecked;
                LockState.SummLockEmployeeBMId = null;
                LockState.Id = NowCusRegInf.Id;
                _inspectionTotalService.UpdateCuslockSate(LockState);
            }

            #region 总检解除锁定
            //if (NowCusRegInf != null && NowCusRegInf.Id != Guid.Empty && NowCusRegInf.SummLocked == (int)SummLockedState.Alr && NowCusRegInf.SummLockEmployeeBMId == CurrentUser.Id)
            //{
            //    // UpdateCuslockSate(LockStateDto dto)
            //    LockStateDto LockState = new LockStateDto();
            //    LockState.SummLocked = (int)SummLockedState.Unchecked;
            //    LockState.SummLockEmployeeBMId = null;
            //    LockState.Id = NowCusRegInf.Id;
            //    _inspectionTotalService.UpdateCuslockSate(LockState);
            //}

            #endregion
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (NowCusRegInf != null && NowCusRegInf.Id != Guid.Empty && NowCusRegInf.SummLocked == (int)SummLockedState.Alr /*&& NowCusRegInf.SummLockEmployeeBMId == CurrentUser.Id*/)
            {
                #region 总检解除锁定
                // UpdateCuslockSate(LockStateDto dto)
                LockStateDto LockState = new LockStateDto();
                LockState.SummLocked = (int)SummLockedState.Unchecked;
                LockState.SummLockEmployeeBMId = null;
                LockState.Id = NowCusRegInf.Id;
                _inspectionTotalService.UpdateCuslockSate(LockState);
                //解锁
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = NowCusRegInf.CustomerBM;
                createOpLogDto.LogName = NowCusRegInf.Name;
                createOpLogDto.LogText = "解锁";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.SumId;
                _commonAppService.SaveOpLog(createOpLogDto);
                MessageBox.Show("已解除锁定！");
            }
            #endregion

        }

        private void treeListZhenDuan_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu3.ShowPopup(MousePosition);

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // var sumdto = treeListZhenDuan.GetFocusedRow() as CustomerRegisterSummarizeSuggestDto;
            var sumdto = new CustomerRegisterSummarizeSuggestDto();
             //更新建议的一会改
             SummarizeAdviceEdit frm = new SummarizeAdviceEdit(sumdto.SummarizeName, sumdto.Advice);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                DefinedCacheHelper.UpdateSummarizeAdvices();
                //customGridZhenDuan2.Properties.DataSource = DefinedCacheHelper.GetSummarizeAdvices();
            }
        }

        private void memoEditHuiZong_Validated(object sender, EventArgs e)
        {
            //var currentNumber = 0;
            //foreach (var paragraph in memoEditHuiZong.Document.Paragraphs)
            //{
            //    var length = (currentNumber + 1).ToString().Length;
            //    var range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
            //    var text = memoEditHuiZong.Document.GetText(range);
            //    if(int.TryParse(text, out var number))
            //    {
            //        if(number != currentNumber + 1)
            //        {
            //            memoEditHuiZong.Document.Replace(range, (currentNumber + 1).ToString());
            //        }
            //        currentNumber++;
            //    }
            //    else
            //    {
            //        currentNumber = 0;
            //    }
            //}
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var currentNumber = 0;
            foreach (var paragraph in memoEditHuiZong.Document.Paragraphs)
            {
                var length = (currentNumber + 1).ToString().Length;
                var range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
                var text = memoEditHuiZong.Document.GetText(range);

                var remove = false;
                while (int.TryParse(text, out var _))
                {
                    remove = true;
                    length++;
                    range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
                    text = memoEditHuiZong.Document.GetText(range);
                }
                if (int.TryParse(remove ? text.Remove(length - 1) : text, out var number))
                {
                    if (number != currentNumber + 1)
                    {
                        if (remove)
                        {
                            range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length - 1);
                        }
                        memoEditHuiZong.Document.Replace(range, (currentNumber + 1).ToString());
                    }
                    currentNumber++;
                }
                else
                {
                    currentNumber = 0;
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            //memoEditHuiZong
            string selectText = memoEditHuiZong.Document.GetText(this.memoEditHuiZong.Document.Selection);
            if (selectText.Length > 0)
            {

                using (var frm = new BasicDictionaryEditor(selectText, BasicDictionaryType.SumHide))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        memoEditHuiZong.Text = memoEditHuiZong.Text.Replace(selectText + "。", "");
                        memoEditHuiZong.Text = memoEditHuiZong.Text.Replace(selectText + "；", "");
                        memoEditHuiZong.Text = memoEditHuiZong.Text.Replace(selectText + ";", "");
                        memoEditHuiZong.Text = memoEditHuiZong.Text.Replace(selectText + "\r\n", "");
                        memoEditHuiZong.Text = memoEditHuiZong.Text.Replace(selectText, "");
                        //更新字典缓存
                        DefinedCacheHelper.UpdateBasicDictionary();
                    }
                }
            }
        }

        private void butA_Click(object sender, EventArgs e)
        {
            setWJZ(1);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            setWJZ(2);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            var cusGroup = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();

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
                    //屏蔽职业检数据
                    gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.RefreshDataSource();
                    gridControl3.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void repositoryItemMemoEdit5_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void repositoryItemMemoEdit6_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is MemoEdit cb)
            {
                selectrxt = cb.SelectedText;
            }
        }

        private void repositoryItemMemoEdit5_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is MemoEdit cb)
            {
                selectrxt = cb.SelectedText;
            }
        }

   
        private void labClient_Click(object sender, EventArgs e)
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
            alertInfo.Show(this, "复制提示", $"“{labelControl.Text}”已复制到剪贴板！");
        }

        private void dockZYYC_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            //if()
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (_tjlCustomerRegDto == null)
            {
                MessageBox.Show("请选择体检人！");
                return;
            }
            var cusItem = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();

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
                if (cusItem.ItemBM.moneyType == 1 || cusItem.ItemBM.moneyType == 2)
                {
                    MessageBox.Show("数值型项目不可设置阳性！");
                    return;
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
                    var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
                    //检查结果
                    var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                            .GetCustomerRegisterItem(input);
                    //屏蔽职业检数据
                    gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.RefreshDataSource();
                    gridControl3.Refresh();
                    //屏蔽职业检数据
                    var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.DataSource = cusgrouplist;
                    //科室小结
                    var customerRegisterDepartmentSummaryTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterDepartmentSummaryAppService
                        .GetCustomerRegisterDepartmentSummary(input);
                    var deparIDlist = cusgrouplist.Select(p => p.DepartmentId).Distinct().ToList();
                    //gridControl2.DataSource = customerRegisterDepartmentSummaryTask.Result.Where(
                    //    p => deparIDlist.Contains(p.DepartmentId.Value)).ToList();
                    //gridControl2.RefreshDataSource();
                    //gridControl2.Refresh();
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
            var cusItem = gridControl3.GetFocusedRowDto<CustomerRegisterItemDto>();

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
                if (cusItem.ItemBM.moneyType == 1 || cusItem.ItemBM.moneyType == 2)
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
                    var input = new EntityDto<Guid>(_tjlCustomerRegDto.Id);
                    //检查结果
                    var customerRegisterItemTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterItemAppService
                            .GetCustomerRegisterItem(input);
                    //屏蔽职业检数据
                    gridControl3.DataSource = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.RefreshDataSource();
                    gridControl3.Refresh();
                    //屏蔽职业检数据
                    var cusgrouplist = customerRegisterItemTask.Result.Where(p => !p.CustomerItemGroupBM.IsZYB.HasValue || p.CustomerItemGroupBM.IsZYB != 1).ToList();
                    gridControl3.DataSource = cusgrouplist;
                    //科室小结
                    var customerRegisterDepartmentSummaryTask = DefinedCacheHelper.DefinedApiProxy.CustomerRegisterDepartmentSummaryAppService
                        .GetCustomerRegisterDepartmentSummary(input);
                    //var deparIDlist = cusgrouplist.Select(p => p.DepartmentId).Distinct().ToList();
                    //gridControl2.DataSource = customerRegisterDepartmentSummaryTask.Result.Where(
                    //    p => deparIDlist.Contains(p.DepartmentId.Value)).ToList();
                    //gridControl2.RefreshDataSource();
                    //gridControl2.Refresh();
                }
            }
            else
            {
                XtraMessageBox.Show("没有获取到数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton9_Click_1(object sender, EventArgs e)
        {
            var currentNumber = 0;
            foreach (var paragraph in memoEditHuiZong.Document.Paragraphs)
            {
                var length = (currentNumber + 1).ToString().Length;
                var range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
                var text = memoEditHuiZong.Document.GetText(range);

                var remove = false;

                if (text.Contains(".") || text.Contains("、"))
                { remove = true; }
                while (int.TryParse(text, out var _))
                {
                    remove = true;
                    length++;
                    range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
                    text = memoEditHuiZong.Document.GetText(range);
                }
                if (int.TryParse(remove ? text.Remove(length - 1) : text, out var number))
                {
                    if (number != currentNumber + 1)
                    {
                        if (remove)
                        {
                            range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length - 1);
                        }
                        memoEditHuiZong.Document.Replace(range, (currentNumber + 1).ToString());
                    }
                    currentNumber++;
                }

                if (!int.TryParse(text, out var _) && !int.TryParse(remove ? text.Remove(length - 1) : text, out var number1))
                {

                    range = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, length);
                    string ss = memoEditHuiZong.Document.GetText(range);
                    if (!string.IsNullOrEmpty(ss.Replace("\r\n", "").Replace(" ", "").Replace("\t", "")))
                    {
                        var range1 = memoEditHuiZong.Document.CreateRange(paragraph.Range.Start, 0);
                        memoEditHuiZong.Document.Replace(range1, ((currentNumber + 1) + ".").ToString());

                        currentNumber++;
                    }

                }


            }
        }

        private void dockPanel1_Click_1(object sender, EventArgs e)
        {

        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            dockPanelJL.Visibility= DevExpress.XtraBars.Docking.DockVisibility.Visible;
            var cusRegSum = getStrToDR(richAD.Text);// treeListZhenDuan.DataSource as List<CustomerRegisterSummarizeSuggestDto>;
          
            if (cusRegSum != null && cusRegSum.Count > 0)
            {
                string zd = string.Join("\r\n", cusRegSum.Select(p => p.SummarizeOrderNum + "." + p.SummarizeName));
                memoEdit1.Text = zd;
               
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            var sumstr = memoEdit1.Text.Replace("\r\n", "|");
            var sumlist = sumstr.Split('|').Where(p => p != "").ToList();
            string newsum = "";
            int num = 1;
            foreach (var sum in sumlist)
            {
                var index = sum.IndexOf(".");

                var xh = sum.Substring(index + 1, sum.Length - index-1);
                var sumname =  num + "." + xh;
                num++;
                newsum += sumname +"\r\n";
            }
            memoEdit1.Text = newsum;
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            dockPanelJL.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(memoEdit1.Text))
            {
                var sumNamelist = memoEdit1.Text.Replace("\r\n", "|").Split('|').Where(p=>p!="").ToList();
                var rows = new List<CustomerRegisterSummarizeSuggestDto>();
                int num = 1;
                foreach (string  sum in sumNamelist)
                {
                  var  sumnew = sum.Substring(sum.IndexOf(".")+1, sum.Length - sum.IndexOf(".")-1);
                    if (sum == "")
                    {
                        continue;
                    }
                    var sumad = DefinedCacheHelper.GetSummarizeAdvices().FirstOrDefault
                        (o => o.AdviceName == sumnew);
                    if (sumad == null)
                    {
                        var sumpp = UPAdviseBySumName(sumnew);
                        if (sumpp == null)
                        {
                            SummarizeAdviceDto summarize = new SummarizeAdviceDto();
                            summarize.AdviceName = sumnew;
                            summarize.OrderNum = num;
                            summarize.IsTestInfo = 1;
                            summarize.SummAdvice = "";
                            rows.Add(SummarizeBMToJL(summarize));
                            num++;
                        }
                        else
                        {
                            sumpp.OrderNum= num;
                            sumpp.AdviceName= sumnew;
                            rows.Add(SummarizeBMToJL(sumpp));
                            num++;
                        }

                    }
                    else
                    {
                        if (gridViewCusReg.GetFocusedRow() is OutCusListDto row)
                        {
                            sumad.OrderNum = num;

                           rows.Add(SummarizeBMToJL(sumad));
                            num++;
                        }
                    }

                }

                rows = rows.OrderBy(r => r.SummarizeOrderNum).ToList();
                foreach (var customerRegisterSummarizeSuggestDto in rows)
                {
                    customerRegisterSummarizeSuggestDto.SummarizeOrderNum = rows.IndexOf(customerRegisterSummarizeSuggestDto) + 1;
                }
                rows = HBSum(rows);
                rows = MakeReview(rows);

                //treeListZhenDuan.DataSource = rows;
                richAD.Text = AdDrTOStr(rows);
            }
    
        }
        public List<CustomerRegisterSummarizeSuggestDto> UPAdvise()
        {
            // 生成汇总
            var basicDictionary = DefinedCacheHelper.GetBasicDictionary();
            var field总检基础字典数据 = basicDictionary.Where(r => r.Type == BasicDictionaryType.CusSumSet.ToString()).ToList();
            if (field总检基础字典数据.Count == 0)
            {
                ShowMessageBoxError("缺少总检配置，无法生成总检!");
                return new List<CustomerRegisterSummarizeSuggestDto>();
            }
            var field非异常诊断前缀 = field总检基础字典数据.Find(r => r.Value == 7)?.Remarks;
            if (!string.IsNullOrWhiteSpace(memoEditHuiZong.Text))
            {
                // 匹配建议
                var field总检建议列表 = DefinedCacheHelper.GetSummarizeAdvices().Where(o => o.AdviceName != "").OrderBy(p => p.OrderNum);
                // 过滤数据
                var field已匹配建议 = new ConcurrentDictionary<string, SummarizeAdviceDto>();
                var field匹配多条建议 = false;
                var field匹配多条建议规则 = field总检基础字典数据.Find(r => r.Value == 5);
                if (field匹配多条建议规则 != null && field匹配多条建议规则.Remarks == 1.ToString())
                {
                    field匹配多条建议 = true;
                }
                Parallel.ForEach(field总检建议列表, o =>
                {
                   
                    string adName = "";
                    if (string.IsNullOrEmpty(o.Advicevalue))
                    {
                        adName = o.AdviceName;
                    }
                    else
                    { adName = o.Advicevalue; }
                    var field建议依据集合 = adName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in field建议依据集合)
                    {
                        if (memoEditHuiZong.Text.Contains(s))
                        {
                            //排除非异常前缀如 未见脂肪干
                            if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                            {
                                var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                bool havalue = false;
                                foreach (var str in field非异常诊断前缀列表)
                                {
                                    if (memoEditHuiZong.Text.Contains(str + s))
                                    { havalue = true; }
                                }
                                if (havalue == false)
                                {
                                    field已匹配建议.TryAdd(s, o);
                                    break;
                                }
                            }
                            else
                            {
                                field已匹配建议.TryAdd(s, o);
                                break;
                            }
                        }
                    }
                });

                if (field已匹配建议.Count != 0)
                {
                    //按汇总顺序排序
                    foreach (var item in field已匹配建议)
                    {
                        if (item.Value != null)
                        {
                            item.Value.OrderNum = memoEditHuiZong.Text.IndexOf(item.Key);
                        }
                    }
                    if (!field匹配多条建议)
                    {
                        var field待删除建议 = (from keyValuePair in field已匹配建议 from keyValuePair1 in field已匹配建议 where keyValuePair1.Key != keyValuePair.Key where keyValuePair1.Key.Contains(keyValuePair.Key) select keyValuePair.Key).ToList();
                        foreach (var s in field待删除建议)
                        {
                            field已匹配建议.TryRemove(s, out var value);
                        }
                    }

                    if (gridViewCusReg.GetFocusedRow() is OutCusListDto row)
                    {
                        var rows = new List<CustomerRegisterSummarizeSuggestDto>();

                        foreach (var keyValuePair in field已匹配建议)
                        {


                            rows.Add(SummarizeBMToJL(keyValuePair.Value));
                        }

                        rows = rows.OrderBy(r => r.SummarizeOrderNum).ToList();
                        foreach (var customerRegisterSummarizeSuggestDto in rows)
                        {
                            customerRegisterSummarizeSuggestDto.SummarizeOrderNum = rows.IndexOf(customerRegisterSummarizeSuggestDto) + 1;
                        }
                        rows = HBSum(rows);
                        rows = MakeReview(rows);

                        return rows;
                        // treeListZhenDuan.DataSource = rows;

                    }
                }
            }
            return null;
        }

        public SummarizeAdviceDto UPAdviseBySumName(string sumName)
        {
            // 生成汇总
            var basicDictionary = DefinedCacheHelper.GetBasicDictionary();
            var field总检基础字典数据 = basicDictionary.Where(r => r.Type == BasicDictionaryType.CusSumSet.ToString()).ToList();
            if (field总检基础字典数据.Count == 0)
            {
                ShowMessageBoxError("缺少总检配置，无法生成总检!");
                return null;
            }
            var field非异常诊断前缀 = field总检基础字典数据.Find(r => r.Value == 7)?.Remarks;
            if (!string.IsNullOrWhiteSpace(sumName))
            {
                // 匹配建议
                var field总检建议列表 = DefinedCacheHelper.GetSummarizeAdvices().Where(o => o.AdviceName != "").OrderByDescending(p =>p.AdviceName.Length);
                // 过滤数据
               
                var field匹配多条建议 = false;
                var field匹配多条建议规则 = field总检基础字典数据.Find(r => r.Value == 5);
                if (field匹配多条建议规则 != null && field匹配多条建议规则.Remarks == 1.ToString())
                {
                    field匹配多条建议 = true;
                }
                foreach ( var o in field总检建议列表 )
                {

                    string adName = "";
                    if (string.IsNullOrEmpty(o.Advicevalue))
                    {
                        adName = o.AdviceName;
                    }
                    else
                    { adName = o.Advicevalue; }
                    var field建议依据集合 = adName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in field建议依据集合)
                    {
                        if (sumName.Contains(s))
                        {
                            //排除非异常前缀如 未见脂肪干
                            if (!string.IsNullOrWhiteSpace(field非异常诊断前缀))
                            {
                                var field非异常诊断前缀列表 = field非异常诊断前缀.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                bool havalue = false;
                                foreach (var str in field非异常诊断前缀列表)
                                {
                                    if (sumName.Contains(str + s))
                                    { havalue = true; }
                                }
                                if (havalue == false)
                                {
                                    return o;
                                    
                               
                                }
                            }
                            else
                            {
                                return o;
                              
                            }
                        }
                    }
                };
              
            }
            return null;
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            var rows = UPAdvise();
            if (rows != null && rows.Count > 0)
            {
                string zd = string.Join("\r\n", rows.Select(p =>
                +p.SummarizeOrderNum +"."+ p.SummarizeName));
                memoEdit1.Text = zd;
            }
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
           var rows = UPAdvise();
            //treeListZhenDuan.DataSource = rows;
            richAD.Text = AdDrTOStr(rows);
        }       
        private void tvJianChaXiangMu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var SelectId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());

            var SelectDepId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Tag.ToString());
             
            
           
            if (tvJianChaXiangMu.SelectedNode.Level == 0)
            {

                List < CustomerRegisterItemDto > cusgroup= gridControl3.DataSource as List<CustomerRegisterItemDto>;

                var cusGroup = currCusRegGroupItems.Where(p => p.DepartmentId ==
                 SelectDepId).ToList();
                gridControl3.DataSource = cusGroup;


                //string depsear = "[DepartmentId] =='" + SelectDepId + "'";
                //gridView3.ActiveFilter.Add(gridColumn34,
                //    new ColumnFilterInfo(depsear));
            }
            else if (tvJianChaXiangMu.SelectedNode.Level == 1)
            {
                //SelectDepId = Guid.Parse(tvJianChaXiangMu.SelectedNode.Parent.Tag.ToString());
                //string depsear = "[ItemGroupBMId] =='" + SelectId + "'";
                //gridView3.ActiveFilter.Add(gridColumn34,
                //    new ColumnFilterInfo(depsear));

                var cusGroup = currCusRegGroupItems.Where(p => 
                p.ItemGroupBMId == SelectId).ToList();
                gridControl3.DataSource = cusGroup;
                SelectDepId = cusGroup.FirstOrDefault().DepartmentId;
            }
           
            var cusDepartsum = currCusDeptSum.FirstOrDefault(p=>p.DepartmentId==
            SelectDepId);
            if (cusDepartsum != null)
            {
                memoEditZhenDuan.Text = cusDepartsum?.DagnosisSummary;

                labelControl7.Text = "科室医生：" + DefinedCacheHelper.GetComboUsers().FirstOrDefault(p => p.Id
                  == cusDepartsum.ExamineEmployeeId)?.Name;
            }
        }
    }
 
}

/*
 * TjlCustomerDepSummary
 * 科室小结表
 *
 * TjlCustomerSummarize
 * 体检人总检汇总表
 *
 * TjlCustomerRegItem
 * 体检人预约项目记录表
 *
 * TjlCustomerSummarize
 * 总检汇总表
 *
 * TjlCustomerSummarizeBM
 * 总检建议列表
 */
