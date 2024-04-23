using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using HealthExamination.HardwareDrivers;
using HealthExamination.HardwareDrivers.Models.IdCardReader;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;
using HealthExaminationSystem.Enumerations.Models;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Api.Controllers;
using DevExpress.Utils;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class CusDetail : UserBaseForm
    {
        #region 字典
        public List<SexModel> sexList;//性别字典
        public List<MarrySateModel> marrySateList;//婚育字典
        public List<BreedStateModel> breedStateList;//孕育字典
        public List<EnumModel> reviewStates;//复查状态字典
        public List<ClientRegDto> clientRegs;//单位及分组字典
        #endregion
        public string curCustomerBM;//当前体检号
        public QueryCustomerRegDto curCustomRegInfo;//当前客户预约信息
        private ICustomerAppService customerSvr;//体检预约
        public bool isCustomerReg;//是否前台登记功能
        private IIDNumberAppService iIDNumberAppService;
        private IIdCardReaderHardwareDriver driver;
        // private readonly IFormRoleAppService _formRoleAppService;
        private IPersonnelCategoryAppService _personnelCategoryAppService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService= new  OccDisProposalNewAppService();
        public CusDetail()
        {
            InitializeComponent();
      
        }
        #region 事件
        private void CusDetail_Load(object sender, EventArgs e)
        {
            try
            {
                //设置时间控件显示时间
                txtBookingDate.Properties.VistaDisplayMode = DefaultBoolean.True;
                txtBookingDate.Properties.VistaEditTime = DefaultBoolean.True;
                this.txtBookingDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                this.txtBookingDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.txtBookingDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                this.txtBookingDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.txtBookingDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

                var clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
                var Clientlndutry = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientlndutry).ToList();
                txtCustomerTrade.Properties.DataSource = Clientlndutry;
                _personnelCategoryAppService = new PersonnelCategoryAppService();

                this.KeyPreview = true;
                iIDNumberAppService = new IDNumberAppService();
                customerSvr = new CustomerAppService();
                //加载1+X问卷
                LoadData();
                BindConrolData();
                if (isCustomerReg)
                {
                    txtIDCardNo.Properties.Buttons[0].Visible = false;//读身份证按钮隐藏
                    this.KeyPreview = false;
                    labTeamStr.Text = "分组：";
                }
                else
                {
                    labTeamStr.Text = "<Color=Red>✶</Color>分组：";
                    if (curCustomRegInfo != null)
                    {
                        if (!string.IsNullOrWhiteSpace(curCustomerBM))
                        {
                            txtIDCardNo.Properties.Buttons[0].Visible = false;
                            this.KeyPreview = false;
                        }
                    }
                }
                gridView4.Columns[Sex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
               // tileView1.EndUpdate();
            }
        }
        private void CusDetail_Shown(object sender, EventArgs e)
        {
            if (checkReplaceSate.Checked && string.IsNullOrWhiteSpace(curCustomRegInfo.PrimaryName))
            {
                ShowMessageBoxInformation("替检必须填写原体检人信息");
                txtPrimaryName.Focus();
            }
            if (!isCustomerReg)
            {
                if (string.IsNullOrWhiteSpace(curCustomRegInfo.CustomerBM))
                {
                    try
                    {
                        curCustomRegInfo.CustomerBM = iIDNumberAppService.CreateArchivesNumBM();
                        txtCustoemrCode.EditValue = curCustomRegInfo.CustomerBM;
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBox(ex);
                    }
                }
            }
        }
        /// <summary>
        /// 值改变事件 单位改变分组改变
        /// </summary>
        private void txtClientRegID_EditValueChanged(object sender, EventArgs e)
        {
            var control = sender as GridLookUpEditBase;
            if (control.EditValue == null)
            {
                txtTeamID.Properties.DataSource = null;
            }
            else
            {
                try
                {
                    var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(control.EditValue?.ToString()) });
                    if (!string.IsNullOrWhiteSpace(comSex.EditValue?.ToString()))
                    {
                        list = list.Where(o => o.Sex == Convert.ToInt32(comSex.EditValue) || o.Sex == (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified)?.ToList();
                    }
                    txtTeamID.Properties.DataSource = list;
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
            }
        }
        private void comSex_EditValueChanged(object sender, EventArgs e)
        {
            upteam();

        }
        /// <summary>
        /// 编辑控件中的按钮点击事件
        /// </summary>
        private void edior_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        private void butOk_Click(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPage.Name == tabcusInfo.Name)
            {//当前页面是基本信息
                var data = GetDataFromControl();
                if (data == null)
                    return;
                try
                {
                    if (data.CustomerItemGroup != null)
                    {
                        if (data.CustomerItemGroup.Any(o => o.PayerCat == (int)PayerCatType.NoCharge))
                            data.CostState = (int)PayerCatType.NoCharge;
                    }
                    if (isCustomerReg)
                    {
                        curCustomRegInfo = data;
                        DialogResult = DialogResult.OK;
                        return;
                    }
                    if (data.RegisterState == null)//当登记状态没有值且是单位预约用的时候就为其赋值
                        data.RegisterState = (int)RegisterState.No;
                     curCustomRegInfo = customerSvr.RegCustomer(new List<QueryCustomerRegDto>() { data }).FirstOrDefault();
                   
                    
                    ShowMessageSucceed("保存成功！");
                    DialogResult = DialogResult.OK;

                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                }
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        private void butcancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion


        #region 处理
        /// <summary>
        ///控件绑定数据源 赋值
        /// </summary>
        private void BindConrolData()
        {
            //字典无数据时加载数据
            if (sexList == null)
                sexList = SexHelper.GetSexForPerson();//.GetSexModelsForItemInfo();
            if (marrySateList == null)
                marrySateList = MarrySateHelper.GetMarrySateModels();
            if (breedStateList == null)
                breedStateList = BreedStateHelper.GetBreedStateModels();
            if (reviewStates == null)
                reviewStates = ReviewSateTypeHelper.GetReviewStates();
            if (clientRegs == null)
            {
                try
                {
                    //clientRegs = customerSvr.QuereyClientRegInfos(new FullClientRegDto());//加载单位分组数据
                }
                catch (UserFriendlyException ex)
                {
                    ShowMessageBox(ex);
                    return;
                }
            }
            txtPersonnelCategory.Properties.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
            comSex.Properties.DataSource = sexList;
            comMarriageStatus.Properties.DataSource = marrySateList;
            txtConceive.Properties.DataSource = breedStateList;
            lookUpEditCustomerType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList();
            lookUpEditClientType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList();
            txtReviewSate.Properties.DataSource = reviewStates;
            txtClientRegID.Properties.DataSource = clientRegs;
            //labHaveBreakfast.Properties.DataSource = BreakfastTypeHelper.GetBreakfastTypes();
            //checkMessage.Properties.DataSource = MessageEmailStateHelper.GetMessageEmailStates();
            txtDegree.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.DegreeType.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.DegreeType);
            txtSecretlevel.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.SecrecyLevel.ToString())?.ToList();// CacheHelper.GetBasicDictionarys(BasicDictionaryType.SecrecyLevel);
            var emaillist = MessageEmailStateHelper.GetMessageEmailStates();
            emaillist.RemoveAll(o => o.Id == (int)MessageEmailState.HasBeenSent);
            txtEmailReport.Properties.DataSource = emaillist;
            txtReportBySelf.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ReportSentType.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.ReportSentType);
            txtStoreAdressP.Properties.DataSource = AdministrativeDivisionHelper.GetProvince();//DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.Province.ToString())?.ToList();// CacheHelper.GetBasicDictionarys(BasicDictionaryType.Province);
            comStoreAdressS.Properties.DataSource = null;//DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.City.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.City);
            txtStoreAdressQ.Properties.DataSource = null;//DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.Area.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.Area);
            txtIDCardType.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString())?.ToList();// CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType);
            var shenfenzheng = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString()).FirstOrDefault(o => o.Text.Contains("身份证"));// Common.Helpers.CacheHelper.GetBasicDictionarys(BasicDictionaryType.CertificateType).FirstOrDefault(o => o.Text == "身份证");
            if (shenfenzheng != null)
            {
                txtIDCardType.EditValue = shenfenzheng.Value;
            }
            lookUpEditInfoSource.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ClientSource.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.ClientSource);
            if (!string.IsNullOrWhiteSpace(curCustomerBM))
            {
                var result= customerSvr.QueryCustomerReg(new SearchCustomerDto { CustomerBM = curCustomerBM });
                curCustomRegInfo = result.FirstOrDefault();
            }
            if (curCustomRegInfo != null)
            {
                if (curCustomRegInfo.Customer == null)
                    curCustomRegInfo.Customer = new QueryCustomerDto();
                txtCustoemrCode.EditValue = curCustomRegInfo.CustomerBM;
                txtArchivesNum.EditValue = curCustomRegInfo.Customer.ArchivesNum;
                txtName.EditValue = curCustomRegInfo.Customer.Name;
                comSex.EditValue = curCustomRegInfo.Customer.Sex;
                txtAge.EditValue = curCustomRegInfo.Customer.Age;
                comMarriageStatus.EditValue = curCustomRegInfo.Customer.MarriageStatus;
                txtConceive.EditValue = curCustomRegInfo.ReadyPregnancybirth;
                txtMobile.EditValue = curCustomRegInfo.Customer.Mobile;
                txtIDCardNo.EditValue = curCustomRegInfo.Customer.IDCardNo;
                if (curCustomRegInfo.Customer.IDCardType.HasValue)
                {
                    txtIDCardType.EditValue = curCustomRegInfo.Customer.IDCardType;
                }
                DateBirthday.EditValue = curCustomRegInfo.Customer.Birthday;
                DateChekDate.EditValue = curCustomRegInfo.BookingDate;
                lookUpEditInfoSource.EditValue = curCustomRegInfo.InfoSource;
                if (curCustomRegInfo.CustomerType.HasValue)
                    lookUpEditCustomerType.EditValue = curCustomRegInfo.CustomerType;
                else
                {
                    lookUpEditCustomerType.EditValue = null;
                   // lookUpEditCustomerType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList().FirstOrDefault().Value;

                }
                if (curCustomRegInfo.PhysicalType.HasValue)
                {
                    lookUpEditClientType.EditValue = Convert.ToInt32(curCustomRegInfo.PhysicalType);
                }
                else
                {
                    lookUpEditClientType.EditValue = null;
                    lookUpEditClientType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList().FirstOrDefault().Value;
                }
                if (curCustomRegInfo.PersonnelCategoryId.HasValue)
                    txtPersonnelCategory.EditValue = curCustomRegInfo.PersonnelCategoryId;

                txtTelephone.EditValue = curCustomRegInfo.Customer.Telephone;
                if (curCustomRegInfo.AppointmentTime.HasValue)
                    txtBookingDate.EditValue = curCustomRegInfo.AppointmentTime;
                else
                    txtBookingDate.EditValue = DateTime.Now;
                txtCardNumber.EditValue = curCustomRegInfo.Customer.CardNumber;
                txtWorkNumber.EditValue = curCustomRegInfo.Customer.WorkNumber;
                if (int.TryParse(curCustomRegInfo.Customer.CustomerTrade, out int outrTrade))
                {
                    txtCustomerTrade.EditValue = outrTrade;
                }
                txtDuty.EditValue = curCustomRegInfo.Customer.Duty;
                txtDegree.EditValue = curCustomRegInfo.Customer.Degree;
                txtLastMenstruation.EditValue = curCustomRegInfo.LastMenstruation;
                txtGestation.EditValue = curCustomRegInfo.Gestation;
                txtMedicalCard.EditValue = curCustomRegInfo.Customer.MedicalCard;
                txtSectionNum.EditValue = curCustomRegInfo.Customer.SectionNum;
                txtVisitCard.EditValue = curCustomRegInfo.Customer.VisitCard;
                txtHospitalNum.EditValue = curCustomRegInfo.Customer.HospitalNum;
                lookUpEditInfoSource.EditValue = curCustomRegInfo.InfoSource;
                txtSecretlevel.EditValue = curCustomRegInfo.Customer.Secretlevel;
                txtGuoJi.EditValue = curCustomRegInfo.Customer.GuoJi;
                txtEmailReport.EditValue = curCustomRegInfo.EmailReport;
                txtEmail.EditValue = curCustomRegInfo.Customer.Email;
                txtReportBySelf.EditValue = curCustomRegInfo.ReportBySelf;
                txtPostgCode.EditValue = curCustomRegInfo.Customer.PostgCode;
                txtQq.EditValue = curCustomRegInfo.Customer.Qq;
                if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressP))
                    txtStoreAdressP.EditValue = curCustomRegInfo.Customer.StoreAdressP;
                if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressS))
                    comStoreAdressS.EditValue = curCustomRegInfo.Customer.StoreAdressS;
                if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressQ))
                    txtStoreAdressQ.EditValue = curCustomRegInfo.Customer.StoreAdressQ;
                txtAdress.EditValue = curCustomRegInfo.Customer.Address;
                txthomeAdress.EditValue = curCustomRegInfo.Customer.HomeAddress;
                txtClientRegID.EditValue = curCustomRegInfo.ClientRegId;

                txtTeamID.EditValue = curCustomRegInfo.ClientTeamInfo_Id;
                if (curCustomRegInfo.ReplaceSate.HasValue)
                {
                    if (curCustomRegInfo.ReplaceSate.Value == 2)
                    {//是替检
                        checkReplaceSate.Checked = true;
                    }
                    else
                    {
                        checkReplaceSate.Checked = false;
                    }
                }
                txtPrimaryName.EditValue = curCustomRegInfo.PrimaryName;
                txtRemarks1.EditValue = curCustomRegInfo.Customer.Remarks;
                txtReviewSate.EditValue = curCustomRegInfo.ReviewSate;

                if (curCustomRegInfo.BlindSate.HasValue)
                {
                    if (curCustomRegInfo.BlindSate.Value == (int)BlindSate.Normal)
                    {
                        checkBlindSate.Checked = false;
                    }
                    else if (curCustomRegInfo.BlindSate.Value == (int)BlindSate.Blind)
                    {
                        checkBlindSate.Checked = true;
                    }
                }
                if (curCustomRegInfo.UrgentState.HasValue)
                {
                    if (curCustomRegInfo.UrgentState == 1)
                    {
                        checkUrgentState.Checked = false;
                    }
                    else if (curCustomRegInfo.UrgentState == 2)
                    {
                        checkUrgentState.Checked = true;
                    }
                }

                //labHaveBreakfast.EditValue = curCustomRegInfo.HaveBreakfast;
                if (curCustomRegInfo.HaveBreakfast == (int)BreakfastType.Eat)
                    checkHaveBreakfast.Checked = true;
                else
                    checkHaveBreakfast.Checked = false;
                if (curCustomRegInfo.FamilyState == 1)
                {
                    chckFamilyState.Checked = false;
                }
                else if (curCustomRegInfo.FamilyState == 2)
                {
                    chckFamilyState.Checked = true;
                }
                //checkMessage.EditValue = curCustomRegInfo.Message;
                if (curCustomRegInfo.Message == (int)MessageEmailState.Open)
                    checkMsg.Checked = true;
                else
                    checkMsg.Checked = false;
                if (curCustomRegInfo.MailingReport == 1)
                {
                    checkMailingReport.Checked = true;
                }
                else if (curCustomRegInfo.MailingReport == 2)
                {
                    checkMailingReport.Checked = false;
                }
                if (curCustomRegInfo.ExamPlace == 1)
                {
                    checkExamPlace.Checked = false;
                }
                else if (curCustomRegInfo.ExamPlace == 2)
                {
                    checkExamPlace.Checked = true;
                }
                txtIntroducer.EditValue = curCustomRegInfo.Introducer;
                txtDepartment.EditValue = curCustomRegInfo.Customer.Department;
                txtNation.EditValue = curCustomRegInfo.Customer.Nation;
                txtJKZ.EditValue = curCustomRegInfo.JKZBM;
                textEditRegRemark.EditValue = curCustomRegInfo.Remarks;
                //职业健康
                txtWorkName.EditValue = curCustomRegInfo.WorkName;
                txtTypeWork.EditValue = curCustomRegInfo.TypeWork;
                txtTotalWorkAge.EditValue = curCustomRegInfo.TotalWorkAge;
                txtInjuryAge.EditValue = curCustomRegInfo.InjuryAge;
                textEditClientname.EditValue = curCustomRegInfo.employerEnterpriseName;
                textEditClientCode.EditValue = curCustomRegInfo.employerCreditCode;
                textEditOther.EditValue = curCustomRegInfo.OtherTypeWork;
                comunit2.Text = curCustomRegInfo.InjuryAgeUnit;
                comunit.Text = curCustomRegInfo.WorkAgeUnit;
                txtRiskS.EditValue = curCustomRegInfo.RiskS;
                txtCheckType.EditValue = curCustomRegInfo.PostState;
                if (curCustomRegInfo.OccHazardFactors != null && curCustomRegInfo.OccHazardFactors.Count > 0)
                {
                    txtRiskS.Tag = curCustomRegInfo.OccHazardFactors;
                }
            }
            else
                curCustomRegInfo = new QueryCustomerRegDto();
            //加载1+X问卷
            if (curCustomRegInfo.Id != Guid.Empty)
            {
                EntityDto<Guid> entityDto = new EntityDto<Guid>();
                entityDto.Id = curCustomRegInfo.Id;
                InitialUpdate(entityDto);
                getCusques(entityDto);
                getCusGroups(entityDto);
            }
        }
        /// <summary>
        /// 从控件中获取数据
        /// </summary>
        private QueryCustomerRegDto GetDataFromControl()
        {
            dxErrorProvider.ClearErrors();
            if (curCustomRegInfo == null)
                curCustomRegInfo = new QueryCustomerRegDto();
            if (curCustomRegInfo.Customer == null)
                curCustomRegInfo.Customer = new QueryCustomerDto();
            var input = curCustomRegInfo;
            input.CustomerBM = txtCustoemrCode.EditValue?.ToString();
            if (string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                dxErrorProvider.SetError(txtCustoemrCode, string.Format(Variables.MandatoryTips, "体检号"));
                txtCustoemrCode.Focus();
                return null;
            }
            input.Customer.ArchivesNum = txtArchivesNum.EditValue?.ToString().Trim();
            if (string.IsNullOrWhiteSpace(input.Customer.ArchivesNum))
            {
                input.Customer.ArchivesNum = input.CustomerBM;
            }
            input.Customer.Name = txtName.EditValue?.ToString().Trim();
            if (string.IsNullOrWhiteSpace(input.Customer.Name))
            {
                dxErrorProvider.SetError(txtName, string.Format(Variables.MandatoryTips, "姓名"));
                txtName.Focus();
                return null;
            }
            if (comSex.EditValue != null)
            {
                input.Customer.Sex = (int)comSex.EditValue;
            }
            else
            {
                dxErrorProvider.SetError(comSex, string.Format(Variables.MandatoryTips, "性别"));
                comSex.Focus();
                return null;
            }
            if (txtAge.EditValue != null)
            {
                input.Customer.Age = input.RegAge = Convert.ToInt32(txtAge.EditValue);
            }
            else
            {
                dxErrorProvider.SetError(txtAge, string.Format(Variables.MandatoryTips, "年龄"));
                txtAge.Focus();
                return null;
            }

            if (string.IsNullOrWhiteSpace(txtTeamID.Text))
            {
                if (!isCustomerReg)
                {
                    dxErrorProvider.SetError(txtTeamID, string.Format(Variables.MandatoryTips, "分组"));
                    txtTeamID.Focus();
                    return null;
                }
            }
            else
            {
                input.ClientTeamInfo_Id = Guid.Parse(txtTeamID.EditValue.ToString());
                var teams = txtTeamID.Properties.DataSource as List<ClientTeamInfoDto>;
                if (teams != null)
                    input.ClientTeamInfo = teams.FirstOrDefault(o => o.Id == input.ClientTeamInfo_Id);
            }

            if (!isCustomerReg)
            {
                var _service = new ClientRegAppService();
                var groups = _service.GetClientTeamRegByClientId(new SearchClientTeamInfoDto() { Id = input.ClientTeamInfo_Id.Value });
                if (input.CustomerItemGroup == null)
                    input.CustomerItemGroup = new List<TjlCustomerItemGroupDto>();
                else
                {
                    var delList = new List<TjlCustomerItemGroupDto>();
                    foreach (var items in curCustomRegInfo.CustomerItemGroup)
                    {//已有正常项目的处理：
                        if (items.IsAddMinus != (int)AddMinusType.Normal)
                            continue;

                        if (!groups.Any(o => o.ItemGroup?.Id == items.ItemGroupBM_Id))
                        {
                            if (items.MReceiptInfoClientlId.HasValue)
                            {
                                ShowMessageBoxWarning("该人员所在单位已为其结算，请先作废发票，再调整分组。");
                                txtTeamID.EditValue = txtTeamID.OldEditValue;
                                return null;
                            }
                            if (items.PayerCat == (int)PayerCatType.ClientCharge && items.CheckState != (int)PhysicalEState.Not)
                            {
                                items.PayerCat = (int)PayerCatType.NoCharge;
                                items.TTmoney = 0.00M;
                                items.GRmoney = items.ItemPrice;
                                items.PriceAfterDis = items.ItemPrice;
                                items.DiscountRate = 1.00M;
                                items.IsAddMinus = (int)AddMinusType.Add;
                            }
                            else
                                delList.Add(items);
                        }
                    }
                    foreach (var del in delList)
                    {
                        curCustomRegInfo.CustomerItemGroup.Remove(del);
                    }
                }

                foreach (var g in groups)
                {
                    if (input.CustomerItemGroup.Any(o => o.ItemGroupBM_Id == g.ItemGroup?.Id))
                    {
                        continue;
                    }
                    var info = new TjlCustomerItemGroupDto()
                    {
                        ItemGroupBM_Id = g.ItemGroup.Id,
                        ItemPrice = g.ItemGroupMoney,
                        PriceAfterDis = g.ItemGroupDiscountMoney,
                        ItemGroupName = g.ItemGroupName,
                        DiscountRate = g.Discount,
                        GRmoney = 0.00M,
                        IsAddMinus = (int)AddMinusType.Normal,
                        ItemGroupOrder = g.ItemGroupOrder,
                        PayerCat = g.PayerCatType,
                        TTmoney = g.ItemGroupDiscountMoney,
                        ItemSuitId = g.ItemSuitId,//g.ItemSuit_Id,
                        ItemSuitName = g.ItemSuitName,
                        CheckState = (int)ProjectIState.Not,
                        GuidanceSate = (int)PrintSate.NotToPrint,
                        BarState = (int)PrintSate.NotToPrint,
                        RequestState = (int)PrintSate.NotToPrint,
                        RefundState = (int)PayerCatType.NotRefund,
                        BillingEmployeeBMId = CurrentUser.Id,
                        SummBackSate = (int)SummSate.NotAlwaysCheck,
                        SFType = Convert.ToInt32(g.ItemGroup.ChartCode),

                    };
                    if (g.PayerCatType == (int)PayerCatType.ClientCharge)
                    {
                        info.GRmoney = 0.00M;
                        info.TTmoney = g.ItemGroupDiscountMoney;
                    }
                    else if (g.PayerCatType == (int)PayerCatType.PersonalCharge)
                    {
                        info.GRmoney = g.ItemGroupDiscountMoney;
                        info.TTmoney = 0.00M;
                        info.PayerCat = (int)PayerCatType.NoCharge;
                    }
                    var depart = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == g.ItemGroup.DepartmentId);
                    if (depart != null)
                    {
                        info.DepartmentId = depart.Id;
                        info.DepartmentName = depart.Name;
                        info.DepartmentOrder = depart.OrderNum;
                    }
                    if (g.IsZYB.HasValue)
                    {
                        info.IsZYB = g.IsZYB;
                    }
                    input.CustomerItemGroup.Add(info);
                }
            }
            if (comMarriageStatus.EditValue != null)
            {
                input.MarriageStatus = (int)comMarriageStatus.EditValue;
            }
            else
            {
                input.MarriageStatus = null;
            }
            input.Customer.MarriageStatus = input.MarriageStatus;
            if (txtConceive.EditValue != null)
            {
                input.ReadyPregnancybirth = (int)txtConceive.EditValue;
            }
            else
            {
                input.ReadyPregnancybirth = null;
            }
            input.Customer.Mobile = txtMobile.EditValue?.ToString().Trim();
            input.Customer.IDCardNo = txtIDCardNo.EditValue?.ToString().Trim();
            if (txtIDCardType.EditValue == null)
            {
                input.Customer.IDCardType = null;
            }
            else
            {
                input.Customer.IDCardType = (int)txtIDCardType.EditValue;
            }
            if (DateBirthday.EditValue != null)
            {
                input.Customer.Birthday = DateBirthday.DateTime;
            }
            if (DateChekDate.EditValue == null)
            {
                input.BookingDate = null;
            }
            else
            {
                input.BookingDate = (DateTime)DateChekDate.EditValue;
            }
            if (lookUpEditCustomerType.EditValue == null)
            {
                input.CustomerType = null;
            }
            else
            {
                input.CustomerType = (int)lookUpEditCustomerType.EditValue;
            }
            input.Customer.CustomerType = input.CustomerType;
            if (lookUpEditClientType.EditValue == null)
                input.PhysicalType = null;
            else
                input.PhysicalType = (int)lookUpEditClientType.EditValue;
            if (!string.IsNullOrWhiteSpace(txtPersonnelCategory.EditValue?.ToString()))
            {
                input.PersonnelCategoryId = Guid.Parse(txtPersonnelCategory.EditValue.ToString());
            }
            else
            {
                input.PersonnelCategoryId = null;
            }

            input.Customer.Telephone = txtTelephone.EditValue?.ToString();
            if (txtBookingDate.EditValue != null)
                input.AppointmentTime = txtBookingDate.DateTime;
            else
                input.AppointmentTime = null;
            input.Customer.CardNumber = txtCardNumber.EditValue?.ToString();
            input.Customer.WorkNumber = txtWorkNumber.EditValue?.ToString();
            input.Customer.CustomerTrade = txtCustomerTrade.EditValue?.ToString();
            input.Customer.Duty = txtDuty.EditValue?.ToString();
            if (txtDegree.EditValue == null)
                input.Customer.Degree = null;
            else
                input.Customer.Degree = (int)txtDegree.EditValue;
            input.LastMenstruation = txtLastMenstruation.EditValue?.ToString();
            input.Gestation = txtGestation.EditValue?.ToString();
            input.Customer.MedicalCard = txtMedicalCard.EditValue?.ToString();
            input.Customer.SectionNum = txtSectionNum.EditValue?.ToString();
            input.Customer.VisitCard = txtVisitCard.EditValue?.ToString();
            input.Customer.HospitalNum = txtHospitalNum.EditValue?.ToString();
            if (lookUpEditInfoSource.EditValue != null)
            {
                input.InfoSource = Convert.ToInt32(lookUpEditInfoSource.EditValue);
            }
            else
            {
                input.InfoSource = null;
            }
            if (txtSecretlevel.EditValue != null)
                input.Customer.Secretlevel = (int)txtSecretlevel.EditValue;
            else
                input.Customer.Secretlevel = null;
            input.Customer.GuoJi = txtGuoJi.EditValue?.ToString();
            if (txtEmailReport.EditValue != null)
                input.EmailReport = (int)txtEmailReport.EditValue;
            else
                input.EmailReport = null;
            input.Customer.Email = txtEmail.EditValue?.ToString();
            if (txtReportBySelf.EditValue == null)
                input.ReportBySelf = null;
            else
                input.ReportBySelf = (int)txtReportBySelf.EditValue;
            input.Customer.PostgCode = txtPostgCode.EditValue?.ToString();
            input.Customer.Qq = txtQq.EditValue?.ToString();
            input.Customer.StoreAdressP = txtStoreAdressP.EditValue?.ToString();
            input.Customer.StoreAdressS = comStoreAdressS.EditValue?.ToString();
            input.Customer.StoreAdressQ = txtStoreAdressQ.EditValue?.ToString();
            input.Customer.Address = txtAdress.EditValue?.ToString();
            input.Customer.HomeAddress = txthomeAdress.EditValue?.ToString();
            if (txtClientRegID.EditValue == null)
                input.ClientRegId = null;
            else
                input.ClientRegId = Guid.Parse(txtClientRegID.EditValue.ToString());
            if (checkReplaceSate.Checked)
                input.ReplaceSate = 2;
            else
                input.ReplaceSate = 1;
            input.PrimaryName = txtPrimaryName.EditValue?.ToString();
            input.Customer.Remarks = txtRemarks1.EditValue?.ToString();
            if (txtReviewSate.EditValue == null)
                input.ReviewSate = null;
            else
                input.ReviewSate = (int)txtReviewSate.EditValue;
            if (checkBlindSate.Checked)
            {
                input.BlindSate = 2;
            }
            else
            {
                input.BlindSate = 1;
            }
            if (checkUrgentState.Checked)
            {
                input.UrgentState = 2;
            }
            else
            {
                input.UrgentState = 1;
            }
            if (checkHaveBreakfast.Checked)
                input.HaveBreakfast = (int)BreakfastType.Eat;
            else
                input.HaveBreakfast = (int)BreakfastType.NotEat;
            //if (labHaveBreakfast.EditValue != null)
            //{
            //    input.HaveBreakfast =(int)labHaveBreakfast.EditValue;
            //}
            //else
            //{
            //    input.HaveBreakfast = null;
            //}
            if (chckFamilyState.Checked)
            {
                input.FamilyState = 2;
            }
            else
            {
                input.FamilyState = 1;
            }
            if (checkMsg.Checked)
                input.Message = (int)MessageEmailState.Open;
            else
                input.Message = (int)MessageEmailState.Close;
            //if (checkMessage.EditValue != null)
            //    input.Message = (int)checkMessage.EditValue;
            //else
            //    input.Message = null;
            if (checkMailingReport.Checked)
                input.MailingReport = 1;
            else
                input.MailingReport = 2;
            if (checkExamPlace.Checked)
                input.ExamPlace = 2;
            else
                input.ExamPlace = 1;
            input.Introducer = txtIntroducer.Text;
            input.JKZBM = txtJKZ.Text;
            input.Customer.Department = txtDepartment.Text;
            input.Customer.Nation = txtNation.Text; ;
            if (!input.SummSate.HasValue)
                input.SummSate = (int)SummSate.NotAlwaysCheck;
            if (!input.SendToConfirm.HasValue)
                input.SendToConfirm = (int)SendToConfirm.No;
            input.Remarks = textEditRegRemark.EditValue?.ToString();
            //职业健康
            if (txtRiskS.Tag != null && txtRiskS.EditValue!=null)
            {
                input.OccHazardFactors= (List<ShowOccHazardFactorDto>)txtRiskS.Tag;
                input.RiskS = txtRiskS.EditValue.ToString();
            }
            if (txtTypeWork.EditValue != null)
            {
                input.TypeWork = txtTypeWork.EditValue.ToString();
            }
            else
            { input.TypeWork = ""; }
            if (txtWorkName.EditValue != null)
            {
                input.WorkName = txtWorkName.EditValue.ToString();
            }
            else
            { input.WorkName = ""; }
            if (txtCheckType.EditValue != null)
            {
                input.PostState = txtCheckType.EditValue.ToString();
            }
            else
            {
                input.PostState = "";
            }
            if (txtTotalWorkAge.EditValue != null)
            {
                input.TotalWorkAge = txtTotalWorkAge.EditValue.ToString();
                input.WorkAgeUnit = comunit.Text;
            }            
            if (txtInjuryAge.EditValue != null)
            {
                input.InjuryAge = txtInjuryAge.EditValue.ToString();
                input.InjuryAgeUnit = comunit2.Text;
            }
             
                input.employerEnterpriseName = textEditClientname.EditValue?.ToString();                
            
            
                input.employerCreditCode= textEditOther.EditValue?.ToString();

            curCustomRegInfo.OtherTypeWork = textEditOther.EditValue?.ToString();
            return input;
        }



        #endregion

        private void txtAge_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAge.Text)) return;
            var year = DateTime.Now.Year - Convert.ToInt32(txtAge.EditValue);
            var date = new DateTime(year, DateBirthday.DateTime.Month, DateBirthday.DateTime.Day);
            if (date != DateBirthday.DateTime)
                DateBirthday.EditValue = date;
            upteam();
        }

        private void DateBirthday_Leave(object sender, EventArgs e)
        {
            if (DateBirthday.EditValue != null)
            {
                var age = DateTime.Now.Year - DateBirthday.DateTime.Year;
                if (txtAge.EditValue?.ToString() != age.ToString())
                    txtAge.EditValue = age;
            }
        }

        private void txtIDCardNo_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {

            if (txtIDCardType.Text.Contains("身份证"))
            {
                dxErrorProvider.ClearErrors();
                if (e.Value?.ToString() == null)
                {
                    txtAge.ReadOnly = false;
                    comSex.ReadOnly = false;
                    DateBirthday.ReadOnly = false;
                    return;
                }
                else
                {
                    var data = VerificationHelper.GetByIdCard(e.Value?.ToString());
                    if (data != null)
                    {
                        txtAge.EditValue = data.Age;
                        comSex.EditValue = (int)data.Sex;
                        DateBirthday.EditValue = data.Birthday;
                        txtAge.ReadOnly = true;
                        comSex.ReadOnly = true;
                        DateBirthday.ReadOnly = true;
                    }
                    else
                    {
                        dxErrorProvider.SetError(txtIDCardNo, string.Format("身份证号输入错误"));
                        txtIDCardNo.EditValue = null;
                        txtIDCardNo.Focus();
                        txtAge.ReadOnly = false;
                        comSex.ReadOnly = false;
                        DateBirthday.ReadOnly = false;
                    }
                }
            }
        }

        private void txtStoreAdressP_EditValueChanged(object sender, EventArgs e)
        {
            comStoreAdressS.EditValue = null;
            if (txtStoreAdressP.EditValue != null)
            {
                comStoreAdressS.Properties.DataSource =
                    AdministrativeDivisionHelper.GetCity(new AdministrativeDivisionDto
                    { Code = txtStoreAdressP.EditValue.ToString() });
            }
        }

        private void comStoreAdressS_EditValueChanged(object sender, EventArgs e)
        {
            txtStoreAdressQ.EditValue = null;
            if (comStoreAdressS.EditValue != null)
            {
                txtStoreAdressQ.Properties.DataSource =
                    AdministrativeDivisionHelper.GetCounty(new AdministrativeDivisionDto
                    { Code = comStoreAdressS.EditValue.ToString() });
            }
        }

        private void txtTeamID_EditValueChanged(object sender, EventArgs e)
        {
            if (curCustomRegInfo.Id == Guid.Empty)
            {
                if (!string.IsNullOrWhiteSpace(txtTeamID.EditValue?.ToString()))
                {
                    var teams = txtTeamID.Properties.DataSource as List<ClientTeamInfoDto>;
                    if (teams != null)
                    {
                        var team = teams.FirstOrDefault(o => o.Id == Guid.Parse(txtTeamID.EditValue.ToString()));
                        if (team != null)
                        {
                            if (team.EmailStatus == 2)
                                txtEmailReport.EditValue = (int)MessageEmailState.Open;
                            else
                                txtEmailReport.EditValue = (int)MessageEmailState.Close;
                            if (team.MessageStatus == 2)
                                checkMsg.Checked = true;
                            else
                                checkMsg.Checked = false;
                            if (team.BreakfastStatus == (int)BreakfastType.Eat)
                                checkHaveBreakfast.Checked = true;
                            else
                                checkHaveBreakfast.Checked = false;
                            if (team.HealthyMGStatus == 2)
                                checkMailingReport.Checked = true;
                            else
                                checkMailingReport.Checked = false;
                            if (team.BlindSate == (int)BlindSate.Normal)
                                checkBlindSate.Checked = false;
                            else if (team.BlindSate == (int)BlindSate.Blind)
                                checkBlindSate.Checked = true;
                            lookUpEditClientType.EditValue = team.TJType;

                            if (team.OccHazardFactors != null && team.OccHazardFactors.Count > 0)
                            {
                                
                                    txtRiskS.Tag = team.OccHazardFactors;
                                    txtRiskS.EditValue = team.RiskName;
                                
                            }
                            if (team.WorkShop != null)
                                txtWorkName.EditValue = team.WorkShop;
                            if (team.CheckType != null)
                                txtCheckType.EditValue = team.CheckType;
                            if (team.WorkType != null)
                                txtTypeWork.EditValue = team.WorkType;
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(txtTeamID.OldEditValue?.ToString()) || txtTeamID.EditValue?.ToString() == curCustomRegInfo.ClientTeamInfo_Id?.ToString())
                return;
            if (curCustomRegInfo.CustomerItemGroup == null)
                return;
            if (curCustomRegInfo.CustomerItemGroup.Any(o => o.MReceiptInfoClientlId.HasValue))
            {
                ShowMessageBoxWarning("该人员所在单位已为其结算，请先作废发票，再调整分组。");
                txtTeamID.EditValue = txtTeamID.OldEditValue;
                return;
            }

        }
        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {                
                var result = customerSvr.getOneAddXQuestionnaires();
                splashScreenManager.SetWaitFormDescription(Variables.LoadingForForm);
                layoutControlGroupBase.BeginUpdate();
                foreach (var ques in result)
                {
                    if (ques.Category == "家族史")
                    {
                        var checkEdit = new CheckEdit
                        {
                            Text = ques.Name
                        };
                       flowfamily.Controls.Add(checkEdit);
                       checkEdit.Name= ques.Id.ToString("N");

                    }
                    if (ques.Category == "现病史")
                    {
                        var checkEdit = new CheckEdit
                        {
                            Text = ques.Name
                        };
                        flownow.Controls.Add(checkEdit);
                        checkEdit.Name = ques.Id.ToString("N");
                    }                  
                    
                    if (ques.Category == "现有症状或体征")
                    {
                        var checkEdit = new CheckEdit
                        {
                            Text = ques.Name
                        };
                        checkEdit.Width = 250;
                        flownowill.Controls.Add(checkEdit);
                        checkEdit.Name = ques.Id.ToString("N");

                    }
                  
                }

                layoutControlGroupBase.EndUpdate();
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
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
        private void InitialUpdate(EntityDto<Guid> input)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
            try
            {
                
                var modules = customerSvr.getCustomerQuestionDtos(input);
               splashScreenManager.SetWaitFormDescription(Variables.LoadingEditor);                
                foreach (var module in modules)
                {
                    if (module.QuestionType == "家族史")
                    {
                        var name = module.OneAddXQuestionnaireid.Value.ToString("N");
                        // var item = layoutControlGroupFamily.Items.FindByName(name);
                        
                        var item = flowfamily.Controls.Find(name,false);                        
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "现病史")
                    {
                        var name = module.OneAddXQuestionnaireid.Value.ToString("N");
                        var item = flownow.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                    else if (module.QuestionType == "现有症状或体征")
                    {
                        var name = module.OneAddXQuestionnaireid.Value.ToString("N");                       
                        var item = flownowill.Controls.Find(name, false);
                        ((CheckEdit)item[0]).Checked = true;
                    }
                }
                
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
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
        private void getCusques(EntityDto<Guid> input)
        {
            var cusques = customerSvr.getSearchItemSuitDto(input);
            gridControlSuit.DataSource = cusques;
        }
        private void getCusGroups(EntityDto<Guid> input)
        {
            var cusgroups = customerSvr.getCustomerAddPackageItem(input);
         
            gridControl2.DataSource = cusgroups;

        }
        //读身份证
        private void txtIDCardNo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ReadIdCard();
        }

        private void CusDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                ReadIdCard();
        }
        //读取身份证数据
        public void ReadIdCard()
        {
            if (!curCustomRegInfo.ClientRegId.HasValue)
            {
                ShowMessageBoxInformation("当前不是新增人员，不可读卡");
                return;
            }
            driver = DriverFactory.GetDriver<IIdCardReaderHardwareDriver>();
            if (driver != null)
            {
                var card = driver.ReadCardInfo();
                if (card.Succeed)
                {
                    try
                    {
                        var data = customerSvr.QueryCustomerReg(new SearchCustomerDto() { IDCardNo = card.Card.IdCardNo, NotCheckState = (int)PhysicalEState.Complete });
                        if (data != null)
                            curCustomRegInfo = data.FirstOrDefault();
                        if(curCustomRegInfo==null)
                            curCustomRegInfo = new QueryCustomerRegDto() { Customer = new QueryCustomerDto(), CustomerItemGroup = new List<TjlCustomerItemGroupDto>() };
                        if (!string.IsNullOrWhiteSpace(curCustomRegInfo.CustomerBM))
                        {
                            ShowMessageBoxInformation("该人员已经有正在体检的信息，完成后才可重新预约。");
                            return;
                        }
                        curCustomRegInfo.Customer.IDCardNo = card.Card.IdCardNo;
                        curCustomRegInfo.Customer.Name = card.Card.Name;
                        int sexCode = 1;
                        if (card.Card.Sex.Contains("女"))
                        {
                            sexCode = 2;

                        }
                        curCustomRegInfo.Customer.Sex = sexCode;
                        DateTime.TryParseExact(card.Card.Birthday,
                            "yyyyMMdd",
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.None,
                            out var dt);
                        curCustomRegInfo.Customer.Birthday = dt;
                        if (card.Card.CertType == "I")
                        {// 外国人居留证
                            var idCard = (ResidencePermit)card.Card;
                            var waiguo = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "外国人居留证");
                            if (waiguo != null)
                                curCustomRegInfo.Customer.IDCardType = waiguo.Value;
                            curCustomRegInfo.Customer.GuoJi = idCard.NationCode;
                        }
                        else if (card.Card.CertType == "J")
                        {//港澳台居住证
                            var idCard = (ResidentialPass)card.Card;
                            var gangao = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.CertificateType).ToString()).FirstOrDefault(o => o.Text == "港澳台居住证");
                            if (gangao != null)
                                curCustomRegInfo.Customer.IDCardType = gangao.Value;
                            curCustomRegInfo.Customer.Address = idCard.Address;
                        }
                        else if (card.Card.CertType == string.Empty)
                        {//身份证
                            var idCard = (IdCard)card.Card;
                            curCustomRegInfo.Customer.Address = idCard.Address;
                        }
                        //加载数据到控件
                        ReSetData();
                    }
                    catch (Exception ex)
                    {
                        ShowMessageBoxWarning(ex.Message);
                    }

                }
                else
                {
                    ShowMessageBoxWarning(card.Explain);
                }
            }
        }
        //重新加载控件数据
        public void ReSetData()
        {
            //txtCustoemrCode.EditValue = curCustomRegInfo.CustomerBM;
            txtArchivesNum.EditValue = curCustomRegInfo.Customer.ArchivesNum;
            txtName.EditValue = curCustomRegInfo.Customer.Name;
            comSex.EditValue = curCustomRegInfo.Customer.Sex;
            txtAge.EditValue = curCustomRegInfo.Customer.Age;
            comMarriageStatus.EditValue = curCustomRegInfo.Customer.MarriageStatus;
            txtMobile.EditValue = curCustomRegInfo.Customer.Mobile;
            txtIDCardNo.EditValue = curCustomRegInfo.Customer.IDCardNo;
            txtIDCardType.EditValue = curCustomRegInfo.Customer.IDCardType;
            DateBirthday.EditValue = curCustomRegInfo.Customer.Birthday;
            if (curCustomRegInfo.CustomerType.HasValue)
                lookUpEditCustomerType.EditValue = curCustomRegInfo.CustomerType;
            else
            {
                lookUpEditCustomerType.EditValue = null;
                //lookUpEditCustomerType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CustomerType.ToString())?.ToList().FirstOrDefault().Value;

            }
            if (curCustomRegInfo.PhysicalType.HasValue)
            {
                lookUpEditClientType.EditValue = Convert.ToInt32(curCustomRegInfo.PhysicalType);
            }
            else
            {
                lookUpEditClientType.EditValue = null;
                lookUpEditClientType.EditValue = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.ToList().FirstOrDefault().Value;
            }
            if (curCustomRegInfo.PersonnelCategoryId.HasValue)
                txtPersonnelCategory.EditValue = curCustomRegInfo.PersonnelCategoryId;

            txtTelephone.EditValue = curCustomRegInfo.Customer.Telephone;
            txtBookingDate.EditValue = DateTime.Now;
            txtCardNumber.EditValue = curCustomRegInfo.Customer.CardNumber;
            txtWorkNumber.EditValue = curCustomRegInfo.Customer.WorkNumber;
            ///txtCustomerTrade.EditValue = curCustomRegInfo.Customer.CustomerTrade;
            if (int.TryParse(curCustomRegInfo.Customer.CustomerTrade, out int outrTrade))
            {
                txtCustomerTrade.EditValue = outrTrade;
            }
            txtDuty.EditValue = curCustomRegInfo.Customer.Duty;
            txtDegree.EditValue = curCustomRegInfo.Customer.Degree;
            txtLastMenstruation.EditValue = curCustomRegInfo.LastMenstruation;
            txtGestation.EditValue = curCustomRegInfo.Gestation;
            txtMedicalCard.EditValue = curCustomRegInfo.Customer.MedicalCard;
            txtSectionNum.EditValue = curCustomRegInfo.Customer.SectionNum;
            txtVisitCard.EditValue = curCustomRegInfo.Customer.VisitCard;
            txtHospitalNum.EditValue = curCustomRegInfo.Customer.HospitalNum;
            lookUpEditInfoSource.EditValue = curCustomRegInfo.InfoSource;
            txtSecretlevel.EditValue = curCustomRegInfo.Customer.Secretlevel;
            txtGuoJi.EditValue = curCustomRegInfo.Customer.GuoJi;
            txtEmailReport.EditValue = curCustomRegInfo.EmailReport;
            txtEmail.EditValue = curCustomRegInfo.Customer.Email;
            txtReportBySelf.EditValue = curCustomRegInfo.ReportBySelf;
            txtPostgCode.EditValue = curCustomRegInfo.Customer.PostgCode;
            txtQq.EditValue = curCustomRegInfo.Customer.Qq;
            if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressP))
                txtStoreAdressP.EditValue = curCustomRegInfo.Customer.StoreAdressP;
            if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressS))
                comStoreAdressS.EditValue = curCustomRegInfo.Customer.StoreAdressS;
            if (!string.IsNullOrWhiteSpace(curCustomRegInfo.Customer.StoreAdressQ))
                txtStoreAdressQ.EditValue = curCustomRegInfo.Customer.StoreAdressQ;
            txtAdress.EditValue = curCustomRegInfo.Customer.Address;
            txthomeAdress.EditValue = curCustomRegInfo.Customer.HomeAddress;
            txtRemarks1.EditValue = curCustomRegInfo.Customer.Remarks;
            txtReviewSate.EditValue = curCustomRegInfo.ReviewSate;
        }

        private void gridLookUpEdit1View_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == colFree.FieldName)
            {
                var data = gridLookUpEdit1View.GetRowCellValue(e.ListSourceRowIndex, colIsFree);
                if (data != null)
                {
                    if ((bool)data)
                        e.DisplayText = "是";
                    else
                        e.DisplayText = "否";
                }
            }
        }

        private void comMarriageStatus_EditValueChanged(object sender, EventArgs e)
        {
            upteam();
        }

        private void txtConceive_EditValueChanged(object sender, EventArgs e)
        {
            upteam();
        }
        private void upteam()
        {
            if (!string.IsNullOrWhiteSpace(txtClientRegID.EditValue?.ToString()))
            {
                var list = customerSvr.QueryClientTeamInfos(new ClientTeamInfoDto() { ClientReg_Id = Guid.Parse(txtClientRegID.EditValue?.ToString()) });
                if (!string.IsNullOrWhiteSpace(txtConceive.EditValue?.ToString()) && Convert.ToInt32(txtConceive.EditValue)!= (int)BreedState.No)
                {
                    list = list.Where(o => o.ConceiveStatus == Convert.ToInt32(txtConceive.EditValue) || o.ConceiveStatus == (int)BreedState.No)?.ToList();

                }
                if (!string.IsNullOrWhiteSpace(comMarriageStatus.EditValue?.ToString()) && Convert.ToInt32(comMarriageStatus.EditValue)!= (int)MarrySate.Unstated)
                {
                    int marrysate = (int)MarrySate.Unstated;
                    list = list.Where(o => o.MaritalStatus == Convert.ToInt32(comMarriageStatus.EditValue) || o.MaritalStatus == marrysate)?.ToList();

                }  
                if (!string.IsNullOrWhiteSpace(comSex.EditValue?.ToString()) && Convert.ToInt32(comSex.EditValue) != (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified)
                {
                    list = list.Where(o => o.Sex == Convert.ToInt32(comSex.EditValue) || o.Sex == (int)global::HealthExaminationSystem.Enumerations.Sex.GenderNotSpecified)?.ToList();
                }
                if (txtAge.Text != "")
                {
                    list = list.Where(o => o.MinAge <= int.Parse(txtAge.Text) && o.MaxAge >= int.Parse(txtAge.Text))?.ToList();
                }
                    txtTeamID.Properties.DataSource = list;
            }
           
        }

        private void lookUpEditClientType_EditValueChanged(object sender, EventArgs e)
        {
            if ((Variables.ISZYB=="1" && lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("职业")) || Variables.ISZYB == "2")
            {
                //检查类型
                ChargeBM chargeBM = new ChargeBM();

                chargeBM.Name = ZYBBasicDictionaryType.Checktype.ToString();
                var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtCheckType.Properties.DataSource = lis1;
                //车间
                chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                var lis2 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtWorkName.Properties.DataSource = lis2;
                //工种
                chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
                var lis3 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                txtTypeWork.Properties.DataSource = lis3;
                groupzyb.Visibility = LayoutVisibility.Always;
                comunit2.Text = "年";
                comunit.Text= "年";
                layjjz.Visibility = LayoutVisibility.Never;
            }
            else
            {
                groupzyb.Visibility = LayoutVisibility.Never;
                layjjz.Visibility = LayoutVisibility.Never;
                if (lookUpEditClientType.EditValue != null && lookUpEditClientType.Text.ToString().Contains("食品"))
                {
                    txtJKZ.Text = iIDNumberAppService.CreateJKZBM();
                    layjjz.Visibility = LayoutVisibility.Always;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmSelectHazard frmSelectHazard = new frmSelectHazard();
            if (txtRiskS.Tag != null && txtRiskS.Text != "")
            {
                var riskls = (List<ShowOccHazardFactorDto>)txtRiskS.Tag;
                frmSelectHazard.outOccHazardFactors = riskls;


            }
            if (frmSelectHazard.ShowDialog() == DialogResult.OK)
            {
                txtRiskS.Tag = null;
                var Hazard = frmSelectHazard.outOccHazardFactors;
                txtRiskS.Tag = Hazard;
                txtRiskS.Text = string.Join("，", Hazard.Select(o => o.Text).ToList()).TrimEnd('，');
            }
        }

        private void txtWorkName_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void txtTypeWork_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var editor = sender as PopupBaseEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (editor.EditValue == null)
                    return;
                editor.EditValue = null;

            }
        }

        private void txtCheckType_ButtonClick(object sender, ButtonPressedEventArgs e)
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
