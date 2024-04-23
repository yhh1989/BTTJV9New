using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using DevExpress.XtraTreeList.Nodes;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseConsulitation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccHazardFactor;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using DevExpress.XtraLayout.Utils;
using System.Diagnostics;
using System.Net;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination;
using Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisProposalNew;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations.Helpers;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccdiseaseSet
{
    public partial class OccdieaseConsultation : UserBaseForm
    {
        private readonly IOccdiseaseConsulitationAppService _OccdiseaseConsulitationAppService;
        private readonly IOccDisProposalNewAppService _OccDisProposalNewAppService;
        private PictureController _pictureController;
        private readonly IOccHazardFactorAppService _OccHazardFactorAppService;
        private string _cusRegBM = "";
        private readonly PhysicalExaminationAppService _PhysicalAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IInspectionTotalAppService _inspectionTotalService;
        public static string ParentIds;
        public static string keys;
        public static string Names;

        public OccdieaseConsultation()
        {
            InitializeComponent();
            _OccdiseaseConsulitationAppService = new OccdiseaseConsulitationAppService();
            _OccDisProposalNewAppService = new OccDisProposalNewAppService();
            _pictureController = new PictureController();
            _OccHazardFactorAppService = new OccHazardFactorAppService();
            _PhysicalAppService = new PhysicalExaminationAppService();
            _commonAppService = new CommonAppService();
            _inspectionTotalService = new InspectionTotalAppService();
        }
        public OccdieaseConsultation(string customerRegBM) : this()
        {
            _cusRegBM = customerRegBM;

        }
        private Guid _id;
        private OccdieaseBasicInformationDto CurrCusReg = null;
        private static string PicAddress { get; set; }
        private Guid? singImage { get; set; }

        private void OccdieaseConsultation_Load(object sender, EventArgs e)
        {
            var strqt = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ForegroundFunctionControl, 115)?.Remarks;
            if (strqt != null && strqt == "N")
            {
                layoutControlGroup3.Visibility = LayoutVisibility.Never;
            }
            //痊愈           
            var listQY = new List<EnumModel>();
            listQY.Add(new EnumModel { Id = 0, Name = "否" });
            listQY.Add(new EnumModel { Id = 1, Name = "是" });
            listQY.Add(new EnumModel { Id = 2, Name = "无" });
            repositoryItemLookUpEdit3.DataSource = listQY;
            repositoryItemLookUpEdit3.DisplayMember = "Name";
            repositoryItemLookUpEdit3.ValueMember = "Id";
            repositoryItemLookUpEdit3.KeyMember = "Id";
            // repositoryItemSearchLookUpEdit8.EditValue = "无";


            //吸烟
            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 1, Name = "从不吸烟" });
            lists.Add(new EnumModel { Id = 2, Name = "偶尔吸烟" });
            lists.Add(new EnumModel { Id = 3, Name = "曾吸烟，已戒烟" });
            lists.Add(new EnumModel { Id = 4, Name = "经常吸烟" });
            smoke.Properties.DataSource = lists;
            smoke.EditValue = "从不吸烟";
            //饮酒
            var list = new List<EnumModel>();
            list.Add(new EnumModel { Id = 1, Name = "从不饮酒" });
            list.Add(new EnumModel { Id = 2, Name = "偶尔饮酒" });
            list.Add(new EnumModel { Id = 3, Name = "曾饮酒，已戒酒" });
            list.Add(new EnumModel { Id = 4, Name = "经常饮酒" });
            Drink.Properties.DataSource = list;
            Drink.EditValue = "从不饮酒";

            ////流产与否
            //var listss = new List<EnumModel>();
            //listss.Add(new EnumModel { Id = 0, Name = "是" });
            //listss.Add(new EnumModel { Id = 1, Name = "否" });
            //txtISLC.Properties.DataSource = listss;
            //txtISLC.EditValue = "否";

            ////早产与否
            //var zaochan = new List<EnumModel>();
            //zaochan.Add(new EnumModel { Id = 0, Name = "是" });
            //zaochan.Add(new EnumModel { Id = 1, Name = "否" });
            //txtIsZC.Properties.DataSource = zaochan;
            //txtIsZC.EditValue = "否";
            ////死产与否
            //var sichan = new List<EnumModel>();
            //sichan.Add(new EnumModel { Id = 0, Name = "是" });
            //sichan.Add(new EnumModel { Id = 1, Name = "否" });
            //txtIsDie.Properties.DataSource = sichan;
            //txtIsDie.EditValue = "否";
            //症状小类
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Symptom.ToString();
            var dll = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            repositoryItemSearchLookUpEdit6.DataSource = dll;
            repositoryItemSearchLookUpEdit6.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit6.ValueMember = "Text";

            //放射线种类
            ChargeBM chargeBMfs = new ChargeBM();
            chargeBMfs.Name = ZYBBasicDictionaryType.Radioactive.ToString();
            var fsdll = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBMfs);
            repositoryItemCheckedComboBoxEdit6.DataSource = fsdll;
            repositoryItemCheckedComboBoxEdit6.DisplayMember = "Text";
            repositoryItemCheckedComboBoxEdit6.ValueMember = "Id";

            repositoryItemCheckedComboBoxEdit7.DataSource = fsdll;
            repositoryItemCheckedComboBoxEdit7.DisplayMember = "Text";
            repositoryItemCheckedComboBoxEdit7.ValueMember = "Text";
            //家族史疾病
            ChargeBM chargeBMs = new ChargeBM();
            chargeBMs.Name = ZYBBasicDictionaryType.MedicalHistory.ToString();
            var dlls = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBMs);
            repositoryItemSearchLookUpEdit2.DataSource = dlls;
            repositoryItemSearchLookUpEdit2.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit2.ValueMember = "Text";
            repositoryItemSearchLookUpEdit3.DataSource = dlls;
            repositoryItemSearchLookUpEdit3.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit3.ValueMember = "Text";

            //治疗方式

            chargeBM.Name = ZYBBasicDictionaryType.Treatment.ToString();
            var zlfs = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            repositoryItemSearchLookUpEdit4.DataSource = zlfs;
            repositoryItemSearchLookUpEdit4.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit4.ValueMember = "Text";
            repositoryItemSearchLookUpEdit4.KeyMember = "Text";
            //工种
            chargeBM.Name = ZYBBasicDictionaryType.WorkType.ToString();
            var lis3 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            repositoryItemSearchLookUpEdit5.DataSource = lis3;
            repositoryItemSearchLookUpEdit5.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit5.ValueMember = "Text";
            repositoryItemSearchLookUpEdit5.KeyMember = "Text";

            //工种
            repositoryItemSearchLookUpEdit9.DataSource = lis3;
            repositoryItemSearchLookUpEdit9.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit9.ValueMember = "Text";
            repositoryItemSearchLookUpEdit9.KeyMember = "Text";
            //foreach (var dis in dlls)
            //{
            //    workType.Items.Add(dis.Text);
            //}
            //职业史-危害因素
            var outresult = _OccHazardFactorAppService.getSimpOccHazardFactors();

            repositoryItemCheckedComboBoxEdit1.DataSource = outresult;
            repositoryItemCheckedComboBoxEdit1.DisplayMember = "Text";
            repositoryItemCheckedComboBoxEdit1.ValueMember = "Id";

            //照射种类
            RadiationDto dto = new RadiationDto();
            var Radiation = _OccHazardFactorAppService.ShowRadiation(dto);

            repositoryItemCheckedComboBoxEdit5.DataSource = Radiation;
            repositoryItemCheckedComboBoxEdit5.DisplayMember = "Text";
            repositoryItemCheckedComboBoxEdit5.ValueMember = "Id";


            #region 加载基本信息
            //照射种类
            checkedComboBoxZSZL.Properties.DataSource = Radiation;
            checkedComboBoxZSZL.Properties.DisplayMember = "Text";
            checkedComboBoxZSZL.Properties.ValueMember = "Text";
            //婚否
           var    marrySateList = MarrySateHelper.GetMarrySateModelsForItemInfo();
            comMarriageStatus.Properties.DataSource = marrySateList;//婚否 

            //文化程度
            txtDegree.Properties.DataSource = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.DegreeType.ToString())?.ToList(); //CacheHelper.GetBasicDictionarys(BasicDictionaryType.DegreeType);


            #endregion



            //防护措施

            chargeBM.Name = ZYBBasicDictionaryType.Protect.ToString();
            var dl1 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);

            repositoryItemCheckedComboBoxEdit2.DataSource = dl1;
            repositoryItemCheckedComboBoxEdit2.DisplayMember = "Text";
            repositoryItemCheckedComboBoxEdit2.ValueMember = "Id";

            //车间
            chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
            var lis2 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            repositoryItemSearchLookUpEdit7.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit7.ValueMember = "Text";
            repositoryItemSearchLookUpEdit7.DataSource = lis2;
            //车间
            repositoryItemSearchLookUpEdit8.DataSource = lis2;
            repositoryItemSearchLookUpEdit8.DisplayMember = "Text";
            repositoryItemSearchLookUpEdit8.ValueMember = "Text";
            repositoryItemSearchLookUpEdit8.DataSource = lis2;




            if (!string.IsNullOrEmpty(_cusRegBM))
            {
                txtrisk.Text = _cusRegBM;
                simpleButton6.PerformClick();
            }

            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            teSJQ.DateTime = date;
            daSJZ.DateTime = date;

            gridView1.Columns[conSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[conSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);

        }

        //问卷id
        private Guid OccQuestionnaireId;

        //查询
        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            pictureEdit1.Image = null;
            picqm.Image = null;
            CurrCusReg = null;
            var OccGet = new OccdieaseBasicGet();
            OccGet.CustomerBM = txtrisk.Text.Trim();
            OccGet.Name = getname.Text.Trim();

            if (Startdate.EditValue != null)
            {
                var date = Startdate.DateTime.AddDays(-1);
                OccGet.StartTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            }
            if (Enddate.EditValue != null)
            {
                var date = Enddate.DateTime.AddDays(1);
                OccGet.EndTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            }
            try
            {
                //基本信息
                var result = _OccdiseaseConsulitationAppService.GetAllCustomer(OccGet);
                if (result.CostState == 1)
                {
                    MessageBox.Show("该体检人未收费不能问诊！");
                    return;
                }
                CurrCusReg = result;
                _id = result.Id;
                //txtrisk.Text = result.CustomerBM;
                name.Text = result.Name;

                sex.Text = result.Sex == 1 ? "男" : "女";
                if (sex.Text.Contains("女"))
                {
                    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                age.Text = result.Age.ToString();
                clientname.Text = result.ClientName;
                hurt.Text = result.RiskS;
                type.Text = result.TypeWork;
                time.Text = result.LoginDate.ToString();
                customerbm.Text = result.CustomerBM;
                group.Text = result.TeamName;
                labelWork.Text = result.PostState;
                labelGL.Text = result.InjuryAge + result.InjuryAgeUnit;
                comMarriageStatus.EditValue = result.MarriageStatus;
                txtDuty.Text = result.Duty;
                txtDegree.EditValue = result.Degree;
                txtMobile.EditValue = result.Mobile;
                checkedComboBoxZSZL.EditValue = "";
                checkedComboBoxZSZL.Text = "";
                checkedComboBoxZSZL.EditValue = result.RadiationName;

                #region 控制
                //职业健康            
                var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
                var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
                var tjlb = Clientcontract.FirstOrDefault(o => o.Value == result.PhysicalType)?.Text;
                if (tjlb != null && tjlb.Contains("放射"))
                {
                    layLifeHistory.Visibility = LayoutVisibility.Always;
                    layMerr.Visibility = LayoutVisibility.Always;
                    groupzys1.Visibility = LayoutVisibility.Always;
                    groupjzs.Visibility = LayoutVisibility.Always;
                    layoutControlItemfs.Visibility = LayoutVisibility.Always;
                    conWorkName.Caption = "部门";
                    //车间
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                    var lis2 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                      lis2 = lis2.Where(p=>!p.Remarks.Contains("职业")).ToList();
                    repositoryItemSearchLookUpEdit7.DisplayMember = "Text";
                    repositoryItemSearchLookUpEdit7.ValueMember = "Text";
                    repositoryItemSearchLookUpEdit7.DataSource = lis2;

                }
                else
                {
                    layLifeHistory.Visibility = LayoutVisibility.Never;
                    layMerr.Visibility = LayoutVisibility.Never;
                    groupzys1.Visibility = LayoutVisibility.Never;
                    groupjzs.Visibility = LayoutVisibility.Never;
                    layoutControlItemfs.Visibility = LayoutVisibility.Never;
                    conWorkName.Caption = "车间";
                    ChargeBM chargeBM = new ChargeBM();
                    chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                    var lis2 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                    lis2 = lis2.Where(p => p.Remarks!=null && !p.Remarks.Contains("放射")).ToList();
                    repositoryItemSearchLookUpEdit7.DisplayMember = "Text";
                    repositoryItemSearchLookUpEdit7.ValueMember = "Text";
                    repositoryItemSearchLookUpEdit7.DataSource = lis2;
                }
                #endregion
                //问卷
                var OccGets = new OccdieaseHistoryRucan();
                OccGets.Id = _id;
                var results = _OccdiseaseConsulitationAppService.GetAllOccupationHistory(OccGets);
                //给问卷id赋值（修改需要用）
                if (results != null)
                {
                    OccQuestionnaireId = results.Id;
                    // 签名处理
                    if (results.SignaTureImage.HasValue)
                    {
                        singImage = results.SignaTureImage.Value;
                        var resultes = _pictureController.GetUrl(results.SignaTureImage.Value);
                        pictureEdit1.Image = ImageHelper.GetUriImage(new Uri(resultes.RelativePath));
                    }
                    //职业史
                    if (results.OccCareerHistory == null)
                    {
                        results.OccCareerHistory = new List<OccupationHistoryDto>();

                    }
                    results.OccCareerHistory = results.OccCareerHistory.OrderBy(p=>p.StarTime).ToList();
                    results.OccCareerHistory.Add(AddHistoryEmpRow());

                    gridzys.DataSource = results.OccCareerHistory;
                    //既往史
                    if (results.OccPastHistory == null)
                    {
                        results.OccPastHistory = new List<OccQuesPastHistoryDto>();

                    }
                    results.OccPastHistory.Add(AddPastHistoryEmpRow());
                    gridjws.DataSource = results.OccPastHistory;
                    //家族史
                    if (results.OccFamilyHistory == null)
                    {
                        results.OccFamilyHistory = new List<OccQuesFamilyHistoryDto>();
                    }
                    results.OccFamilyHistory.Add(AddFamilyHistoryRow());
                    gridjzs.DataSource = results.OccFamilyHistory;

                    //婚姻史
                    if (results.OccQuesMerriyHistory == null)
                    {
                        results.OccQuesMerriyHistory = new List<OccQuesMerriyHistoryDto>();
                    }
                    results.OccQuesMerriyHistory.Add(AddQuesMerriyHistoryRow());
                    gridMerr.DataSource = results.OccQuesMerriyHistory;

                    //放射职业史
                    if (results.RadioactiveCareerHistory == null)
                    {
                        results.RadioactiveCareerHistory = new List<OccQuesRadioactiveCareerHistoryDto>();
                    }
                    results.RadioactiveCareerHistory.Add(AddRadioactiveCareerHistoryRow());
                    gridRadioactive.DataSource = results.RadioactiveCareerHistory;
                    // workType.EditValue= results.OccFamilyHistory;
                    //occpast.WorkType = workType.EditValue.ToString();
                    textGeneticHistory.Text = results.GeneticHistory;
                    textMedicationHistory.Text = results.MedicationHistory;
                    textAllergicHistory.Text = results.AllergicHistory;
                    textDrugTaboo.Text = results.DrugTaboo;
                    textEditHisIll.Text = results.PastHistory;
                    //月经及生育史
                    txtStarAge.Text = results.StarAge.ToString();
                    txtCle.Text = results.Cycle.ToString();
                    spinDay.Text = results.period.ToString();
                    txtendage.Text = results.EndAge.ToString();
                    txtChildCout.Text = results.ChildrenNum.ToString();

                    #region 新增0929
                    spPregnancyCount.Text = results.PregnancyCount?.ToString();
                    spLiveBirth.Text = results.LiveBirth?.ToString();
                    spTeratogenesis.Text = results.Teratogenesis?.ToString();
                    spMultipleBirths.Text = results.MultipleBirths?.ToString();
                    spEctopicPregnancy.Text = results.EctopicPregnancy?.ToString();
                    textInfertility.Text = results.Infertility?.ToString();
                    spBoyChildrenNum.Text = results.BoyChildrenNum?.ToString();
                    dateBoyBrith.EditValue = results.BoyBrith?.ToString();
                    spgrilChildrenNum.Text = results.grilChildrenNum?.ToString();
                    dategrilBrith.EditValue = results.grilBrith?.ToString();
                    textChildHealthy.EditValue = results.ChildHealthy?.ToString();
                    textLifeHistory.EditValue = results.LifeHistory?.ToString();
                    spQuitYears.EditValue = results.QuitYears?.ToString();
                    #endregion

                    txtLCCount.Text = results.AbortionCount.ToString();


                    spinEditPrematureDeliveryCount.Text = results.PrematureDeliveryCount?.ToString();
                    spinEditStillbirthCount.Text = results.StillbirthCount?.ToString();
                    spinEditAbnormityCount.Text = results.AbnormityCount?.ToString();

                    //吸烟饮酒史
                    smoke.EditValue = results.IsSmoke;
                    txtSmokeCout.Text = results.SmokeCount.ToString();
                    txtsmokeYears.Text = results.SmokeYears.ToString();
                    Drink.EditValue = results.IsDrink;
                    txtDrinkCount.Text = results.DrinkCount.ToString();
                    txtDrinkYesrs.Text = results.DrinkYears.ToString();
                    //近期症状                 
                    if (results.OccQuesSymptom == null)
                    {
                        results.OccQuesSymptom = new List<OccQuesSymptomDto>();
                    }
                    results.OccQuesSymptom.Add(AddSymptomRow());
                    gridZZ.DataSource = results.OccQuesSymptom;
                    textEdit3.Text = results.AskAdvice;
                }
                else
                {
                    OccQuestionnaireId = Guid.Empty;
                    //职业史
                    gridzys.DataSource = null;
                    var OccCareerHistory = new List<OccupationHistoryDto>();
                    if (!string.IsNullOrEmpty(CurrCusReg.ClientName) && 
                        ((!string.IsNullOrEmpty(result.PostState) && !result.PostState.Contains("上岗")) 
                        || string.IsNullOrEmpty(result.PostState)))
                    {
                        var history = AddOldHistoryRow(CurrCusReg);
                        OccCareerHistory.Add(history);
                    }
                    OccCareerHistory.Add(AddHistoryEmpRow());
                    gridzys.DataSource = OccCareerHistory;
                    //既往史
                    gridjws.DataSource = null;
                    var OccPastHistory = new List<OccQuesPastHistoryDto>();
                    OccPastHistory.Add(AddPastHistoryEmpRow());
                    gridjws.DataSource = OccPastHistory;
                    textEditHisIll.Text = "无特殊";
                    //家族史
                    gridjzs.DataSource = null;
                    var OccFamilyHistory = new List<OccQuesFamilyHistoryDto>();
                    OccFamilyHistory.Add(AddFamilyHistoryRow());
                    gridjzs.DataSource = OccFamilyHistory;

                    //婚姻史
                    gridMerr.DataSource = null;
                    var QuesMerriyHistory = new List<OccQuesMerriyHistoryDto>();
                    QuesMerriyHistory.Add(AddQuesMerriyHistoryRow());
                    gridMerr.DataSource = QuesMerriyHistory;

                    //放射职业史
                    gridRadioactive.DataSource = null;
                    var QuesRadioactiveCareerHistory = new List<OccQuesRadioactiveCareerHistoryDto>();
                    if (!string.IsNullOrEmpty(CurrCusReg.ClientName) && 
                        ( (!string.IsNullOrEmpty(result.PostState) && !result.PostState.Contains("上岗")) 
                        || string.IsNullOrEmpty(result.PostState) ))
                    {
                        var history = AddOldRadioactiveRow(CurrCusReg);
                        QuesRadioactiveCareerHistory.Add(history);
                    }
                    QuesRadioactiveCareerHistory.Add(AddRadioactiveCareerHistoryRow());
                    
                    gridRadioactive.DataSource = QuesRadioactiveCareerHistory;


                    // workType.EditValue= results.OccFamilyHistory;
                    //occpast.WorkType = workType.EditValue.ToString();
                    //月经及生育史
                    txtStarAge.Text = "";
                    txtCle.Text = "";
                    txtendage.Text = "";
                    txtChildCout.Text = "0";

                    txtLCCount.Text = "";
                    spinEditPrematureDeliveryCount.Text = "";
                    spinEditStillbirthCount.Text = "";
                    spinEditAbnormityCount.Text = "";



                    //吸烟饮酒史

                    txtSmokeCout.Text = "";
                    txtsmokeYears.Text = "";

                    txtDrinkCount.Text = "";
                    txtDrinkYesrs.Text = "";
                    //近期症状
                    gridZZ.DataSource = null;
                    //近期症状 
                    var OccQuesSymptom = new List<OccQuesSymptomDto>();
                    OccQuesSymptom.Add(AddSymptomRow());
                    gridZZ.DataSource = OccQuesSymptom;
                    textEdit3.Text = "";
                    txtStarAge.Text = "13";
                    txtCle.Value = 28;

                    smoke.EditValue = "从不吸烟";
                    Drink.EditValue = "从不饮酒";
                    //txtISLC.EditValue = "否";
                    //txtIsZC.EditValue = "否";
                    //txtIsDie.EditValue = "否";

                    textEditHisIll.Text = "无特殊";
                    textGeneticHistory.Text = "";
                    textAllergicHistory.Text = "";
                    textMedicationHistory.Text = "";
                    textDrugTaboo.Text = "";
                    #region 新增0929
                    spPregnancyCount.Text = "";
                    spLiveBirth.Text = "";
                    spTeratogenesis.Text = "";
                    spMultipleBirths.Text = "";
                    spEctopicPregnancy.Text = "";
                    textInfertility.Text = "";
                    spBoyChildrenNum.Text = "";
                    dateBoyBrith.EditValue = "";
                    spgrilChildrenNum.Text = "";
                    dategrilBrith.EditValue = "";
                    textChildHealthy.EditValue = "";
                    textLifeHistory.EditValue = "";
                    spQuitYears.EditValue = "";
                    #endregion
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
        }
        /// <summary>
        /// 添加单位职业史
        /// </summary>
        /// <param name="CusReg"></param>
        /// <returns></returns>
        private OccupationHistoryDto AddOldHistoryRow(OccdieaseBasicInformationDto CusReg)
        {
            OccupationHistoryDto occupationHistoryDto = new OccupationHistoryDto();
            if (CusReg != null)
            {

                if (decimal.TryParse(CusReg.InjuryAge, out decimal juryAge))
                    occupationHistoryDto.WorkYears = juryAge;
                occupationHistoryDto.UnitAge = CusReg.InjuryAgeUnit;
                if (int.TryParse(CusReg.InjuryAge, out int age))
                {
                    if (CusReg.InjuryAgeUnit.Contains("月"))
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddMonths(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;

                    }
                    else if (CusReg.InjuryAgeUnit.Contains("日"))
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddDays(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;
                    }
                    else
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddYears(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;
                    }

                }
                if (occupationHistoryDto.StarTime.HasValue || occupationHistoryDto.EndTime.HasValue)
                {
                    var njs = term(occupationHistoryDto.StarTime.Value, occupationHistoryDto.EndTime.Value);
                    var nj = njs.Split('|');
                    occupationHistoryDto.StrWorkYears = nj[1];
                }
                if (!occupationHistoryDto.EndTime.HasValue)
                {
                    if (CusReg.LoginDate.HasValue)
                    {
                        occupationHistoryDto.EndTime = CusReg.LoginDate;
                    }
                    else
                    {
                        occupationHistoryDto.EndTime = _commonAppService.GetDateTimeNow().Now;
                    }
                }
                if (CusReg.StarTime.HasValue)
                {
                    occupationHistoryDto.StarTime = CusReg.StarTime;
                    if (CusReg.LoginDate.HasValue)
                    {
                        occupationHistoryDto.EndTime = CusReg.LoginDate;
                    }
                    else
                    {
                        occupationHistoryDto.EndTime = _commonAppService.GetDateTimeNow().Now;
                    }
                    #region 计算工龄
                    var gl = setgs(occupationHistoryDto.StarTime.Value, occupationHistoryDto.EndTime.Value);
                    if (gl.Count == 2)
                    {
                        occupationHistoryDto.StrWorkYears = gl[1];
                        occupationHistoryDto.WorkYears = Convert.ToDecimal(gl[0]);
                        occupationHistoryDto.UnitAge = "年";
                    }
                    #endregion
                }
                occupationHistoryDto.WorkClient = CusReg.ClientName;
                occupationHistoryDto.WorkType = CusReg.TypeWork;
                occupationHistoryDto.WorkName = CusReg.WorkName;
                var dataresult = new List<OccdieaseHurtDto>();
                var dieaseProtect = new List<OccdieaseProtectDto>();

                var risk = CusReg.OccHazardFactors.Select(p => p.Id).ToList();
                string riskIds = string.Join(",", risk);
                occupationHistoryDto.HazardFactorIds = riskIds;


            }
            return occupationHistoryDto;
        }
        /// <summary>
        /// 添加放射职业史
        /// </summary>
        /// <param name="CusReg"></param>
        /// <returns></returns>
        private OccQuesRadioactiveCareerHistoryDto AddOldRadioactiveRow(OccdieaseBasicInformationDto CusReg)
        {
            OccQuesRadioactiveCareerHistoryDto occupationHistoryDto = new OccQuesRadioactiveCareerHistoryDto();
            if (CusReg != null)
            {

                 
                if (int.TryParse(CusReg.InjuryAge, out int age))
                {
                    if (CusReg.InjuryAgeUnit.Contains("月"))
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddMonths(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;

                    }
                    else if (CusReg.InjuryAgeUnit.Contains("日"))
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddDays(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;
                    }
                    else
                    {
                        occupationHistoryDto.StarTime = System.DateTime.Now.AddYears(-age);
                        occupationHistoryDto.EndTime = System.DateTime.Now;
                    }

                }               
                if (!occupationHistoryDto.EndTime.HasValue)
                {
                    if (CusReg.LoginDate.HasValue)
                    {
                        occupationHistoryDto.EndTime = CusReg.LoginDate;
                    }
                    else
                    {
                        occupationHistoryDto.EndTime = _commonAppService.GetDateTimeNow().Now;
                    }
                }
                if (CusReg.StarTime.HasValue)
                {
                    occupationHistoryDto.StarTime = CusReg.StarTime;
                    if (CusReg.LoginDate.HasValue)
                    {
                        occupationHistoryDto.EndTime = CusReg.LoginDate;
                    }
                    else
                    {
                        occupationHistoryDto.EndTime = _commonAppService.GetDateTimeNow().Now;
                    }                 
                }
                occupationHistoryDto.WorkClient = CusReg.ClientName;
                occupationHistoryDto.WorkType = CusReg.TypeWork;
                occupationHistoryDto.WorkName = CusReg.WorkName;
                var dataresult = new List<OccdieaseHurtDto>();
                var dieaseProtect = new List<OccdieaseProtectDto>();

              


            }
            return occupationHistoryDto;
        }

        #region 添加新行
        //职业史
        private OccupationHistoryDto AddHistoryEmpRow()
        {
            OccupationHistoryDto occupationHistoryDto = new OccupationHistoryDto();
            occupationHistoryDto.CustomerRegBMId = _id;
            return occupationHistoryDto;
        }
        //既往史
        private OccQuesPastHistoryDto AddPastHistoryEmpRow()
        {
            OccQuesPastHistoryDto occupationHistoryDto = new OccQuesPastHistoryDto();
            occupationHistoryDto.CustomerRegBMId = _id;
            occupationHistoryDto.Iscured = 2;
            return occupationHistoryDto;
        }
        //家族史
        private OccQuesFamilyHistoryDto AddFamilyHistoryRow()
        {
            OccQuesFamilyHistoryDto occupationHistoryDto = new OccQuesFamilyHistoryDto();
            occupationHistoryDto.CustomerRegBMId = _id;
            return occupationHistoryDto;
        }
        //婚姻史
        private OccQuesMerriyHistoryDto AddQuesMerriyHistoryRow()
        {

            OccQuesMerriyHistoryDto MerriyHistoryDto = new OccQuesMerriyHistoryDto();

            MerriyHistoryDto.CustomerRegBMId = _id;
            MerriyHistoryDto.OccHealth = "健康";
            return MerriyHistoryDto;
        }
        //放射职业史
        private OccQuesRadioactiveCareerHistoryDto AddRadioactiveCareerHistoryRow()
        {
            OccQuesRadioactiveCareerHistoryDto RadioactiveCareerHistory = new OccQuesRadioactiveCareerHistoryDto();

            RadioactiveCareerHistory.CustomerRegBMId = _id;
            return RadioactiveCareerHistory;
        }
        //症状
        private OccQuesSymptomDto AddSymptomRow()
        {
            OccQuesSymptomDto occupationHistoryDto = new OccQuesSymptomDto();
            occupationHistoryDto.CustomerRegBMId = _id;
            return occupationHistoryDto;
        }
        #endregion
        /// <summary>
        /// 职业史删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //删除
            //MessageBox.Show("删除");
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewzys.GetFocusedRow() as OccupationHistoryDto;
                if (currentItem == null)
                    return;

                var dataresult = gridzys.DataSource as List<OccupationHistoryDto>;
                dataresult.Remove(currentItem);
                gridzys.DataSource = dataresult;
                gridzys.RefreshDataSource();
                gridzys.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                Cusid = _id;
                var griddatda = gridzys.DataSource as List<OccupationHistoryDto>;
                if (griddatda == null)
                {
                    griddatda = new List<OccupationHistoryDto>();

                }
                griddatda.Add(AddHistoryEmpRow());
                gridzys.DataSource = griddatda;
                gridzys.RefreshDataSource();
                gridzys.Refresh();
            }


        }

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        /// <summary>
        /// 既往史删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewjws.GetFocusedRow() as OccQuesPastHistoryDto;
                if (currentItem == null)
                    return;

                var dataresult = gridjws.DataSource as List<OccQuesPastHistoryDto>;
                dataresult.Remove(currentItem);
                gridjws.DataSource = dataresult;
                gridjws.RefreshDataSource();
                gridjws.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                var griddatda = gridjws.DataSource as List<OccQuesPastHistoryDto>;
                if (griddatda == null)
                {

                    griddatda = new List<OccQuesPastHistoryDto>();

                }
                griddatda.Add(AddPastHistoryEmpRow());
                gridjws.DataSource = griddatda;
                gridjws.RefreshDataSource();
                gridjws.Refresh();
            }
        }

        /// <summary>
        /// 家族史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var currentItem = gridViewjws.GetFocusedRow() as OccQuesFamilyHistoryDto;
            if (currentItem == null)
                return;

            var dataresult = gridZZ.DataSource as List<OccQuesFamilyHistoryDto>;
            dataresult.Remove(currentItem);
            gridZZ.DataSource = dataresult;
            gridZZ.RefreshDataSource();
            gridZZ.Refresh();
        }
        public static Guid? Cusid;
        /// <summary>
        /// 添加职业史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            Cusid = _id;
            var griddatda = gridzys.DataSource as List<OccupationHistoryDto>;
            if (griddatda == null)
            {
                griddatda = new List<OccupationHistoryDto>();

            }
            griddatda.Add(AddHistoryEmpRow());
            gridzys.DataSource = griddatda;
            gridzys.RefreshDataSource();
            gridzys.Refresh();
            return;

            //只有一条记录
            if (CurrCusReg != null && griddatda.Count == 0)
            {
                using (var frm = new OccupationHistoryEdit(CurrCusReg))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        griddatda.Add(frm.occpast);
                        gridzys.DataSource = griddatda;
                        gridzys.RefreshDataSource();
                        gridzys.Refresh();
                    }
                }
            }
            else
            {
                using (var frm = new OccupationHistoryEdit())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //simpleButton6.PerformClick();                    

                        griddatda.Add(frm.occpast);
                        gridzys.DataSource = griddatda;
                        gridzys.RefreshDataSource();
                        gridzys.Refresh();
                    }
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            var griddatda = gridjws.DataSource as List<OccQuesPastHistoryDto>;
            if (griddatda == null)
            {
                griddatda = new List<OccQuesPastHistoryDto>();
            }
            griddatda.Add(AddPastHistoryEmpRow());
            gridjws.DataSource = griddatda;
            gridjws.RefreshDataSource();
            gridjws.Refresh();
            return;
            //Cusid = _id;
            ////var frm2 = new OccQuesPastHistoryEdit((Guid)Cusid);
            //using (var frm = new OccQuesPastHistoryEdit())
            //{

            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        // simpleButton6.PerformClick();

            //        var griddatda = gridjws.DataSource as List<OccQuesPastHistoryDto>;
            //        if (griddatda == null)
            //        {
            //            griddatda = new List<OccQuesPastHistoryDto>();
            //        }
            //        griddatda.Add(frm.occpast);
            //        gridjws.DataSource = griddatda;
            //        gridjws.RefreshDataSource();
            //        gridjws.Refresh();
            //    }
            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var griddatda = gridjzs.DataSource as List<OccQuesFamilyHistoryDto>;
            if (griddatda == null)
            {
                griddatda = new List<OccQuesFamilyHistoryDto>();
            }
            griddatda.Add(AddFamilyHistoryRow());
            gridjws.DataSource = griddatda;
            gridjzs.RefreshDataSource();
            gridjzs.Refresh();


            // Cusid = _id;
            // if (string.IsNullOrEmpty( workType.Text ))
            // {
            //     dxErrorProvider.SetError(workType, string.Format(Variables.MandatoryTips, "疾病小类"));
            //     workType.Focus();
            //     return;                
            // }
            // if (string.IsNullOrEmpty(textEdit11.Text))
            // {
            //     dxErrorProvider.SetError(textEdit11, string.Format(Variables.MandatoryTips, "病人关系"));
            //     textEdit11.Focus();
            //     return;
            // }

            //var occpast = new OccQuesFamilyHistoryDto();

            // occpast.CustomerRegBMId = OccdieaseConsultation.Cusid;
            // occpast.IllName = workType.Text.ToString();
            // occpast.relatives = textEdit11.Text.Trim();
            // var griddatda = gridjzs.DataSource as List<OccQuesFamilyHistoryDto>;
            // if (griddatda == null)
            // {
            //     griddatda = new List<OccQuesFamilyHistoryDto>();
            // }
            // griddatda.Add(occpast);
            // gridjzs.DataSource = griddatda;
            // gridjzs.RefreshDataSource();
            // gridjzs.Refresh();
            // workType.Text = "";
            // textEdit11.Text = "";


        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var griddatda = gridZZ.DataSource as List<OccQuesSymptomDto>;
            if (griddatda == null)
            {
                griddatda = new List<OccQuesSymptomDto>();
            }
            griddatda.Add(AddSymptomRow());
            gridZZ.DataSource = griddatda;
            gridZZ.RefreshDataSource();
            gridZZ.Refresh();
            //  Cusid = _id;
            //  if (lookUpEdit4.EditValue == null)
            //  {
            //      dxErrorProvider.SetError(lookUpEdit4, string.Format(Variables.MandatoryTips, "症状小类"));
            //      lookUpEdit4.Focus();
            //      return;
            //  }
            //  if (string.IsNullOrEmpty(radioGroup1.Text))
            //  {
            //      dxErrorProvider.SetError(radioGroup1, string.Format(Variables.MandatoryTips, "症状程度"));
            //      radioGroup1.Focus();
            //      return;
            //  }
            //var  occpast = new OccQuesSymptomDto();

            //  occpast.CustomerRegBMId = Cusid;
            //  occpast.Name = lookUpEdit4.Text.Trim();

            //  occpast.Degree = radioGroup1.EditValue.ToString();

            //  var griddatda = gridZZ.DataSource as List<OccQuesSymptomDto>;
            //  if (griddatda == null)
            //  {
            //      griddatda = new List<OccQuesSymptomDto>();
            //  }
            //  griddatda.Add(occpast);
            //  gridZZ.DataSource = griddatda;
            //  gridZZ.RefreshDataSource();
            //  gridZZ.Refresh();
            //  lookUpEdit4.EditValue = null;
            //  radioGroup1.SelectedIndex = 0;
            //using (var frm = new OccQuestionSymptom())
            //{

            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {

            //        var griddatda = gridControl3.DataSource as List<OccQustionSymptomrucan>;
            //        if (griddatda == null)
            //        {
            //            griddatda = new List<OccQustionSymptomrucan>();
            //        }
            //        griddatda.Add(frm.occpast);
            //        gridControl3.DataSource = griddatda;
            //        gridControl3.RefreshDataSource();
            //        gridControl3.Refresh();
            //    }
            //}

        }

        private void repositoryItemButtonEdit4_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewzz.GetFocusedRow() as OccQuesSymptomDto;
                if (currentItem == null)
                    return;

                var dataresult = gridZZ.DataSource as List<OccQuesSymptomDto>;
                dataresult.Remove(currentItem);
                gridZZ.DataSource = dataresult;
                gridZZ.RefreshDataSource();
                gridZZ.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                var griddatda = gridZZ.DataSource as List<OccQuesSymptomDto>;
                if (griddatda == null)
                {
                    griddatda = new List<OccQuesSymptomDto>();
                }
                griddatda.Add(AddSymptomRow());
                gridZZ.DataSource = griddatda;
                gridZZ.RefreshDataSource();
                gridZZ.Refresh();
            }
        }

        private void workType1_EditValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// /保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            string TSMess = "";
            try
            {
                if (CurrCusReg == null)
                {
                    MessageBox.Show("请选择体检人");
                }
                else
                {  
                  
                   TSMess = CurrCusReg.CustomerBM +":"+ System.DateTime.Now.ToString() +"保存问诊" + "\r\n";
                    AddErLog(TSMess);
                
                }
                PictureDto result = null;
                if (pictureEdit1.EditValue != null)
                {

                    try
                    {
                        var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString() + ".jpg";
                        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                        }
                        pictureEdit1.Image.Save(url);
                        PicAddress = url;
                        result = _pictureController?.Uploading(PicAddress, "QuesCusSign");
                        //增加本地日志
                        TSMess = CurrCusReg.CustomerBM + ":" + System.DateTime.Now.ToString() + "保存问诊图片" + "\r\n"
                            + result.Id;
                        AddErLog(TSMess);
                    }
                    catch (Exception ex)
                    {
                        //增加本地日志
                        TSMess = CurrCusReg.CustomerBM + ":" + System.DateTime.Now.ToString() + "保存问诊图片报错" + "\r\n"
                         + ex.Message;                        
                        AddErLog(TSMess);
                        throw;
                    }
                }
                if (_id == null || _id == Guid.Empty)
                {
                    MessageBox.Show("请选择体检人！");
                    return;
                }
                //问卷id
                var OccQuestId = OccQuestionnaireId;
                //职业史内容
                var ObjectZY = gridzys.DataSource as List<OccupationHistoryDto>;
                if (ObjectZY != null && ObjectZY.Count > 0)
                {
                    //查询是否存在没有工种的情况
                    //var cuss = ObjectZY.Where(p =>( p.WorkClient != "" && p.WorkClient != null) && (p.WorkType == "" || p.WorkType == null));
                    var Noworkcount = ObjectZY.Where(p => (p.WorkClient != "" && p.WorkClient != null) && (p.WorkType == "" || p.WorkType == null)).Count();
                    if (Noworkcount > 0)
                    {
                        MessageBox.Show("职业史工种不能为空，请检查！");
                        return;
                    }
                    #region 计算工龄
                    foreach (var hisdet in ObjectZY)
                    {
                        if (string.IsNullOrEmpty(hisdet.StrWorkYears) && hisdet.StarTime.HasValue
                            && hisdet.EndTime.HasValue)
                        {
                            #region 计算工龄
                            var gl = setgs(hisdet.StarTime.Value, hisdet.EndTime.Value);
                            if (gl.Count == 2)
                            {
                                hisdet.StrWorkYears = gl[1];
                                hisdet.WorkYears = Convert.ToDecimal(gl[0]);
                                hisdet.UnitAge = "年";
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                //既往史
                var ObjectJw = gridjws.DataSource as List<OccQuesPastHistoryDto>;
                //家族史
                var ObjectJz = gridjzs.DataSource as List<OccQuesFamilyHistoryDto>;
                //近期症状
                var ObjectZZ = gridZZ.DataSource as List<OccQuesSymptomDto>;
                //婚姻史

                var ObjectMerr = gridMerr.DataSource as List<OccQuesMerriyHistoryDto>;
                //放射职业史

                var ObjectRadioactiveCareer = gridRadioactive.DataSource as List<OccQuesRadioactiveCareerHistoryDto>;
                Cusid = _id;
                string iSCL = "否";
                if (Convert.ToInt32(txtLCCount.Text) > 0)
                {
                    iSCL = "是";
                }
                string PrematureDelivery = "否";
                if (Convert.ToInt32(spinEditPrematureDeliveryCount.Text) > 0)
                {
                    PrematureDelivery = "是";
                }
                string Stillbirth = "否";
                if (Convert.ToInt32(spinEditStillbirthCount.Text) > 0)
                {
                    Stillbirth = "是";
                }
              
                dynamic input = new
                {
                    OccQuestId,
                    Cusid,
                    ObjectZY,
                    ObjectJw,
                    ObjectJz,
                    ObjectZZ,
                    ObjectMerr,
                    ObjectRadioactiveCareer,
                    stringData = new
                    {
                        chuchao = txtStarAge.Text,
                        yuejing = txtCle.Text,
                        jq = Convert.ToInt32(spinDay.Text),
                        tingjing = txtendage.Text,
                        zinvSum = Convert.ToInt32(txtChildCout.Text),
                        isliuchan = iSCL,
                        liuchan = Convert.ToInt32(txtLCCount.Text),
                        PrematureDeliveryCount = Convert.ToInt32(spinEditPrematureDeliveryCount.Text),
                        StillbirthCount = Convert.ToInt32(spinEditStillbirthCount.Text),
                        AbnormityCount = Convert.ToInt32(spinEditAbnormityCount.Text),
                        iszc = PrematureDelivery,
                        issichan = Stillbirth,
                        isSmok = smoke.Text,
                        SignaTureImage = result?.Id,
                        xiyancishu = Convert.ToInt32(txtSmokeCout.Text),
                        xiyannianxian = Convert.ToInt32(txtsmokeYears.Text),
                        isyinjiu = Drink.Text,
                        yinjiucishu = Convert.ToInt32(txtDrinkCount.Text),
                        yinjiunianxian = Convert.ToDecimal(txtDrinkYesrs.Text),
                        GeneticHistory = textGeneticHistory.Text,
                        MedicationHistory = textMedicationHistory.Text,
                        AllergicHistory = textAllergicHistory.Text,
                        DrugTaboo = textDrugTaboo.Text,
                        PastHistory = textEditHisIll.Text,
                        PregnancyCount = spPregnancyCount.Text,
                        LiveBirth = spLiveBirth.Text,
                        Teratogenesis = spTeratogenesis.Text,
                        MultipleBirths = spMultipleBirths.Text,
                        EctopicPregnancy = spEctopicPregnancy.Text,
                        Infertility = textInfertility.Text,
                        BoyChildrenNum = spBoyChildrenNum.Text,
                        BoyBrith = dateBoyBrith.EditValue,
                        grilChildrenNum = spgrilChildrenNum.Text,
                        grilBrith = dategrilBrith.EditValue,
                        ChildHealthy = textChildHealthy.Text,
                        LifeHistory = textLifeHistory.Text,
                        QuitYears = spQuitYears.Text
                    }
                };
                //增加本地日志
                TSMess = CurrCusReg.CustomerBM + ":" + System.DateTime.Now.ToString() + "保存问诊入参" + "\r\n"
                    + JsonConvert.SerializeObject(input) + "\r\n";
                AddErLog(TSMess);
                if (ObjectZY != null &&
                    ( ObjectZY.Any(o =>o.WorkClient!=null && o.WorkClient!="" &&
                    !o.StarTime.HasValue) ||
                    ObjectZY.Any(o => o.WorkClient != null && o.WorkClient != "" && !o.EndTime.HasValue)))
                {
                    MessageBox.Show("职业开始时间结束时间不能为空！");
                    return;
                }
                var results = _OccdiseaseConsulitationAppService.AddData(input);

                if (results != null && results.Id != Guid.Empty)
                {
                    MessageBox.Show("保存成功！");
                    //保存成功
                    OccQuestionnaireId = results.Id;
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    if (CurrCusReg != null)
                    {
                        createOpLogDto.LogBM = CurrCusReg.CustomerBM;
                        createOpLogDto.LogName = CurrCusReg.Name;
                    }
                    createOpLogDto.LogText = "保存问诊成功";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.WZ;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
                else
                {
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    if (CurrCusReg != null)
                    {
                        createOpLogDto.LogBM = CurrCusReg.CustomerBM;
                        createOpLogDto.LogName = CurrCusReg.Name;
                    }
                    createOpLogDto.LogText = "保存问诊异常";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.WZ;
                    _commonAppService.SaveOpLog(createOpLogDto);
                }
            }
            catch (UserFriendlyException ex)
            {

                TSMess = CurrCusReg.CustomerBM + ":" + System.DateTime.Now.ToString() + "保存问诊报错" + "\r\n"
                 + ex.Message + "\r\n";

                AddErLog(TSMess);
                ShowMessageBox(ex);
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                if (CurrCusReg != null)
                {
                    createOpLogDto.LogBM = CurrCusReg.CustomerBM;
                    createOpLogDto.LogName = CurrCusReg.Name;
                }
                createOpLogDto.LogText = "保存问诊异常";
                if (ex.Message.Length > 300)
                { createOpLogDto.LogDetail = ex.Message.Substring(1,300); }
                else
                {
                    createOpLogDto.LogDetail = ex.Message;
                }
                createOpLogDto.LogType = (int)LogsTypes.WZ;
                _commonAppService.SaveOpLog(createOpLogDto);

               
                return;
            }
             
        }
        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="error"></param>
        public static void AddErLog(string error)
        {
            string filename = DateTime.Now.ToString("yyyyMMdd") + "问诊.log";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Log\\";

            filename = filepath + filename;

            TextWriter writer;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (File.Exists(filename))
            {
                writer = File.AppendText(filename);
            }
            else
            {
                FileInfo filenameInfo = new FileInfo(filename);
                writer = filenameInfo.CreateText();
            }
            writer.WriteLine("time:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteLine("问诊:" + error);
            writer.WriteLine("");
            writer.Close();

        }
        private void workType_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            //if (treeView1.FocusedNode.GetValue(0).ToString() == "全部")
            //{
            //    groupzys.Visibility =DevExpress.XtraLayout.Utils.LayoutVisibility.Always ;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    if (sex.Text.Contains("女"))
            //    {
            //        groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    }
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "职业史")
            //{
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "既往史")
            //{
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "家族史")
            //{
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "月经及生育史")
            //{
            //    if (sex.Text.Contains("男"))
            //    {
            //        MessageBox.Show("男性无需录入月经及生育史！");
            //        return;
            //    }
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "吸烟饮酒史")
            //{
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            //if (treeView1.FocusedNode.GetValue(0).ToString() == "近期症状")
            //{
            //    groupzys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjws.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupjzs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupyjs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupxys.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    groupzz.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}

        }

        private void treeView1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {

        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == gridColumn15.FieldName)
            //{
            //    if (!string.IsNullOrEmpty(e.Value?.ToString()))
            //    {
            //        if (e.Value?.ToString() == "0")
            //        {
            //            e.DisplayText = "是";
            //        }
            //        else if (e.Value?.ToString() == "1")
            //        {
            //            e.DisplayText = "否";
            //        }
            //        else
            //        {
            //            e.DisplayText = "无";
            //        }

            //    }

            //}
        }

        private void txtrisk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtrisk.Text))
            {
                simpleButton6.PerformClick();
            }
        }
        //保存手写签名
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_id != null || _id != Guid.Empty)
            {
                try
                {
                    frmsxqm frmsxqm = new frmsxqm();
                    if (frmsxqm.ShowDialog() == DialogResult.OK)
                    {
                        if (frmsxqm.imagesxqm != null)
                        {
                            picqm.Image = frmsxqm.imagesxqm;
                            Image image = picqm.Image;
                            PicAddress = Convert.ToString(frmsxqm.imagesxqms);
                            pictureEdit1.Image = Image.FromFile(PicAddress);
                            //保存手写签名待完成
                            //string strfile = CommonTools.GetSysSeting("tjtpsqqm");
                            //strfile += txtArch.Text + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".jpg";
                            //image.Save(strfile);
                            //string strsql = "update Customer set imagesxqm='" + strfile + "' where ArchivesNum='" + txtArch.Text + "'";
                            //set.UpdateSQL(strsql);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("体检号不能为空");
            }
        }

        private void repositoryItemButtonEdit5_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewjzs.GetFocusedRow() as OccQuesFamilyHistoryDto;
                if (currentItem == null)
                    return;

                var dataresult = gridjzs.DataSource as List<OccQuesFamilyHistoryDto>;
                dataresult.Remove(currentItem);
                gridjzs.DataSource = dataresult;
                gridjzs.RefreshDataSource();
                gridjzs.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                var griddatda = gridjzs.DataSource as List<OccQuesFamilyHistoryDto>;
                if (griddatda == null)
                {
                    griddatda = new List<OccQuesFamilyHistoryDto>();
                }
                griddatda.Add(AddFamilyHistoryRow());
                gridjzs.DataSource = griddatda;
                gridjzs.RefreshDataSource();
                gridjzs.Refresh();
            }

        }

        private void btnAddvancedSearch_Click(object sender, EventArgs e)
        {
            var cusListForm = new OccCusList();
            if (cusListForm.ShowDialog() == DialogResult.OK)
            {
                txtrisk.Text = cusListForm.curCustomerBM;
                simpleButton6.PerformClick();
                //var data = QueryCustomerRegData(new SearchCustomerDto { CustomerBM = cusListForm.curCustomerBM });
                //ClearControlData();
                //LoadCustomerData(data);
                // SetBtnEnabled();
            }
        }

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var currData = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
            if (currData.StarTime.HasValue && currData.EndTime.HasValue)
            {

                setgl();
            }
        }



        private void repositoryItemDateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            var currData = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
            if (currData.StarTime.HasValue && currData.EndTime.HasValue)
            {

                setgl();
            }

        }
        private void setgl()
        {
            //var currData = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
            var obstar = gridViewzys.GetFocusedRowCellValue(conStarTime);
            var obend = gridViewzys.GetFocusedRowCellValue(conEndTime);

            if (obstar != null && obend != null)
            {
                var star = DateTime.Parse(obstar.ToString());
                var end = DateTime.Parse(obend.ToString());
                var njs = term(star, end);
                var nj = njs.Split('|');
                var denj = decimal.Parse(nj[0]);
                gridViewzys.SetFocusedRowCellValue(conWorkYears, denj.ToString());
                gridViewzys.SetFocusedRowCellValue(conUnitAge, "年");
                gridViewzys.SetFocusedRowCellValue(conStrWorkYears, nj[1]);
                gridViewzys.RefreshData();
                gridzys.RefreshDataSource();
            }
        }
        private List<string> setgs(DateTime star, DateTime end)
        {
            //var currData = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
            //var obstar = gridViewzys.GetFocusedRowCellValue(conStarTime);
            //var obend = gridViewzys.GetFocusedRowCellValue(conEndTime);

            //if (obstar != null && obend != null)
            //{
            //var star = DateTime.Parse(obstar.ToString());
            //var end = DateTime.Parse(obend.ToString());
            List<string> dtlist = new List<string>();
            var njs = term(star, end);
            var nj = njs.Split('|');
            var denj = decimal.Parse(nj[0]);
            gridViewzys.SetFocusedRowCellValue(conWorkYears, denj.ToString());
            gridViewzys.SetFocusedRowCellValue(conUnitAge, "年");
            gridViewzys.SetFocusedRowCellValue(conStrWorkYears, nj[1]);
            gridViewzys.RefreshData();
            gridzys.RefreshDataSource();
            dtlist.Add(denj.ToString());
            dtlist.Add(nj[1]);
            return dtlist;
            //}
        }
        private string term(DateTime b, DateTime e)
        {
            if (b < e)
            {
                var t = new
                {
                    bm = b.Month,
                    em = e.Month,
                    bd = b.Day,
                    ed = e.Day
                };
                int diffMonth = (e.Year - b.Year) * 12 + (t.em - t.bm),//相差月
                    diffYear = diffMonth / 12;//相差年

                int[] d = new int[3] { 0, 0, 0 };
                if (diffYear > 0)
                {
                    if (t.em == t.bm && t.ed < t.bd)
                    {
                        d[0] = diffYear - 1;
                    }
                    else d[0] = diffYear;
                }

                if (t.ed >= t.bd)
                {
                    d[1] = diffMonth % 12;
                    d[2] = t.ed - t.bd;
                }
                else//结束日 小于 开始日
                {
                    int dm = diffMonth - 1;
                    d[1] = dm % 12;
                    TimeSpan ts = e - b.AddMonths(dm);
                    d[2] = ts.Days;
                }
                StringBuilder sb = new StringBuilder();

                if (d.Sum() > 0)
                {
                    sb.Append(string.Format("{0}.", d[0]));//年
                    sb.Append(string.Format("{0}", d[1]));//月
                                                          //sb.Append(string.Format("{0}", d[2]));//日
                    sb.Append(string.Format("|{0}年", d[0]));//年
                    sb.Append(string.Format("{0}月", d[1]));//月
                    sb.Append(string.Format("{0}日", d[2]));//日
                }
                else
                {
                    sb.Append("0.");//年
                    sb.Append("0");//月
                                   //sb.Append("0");//日
                    sb.Append("|0年");//年
                    sb.Append("0月");//月
                    sb.Append("0日");//日
                }
                return sb.ToString();
            }
            else throw new Exception("开始日期必须小于结束日期");
        }

        private void gridViewzys_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            setgl();
        }

        private void gridViewzys_DataSourceChanged(object sender, EventArgs e)
        {
            // setgl();
        }

        private void gridzys_DataSourceChanged(object sender, EventArgs e)
        {
            //setgl();
        }

        private void gridViewzys_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //setgl();
        }

        private void gridzys_Leave(object sender, EventArgs e)
        {

        }

        private void gridViewzys_MouseLeave(object sender, EventArgs e)
        {
            setgl();
        }

        private void repositoryItemButtonEdit6_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewRad.GetFocusedRow() as OccQuesRadioactiveCareerHistoryDto;
                if (currentItem == null)
                    return;

                var dataresult = gridViewRad.DataSource as List<OccQuesRadioactiveCareerHistoryDto>;
                dataresult.Remove(currentItem);
                gridRadioactive.DataSource = dataresult;
                gridRadioactive.RefreshDataSource();
                gridRadioactive.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                var griddatda = gridViewRad.DataSource as List<OccQuesRadioactiveCareerHistoryDto>;
                if (griddatda == null)
                {

                    griddatda = new List<OccQuesRadioactiveCareerHistoryDto>();

                }
                griddatda.Add(AddRadioactiveCareerHistoryRow());
                gridRadioactive.DataSource = griddatda;
                gridRadioactive.RefreshDataSource();
                gridRadioactive.Refresh();
            }
        }

        private void repositoryItemButtonEdit7_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                var currentItem = gridViewMerr.GetFocusedRow() as OccQuesMerriyHistoryDto;
                if (currentItem == null)
                    return;

                var dataresult = gridViewMerr.DataSource as List<OccQuesMerriyHistoryDto>;
                dataresult.Remove(currentItem);
                gridMerr.DataSource = dataresult;
                gridMerr.RefreshDataSource();
                gridMerr.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Plus)
            {
                var griddatda = gridViewMerr.DataSource as List<OccQuesMerriyHistoryDto>;
                if (griddatda == null)
                {

                    griddatda = new List<OccQuesMerriyHistoryDto>();

                }
                griddatda.Add(AddQuesMerriyHistoryRow());
                gridMerr.DataSource = griddatda;
                gridMerr.RefreshDataSource();
                gridMerr.Refresh();
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

            try
            {

                if (CurrCusReg != null && CurrCusReg.CusPhotoBmId.HasValue &&
                CurrCusReg.CusPhotoBmId != Guid.Empty
                )
                {
                    var urlPath = new PictureController().GetUrl(CurrCusReg.CusPhotoBmId.Value);
                    var url = AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\" + Guid.NewGuid().ToString();
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"CustomReg\");
                    }
                    var file = DownFile2(urlPath.Thumbnail, url);
                    if (file != "" && File.Exists(file))
                    {
                        string args = file;
                        var path = AppDomain.CurrentDomain.BaseDirectory + "\\人证核验";
                        Process KHMsg = new Process();
                        KHMsg.StartInfo.FileName = path + "\\WindowsFormsApp1.exe";
                        KHMsg.StartInfo.Arguments = args;
                        KHMsg.Start();

                        while (!KHMsg.HasExited) { } //如果exe还没关闭，则等待
                        if (KHMsg.ExitCode != 1)
                        {
                            MessageBox.Show("认证核验失败！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("下载照片失败！");
                    }
                }
                else
                {
                    MessageBox.Show("无照片信息！");
                }
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
                return;
            }

        }
        //url  要下载的图片地址，全路径
        //file 要保存的地址 文件夹/文件名
        //ext  图片后缀
        //fileType 图片后缀
        public static string DownFile2(string url, string file)
        {
            WebClient wc = new WebClient();
            string reslut = "";
            try
            {

                Stream stream = wc.OpenRead(url);
                //如果图片没有后缀的，根据下面方法获取后缀名
                var fileType = wc.ResponseHeaders["Content-Type"].Split('/')[1];

                stream.Close();  //以及释放内存  
                wc.Dispose();
                //  只下载这几种类型png  gif  jpeg  bmp  jpg
                if (fileType != "png" && fileType != "gif" && fileType != "jpeg" && fileType != "bmp" && fileType != "jpg")
                {
                    reslut = "";
                    return reslut;
                }

                file += "." + fileType;
                //下载方法
                wc.DownloadFile(url, file);
                reslut = file;
            }
            catch (Exception e)
            {

                reslut = "";
            }
            //下载
            //读取下载下来的源文件HTML格式的字符
            //string mainData = File.ReadAllText(file, Encoding.UTF8);
            //return mainData;
            return reslut;
        }

        private void dockPanel1_Click(object sender, EventArgs e)
        {

        }

        private void butsearch_Click(object sender, EventArgs e)
        {
            LoadData();
            // CX();
        }

        //public void CX()
        //{
        //    dxErrorProvider.ClearErrors();
        //    var closeWait = false;
        //    if (!splashScreenManager.IsSplashFormVisible)
        //    {
        //        splashScreenManager.ShowWaitForm();
        //        closeWait = true;
        //    }
        //    splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);
        //    gridControl1.DataSource = null;


        //    CustomerRegPhysicalDto dto = new CustomerRegPhysicalDto();
        //    CustomerPhysicalDto customer = new CustomerPhysicalDto();
        //    try
        //    {

        //        if (!string.IsNullOrWhiteSpace(teTJH.Text.Trim()))
        //        {
        //            customer.SerchInput = teTJH.Text.Trim();
        //        }



        //        else
        //        {

        //            //时间
        //            if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
        //            {
        //                if (Convert.ToDateTime(teSJQ.Text) > Convert.ToDateTime(daSJZ.Text))
        //                {
        //                    dxErrorProvider.SetError(daSJZ, string.Format(Variables.GreaterThanTips, "起始日期", "结束日期"));
        //                    daSJZ.Focus();
        //                    return;
        //                }

        //                dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
        //                dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
        //                    dto.BookingDateStart = Convert.ToDateTime(teSJQ.Text);
        //                if (string.IsNullOrWhiteSpace(teSJQ.Text.Trim()) && !string.IsNullOrWhiteSpace(daSJZ.Text.Trim()))
        //                    dto.BookingDateEnd = Convert.ToDateTime(daSJZ.Text);
        //            }
        //        }
        //        //clientReg.ClientInfo = clientInfo;
        //        dto.Customer = customer;
        //        //dto.ClientReg = clientReg;




        //        var output = _PhysicalAppService.PersonalInformationQuery(new PageInputDto<CustomerRegPhysicalDto> { TotalPages = TotalPages, CurentPage = CurrentPage, Input = dto });


        //        gridControl1.DataSource = output;

        //        if (output != null)
        //        {
        //            TotalPages = output.TotalPages;
        //            CurrentPage = output.CurrentPage;
        //            gridControl1.DataSource = output.Result;
        //        }

        //        InitialNavigator(dataNavigator1);


        //    }
        //    catch (ApiProxy.UserFriendlyException ex)
        //    {
        //        ShowMessageBox(ex);
        //    }
        //    finally
        //    {
        //        if (closeWait)
        //        {
        //            if (splashScreenManager.IsSplashFormVisible)
        //                splashScreenManager.CloseWaitForm();
        //        }
        //    }



        //    gridView1.Columns[conSex.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
        //    gridView1.Columns[conSex.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);


        //}

        public void LoadData(string cusRegBM = "")
        {
            AutoLoading(() =>
            {
                gridCusReg.DataSource = null;

                // gridView1.FocusedRowHandle = -1;
                var input = new InSearchCusDto();
                if (cusRegBM != "")
                { input.CusNameBM = cusRegBM; }
                else
                {
                    if (!string.IsNullOrEmpty(textName.Text.Trim()))
                    {
                        input.CusNameBM = textName.Text.Trim();
                    }

                    input.DateType = 0;
                    input.LoginStar = Convert.ToDateTime(teSJQ.Text);
                    input.LoginEnd = Convert.ToDateTime(daSJZ.DateTime.AddDays(1).ToShortDateString());

                }
                var output = _inspectionTotalService.GetOutCus(input).ToList();
                var tjtyplist = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.ExaminationType.ToString())?.
                Where(p => p.Text.Contains("职业") || p.Text.Contains("放射")).Select(p => p.Value).ToList();
                output = output.Where(p => p.PhysicalType.HasValue && tjtyplist.Contains(p.PhysicalType.Value)).ToList();
                var sum = output.Count();
                var not = output.Count(o => o.SummSate == (int)SummSate.NotAlwaysCheck);

                gridCusReg.DataSource = output;


            });
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
                var regBM = gridView1.GetFocusedRowCellValue(conCustomerBM.FieldName);
                if (!string.IsNullOrEmpty(regBM?.ToString()))
                {
                    txtrisk.Text = regBM?.ToString();
                    simpleButton6.PerformClick();
                }
            }
        }

        private void dockPanel1_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            dockPanel1.HideSliding();
            //Point screenPoint = Control.MousePosition;
            //SetCursorPos(screenPoint.X, screenPoint.Y - 100);
            //treeListZhenDuan.Select();

            //treeListZhenDuan.Focus();
            e.Cancel = true;
        }
        //新加工种
        private void repositoryItemSearchLookUpEdit7_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "添加")
            {
                keys = "Workshop";
                Names = "车间名称";
                ParentIds = "";
                using (var frm = new OccBasicDictionaryEdit("1"))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //车间
                        ChargeBM chargeBM = new ChargeBM();
                        chargeBM.Name = ZYBBasicDictionaryType.Workshop.ToString();
                        var lis2 = _OccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
                        repositoryItemSearchLookUpEdit7.DisplayMember = "Text";
                        repositoryItemSearchLookUpEdit7.ValueMember = "Text";
                        repositoryItemSearchLookUpEdit7.DataSource = lis2;

                        gridViewzys.RefreshData();
                        gridzys.RefreshDataSource();


                    }
                }
            }

        }

        private void repositoryItemCheckedComboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "检索")
            {
                var zysDto = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
                var OccHazard = repositoryItemCheckedComboBoxEdit1.DataSource as List<ShowOccHazardFactorDto>;
                frmSelectHazard frmSelectHazard = new frmSelectHazard();

                if (zysDto != null && !string.IsNullOrEmpty(zysDto.HazardFactorIds))
                {
                    if (zysDto.HazardFactorIds != null)
                    {
                        var list = zysDto.HazardFactorIds.Split(',').ToList();
                        List<Guid> hazId = new List<Guid>();
                        foreach (var haz in list)
                        {
                            if (!string.IsNullOrEmpty(haz))
                            {
                                hazId.Add(Guid.Parse(haz));
                            }
                        }
                        var checklsit = OccHazard.Where(p => hazId.Contains(p.Id)).ToList();


                        frmSelectHazard.outOccHazardFactors = checklsit;
                    }


                }
                if (frmSelectHazard.ShowDialog() == DialogResult.OK)
                {

                    var Hazard = frmSelectHazard.outOccHazardFactors;

                    //zysDto.WorkClient = "111111";
                    zysDto.HazardFactorIds = string.Join(",", Hazard.Select(o => o.Id).ToList()).TrimEnd(',');
                    // zysDto.HisHazardFactorsText = string.Join(",", Hazard.Select(o => o.Text).ToList()).TrimEnd(',');
                    zysDto.OccHisHazardFactors = null;
                    ModelHelper.CustomMapTo(zysDto, zysDto);
                    gridViewzys.SetFocusedRowCellValue(conHazardFactorIds.FieldName, zysDto.HazardFactorIds);
                    gridzys.RefreshDataSource();
                    repositoryItemCheckedComboBoxEdit1.RefreshDataSource();

                    gridViewzys.RefreshData();
                    gridzys.RefreshDataSource();
                }
            }
        }

        private void repositoryItemCheckedComboBoxEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "检索")
            {
                var zysDto = gridzys.GetFocusedRowDto<OccupationHistoryDto>();
                var OccHazard = repositoryItemCheckedComboBoxEdit2.DataSource as List<OutOccDictionaryDto>;
                frmProtective frmSelectHazard = new frmProtective();

                if (zysDto != null && !string.IsNullOrEmpty(zysDto.HazardFactorIds))
                {
                    if (zysDto.ProtectiveIds != null)
                    {
                        var list = zysDto.ProtectiveIds.Split(',').ToList();
                        List<Guid> hazId = new List<Guid>();
                        foreach (var haz in list)
                        {
                            if (!string.IsNullOrEmpty(haz))
                            {
                                hazId.Add(Guid.Parse(haz));
                            }
                        }
                        var checklsit = OccHazard.Where(p => hazId.Contains(p.Id)).ToList();


                        frmSelectHazard.outOccHazardFactors = checklsit;
                    }
                }
                if (frmSelectHazard.ShowDialog() == DialogResult.OK)
                {

                    var Hazard = frmSelectHazard.outOccHazardFactors;

                    //zysDto.WorkClient = "111111";
                    zysDto.ProtectiveIds = string.Join(",", Hazard.Select(o => o.Id).ToList()).TrimEnd(',');
                    // zysDto.HisHazardFactorsText = string.Join(",", Hazard.Select(o => o.Text).ToList()).TrimEnd(',');

                    ModelHelper.CustomMapTo(zysDto, zysDto);
                    gridViewzys.SetFocusedRowCellValue(conProtectiveIds.FieldName, zysDto.ProtectiveIds);
                    gridzys.RefreshDataSource();
                    repositoryItemCheckedComboBoxEdit2.RefreshDataSource();
                    var index = gridViewzys.GetFocusedDataSourceRowIndex();
                    gridViewzys.RefreshRow(index);
                    gridViewzys.RefreshData();
                    gridzys.RefreshDataSource();
                }
            }
        }

        private void OccdieaseConsultation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmzz frmzz = new frmzz();
                if (frmzz.ShowDialog() == DialogResult.OK)
                {
                    var griddatda = gridZZ.DataSource as List<OccQuesSymptomDto>;
                    if (griddatda == null)
                    {
                        griddatda = new List<OccQuesSymptomDto>();
                    }
                    var zzlist = frmzz.outOccDictionaries;
                    if (zzlist != null && zzlist.Count > 0)
                    {
                        foreach (var zz in zzlist)
                        {

                            OccQuesSymptomDto occupationHistoryDto = new OccQuesSymptomDto();
                            occupationHistoryDto.CustomerRegBMId = _id;
                            occupationHistoryDto.Degree = "+";
                            occupationHistoryDto.Name = zz.Text;
                            //  occupationHistoryDto.
                            griddatda.Add(occupationHistoryDto);

                        }
                        griddatda = griddatda.Where(p => p.Name != null).ToList();
                        griddatda.Add(AddSymptomRow());

                        gridZZ.DataSource = griddatda;
                        gridZZ.RefreshDataSource();
                        gridZZ.Refresh();

                    }
                }
            }
        }

        private void txtSmokeCout_Enter(object sender, EventArgs e)
        {
            //var  ss=sender as SpinEdit;


            //ss.SelectAll();
        }

        private void txtsmokeYears_Enter(object sender, EventArgs e)
        {
            //txtsmokeYears.SelectAll();
        }

        private void spQuitYears_Enter(object sender, EventArgs e)
        {
            //txtsmokeYears.SelectAll();
        }

        private void txtDrinkCount_Enter(object sender, EventArgs e)
        {
            //txtsmokeYears.SelectAll();
        }

        private void txtDrinkYesrs_Enter(object sender, EventArgs e)
        {
            // txtsmokeYears.SelectAll();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (_id == null || _id == Guid.Empty)
                {
                    MessageBox.Show("请选择体检人！");
                    return;
                }
                //基本信息
                SaveCusDto saveCus = new SaveCusDto();
                saveCus.Id = _id;
                if (!string.IsNullOrEmpty(comMarriageStatus.EditValue?.ToString()))
                {
                    saveCus.MarriageStatus = (int)comMarriageStatus.EditValue;
                }
                if (!string.IsNullOrEmpty(txtDuty.EditValue?.ToString()))
                {
                    saveCus.Duty = txtDuty.EditValue?.ToString();
                }
                if (!string.IsNullOrEmpty(txtDegree.EditValue?.ToString()))
                {
                    saveCus.Degree = (int)txtDegree.EditValue;
                }
                if (!string.IsNullOrEmpty(txtMobile.EditValue?.ToString()))
                {
                    saveCus.Mobile = txtMobile.EditValue?.ToString();
                }
                if (!string.IsNullOrEmpty(checkedComboBoxZSZL.EditValue?.ToString()))
                {
                    saveCus.RadiationName = checkedComboBoxZSZL.EditValue?.ToString();
                }
                _OccdiseaseConsulitationAppService.SaveCustomer(saveCus);
                //如果修改照射种类自动加载照射种类
                if (!string.IsNullOrEmpty(checkedComboBoxZSZL.EditValue?.ToString()))
                {
                    var QuesRadioactives = gridRadioactive.DataSource as List<OccQuesRadioactiveCareerHistoryDto>;
                    if (QuesRadioactives != null && QuesRadioactives.Count > 0)
                    {
                        var fist = QuesRadioactives.Where(p=>p.WorkClient !=null && p.WorkClient !="").FirstOrDefault();
                        if (fist!=null && string.IsNullOrEmpty(fist.RadiationIds) &&  !string.IsNullOrEmpty(fist.WorkClient))
                        {
                            //照射种类
                            RadiationDto dto = new RadiationDto();
                            var Radiation = _OccHazardFactorAppService.ShowRadiation(dto);

                            List<string> rad = checkedComboBoxZSZL.EditValue?.ToString().Replace(", ", ",").Split(',').ToList();
                            

                            var list = Radiation.Where(p => rad.Contains(p.Text.Trim())).ToList();
                            var raids = list.Select(p => p.Id).ToList();
                            fist.RadiationIds = string.Join(",", raids);
                            gridRadioactive.DataSource = QuesRadioactives;
                            gridRadioactive.RefreshDataSource();
                        }
                    }

                }
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
