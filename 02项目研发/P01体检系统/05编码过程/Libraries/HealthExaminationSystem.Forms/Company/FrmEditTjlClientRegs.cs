using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrint;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval;
using Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Comm;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.CusReg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmEditTjlClientRegs : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService; // 预约仓储
        private readonly ICommonAppService _CommonAppService; // 
        private readonly PhysicalExaminationAppService _PhysicalAppService;
        private readonly IClientInfoesAppService _ClientInfoesAppService;
        private readonly IIDNumberAppService _IDNumberAppService;
        private IPersonnelCategoryAppService _personnelCategoryAppService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService=new OccDisProposalNewAppService();
        private IOAApprovalAppService oAApprovalAppService = new OAApprovalAppService();
        private CreateClientXKSetDto createClientXKSetDto = new CreateClientXKSetDto();
        private IChargeAppService _chargeAppService { get; set; }
        //编辑方式
        public int EditMode;
        //单位团体预约Dto
        public ClientTeamRegDto cteDto;
        //单位团体预约ID
        public Guid clireg_Id = Guid.Empty;

        public Guid Cli_infoId;
        private long? UserId  ;
        bool isModified = false;
        List<ClientTeamCusListDto> cusInfoList;

        private SearchItemSuitDto ItemSuitDto = new SearchItemSuitDto(); //套餐所需
        private GetClientVerSionDto getClientVerSionDto = new GetClientVerSionDto();
        #region 人员信息申明变量
        private readonly IClientRegAppService _clientReg = new ClientRegAppService();
        private ICustomerAppService customerSvr;//体检预约 
        private IBarPrintAppService barPrintAppService;//体检预约 
        private readonly List<SexModel> _sexModels;
        public Guid ClientinfoId;   //单位ID
        public Guid ClientRegId;    //单位预约ID
        public Guid TeamId;  //分组ID
        private IItemSuitAppService itemSuitAppSvr;//套餐
        #endregion 申明变量
        public FrmEditTjlClientRegs()
        {
            InitializeComponent();
            _chargeAppService = new ChargeAppService();
            _clientRegAppService = new ClientRegAppService();
            _CommonAppService = new CommonAppService();
            _PhysicalAppService = new PhysicalExaminationAppService();
            _ClientInfoesAppService = new ClientInfoesAppService();
            _IDNumberAppService = new IDNumberAppService();
            _personnelCategoryAppService = new PersonnelCategoryAppService();
            itemSuitAppSvr = new ItemSuitAppService();
            txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
            //单位团体预约Dto
            cteDto = new ClientTeamRegDto();
            cteDto.ListClientTeam = new List<CreateClientTeamInfoesDto>();
            cteDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
            //单位团体预约ID
            if (EditMode == (int)EditModeType.Add)
            {
                clireg_Id = Guid.NewGuid();
                ZanCun();
            }
            repositoryItemLookUpEdit1.DataSource = PrintSateHelper.GetSexModels();
            repositoryItemLookUpEdit2.DataSource= PrintSateHelper.GetSexModels();
            #region 申明变量
            customerSvr = new CustomerAppService();
            barPrintAppService = new BarPrintAppService();
            _sexModels = SexHelper.GetSexModelsForItemInfo();

            #endregion


        }
        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="b1">byte数组1</param>
        /// <param name="b2">byte数组2</param>
        /// <returns>是否相等</returns>
        private bool PasswordEquals(byte[] b1, byte[] b2)
        {
            if (b1.Length != b2.Length) return false;
            if (b1 == null || b2 == null) return false;
            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i])
                    return false;
            return true;
        }
        #region 系统事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            AutoLoading(() =>
            {
                btnSave.Enabled = false;
                if (EditMode == (int)EditModeType.Edit)
                {
                    ///获取预约表版本号
                    var nowVerSion = _clientRegAppService.getClientVerSion(new EntityDto<Guid> { Id = clireg_Id });
                    if (getClientVerSionDto != null && getClientVerSionDto.RowVersion != null &&
                    nowVerSion != null && nowVerSion.RowVersion != null &&
                     !PasswordEquals(nowVerSion.RowVersion, getClientVerSionDto.RowVersion))
                    {
                        
                        XtraMessageBox.Show("单位预约信息，其他人已修改请重新打开页面再编辑！");
                        return;
                    }
                }
                bool isTrue = saveData(true);
                if (isTrue)
                {
                    getClientVerSionDto = _clientRegAppService.getClientVerSion(new EntityDto<Guid> { Id = clireg_Id });
                    EditLoad();
                }
                btnSave.Enabled = true;
            });
            isModified = false;
            //this.Close();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearItem_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            if (gridClientItem.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息！");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (string.IsNullOrWhiteSpace(TeamId))
            {
                ShowMessageBoxInformation("请先选择分组信息！");
                return;
            }
            var teamGuid = new Guid(TeamId);
            var clearTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
            if (clearTeamItem != null && clearTeamItem.Count() > 0)
            {
                for (int i = 0; i < clearTeamItem.Count(); i++)
                {
                    cteDto.ListClientTeamItem.Remove(clearTeamItem[i]);
                }
                gridClientItem.DataSource = null;
                //ItemSuit = null;
                var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);
                regTeam.ItemSuit_Id = null;
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "删除")
            {
                var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
                if (string.IsNullOrWhiteSpace(TeamId))
                {
                    ShowMessageBoxInformation("请先选择分组信息！");
                    return;
                }
                var teamGuid = new Guid(TeamId);
                var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);
                var teamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid);
               
                if (regTeam.RegisterState != null && regTeam.RegisterState == true)
                {
                    ShowMessageBoxInformation("此分组下已有人员登记，无非进行删除！");
                    return;
                }
                else
                {
                    if (teamItem != null && teamItem.Count() > 0)
                    {
                       
                        
                        DialogResult drsuit = XtraMessageBox.Show("此分组下已有项目，确定删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (drsuit == DialogResult.Cancel)
                        {
                            return;
                        }

                    }
                   

                    int TeamPp = Convert.ToInt32(bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "TiJianRenShu"));
                    if (TeamPp > 0)
                    {
                        ShowMessageBoxInformation("此分组下已有人员，请先删除对应分组人员");
                        using (FrmClientRegCustomerList frmCustomer = new FrmClientRegCustomerList())
                        {

                            frmCustomer.ClientRegId = clireg_Id;
                            frmCustomer.TeamId = teamGuid;
                            if (frmCustomer.ShowDialog() == DialogResult.OK)
                            {
                                // EntityDto<Guid> et = new EntityDto<Guid>();

                            }
                        }
                    }


                }
                DialogResult dr = XtraMessageBox.Show("是否删除当前分组？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //删除grid上的行数据
                    bandedGridView1.DeleteRow(bandedGridView1.FocusedRowHandle);

                    //删除缓存中的数据
                    var item = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);
                    //EditLoad();

                }
            }
            if (e.Button.Caption == "修改")
            {

                var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
                var teamGuid = new Guid(TeamId);
                var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);

                using (FrmClientTeamAdd frmTeamAdd = new FrmClientTeamAdd() { EditMode = (int)EditModeType.Edit, TeamInfoes = regTeam })
                {

                    if (frmTeamAdd.ShowDialog() == DialogResult.OK)
                    {
                        grdTeamData.DataSource = null;
                        grdTeamData.DataSource = cteDto.ListClientTeam;
                    }

                }

            }
        }

        #endregion


        //系统加载load事件
        private void EditLoad()
        {
            //绑定下拉框数据
            BindCombData();
            if (EditMode == (int)EditModeType.Edit)
            {
                //读取团体预约所有信息
                cteDto = _clientRegAppService.getClientRegList(new EntityDto<Guid> { Id = clireg_Id });
                ///获取预约表版本号
                getClientVerSionDto = _clientRegAppService.getClientVerSion(new EntityDto<Guid> { Id = clireg_Id });

                //绑定预约信息
                EditClientReg(cteDto.ClientRegDto);
                //绑定分组信息
                if (cteDto.ListClientTeam != null && cteDto.ListClientTeam.Count > 0)
                {
                    foreach (var item in cteDto.ListClientTeam)
                    {
                        item.EditModle = true;
                        if (item.EditModle == true)
                        {

                            btnClearItem.Enabled = false;
                        }
                        else
                        {
                            // btnChoiceItemSuit.Enabled = true;
                        }
                    }
                    grdTeamData.DataSource = cteDto.ListClientTeam;
                    var teamId = cteDto.ListClientTeam.Select(o => o.Id).FirstOrDefault();
                    //绑定分组项目信息
                    if (cteDto.ListClientTeamItem != null && cteDto.ListClientTeamItem.Count > 0)
                    {
                        var selItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamId);
                        if (selItem != null && selItem.Count() > 0)
                        {
                            gridClientItem.DataSource = ItemGropsSort(selItem?.ToList());
                        }
                    }
                }
                //格式化数据
                formatGrid();

                //判断是否禁用
                WhetherReadOnly();

                ClientRegId = clireg_Id;

            }
            else
            {
                //customGridLookUpEdit1.Properties.DataSource = _ClientInfoesAppService.Query(new ClientInfoesListInput());
                var clientPaaent = _ClientInfoesAppService.QueryClientName(new ChargeBM());
                customGridLookUpEdit1.Properties.DataSource = clientPaaent;
                //开始时间
                txtStartCheckDate.DateTime = _CommonAppService.GetDateTimeNow().Now;
                //结束时间
                txtEndCheckDate.DateTime = txtStartCheckDate.DateTime;
                //盲检
                LeBlindSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                LeBlindSate.ItemIndex = 0;
                //封账
                txtFZState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                txtFZState.ItemIndex = 1;
                //锁定
                txtSDState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                txtSDState.ItemIndex = 1;
                //到检
                txtControlDate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                txtControlDate.ItemIndex = 1;
                //散检
                txtClientSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                txtClientSate.ItemIndex = 0;
                //已检
                txtClientCheckSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                txtClientCheckSate.ItemIndex = 1;

                //编辑状态
                txtFZState.Enabled = false;
                txtSDState.Enabled = false;
                //txtControlDate.Enabled = false;
                txtClientCheckSate.Enabled = false;
                txtClientRegBM.Text = _IDNumberAppService.CreateClientRegBM();
                txtClientRegNum.Text = "1";
                WhetherReadOnly();
            }

        }
        #region 系统加载
        bool first = false;
        private void FrmEditTjlClientRegs_Load(object sender, EventArgs e)
        {
            var UsersListOutputDto = DefinedCacheHelper.GetComboUsers();
            if (UsersListOutputDto != null && UsersListOutputDto.Count > 0)
            {
                // BindCustomGridLookUpEdit<UserFormDto>.BindGridLookUpEdit(gluClientDegree, UsersListOutputDto, "Name", "Id", "Name", 15, "Id");
                gluClientDegree.Properties.DataSource = UsersListOutputDto;
                //gluClientDegree.EditValue = CurrentUser.Id;
            }
            first = true;
            bandedGridView1.IndicatorWidth = 50;
            gridView2.IndicatorWidth = 50;
            gvCustomerRegs.IndicatorWidth = 50;

            EditLoad();
            // bandedGridView1.OptionsSelection.MultiSelect = true;
            bandedGridView1.OptionsSelection.MultiSelect = true;
            
            bandedGridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
           
            first = false;
            repositoryItemLookUpEdit8.DataSource = IsZYBStateHelper.GetZYBStateModels();
            createClientXKSetDto = oAApprovalAppService.getCreateClientXKSet();
            gvCustomerRegs.Columns[conRegMessageState.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gvCustomerRegs.Columns[conRegMessageState.FieldName].DisplayFormat.Format = new CustomFormatter(ShortMessageStateHelper.ShortMessageStateFormatter);


        }

        /// <summary>
        /// 编辑绑定预约信息
        /// </summary>
        public void EditClientReg(CreateClientRegDto item)
        {
            customGridLookUpEdit1.Enabled = false;
            txtClientRegBM.Text = item.ClientRegBM.ToString();
            txtClientRegNum.Text = item.ClientRegNum.ToString();
            //txtRegPersonCount.Text = item.RegPersonCount.ToString();
            txtLinkMan.EditValue = item.linkMan;

            txtStartCheckDate.DateTime = item.StartCheckDate;
            txtEndCheckDate.DateTime = item.EndCheckDate;
            LeBlindSate.EditValue = EnumHelper.GetEnumDescs(typeof(BlindSate))[item.BlindSate - 1].Key;
            txtFZState.EditValue = EnumHelper.GetEnumDescs(typeof(FZState))[item.FZState - 1].Key;
            txtSDState.EditValue = EnumHelper.GetEnumDescs(typeof(SDState))[item.SDState - 1].Key;
            txtControlDate.EditValue = EnumHelper.GetEnumDescs(typeof(ControlDate))[item.ControlDate - 1].Key;
            txtClientSate.EditValue = EnumHelper.GetEnumDescs(typeof(ClientSate))[item.ClientSate - 1].Key;
            txtClientCheckSate.EditValue = EnumHelper.GetEnumDescs(typeof(ClientCheckSate))[item.ClientCheckSate.Value - 1].Key;
            txtRemark.Text = item.Remark;
            txtRegInfo.Text = item.RegInfo;
            customGridLookUpEdit1.EditValue = item.ClientInfo_Id;
            //customGridLookUpEdit1.EditValue = item.ClientInfo.Id;
            clireg_Id = item.Id;
            txtPersonnelCategory.EditValue = item.PersonnelCategoryId;
            if (item.UserId.HasValue)
            {
                gluClientDegree.EditValue = item.UserId;
            }
            if (item.ReportDays.HasValue)
            {
                spinEditDays.EditValue = item.ReportDays;
            }
        }

        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        public void BindCombData()
        {
            //单位信息
            //var clientList = _ClientInfoesAppService.Query(new ClientInfoesListInput());
            ////封账处理，后面解决            
            //customGridLookUpEdit1.Properties.DataSource = clientList;
            ChargeBM chargeBM = new ChargeBM();
            if (EditMode == (int)EditModeType.Edit)
            {
                chargeBM.Id = Cli_infoId;
            }
            var clientPaaent = _ClientInfoesAppService.QueryClientName(chargeBM);
            customGridLookUpEdit1.Properties.DataSource = clientPaaent;

            //盲检
            LeBlindSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(BlindSate));
            LeBlindSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //封账
            txtFZState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(FZState));
            txtFZState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //锁定
            txtSDState.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(SDState));
            txtSDState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //到检
            txtControlDate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ControlDate));
            txtControlDate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //散检
            txtClientSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ClientSate));
            txtClientSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            //已检
            txtClientCheckSate.Properties.DataSource = EnumHelper.GetEnumDescs(typeof(ClientCheckSate));
            txtClientCheckSate.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //套餐
            var suitlist= DefinedCacheHelper.GetItemSuit().Where(o => o.ItemSuitType == 1 || o.ItemSuitType == (int)ItemSuitType.OnLine).ToList();
            cuslookItemSuit.Properties.DataSource = suitlist.Where(p => p.IsendDate != 1 || (p.IsendDate == 1 && p.endDate >= System.DateTime.Now)).ToList();
        }


        /// <summary>
        /// 格式化表格数据
        /// </summary>
        private void formatGrid()
        {

            //bandedGridView1.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[3].DisplayFormat.Format = new CustomFormatter(SumYSMoney);//应付金额
            //bandedGridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[4].DisplayFormat.Format = new CustomFormatter(SumSJMoney);//实付金额

            //bandedGridView1.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[2].DisplayFormat.Format = new CustomFormatter(SumTJRS);//人数

            //bandedGridView1.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[6].DisplayFormat.Format = new CustomFormatter(FormatSex);//性别
            //bandedGridView1.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[7].DisplayFormat.Format = new CustomFormatter(FormatAge);//年龄
            //bandedGridView1.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[8].DisplayFormat.Format = new CustomFormatter(FormatMarry);//婚状
            //bandedGridView1.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[9].DisplayFormat.Format = new CustomFormatter(FormatYungZ);//孕状
            //bandedGridView1.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[10].DisplayFormat.Format = new CustomFormatter(FormatAge);//年龄
        }

        /// <summary>
        /// 判断是否编辑状态
        /// </summary>
        public void WhetherReadOnly()
        {
            //int it = _clientRegAppService.GetClientTeam(et).Count;

            //封账、体检完成：不能添加项目与人员
            if ((int)txtClientCheckSate.EditValue == (int)ClientCheckSate.Complete || (int)txtFZState.EditValue == (int)FZState.Already)
            {
                //btnChoiceItemSuit.Enabled = false;
                //btn1X.Enabled = false;
                btnChoiceItem.Enabled = false;
                butX.Enabled = false;
                btnTeamItem.Enabled = false;
                //btnUserList.Enabled = false;
                btnClearItem.Enabled = false;
            }
            //人员登记后不可编辑

            if (cteDto.ClientRegDto.RegisterState != null && cteDto.ClientRegDto.RegisterState == true)
            {
                //btnChoiceItemSuit.Enabled = false;
                //btn1X.Enabled = false;
                btnChoiceItem.Enabled = false;
                butX.Enabled = false;
                btnTeamItem.Enabled = false;
                btnClearItem.Enabled = false;
            }
            //是新增还是编辑
            if (EditMode == (int)EditModeType.Add)
            {
                btnaddTeam.Enabled = true;
                btnTeam.Enabled = true;
                //btnUserList.Enabled = true;
            }
            //else
            //{
            //    btnaddTeam.Enabled = false;
            //    btnTeam.Enabled = false;
            //}
        }
        #endregion 系统加载

        #region 添加分组
        private void btnaddTeam_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return ;
            }
            int teamCode = 1;
            if (cteDto.ListClientTeam != null&& cteDto.ListClientTeam.Count>0)
            {
                //teamCode = cteDto.ListClientTeam.Count + 1;
                teamCode = cteDto.ListClientTeam.Max(o => o.TeamBM) + 1;
            }

            
            using (FrmClientTeamAdd frmclientitemadd = new FrmClientTeamAdd(clireg_Id, null) { EditMode = (int)EditModeType.Add, teamCode = teamCode })
            {
                
                frmclientitemadd.TeamInfoesList = cteDto.ListClientTeam;
                frmclientitemadd.SaveDataComplate += (data) =>
                {
                    
                        cteDto.ListClientTeam.Add(data);                                      
                };
                if (frmclientitemadd.ShowDialog() == DialogResult.OK)
                {
                    grdTeamData.DataSource = null;
                    EntityDto<Guid> et = new EntityDto<Guid>();
                    et.Id = clireg_Id;
                    grdTeamData.DataSource = cteDto.ListClientTeam; //_clientRegAppService.GetClientTeam(et);

                    bandedGridView1.UnselectRow(0);
                    bandedGridView1.SelectRow(bandedGridView1.RowCount - 1);
                    bandedGridView1.FocusedRowHandle = bandedGridView1.RowCount - 1;
                    gdvCustomerReg.DataSource = null;

                    FocusedRow();
                    formatGrid();
                    WhetherReadOnly();
                }
            }

        }
        #endregion 添加分组

        #region 分组管理
        private void btnTeam_Click(object sender, EventArgs e)
        {
            //if (EditMode == (int)EditModeType.Add)
            //{
            //    ShowMessageBoxInformation("请先保存预约信息！");
            //    return;
            //}
            if (grdTeamData.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            using (FrmTjlClientTeamAdd frmTeamAdd = new FrmTjlClientTeamAdd())
            {
                //var listTeam = new List<CreateClientTeamInfoesDto>();
                //listTeam = cteDto.ListClientTeam;
                frmTeamAdd.TeamInfoesList = cteDto.ListClientTeam;
                frmTeamAdd.ClientReg = cteDto.ClientRegDto;               
                frmTeamAdd.SaveDataTeamInfoes += (TeamInfoList) =>
                {

                };

                if (frmTeamAdd.ShowDialog() == DialogResult.OK)
                {
                    //重新绑定分组信息列表
                    grdTeamData.RefreshDataSource();                   
                    //grdTeamData.DataSource = null;
                    //grdTeamData.DataSource = cteDto.ListClientTeam;
                }
            }

        }
        #endregion 分组管理

        #region 人员管理
        private void btnUserList_Click(object sender, EventArgs e)
        {
            //厉害噢，这代码写的真硬！
            //bool IsNoSave = saveData(false);
            //if (!IsNoSave)
            //{
            //    return;
            //}
            //// 上面都保存了……
            //if (EditMode == (int)EditModeType.Add)
            //{
            //    ShowMessageBoxInformation("请先保存预约信息！");
            //    return;
            //}
            //using (FrmClientRegCustomerList frmcuslist = new FrmClientRegCustomerList())
            //{
            //    frmcuslist.ClientRegId = clireg_Id;
            //    frmcuslist.cteDto = cteDto;
            //    //frmcuslist.ListClientTeam = cteDto.ListClientTeam;
            //    //frmcuslist.ListClientTeamItem = cteDto.ListClientTeamItem;

            //    if (frmcuslist.ShowDialog() == DialogResult.OK)
            //    {
            //        EntityDto<Guid> et = new EntityDto<Guid>();
            //        EditLoad();
            //    }
            //}

            if (EditMode == (int)EditModeType.Add)
            {
                ShowMessageBoxInformation("请先保存预约信息！");
                return;
            }
            using (FrmClientRegCustomerList frmcuslist = new FrmClientRegCustomerList())
            {
                frmcuslist.ClientRegId = clireg_Id;
                frmcuslist.cteDto = cteDto;
                frmcuslist.ShowDialog();
            }
        }
        #endregion 人员管理

        #region 选择套餐

        /// <summary>
        /// 根据条件搜索套餐信息
        /// </summary>
        /// <param name="dto"></param>
        public List<SimpleItemSuitDto> SelectItemSuit(SearchItemSuitDto dto)
        {
            var Suits = DefinedCacheHelper.GetItemSuit().Where(o => (o.ItemSuitType == (int)ItemSuitType.Suit || o.ItemSuitType == (int)ItemSuitType.OnLine)  && o.Available == 1);
            if (dto != null)
            {
                if (dto.Sex.HasValue)
                    Suits = Suits.Where(o => o.Sex == dto.Sex);
            }
            return Suits?.ToList();
        }

        /// <summary>
        /// 选择套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoiceItemSuit_Click(object sender, EventArgs e)
        {

            //ItemSuitDto.ItemSuitType = Convert.ToInt32(ItemSuitType.Suit);
            //ItemSuitDto.Available = 1;
            if (grdTeamData.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (string.IsNullOrWhiteSpace(TeamId))
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var teamGuid = new Guid(TeamId);
            var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);


            using (FrmSelectItemSuit frmSelectItemSuit = new FrmSelectItemSuit(regTeam))
            {
                frmSelectItemSuit.InputisOnePlus = false;
                frmSelectItemSuit.SaveDataComplateForGroup += (ItemSuit, TeamRegitem) =>
                  {
                      if (ItemSuit.Id != regTeam.ItemSuit_Id && regTeam.ItemSuit_Id.HasValue)
                      {
                          DialogResult dr = DevExpress.XtraEditors.XtraMessageBox.Show("您已经选择【" + regTeam.ItemSuitName + "】是否切换为【" + ItemSuit.ItemSuitName + "】？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                          if (dr != DialogResult.OK)
                              return;
                      }
                      if (ItemSuit != null)
                      {
                          regTeam.ItemSuit_Id = ItemSuit.Id;
                          regTeam.ItemSuitName = ItemSuit.ItemSuitName;
                      }
                      if (TeamRegitem != null && TeamRegitem.Count > 0)
                      {
                          if (cteDto.ListClientTeamItem
                          != null && cteDto.ListClientTeamItem.Count > 0)
                          {
                              var delCliTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
                              if (delCliTeamItem != null && delCliTeamItem.Count() > 0)
                                  for (int i = 0; i < delCliTeamItem.Count(); i++)
                                  {
                                      cteDto.ListClientTeamItem.Remove(delCliTeamItem[i]);
                                  }
                          }
                          else
                              cteDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
                          //cteDto.ListClientTeamItem.Remove()
                          foreach (var regItem in TeamRegitem)
                          {
                              if (regItem.Id == null || regItem.Id == Guid.Empty)
                                  regItem.Id = Guid.NewGuid();
                              if (regItem.ClientRegId == null || regItem.ClientRegId == Guid.Empty)
                                  regItem.ClientRegId = clireg_Id;
                              cteDto.ListClientTeamItem.Add(regItem);
                          }
                      }
                  };
                if (frmSelectItemSuit.ShowDialog() == DialogResult.OK)
                {
                    gridClientItem.DataSource = null;
                    gridClientItem.DataSource = ItemGropsSort(cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid)?.ToList());

                }
                //frmSelectItemSuit.ItemSuitDto = ItemSuitDto;

            }
        }

        #endregion 选择套餐

        #region 选择1+X
        /// <summary>
        /// 选择1+X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1X_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            //ItemSuitDto.ItemSuitType = Convert.ToInt32(ItemSuitType.OnePlusX);
            //ItemSuitDto.Available = 1;
            if (grdTeamData.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (string.IsNullOrWhiteSpace(TeamId))
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var teamGuid = new Guid(TeamId);
            var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);

            //获取项目信息
            if (cteDto.ListClientTeamItem == null || cteDto.ListClientTeamItem.Count == 0)
                cteDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
            //var listTeamItem = new List<ClientTeamRegitemViewDto>();
            var listTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();

            using (FrmSelectItemSuit frmSelectItemSuit = new FrmSelectItemSuit(regTeam, listTeamItem))
            {

                bool _isCheckSate = true;
                if (EditMode == (int)EditModeType.Add)
                    _isCheckSate = false;

                frmSelectItemSuit.SeleteItemGroup(listTeamItem, regTeam, false, _isCheckSate);
                frmSelectItemSuit.InputisOnePlus = true;
                frmSelectItemSuit.SaveDataComplateForGroup += (ItemSuit, TeamRegitem) =>
                {
                    if (TeamRegitem != null && TeamRegitem.Count > 0)
                    {
                        if (cteDto.ListClientTeamItem != null && cteDto.ListClientTeamItem.Count > 0)
                        {
                            var delCliTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
                            if (delCliTeamItem != null && delCliTeamItem.Count() > 0)
                                for (int i = 0; i < delCliTeamItem.Count(); i++)
                                {
                                    cteDto.ListClientTeamItem.Remove(delCliTeamItem[i]);
                                }
                        }
                        foreach (var regItem in TeamRegitem)
                        {
                            if (regItem.Id == null || regItem.Id == Guid.Empty)
                                regItem.Id = Guid.NewGuid();
                            if (regItem.ClientRegId == null || regItem.ClientRegId == Guid.Empty)
                                regItem.ClientRegId = clireg_Id;
                            cteDto.ListClientTeamItem.Add(regItem);
                        }
                    }
                };
                if (frmSelectItemSuit.ShowDialog() == DialogResult.OK)
                {
                    gridClientItem.DataSource = null;
                    gridClientItem.DataSource = ItemGropsSort(cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid)?.ToList());
                }
                //frmSelectItemSuit.ItemSuitDto = ItemSuitDto;

            }

        }
        #endregion

        #region 选择组合

        /// <summary>
        /// 选择组合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoiceItem_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            if (grdTeamData.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (string.IsNullOrWhiteSpace(TeamId))
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var teamGuid = new Guid(TeamId);
            //分组信息
            //var regTeam = new CreateClientTeamInfoesDto();
            var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);
            //分组项目信息
            if (cteDto.ListClientTeamItem == null || cteDto.ListClientTeamItem.Count == 0)
                cteDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
            //var listTeamItem = new List<ClientTeamRegitemViewDto>();
            var listTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
            bool _isCheckSate = true;
            if (EditMode == (int)EditModeType.Add)
                _isCheckSate = false;

            using (FrmSeleteItemGroup frmSeleteItemGroup = new FrmSeleteItemGroup(listTeamItem, regTeam, false, _isCheckSate))
            {
                frmSeleteItemGroup.SaveDataComplateForGroup += (TeamRegitem) =>
                  {
                      if (cteDto.ListClientTeamItem != null && cteDto.ListClientTeamItem.Count > 0)
                      {
                          var delCliTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
                          if (delCliTeamItem != null && delCliTeamItem.Count() > 0)
                              for (int i = 0; i < delCliTeamItem.Count(); i++)
                              {
                                  cteDto.ListClientTeamItem.Remove(delCliTeamItem[i]);
                              }
                      }
                      foreach (var regItem in TeamRegitem)
                      {
                          if (regItem.Id == null || regItem.Id == Guid.Empty)
                              regItem.Id = Guid.NewGuid();
                          if (regItem.ClientRegId == null || regItem.ClientRegId == Guid.Empty)
                              regItem.ClientRegId = clireg_Id;
                          cteDto.ListClientTeamItem.Add(regItem);
                      }
                  };
                if (frmSeleteItemGroup.ShowDialog() == DialogResult.OK)
                {
                    gridClientItem.DataSource = null;
                    gridClientItem.DataSource = ItemGropsSort(cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid)?.ToList());
                }
            }
        }
        #endregion 选择组合

        #region 复制分组项目

        /// <summary>
        /// 复制分组项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeamItem_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            using (FrmCopyTeamItem frmCopyTeamItem = new FrmCopyTeamItem())
            {
                frmCopyTeamItem.regId = clireg_Id;
                if (grdTeamData.DataSource != null)
                {
                    var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
                    if (!string.IsNullOrWhiteSpace(TeamId))
                        frmCopyTeamItem.TeamId = Guid.Parse(TeamId);
                }
                //返回数据进行增加处理
                frmCopyTeamItem.SaveTeamItem += (TeamInfo, ItemInfo) =>
                  {
                      if (TeamInfo != null && TeamInfo.Count > 0)
                      {
                          foreach (var team in TeamInfo)
                          {
                              var listItem = ItemInfo.Where(o => o.ClientTeamInfoId == team.Id).ToList();
                              team.Id = Guid.NewGuid();
                              foreach (var item in listItem)
                              {
                                  item.Id = Guid.NewGuid();
                                  item.ClientTeamInfoId = team.Id;
                                  item.ItemGroup = null;
                                  cteDto.ListClientTeamItem.Add(item);
                              }
                              var teamCode = cteDto.ListClientTeam.Count + 1;
                              if (cteDto.ListClientTeam.Count > 0)
                              {
                                    teamCode = cteDto.ListClientTeam.Max(p => p.TeamBM) + 1;
                              }
                              team.TeamBM = teamCode;
                              cteDto.ListClientTeam.Add(team);
                          }
                      }
                      else
                      {
                          foreach (var item in ItemInfo)
                          {
                              if (cteDto.ListClientTeamItem.Any(o => o.TbmItemGroupid == item.TbmItemGroupid && o.ClientTeamInfoId == item.ClientTeamInfoId))
                                  continue;
                              cteDto.ListClientTeamItem.Add(item);
                          }
                      }
                  };
                if (frmCopyTeamItem.ShowDialog() == DialogResult.OK)
                {
                    //重新绑定数据
                    grdTeamData.DataSource = null;
                    grdTeamData.DataSource = cteDto.ListClientTeam;

                    gridClientItem.DataSource = null;

                    bandedGridView1.UnselectRow(0);
                    bandedGridView1.SelectRow(bandedGridView1.RowCount - 1);
                    bandedGridView1.FocusedRowHandle = bandedGridView1.RowCount - 1;
                    gdvCustomerReg.DataSource = null;
                    FocusedRow();
                    //gridClientItem.DataSource = cteDto.ListClientTeamItem.Where(o=>o.ClientTeamInfoId==);
                }
            }
        }
        #endregion


        #region 方法
        //FormatCheckSate
        private string FormatMarry(object arg)
        {
            switch (arg.ToString())
            {
                case "10":
                    return "未婚";
                case "20":
                    return "已婚";
                case "21":
                    return "初婚";
                case "22":
                    return "再婚";
                case "23":
                    return "复婚";
                case "30":
                    return "丧偶";
                case "40":
                    return "离婚";
                default:
                    return "不明";
            }
        }
        private string FormatYungZ(object arg)
        {
            if (arg.ToString() == "0")
            {
                return "备孕";
            }
            else if (arg.ToString() == "1")
            {
                return "已孕";
            }
            else
            {
                return "哺乳期";
            }
        }
        private string FormatAge(object arg)
        {
            return arg + "岁";
        }

        private string FormatSex(object arg)
        {
            if (arg.ToString() == "1")
            {
                return "男";
            }
            else if (arg.ToString() == "2")
            {
                return "女";
            }
            else
            {
                return "不限";
            }
        }
        private string FormatClienCheck(object arg)
        {
            if (arg.ToString() == "1")
            {
                return "完成";
            }
            else
            {
                return "未完成";
            }
        }
        private string FormatCheckSate(object arg)
        {
            if (arg.ToString() == "1")
            {
                return "未到检";
            }
            else
            {
                return "已到检";
            }
        }
        //private string FormatSexs(object arg)
        //{
        //    try
        //    {

        //        return _sexModels.Find(r => r.Id == (int)arg).Display;
        //    }
        //    catch
        //    {
        //        return _sexModels.Find(r => r.Id == (int)Sw.Hospital.HealthExaminationSystem.Common.Enums.Sex.GenderNotSpecified).Display;
        //    }
        //}
        private string SumTJRS(object arg)
        {

            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Count.ToString();
            return TJRS;

        }
        //SumSJ
        private string SumSJRS(object arg)
        {
            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Where(r => r.CheckSate != 1).Count().ToString();
            return TJRS;
        }

        private string SumWJRS(object arg)
        {
            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Where(r => r.CheckSate == 1).Count().ToString();
            return TJRS;
        }
        private string SumSJMoney(object arg)
        {

            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        //SumJX
        private string SumAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumYSMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumJQX(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXJXMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustMinusMoney);
            string ss = sumMcusPayMoneys.Value.ToString();
            return sumMcusPayMoneys.Value.ToString();
        }

        private void ZanCun()
        {

            var stratTime = txtStartCheckDate.DateTime;

            var endTime = txtEndCheckDate.DateTime;


            var clientRegDto = new CreateClientRegDto();
            clientRegDto.Id = clireg_Id;

            clientRegDto.StartCheckDate = Convert.ToDateTime(stratTime);
            clientRegDto.EndCheckDate = Convert.ToDateTime(endTime);


            cteDto.ClientRegDto = clientRegDto;
        }


        #region 保存方法

        /// <summary>
        /// 保存团体预约信息
        /// </summary>
        private bool saveData(bool IsNo)
        {
            dxErrorProvider.ClearErrors();
            var ClientRegBM = txtClientRegBM.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientRegBM))
            {
                dxErrorProvider.SetError(txtClientRegBM, string.Format(Variables.MandatoryTips, "编码"));
                txtClientRegBM.Focus();
                return false;
            }            
            var ClientInfo = customGridLookUpEdit1.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientInfo))
            {
                dxErrorProvider.SetError(customGridLookUpEdit1, string.Format(Variables.MandatoryTips, "单位不可为空"));
                customGridLookUpEdit1.Focus();
                return false;
            }

            var ClientRegNum = txtClientRegNum.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientRegNum))
            {
                dxErrorProvider.SetError(txtClientRegNum, string.Format(Variables.MandatoryTips, "次数"));
                txtClientRegNum.Focus();
                return false;
            }
            //var RegPersonCount = txtRegPersonCount.Text.Trim();
            //if (string.IsNullOrWhiteSpace(RegPersonCount))
            //{
            //    dxErrorProvider.SetError(txtRegPersonCount, string.Format(Variables.MandatoryTips, "人数"));
            //    txtRegPersonCount.Focus();
            //    return false;
            //}           
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账不能保存！");
                return false;
            }
            var linkMan = txtLinkMan.Text.Trim();
            if (string.IsNullOrWhiteSpace(linkMan))
            {
                dxErrorProvider.SetError(txtLinkMan, string.Format(Variables.MandatoryTips, "单位负责人"));
                txtLinkMan.Focus();
                return false;
            }

            if (Convert.ToDateTime(txtStartCheckDate.EditValue) > Convert.ToDateTime(txtEndCheckDate.EditValue))
            {
                dxErrorProvider.SetError(txtStartCheckDate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                txtStartCheckDate.Focus();
                return false;
            }

            if (cteDto.ListClientTeam == null || cteDto.ListClientTeam.Count == 0)
            {
                ShowMessageBoxInformation("请添加分组信息！");
                return false;
            }
            foreach (var teamInfo in cteDto.ListClientTeam)
            {
                var teamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamInfo.Id);
                // 你能找到一个 teamItem 项是 NULL 的情况吗
                // 难道程序不会崩溃吗
                //if (teamItem == null || teamItem.Count() == 0)
                //{
                //    ShowMessageBoxInformation(string.Format("{0}下没有选择项目", teamInfo.TeamName));
                //    return false;
                //}
                if (teamInfo.Sex != (int)Sex.GenderNotSpecified && teamInfo.ItemSuit_Id.HasValue)
                {
                    var suit = DefinedCacheHelper.GetItemSuit().FirstOrDefault(o => o.Id == teamInfo.ItemSuit_Id);
                    if (suit != null)
                        if (suit.Sex != (int)Sex.GenderNotSpecified && suit.Sex != teamInfo.Sex)
                        {
                            var strSex = EnumHelper.GetEnumDesc((Sex)teamInfo.Sex);
                            ShowMessageBoxWarning(string.Format("分组【{0}】套餐适用性别为【{1}】，请修改分组适用性别后再保存。", teamInfo.TeamName, strSex));
                            return false;
                        }
                }

            }
            var stratTime = txtStartCheckDate.EditValue;

            var endTime = txtEndCheckDate.EditValue;

            var BlindSate = LeBlindSate.EditValue;

            var FZState = txtFZState.EditValue;

            var SDState = txtSDState.EditValue;

            var ControlDate = txtControlDate.EditValue;

            var ClientSate = txtClientSate.EditValue;

            var ClientCheckSate = txtClientCheckSate.EditValue;

            var Remark = txtRemark.Text.Trim();


            var clientRegDto = new CreateClientRegDto();
            clientRegDto.Id = clireg_Id;
            clientRegDto.ClientRegBM = ClientRegBM;
            clientRegDto.ClientRegNum = Convert.ToInt32(ClientRegNum);
            clientRegDto.RegPersonCount = 0;
            clientRegDto.linkMan = linkMan;
            clientRegDto.StartCheckDate = Convert.ToDateTime(stratTime);
            clientRegDto.EndCheckDate = Convert.ToDateTime(endTime);
            clientRegDto.BlindSate = (int)BlindSate;
            clientRegDto.FZState = (int)FZState;
            clientRegDto.SDState = (int)SDState;
            clientRegDto.ControlDate = (int)ControlDate;
            clientRegDto.ClientSate = (int)ClientSate;
            clientRegDto.ClientCheckSate = (int)ClientCheckSate;
            clientRegDto.Remark = Remark;
            clientRegDto.RegInfo = txtRegInfo.Text.Trim();
            clientRegDto.ClientInfo_Id = (Guid)customGridLookUpEdit1.EditValue;
            if (!string.IsNullOrEmpty(gluClientDegree.EditValue?.ToString()))
            {
                UserId = (long)gluClientDegree.EditValue;
            }
            if (!string.IsNullOrEmpty(spinEditDays.EditValue?.ToString()))
            {
                clientRegDto.ReportDays = Convert.ToInt32(spinEditDays.EditValue.ToString());
            }
            else
            {
                clientRegDto.ReportDays = null;
            }
            clientRegDto.UserId = UserId;
            clientRegDto.linkMan = linkMan;
            if (string.IsNullOrWhiteSpace(txtPersonnelCategory.EditValue?.ToString()))
                clientRegDto.PersonnelCategoryId = null;
            else
                clientRegDto.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
            cteDto.ClientRegDto = clientRegDto;
            try
            {
                CreateClientRegDto clientReg = null;
                if (EditMode == (int)EditModeType.Add)
                    clientReg = _clientRegAppService.AddClientReg(cteDto);
                else if (EditMode == (int)EditModeType.Edit)
                    clientReg = _clientRegAppService.EditClientReg(cteDto);
                //var clientReg = _clientRegAppService.insertClientReag(clientRegDto);
                //txtClientRegNum.Text = (clientReg.ClientRegNum + 1).ToString();
                clireg_Id = clientReg.Id;
                WhetherReadOnly();
                // 在这里判断 clientReg 不等于 NULL 有何用
                // 难道不知道在取 Id 值的时候程序已经崩溃了吗
                if (clientReg != null)
                {
                    EditMode = (int)EditModeType.Edit;
                    if (IsNo)
                    {
                        //ShowMessageSucceed("保存成功！");
                    }
                    foreach (var item in cteDto.ListClientTeam)
                    {
                        item.EditModle = true;
                    }

                    customGridLookUpEdit1.Enabled = false;
                    // 保存数据不重新加载，导致项目分组部分数据缺失
                    // 真不知道首次选项目是怎么加载的项目
                    //EditLoad();
                    return true;
                }
                else
                {
                    ShowMessageSucceed("保存失败！");
                    return false;
                }
            }
            catch (UserFriendlyException ex)
            {
                btnSave.Enabled = true;
                ShowMessageBox(ex);
                return false;
            }

        }
        #endregion

        /// <summary>
        /// 行焦点改变时后触发
        /// </summary>
        public void FocusedRow()
        {
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            Guid tmId = new Guid(TeamId);
            var ClientTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == tmId);
            if (ClientTeam.EditModle == true)
            {

                btnClearItem.Enabled = false;
                if (ClientTeam.RegisterState != null && ClientTeam.RegisterState == true)
                {
                    //btnChoiceItemSuit.Enabled = false;
                    //btn1X.Enabled = false;
                    //btnChoiceItem.Enabled = false;  //暂时允许分组内有人员登记也可以进行项目更改
                    btnTeamItem.Enabled = false;
                }
                else
                {
                    //btnChoiceItemSuit.Enabled = true;
                   // btn1X.Enabled = true;
                    btnChoiceItem.Enabled = true;
                    butX.Enabled = true;
                    btnTeamItem.Enabled = true;
                }
            }
            else
            {
                //btnChoiceItemSuit.Enabled = true;
                //btn1X.Enabled = true;
                btnChoiceItem.Enabled = true;
                butX.Enabled = true;
                btnTeamItem.Enabled = true;
                btnClearItem.Enabled = true;
            }

            ClientItemReload(tmId);
            ItemSuitDto.Sex = Convert.ToInt32(bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Sex"));
            ItemSuitDto.MinAge = Convert.ToInt32(bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "MinAge"));
            ItemSuitDto.MaxAge = Convert.ToInt32(bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "MaxAge"));
        }

        #region 点击分组查看套餐

        /// <summary>
        /// 点击分组查看套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
         
           
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
             
              
                FocusedRow();
                updateTeamCus();

            }




        }
        #endregion

        #region 套餐组合展示

        /// <summary>
        /// 套餐项目显示
        /// </summary>
        /// <param name="TeamId"></param>
        public void ClientItemReload(Guid TeamId)
        {
            AutoLoading(() =>
            {
                if (cteDto.ListClientTeamItem == null || cteDto.ListClientTeamItem.Count == 0)
                {

                    gridClientItem.DataSource = null;
                    return;
                }
                var clientItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == TeamId).ToList();
                //var clientItem = _clientRegAppService.GetClientTeamRegByClientId(new SearchClientTeamInfoDto() { Id = TeamId });

                gridClientItem.DataSource = clientItem.Any() ? ItemGropsSort(clientItem.ToList()) : null;
            });
        }

        #endregion

        #region 单位选择

        private void customGridLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            var dto = customGridLookUpEdit1.GetSelectedDataRow() as ClientInfosNameDto;
            //var dtof = customGridLookUpEdit1.GetSelectedDataRow();

            if (dto != null)
            {
                if (string.IsNullOrWhiteSpace(dto.LinkMan))
                {
                }
                else
                {
                    txtLinkMan.Text = dto.LinkMan;
                }
            }
            else
            {
                txtLinkMan.Text = "";
                return;
            }

            // UserId = CurrentUser.Id;
            if (!string.IsNullOrEmpty(gluClientDegree.EditValue?.ToString()))
            {
                UserId = (long)gluClientDegree.EditValue;
            }
            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id = dto.Id;
            int count = _clientRegAppService.GetClientNumber(input);
            if (EditMode == (int)EditModeType.Add)
            {
                txtClientRegNum.Text = (count + 1).ToString();
            }
            else
                txtClientRegNum.Text = count.ToString();
        }
        #endregion

        #endregion
        /// <summary>
        /// 已选项目排序
        /// </summary>
        private List<ClientTeamRegitemViewDto> ItemGropsSort(List<ClientTeamRegitemViewDto> items)
        {
            if (items != null)
                return items.OrderBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder)?.ToList();
            else
                return null;
        }

        private void butChargeSetting_Click(object sender, EventArgs e)
        {
           
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能进行收费设置！");
                return;
            }
            bool IsNoSave = saveData(false);
            if (!IsNoSave)
            {
                return;
            }
            List<Guid> list = new List<Guid>();
            var datasource = grdTeamData.DataSource as List<CreateClientTeamInfoesDto>;
            if (datasource == null)
            {
                ShowMessageBoxInformation("请添加分组！");
                return;
            }
            foreach (var item in datasource)
            {
                list.Add(item.Id);
            }
            if (EditMode == (int)EditModeType.Add)
            {
                ShowMessageBoxInformation("请先保存预约信息！");
                return;
            }
            using (var frm = new FrmClientTeamCharge(list, customGridLookUpEdit1.Text.Trim(), txtClientRegBM.Text.Trim(), cteDto.ClientRegDto?.ClientInfo_Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    EditLoad();
                    return;
                }

            }
        }

        /// <summary>
        /// 焦点改变后按钮禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
          
            WhetherReadOnly();
            //更新更新性别更新套餐
            var Teaminfo = bandedGridView1.GetFocusedRow() as CreateClientTeamInfoesDto;
            if (Teaminfo != null)
            {
                var input = new SearchItemSuitDto() { ItemSuitType = (int)ItemSuitType.Suit };
                var result = DefinedCacheHelper.GetItemSuit().Where(o => (o.ItemSuitType == 1 || o.ItemSuitType == (int)ItemSuitType.OnLine) && o.Available == 1)?.ToList();
                if (Teaminfo.Sex != (int)Sex.GenderNotSpecified && Teaminfo.Sex != (int)Sex.Unknown)
                {
                    result = result.Where(o => o.Sex == Teaminfo.Sex || o.Sex == (int)Sex.GenderNotSpecified)?.ToList();
                }
                if (Teaminfo.TJType.HasValue)
                { result = result.Where(o => o.ExaminationType == Teaminfo.TJType || o.ExaminationType == null)?.ToList(); }
                result = result.Where(p => p.IsendDate != 1 || (p.IsendDate == 1 && p.endDate >= System.DateTime.Now)).ToList();

                cuslookItemSuit.Properties.DataSource = result;
              
            }


        }
        //行号
        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        //行号
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void txtPersonnelCategory_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == colFree.FieldName)
            {
                var data = gridView1.GetRowCellValue(e.ListSourceRowIndex, colIsFree);
                if (data != null)
                {
                    if ((bool)data)
                        e.DisplayText = "是";
                    else
                        e.DisplayText = "否";
                }
            }
        }
        #region 人员信息

        #region 构造函数       
        /// <summary>
        /// 性别转换
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatSexs(object arg)
        {
            try
            {
                return _sexModels.Find(r => r.Id == (int)arg).Display;
            }
            catch
            {
                return _sexModels.Find(r => r.Id == (int)Sex.GenderNotSpecified).Display;
            }
        }
        /// <summary>
        /// 体检状态
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string FormatCheckState(object arg)
        {
            try
            {
                return EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(ExaminationState), arg.ToString())));
            }
            catch
            {
                return "未知";
            }
        }
        #endregion 构造函数

        #region 系统事件

        #region 系统加载
        private void FrmClientRegCustomerList_Load(object sender, EventArgs e)
        {
            #region 申明变量
            customerSvr = new CustomerAppService();
            barPrintAppService = new BarPrintAppService();
            #endregion          
        }
        #endregion 系统加载
        #region 添加
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            if (ClientRegId == Guid.Empty)
            {
                ShowMessageBoxInformation("请先保存预约信息再执行该操作！");
                return;

            }
            using (var cusDetail = new CusDetail { curCustomRegInfo = new QueryCustomerRegDto { ClientRegId = ClientRegId, PersonnelCategoryId = cteDto.ClientRegDto.PersonnelCategoryId } })
            {
                if (cusDetail.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion 添加

        #region 分页
        /// <summary>
        /// 分页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNav_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Reload();
        }
        #endregion

        #region 修改编辑
        /// <summary>
        /// 修改编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            EditCustomerReg();
        }


        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCustomerRegs_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && e.RowHandle >= 0)
            {
                var CustomerBM = gvCustomerRegs.GetRowCellValue(this.gvCustomerRegs.FocusedRowHandle, "CustomerBM").ToString();
                //var row = gvCustomerRegs.GetRow(rowList[0]) as ClientTeamCusListDto;
                //var CustomerBM = row.CustomerBM;
                using (var cusDetail = new CusDetail() { curCustomerBM = CustomerBM, })
                {
                    if (cusDetail.ShowDialog() == DialogResult.OK)
                        Reload();
                }
                //EditCustomerReg();
            }
        }

        /// <summary>
        /// 编辑人员预约信息
        /// </summary>
        public void EditCustomerReg()
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            //var CustomerBM = gvCustomerRegs.GetRowCellValue(this.gvCustomerRegs.FocusedRowHandle, "CustomerBM").ToString();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要修改的数据！");
                return;
            }
            if (rowList.Count() > 1)
            {
                ShowMessageBoxInformation("请选择一条数据进行编辑！");
                return;
            }
            var row = gvCustomerRegs.GetRow(rowList[0]) as ClientTeamCusListDto;
            var CustomerBM = row.CustomerBM;
            using (var cusDetail = new CusDetail() { curCustomerBM = CustomerBM })
            {
                if (cusDetail.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                XtraMessageBox.Show("请选择要删除的数据！");
                return;
            }
            List<EntityDto<Guid>> listGuid = new List<EntityDto<Guid>>();
            foreach (var item in rowList)
            {
                var row = gvCustomerRegs.GetRow(item) as ClientTeamCusListDto;
                //if (row.CheckSate != (int)ExaminationState.Alr)
                //{
                //    ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", row.Customer.Name));
                //    //XtraMessageBox.Show(string.Format("{0}已开始体检，无法删除！",row.Customer.Name));
                //    return;
                //}bug3212张玉玲要求判断登记状态，已登记不能删除
                if (row.RegisterState == (int)RegisterState.Yes)
                {
                    ShowMessageBoxInformation(string.Format("{0}已登记，无法删除！", row.Name));
                    return;
                }
                if (row.CheckSate != (int)ExaminationState.Alr)
                {
                    ShowMessageBoxInformation(string.Format("{0}已开始体检，无法删除！", row.Name));
                    return;
                }
                EntityDto<Guid> gId = new EntityDto<Guid> { Id = row.Id };
                listGuid.Add(gId);
            }
            //操作数据库
            AutoLoading(() =>
            {
                _clientReg.DelCustomerReg(listGuid);
            });
            Reload();
            ShowMessageSucceed("删除成功！");
            //XtraMessageBox.Show("删除成功！");
        }
        #endregion

        #region 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 导入导出

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemExport_Click(object sender, EventArgs e)
        {
            var gridppUrl = GridppHelper.GetTemplate("人员预约信息模板.xls");
            if (GridppHelper.VitrualFileExist(gridppUrl))
            {
                GridppHelper.Download(gridppUrl, "人员预约信息模板");
            }
            else
            {
                MessageBox.Show(gridppUrl+"，模板下载文件没找到，使用程序生成模板方式");

                var strList = new List<string>() {
                "姓名 *",
                "性别 *",
                "年龄 *",
                "婚姻状况",
                "分组编码",
                "体检号",
                "移动电话",
                "身份证号",
                "出生日期",
                "车间",
                "工种",
                "接害工龄",
                "总工龄",
                "危害因素（多个，用“|”隔开）",
                "体检类别",
                "工号",
                "部门",
                "职务",
                "医保卡号",
                "预检时间",
                "流水号",
                "民族",
                "准备孕或育",
                "备注",
                "预约备注",
                "介绍人",
                "固定电话",
                "检查类型",
                "接害工龄单位",
                "总工龄单位",
                "客户类别",
                "通讯地址",
                "邮政编码",
                "电子邮件",
                "腾讯QQ",
                "证件类型",
                 "接害开始时间",
                  "其他工种",
                   "用工单位名称",
                   "用工单位机构代码"
            };
                JArray mb_jarray = new JArray();

                var sexList = SexHelper.GetSexForPerson().Select(o => o.Display).ToList();//性别

                var marrySateList = MarrySateHelper.GetMarrySateModelsForItemInfo().Select(o => o.Display).ToList();//婚否

                var breedStateList = BreedStateHelper.GetBreedStateModels().Select(o => o.Display).ToList();//孕育

                //体检类别
                var tjlb = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString()).Select(o => o.Text)?.ToList();
                //检查类型
                ChargeBM chargeBM = new ChargeBM();
                chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
                var jclxlist = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();

            //证件类型
            var CardTypelist = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString())?.Select(o => o.Text)?.ToList();

            ////车间
            //chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
            //var cjlis = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();

                ////工种
                //chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
                //var gzlis = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM).Select(o => o.Text).ToList();

                //性别 
                if (sexList.Count > 0)
                {
                    JObject XBObject = new JObject();
                    XBObject.Add("vlaue", JsonConvert.SerializeObject(sexList));
                    XBObject.Add("cel", "1");
                    mb_jarray.Add(XBObject);
                }
                //婚姻状况;
                if (marrySateList.Count > 0)
                {
                    JObject ZTObject = new JObject();
                    ZTObject.Add("vlaue", JsonConvert.SerializeObject(marrySateList));
                    ZTObject.Add("cel", "3");
                    mb_jarray.Add(ZTObject);
                }
                //孕否
                if (breedStateList.Count > 0)
                {
                    JObject YFObject = new JObject();
                    YFObject.Add("vlaue", JsonConvert.SerializeObject(breedStateList));
                    YFObject.Add("cel", "22");
                    mb_jarray.Add(YFObject);
                }
                //体检类别
                if (tjlb.Count > 0)
                {
                    JObject TJLBObject = new JObject();
                    TJLBObject.Add("vlaue", JsonConvert.SerializeObject(tjlb));
                    TJLBObject.Add("cel", "14");
                    mb_jarray.Add(TJLBObject);
                }

                //检查类型
                if (jclxlist.Count > 0)
                {
                    JObject JCLXObject = new JObject();
                    JCLXObject.Add("vlaue", JsonConvert.SerializeObject(jclxlist));
                    JCLXObject.Add("cel", "26");
                    mb_jarray.Add(JCLXObject);
                }

                //年月
                JObject NYObject = new JObject();
                NYObject.Add("vlaue", JsonConvert.SerializeObject(new string[] { "年", "月" }));
                NYObject.Add("cel", "28");
                mb_jarray.Add(NYObject);

                //年月
                JObject NYObject1 = new JObject();
                NYObject1.Add("vlaue", JsonConvert.SerializeObject(new string[] { "年", "月" }));
                NYObject1.Add("cel", "29");
                mb_jarray.Add(NYObject1);

            //时间格式字段
            List<int> cellIndexs = new List<int>();
            cellIndexs.Add(6);
            cellIndexs.Add(12);
            //检查类型
            if (CardTypelist.Count > 0)
            {
                JObject JCLXObject = new JObject();
                JCLXObject.Add("vlaue", JsonConvert.SerializeObject(CardTypelist));
                JCLXObject.Add("cel", "35");
                mb_jarray.Add(JCLXObject);
            }
            GridControlHelper.DownloadTemplate(strList, "人员预约信息模板", mb_jarray, cellIndexs, "yyyy-MM-dd");
            //GridControlHelper.ExportByGridControl(strList, "人员预约信息模板");
        }

        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            if (ClientRegId == Guid.Empty)
            {
                ShowMessageBoxInformation("请先保存预约信息再执行该操作！");
                return;
            }
            if (cteDto.ListClientTeam != null)
            {
                List<Guid> guids = cteDto.ListClientTeam.Select(o => o.Id).ToList();
                var team = _clientReg.GetClientTeamInfosById(guids);
                if (cteDto.ListClientTeam.Count != team.Count)
                {
                    ShowMessageBoxInformation("存在未保存的分组信息，请先保存预约信息再执行该操作！");
                    return;

                }
            }
            using (var frmImport = new FrmImportExcel() { ClientRegId = ClientRegId })
            {
                frmImport.cteDto = cteDto;
                //frmImport.ListClientTeam = ListClientTeam;
                //frmImport.ListClientTeamItem = ListClientTeamItem;
                if (frmImport.ShowDialog() == DialogResult.OK)
                    Reload();
            }
        }
        #endregion

        #region 打印

        /// <summary>
        /// 打印导引单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印导引单的数据！");
                return;
            }
            foreach (var item in rowList)
            {
                if (gvCustomerRegs.GetRow(item) is ClientTeamCusListDto row)
                {
                    PrintGuidanceNew.Print(row.Id);
                }
            }
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBarPrint_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印条码的数据！");
                //XtraMessageBox.Show("请选择要打印条码的数据！");
                return;
            }
            foreach (var item in rowList)
            {
                var row = gvCustomerRegs.GetRow(item) as ClientTeamCusListDto;
                FrmBarPrint frmBarPrint = new FrmBarPrint();
                CusNameInput cus = new CusNameInput();
                cus.Id = row.Id;
                frmBarPrint.cusNameInput = cus;
                frmBarPrint.IsPrintShowDialog = true;
                if (frmBarPrint.ShowDialog() == DialogResult.OK)
                {
                    frmBarPrint.Close();
                }

            }
        }


        /// <summary>
        /// 打印导引单\条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印的数据！");
                //XtraMessageBox.Show("请选择要打印的数据！");
                return;
            }
           
           
            foreach (var item in rowList)
            {
                List<CusNameInput> cuslist = new List<CusNameInput>();
                var row = gvCustomerRegs.GetRow(item) as ClientTeamCusListDto;

                CusNameInput cus = new CusNameInput();
                cus.Id = row.Id;
                cus.Theme = "1";
                cuslist.Add(cus);
                Thread thread = new Thread(new ParameterizedThreadStart(PrintGuidance));
                thread.Start(row.Id);
                thread.IsBackground = true;
                #region 打条码提示
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
                if (HISjk != null && HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                    if (HISName != null && HISName == "江苏鑫亿四院")
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = cus.Id;

                        var slo = customerSvr.getupdate(chargeBM);
                        if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                        {
                            MessageBox.Show(row.CustomerBM + "还未生成申请单不能打印条码，请批量登记或登记后再打印！");
                            return;
                        }
                    }
                }
                #endregion
                Thread threadB = new Thread(new ParameterizedThreadStart(PrintBar));
                threadB.Start(cuslist);
                threadB.IsBackground = true;

            }
           
            
        }
        /// <summary>
        /// 打印条码
        /// </summary>
        private void PrintBar(object cuslist)
        {
            this.Invoke(new Action(() =>
            {
                List<CusNameInput> cusNameInputs = (List<CusNameInput>)cuslist;
                FrmBarPrint frmBarPrint = new FrmBarPrint();
                frmBarPrint.BarPrintAll(cusNameInputs);
            }));

        }
        /// <summary>
        /// 打印导引单
        /// </summary>
        private void PrintGuidance(object ID)
        {
            this.Invoke(new Action(() =>
            {
                Guid guidId  = (Guid)ID;
            PrintGuidanceNew.Print(guidId);
            }));

        }
        #endregion

        #endregion 系统事件

        #region 初始化窗体

        #endregion


        /// <summary>
        /// 人员预约信息绑定
        /// 绑定Grid数据
        /// 分页绑定
        /// </summary>
        public void Reload()
        {
            int man = 0;
            int woman = 0;
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            gdvCustomerReg.DataSource = null;
            try
            {
                //PageInputDto<CustomerRegsInputDto> page = new PageInputDto<CustomerRegsInputDto>();
                //page.TotalPages = TotalPages;
                //page.CurentPage = CurrentPage;

                CustomerRegsInputDto dto = new CustomerRegsInputDto();
                string strSearch = txtSearch.Text;
                if (ClientinfoId != Guid.Empty && ClientinfoId != null)
                    dto.ClientinfoId = ClientinfoId;
                if (ClientRegId != Guid.Empty && ClientinfoId != null)
                    dto.ClientRegId = ClientRegId;
                if (!string.IsNullOrWhiteSpace(strSearch))
                    dto.TextValue = strSearch;
                if (rdoCheckState.EditValue != null && (int)rdoCheckState.EditValue != (int)ExaminationState.Whole)
                    dto.CheckState = (int?)rdoCheckState.EditValue;
                List<ClientTeamCusListDto> customerReg = new List<ClientTeamCusListDto>();
                if (dto.ClientRegId != null)
                {
                    // customerReg = _clientReg.GetCustomerReg(dto);
                    customerReg = _clientReg.GetCustomerRegList(dto);
                }
                
               // gdvCustomerReg.DataSource = customerReg;
                cusInfoList = customerReg.ToList();
                updateTeamCus();
                //foreach (var tm in customerReg)
                //{
                //    if (tm.Customer.Sex == 1)
                //    {
                //        man += 1;
                //    }
                //    else
                //    {
                //        woman += 1;
                //    }
                //}
                //LbCustomer.Text = "共" + customerReg.Count + "人";
                //LbCustomerN.Text = "男" + man + "人";
                //LbCustomerV.Text = "女" + woman + "人";
            }
            catch (ApiProxy.UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 1)
            {
                Reload();
            }
        }



        #endregion

        private void cuslookItemSuit_EditValueChanged(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            if (grdTeamData.DataSource == null)
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (string.IsNullOrWhiteSpace(TeamId))
            {
                ShowMessageBoxInformation("请先选择分组信息");
                return;
            }           
            var teamGuid = new Guid(TeamId);
            #region 已登记不能修改套餐
            if (cusInfoList == null)
            {
                CustomerRegsInputDto dto = new CustomerRegsInputDto();
                string strSearch = txtSearch.Text;
                if (ClientinfoId != Guid.Empty && ClientinfoId != null)
                    dto.ClientinfoId = ClientinfoId;
                if (ClientRegId != Guid.Empty && ClientinfoId != null)
                    dto.ClientRegId = ClientRegId;
                if (!string.IsNullOrWhiteSpace(strSearch))
                    dto.TextValue = strSearch;
                if (rdoCheckState.EditValue != null && (int)rdoCheckState.EditValue != (int)ExaminationState.Whole)
                    dto.CheckState = (int?)rdoCheckState.EditValue;
                List<ClientTeamCusListDto> customerReg = new List<ClientTeamCusListDto>();
                if (dto.ClientRegId != null)
                {
                    customerReg = _clientReg.GetCustomerRegList(dto);
                }
                
                cusInfoList = customerReg.ToList();
            }
           
            var cuslist = cusInfoList.Where(o => o.TeamId== teamGuid && o.RegisterState== (int)RegisterState.Yes).ToList();
            if (cuslist.Count>0)
            {
                ShowMessageBoxInformation(string.Format("该份组已有人员登记，无法修改套餐！"));
                return;
            }
            var cusnochecklist = cusInfoList.Where(o => o.TeamId == teamGuid && o.CheckSate != (int)ExaminationState.Alr).ToList();
            if (cusnochecklist.Count>0)
            {
                ShowMessageBoxInformation(string.Format("该份组已有人员检查，无法修改套餐！"));
                return;
            }
            #endregion
            var regTeam = cteDto.ListClientTeam.FirstOrDefault(o => o.Id == teamGuid);

            if (cuslookItemSuit.EditValue == null)
                return;
            var editor = sender as GridLookUpEdit;
            var data = editor.GetSelectedDataRow();
            if (data != null)
            {
                var itemSuitinfo = data as SimpleItemSuitDto;
                FullItemSuitDto ItemSuit = null;
                try
                {
                    var ret = itemSuitAppSvr.QueryFulls(new SearchItemSuitDto() { Id = itemSuitinfo.Id });
                    if (ret != null)
                    {
                        if (ret.Count > 0)
                            ItemSuit = ret.First();
                    }
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
                bool InputisPersonal = false;
                List<ClientTeamRegitemViewDto> TeamRegitem = new List<ClientTeamRegitemViewDto>();
                foreach (var item in ItemSuit.ItemSuitItemGroups)
                {
                    ClientTeamRegitemViewDto TeamRegItem = new ClientTeamRegitemViewDto();
                    TeamRegItem.ItemGroupName = item.ItemGroup.ItemGroupName;
                    TeamRegItem.DepartmentName = item.ItemGroup.Department.Name;
                    //
                    TeamRegItem.TbmDepartmentid = item.ItemGroup.DepartmentId;
                    TeamRegItem.DepartmentOrder = item.ItemGroup.Department.OrderNum;
                    TeamRegItem.TbmItemGroupid = item.ItemGroupId;

                    TeamRegItem.Discount = item.Suitgrouprate == null ? 1 : item.Suitgrouprate.Value;
                    //
                    TeamRegItem.ItemGroupOrder = item.ItemGroup.OrderNum;
                    TeamRegItem.ItemGroupMoney = item.ItemPrice == null ? 0 : item.ItemPrice.Value;
                    TeamRegItem.Discount = item.Suitgrouprate.Value;
                    TeamRegItem.ItemGroupDiscountMoney = item.PriceAfterDis.Value;
                    TeamRegItem.PayerCatType = InputisPersonal ? 2 : 3;
                    TeamRegItem.ItemSuitId = ItemSuit.Id;
                    TeamRegItem.ItemSuitName = ItemSuit.ItemSuitName;
                    TeamRegItem.ClientTeamInfoId = teamGuid;
                    TeamRegitem.Add(TeamRegItem);
                }


                if (ItemSuit.Id != regTeam.ItemSuit_Id && regTeam.ItemSuit_Id.HasValue)
                {
                    DialogResult dr = DevExpress.XtraEditors.XtraMessageBox.Show("您已经选择【" + regTeam.ItemSuitName + "】是否切换为【" + ItemSuit.ItemSuitName + "】？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr != DialogResult.OK)
                        return;
                }
                if (ItemSuit != null)
                {
                    regTeam.ItemSuit_Id = ItemSuit.Id;
                    regTeam.ItemSuitName = ItemSuit.ItemSuitName;
                }
                if (TeamRegitem != null && TeamRegitem.Count > 0)
                {
                    if (cteDto.ListClientTeamItem
                    != null && cteDto.ListClientTeamItem.Count > 0)
                    {
                        var delCliTeamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid).ToList();
                        if (delCliTeamItem != null && delCliTeamItem.Count() > 0)
                            for (int i = 0; i < delCliTeamItem.Count(); i++)
                            {
                                cteDto.ListClientTeamItem.Remove(delCliTeamItem[i]);
                            }
                    }
                    else
                        cteDto.ListClientTeamItem = new List<ClientTeamRegitemViewDto>();
                    //cteDto.ListClientTeamItem.Remove()
                    foreach (var regItem in TeamRegitem)
                    {
                        if (regItem.Id == null || regItem.Id == Guid.Empty)
                            regItem.Id = Guid.NewGuid();
                        if (regItem.ClientRegId == null || regItem.ClientRegId == Guid.Empty)
                            regItem.ClientRegId = clireg_Id;
                        cteDto.ListClientTeamItem.Add(regItem);
                    }
                }

                gridClientItem.DataSource = null;
                gridClientItem.DataSource = ItemGropsSort(cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamGuid)?.ToList());


            }

        }

        private void FrmEditTjlClientRegs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified && bandedGridView1.RowCount > 0)
            {
                if (!getFZState())
                {

                    DialogResult dr = XtraMessageBox.Show("有数据未保存是否保存？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        bool isTrue = saveData(true);
                        if (isTrue == false)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void bandedGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (first == false)
            {
                isModified = true;
            }
        }

        private void gridView2_DataSourceChanged(object sender, EventArgs e)
        {
            if (first == false)
            {
                isModified = true;
            }
        }
        private void updateTeamCus()
        {
            if (cusInfoList != null)
            {
                var selectIndexes = bandedGridView1.GetSelectedRows();
                if (selectIndexes.Length != 0)
                {
                    List<Guid> temId = new List<Guid>();
                    foreach (var index in selectIndexes)
                    {

                        var id = (Guid)bandedGridView1.GetRowCellValue(index, gridColumn1.FieldName);
                        temId.Add(id);
                    }
                    //var temId = bandedGridView1.GetFocusedRowCellValue(gridColumn1.FieldName);
                    if (temId != null && temId.Count>0)
                    {
                        var cuslist = cusInfoList.Where(o => temId.Contains(o.TeamId)).ToList();
                        gdvCustomerReg.DataSource = cuslist;
                        gdvCustomerReg.Refresh();
                        LbCustomer.Text = "共" + cuslist.Count + "人";
                        int sexMan = (int)Sex.Man;
                        int sexwomen = (int)Sex.Woman;
                        LbCustomerN.Text = "男" + cuslist.Where(o => o.Sex== sexMan).Count() + "人";
                        LbCustomerV.Text = "女" + cuslist.Where(o => o.Sex == sexwomen).Count() + "人";
                    }
                }

               
            }
        }

        private void grdTeamData_DoubleClick(object sender, EventArgs e)
        {


        }

        private void rdoCheckState_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ClientTeamCusListDto> cuslist = new List<ClientTeamCusListDto>();
            int ? chekstate;
            if (rdoCheckState.EditValue != null && (int)rdoCheckState.EditValue != (int)ExaminationState.Whole)
            {
                chekstate = (int?)rdoCheckState.EditValue;
                var culs= cusInfoList.Where(o => o.CheckSate == chekstate).ToList();
                gdvCustomerReg.DataSource = culs;
                cuslist = culs;
            }
            else
            {
                gdvCustomerReg.DataSource = cusInfoList;
                cuslist = cusInfoList.ToList();
            }           
            LbCustomer.Text = "共" + cuslist.Count + "人";
            int sexMan = (int)Sex.Man;
            int sexwomen = (int)Sex.Woman;
            LbCustomerN.Text = "男" + cuslist.Where(o => o.Sex == sexMan).Count() + "人";
            LbCustomerV.Text = "女" + cuslist.Where(o => o.Sex == sexwomen).Count() + "人";


        }

        private void txtSearch_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {

                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        string searchName = txtSearch.Text;
                        var cuslis = cusInfoList.Where(o => o.Name == searchName || o.CustomerBM == searchName).ToList();
                        gdvCustomerReg.DataSource = cuslis;
                    }
                    else
                    {
                        gdvCustomerReg.DataSource = cusInfoList;
                    }

                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        private void gvCustomerRegs_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private bool getFZState()
        {
           
            bool isFZState = false;
            if (EditMode != (int)EditModeType.Add)
            {
                if (clireg_Id != Guid.Empty)
                {
                    var FZSt = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = clireg_Id });
                    if (FZSt == 1)
                    {
                        return true;
                    }
                }
            }
            return isFZState;
           

        }

        private void butBarPrint_Click(object sender, EventArgs e)
        {

            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要打印的数据！");
                //XtraMessageBox.Show("请选择要打印的数据！");
                return;
            }
            List<CusNameInput> cuslist = new List<CusNameInput>();
            foreach (var item in rowList)
            {

                var row = gvCustomerRegs.GetRow(item) as ClientTeamCusListDto;                
                CusNameInput cus = new CusNameInput();
                cus.Id = row.Id;
                cus.CusRegBM = row.CustomerBM;
                cus.Theme = "1";
                cuslist.Add(cus);
                #region 打条码提示
                var HISjk = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 1)?.Remarks;
                if (HISjk!=null && HISjk == "1")
                {
                    var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2)?.Remarks;
                    if (HISName!=null && HISName == "江苏鑫亿四院")
                    {
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Id = cus.Id;
                      
                        var slo = customerSvr.getupdate(chargeBM);
                        if (slo != null && slo.Count > 0 && slo.FirstOrDefault()?.ct > 0)
                        {
                            MessageBox.Show(row.CustomerBM +"还未生成申请单不能打印条码，请批量登记或登记后再打印！");
                            return;
                        }
                    }
                }
                #endregion
            }
            FrmBarPrint frmBarPrint = new FrmBarPrint();
            frmBarPrint.BarPrintAll(cuslist);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                if (EditMode == (int)EditModeType.Edit)
                {
                    ///获取预约表版本号
                    var nowVerSion = _clientRegAppService.getClientVerSion(new EntityDto<Guid> { Id = clireg_Id });
                    if (getClientVerSionDto != null && getClientVerSionDto.RowVersion != null &&
                    nowVerSion != null && nowVerSion.RowVersion != null &&
                     !PasswordEquals(nowVerSion.RowVersion, getClientVerSionDto.RowVersion))
                    {

                        XtraMessageBox.Show("单位预约信息，其他人已修改请重新打开页面再编辑！");
                        return;
                    }
                }
                btnSave.Enabled = false;
              
                bool isTrue = saveDataN(true);
            
                if (isTrue)
                {    //日志

                    CommonAppService _commonAppService = new CommonAppService();
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = txtClientRegBM.Text.Trim();
                    createOpLogDto.LogName = customGridLookUpEdit1.Text.Trim();
                    createOpLogDto.LogText = "团体预约分步保存";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    EditLoad();
                }
                btnSave.Enabled = true;
            });
            isModified = false;
        }
        private bool saveDataN(bool IsNo)
        {
            dxErrorProvider.ClearErrors();
            var ClientRegBM = txtClientRegBM.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientRegBM))
            {
                dxErrorProvider.SetError(txtClientRegBM, string.Format(Variables.MandatoryTips, "编码"));
                txtClientRegBM.Focus();
                return false;
            }
            var ClientInfo = customGridLookUpEdit1.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientInfo))
            {
                dxErrorProvider.SetError(customGridLookUpEdit1, string.Format(Variables.MandatoryTips, "单位不可为空"));
                customGridLookUpEdit1.Focus();
                return false;
            }

            var ClientRegNum = txtClientRegNum.Text.Trim();
            if (string.IsNullOrWhiteSpace(ClientRegNum))
            {
                dxErrorProvider.SetError(txtClientRegNum, string.Format(Variables.MandatoryTips, "次数"));
                txtClientRegNum.Focus();
                return false;
            }
            //var RegPersonCount = txtRegPersonCount.Text.Trim();
            //if (string.IsNullOrWhiteSpace(RegPersonCount))
            //{
            //    dxErrorProvider.SetError(txtRegPersonCount, string.Format(Variables.MandatoryTips, "人数"));
            //    txtRegPersonCount.Focus();
            //    return false;
            //}           
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账不能保存！");
                return false;
            }
            var linkMan = txtLinkMan.Text.Trim();
            if (string.IsNullOrWhiteSpace(linkMan))
            {
                dxErrorProvider.SetError(txtLinkMan, string.Format(Variables.MandatoryTips, "单位负责人"));
                txtLinkMan.Focus();
                return false;
            }

            if (Convert.ToDateTime(txtStartCheckDate.EditValue) > Convert.ToDateTime(txtEndCheckDate.EditValue))
            {
                dxErrorProvider.SetError(txtStartCheckDate, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                txtStartCheckDate.Focus();
                return false;
            }

            if (cteDto.ListClientTeam == null || cteDto.ListClientTeam.Count == 0)
            {
                ShowMessageBoxInformation("请添加分组信息！");
                return false;
            }
            foreach (var teamInfo in cteDto.ListClientTeam)
            {
                var teamItem = cteDto.ListClientTeamItem.Where(o => o.ClientTeamInfoId == teamInfo.Id);
                // 你能找到一个 teamItem 项是 NULL 的情况吗
                // 难道程序不会崩溃吗
                if (teamItem == null || teamItem.Count() == 0)
                {
                    ShowMessageBoxInformation(string.Format("{0}下没有选择项目", teamInfo.TeamName));
                    return false;
                }
                if (teamInfo.Sex != (int)Sex.GenderNotSpecified && teamInfo.ItemSuit_Id.HasValue)
                {
                    var suit = DefinedCacheHelper.GetItemSuit().FirstOrDefault(o => o.Id == teamInfo.ItemSuit_Id);
                    if (suit != null)
                        if (suit.Sex != (int)Sex.GenderNotSpecified && suit.Sex != teamInfo.Sex)
                        {
                            var strSex = EnumHelper.GetEnumDesc((Sex)teamInfo.Sex);
                            ShowMessageBoxWarning(string.Format("分组【{0}】套餐适用性别为【{1}】，请修改分组适用性别后再保存。", teamInfo.TeamName, strSex));
                            return false;
                        }
                }

            }
            var stratTime = txtStartCheckDate.EditValue;

            var endTime = txtEndCheckDate.EditValue;

            var BlindSate = LeBlindSate.EditValue;

            var FZState = txtFZState.EditValue;

            var SDState = txtSDState.EditValue;

            var ControlDate = txtControlDate.EditValue;

            var ClientSate = txtClientSate.EditValue;

            var ClientCheckSate = txtClientCheckSate.EditValue;

            var Remark = txtRemark.Text.Trim();


            var clientRegDto = new CreateClientRegDto();
            clientRegDto.Id = clireg_Id;
            clientRegDto.ClientRegBM = ClientRegBM;
            clientRegDto.ClientRegNum = Convert.ToInt32(ClientRegNum);
            clientRegDto.RegPersonCount = 0;
            clientRegDto.linkMan = linkMan;
            clientRegDto.StartCheckDate = Convert.ToDateTime(stratTime);
            clientRegDto.EndCheckDate = Convert.ToDateTime(endTime);
            clientRegDto.BlindSate = (int)BlindSate;
            clientRegDto.FZState = (int)FZState;
            clientRegDto.SDState = (int)SDState;
            clientRegDto.ControlDate = (int)ControlDate;
            clientRegDto.ClientSate = (int)ClientSate;
            clientRegDto.ClientCheckSate = (int)ClientCheckSate;
            clientRegDto.Remark = Remark;
            clientRegDto.ClientInfo_Id = (Guid)customGridLookUpEdit1.EditValue;
            if (!string.IsNullOrEmpty(gluClientDegree.EditValue?.ToString()))
            {
                UserId = (long)gluClientDegree.EditValue;
            }
            clientRegDto.UserId = UserId;
            clientRegDto.linkMan = linkMan;
            if (string.IsNullOrWhiteSpace(txtPersonnelCategory.EditValue?.ToString()))
                clientRegDto.PersonnelCategoryId = null;
            else
                clientRegDto.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
            cteDto.ClientRegDto = clientRegDto;
            try
            {
                CreateClientRegDto clientReg = null;
                if (EditMode == (int)EditModeType.Add)
                    clientReg = _clientRegAppService.AddClientReg(cteDto);
                else if (EditMode == (int)EditModeType.Edit)
                {
                   var UpClientCusItems = _clientRegAppService.EditClientRegN(cteDto);
                    //MessageBox.Show("保存单位预约成功");
                    clientReg = new CreateClientRegDto();
                    clientReg = cteDto.ClientRegDto;
                    if (UpClientCusItems.Count > 0)
                    {
                        _clientRegAppService.SynClientCusItem(UpClientCusItems);
                       // MessageBox.Show("同步人员项目成功");
                        //var teamIs = UpClientCusItems.Select(o => o.ClientTeamInfoId).Distinct().ToList();
                        //foreach (var teamid in teamIs)
                        //{
                        //    EntityDto<Guid> entityDto = new EntityDto<Guid>();
                        //    entityDto.Id = teamid.Value;
                        //    _clientRegAppService.AcynCustomerItem(entityDto);
                        //}
                        //MessageBox.Show("计算人员金额成功");
                    }
                }
                //var clientReg = _clientRegAppService.insertClientReag(clientRegDto);
                //txtClientRegNum.Text = (clientReg.ClientRegNum + 1).ToString();
                clireg_Id = clientReg.Id;
                WhetherReadOnly();
                // 在这里判断 clientReg 不等于 NULL 有何用
                // 难道不知道在取 Id 值的时候程序已经崩溃了吗
                if (clientReg != null)
                {
                    EditMode = (int)EditModeType.Edit;
                    if (IsNo)
                    {
                        //ShowMessageSucceed("保存成功！");
                    }
                    foreach (var item in cteDto.ListClientTeam)
                    {
                        item.EditModle = true;
                    }

                    customGridLookUpEdit1.Enabled = false;
                    // 保存数据不重新加载，导致项目分组部分数据缺失
                    // 真不知道首次选项目是怎么加载的项目
                    //EditLoad();
                    return true;
                }
                else
                {
                    ShowMessageSucceed("保存失败！");
                    return false;
                }
            }
            catch (UserFriendlyException ex)
            {
                btnSave.Enabled = true;
                ShowMessageBox(ex);
                return false;
            }

        }

        private void bandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //ButtonPressedEventArgs en = new ButtonPressedEventArgs();
            //repositoryItemButtonEdit1_ButtonClick(sender,  e);
           // repositoryItemButtonEdit1
        }

        private void customGridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //设置为团体收费审核必须审核后才能添加单位
            if (EditMode != (int)EditModeType.Edit && customGridLookUpEdit1.EditValue !=null && customGridLookUpEdit1.EditValue !=""  
                && createClientXKSetDto!=null && createClientXKSetDto.ZKType==1)
            {
                SearchOAApproValcsDto searchOAApproValcsDto = new SearchOAApproValcsDto();
                searchOAApproValcsDto.ClientInfoId = (Guid)customGridLookUpEdit1.EditValue;
                var zk = oAApprovalAppService.SearchOAApproValcs(searchOAApproValcsDto);
                if (zk.Count == 0)
                {
                    MessageBox.Show("单位折扣方式为单位折扣审核，该单位尚未设置折扣，不能添加预约！");
                    customGridLookUpEdit1.EditValue = null;
                    return;
                }
                if (zk[0].AppliState == (int)OAApState.NoAp)
                {
                    MessageBox.Show("该单位存在未审核的折扣申请，不能添加预约！");
                    customGridLookUpEdit1.EditValue = null;
                    return;
                }
                if (zk[0].AppliState == (int)OAApState.reAp)
                {
                    MessageBox.Show("该单位折扣申请被拒绝，不能添加预约！");
                    customGridLookUpEdit1.EditValue = null;
                    return;
                }
                gluClientDegree.EditValue = CurrentUser.Id;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ClientTeamCusListDto> cuslist = new List<ClientTeamCusListDto>();
            int? chekstate;
           
            if (radioGroupRegState.EditValue != null && (int)radioGroupRegState.EditValue != (int)ExaminationState.Whole)
            {
                chekstate = (int?)radioGroupRegState.EditValue;
                var culs = cusInfoList.Where(o => o.RegisterState == chekstate).ToList();
                gdvCustomerReg.DataSource = culs;
                cuslist = culs;
            }
            else
            {
                gdvCustomerReg.DataSource = cusInfoList;
                cuslist = cusInfoList.ToList();
            }
            LbCustomer.Text = "共" + cuslist.Count + "人";
            int sexMan = (int)Sex.Man;
            int sexwomen = (int)Sex.Woman;
            LbCustomerN.Text = "男" + cuslist.Where(o => o.Sex == sexMan).Count() + "人";
            LbCustomerV.Text = "女" + cuslist.Where(o => o.Sex == sexwomen).Count() + "人";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            #region 发送短信
            var rowList = gvCustomerRegs.GetSelectedRows();
            if (rowList == null || rowList.Count() == 0)
            {
                ShowMessageBoxInformation("请选择要发送短信的人员！");
                return;
            }
            DateTime Star = new DateTime();
            DateTime End = new DateTime();
            frmCheckDatecs frmCheckDatecs = new frmCheckDatecs(txtStartCheckDate.DateTime, txtEndCheckDate.DateTime);
            frmCheckDatecs.ShowDialog();
            if (frmCheckDatecs.DialogResult == DialogResult.OK)
            {
                Star = frmCheckDatecs.Star;
                End = frmCheckDatecs.End;
                //体检类别

                var MessageModle = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.ShortModle.ToString() && o.Value == 1)?.Remarks;
                if (string.IsNullOrEmpty(MessageModle))
                {
                    XtraMessageBox.Show("请在字典设置“短信模板”，编码为1中设置预约短信模板");
                    return;
                }
                foreach (var item in rowList)
                {
                    if (gvCustomerRegs.GetRow(item) is ClientTeamCusListDto row)
                    {
                        if (!string.IsNullOrEmpty(row.Mobile))
                        {
                            ShortMessageDto input = new ShortMessageDto();
                            input.Age = row.Age;
                            //input.CustomerId = row.CustomerId;
                            input.CustomerRegId = row.Id;
                            var shortMes = MessageModle.Replace("【姓名】", row.Name);
                            if (row.Sex == 1)
                            {
                                shortMes = shortMes.Replace("【性别】", "先生");
                            }
                            else if (row.Sex == 2)
                            {
                                shortMes = shortMes.Replace("【性别】", "女士");
                            }
                            shortMes = shortMes.Replace("【体检号】", row.CustomerBM);

                            shortMes = shortMes.Replace("【单位】", customGridLookUpEdit1.Text.Trim());
                            if (Star.ToString("yyyy年MM月dd日") == End.ToString("yyyy年MM月dd日"))
                            {
                                shortMes = shortMes.Replace("【时间段】", Star.ToString("yyyy年MM月dd日"));

                            }
                            else
                            {
                                shortMes = shortMes.Replace("【时间段】", Star.ToString("yyyy年MM月dd日") + "至" + End.ToString("yyyy年MM月dd日"));
                            }
                            shortMes = shortMes.Replace("【时间段小时】", frmCheckDatecs.StarTime.ToString("HH:mm") + "至" + frmCheckDatecs.EndTime.ToString("HH:mm"));
                            input.Message = shortMes;
                            input.CustomerBM = row.CustomerBM;
                            input.MessageType = 1;
                            input.Mobile = row.Mobile;
                            input.Name = row.Name;
                            input.SendState = 0;
                            input.Sex = row.Sex;
                            _clientRegAppService.SaveMessage(input);
                        }
                    }
                }
            } 
            #endregion

        }
    }
}