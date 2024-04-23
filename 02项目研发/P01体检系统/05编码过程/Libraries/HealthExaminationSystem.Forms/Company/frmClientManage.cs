using Abp.Application.Services.Dto;
 
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class frmClientManage : UserBaseForm
    {
        /// <summary>
        /// 单位服务
        /// </summary>
        private readonly IClientInfoesAppService _clientInfoesAppService;

        private readonly IClientRegAppService _clientRegAppService;

        private bool _treeNodeExpandOrCollapse;
        private List<ClientInfosNameDto> clientInfosNames = new List<ClientInfosNameDto>();

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserAppService _userAppService;

        private List<UserForComboDto> _userList;
        public event Action<Guid> CreateCompanyComplete;
        #region 常量与构造        

        //编码
        IIDNumberAppService _iIdNumber = new IDNumberAppService();
        //编辑方式
        public int EditMode;
        //主键
        public Guid clientId;
        //父级单位ID
        public string ParentId;
        //父级单位名称
        public string ParentName = "所有单位";

        private readonly ICommonAppService _commonAppService;


        /// <summary>
        /// 单位服务
        /// </summary>
        private IClientInfoesAppService ClientInfoservice = null;
        public IClientInfoesAppService _ClientInfoService
        {
            get
            {
                if (ClientInfoservice == null) ClientInfoservice = new ClientInfoesAppService();
                return ClientInfoservice;
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        private IUserAppService _Userservice = null;
        public IUserAppService Userservice
        {
            get
            {
                if (_Userservice == null) _Userservice = new UserAppService();
                return _Userservice;
            }
        }

        /// <summary>
        /// 字典信息
        /// </summary>
        private IBasicDictionaryAppService service = null;
        public IBasicDictionaryAppService _Service
        {
            get
            {
                if (service == null) service = new BasicDictionaryAppService();
                return service;
            }
        }
        #endregion
        public frmClientManage()
        {
            InitializeComponent();
            _clientInfoesAppService = new ClientInfoesAppService();
            _clientRegAppService = new ClientRegAppService();
            _userAppService = new UserAppService();
            _commonAppService = new CommonAppService();
        }

        private void frmClientManage_Load(object sender, EventArgs e)
        {
            EnableBut(true);
            //行号宽度
            treClientInfo.IndicatorWidth = 30;
           
        }
        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public void InitializationUI()
        {
            //绑定数据源
            BindDataSource();
            //编辑单位
            if (EditMode == (int)EditModeType.Edit)
                BindEditDataSource();
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public void BindDataSource()
        {
        
            //经济类型
            var EconomicsType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.EconomicsType);
            var EconomicsTypes = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == EconomicsType);
            comboBoxEdit1.Properties.DataSource = EconomicsTypes;
            //企业规模
            var ScaleType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ScaleType);
            var ScaleTypes = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == ScaleType);
            comboBoxEdit2.Properties.DataSource = ScaleTypes;
            //所属行业
            //var Clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
            //var Clientlndutrys = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == Clientlndutry);
            //comboBoxEdit3.Properties.DataSource = Clientlndutrys;
            //省
            //var province= Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Province);
            //var Province = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == province);
            var Province = AdministrativeDivisionHelper.GetProvince();
            lookUpEditProvince.Properties.DataSource = Province;

            ////市
            ////input.Type = BasicDictionaryType.City.ToString();
            //var city = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.City);
            //var City = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == city);
            ////var City = CacheHelper.GetBasicDictionarys(BasicDictionaryType.City);//_Service.Query(input);
            //lookUpEditCity.Properties.DataSource = City;
            ////lookUpEditCity.ItemIndex = 0;

            ////区
            ////input.Type = BasicDictionaryType.Area.ToString();
            //var area = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Area);
            //var Area = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == area);
            ////var Area = CacheHelper.GetBasicDictionarys(BasicDictionaryType.Area);//_Service.Query(input);
            //lookUpEditArea.Properties.DataSource = Area;
            ////lookUpEditArea.ItemIndex = 0;

            //行业
            //input.Type = BasicDictionaryType.Clientlndutry.ToString();
            var clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
            var Clientlndutry = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientlndutry);
            //var Clientlndutry = CacheHelper.GetBasicDictionarys(BasicDictionaryType.Clientlndutry);//_Service.Query(input);
            lookUpEditClientlndutry.Properties.DataSource = Clientlndutry.ToList();
            //lookUpEditClientlndutry.ItemIndex = 0;

            //单位类型
            //input.Type = BasicDictionaryType.ClientType.ToString();
            var clientType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ClientType);
            var ClientType = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientType);
            //var ClientType = CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientType);//_Service.Query(input);
            lookUpEditClientType.Properties.DataSource = ClientType;
            //lookUpEditClientType.ItemIndex = 0;

            //合同性质
            //input.Type = BasicDictionaryType.Clientcontract.ToString();
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientcontract);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            //var Clientcontract = CacheHelper.GetBasicDictionarys(BasicDictionaryType.Clientcontract);//_Service.Query(input);
            lookUpEditClientcontract.Properties.DataSource = Clientcontract;
            //lookUpEditClientcontract.ItemIndex = 0;

            //单位状态
            //input.Type = BasicDictionaryType.ClientSate.ToString();
            var clientSate = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ClientSate);
            var ClientSate = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientSate);
            //var ClientSate = CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSate); //_Service.Query(input);
            lookUpEditClientSate.Properties.DataSource = ClientSate;
            //lookUpEditClientSate.ItemIndex = 0;

            //客服用户信息
            //var UsersListOutputDto = Userservice.GetUsers();
            var UsersListOutputDto = DefinedCacheHelper.GetComboUsers();
            if (UsersListOutputDto != null && UsersListOutputDto.Count > 0)
            {
                // BindCustomGridLookUpEdit<UserFormDto>.BindGridLookUpEdit(gluClientDegree, UsersListOutputDto, "Name", "Id", "Name", 15, "Id");
                gluClientDegree.Properties.DataSource = UsersListOutputDto;
                gluClientDegree.EditValue = CurrentUser.Id;
            }

            //来源
            var LisClientSource = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ClientSource);
            var lisClientSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == LisClientSource);
            //var lisClientSource = CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);// EnumHelper.GetEnumDescs(typeof(ExaminationSource));
            var aa = EnumHelper.GetEnumDescs(typeof(ExaminationSource));
            lueClientSource.Properties.DataSource = lisClientSource;

            //父级单位
            //textClientParent.Text = ParentName;

            //var clientPaaent = _ClientInfoService.Query(new ClientInfoesListInput());

            var clientPaaent = _ClientInfoService.QueryClientName(new ChargeBM());
            clientPaaent = clientPaaent.Where(o => o.Id != clientId).ToList();
            if (clientPaaent.Any())
            {
                clientInfosNames = clientPaaent.ToList();
                BindCustomGridLookUpEdit<ClientInfosNameDto>.BindGridLookUpEdit(glueParentClient, clientPaaent, "ClientName", "Id", "ClientName", 15, "Id");
            }
          

        }

        /// <summary>
        /// 绑定编辑数据
        /// </summary>
        public void BindEditDataSource()
        {
            //var Id = frmClientInfo.gridViewClientInfo.GetRowCellValue(frmClientInfo.gridViewClientInfo.FocusedRowHandle, "Id").ToString();
            ClientInfoesListInput dto = new ClientInfoesListInput();
            dto.Id = clientId;
            var cli = _ClientInfoService.Get(dto);
            if (cli != null)
            {
                txtClientBM.Text = cli.ClientBM.ToString();
                txtClientName.Text = cli.ClientName;
                txtClientAbbreviation.Text = cli.ClientAbbreviation;
                txtHelpCode.Text = cli.HelpCode;
                txtWubiCode.Text = cli.WubiCode;
                txtOrganizationCode.Text = cli.OrganizationCode;
                if (!string.IsNullOrWhiteSpace(cli.StoreAdressP))
                    lookUpEditProvince.EditValue = cli.StoreAdressP;
                else
                { lookUpEditProvince.EditValue = null; }
                if (!string.IsNullOrWhiteSpace(cli.StoreAdressS))
                {
                    AdministrativeDivisionDto proDto = new AdministrativeDivisionDto
                    {
                        Code = lookUpEditProvince.EditValue.ToString(),
                    };
                    var City = AdministrativeDivisionHelper.GetCity(proDto);
                    lookUpEditCity.Properties.DataSource = City;
                    lookUpEditCity.EditValue = cli.StoreAdressS;
                }
                else
                { lookUpEditCity.EditValue = null; }
                if (!string.IsNullOrWhiteSpace(cli.StoreAdressQ))
                {
                    AdministrativeDivisionDto citDto = new AdministrativeDivisionDto
                    {
                        Code = lookUpEditCity.EditValue.ToString(),
                    };
                    var Area = AdministrativeDivisionHelper.GetCounty(citDto);
                    lookUpEditArea.Properties.DataSource = Area;
                    lookUpEditArea.EditValue = cli.StoreAdressQ;
                }
                else
                { lookUpEditArea.EditValue = null; }
                if (!string.IsNullOrWhiteSpace(cli.StoreAdressX))
                {
                    AdministrativeDivisionDto citDto = new AdministrativeDivisionDto
                    {
                        Code = lookUpEditArea.EditValue.ToString(),
                    };
                    var Area = AdministrativeDivisionHelper.GetTownship(citDto);
                    lookUpTownship.Properties.DataSource = Area;
                    lookUpTownship.EditValue = cli.StoreAdressX;
                }
                else
                { lookUpTownship.EditValue = null; }
                txtAddress.Text = cli.Address;
                gluClientDegree.EditValue = cli.UserId;
                if (!string.IsNullOrWhiteSpace(cli.Clientlndutry))
                    lookUpEditClientlndutry.EditValue = Convert.ToInt32(cli.Clientlndutry);
                else
                {
                    lookUpEditClientlndutry.EditValue = null;

                }
                if (!string.IsNullOrWhiteSpace(cli.ClientType))
                    lookUpEditClientType.EditValue = Convert.ToInt32(cli.ClientType);
                if (!string.IsNullOrWhiteSpace(cli.Clientcontract))
                    lookUpEditClientcontract.EditValue = Convert.ToInt32(cli.Clientcontract);
                //lueClientSource.EditValue = cli.ClientSource.ToString();
                txtMobile.Text = cli.Mobile;
                txtFax.Text = cli.Fax;
                txtLinkMan.Text = cli.LinkMan;
                txtPostCode.Text = cli.PostCode;
                txtClientEmail.Text = cli.ClientEmail;
                if (clientInfosNames.Any())
                {
                    var clentlist = clientInfosNames.Where(P=>P.Id!= cli.Id).ToList();
                    BindCustomGridLookUpEdit<ClientInfosNameDto>.BindGridLookUpEdit(glueParentClient, clentlist, "ClientName", "Id", "ClientName", 15, "Id");
                }
                if (cli.Parent != null)
                {
                    //textClientParent.Text = cli.Parent.ClientName;
                    glueParentClient.EditValue = cli.Parent.Id;
                    ParentId = cli.Parent.Id.ToString();
                    ParentName = cli.Parent.ClientName;
                }
                else
                {
                    glueParentClient.EditValue = null;

                }
                if (!string.IsNullOrWhiteSpace(cli.SocialCredit))
                    textEdit1.Text = cli.SocialCredit.ToString();
                else
                {
                    textEdit1.Text = "";
                }
                comboBoxEdit1.EditValue = cli.EconomicType;
                comboBoxEdit2.EditValue = cli.Scale;
                txtClientQQ.Text = cli.ClientQQ;
                txtClientBank.Text = cli.ClientBank;
                txtClientAccount.Text = cli.ClientAccount;
                txtTelephone.Text = cli.Telephone;
                if (cli.Limit == 1)
                    checkEdit1.Checked = true;
                if (!string.IsNullOrWhiteSpace(cli.ClientSate))
                    lookUpEditClientSate.EditValue = Convert.ToInt32(cli.ClientSate);
                if (!string.IsNullOrWhiteSpace(cli.ClientSource))
                {
                    //var strSource = cli.ClientSource;
                    //var source = Enum.Parse(typeof(ExaminationSource), strSource);
                    lueClientSource.EditValue = Convert.ToInt32(cli.ClientSource);

                    //string strSourceText = string.Empty;
                    //foreach (var item in strList)
                    //{
                    //    var source = Enum.Parse(typeof(ExaminationSource), item).ToString();
                    //    var sourceText = EnumHelper.GetEnumDesc((Enum)(Enum.Parse(typeof(ExaminationSource), item)));
                    //    strSourceText += sourceText + ",";
                    //    strSource += source + ",";
                    //}
                    //lueClientSource.EditValue = strSource.Substring(0, strSource.Length - 1);
                    //lueClientSource.EditValue.RefreshEditValue();
                    //cmbClientSource.Text= strSourceText.Substring(0, strSource.Length - 1);
                }
                dateEditCreate.EditValue = cli.CreationTime;
                customCreate.EditValue = cli.CreatorUserId;
                if (cli.LastModificationTime.HasValue)
                { dateEdit.EditValue = cli.LastModificationTime; }
                if (cli.LastModifierUserId.HasValue)
                { customEdit.EditValue = cli.LastModifierUserId; }
            }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCreationTime1.DateTime > txtCreationTime2.DateTime)
            {
                ShowMessageBoxWarning("开始时间大于结束时间，请修改后查询");
                return;
            }

            Reload();
        }
        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        public void InitializeFormData()
        {
            AutoLoading(() =>
            {
                //var lisClientSource = CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);
                var lisClientSource = DefinedCacheHelper.GetBasicDictionary()
                    .Where(r => r.Type == BasicDictionaryType.ClientSource.ToString());
                txtClientSource.Properties.DataSource = lisClientSource;

                if (_userList == null)
                {
                    // _userList = _userAppService.GetUsers();
                    _userList = DefinedCacheHelper.GetComboUsers();

                }

                if (_userList != null && _userList.Count > 0)
                {
                    //var mod = new ModelHandler<UserFormDto>();
                    //var dtuser = mod.FillDataTable(_userList);
                    //BindCustomGridLookUpEdit<UserFormDto>.BindGridLookUpEdit(txtClientDegree, _userList, "Name", "Id", "Name", 15, "Id");
                    txtClientDegree.Properties.DataSource = _userList;
                    customCreate.Properties.DataSource= _userList;
                    customEdit.Properties.DataSource = _userList;
                }
            }, Variables.LoadingForForm);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Reload()
        {
            treClientInfo.DataSource = null;
            AutoLoading(() =>
            {
                //var page = new PageInputDto<ClientInfoesListInput> { TotalPages = TotalPages, CurentPage = CurentPage };

                var dto = new ClientInfoesListInput();

                //单位编码信息
                if (!string.IsNullOrWhiteSpace(textEdit2.Text))
                {
                    dto.ClientBM = textEdit2.Text;
                }

                //单位名称
                if (!string.IsNullOrWhiteSpace(textEdit4.Text))
                {
                    dto.ClientName = textEdit4.Text;
                }

                //来源
                if (txtClientSource.EditValue != null && !txtClientSource.EditValue.Equals(""))
                {
                    dto.ClientSource = txtClientSource.EditValue?.ToString();
                }

                ////联系人
                //if (!string.IsNullOrWhiteSpace(txtLinkMan.Text))
                //{
                //    dto.LinkMan = txtLinkMan.Text;
                //}

                //开始时间
                if (!string.IsNullOrWhiteSpace(txtCreationTime1.Text))
                {
                    dto.StartTime = txtCreationTime1.DateTime;
                }

                //结束时间
                if (!string.IsNullOrWhiteSpace(txtCreationTime2.Text))
                {
                    dto.EndTime = txtCreationTime2.DateTime;
                }

                //专属客服
                if (txtClientDegree.EditValue != null && !txtClientDegree.EditValue.Equals(""))
                {
                    dto.UserId = Convert.ToInt32(txtClientDegree.EditValue);
                }

                AutoLoading(() =>
                {
                    var output = _clientInfoesAppService.PageFulls(new PageInputDto<ClientInfoesListInput>
                    {
                        TotalPages = TotalPages,
                        CurentPage = CurrentPage,
                        Input = dto
                    });
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator);
                    treClientInfo.DataSource = output.Result;
                });
            });
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearUi();
        }
        /// <summary>
        /// 清除窗体
        /// </summary>
        public void ClearUi()
        {
            txtClientBM.Text = "";
            txtClientName.Text = "";
            txtCreationTime1.EditValue = null;
            txtCreationTime2.EditValue = null;
            txtClientDegree.EditValue = null;
            txtLinkMan.Text = "";
            txtClientSource.EditValue = null;
            txtClientSource.RefreshEditValue();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            EditMode = (int)EditModeType.Add;
            ClearDataSource();
            EnableBut(false);
        }
        /// <summary>
        /// 绑定编辑数据
        /// </summary>
        public void ClearDataSource()
        {
            //单位编码
            if (EditMode == (int)EditModeType.Add)
            {
                var ClientBM = _iIdNumber.CreateClientBM();
                if (!string.IsNullOrWhiteSpace(ClientBM))
                    txtClientBM.Text = ClientBM;
            }
            txtClientName.Text = "";
            txtClientAbbreviation.Text = "";
            txtHelpCode.Text = "";
            txtWubiCode.Text = "";
            txtOrganizationCode.Text = "";
            lookUpEditProvince.EditValue = null;
            lookUpEditCity.EditValue = null;
            lookUpEditArea.EditValue = null;
            lookUpTownship.EditValue = null;
            txtAddress.Text = "";
            gluClientDegree.EditValue = null;
            lookUpEditClientlndutry.EditValue = null;
            lookUpEditClientType.EditValue = null;
            lookUpEditClientcontract.EditValue = null;
            txtMobile.Text = "";
            txtFax.Text = "";
            txtLinkMan.Text = "";
            txtPostCode.Text = "";
            txtClientEmail.Text = "";
            glueParentClient.EditValue = null;
            textEdit1.Text = "";
            comboBoxEdit1.EditValue = null;
            comboBoxEdit2.EditValue = null;
            txtClientQQ.Text = "";
            txtClientBank.Text = "";
            txtClientAccount.Text = "";
            txtTelephone.Text = "";
            checkEdit1.Checked = true;
            lookUpEditClientSate.EditValue = null;
            lueClientSource.EditValue = null;


        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            var question = XtraMessageBox.Show("是否删除？", "询问",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var row = treClientInfo.FocusedNode;
                if (row == null)
                {
                    ShowMessageBoxInformation("请选择要修改的数据！");

                    //XtraMessageBox.Show("请选择要修改的数据！");
                    return;
                }

                var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
                var client = _clientInfoesAppService.Get(new ClientInfoesListInput { Id = Guid.Parse(id) });
                if (client.Parent == null)
                {
                    //父级单位
                    var clientInfo = _clientInfoesAppService.Query(new ClientInfoesListInput { ParentId = client.Id });
                    if (clientInfo.Count != 0)
                    {
                        foreach (var item in clientInfo)
                        {
                            var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = item.Id });
                            if (clientregs != null)
                            {
                                if (clientregs.Count > 0)
                                {
                                    ShowMessageBoxInformation("已有预约信息，无法删除！");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = client.Id });
                        if (clientregs != null)
                        {
                            if (clientregs.Count > 0)
                            {
                                ShowMessageBoxInformation("已有预约信息，无法删除！");
                                return;
                            }
                        }
                    }

                    _clientInfoesAppService.Del(new ClientInfoesDto { Id = Guid.Parse(id) });
                    foreach (var item in clientInfo)
                        _clientInfoesAppService.Del(new ClientInfoesDto { Id = item.Id });
                    Reload();
                    return;
                }

                //以下子级单位
                //查询子级单位的预约信息
                var clientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput { ParentId = Guid.Parse(id) });
                if (clientInfoes != null)
                {
                    foreach (var item in clientInfoes)
                    {
                        var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = item.Id });
                        if (clientregs != null)
                        {
                            if (clientregs.Count > 0)
                            {
                                ShowMessageBoxInformation("已有预约信息，无法删除！");
                                return;
                            }
                        }
                    }

                    foreach (var item in clientInfoes)
                        if (!GetQuery(item.Id))
                        {
                            ShowMessageBoxInformation("已有预约信息，无法删除！");
                            return;
                        }
                }

                var clientreg = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = Guid.Parse(id) });
                if (clientreg != null)
                {
                    if (clientreg.Count > 0)
                    {
                        ShowMessageBoxInformation("已有预约信息，无法删除！");
                    }
                    else
                    {
                        _clientInfoesAppService.Del(new ClientInfoesDto { Id = Guid.Parse(id) });
                        var delclientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput
                        { ParentId = Guid.Parse(id) });
                        foreach (var item in delclientInfoes)
                            _clientInfoesAppService.Del(new ClientInfoesDto { Id = item.Id });
                        Reload();
                    }
                }
                else
                {
                    _clientInfoesAppService.Del(new ClientInfoesDto { Id = Guid.Parse(id) });
                    var delclientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput
                    { ParentId = Guid.Parse(id) });
                    foreach (var item in delclientInfoes)
                        _clientInfoesAppService.Del(new ClientInfoesDto { Id = item.Id });
                    Reload();
                }
            }
            catch (UserFriendlyException ex)
            {
                //ShowMessageBox(ex);
                MessageBox.Show(ex.Message);
            }
        }
        public bool GetQuery(Guid Id)
        {
            var clientInfoes = _clientInfoesAppService.Query(new ClientInfoesListInput { ParentId = Id });
            if (clientInfoes != null)
            {
                foreach (var item in clientInfoes)
                {
                    var clientregs = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = item.Id });
                    if (clientregs != null)
                    {
                        if (clientregs.Count > 0)
                        {
                            ShowMessageBoxInformation("已有预约信息，无法删除！");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }
        public void Export()
        {
            //// 重名则会提示重复是否要覆盖
            //if (saveFileDialog.ShowDialog() != DialogResult.OK)
            //    return;

            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName, treClientInfo);
            //if (Path.GetExtension(fileName) == ".xlsx")
            //{
            //    var xlsxExportOptions = new XlsxExportOptions();
            //    treClientInfo.ExportToXlsx(fileName, xlsxExportOptions);
            //}
            //else
            //{
            //    var xlsExportOptions = new XlsExportOptions();
            //    treClientInfo.ExportToXls(fileName, xlsExportOptions);
            //}

            //if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            //    DialogResult.Yes)
            //    Process.Start(fileName); //打开指定路径下的文件
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();

            var ClientBM = txtClientBM.Text;
            if (string.IsNullOrWhiteSpace(ClientBM))
            {
                dxErrorProvider.SetError(txtClientBM, string.Format(Variables.MandatoryTips, "编码"));
                txtClientBM.Focus();
                return;
            }

            var ClientName = txtClientName.Text;
            if (string.IsNullOrWhiteSpace(ClientName))
            {
                dxErrorProvider.SetError(txtClientName, string.Format(Variables.MandatoryTips, "单位名称"));
                txtClientName.Focus();
                return;
            }

            var ClientAbbreviation = txtClientAbbreviation.Text;         

            var HelpCode = txtHelpCode.Text;
            if (string.IsNullOrWhiteSpace(HelpCode))
            {
                dxErrorProvider.SetError(txtHelpCode, string.Format(Variables.MandatoryTips, "助记码"));
                txtHelpCode.Focus();
                return;
            }

            var LinkMan = txtLinkMan.Text;
            if (string.IsNullOrWhiteSpace(LinkMan))
            {
                dxErrorProvider.SetError(txtLinkMan, string.Format(Variables.MandatoryTips, "企业负责人"));
                txtLinkMan.Focus();
                return;
            }

            //增加单位信息
            ClientInfoesDto dto = new ClientInfoesDto();
            if (EditMode == (int)EditModeType.Edit)
                dto.Id = clientId;
            dto.ClientBM = ClientBM;
            dto.ClientName = ClientName;
            dto.ClientAbbreviation = ClientAbbreviation;
            dto.HelpCode = HelpCode;
            var WubiCode = txtWubiCode.Text;
            if (!string.IsNullOrWhiteSpace(WubiCode))
                dto.WubiCode = WubiCode;
            var OrganizationCode = txtOrganizationCode.Text;
            if (!string.IsNullOrWhiteSpace(OrganizationCode))
                dto.OrganizationCode = OrganizationCode;

            if (lookUpEditProvince.EditValue != null)
                dto.StoreAdressP = lookUpEditProvince.EditValue.ToString();
            if (lookUpEditCity.EditValue != null)
                dto.StoreAdressS = lookUpEditCity.EditValue.ToString();
            if (lookUpEditArea.EditValue != null)
                dto.StoreAdressQ = lookUpEditArea.EditValue.ToString();
            if (lookUpTownship.EditValue != null)
                dto.StoreAdressX = lookUpTownship.EditValue.ToString();
            if (lueClientSource.EditValue != null)
                dto.ClientSource = lueClientSource.EditValue?.ToString();//((int)Enum.Parse(typeof(ExaminationSource), lueClientSource.EditValue.ToString())).ToString();

            dto.Address = txtAddress.Text;
            var ClientDegree = gluClientDegree.EditValue;
            if (ClientDegree != null && !string.IsNullOrWhiteSpace(ClientDegree.ToString()))
                dto.UserId = Convert.ToInt32(ClientDegree);
            if (lookUpEditClientlndutry.EditValue != null)
                dto.Clientlndutry = lookUpEditClientlndutry.EditValue.ToString();
            if (lookUpEditClientType.EditValue != null)
                dto.ClientType = lookUpEditClientType.EditValue.ToString();
            if (lookUpEditClientcontract.EditValue != null)
                dto.Clientcontract = lookUpEditClientcontract.EditValue.ToString();
            if (textEdit1.EditValue != null)
                dto.SocialCredit = textEdit1.EditValue.ToString();             
            if (comboBoxEdit1.EditValue != null)
                dto.EconomicType = (int)comboBoxEdit1.EditValue;
            if (comboBoxEdit2.EditValue != null)
                dto.Scale = (int)comboBoxEdit2.EditValue;

            //验证手机号
            if (!string.IsNullOrWhiteSpace(txtMobile.Text))
            {
                Regex rx = new Regex("^1[34578]\\d{9}$");
                if (!rx.IsMatch(txtMobile.Text)) //不匹配
                {
                    dxErrorProvider.SetError(txtMobile, string.Format(Variables.BoxFormat, "手机号"));
                    txtMobile.Focus();
                    return;
                }
                else
                {
                    dto.Mobile = txtMobile.Text;
                }
            }

            //传真验证
            if (!string.IsNullOrWhiteSpace(txtFax.Text))
            {
                Regex rx = new Regex("^(([0-9]{3,4}-)|[0-9]{3.4}-)?[0-9]{7,8}$");
                if (!rx.IsMatch(txtFax.Text)) //不匹配
                {
                    dxErrorProvider.SetError(txtFax, string.Format(Variables.BoxFormat, "传真"));
                    txtFax.Focus();
                    return;
                }
                else
                {
                    dto.Fax = txtFax.Text;
                }
            }

            dto.LinkMan = txtLinkMan.Text;
            //企业邮箱验证
            if (!string.IsNullOrWhiteSpace(txtClientEmail.Text))
            {
                Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
                if (!RegEmail.IsMatch(txtClientEmail.Text)) //不匹配
                {
                    dxErrorProvider.SetError(txtClientEmail, string.Format(Variables.BoxFormat, "企业邮箱"));
                    txtClientEmail.Focus();
                    return;
                }
                else
                {
                    dto.ClientEmail = txtClientEmail.Text;
                }
            }




            //dto.ClientEmail = txtClientEmail.Text;

            //邮政编码
            if (!string.IsNullOrWhiteSpace(txtPostCode.Text))
            {
                Regex RegEmail = new Regex("[0-9]{1}([0-9]+){5}");
                if (!RegEmail.IsMatch(txtPostCode.Text)) //不匹配
                {
                    dxErrorProvider.SetError(txtPostCode, string.Format(Variables.BoxFormat, "邮政编码"));
                    txtPostCode.Focus();
                    return;
                }
                else
                {
                    dto.PostCode = txtPostCode.Text;
                }
            }

            //if (!string.IsNullOrWhiteSpace(ParentId))
            //{
            //    dto.Parent = new ClientInfoesDto { Id = new Guid(ParentId) };
            //}

            if (!string.IsNullOrEmpty( glueParentClient.EditValue?.ToString()))
            {
                ParentId = glueParentClient.EditValue.ToString();
                dto.Parent = new ClientInfoesDto { Id = new Guid(ParentId) };
            }
            //企业QQ
            if (!string.IsNullOrWhiteSpace(txtClientQQ.Text))
            {
                int tmp = int.Parse(txtClientQQ.Text);
                if (!int.TryParse(txtClientQQ.Text, out tmp)) //不匹配
                {
                    dxErrorProvider.SetError(txtClientQQ, string.Format(Variables.NumberTips, "企业QQ"));
                    txtClientQQ.Focus();
                    return;
                }
                else
                {
                    dto.ClientQQ = txtClientQQ.Text;
                }
            }

            dto.ClientBank = txtClientBank.Text;

            dto.ClientAccount = txtClientAccount.Text;


            //联系电话
            //if (!string.IsNullOrWhiteSpace(txtTelephone.Text))
            //{
            //    //int tmp = long.Parse(txtTelephone.Text);
            //    if (!long.TryParse(txtTelephone.Text, out long tm)) //不匹配
            //    {
            //        dxErrorProvider.SetError(txtTelephone, string.Format(Variables.NumberTips, "联系电话"));
            //        txtTelephone.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        dto.Telephone = txtTelephone.Text;
            //    }
            //}
            dto.Telephone = txtTelephone.Text;
            dto.Limit = Convert.ToInt32(checkEdit1.Checked);
            if (lookUpEditClientSate.EditValue != null)
                dto.ClientSate = lookUpEditClientSate.EditValue.ToString();
            //if (!string.IsNullOrWhiteSpace(cmbClientSource.EditValue.ToString()))
            //{
            //    var str = cmbClientSource.EditValue?.ToString()?.Split(',');
            //    if (str != null)
            //    {
            //        List<int> list = new List<int>();
            //        string strSource = string.Empty;
            //        foreach (var item in str)
            //        {
            //            list.Add((int)Enum.Parse(typeof(ExaminationSource), item));
            //            strSource += (int)Enum.Parse(typeof(ExaminationSource), item) + ",";
            //        }
            //        dto.ClientSource = strSource.Substring(0, strSource.Length - 1);
            //    }
            //    //dto.ClientSource = txtClientSource.EditValue.ToString();
            //}
            try
            {
                //新增单位
                if (EditMode == (int)EditModeType.Add)
                {
                    var client = _ClientInfoService.Add(dto);
                    CreateCompanyComplete?.Invoke(client.Id);
                    //var data = frmClientInfo.treClientInfo.DataSource as List<ClientInfosViewDto>;
                    //data.Add(NewClient);
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = client.ClientBM;
                    createOpLogDto.LogName = client.ClientName;
                    createOpLogDto.LogText = "添加单位：" + client.ClientName;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    EditMode = (int)EditModeType.Edit;
                    clientId = client.Id;                   
                    BindEditDataSource();
                    EnableBut(true);
                }
                //修改单位
                else if (EditMode == (int)EditModeType.Edit)
                {
                    var client = _ClientInfoService.Edit(dto);
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = dto.ClientBM;
                    createOpLogDto.LogName = dto.ClientName;
                    createOpLogDto.LogText = "修改单位：" + dto.ClientName;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    EditMode = (int)EditModeType.Edit;
                    clientId = client.Id;
                    BindEditDataSource();
                    EnableBut(true);
                }
                btnSearch.PerformClick();

                //查询数据库
                //frmClientInfo.Reload();
                //var clientInfoList = _ClientInfoService.Query(new ClientInfoesListInput() { });
                //if (clientInfoList != null)
                //    frmClientInfo.treClientInfo.DataSource = clientInfoList;
                //frmClientInfo.wgrdClientData.RefreshDataSource();
                // 保存成功直接关闭窗体，提示没什么意义
                //ShowMessageSucceed("保存成功！");

                EnableBut(true);
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
               // MessageBox.Show(ex.);
            }
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            if (clientId == null)
            {
                MessageBox.Show("请选中需要修改的数据！");
                return;
            }        
            EditMode = (int)EditModeType.Edit;
            EnableBut(false);
        }

        private void treClientInfo_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (btnsave.Enabled == true)
            {
                string ts = "正在保存数据，是否切换数据，切换后没保存的数据将丢失！";
                 
                DialogResult dr = XtraMessageBox.Show(ts, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            var row = treClientInfo.FocusedNode;
            if (row == null)
            {

                return;
            }
            var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
            EditMode = (int)EditModeType.Edit;
            clientId = Guid.Parse(id);
            BindEditDataSource();
            EnableBut(true);
        }

        private void btnSubClient_Click(object sender, EventArgs e)
        {
            var row = treClientInfo.FocusedNode;
            if (row == null)
            {
                ShowMessageBoxInformation("请选择父级单位！");
                return;
            }
            var id = treClientInfo.FocusedNode.GetValue("Id").ToString();
            var clientName = treClientInfo.FocusedNode.GetValue("ClientName").ToString();
            ParentId = id;
            ParentName = clientName;
            EditMode = (int)EditModeType.Add;
            glueParentClient.EditValue = ParentId;
            EnableBut(false);
        }
        private void EnableBut(bool isEnable)
        {
            btnAddNew.Enabled = isEnable;
            btnSubClient.Enabled = isEnable;
            btnUpdate.Enabled= isEnable;
            btnsave.Enabled = !isEnable;
            btnDel.Enabled = isEnable;
        }

        private void frmClientManage_Shown(object sender, EventArgs e)
        {
            //绑定体检来源
            InitializeFormData();
            InitializationUI();
        }

        private void txtClientName_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
           
            var name = txtClientName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                //if (!string.IsNullOrWhiteSpace(name))
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    //if (string.IsNullOrWhiteSpace(txtHelpCode.Text))
                    txtHelpCode.Text = result.Brief;
                    //if (string.IsNullOrWhiteSpace(txtClientAbbreviation.Text))
                    //txtClientAbbreviation.Text = result.Brief;
                    WuBiHelper wubiHelper = new WuBiHelper();
                    var wubi = wubiHelper.GetWBCode(name);
                    txtWubiCode.Text = wubi;

                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void lookUpEditProvince_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditProvince.EditValue != null)
            {
                AdministrativeDivisionDto dto = new AdministrativeDivisionDto
                {
                    Code = lookUpEditProvince.EditValue.ToString(),
                };
                var City = AdministrativeDivisionHelper.GetCity(dto);
                lookUpEditCity.Properties.DataSource = City;
                lookUpEditArea.Properties.DataSource = null;
            }
        }

        private void lookUpEditCity_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditCity.EditValue != null)
            {
                AdministrativeDivisionDto dto = new AdministrativeDivisionDto
                {
                    Code = lookUpEditCity.EditValue.ToString(),
                };
                var Area = AdministrativeDivisionHelper.GetCounty(dto);
                lookUpEditArea.Properties.DataSource = Area;
            }
        }

        private void lookUpEditArea_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditArea.EditValue != null)
            {
                AdministrativeDivisionDto dto = new AdministrativeDivisionDto
                {
                    Code = lookUpEditArea.EditValue.ToString(),
                };
                var Area = AdministrativeDivisionHelper.GetTownship(dto);
                lookUpTownship.Properties.DataSource = Area;
            }
        }

        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            Reload();
        }

        private void lookUpEditProvince_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;            

            }
        }

        private void lookUpEditCity_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void glueParentClient_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lookUpTownship_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lookUpEditArea_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void gluClientDegree_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lookUpEditClientlndutry_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEditClientlndutry_ButtonClick(object sender, ButtonPressedEventArgs e)
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

        private void lookUpEditClientcontract_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lueClientSource_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void lookUpEditClientSate_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }
    }
}
