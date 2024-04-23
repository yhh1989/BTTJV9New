using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
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
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmEditTjlClientInfoes : UserBaseForm
    {
        #region 常量与构造
        //private FrmTjlClientInfoes frmClientInfo;

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
        public FrmEditTjlClientInfoes()
        {
            InitializeComponent();
            _commonAppService = new CommonAppService();
        }

        //public FrmEditTjlClientInfoes(FrmTjlClientInfoes frmClient)
        //{
        //    InitializeComponent();
        //    _commonAppService = new CommonAppService();
        //    frmClientInfo = frmClient;
        //}

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
        #region 事件
        #region 加载事件
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEditTjlClientInfoes_Load(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 下拉框事件

        /// <summary>
        /// 选择省
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 清空省
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditProvince_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditProvince.EditValue = null;
        }

        /// <summary>
        /// 选择市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 清空市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditCity_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditCity.EditValue = null;
        }

        /// <summary>
        /// 区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditArea_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditArea.EditValue = null;
        }

        /// <summary>
        /// 所属客服
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gluClientDegree_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                gluClientDegree.EditValue = null;
        }

        /// <summary>
        /// 行业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditClientlndutry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditClientlndutry.EditValue = null;
        }

        /// <summary>
        /// 单位类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditClientType_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditClientType.EditValue = null;
        }

        /// <summary>
        /// 合同性质
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditClientcontract_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditClientcontract.EditValue = null;
        }

        /// <summary>
        /// 单位状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditClientSate_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lookUpEditClientSate.EditValue = null;
        }

        /// <summary>
        /// 来源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lueClientSource_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                lueClientSource.EditValue = null;
        }

        /// <summary>
        /// 父级单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glueParentClient_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                glueParentClient.EditValue = null;
        }
        #endregion

        #endregion
        #region 公共方法
        /// <summary>
        /// 获取组合数据
        /// </summary>
        public void BindData()
        {
            InitializationUI();
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
            //单位编码
            if (EditMode == (int)EditModeType.Add)
            {
                var ClientBM = _iIdNumber.CreateClientBM();
                if (!string.IsNullOrWhiteSpace(ClientBM))
                    txtClientBM.Text = ClientBM;
            }
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
            lookUpEditClientlndutry.Properties.DataSource = Clientlndutry;
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
                BindCustomGridLookUpEdit<ClientInfosNameDto>.BindGridLookUpEdit(glueParentClient, clientPaaent, "ClientName", "Id", "ClientName", 15, "Id");
            }
            glueParentClient.EditValue = ParentId;

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

                txtAddress.Text = cli.Address;
                gluClientDegree.EditValue = cli.UserId;
                if (!string.IsNullOrWhiteSpace(cli.Clientlndutry))
                    lookUpEditClientlndutry.EditValue = Convert.ToInt32(cli.Clientlndutry);
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
                if (cli.Parent != null)
                {
                    //textClientParent.Text = cli.Parent.ClientName;
                    glueParentClient.EditValue = cli.Parent.Id;
                    ParentId = cli.Parent.Id.ToString();
                    ParentName = cli.Parent.ClientName;
                }
                if (!string.IsNullOrWhiteSpace(cli.SocialCredit))
                    textEdit1.Text = cli.SocialCredit.ToString();
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
            }
        }

        #endregion



        /// <summary>
        /// 保存单位管理信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //if (string.IsNullOrWhiteSpace(ClientAbbreviation))
            //{
            //    dxErrorProvider.SetError(txtClientAbbreviation, string.Format(Variables.MandatoryTips, "单位简称"));
            //    txtClientAbbreviation.Focus();
            //    return;
            //}

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

            if (glueParentClient.EditValue != null)
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


            ////联系电话
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
                }
                //修改单位
                else if (EditMode == (int)EditModeType.Edit)
                {
                    _ClientInfoService.Edit(dto);
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = dto.ClientBM;
                    createOpLogDto.LogName = dto.ClientName;
                    createOpLogDto.LogText = "修改单位：" + dto.ClientName;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ClientId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                //查询数据库
                //frmClientInfo.Reload();
                //var clientInfoList = _ClientInfoService.Query(new ClientInfoesListInput() { });
                //if (clientInfoList != null)
                //    frmClientInfo.treClientInfo.DataSource = clientInfoList;
                //frmClientInfo.wgrdClientData.RefreshDataSource();
                // 保存成功直接关闭窗体，提示没什么意义
                //ShowMessageSucceed("保存成功！");
                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
        }

        public event Action<Guid> CreateCompanyComplete;

        /// <summary>
        /// 助记码自动增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        private void txtClientName_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            var name = txtClientName.Text.Trim();
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
        #endregion

        private void customGridView1_DragObjectOver(object sender, DevExpress.XtraGrid.Views.Base.DragObjectOverEventArgs e)
        {
            GridColumn column = e.DragObject as GridColumn;
            if (column != null)
            {
                e.DropInfo.Valid = false;
            }
        }

        private void glueParentClient_Properties_Enter(object sender, EventArgs e)
        {

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
    }
}
