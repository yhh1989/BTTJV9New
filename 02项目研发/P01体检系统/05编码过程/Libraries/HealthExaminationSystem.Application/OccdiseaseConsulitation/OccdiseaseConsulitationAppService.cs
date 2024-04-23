using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation
{
    [AbpAuthorize]

    public class OccdiseaseConsulitationAppService : MyProjectAppServiceBase, IOccdiseaseConsulitationAppService
    {

        private readonly IRepository<TjlCustomerReg, Guid> _tjlCustomerReg;
        private readonly IRepository<TjlOccQuestionnaire, Guid> _tjlOccQuestion;
        private readonly IRepository<TjlOccQuesPastHistory, Guid> _tjlOccQuestionPast;
        private readonly IRepository<TjlOccQuesFamilyHistory, Guid> _tjlOccQuestionFamily;
        private readonly IRepository<TjlOccQuesSymptom, Guid> _tjlOccQuestionSymptom;
        private readonly IRepository<TjlOccQuesCareerHistory, Guid> _tjlOccQuestionCarrer;
        private readonly IRepository<TjlOccQuesHisProtective, Guid> _tjlOccQuestionHisProtective;
        private readonly IRepository<TjlOccQuesHisHazardFactors, Guid> _tjlOccQuestionHisHazardFactors;
        private readonly IRepository<TbmOccHazardFactor, Guid> _TbmOccHazardFactor;
        private readonly IRepository<TbmOccDictionary, Guid> _TbmOccDictionary;
        private readonly IRepository<TjlOccQuesMerriyHistory, Guid> _TjlOccQuesMerriyHistory;
        private readonly IRepository<TjlOccQuesRadioactiveCareerHistory, Guid> _TjlOccQuesRadioactiveCareerHistory;
        private readonly IRepository<TbmRadiation, Guid> _TbmRadiation;

        private readonly IRepository<TjlCustomer, Guid> _jlCustomer;

        public OccdiseaseConsulitationAppService(IRepository<TjlCustomerReg, Guid> tjlCustomerReg, IRepository<TjlOccQuestionnaire, Guid> tjlOccQuestion, IRepository<TjlOccQuesPastHistory, Guid> tjlOccQuestionPast, IRepository<TjlOccQuesSymptom, Guid> tjlOccQuestionSymptom, IRepository<TjlOccQuesCareerHistory, Guid> tjlOccQuestionCarrer, IRepository<TjlOccQuesHisProtective, Guid> tjlOccQuestionHisProtective, IRepository<TjlOccQuesHisHazardFactors, Guid> tjlOccQuestionHisHazardFactors,
            IRepository<TjlOccQuesFamilyHistory, Guid> tjlOccQuestionFamily,
            IRepository<TbmOccHazardFactor, Guid> TbmOccHazardFactor,
            IRepository<TbmOccDictionary, Guid> TbmOccDictionary,
             IRepository<TjlOccQuesMerriyHistory, Guid> TjlOccQuesMerriyHistory,
             IRepository<TjlOccQuesRadioactiveCareerHistory, Guid> TjlOccQuesRadioactiveCareerHistory,
             IRepository<TbmRadiation, Guid> TbmRadiation,
             IRepository<TjlCustomer, Guid> TjlCustomer)
        {
            _tjlCustomerReg = tjlCustomerReg;
            _tjlOccQuestion = tjlOccQuestion;
            _tjlOccQuestionPast = tjlOccQuestionPast;
            _tjlOccQuestionSymptom = tjlOccQuestionSymptom;
            _tjlOccQuestionCarrer = tjlOccQuestionCarrer;
            _tjlOccQuestionHisProtective = tjlOccQuestionHisProtective;
            _tjlOccQuestionHisHazardFactors = tjlOccQuestionHisHazardFactors;
            _tjlOccQuestionFamily = tjlOccQuestionFamily;
            _TbmOccHazardFactor = TbmOccHazardFactor;
            _TbmOccDictionary = TbmOccDictionary;
            _TjlOccQuesMerriyHistory = TjlOccQuesMerriyHistory;
            _TjlOccQuesRadioactiveCareerHistory = TjlOccQuesRadioactiveCareerHistory;
            _TbmRadiation = TbmRadiation;
            _jlCustomer = TjlCustomer;
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <returns></returns>
        public OccdieaseBasicInformationDto GetAllCustomer(OccdieaseBasicGet input)
        {
            var CusReg = _tjlCustomerReg.GetAll();
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                CusReg = CusReg.Where(i => i.CustomerBM == input.CustomerBM);
                var reg = CusReg.FirstOrDefault(o => o.CustomerBM == input.CustomerBM);
            }
            if (input.StartTime.HasValue)
                CusReg = CusReg.Where(i => i.LoginDate == input.StartTime);
            if (input.EndTime.HasValue)
                CusReg = CusReg.Where(i => i.LoginDate == input.EndTime);
            if (!string.IsNullOrEmpty(input.Name))
            {
                // CusReg = CusReg.Where(i => i.Customer.Name == input.Name);

                CusReg = CusReg.Where(r => r.Customer.Name == input.Name || r.Customer.NameAB.ToUpper().Contains(input.Name.ToUpper()));

            }

            var ls = CusReg.Select(o => new OccdieaseBasicInformationDto
            {
                CusPhotoBmId = o.Customer.CusPhotoBmId,
                CostState = o.CostState,
                PhysicalType = o.PhysicalType,
                Age = o.Customer.Age,
                ClientName = o.ClientInfo == null ? "" : o.ClientInfo.ClientName,
                TeamName = o.ClientTeamInfo == null ? "" : o.ClientTeamInfo.TeamName,
                CustomerBM = o.CustomerBM,
                Id = o.Id,
                LoginDate = o.LoginDate,
                Name = o.Customer.Name,
                RiskS = o.RiskS,
                Sex = o.Customer.Sex,
                TypeWork = o.TypeWork,
                PostState = o.PostState,
                InjuryAge = o.InjuryAge,
                InjuryAgeUnit = o.InjuryAgeUnit,
                TotalWorkAge = o.TotalWorkAge,
                WorkAgeUnit = o.WorkAgeUnit,
                WorkName = o.WorkName,
                 StarTime=o.StarTime,
                  Degree=o.Customer.Degree,
                   Duty=o.Customer.Duty,
                    MarriageStatus=o.Customer.MarriageStatus,
                     Mobile=o.Customer.Mobile,
                      RadiationName=o.RadiationName,
                OccHazardFactors = o.OccHazardFactors.Select(p => new SimpOccHazardFactorDto
                {
                    CASBM = p.CASBM,
                    Id = p.Id,
                    Text = p.Text,
                    Protectivis = p.Protectivis.Select(n => new SimpOccHazardFactorsProtectiveDto
                    {
                        OrderNum = n.OrderNum,
                        Text = n.Text
                    }).ToList()

                }).ToList()
            }).ToList();

            if (ls != null && ls.Count > 0)
            {
                return ls[0];
            }
            else
            {
                return new OccdieaseBasicInformationDto();
            }

        }
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <returns></returns>
        public void SaveCustomer(SaveCusDto input)
        {
            var CusReg = _tjlCustomerReg.Get(input.Id);            
              
            if (CusReg != null)
            {
                var cusinfo = CusReg.Customer;
                cusinfo.MarriageStatus = input.MarriageStatus;
                cusinfo.Degree = input.Degree;
                cusinfo.Mobile = input.Mobile;
                cusinfo.Duty = input.Duty;
                _jlCustomer.Update(cusinfo);
            }
            CusReg.RadiationName = input.RadiationName;

            _tjlCustomerReg.Update(CusReg);


        }

        /// <summary>
        /// 获取问卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuestionnaireDto GetAllOccupationHistory(OccdieaseHistoryRucan input)
        {

            //var Occhis = _tjlOccQuestion.GetAll().Where(i => i.Id == input.Id);
            var Occhiss = _tjlOccQuestion.FirstOrDefault(i => i.CustomerRegBMId == input.Id);
            return Occhiss.MapTo<OccQuestionnaireDto>();

        }

        /// <summary>
        /// 添加既往史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuesPastHistoryDto Add(OccQuestionPastAddrucan input)
        {
            var Occ = _tjlOccQuestion.GetAll();
            if (input.occpast.CustomerRegBMId != null)
            {
                var cus = Occ.Where(i => i.CustomerRegBMId == input.occpast.CustomerRegBMId);
            }
            OccQuestionnaireDto dt = new OccQuestionnaireDto();
            var entity = input.occpast.MapTo<TjlOccQuesPastHistory>();
            entity.Id = Guid.NewGuid();
            entity.CustomerRegBMId = input.occpast.CustomerRegBMId;
            entity.DiagnTime = input.occpast.DiagnTime;
            entity.IllName = input.occpast.IllName;
            entity.Iscured = input.occpast.Iscured;
            entity.Treatment = input.occpast.Treatment;
            entity.DiagnosisClient = input.occpast.DiagnosisClient;
            entity.DiagnTime = input.occpast.DiagnTime;
            var ds = _tjlOccQuestionPast.Insert(entity);
            var dtos = entity.MapTo<OccQuesPastHistoryDto>();
            return dtos;
        }

        /// <summary>
        /// 添加家族史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuesFamilyHistoryDto AddFamily(OccQuestionFamilyrucanDto input)
        {
            var Occ = _tjlOccQuestion.GetAll();
            if (input.Occfamly.CustomerRegBMId != null)
            {
                var cus = Occ.Where(i => i.CustomerRegBMId == input.Occfamly.CustomerRegBMId);
            }
            OccQuestionnaireDto dt = new OccQuestionnaireDto();
            var entity = input.Occfamly.MapTo<TjlOccQuesFamilyHistory>();
            entity.Id = Guid.NewGuid();
            entity.CustomerRegBMId = input.Occfamly.CustomerRegBMId;
            entity.IllName = input.Occfamly.IllName;
            entity.relatives = input.Occfamly.relatives;

            var ds = _tjlOccQuestionFamily.Insert(entity);
            var dtos = entity.MapTo<OccQuesFamilyHistoryDto>();
            return dtos;
        }

        /// <summary>
        /// 添加近期症状
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccQuesSymptomDto AddSymptom(OccQustionSymptomrucanDto input)
        {
            var Occ = _tjlOccQuestion.GetAll();
            if (input.occQustionSymptom.CustomerRegBMId != null)
            {
                var cus = Occ.Where(i => i.CustomerRegBMId == input.occQustionSymptom.CustomerRegBMId);
            }
            OccQuestionnaireDto dt = new OccQuestionnaireDto();
            var entity = input.occQustionSymptom.MapTo<TjlOccQuesSymptom>();
            entity.Id = Guid.NewGuid();
            entity.CustomerRegBMId = input.occQustionSymptom.CustomerRegBMId;
            entity.Name = input.occQustionSymptom.Name;
            entity.Degree = input.occQustionSymptom.Degree;

            var ds = _tjlOccQuestionSymptom.Insert(entity);
            var dtos = entity.MapTo<OccQuesSymptomDto>();
            return dtos;
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
        //public OccQuestionnaireDto EditQuestionnaire()
        //{

        //}

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="DynamicData">动态数据集合</param>
        /// <returns></returns>
        public OccQuestionnaireDto AddData(dynamic DynamicData)
        { 
            var ObjectZY = new List<OccupationHistoryDto>();
            var ObjectJw = new List<OccQuesPastHistoryDto>();
            var ObjectJz = new List<OccQuesFamilyHistoryDto>();
            var ObjectZZ = new List<OccQuesSymptomDto>();
            var ObjectMerr = new List<OccQuesMerriyHistoryDto>();
            var ObjectRadioa = new List<OccQuesRadioactiveCareerHistoryDto>();
            Guid? quesid = DynamicData.OccQuestId;
            //判断添加还是修改
            if (quesid!=null && quesid!=Guid.Empty)
            {
                //插入主表数据
                var tjloccque = _tjlOccQuestion.Get(quesid.Value);
                tjloccque.Id = DynamicData.OccQuestId;
                if (DynamicData.stringData.chuchao != null && DynamicData.stringData.chuchao != "")
                {
                    tjloccque.StarAge = DynamicData.stringData.chuchao;
                }
                tjloccque.Cycle = DynamicData.stringData.yuejing;
                if (DynamicData.stringData.tingjing != null && DynamicData.stringData.tingjing != "")
                {
                    tjloccque.EndAge = DynamicData.stringData.tingjing;
                }
                
                tjloccque.ChildrenNum = DynamicData.stringData.zinvSum;
                tjloccque.IsAbortion = DynamicData.stringData.isliuchan;
                tjloccque.AbortionCount = DynamicData.stringData.liuchan;
                tjloccque.IsPrematureDelivery = DynamicData.stringData.iszc;
                tjloccque.IsStillbirth = DynamicData.stringData.issichan;
                tjloccque.IsSmoke = DynamicData.stringData.isSmok;
                tjloccque.SmokeCount = DynamicData.stringData.xiyancishu;
                tjloccque.SmokeYears = DynamicData.stringData.xiyannianxian;
                tjloccque.IsDrink = DynamicData.stringData.isyinjiu;
                tjloccque.DrinkCount = DynamicData.stringData.yinjiucishu;
                tjloccque.DrinkYears = DynamicData.stringData.yinjiunianxian;
                tjloccque.SignaTureImage= DynamicData?.stringData.SignaTureImage;
                tjloccque.MedicationHistory = DynamicData?.stringData.MedicationHistory;
                tjloccque.GeneticHistory = DynamicData?.stringData.GeneticHistory;
                tjloccque.AllergicHistory = DynamicData?.stringData.AllergicHistory;
                tjloccque.DrugTaboo = DynamicData?.stringData.DrugTaboo;
                tjloccque.PrematureDeliveryCount = DynamicData?.stringData.PrematureDeliveryCount;
                tjloccque.StillbirthCount = DynamicData?.stringData.StillbirthCount;
                tjloccque.AbnormityCount = DynamicData?.stringData.AbnormityCount;
                tjloccque.PastHistory= DynamicData?.stringData.PastHistory;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.jq?.ToString()) && int.TryParse(DynamicData?.stringData.jq.ToString(), out int jq))
                {
                    tjloccque.period = DynamicData?.stringData.jq;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.PregnancyCount?.ToString()) && int.TryParse(DynamicData?.stringData.PregnancyCount.ToString(), out int PregnancyCount))
                {
                    tjloccque.PregnancyCount = DynamicData?.stringData.PregnancyCount;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.LiveBirth?.ToString()) && int.TryParse(DynamicData?.stringData.LiveBirth.ToString(), out int LiveBirth))
                {
                    tjloccque.LiveBirth = DynamicData?.stringData.LiveBirth;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.Teratogenesis?.ToString()) && int.TryParse(DynamicData?.stringData.Teratogenesis.ToString(), out int Teratogenesis))
                {
                    tjloccque.Teratogenesis = DynamicData?.stringData.Teratogenesis;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.MultipleBirths?.ToString()) && int.TryParse(DynamicData?.stringData.MultipleBirths.ToString(), out int MultipleBirths))
                {
                    tjloccque.MultipleBirths = DynamicData?.stringData.MultipleBirths;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.EctopicPregnancy?.ToString()) && int.TryParse(DynamicData?.stringData.EctopicPregnancy.ToString(), out int EctopicPregnancy))
                {
                    tjloccque.EctopicPregnancy = DynamicData?.stringData.EctopicPregnancy;
                }
                //if (!string.IsNullOrEmpty(DynamicData?.stringData?.Infertility?.ToString()) )
                //{
                    tjloccque.Infertility = DynamicData?.stringData.Infertility;
                //}
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.BoyChildrenNum?.ToString()) && int.TryParse(DynamicData?.stringData.BoyChildrenNum.ToString(), out int BoyChildrenNum))
                {
                    tjloccque.BoyChildrenNum = DynamicData?.stringData.BoyChildrenNum;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.BoyBrith?.ToString()) && DateTime.TryParse(DynamicData?.stringData.BoyBrith.ToString(), out DateTime boydt))
                {
                    tjloccque.BoyBrith = DynamicData?.stringData.BoyBrith;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.grilChildrenNum?.ToString()) && int.TryParse(DynamicData?.stringData.grilChildrenNum.ToString(), out int grilChildrenNum))
                {
                    tjloccque.grilChildrenNum = DynamicData?.stringData.grilChildrenNum;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.grilBrith?.ToString()) && DateTime.TryParse(DynamicData?.stringData.grilBrith.ToString(), out DateTime grildt))
                {
                    tjloccque.grilBrith = DynamicData?.stringData.grilBrith;
                }
                tjloccque.ChildHealthy = DynamicData?.stringData.ChildHealthy;
                tjloccque.LifeHistory = DynamicData?.stringData.LifeHistory;
                //tjloccque.QuitYears = DynamicData?.stringData.QuitYears;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.QuitYears?.ToString()) && int.TryParse(DynamicData?.stringData.QuitYears.ToString(), out int QuitYears))
                {
                    tjloccque.QuitYears = DynamicData?.stringData.QuitYears;
                }
                var entity = _tjlOccQuestion.Update(tjloccque);
                if (!string.IsNullOrEmpty(DynamicData.ObjectZY?.ToString()))
                {
                    ObjectZY = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccupationHistoryDto>>(DynamicData.ObjectZY.ToString()) as List<OccupationHistoryDto>;
                    ObjectZY = ObjectZY.Where(p => p.WorkClient != null && p.WorkClient != "").ToList();
                }
                var LsitObjectZY = new List<TjlOccQuesCareerHistory>();
                if (!string.IsNullOrEmpty(DynamicData.ObjectJw?.ToString()))
                {
                    ObjectJw = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesPastHistoryDto>>(DynamicData.ObjectJw.ToString()) as List<OccQuesPastHistoryDto>;
                    ObjectJw = ObjectJw.Where(p => p.IllName != null && p.IllName != "").ToList();
                }
                var LsitObjectJw = new List<TjlOccQuesPastHistory>();
                if (!string.IsNullOrEmpty(DynamicData.ObjectJz?.ToString()))
                {
                    ObjectJz = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesFamilyHistoryDto>>(DynamicData.ObjectJz.ToString()) as List<OccQuesFamilyHistoryDto>;
                    ObjectJz = ObjectJz.Where(p=>p.IllName!=null && p.IllName!="").ToList();
                }
                var LsitObjectJz = new List<TjlOccQuesFamilyHistory>();
                if (!string.IsNullOrEmpty(DynamicData.ObjectZZ?.ToString()))
                {
                    ObjectZZ = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesSymptomDto>>(DynamicData.ObjectZZ.ToString()) as List<OccQuesSymptomDto>;
                    ObjectZZ = ObjectZZ.Where(p=>p.Name!=null && p.Name!="").ToList();
                }
                if (!string.IsNullOrEmpty(DynamicData.ObjectMerr?.ToString()))
                {
                    ObjectMerr = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesMerriyHistoryDto>>(DynamicData.ObjectMerr.ToString()) as List<OccQuesMerriyHistoryDto>;
                    ObjectMerr = ObjectMerr.Where(p => p.Radioactive != null && p.Radioactive != "").ToList();
                }
                if (!string.IsNullOrEmpty(DynamicData.ObjectRadioactiveCareer?.ToString()))
                {
                    ObjectRadioa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesRadioactiveCareerHistoryDto>>(DynamicData.ObjectRadioactiveCareer.ToString()) as List<OccQuesRadioactiveCareerHistoryDto>;
                    ObjectRadioa = ObjectRadioa.Where(p => p.WorkClient != null && p.WorkClient != "").ToList();
                }


                var LsitObjectZZ = new List<TjlOccQuesSymptom>();
                //判断职业史
                if (DynamicData.ObjectZY != null && DynamicData.ObjectZY.Count > 0)
                { 

                   
                    var hszys = _tjlOccQuestionCarrer.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Select(o => o.Id).ToList();
                    //删除职业史危害因素
                    var deltewh = _tjlOccQuestionHisHazardFactors.GetAll().Where(o => hszys.Contains(o.OccCareerHistoryBMId.Value)).Delete();
                    //删除职业史防护措施
                    var delteZZ=_tjlOccQuestionHisProtective.GetAll().Where(o => hszys.Contains(o.OccCareerHistoryBMId.Value)).Delete();
                    //删除没有的职业史
                    var delet = _tjlOccQuestionCarrer.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    if (ObjectZY.Count==0)
                    {
                        var cusreg = _tjlCustomerReg.Get(entity.CustomerRegBMId.Value);
                        if (string.IsNullOrEmpty(cusreg.InjuryAge) || string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        {
                            if (string.IsNullOrEmpty(cusreg.InjuryAge))
                            { cusreg.InjuryAge = "0"; }
                            if (string.IsNullOrEmpty(cusreg.TotalWorkAge))
                            { cusreg.TotalWorkAge = "0"; }
                            _tjlCustomerReg.Update(cusreg);
                        }

                    }
                    foreach (var zy in ObjectZY)
                    {
                       
                        //else
                        //{
                            //新增职业史
                            zy.Id = Guid.NewGuid();                     

                        //var zyswh = zy.OccHisHazardFactors?.ToList();
                        //var zyszz = zy.OccHisProtectives?.ToList();
                        zy.OccHisHazardFactors = null;
                        zy.OccHisProtectives = null;

                            var itemGroupEntity = zy.MapTo<TjlOccQuesCareerHistory>();
                        itemGroupEntity.CustomerRegBMId = tjloccque.CustomerRegBMId;
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                            LsitObjectZY.Add(itemGroupEntity);
                          var  ishs = _tjlOccQuestionCarrer.Insert(itemGroupEntity);
                       
                        //保存职业史危害因素
                       
                        if (!string.IsNullOrEmpty(zy.HazardFactorIds))
                        {
                            var riskIds = zy.HazardFactorIds.Split(',').ToList();
                            foreach (var occf in riskIds)
                            {
                                if (!string.IsNullOrEmpty(occf))
                                {
                                    var riskId = Guid.Parse(occf);
                                    var tbmris = _TbmOccHazardFactor.GetAll().FirstOrDefault(p => p.Id == riskId);
                                    if (tbmris != null)
                                    {
                                        TjlOccQuesHisHazardFactors tbmoccf = new TjlOccQuesHisHazardFactors();
                                        tbmoccf.CASBM = tbmris.CASBM;
                                        tbmoccf.Text = tbmris.Text;
                                        tbmoccf.TypeName = tbmris.Parent?.Text;
                                        tbmoccf.Id = Guid.NewGuid();
                                        tbmoccf.OccCareerHistoryBMId = zy.Id;
                                        _tjlOccQuestionHisHazardFactors.Insert(tbmoccf);
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(zy.ProtectiveIds))
                        {
                            var protctIDs = zy.ProtectiveIds.Split(',').ToList();
                            foreach (var occf in protctIDs)
                            {
                                if (!string.IsNullOrEmpty(occf))
                                {
                                    var proctId = Guid.Parse(occf);
                                    var occ = _TbmOccDictionary.GetAll().FirstOrDefault(
                                        p => p.Type == "Protect" && p.Id == proctId);
                                    if (occ != null)
                                    {
                                        TjlOccQuesHisProtective tbmoccf = new TjlOccQuesHisProtective();
                                        tbmoccf.BM = occ.code;
                                        tbmoccf.Text = occ.Text;
                                        tbmoccf.Id = Guid.NewGuid();
                                        tbmoccf.OccCareerHistoryBMId = zy.Id;
                                        _tjlOccQuestionHisProtective.Insert(tbmoccf);
                                    }
                                }
                            }
                        }
                        //}
                    }
                    #region 计算工龄
                    if (ObjectZY.Count > 0)
                    {
                        //修改体检工龄
                        #region 按开始结束时间
                        

                        #endregion
                        #region 按接害
                        var ZGLList = ObjectZY.Select(p =>p.EndTime.Value- p.StarTime.Value).ToList();
                         

                        var JHGLList = ObjectZY.Where(p => p.HazardFactorIds != null && p.HazardFactorIds != "")
                            .Select(p => p.EndTime.Value - p.StarTime.Value).ToList();

                        //var maxJHGL = ObjectZY.Where(p => p.HazardFactorIds != null && p.HazardFactorIds != "").Max(p => p.EndTime);

                        if (ZGLList.Count>0 && JHGLList.Count>0)
                        {
                            
                                var cusreg = _tjlCustomerReg.Get(tjloccque.CustomerRegBMId.Value);
                            if (!cusreg.PostState.Contains("上岗"))
                            {
                                //接害工龄
                                TimeSpan ts = new TimeSpan();
                                foreach (var tn in JHGLList)
                                {
                                    ts = ts + tn;
                                }

                                int yes = Convert.ToInt32(ts.TotalDays) / 365;
                                int moth = Convert.ToInt32(ts.TotalDays) % 365 / 30;
                                //一个月30天不是太准确
                                try
                                {
                                    if (ObjectZY.Count == 1)
                                    {
                                        var mm = ObjectZY.FirstOrDefault().StrWorkYears.Replace("年", "|").Replace("月", "|").
                                            Replace("日", "").Split('|');
                                        moth = int.Parse(mm[1]);
                                    }
                                    if (moth >= 12)
                                    {
                                        moth = 11;
                                    }
                                }
                                catch (Exception)
                                {

                                }
                                if (yes == 0)
                                {
                                    cusreg.InjuryAge = moth.ToString();
                                }
                                else
                                { cusreg.InjuryAge = yes + "." + moth; }
                                //总工龄
                                TimeSpan ts1 = new TimeSpan();
                                foreach (var tn in ZGLList)
                                {
                                    ts1 = ts1 + tn;
                                }
                                int yes1 = Convert.ToInt32(ts1.TotalDays) / 365;
                                int moth1 = Convert.ToInt32(ts1.TotalDays) % 365 / 30;
                                //一个月30天不是太准确
                                try
                                {
                                    if (ObjectZY.Count == 1)
                                    {
                                        var mm = ObjectZY.FirstOrDefault().StrWorkYears.Replace("年", "|").Replace("月", "|").
                                            Replace("日", "").Split('|');
                                        moth1 = int.Parse(mm[1]);
                                    }
                                    if (moth1 >= 12)
                                    {
                                        moth1 = 11;
                                    }
                                }
                                catch (Exception)
                                {

                                }
                                if (yes1 == 0)
                                {
                                    cusreg.TotalWorkAge = moth1.ToString();
                                }
                                else
                                {
                                    cusreg.TotalWorkAge = yes1 + "." + moth1;
                                }

                                var WorkType = ObjectZY.Where(p => p.WorkType != null && p.WorkType != "" &&
                                p.HazardFactorIds != null && p.HazardFactorIds != "").
                                    OrderByDescending(p => p.StarTime).FirstOrDefault();
                                if (WorkType != null)
                                {
                                    cusreg.TypeWork = WorkType.WorkType?.ToString();
                                }
                                var WorkName = ObjectZY.Where(p => p.WorkName != null && p.WorkName != "" &&
                                p.HazardFactorIds != null && p.HazardFactorIds != "").
                                   OrderByDescending(p => p.StarTime).FirstOrDefault();
                                if (WorkName != null)
                                {
                                    cusreg.WorkName = WorkName.WorkName?.ToString();
                                }
                                if (yes > 0)
                                {
                                    cusreg.InjuryAgeUnit = "年";
                                }
                                else
                                { cusreg.InjuryAgeUnit = "月"; }

                                if (yes1 > 0)
                                {
                                    cusreg.WorkAgeUnit = "年";
                                }
                                else
                                { cusreg.WorkAgeUnit = "月"; }


                                _tjlCustomerReg.Update(cusreg);
                            }

                        }
                        #endregion
                    }
                    #endregion
                    entity.OccCareerHistory = LsitObjectZY;


                }
                //没有职业史都删掉
                else
                {//先删除职业史管理危害因素

                    var CarrerHisIDs = _tjlOccQuestionCarrer.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Select(
                        p => p.Id).ToList();
                    _tjlOccQuestionHisHazardFactors.GetAll().Where(o => CarrerHisIDs.Contains(o.OccCareerHistoryBMId.Value)).Delete();
                    _tjlOccQuestionHisProtective.GetAll().Where(o => CarrerHisIDs.Contains(o.OccCareerHistoryBMId.Value)).Delete();
                    var Carrer = _tjlOccQuestionCarrer.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();

                  
                }

                #region 放射职业史
                //判断既往史
                var LsitObjectRadioactive = new List<TjlOccQuesRadioactiveCareerHistory>();
                if (DynamicData.ObjectRadioactiveCareer != null && DynamicData.ObjectRadioactiveCareer.Count > 0)
                {
                    //删除没有的既往史
                    var delradioajwslist = _TjlOccQuesRadioactiveCareerHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).ToList();
                    foreach (var delradioajws in delradioajwslist)
                    {
                        _TjlOccQuesRadioactiveCareerHistory.Delete(delradioajws);
                    }
                    //var delet = _TjlOccQuesRadioactiveCareerHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zw in ObjectRadioa)
                    {
                        //var ishs = _TjlOccQuesRadioactiveCareerHistory.FirstOrDefault(o => o.OccQuestionnaireBMId == quesid);
                        ////已有修改
                        //if (ishs != null)
                        //{

                        //    // 更新既往史
                        //    var ss = zw.MapTo(ishs);
                        //   // ishs.TbmOccDictionarys = new ICollection<TbmOccDictionary>(); 
                        //    if (!string.IsNullOrEmpty(ishs.TbmOccDictionaryIDs))
                        //    {
                        //        var diclist = ishs.TbmOccDictionaryIDs.Split(',').ToList();
                        //        foreach (var dic in diclist)
                        //        {
                        //            var dicint = _TbmOccDictionary.GetAll().FirstOrDefault(p => p.Id ==Guid.Parse( dic));
                        //            if (dicint != null)
                        //            {
                        //                ishs.TbmOccDictionarys.Add(dicint);
                        //            }
                        //        }
                        //    }
                        //    if (!string.IsNullOrEmpty(ishs.RadiationIds))
                        //    {
                        //        var diclist = ishs.RadiationIds.Split(',').ToList();
                        //        foreach (var dic in diclist)
                        //        {
                        //            var dicint = _TbmRadiation.GetAll().FirstOrDefault(p => p.Id == Guid.Parse(dic));
                        //            if (dicint != null)
                        //            {
                        //                ishs.Radiations.Add(dicint);
                        //            }
                        //        }
                        //    }
                        //    LsitObjectRadioactive.Add(ss);
                        //    ishs = _TjlOccQuesRadioactiveCareerHistory.Update(ishs);


                        //}
                        //else
                        //{
                            //新增既往史
                            zw.Id = Guid.NewGuid();
                            var itemGroupEntity = zw.MapTo<TjlOccQuesRadioactiveCareerHistory>();
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                        itemGroupEntity.TbmOccDictionarys = new List<TbmOccDictionary>();
                            if (!string.IsNullOrEmpty(itemGroupEntity.TbmOccDictionaryIDs))
                            {
                                var diclist = itemGroupEntity.TbmOccDictionaryIDs.Split(',').ToList();
                                foreach (var dic in diclist)
                                {

                                var guidid = Guid.Parse(dic);
                                    var dicint = _TbmOccDictionary.GetAll().FirstOrDefault(p => p.Id == guidid);
                                    if (dicint != null)
                                    {
                                        itemGroupEntity.TbmOccDictionarys.Add(dicint);
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(itemGroupEntity.RadiationIds))
                            {
                                var diclist = itemGroupEntity.RadiationIds.Split(',').ToList();
                            itemGroupEntity.Radiations = new List<TbmRadiation>();
                            foreach (var dic in diclist)
                                {
                                var guidid = Guid.Parse(dic);
                                var dicint = _TbmRadiation.GetAll().FirstOrDefault(p => p.Id == guidid);
                                    if (dicint != null)
                                    {
                                        itemGroupEntity.Radiations.Add(dicint);
                                    }
                                }
                            }
                            LsitObjectRadioactive.Add(itemGroupEntity);
                             _TjlOccQuesRadioactiveCareerHistory.Insert(itemGroupEntity);

                        //}
                    }
                    entity.RadioactiveCareerHistory = LsitObjectRadioactive;
                }
                //没有既往史都删掉
                else
                {
                    var Past = _TjlOccQuesRadioactiveCareerHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }
                #endregion
                #region 婚姻史
                //判断婚姻史
                var LsitObjectMerr = new List<TjlOccQuesMerriyHistory>();
                if (DynamicData.ObjectMerr != null && DynamicData.ObjectMerr.Count > 0)
                {
                    //删除没有的婚姻史

                    var delet = _TjlOccQuesMerriyHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zw in ObjectMerr)
                    {
                        //var ishs = _TjlOccQuesMerriyHistory.FirstOrDefault(o => o.OccQuestionnaireBMId == quesid);
                        ////已有修改
                        //if (ishs != null)
                        //{
                        //    // 更新婚姻史
                        //    var ss = zw.MapTo(ishs);
                        //    // ishs.TbmOccDictionarys = new ICollection<TbmOccDictionary>(); 
                        //    LsitObjectMerr.Add(ss);
                        //    ishs = _TjlOccQuesMerriyHistory.Update(ishs);

                        //}
                        //else
                        //{
                            //新增婚姻史
                            zw.Id = Guid.NewGuid();
                            var itemGroupEntity = zw.MapTo<TjlOccQuesMerriyHistory>();
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                            LsitObjectMerr.Add(itemGroupEntity);
                              _TjlOccQuesMerriyHistory.Insert(itemGroupEntity);

                        //}
                    }
                    entity.OccQuesMerriyHistory = LsitObjectMerr;
                }
                //没有既往史都删掉
                else
                {
                    var Past = _TjlOccQuesMerriyHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }
                #endregion
                //判断既往史
                if (DynamicData.ObjectJw != null && DynamicData.ObjectJw.Count > 0)
                {
                    //删除没有的既往史
                    var delet = _tjlOccQuestionPast.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zw in ObjectJw)
                    {
                        var ishs = _tjlOccQuestionPast.FirstOrDefault(o => o.OccQuestionnaireBMId == quesid);
                        //已有修改
                        if (ishs != null)
                        {

                            // 更新既往史
                            var ss=zw.MapTo(ishs);
                            LsitObjectJw.Add(ss);
                            ishs = _tjlOccQuestionPast.Update(ishs);


                        }
                        else
                        {
                            //新增既往史
                            zw.Id = Guid.NewGuid();
                            var itemGroupEntity = zw.MapTo<TjlOccQuesPastHistory>();
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                            LsitObjectJw.Add(itemGroupEntity);
                            ishs = _tjlOccQuestionPast.Insert(itemGroupEntity);

                        }
                    }                  
                    entity.OccPastHistory = LsitObjectJw;
                }
                //没有既往史都删掉
                else
                {
                    var Past = _tjlOccQuestionPast.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }

             //判断家族史
                if (DynamicData.ObjectJz != null && DynamicData.ObjectJz.Count > 0)
                {
                    //删除没有的家族史
                    var delet = _tjlOccQuestionFamily.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zy in ObjectJz)
                    {
                        var ishs = _tjlOccQuestionFamily.FirstOrDefault(o => o.OccQuestionnaireBMId == quesid);
                        //已有修改
                        if (ishs != null)
                        {
                            // 更新家族史
                            var ss=zy.MapTo(ishs);
                            LsitObjectJz.Add(ss);
                            ishs = _tjlOccQuestionFamily.Update(ishs);
                        }
                        else
                        {
                            //新增家族史
                            zy.Id = Guid.NewGuid();
                            var itemGroupEntity = zy.MapTo<TjlOccQuesFamilyHistory>();
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                            LsitObjectJz.Add(itemGroupEntity);
                            ishs = _tjlOccQuestionFamily.Insert(itemGroupEntity);

                        }
                    }                    
                    entity.OccFamilyHistory = LsitObjectJz;
                }
                //没有家族史都删掉
                else
                {
                    var Past = _tjlOccQuestionFamily.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    try
                    { CurrentUnitOfWork.SaveChanges(); }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

              //判断近期症状
                if (DynamicData.ObjectZZ != null && DynamicData.ObjectZZ.Count > 0)
                {
                    //删除没有的近期症状
                    var delet = _tjlOccQuestionSymptom.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zy in ObjectZZ)
                    {
                        var ishs = _tjlOccQuestionSymptom.FirstOrDefault(o => o.OccQuestionnaireBMId == quesid);
                        //已有修改
                        if (ishs != null)
                        {

                            // 更新近期症状
                            var ss=zy.MapTo(ishs);
                            LsitObjectZZ.Add(ss);
                            ishs = _tjlOccQuestionSymptom.Update(ishs);


                        }
                        else
                        {
                            //新增近期症状
                            zy.Id = Guid.NewGuid();
                            var itemGroupEntity = zy.MapTo<TjlOccQuesSymptom>();
                            itemGroupEntity.OccQuestionnaireBMId = DynamicData.OccQuestId;
                            LsitObjectZZ.Add(itemGroupEntity);
                            ishs = _tjlOccQuestionSymptom.Insert(itemGroupEntity);

                        }
                    }
                    entity.OccQuesSymptom = LsitObjectZZ;
                }
                //没有近期症状都删掉
                else
                {
                    var Past = _tjlOccQuestionSymptom.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }
                if (!string.IsNullOrEmpty(DynamicData.Cusid?.ToString()))
                {
                    var cusreg = _tjlCustomerReg.Get((Guid)DynamicData.Cusid);
                    if (cusreg != null)
                    {
                        cusreg.QuestionState = 1;
                        cusreg.QuestionTime = System.DateTime.Now;
                    }

                }
                return new OccQuestionnaireDto
                {
                    Id = entity.Id,
                    StarAge = entity.StarAge,
                    Cycle = entity.Cycle,
                    EndAge = entity.EndAge,
                    ChildrenNum = entity.ChildrenNum,
                    IsAbortion = entity.IsAbortion,
                    AbortionCount = entity.AbortionCount,
                    IsPrematureDelivery = entity.IsPrematureDelivery,
                    IsStillbirth = entity.IsStillbirth,
                    IsSmoke = entity.IsSmoke,
                    SmokeCount = entity.SmokeCount,
                    SmokeYears = entity.SmokeYears,
                    IsDrink = entity.IsDrink,
                    DrinkCount = entity.DrinkCount,
                    DrinkYears = entity.DrinkYears,
                    AskAdvice = entity.AskAdvice,
                    period = entity.period,
                    malFormation = entity.malFormation,
                    Abnormal = entity.Abnormal,
                    CustomerRegBMId = entity.CustomerRegBMId,
                    OccCareerHistory = ObjectZY,
                    OccPastHistory = ObjectJw,
                    OccFamilyHistory = ObjectJz,
                    OccQuesSymptom = ObjectZZ,
                     

                };

            }
            else
            {
                //插入问卷主表数据
                TjlOccQuestionnaire tjlOccQuestionnaire = new TjlOccQuestionnaire();
                tjlOccQuestionnaire.Id = Guid.NewGuid();
                if (DynamicData.stringData.chuchao != null && DynamicData.stringData.chuchao != "")
                {
                    tjlOccQuestionnaire.StarAge = DynamicData.stringData.chuchao;
                }
                
               // tjlOccQuestionnaire.Cycle = DynamicData.stringData.yuejing;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.yuejing?.ToString())
 && int.TryParse(DynamicData?.stringData.yuejing.ToString(), out int yuejing))
                {
                    tjlOccQuestionnaire.Cycle = DynamicData.stringData.yuejing;
                }
                if (DynamicData.stringData.tingjing != null && DynamicData.stringData.tingjing != "")
                {
                    tjlOccQuestionnaire.EndAge = DynamicData.stringData.tingjing;
                }
                //tjlOccQuestionnaire.period = DynamicData?.stringData.jq;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.jq?.ToString())
  && int.TryParse(DynamicData?.stringData.jq.ToString(), out int jq))
                {
                    tjlOccQuestionnaire.period = DynamicData?.stringData.jq;
                }

                //tjlOccQuestionnaire.ChildrenNum = DynamicData.stringData.zinvSum;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.zinvSum?.ToString())
    && int.TryParse(DynamicData?.stringData.zinvSum.ToString(), out int zinvSum))
                {
                    tjlOccQuestionnaire.ChildrenNum = DynamicData.stringData.zinvSum;
                }

                tjlOccQuestionnaire.IsAbortion = DynamicData.stringData.isliuchan;

               // tjlOccQuestionnaire.AbortionCount = DynamicData.stringData.liuchan;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.liuchan?.ToString())
      && int.TryParse(DynamicData?.stringData.liuchan.ToString(), out int liuchan))
                {
                    tjlOccQuestionnaire.AbortionCount = DynamicData.stringData.liuchan;
                }

                tjlOccQuestionnaire.IsPrematureDelivery = DynamicData.stringData.iszc;
                tjlOccQuestionnaire.IsStillbirth = DynamicData.stringData.issichan;
                tjlOccQuestionnaire.IsSmoke = DynamicData.stringData.isSmok;

               // tjlOccQuestionnaire.SmokeCount = DynamicData.stringData.xiyancishu;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.xiyancishu?.ToString())
      && int.TryParse(DynamicData?.stringData.xiyancishu.ToString(), out int xiyancishu))
                {
                    tjlOccQuestionnaire.SmokeCount = DynamicData.stringData.xiyancishu;
                }
                //tjlOccQuestionnaire.SmokeYears = DynamicData.stringData.xiyannianxian;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.xiyannianxian?.ToString())
       && int.TryParse(DynamicData?.stringData.xiyannianxian.ToString(), out int xiyannianxian))
                {
                    tjlOccQuestionnaire.SmokeYears = DynamicData.stringData.xiyannianxian;
                }
                tjlOccQuestionnaire.IsDrink = DynamicData.stringData.isyinjiu;

               // tjlOccQuestionnaire.DrinkCount = DynamicData.stringData.yinjiucishu;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.yinjiucishu?.ToString())
       && int.TryParse(DynamicData?.stringData.yinjiucishu.ToString(), out int yinjiucishu))
                {
                    tjlOccQuestionnaire.DrinkCount = DynamicData.stringData.yinjiucishu;
                }
                //tjlOccQuestionnaire.DrinkYears = DynamicData.stringData.yinjiunianxian;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.yinjiunianxian?.ToString())
        && int.TryParse(DynamicData?.stringData.yinjiunianxian.ToString(), out int yinjiunianxian))
                {
                    tjlOccQuestionnaire.DrinkYears = DynamicData.stringData.yinjiunianxian;
                }

                tjlOccQuestionnaire.AskAdvice = null;
                tjlOccQuestionnaire.period = null;
                tjlOccQuestionnaire.malFormation = null;
                tjlOccQuestionnaire.Abnormal = null;
                tjlOccQuestionnaire.CustomerRegBMId = DynamicData.Cusid;
                tjlOccQuestionnaire.MedicationHistory = DynamicData?.stringData.MedicationHistory;
                tjlOccQuestionnaire.GeneticHistory = DynamicData?.stringData.GeneticHistory;
                tjlOccQuestionnaire.AllergicHistory = DynamicData?.stringData.AllergicHistory;
                tjlOccQuestionnaire.DrugTaboo = DynamicData?.stringData.DrugTaboo;
                tjlOccQuestionnaire.PrematureDeliveryCount = DynamicData?.stringData.PrematureDeliveryCount;

                tjlOccQuestionnaire.StillbirthCount = DynamicData?.stringData.StillbirthCount;
                tjlOccQuestionnaire.AbnormityCount = DynamicData?.stringData.AbnormityCount;
                tjlOccQuestionnaire.PastHistory = DynamicData?.stringData.PastHistory;
                //tjlOccQuestionnaire.period = DynamicData?.stringData.jq;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.jq?.ToString())
         && int.TryParse(DynamicData?.stringData.jq.ToString(), out int jq1))
                {
                    tjlOccQuestionnaire.period = DynamicData?.stringData.jq;
                }
                //新增    

                // tjlOccQuestionnaire.PregnancyCount = DynamicData?.stringData.PregnancyCount;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.PregnancyCount?.ToString())
           && int.TryParse(DynamicData?.stringData.PregnancyCount.ToString(), out int PregnancyCount1))
                {
                    tjlOccQuestionnaire.PregnancyCount = DynamicData?.stringData.PregnancyCount;
                }

                // tjlOccQuestionnaire.LiveBirth = DynamicData?.stringData.LiveBirth;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.LiveBirth?.ToString())
             && int.TryParse(DynamicData?.stringData.LiveBirth.ToString(), out int LiveBirth1))
                {
                    tjlOccQuestionnaire.LiveBirth = DynamicData?.stringData.LiveBirth;
                }

                // tjlOccQuestionnaire.Teratogenesis = DynamicData?.stringData.Teratogenesis;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.Teratogenesis?.ToString())
               && int.TryParse(DynamicData?.stringData.Teratogenesis.ToString(), out int Teratogenesis))
                {
                    tjlOccQuestionnaire.Teratogenesis = DynamicData?.stringData.Teratogenesis;
                }

                // tjlOccQuestionnaire.MultipleBirths = DynamicData?.stringData.MultipleBirths;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.MultipleBirths?.ToString())
                && int.TryParse(DynamicData?.stringData.MultipleBirths.ToString(), out int MultipleBirths))
                {
                    tjlOccQuestionnaire.MultipleBirths = DynamicData?.stringData.MultipleBirths;
                }

                //tjlOccQuestionnaire.EctopicPregnancy = DynamicData?.stringData.EctopicPregnancy;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.EctopicPregnancy?.ToString())
                  && int.TryParse(DynamicData?.stringData.EctopicPregnancy.ToString(), out int EctopicPregnancy))
                {
                    tjlOccQuestionnaire.EctopicPregnancy = DynamicData?.stringData.EctopicPregnancy;
                }
                tjlOccQuestionnaire.Infertility = DynamicData?.stringData.Infertility;
                //tjlOccQuestionnaire.BoyChildrenNum = DynamicData?.stringData.BoyChildrenNum;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.BoyChildrenNum?.ToString())
                   && int.TryParse(DynamicData?.stringData.BoyChildrenNum.ToString(), out int BoyChildrenNum))
                {
                    tjlOccQuestionnaire.BoyChildrenNum = DynamicData?.stringData.BoyChildrenNum;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.BoyBrith?.ToString()) && DateTime.TryParse(DynamicData?.stringData.BoyBrith.ToString(), out DateTime boydt))
                {
                    tjlOccQuestionnaire.BoyBrith = DynamicData?.stringData.BoyBrith;
                }
              //  tjlOccQuestionnaire.grilChildrenNum = DynamicData?.stringData.grilChildrenNum;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.grilChildrenNum?.ToString()) 
                    && int.TryParse(DynamicData?.stringData.grilChildrenNum.ToString(), out int grilChildrenNum))
                {
                    tjlOccQuestionnaire.grilChildrenNum = DynamicData?.stringData.grilChildrenNum;
                }
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.grilBrith?.ToString()) && DateTime.TryParse(DynamicData?.stringData.grilBrith.ToString(), out DateTime grildt))
                {
                    tjlOccQuestionnaire.grilBrith = DynamicData?.stringData.grilBrith;
                }
                tjlOccQuestionnaire.ChildHealthy = DynamicData?.stringData.ChildHealthy;
                tjlOccQuestionnaire.LifeHistory = DynamicData?.stringData.LifeHistory;
                // tjlOccQuestionnaire.QuitYears = DynamicData?.stringData.QuitYears;
                if (!string.IsNullOrEmpty(DynamicData?.stringData?.QuitYears?.ToString()) && int.TryParse(DynamicData?.stringData.QuitYears.ToString(), out int QuitYears1))
                {
                    tjlOccQuestionnaire.QuitYears = DynamicData?.stringData.QuitYears;
                }

                if (DynamicData?.stringData.SignaTureImage !=null && DynamicData?.stringData.SignaTureImage != "")
                {
                    tjlOccQuestionnaire.SignaTureImage = DynamicData?.stringData.SignaTureImage;
                }
                tjlOccQuestionnaire.PastHistory = DynamicData?.stringData.PastHistory;
                var entity = _tjlOccQuestion.Insert(tjlOccQuestionnaire);
                //try
                //{ CurrentUnitOfWork.SaveChanges(); }
                //catch (Exception ex)
                //{
                //    throw;
                //}
                //插入子表数据
                //职业史

                if (!string.IsNullOrEmpty(DynamicData.ObjectZY?.ToString()))
                {
                    ObjectZY = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccupationHistoryDto>>(DynamicData.ObjectZY.ToString()) as List<OccupationHistoryDto>;
                    ObjectZY = ObjectZY.Where(p => p.WorkClient != null && p.WorkClient != "").ToList();
                    var LsitObjectZY = new List<TjlOccQuesCareerHistory>();
                    if (ObjectZY.Count==0)
                    {
                        var cusreg = _tjlCustomerReg.Get(entity.CustomerRegBMId.Value);
                        if (string.IsNullOrEmpty(cusreg.InjuryAge) || string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        {
                            if (string.IsNullOrEmpty(cusreg.InjuryAge))
                            { cusreg.InjuryAge = "0"; }
                            if (string.IsNullOrEmpty(cusreg.TotalWorkAge))
                            { cusreg.TotalWorkAge = "0"; }
                            _tjlCustomerReg.Update(cusreg);
                        }
                    }
                    ObjectZY.ForEach(x =>
                    {

                        var tbmstand = x.MapTo<TjlOccQuesCareerHistory>();
                        //var queWHYS = tbmstand.OccHisHazardFactors;
                        //var queFHCS = tbmstand.OccHisProtectives;
                        tbmstand.OccHisHazardFactors = null;
                        tbmstand.OccHisProtectives = null;

                        tbmstand.Id = Guid.NewGuid();
                        tbmstand.OccQuestionnaireBMId = entity.Id;
                        if (x.StarTime.HasValue && x.StarTime >= DateTime.Parse("1880-1-1"))
                        {
                            tbmstand.StarTime = x.StarTime;
                        }
                        if (x.EndTime.HasValue && x.EndTime >= DateTime.Parse("1880-1-1"))
                        {
                            tbmstand.EndTime = x.EndTime;
                        }
                        tbmstand.WorkClient = x.WorkClient;
                        tbmstand.WorkName = x.WorkName;
                        tbmstand.WorkType = x.WorkType;
                        tbmstand.WorkYears = x.WorkYears;
                        //tbmstand.OccHisHazardFactors = null;
                        //tbmstand.OccHisHazardFactors = new List<TjlOccQuesHisHazardFactors>();
                        //tbmstand.OccHisProtectives = null;
                        //tbmstand.OccHisProtectives = new List<TjlOccQuesHisProtective>();
                        LsitObjectZY.Add(tbmstand);
                        _tjlOccQuestionCarrer.Insert(tbmstand);
                        //修改体检工龄
                        //if (tbmstand.WorkYears.HasValue && entity.CustomerRegBMId.HasValue)
                        //{
                        //    var cusreg = _tjlCustomerReg.Get(entity.CustomerRegBMId.Value);
                        //    if (string.IsNullOrEmpty(cusreg.InjuryAge) || string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        //    {
                        //        //if (string.IsNullOrEmpty(cusreg.InjuryAge))
                        //        //{ cusreg.InjuryAge = tbmstand.WorkYears.ToString(); }
                        //        //if (string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        //        //{ cusreg.TotalWorkAge = tbmstand.WorkYears.ToString(); }
                        //        //if (string.IsNullOrEmpty(cusreg.InjuryAge))
                        //        //{
                        //        //接害工龄
                        //        cusreg.InjuryAge = tbmstand.WorkYears?.ToString();
                        //        //}
                        //        //if (string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        //        //{
                        //        //总工龄
                        //        cusreg.TotalWorkAge = tbmstand.WorkYears?.ToString();
                        //        //}
                        //        //if (string.IsNullOrEmpty(cusreg.TypeWork))
                        //        //{
                        //        //工种
                        //        cusreg.TypeWork = tbmstand.WorkType?.ToString();
                        //        //}
                        //        //if (string.IsNullOrEmpty(cusreg.WorkName))
                        //        //{
                        //        //车间
                        //        cusreg.WorkName = tbmstand.WorkName?.ToString();
                        //        //}
                        //        _tjlCustomerReg.Update(cusreg);
                        //    }
                        //}
                        
                        if (!string.IsNullOrEmpty(tbmstand.HazardFactorIds))
                        {
                            var riskIds = tbmstand.HazardFactorIds.Split(',').ToList();
                            foreach (var occf in riskIds)
                            {
                                if (!string.IsNullOrEmpty(occf))
                                {
                                    var riskId = Guid.Parse(occf);
                                    var tbmris = _TbmOccHazardFactor.GetAll().FirstOrDefault(p => p.Id == riskId);
                                    if (tbmris != null)
                                    {
                                        TjlOccQuesHisHazardFactors tbmoccf = new TjlOccQuesHisHazardFactors();
                                        tbmoccf.CASBM = tbmris.CASBM;
                                        tbmoccf.Text = tbmris.Text;
                                        tbmoccf.TypeName = tbmris.Parent?.Text;
                                        tbmoccf.Id = Guid.NewGuid();
                                        tbmoccf.OccCareerHistoryBMId = tbmstand.Id;
                                        _tjlOccQuestionHisHazardFactors.Insert(tbmoccf);
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(tbmstand.ProtectiveIds))
                        {
                            var protctIDs = tbmstand.ProtectiveIds.Split(',').ToList();
                            foreach (var occf in protctIDs)
                            {
                                if (!string.IsNullOrEmpty(occf))
                                {
                                    var proctId = Guid.Parse(occf);
                                    var occ = _TbmOccDictionary.GetAll().FirstOrDefault(
                                        p => p.Type == "Protect" && p.Id == proctId);
                                    if (occ != null)
                                    {
                                        TjlOccQuesHisProtective tbmoccf = new TjlOccQuesHisProtective();
                                        tbmoccf.BM = occ.code;
                                        tbmoccf.Text = occ.Text;
                                        tbmoccf.Id = Guid.NewGuid();
                                        tbmoccf.OccCareerHistoryBMId = tbmstand.Id;
                                        _tjlOccQuestionHisProtective.Insert(tbmoccf);
                                    }
                                }
                            }
                        }
                    });
                    #region 计算工龄
                    if (ObjectZY.Count > 0)
                    {
                        //修改体检工龄
                        #region 工龄计算

                        #endregion

                        #region 按接害
                       
                        var ZGLList = ObjectZY.Select(p => p.EndTime.Value - p.StarTime.Value).ToList();


                        var JHGLList = ObjectZY.Where(p => p.HazardFactorIds != null && p.HazardFactorIds != "")
                            .Select(p => p.EndTime.Value - p.StarTime.Value).ToList();

                        //var maxJHGL = ObjectZY.Where(p => p.HazardFactorIds != null && p.HazardFactorIds != "").Max(p => p.EndTime);

                        if (ZGLList.Count > 0 && JHGLList.Count > 0)
                        {
                            var cusreg = _tjlCustomerReg.Get(entity.CustomerRegBMId.Value);
                            if (!cusreg.PostState.Contains("上岗"))
                            {
                                //接害工龄
                                TimeSpan ts = new TimeSpan();
                            foreach (var tn in JHGLList)
                            {
                                ts = ts + tn;
                            }

                            int yes = Convert.ToInt32(ts.TotalDays) / 365;
                            int moth = Convert.ToInt32(ts.TotalDays) % 365 / 30;
                            //一个月30天不是太准确
                            try
                            {
                                if (ObjectZY.Count == 1)
                                {
                                    var mm = ObjectZY.FirstOrDefault().StrWorkYears.Replace("年", "|").Replace("月", "|").
                                        Replace("日", "").Split('|');
                                    moth = int.Parse(mm[1]);
                                }
                                if (moth >= 12)
                                {
                                    moth = 11;
                                }
                            }
                            catch (Exception)
                            {

                            }
                            if (yes == 0)
                            {
                                cusreg.InjuryAge = moth.ToString();
                            }
                            else
                            { cusreg.InjuryAge = yes + "." + moth; }
                            //总工龄
                            TimeSpan ts1 = new TimeSpan();
                            foreach (var tn in ZGLList)
                            {
                                ts1 = ts1 + tn;
                            }
                            int yes1 = Convert.ToInt32(ts1.TotalDays) / 365;
                            int moth1 = Convert.ToInt32(ts1.TotalDays) % 365 / 30;
                            //一个月30天不是太准确
                            try
                            {
                                if (ObjectZY.Count == 1)
                                {
                                    var mm = ObjectZY.FirstOrDefault().StrWorkYears.Replace("年", "|").Replace("月", "|").
                                        Replace("日", "").Split('|');
                                    moth1 = int.Parse(mm[1]);
                                }
                                if (moth1 >= 12)
                                {
                                    moth1 = 11;
                                }
                            }
                            catch (Exception)
                            {

                            }
                            if (yes1 == 0)
                            {
                                cusreg.TotalWorkAge = moth1.ToString();
                            }
                            else
                            {
                                cusreg.TotalWorkAge = yes1 + "." + moth1;
                            }

                            var WorkType = ObjectZY.Where(p => p.WorkType != null && p.WorkType != "" &&
                            p.HazardFactorIds != null && p.HazardFactorIds != "").
                                OrderByDescending(p => p.StarTime).FirstOrDefault();
                            if (WorkType != null)
                            {
                                cusreg.TypeWork = WorkType.WorkType?.ToString();
                            }
                            var WorkName = ObjectZY.Where(p => p.WorkName != null && p.WorkName != "" &&
                            p.HazardFactorIds != null && p.HazardFactorIds != "").
                               OrderByDescending(p => p.StarTime).FirstOrDefault();
                            if (WorkName != null)
                            {
                                cusreg.WorkName = WorkName.WorkName?.ToString();
                            }
                            if (yes > 0)
                            {
                                cusreg.InjuryAgeUnit = "年";
                            }
                            else
                            { cusreg.InjuryAgeUnit = "月"; }

                            if (yes1 > 0)
                            {
                                cusreg.WorkAgeUnit = "年";
                            }
                            else
                            { cusreg.WorkAgeUnit = "月"; }
                            
                                _tjlCustomerReg.Update(cusreg);
                            }

                        }
                        #endregion
                    }
                    #endregion
                    entity.OccCareerHistory = LsitObjectZY;
                }
                else
                {

                    var cusreg = _tjlCustomerReg.Get(entity.CustomerRegBMId.Value);
                    if (string.IsNullOrEmpty(cusreg.InjuryAge) || string.IsNullOrEmpty(cusreg.TotalWorkAge))
                    {
                        if (string.IsNullOrEmpty(cusreg.InjuryAge))
                        { cusreg.InjuryAge = "0"; }
                        if (string.IsNullOrEmpty(cusreg.TotalWorkAge))
                        { cusreg.TotalWorkAge = "0"; }
                        _tjlCustomerReg.Update(cusreg);
                    }                      
                     
                }
                //既往史
                if (!string.IsNullOrEmpty(DynamicData.ObjectJw?.ToString()))
                {
                    ObjectJw = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesPastHistoryDto>>(DynamicData.ObjectJw.ToString()) as List<OccQuesPastHistoryDto>;
                    ObjectJw = ObjectJw.Where(p => p.IllName != null && p.IllName != "").ToList();
                    var LsitObjectJw = new List<TjlOccQuesPastHistory>();
                    ObjectJw.ForEach(x =>
                    {
                        var tbmstand = x.MapTo<TjlOccQuesPastHistory>();
                        tbmstand.Id = Guid.NewGuid();
                        tbmstand.OccQuestionnaireBMId = entity.Id;
                        tbmstand.IllName = x.IllName;
                        tbmstand.DiagnTime = x.DiagnTime;
                        tbmstand.DiagnosisClient = x.DiagnosisClient;
                        tbmstand.Treatment = x.Treatment;
                        tbmstand.Iscured = x.Iscured;
                        tbmstand.DiagnosticCode = x.DiagnosticCode;
                        LsitObjectJw.Add(tbmstand);
                        _tjlOccQuestionPast.Insert(tbmstand);
                    });
                    entity.OccPastHistory = LsitObjectJw;
                }
                #region 放射职业史
                //判断既往史
                var LsitObjectRadioactive = new List<TjlOccQuesRadioactiveCareerHistory>();
                if (DynamicData.ObjectRadioactiveCareer != null && DynamicData.ObjectRadioactiveCareer.Count > 0)
                {
                    ObjectRadioa = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesRadioactiveCareerHistoryDto>>(DynamicData.ObjectRadioactiveCareer.ToString()) as List<OccQuesRadioactiveCareerHistoryDto>;
                    ObjectRadioa = ObjectRadioa.Where(p => p.WorkClient != null && p.WorkClient != "").ToList();
                    //删除没有的既往史
                    var delet = _TjlOccQuesRadioactiveCareerHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zw in ObjectRadioa)
                    {
                       
                        //新增既往史
                        zw.Id = Guid.NewGuid();
                        var itemGroupEntity = zw.MapTo<TjlOccQuesRadioactiveCareerHistory>();
                        itemGroupEntity.OccQuestionnaireBMId = tjlOccQuestionnaire.Id;
                        itemGroupEntity.TbmOccDictionarys = new List<TbmOccDictionary>();
                        if (!string.IsNullOrEmpty(itemGroupEntity.TbmOccDictionaryIDs))
                        {
                            var diclist = itemGroupEntity.TbmOccDictionaryIDs.Split(',').ToList();
                            foreach (var dic in diclist)
                            {
                                var guidid = Guid.Parse(dic);
                                var dicint = _TbmOccDictionary.GetAll().FirstOrDefault(p => p.Id == guidid);
                                if (dicint != null)
                                {
                                    itemGroupEntity.TbmOccDictionarys.Add(dicint);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(itemGroupEntity.RadiationIds))
                        {
                            itemGroupEntity.Radiations = new List<TbmRadiation>();
                            var diclist = itemGroupEntity.RadiationIds.Split(',').ToList();
                            foreach (var dic in diclist)
                            {
                                var guidid = Guid.Parse(dic);
                                var dicint = _TbmRadiation.GetAll().FirstOrDefault(p => p.Id == guidid);
                                if (dicint != null)
                                {
                                    itemGroupEntity.Radiations.Add(dicint);
                                }
                            }
                        }
                        LsitObjectRadioactive.Add(itemGroupEntity);
                        _TjlOccQuesRadioactiveCareerHistory.Insert(itemGroupEntity);

                    }
                    entity.RadioactiveCareerHistory = LsitObjectRadioactive;
                }
                //没有既往史都删掉
                else
                {
                    var Past = _TjlOccQuesRadioactiveCareerHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }
                #endregion
                #region 婚姻史
                //判断婚姻史
                var LsitObjectMerr = new List<TjlOccQuesMerriyHistory>();
                if (DynamicData.ObjectMerr != null && DynamicData.ObjectMerr.Count > 0)
                {
                    //删除没有的婚姻史
                    ObjectMerr = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesMerriyHistoryDto>>(DynamicData.ObjectMerr.ToString()) as List<OccQuesMerriyHistoryDto>;
                     ObjectMerr = ObjectMerr.Where(p => p.Radioactive != null && p.Radioactive != "").ToList();
                    var delet = _TjlOccQuesMerriyHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                    foreach (var zw in ObjectMerr)
                    {
                      
                        //新增婚姻史
                        zw.Id = Guid.NewGuid();
                        var itemGroupEntity = zw.MapTo<TjlOccQuesMerriyHistory>();
                        itemGroupEntity.OccQuestionnaireBMId = tjlOccQuestionnaire.Id;
                        LsitObjectMerr.Add(itemGroupEntity);
                        _TjlOccQuesMerriyHistory.Insert(itemGroupEntity);

                       
                    }
                    entity.OccQuesMerriyHistory = LsitObjectMerr;
                }
                //没有既往史都删掉
                else
                {
                    var Past = _TjlOccQuesMerriyHistory.GetAll().Where(o => o.OccQuestionnaireBMId == quesid).Delete();
                }
                #endregion
                //家族史
                if (!string.IsNullOrEmpty(DynamicData.ObjectJz?.ToString()))
                {
                    ObjectJz = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesFamilyHistoryDto>>(DynamicData.ObjectJz.ToString()) as List<OccQuesFamilyHistoryDto>;
                    ObjectJz = ObjectJz.Where(p => p.IllName != null && p.IllName != "").ToList();
                    var LsitObjectJz = new List<TjlOccQuesFamilyHistory>();
                    ObjectJz.ForEach(x =>
                    {
                        var tbmstand = x.MapTo<TjlOccQuesFamilyHistory>();
                        tbmstand.Id = Guid.NewGuid();
                        tbmstand.OccQuestionnaireBMId = entity.Id;
                        tbmstand.IllName = x.IllName;
                        tbmstand.relatives = x.relatives;
                        LsitObjectJz.Add(tbmstand);
                        _tjlOccQuestionFamily.Insert(tbmstand);
                    });
                    entity.OccFamilyHistory = LsitObjectJz;
                }
                //近期症状
                if (!string.IsNullOrEmpty(DynamicData.ObjectZZ?.ToString()))
                {
                    ObjectZZ = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccQuesSymptomDto>>(DynamicData.ObjectZZ.ToString()) as List<OccQuesSymptomDto>;
                    ObjectZZ = ObjectZZ.Where(p => p.Name != null && p.Name != "").ToList();
                    var LsitObjectZZ = new List<TjlOccQuesSymptom>();
                    ObjectZZ.ForEach(x =>
                    {
                        var tbmstand = x.MapTo<TjlOccQuesSymptom>();
                        tbmstand.Id = Guid.NewGuid();
                        tbmstand.OccQuestionnaireBMId = entity.Id;
                        tbmstand.Name = x.Name;
                        tbmstand.Type = x.Type;
                        tbmstand.Degree = x.Degree;
                        LsitObjectZZ.Add(tbmstand);
                        _tjlOccQuestionSymptom.Insert(tbmstand);
                    });
                }
                if (!string.IsNullOrEmpty(DynamicData.Cusid?.ToString()))
                {
                    var cusreg = _tjlCustomerReg.Get((Guid)DynamicData.Cusid);
                    if (cusreg != null)
                    {
                        cusreg.QuestionState = 1;
                        cusreg.QuestionTime = System.DateTime.Now;
                    }

                }
                CurrentUnitOfWork.SaveChanges();
                return new OccQuestionnaireDto
                {
                    Id = entity.Id,
                    StarAge = entity.StarAge,
                    Cycle = entity.Cycle,
                    EndAge = entity.EndAge,
                    ChildrenNum = entity.ChildrenNum,
                    IsAbortion = entity.IsAbortion,
                    AbortionCount = entity.AbortionCount,
                    IsPrematureDelivery = entity.IsPrematureDelivery,
                    IsStillbirth = entity.IsStillbirth,
                    IsSmoke = entity.IsSmoke,
                    SmokeCount = entity.SmokeCount,
                    SmokeYears = entity.SmokeYears,
                    IsDrink = entity.IsDrink,
                    DrinkCount = entity.DrinkCount,
                    DrinkYears = entity.DrinkYears,
                    AskAdvice = entity.AskAdvice,
                    period = entity.period,
                    malFormation = entity.malFormation,
                    Abnormal = entity.Abnormal,
                    CustomerRegBMId = entity.CustomerRegBMId,
                    OccCareerHistory = ObjectZY,
                    OccPastHistory = ObjectJw,
                    OccFamilyHistory = ObjectJz,
                    OccQuesSymptom = ObjectZZ,
                          PregnancyCount = entity.PregnancyCount,
                LiveBirth = entity.LiveBirth,
                Teratogenesis = entity.Teratogenesis,
                MultipleBirths = entity.MultipleBirths,
                EctopicPregnancy = entity.EctopicPregnancy,
                Infertility = entity.Infertility,
                BoyChildrenNum = entity.BoyChildrenNum,
                BoyBrith = entity.BoyBrith,
                grilChildrenNum = entity.grilChildrenNum,
                grilBrith = entity.grilBrith,
                ChildHealthy = entity.ChildHealthy,
                LifeHistory = entity.LifeHistory,
                QuitYears = entity.QuitYears

            };
            }
            
            
        }

        //_tjlOccQuestionCarrer
        //既往史  _tjlOccQuestionPast
        //家族史  _tjlOccQuestionFamily
        //近期症状 _tjlOccQuestionSymptom
    }
}
