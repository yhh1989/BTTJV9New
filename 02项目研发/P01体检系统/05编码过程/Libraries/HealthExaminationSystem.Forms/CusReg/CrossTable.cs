using Abp.Application.Services.Dto;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.PaymentManager.InvoicePrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CrossTable : UserBaseForm
    {
        private ICrossTableAppService crossTableAppService;
        private bool allCheckState = false;
        private bool autoDrawBlood = false;
        private bool autoCorssTable = false;
        private List<SendToConfirmModel> sendToConfirmModels;
        private List<SexModel> sexModels;
        private CameraHelper cameraHelper;
        private string customerIMG;
        private PictureController _pictureController;

        private readonly ICommonAppService _commonAppService;
        private readonly IDoctorStationAppService _DoctorStationAppService =new DoctorStationAppService();
        private ICustomerAppService customerSvr;//体检预约
        /// <summary>
        /// 导引单图片id
        /// </summary>
        private Guid? GuidancePhotoId;
        public CrossTable()
        {
            InitializeComponent();
            crossTableAppService = new CrossTableAppService();
            sendToConfirmModels = SendToConfirmHelper.GetSendToConfirmModels();
            sexModels = SexHelper.GetSexModels();
            _commonAppService = new CommonAppService();
        }

        public CrossTable(string Tijianhao)
        {
            InitializeComponent();
            textEditTijianhao.Text = Tijianhao;
            crossTableAppService = new CrossTableAppService();
            sendToConfirmModels = SendToConfirmHelper.GetSendToConfirmModels();
            sexModels = SexHelper.GetSexModels();
            _commonAppService = new CommonAppService();
        }

        private void CrossTable_Load(object sender, EventArgs e)
        {
            customerSvr = new CustomerAppService();
            Intinal();
            LoadData(1);
            checkEditMorenJiaobiao.Checked = true;
            checkEditMorenChouxue.Checked = true;
            simpleButtonJiaobiao.Visible= true;
        }
        private string FormatSendToConfirm(object arg)
        {
            try
            {
                return sendToConfirmModels.Find(t => t.Id == (int)arg).Display;
            }
            catch
            {
                return sendToConfirmModels.Find(t => t.Id == (int)SendToConfirm.No).Display;
            }
        }

        private string FormatCheckState(object arg)
        {
            return CheckSateHelper.PhysicalEStateFormatter(arg);
        }
        private string FormatSex(object arg)
        {
            try
            {
                return sexModels.Find(s => s.Id == (int)arg).Display;
            }
            catch
            {
                return arg.ToString();
            }
        }

        private string FormatBloodState(object arg)
        {
            if (arg == null)
                return "未抽血";
            else
                return "已抽血";
        }


        /// <summary>
        /// 数据加载
        /// </summary>
        private void LoadData(int? curentPage)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            var selectIndex = gridView1.GetFocusedDataSourceRowIndex();
            gridControlCustomerInfo.DataSource = null;
         
            
            QueryInfoDto queryInput = new QueryInfoDto();
            if (lookUpEditJiaobiaoZhuangtai.EditValue != null)
                queryInput.SendToConfrim = Convert.ToInt32(lookUpEditJiaobiaoZhuangtai.EditValue);
            if (dateEditStartTime.EditValue != null)
                queryInput.StartDate = Convert.ToDateTime(dateEditStartTime.EditValue).Date;
            if (dateEditEndTime.EditValue != null)
                queryInput.EndDate = Convert.ToDateTime(dateEditEndTime.EditValue).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            if (!string.IsNullOrWhiteSpace((string)textEditTijianhao.EditValue))
                queryInput.CustomerRegNum = textEditTijianhao.Text.Trim();
            //txtName
            if (!string.IsNullOrWhiteSpace((string)txtName.EditValue))
                queryInput.Name = txtName.Text.Trim();
         
            try
            {
                var queryResult = crossTableAppService.QueryCustomer(queryInput);
              
             
                gridControlCustomerInfo.DataSource = queryResult;
                gridView1.FocusedRowHandle = selectIndex;
                
                textEditDengjiRenshu.Text = queryResult.Count(a => a.RegisterState == 2).ToString();
                textEditJiaobiaoRenshu.Text = queryResult.Count(a => a.SendToConfirm == 2).ToString();
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }

            TreeListBind();
        }
        /// <summary>treeListItemGroup
        /// 自定义初始化
        /// </summary>
        private void Intinal()
        {
            var sendToConfirmModels = SendToConfirmHelper.GetSendToConfirmModels();
            lookUpEditJiaobiaoZhuangtai.Properties.DataSource = sendToConfirmModels;

            dateEditStartTime.EditValue = DateTime.Now;
            dateEditEndTime.EditValue = DateTime.Now;
            searchLookUpUser.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
            gridView1.Columns[sendToConfirm.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[sendToConfirm.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSendToConfirm);
            gridView1.Columns[bloodState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[bloodState.FieldName].DisplayFormat.Format = new CustomFormatter(FormatBloodState);
            gridView1.Columns[Sex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(FormatSex);
            gridView1.Columns[TijianState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[TijianState.FieldName].DisplayFormat.Format = new CustomFormatter(FormatCheckState);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonChaxun_Click(object sender, EventArgs e)
        {
            if (dateEditEndTime.EditValue != null && dateEditStartTime.EditValue != null)
            {
                if (dateEditEndTime.DateTime < dateEditStartTime.DateTime)
                {
                    ShowMessageBoxWarning("结束时间大于开始时间，请重新选择。");
                    return;
                }
            }

            LoadData(1);
        }
        /// <summary>
        /// 单击列表绑定treelist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
                return;
            textEditXingming.Text = currentDate.Customer.Name;
            textEditXingbie.Text = FormatSex(currentDate.Customer.Sex);
            textEditNianling.Text = currentDate.Customer.Age.ToString() + currentDate.Customer.AgeUnit;
            textTijianhao.Text = currentDate.CustomerBM;
            searchLookUpUser.EditValue = currentDate.SendUserId;


            TreeListBind();

        }
        /// <summary>
        /// treelist绑定
        /// </summary>
        private void TreeListBind()
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                treeListItemGroup.ClearNodes();
                return;
            }
              
            treeListItemGroup.ClearNodes();
            var departemtnIds = currentDate.CustomerItemGroup.Select(c => c.DepartmentId).Distinct();

            foreach (var departmentId in departemtnIds)
            {
                //不显示全部项目
                if (!allCheckState)
                {
                    //没有未检查状态的科室跳过
                    if (!currentDate.CustomerItemGroup.Any(c => c.DepartmentId == departmentId && c.CheckState != (int)ProjectIState.Complete))
                        continue;
                }
                var node = treeListItemGroup.AppendNode(new object[] {
                    currentDate.CustomerItemGroup.FirstOrDefault(c=>c.DepartmentId==departmentId).DepartmentName,departmentId
                }, -1);
                node.Tag = departmentId;
                var departmentGroups = currentDate.CustomerItemGroup.Where(c => c.DepartmentId == departmentId).ToList();
                if (!allCheckState)
                    departmentGroups = departmentGroups.Where(d => d.CheckState != (int)ProjectIState.Complete).ToList();
                foreach (var item in departmentGroups)
                {
                    var stateStr = string.Empty;
                    switch (item.CheckState)
                    {
                        case (int)ProjectIState.Complete:
                            stateStr = "(已检)";
                            break;
                        case (int)ProjectIState.Stay:
                            stateStr = "(待查)";
                            break;
                        case (int)ProjectIState.GiveUp:
                            stateStr = "(放弃)";
                            break;
                        case (int)ProjectIState.Not:
                            stateStr = "(未检)";
                            break;
                        case (int)ProjectIState.Part:
                            stateStr = "(部分检查)";
                            break;
                        case (int)ProjectIState.Temporary:
                            stateStr = "(暂存)";
                            break;
                    }
                    switch (item.CollectionState)
                    {
                        case (int)CollectionState.Normal:
                            stateStr += "(未核收)";
                            break;
                        case (int)CollectionState.Scatter:
                            stateStr += "(已核收)";
                            break;
                        
                    }
                    var sonNode = treeListItemGroup.AppendNode(new object[] {
                    item.ItemGroupName+stateStr,item.Id
                    }, node.Id);
                    sonNode.Tag = item;
                }
                node.ExpandAll();
            }

            #region 抽血状态
            var input = new CustomerRegisterBarCodePrintInformationConditionInput();
            input.BarCodeNumber = currentDate.CustomerBM;
            AutoLoading(() =>
            {
                var result = DefinedCacheHelper.DefinedApiProxy.BloodWorkstationAppService
                    .QueryBarCodePrintRecord(input).Result;
                if (result.Count > 0)
                {
                    if (result.All(p => p.HaveBlood == true))
                    {
                        textEditblood.Text = "全部抽血";
                    }
                    else if (result.Any(p => p.HaveBlood == true))
                    { textEditblood.Text = "部分抽血"; }
                    else
                    { textEditblood.Text = "未抽血"; }


                }
                else
                {
                    textEditblood.Text = "未抽血";
                }
            });
            #endregion
        }
        /// <summary>
        /// 是否显示全部项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditQuanbuXiangmu_CheckedChanged(object sender, EventArgs e)
        {
            allCheckState = !allCheckState;
            TreeListBind();
        }
        #region treelist 父子节点选择联动
        /// <summary>
        /// treelist 父子节点选择联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListItemGroup_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        /// <summary>
        /// 设置子节点的状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        /// <summary>
        /// 设置父节点的状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        #endregion
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNavigator1_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            LoadData(null);
        }
        /// <summary>
        /// 交表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonJiaobiao_Click(object sender, EventArgs e)
        {
            Jiaobiao();
        }
        /// <summary>
        /// 获取本机地址
        /// </summary>
        /// <returns></returns>
        public string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取消交表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonQuxiaoJiaobiao_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.SendToConfirm == (int)SendToConfirm.No)
            {
                ShowMessageBoxInformation("选定记录已是\"未交表\"状态！");
                return;
            }
            if (XtraMessageBox.Show("确定取消交表?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            currentDate.SendToConfirm = (int)SendToConfirm.No;
            try
            {
                var result = crossTableAppService.EditCrossTableState(new CustomerRegForCrossTableDto { Id = currentDate.Id,
                    SendToConfirm = currentDate.SendToConfirm,
                    SendUserId = CurrentUser.Id
                });
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;
                createOpLogDto.LogText = "取消交表";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (result != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void simpleButtonFangqi_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("请至少选择一项项目组!");
                return;
            }
            if (currentDate.SendToConfirm==(int)SendToConfirm.Yes)
            {
                ShowMessageBoxInformation("已交表不能放弃!");
                return;
            }
            var itemList = crossTableAppService.QueryCustomerItems(new QuerCustomerItemsDto { CustomerRegId = currentDate.Id });
            var checkList = treeListItemGroup.GetAllCheckedNodes();
            if (checkList.Where(c => !c.HasChildren).Count() < 1)
            {
                ShowMessageBoxInformation("请至少选择一项项目组!");
                return;
            }
            EditItemGroupStateInput inputList = new EditItemGroupStateInput();
            inputList.Ids = new List<Guid>();
            var Names = new List<string>();
            string InfoStr = "";
            foreach (var item in checkList)
            {
                if (item.HasChildren)
                    continue;
                var group = (CustomerItemGroupDto)item.Tag;
                var items = itemList.Where(i => i.CustomerItemGroupBM?.Id == group.Id).ToList();
                if (items.Any(c => c.ProcessState == (int)ProjectIState.Complete))
                {
                    InfoStr += group.ItemGroupName + "、";
                }
                else
                {
                    if (group.CheckState != (int)ProjectIState.GiveUp)
                    {
                        inputList.CheckState = (int)ProjectIState.GiveUp;
                        inputList.Ids.Add(group.Id);
                        Names.Add(group.ItemGroupName);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(InfoStr))
            {
                InfoStr = InfoStr.Trim('、');
                InfoStr += "，项目组中包含已检查项目，不能进行放弃操作！";
                ShowMessageBoxInformation(InfoStr);
                return;
            }
            FrmGiveUpMessage fromGiveUpMessage = new FrmGiveUpMessage((int)ProjectIState.GiveUp);
            fromGiveUpMessage.ShowDialog();
            if (fromGiveUpMessage.DialogResult == DialogResult.Cancel)
                return;

            inputList.Record = new CusGiveUpDto { remart = fromGiveUpMessage.Remarks };

            try
            {
                var resultList = crossTableAppService.EditItemGroupState(inputList);
                //处理体检状态
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = currentDate.Id;
                _DoctorStationAppService.UpCheckState(input);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;

                createOpLogDto.LogText = "交表界面放弃项目：" + string.Join(",", Names);
               
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (resultList != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void simpleButtonDaijian_Click(object sender, EventArgs e)
        {
            var checkList = treeListItemGroup.GetAllCheckedNodes();
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("请至少选择一项项目组!");
                return;
            }
            var itemList = crossTableAppService.QueryCustomerItems(new QuerCustomerItemsDto { CustomerRegId = currentDate.Id });
            if (checkList.Where(c => !c.HasChildren).Count() < 1)
            {
                ShowMessageBoxInformation("请至少选择一项项目组!");
                return;
            }
            EditItemGroupStateInput inputList = new EditItemGroupStateInput();
            inputList.Ids = new List<Guid>();
            var Names = new List<string>();
            string InfoStr = "";
            foreach (var item in checkList)
            {
                if (item.HasChildren)
                    continue;
                var group = (CustomerItemGroupDto)item.Tag;
                var items = itemList.Where(i => i.CustomerItemGroupBM.Id == group.Id);
                if (items.Any(c => c.ProcessState == (int)ProjectIState.Complete))
                {
                    InfoStr += group.ItemGroupName + "、";
                }
                else
                {
                    if (group.CheckState != (int)ProjectIState.Stay)
                    {
                        inputList.CheckState = (int)ProjectIState.Stay;
                        inputList.Ids.Add(group.Id);
                        Names.Add(group.ItemGroupName);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(InfoStr))
            {
                InfoStr = InfoStr.Trim('、');
                InfoStr += "，项目组中包含已检查项目，不能进行待检操作！";
                ShowMessageBoxInformation(InfoStr);
                return;
            }
            FrmGiveUpMessage fromGiveUpMessage = new FrmGiveUpMessage((int)ProjectIState.Stay);
            fromGiveUpMessage.ShowDialog();
            if (fromGiveUpMessage.DialogResult == DialogResult.Cancel)
                return;

            inputList.Record = new CusGiveUpDto { remart = fromGiveUpMessage.Remarks, stayDate = fromGiveUpMessage.NextDate };

            try
            {
                var resultList = crossTableAppService.EditItemGroupState(inputList);
                //处理体检状态
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = currentDate.Id;
                _DoctorStationAppService.UpCheckState(input);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;

                createOpLogDto.LogText = "交表界面待查项目：" + string.Join(",", Names);

                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (resultList != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }

        }
        /// <summary>
        /// 撤销状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonChexiao_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            var checkList = treeListItemGroup.GetAllCheckedNodes();
            if (checkList.Where(c => !c.HasChildren).Count() < 1)
            {
                ShowMessageBoxInformation("请至少选择一项项目组!");
                return;
            }
            if (XtraMessageBox.Show("确定撤销状态?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            EditItemGroupStateInput inputList = new EditItemGroupStateInput();
            inputList.Ids = new List<Guid>();
            var Names=new List<string>();
            foreach (var item in checkList)
            {
                if (item.HasChildren)
                    continue;
                var group = (CustomerItemGroupDto)item.Tag;
                if (group.CheckState != (int)ProjectIState.Not)
                {
                    inputList.CheckState = (int)ProjectIState.Not;
                    inputList.Ids.Add(group.Id);
                    Names.Add(group.ItemGroupName);
                }
            }
            try
            {
                var resultList = crossTableAppService.EditItemGroupState(inputList);
                //处理体检状态
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = currentDate.Id;
                _DoctorStationAppService.UpCheckState(input);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;

                createOpLogDto.LogText = "待查项目：" + string.Join(",", Names);

                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ResId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (resultList != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }

        }

        private void checkEditMorenChouxue_CheckedChanged(object sender, EventArgs e)
        {
            autoDrawBlood = !autoDrawBlood;
        }
        /// <summary>
        /// 取消抽血
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonQuxiaoChouxue_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.DrawCard == null)
            {
                ShowMessageBoxInformation("选定记录已是\"取消抽血\"状态！");
                return;
            }
            if (XtraMessageBox.Show("确定取消抽血?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            var cancelInput = new CustomerRegForCrossTableDto();
            cancelInput.Id = currentDate.Id;
            cancelInput.BloodState = (int)BloodState.Cancel;
            try
            {
                var result = crossTableAppService.CancelDrawBlood(cancelInput);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;
                createOpLogDto.LogText = "取消抽血";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (result != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        ///// <summary>
        ///// 显示<see cref="UserFriendlyException"/>弹窗
        ///// </summary>
        ///// <param name="e">弹窗信息</param>
        //private void ShowMessageBox(UserFriendlyException e)
        //{
        //    XtraMessageBox.Show(e.Description, e.Code.ToString(), e.Buttons, e.Icon);
        //}

        private void lookUpEditJiaobiaoZhuangtai_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookUpEditJiaobiaoZhuangtai.EditValue = null;
            }
        }

        private void dateEditStartTime_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                dateEditStartTime.EditValue = null;
        }

        private void dateEditEndTime_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                dateEditEndTime.EditValue = null;
        }
        private void checkEditMorenJiaobiao_CheckedChanged(object sender, EventArgs e)
        {
            autoCorssTable = !autoCorssTable;
        }

        private void textEditTijianhao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textEditTijianhao.Text.Trim()))
                {
                    textEditTijianhao.Focus();
                    return;
                }
                LoadData(null);
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                if (currentDate == null)
                    return;
                if (autoCorssTable)
                    Jiaobiao();
                var input = new PageInputDto<QueryInfoDto> { TotalPages = TotalPages, CurentPage = CurrentPage };
                input.Input = new QueryInfoDto();
                input.Input.StartDate = currentDate.LoginDate.Value.Date;
                input.Input.EndDate = currentDate.LoginDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                input.Input.CustomerRegNum = textEditTijianhao.Text.Trim();
                input.Input.SendToConfrim = (int)SendToConfirm.Yes;
                try
                {
                    var queryResult = crossTableAppService.QueryCustomerInfo(input);
                    TotalPages = queryResult.CustomerReg.TotalPages;
                    CurrentPage = queryResult.CustomerReg.CurrentPage;
                    //InitialNavigator(dataNavigator1);
                    gridControlCustomerInfo.DataSource = queryResult.CustomerReg.Result;
                    textEditDengjiRenshu.Text = queryResult.SumRegister.ToString();
                    textEditJiaobiaoRenshu.Text = queryResult.SumSendToConfirm.ToString();
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }

                textEditTijianhao.Focus();
                textEditTijianhao.SelectAll();
            }
        }

        private void treeListItemGroup_DoubleClick(object sender, EventArgs e)
        {
            var currentNode = treeListItemGroup.FocusedNode;
            if (currentNode.HasChildren)
                return;
            var itemGroup = currentNode.Tag as CustomerItemGroupDto;
            if (itemGroup.CheckState == (int)ProjectIState.Stay ||
                itemGroup.CheckState == (int)ProjectIState.GiveUp)
            {
                FrmGiveUpMessage frmMessage = new FrmGiveUpMessage((int)itemGroup.CheckState, itemGroup);
                frmMessage.ShowDialog();
            }
        }

        private void simpleButtonQuerenChouxue_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.DrawCard != null)
            {
                ShowMessageBoxInformation("选定记录已是\"确认抽血\"状态！");
                return;
            }
            if (XtraMessageBox.Show("确定抽血?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                CustomerBloodNumDto bloodInput = new CustomerBloodNumDto();
                bloodInput.CustomerReg = new CustomerRegForCrossTableDto { Id = currentDate.Id };
                bloodInput.EmployeeBM = new Application.Users.Dto.UserViewDto { Id = CurrentUser.Id };
                bloodInput.BloodSate = (int)BloodState.DrawBlood;
                bloodInput.Ip = GetLocalIP();
                var result = crossTableAppService.DrawBlood(bloodInput);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;
                createOpLogDto.LogText = "抽血";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                _commonAppService.SaveOpLog(createOpLogDto);
                if (result != null)
                    LoadData(null);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }

        private void treeListItemGroup_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            var value = e.Node.Tag as CustomerItemGroupDto;
            if (value == null)
                return;
            if (value.IsAddMinus == (int)AddMinusType.Minus)
            {
                e.Appearance.FontStyleDelta = FontStyle.Bold | FontStyle.Strikeout;
                e.Appearance.ForeColor = Color.Green;
            }
            else if (value.IsAddMinus == (int)AddMinusType.Add)
            {
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.Appearance.ForeColor = Color.Red;
            }
            switch (value.CheckState)
            {
                case (int)ProjectIState.Stay:
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                    e.Appearance.ForeColor = Color.Blue;
                    break;
                case (int)ProjectIState.GiveUp:
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                    e.Appearance.ForeColor = Color.DarkGoldenrod;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 减项按钮
        /// </summary>
        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            SetAddMinus((int)AddMinusType.Minus);
        }
        /// <summary>
        /// 取消减项按钮
        /// </summary>
        private void btnCancelSubtraction_Click(object sender, EventArgs e)
        {
            SetAddMinus((int)AddMinusType.Normal);
        }
        /// <summary>
        /// 取消减项或减项
        /// </summary>
        private void SetAddMinus(int addMinusType)
        {
            var data = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if(data!=null)
                if (data.SummSate.HasValue)
                {
                    if (data.SummSate != (int)SummSate.NotAlwaysCheck)
                    {
                        ShowMessageBoxWarning("该人员不是未总检状态，不可加减项。");
                        return;
                    }
                }
            var checkList = treeListItemGroup.GetAllCheckedNodes();
            var regid = Guid.Empty;
            if (checkList.Any(o => !o.HasChildren))
            {
                var ids = new List<Guid>();
                var Names = new List<string>();

                foreach (var item in checkList.Where(o => !o.HasChildren).ToList())
                {
                    var group = (CustomerItemGroupDto)item.Tag;
                    if (group.CheckState != (int)ProjectIState.Not)
                    {
                        if(addMinusType==(int)AddMinusType.Normal)
                            ShowMessageBoxWarning("【" + group.ItemGroupName + "】" + "不可取消减项。");
                        else
                            ShowMessageBoxWarning("【" + group.ItemGroupName + "】" + "不可设为减项。");
                        return;
                    }
                    if (addMinusType == (int)AddMinusType.Normal)
                    {
                        if(group.IsAddMinus!=(int)AddMinusType.Minus)
                        {
                            ShowMessageBoxWarning("【"+group.ItemGroupName+"】"+"不是减项，不可取消。");
                            return;
                        }
                    }
                    regid = group.CustomerRegBMId.Value;
                    if (group.IsAddMinus != addMinusType)
                    {
                        ids.Add(group.Id);
                        Names.Add(group.ItemGroupName);
                    }
                }
                if (ids.Count() > 0)
                {
                    var input = new SetItemGroupAddMinusDto() { ItemGroupIds = ids, RegID = regid, SetAddMinusState = addMinusType };
                    //btnSubtraction.Enabled = false;
                    //btnCancelSubtraction.Enabled = false;
                    AutoLoading(() =>
                    {
                        var result = crossTableAppService.SetItemGroupAddMinusState(input);
                        //日志
                        CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        createOpLogDto.LogBM = data.CustomerBM;
                        createOpLogDto.LogName = data.Customer.Name;
                        if (addMinusType == (int)AddMinusType.Normal)
                        {
                            createOpLogDto.LogText = "减项：" + string.Join(",", Names);
                        }
                        if (addMinusType == (int)AddMinusType.Normal)
                        {
                            createOpLogDto.LogText = "取消减项：" + string.Join(",", Names);
                        }
                        createOpLogDto.LogDetail = "";
                        createOpLogDto.LogType = (int)LogsTypes.ResId;
                        _commonAppService.SaveOpLog(createOpLogDto);
                        var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                        var dellist = new List<CustomerItemGroupDto>();
                        foreach (var id in result.DelItemGroupIds)
                        {
                            dellist.Add(currentDate.CustomerItemGroup.FirstOrDefault(o => o.Id == id));
                        }
                        foreach (var del in dellist)
                        {
                            currentDate.CustomerItemGroup.Remove(del);
                        }
                        foreach (var id in result.ItemGroupIds)
                        {
                            var item = currentDate.CustomerItemGroup.FirstOrDefault(o => o.Id == id);
                            if (item != null)
                                item.IsAddMinus = addMinusType;
                        }
                        TreeListBind();
                    });
                    //btnSubtraction.Enabled = true;
                    //btnCancelSubtraction.Enabled = true;
                }
                else
                {
                    if (addMinusType == (int)AddMinusType.Add)
                        ShowMessageBoxInformation("未选择可减项的项目");
                    else
                        ShowMessageBoxInformation("未选择可取消减项的项目");
                }
            }
            else
            {
                ShowMessageBoxInformation("未选择项目。");
            }
            //处理体检状态
            EntityDto<Guid> inputup = new EntityDto<Guid>();
            inputup.Id = regid; 
            _DoctorStationAppService.UpCheckState(inputup);
        }

        private void simpleButtonCameraHelper_Click(object sender, EventArgs e)
        {
            if (cameraHelper == null || cameraHelper.IsDisposed)
            {
                cameraHelper = new CameraHelper();
                cameraHelper.TakeSnapshotComplete += (ss, ee) =>
                {
                    var camer = ss as CameraHelper;
                    textEditTijianhao.Text = camer.BarCode;
                    customerIMG = camer.ImageName;
                    if (!string.IsNullOrWhiteSpace(textEditTijianhao.Text))
                    {
                        LoadData(1);
                        if (GuidancePhotoId == Guid.Empty || GuidancePhotoId == null)
                        {
                            //上传
                            var result = _pictureController.Uploading(customerIMG, "Test");
                            GuidancePhotoId = result.Id;
                        }
                        else
                        {
                            var result = _pictureController.Update(customerIMG, GuidancePhotoId.Value);
                            GuidancePhotoId = result.Id;
                        }

                        Jiaobiao();
                    }
                };
                cameraHelper.Show(this);
            }
        }
        public void Jiaobiao()
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;
            
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.SendToConfirm == (int)SendToConfirm.Yes)
            {
                ShowMessageBoxInformation("选定记录已是\"已交表\"状态！");
                return;
            }
            currentDate.SendToConfirm = (int)SendToConfirm.Yes;
            try
            {
                var result = crossTableAppService.EditCrossTableState(new CustomerRegForCrossTableDto { Id = currentDate.Id,
                    SendToConfirm = currentDate.SendToConfirm,
                    GuidancePhotoId = GuidancePhotoId ,
                    SendUserId = CurrentUser.Id
                });
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = currentDate.CustomerBM;
                createOpLogDto.LogName = currentDate.Customer.Name;
                createOpLogDto.LogText = "交表";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.ClientId;
                _commonAppService.SaveOpLog(createOpLogDto);
                //生成未小结科室小结
                EntityDto<Guid> input = new EntityDto<Guid>();
                input.Id = currentDate.Id;
                _DoctorStationAppService.CreateAllNoConclusion(input);
                //处理体检状态
                _DoctorStationAppService.UpCheckState(input);
                if (result != null)
                {
                    if (autoDrawBlood)
                    {
                        if (currentDate.DrawCard != null)
                        {
                            LoadData(null);
                            return;
                        }
                        CustomerBloodNumDto bloodInput = new CustomerBloodNumDto();
                        bloodInput.CustomerReg = new CustomerRegForCrossTableDto { Id = result.Id };
                        bloodInput.EmployeeBM = new Application.Users.Dto.UserViewDto { Id = CurrentUser.Id };
                        bloodInput.BloodSate = (int)BloodState.DrawBlood;
                        bloodInput.Ip = GetLocalIP();
                        crossTableAppService.DrawBlood(bloodInput);
                        LoadData(null);
                    }
                    else
                        LoadData(null);
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }
        public void Jiaobiaop()
        {
            var selectIndexes = gridControlCustomerInfo.GetSelectedRowDtos<CustomerRegForCrossTableViewDto>();
            if (selectIndexes ==null)
            {
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {
                    selectIndexes = new List<CustomerRegForCrossTableViewDto>();
                    selectIndexes.Add(currentDate);

                }
               
            }
            if (selectIndexes.Count == 0)
            {
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {                  
                    selectIndexes.Add(currentDate);

                }
               
            }
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {
                //var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                ////var currentDate = gridView1.getda(index) as CustomerRegForCrossTableViewDto;
                //gridControlCustomerInfo.GetSelectedRowDtos[1];
                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                if (currentDate.SendToConfirm == (int)SendToConfirm.Yes)
                {
                    arNolSIt += currentDate.CustomerBM + "已是\"已交表!";
                   // ShowMessageBoxInformation("选定记录已是\"已交表\"状态！");
                    continue;
                }
                currentDate.SendToConfirm = (int)SendToConfirm.Yes;
                try
                {
                    var result = crossTableAppService.EditCrossTableState(new CustomerRegForCrossTableDto { Id = currentDate.Id, SendToConfirm = currentDate.SendToConfirm, GuidancePhotoId = GuidancePhotoId ,SendUserId = CurrentUser.Id });
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = currentDate.CustomerBM;
                    createOpLogDto.LogName = currentDate.Customer.Name;
                    createOpLogDto.LogText = "交表";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    //生成未小结科室小结
                    EntityDto<Guid> input = new EntityDto<Guid>();
                    input.Id = currentDate.Id;
                    _DoctorStationAppService.CreateAllNoConclusion(input);
                    //处理体检状态
                    _DoctorStationAppService.UpCheckState(input);
                    if (result != null)
                    {
                        if (autoDrawBlood)
                        {
                            if (currentDate.DrawCard != null)
                            {
                               // LoadData(null);
                                continue;
                            }
                            CustomerBloodNumDto bloodInput = new CustomerBloodNumDto();
                            bloodInput.CustomerReg = new CustomerRegForCrossTableDto { Id = result.Id };
                            bloodInput.EmployeeBM = new Application.Users.Dto.UserViewDto { Id = CurrentUser.Id };
                            bloodInput.BloodSate = (int)BloodState.DrawBlood;
                            bloodInput.Ip = GetLocalIP();
                            crossTableAppService.DrawBlood(bloodInput);
                           
                        }
                       // else
                           // LoadData(null);
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
            if(arNolSIt!="")
            {
                MessageBox.Show(arNolSIt);
            }
            LoadData(null);
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
                {                   
                    return;
                }
                LoadData(null);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            //if (currentDate == null)
            //{
            //    ShowMessageBoxInformation("尚未选定任何预约记录！");
            //    return;
            //}
            //if (currentDate.SendToConfirm == (int)SendToConfirm.No)
            //{
            //    ShowMessageBoxInformation("选定记录已是\"未交表\"状态！");
            //    return;
            //}
            var selectIndexes = gridControlCustomerInfo.GetSelectedRowDtos<CustomerRegForCrossTableViewDto>();
            if (selectIndexes == null)
            {
                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {
                    selectIndexes = new List<CustomerRegForCrossTableViewDto>();
                    selectIndexes.Add(currentDate);

                }
              
            }
            if (selectIndexes.Count == 0)
            {

                var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                //var currentDate = (List<CustomerRegForCrossTableViewDto>)gridControlCustomerInfo.DataSource;

                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                else
                {
                  
                    selectIndexes.Add(currentDate);

                }
              
            }
            if (XtraMessageBox.Show("确定取消交表?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string arNolSIt = "";
            foreach (var currentDate in selectIndexes)
            {
                //var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
                ////var currentDate = gridView1.getda(index) as CustomerRegForCrossTableViewDto;
                //gridControlCustomerInfo.GetSelectedRowDtos[1];
                if (currentDate == null)
                {
                    ShowMessageBoxInformation("尚未选定任何预约记录！");
                    return;
                }
                if (currentDate.SendToConfirm == (int)SendToConfirm.No)
                {
                    arNolSIt += currentDate.CustomerBM + "已是\"未交表!";
                    // ShowMessageBoxInformation("选定记录已是\"已交表\"状态！");
                    continue;
                }
                currentDate.SendToConfirm = (int)SendToConfirm.No;
                try
                {
                    var result = crossTableAppService.EditCrossTableState(new CustomerRegForCrossTableDto { Id = currentDate.Id, SendToConfirm = currentDate.SendToConfirm , SendUserId = CurrentUser.Id });
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = currentDate.CustomerBM;
                    createOpLogDto.LogName = currentDate.Customer.Name;
                    createOpLogDto.LogText = "取消交表";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    // if (result != null)

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBoxError(ex.ToString());
                }
            }
            if (arNolSIt != "")
            {
                MessageBox.Show(arNolSIt);
            }
            LoadData(null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Jiaobiaop();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            #region 发送短信
            IClientRegAppService _clientRegAppService = new ClientRegAppService(); // 预约仓储
            var MessageModle = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 5)?.Remarks;
            if (string.IsNullOrEmpty(MessageModle))
            {
                XtraMessageBox.Show("请在字典设置“短信模板”，编码为5中设置补检短信模板");
                return;
            }
            var rowList = gridView1.GetSelectedRows();
            foreach (var item in rowList)
            {
                if (gridView1.GetRow(item) is CustomerRegForCrossTableViewDto row)
                {
                    if (row.TjlCusGiveUps.Count > 0)
                    {


                        if (!string.IsNullOrEmpty(row.Customer.Mobile))
                        {
                            ShortMessageDto input = new ShortMessageDto();
                            input.Age = row.Customer.Age;
                            //input.CustomerId = row.CustomerId;
                            input.CustomerRegId = row.Id;
                            var shortMes = MessageModle.Replace("【姓名】", row.Customer.Name);
                            if (row.Customer.Sex == 1)
                            {
                                shortMes = shortMes.Replace("【性别】", "先生");
                            }
                            else if (row.Customer.Sex == 2)
                            {
                                shortMes = shortMes.Replace("【性别】", "女士");
                            }
                            shortMes = shortMes.Replace("【体检号】", row.CustomerBM);

                            //  //补检信息                     
                            var GroupIdlist = row.TjlCusGiveUps.Select(p => p.CustomerItemGroupId).ToList();
                            var GroupNamelist = row.CustomerItemGroup.Where(p=> GroupIdlist.Contains(p.Id)).
                                Select(p=>p.ItemGroupName).ToList();
                            var GroupName = string.Join(",", GroupNamelist);
                            shortMes = shortMes.Replace("【补检项目】", GroupName);
                            shortMes = shortMes.Replace("【补检日期】", row.StayDateFormat.Value.ToString("yy年MM月dd日"));
                            input.Message = shortMes;
                            input.CustomerBM = row.CustomerBM;
                            input.MessageType = 5;
                            input.Mobile = row.Customer.Mobile;
                            input.Name = row.Customer.Name;
                            input.SendState = 0;
                            input.Sex = row.Customer.Sex;
                            _clientRegAppService.SaveMessage(input);
                        }
                    }
                }
            }
            #endregion
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var currentDate = gridControlCustomerInfo.GetFocusedRowDto<CustomerRegForCrossTableViewDto>();
            if (currentDate == null)
            {
                ShowMessageBoxInformation("尚未选定任何预约记录！");
                return;
            }
            if (currentDate.TjlCusGiveUps.Count == 0)
            {
                ShowMessageBoxInformation("该体检人无补检信息！");
                return;
            }

            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id = currentDate.Id;
           var staycheckcus=   customerSvr.SaveSupplementary(input);
          var  ret = PrintGuidanceNew.Print(staycheckcus.Id, false, false, false, false);

            //生成未小结科室小结           
            _DoctorStationAppService.CreateAllNoConclusion(input);
            //处理体检状态
            _DoctorStationAppService.UpCheckState(input);

        }
    }
}
