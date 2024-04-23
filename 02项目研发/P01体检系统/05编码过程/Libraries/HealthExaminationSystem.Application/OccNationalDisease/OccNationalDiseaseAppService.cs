using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease
{
    [AbpAuthorize]
    public class OccNationalDiseaseAppService : MyProjectAppServiceBase, IOccNationalDiseaseAppService
    {
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg;
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlClientReg, Guid> _TjlClientReg;
        private readonly IRepository<TjlClientInfo, Guid> _TjlClientInfo;
        private readonly IRepository<TbmBasicDictionary, Guid> _TbmBasicDictionary;
        private readonly IRepository<TjlCustomerRegRiskFactors, Guid> _TjlCustomerRegRiskFactors;
        private readonly IRepository<TbmOccDictionary, Guid> _TbmOccDictionary;
        private readonly IRepository<TjlCustomerSummarize, Guid> _TjlCustomerSummarize;
        private readonly IRepository<AdministrativeDivision, Guid> _AdministrativeDivision;
        private readonly IRepository<TjlCustomerRegItem, Guid> _TjlCustomerRegItem;
        private readonly IRepository<TbmCountrySet, Guid> _TbmCountrySet;
        public OccNationalDiseaseAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
            IRepository<TjlClientReg, Guid> TjlClientReg,
            IRepository<TbmBasicDictionary, Guid> TbmBasicDictionary,
            IRepository<TjlCustomerRegRiskFactors, Guid> TjlCustomerRegRiskFactors,
            IRepository<TbmOccDictionary, Guid> TbmOccDictionary,
            IRepository<TjlCustomerSummarize, Guid> TjlCustomerSummarize,
            IRepository<AdministrativeDivision, Guid> AdministrativeDivision,
            IRepository<TjlCustomerRegItem, Guid> TjlCustomerRegItem,
            IRepository<TbmCountrySet, Guid> TbmCountrySet,
            IRepository<TjlClientInfo, Guid> TjlClientInfo)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCustomerReg = TjlCustomerReg;
            _TjlClientReg = TjlClientReg;
            _TbmBasicDictionary = TbmBasicDictionary;
            _TjlCustomerRegRiskFactors = TjlCustomerRegRiskFactors;
            _TbmOccDictionary = TbmOccDictionary;
            _TjlCustomerSummarize = TjlCustomerSummarize;
            _AdministrativeDivision = AdministrativeDivision;
            _TjlCustomerRegItem = TjlCustomerRegItem;
            _TbmCountrySet = TbmCountrySet;
            _TjlClientInfo = TjlClientInfo;
        }
        /// <summary>
        /// 用人单位信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataNodeDto GetEnterpriseInfo(InSearchDto input)
        {
            DataNodeDto dataNodeDto = new DataNodeDto();
            dataNodeDto.EventBody = new EventBodyDto();
                  var que = _TjlCustomerReg.GetAll().Where(p => p.RiskS != null && p.PostState != "" && p.SummSate == (int)SummSate.Audited);
            if (input.StarDate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.EndDate);
            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(p => p.ClientRegId == input.ClientRegId);
            }
            if (!string.IsNullOrEmpty(input.CustomerBM))
            {
                que = que.Where(p => p.CustomerBM == input.CustomerBM);
            }
            var clienIDlist = que.Where(p=>p.ClientInfoId !=null).Select(p => p.ClientInfoId).Distinct().ToList();
            dataNodeDto.EventBody.ENTERPRISE_INFO = new List<ENTERPRISE_INFODto>();
            //单位信息
            string InstitutionName = "";
            string InstitutionBM = "";
            var AreaCode = "";//机构代码
            string InstitutionUser = "张三";
            string InstitutionUserTel = "18617517326";
            //var Institution = _TbmBasicDictionary.GetAll().Where(o => o.Type == "Institution").ToList();
            //if (Institution != null && Institution.Count > 0)
            //{
            //    InstitutionName = Institution.FirstOrDefault(o => o.Value == 1)?.Remarks;
            //    InstitutionBM = Institution.FirstOrDefault(o => o.Value == 2)?.Remarks;
            //    AreaCode = Institution.FirstOrDefault(o => o.Value == 5)?.Remarks;
            //    InstitutionUser = Institution.FirstOrDefault(o => o.Value == 3)?.Remarks;
            //    InstitutionUserTel = Institution.FirstOrDefault(o => o.Value == 4)?.Remarks;
            //}
            var countryset = _TbmCountrySet.GetAll().FirstOrDefault();
            if (countryset != null)
            {
                System.Random aa = new Random();
                int j = aa.Next(10000, 99999);

                InstitutionName = countryset.ReportUnit;
                InstitutionBM = countryset.ReportOrgCode;
                AreaCode = countryset.ReportZoneCode;
                InstitutionUser = countryset.WritePeson;
                InstitutionUserTel = countryset.WritePesonTel;
                dataNodeDto.Header = new HeaderDto();
                dataNodeDto.Header.BusinessActivityIdentification = "NEWOMAR";
                dataNodeDto.Header.DocumentId = AreaCode + "-ENTERPRISE_INFO" + "-" +
                    System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + j;//机构编码-业务类别代码-当前时间（yyyyMMddHHmmssSSS）-5 位随机码
                dataNodeDto.Header.License = countryset.License;
                dataNodeDto.Header.OperateType = "Add";
                dataNodeDto.Header.ReportOrgCode = countryset.ReportOrgCode;
                dataNodeDto.Header.ReportZoneCode = countryset.ReportZoneCode;
            }
            foreach (var clientId in clienIDlist)
            {
                ENTERPRISE_INFODto eNTERPRISE_INFODto = new ENTERPRISE_INFODto();
                //var clientReg = _TjlClientReg.Get(clientregId.Value);
                var clientInfo = _TjlClientInfo.Get(clientId.Value);
                if (!string.IsNullOrEmpty( clientInfo.StoreAdressQ) )
                {
                    eNTERPRISE_INFODto.ADDRESS_CODE = clientInfo.StoreAdressQ.PadRight(9, '0').ToString();
                }
                else
                {
                    eNTERPRISE_INFODto.ADDRESS_CODE = "";
                }
                eNTERPRISE_INFODto.ADDRESS_DETAIL = clientInfo.Address;
                eNTERPRISE_INFODto.ADDRESS_ZIP_CODE = clientInfo.PostCode??"";
                eNTERPRISE_INFODto.AREA_CODE = AreaCode;
                eNTERPRISE_INFODto.BUSINESS_SCALE_CODE = clientInfo.Scale.ToString();
                //企业规模                              
                if (clientInfo.Scale.HasValue)
                {
                    var ScaleType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ScaleType);
                    var Scale = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == ScaleType && p.Value == clientInfo.Scale);
                    if (Scale != null && !string.IsNullOrEmpty(Scale.Remarks))
                    {
                        eNTERPRISE_INFODto.BUSINESS_SCALE_CODE = Scale.Remarks;
                    }
                }
                if (!string.IsNullOrEmpty(clientInfo.Telephone))
                { eNTERPRISE_INFODto.CONTACT_TELPHONE = clientInfo.Telephone.ToString(); }
                eNTERPRISE_INFODto.id = clientInfo.ClientBM.ToString();
                eNTERPRISE_INFODto.CREDIT_CODE = clientInfo.SocialCredit;
                eNTERPRISE_INFODto.ECONOMIC_TYPE_CODE = clientInfo.EconomicType.ToString();
                // 经济类型                 
                if (clientInfo.EconomicType.HasValue)
                {
                    var EconomicsTypes = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.EconomicsType);
                    var EconomicType = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == EconomicsTypes && p.Value == clientInfo.EconomicType);
                    if (EconomicType != null && !string.IsNullOrEmpty(EconomicType.Remarks))
                    {
                        eNTERPRISE_INFODto.ECONOMIC_TYPE_CODE = EconomicType.Remarks;
                    }
                }
                eNTERPRISE_INFODto.ENTERPRISE_CONTACT = clientInfo.LinkMan;
                eNTERPRISE_INFODto.ENTERPRISE_NAME = clientInfo.ClientName;
                eNTERPRISE_INFODto.INDUSTRY_CATEGORY_CODE = clientInfo.Clientlndutry;
                if (!string.IsNullOrEmpty(clientInfo.Clientlndutry) && int.TryParse(clientInfo.Clientlndutry,out int hybm))
                {
                    var clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
                    var Clientlndutry = _TbmBasicDictionary.GetAll().FirstOrDefault(p=>p.Type== clientlndutry && p.Value== hybm);
                    if (Clientlndutry != null && !string.IsNullOrEmpty(Clientlndutry.Remarks))
                    {
                        eNTERPRISE_INFODto.INDUSTRY_CATEGORY_CODE = Clientlndutry.Remarks;
                    }
                }
                eNTERPRISE_INFODto.ISSUBSIDIARY = "0";
                eNTERPRISE_INFODto.ORG_CODE = InstitutionBM;
                eNTERPRISE_INFODto.REPORT_DATE = System.DateTime.Now.ToString();
                eNTERPRISE_INFODto.REPORT_PERSON = InstitutionUser;
                eNTERPRISE_INFODto.REPORT_PERSON_TEL = InstitutionUserTel;
                eNTERPRISE_INFODto.REPORT_UNIT = InstitutionName;
                eNTERPRISE_INFODto.TWOLEVELCODE = "";
                eNTERPRISE_INFODto.WRITE_DATE = clientInfo.CreationTime.ToString();
                eNTERPRISE_INFODto.WRITE_PERSON = InstitutionUser;
                eNTERPRISE_INFODto.WRITE_PERSON_TEL = InstitutionUserTel;
                eNTERPRISE_INFODto.WRITE_UNIT = InstitutionName;

                eNTERPRISE_INFODto.AUDIT_INFO = new AUDIT_INFODto();
                eNTERPRISE_INFODto.AUDIT_INFO.AUDITDATE = "";
                eNTERPRISE_INFODto.AUDIT_INFO.AUDITINFO = "";
                eNTERPRISE_INFODto.AUDIT_INFO.AUDITSTATUS = "01";
                eNTERPRISE_INFODto.AUDIT_INFO.AUDIT_ORG = "";
                eNTERPRISE_INFODto.AUDIT_INFO.AUDITOR_NAME = "";
                dataNodeDto.EventBody.ENTERPRISE_INFO.Add(eNTERPRISE_INFODto);
            }

            return dataNodeDto;
        }
        /// <summary>
        /// 职业健康档案
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public XMLDataNodeDto GetExamRecord(InSearchDto input)
        {
            XMLDataNodeDto xmlEventBodyDto = new XMLDataNodeDto();
            xmlEventBodyDto.EventBody = new XmlEventBodyDto();

            xmlEventBodyDto.EventBody.HEALTH_EXAM_RECORD = new List<HEALTH_EXAM_RECORDDto>();

            var countryset = _TbmCountrySet.GetAll().FirstOrDefault();
            //单位信息
            string InstitutionName = "";
            string InstitutionBM = "";
            var AreaCode = "";//地区代码
            string InstitutionUser = "张三";
            string InstitutionUserTel = "18617517326";
            //var Institution = _TbmBasicDictionary.GetAll().Where(o => o.Type == "Institution").ToList();
            if (countryset != null )
            {
                System.Random aa = new Random();
                int j = aa.Next(10000, 99999);
               
                InstitutionName = countryset.ReportUnit;
                InstitutionBM = countryset.ReportOrgCode;
                AreaCode = countryset.ReportZoneCode;
                InstitutionUser = countryset.WritePeson;
                InstitutionUserTel = countryset.WritePesonTel;
                xmlEventBodyDto.Header = new HeaderDto();
                xmlEventBodyDto.Header.BusinessActivityIdentification = "NEWOMAR";
                xmlEventBodyDto.Header.DocumentId = AreaCode + "-HEALTH_EXAM_RECORD" + "-" +
                    System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + j;//机构编码-业务类别代码-当前时间（yyyyMMddHHmmssSSS）-5 位随机码
                xmlEventBodyDto.Header.License = countryset.License;
                xmlEventBodyDto.Header.OperateType = "Add";
                xmlEventBodyDto.Header.ReportOrgCode = countryset.ReportOrgCode;
                xmlEventBodyDto.Header.ReportZoneCode = countryset.ReportZoneCode;
            }
            
            var que = _TjlCustomerReg.GetAll().Where(p => p.RiskS != null && p.PostState != "" && p.SummSate == (int)SummSate.Audited);
            if (input.StarDate.HasValue)
            {
                que = que.Where(p => p.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(p => p.LoginDate < input.EndDate);
            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(p => p.ClientRegId == input.ClientRegId);
            }
            if(!string.IsNullOrEmpty( input.CustomerBM))
            {
                que = que.Where(p => p.CustomerBM == input.CustomerBM);
            }
            var cuslist = que.ToList();
            //体检类型
            //职业健康工种
            var TypeWork = _TbmOccDictionary.GetAll().Where(o => o.Type == "WorkType").Select(o => new { o.code, o.Text }).ToList();
            var Checktype = _TbmOccDictionary.GetAll().Where(o => o.Type == "Checktype").Select(o => new { o.code, o.Text }).ToList();
            var occSumlist = _TbmOccDictionary.GetAll().Where(o => o.Type == "Conclusion" && o.IsDeleted == false).Select(o => new { o.code, o.Text }).ToList();
            

            foreach (var cus in cuslist)
            {if (cus.ClientInfo == null)
                {
                    continue;
                }
                var cusinfo = cus.Customer;
                var cusSum = _TjlCustomerSummarize.GetAll().FirstOrDefault(p=>p.CustomerRegID==cus.Id);
                var occsum = _TjlOccCustomerSum.GetAll().FirstOrDefault(p=>p.CustomerRegBMId==cus.Id);
                var ClientInfo = cus.ClientInfo;
                var risk = cus.OccHazardFactors;
                var risIds = risk.Select(p=>p.OrderNum).ToList();
                var cusItemlist = _TjlCustomerRegItem.GetAll().Where(p=>p.CustomerRegId== cus.Id && p.ItemBM.StandardCode !="" 
                && p.DepartmentBM !=null && p.ItemGroupBM!=null && p.ItemBM !=null).
                    OrderBy(p=>p.DepartmentBM.OrderNum).ThenBy(p=>p.ItemGroupBM.OrderNum).ThenBy(p=>p.ItemBM.OrderNum).ToList();
                string riskid = string.Join(",", risIds);
              
                HEALTH_EXAM_RECORDDto hEALTH_EXAM_RECORDDto = new HEALTH_EXAM_RECORDDto();
                hEALTH_EXAM_RECORDDto.ACTOR_CODE = riskid;
                hEALTH_EXAM_RECORDDto.AREA_CODE = AreaCode;

                hEALTH_EXAM_RECORDDto.AUDIT_INFO = new AUDIT_INFODto();
                hEALTH_EXAM_RECORDDto.AUDIT_INFO.AUDITDATE = "";
                hEALTH_EXAM_RECORDDto.AUDIT_INFO.AUDITINFO = "";
                hEALTH_EXAM_RECORDDto.AUDIT_INFO.AUDITOR_NAME = "";
                hEALTH_EXAM_RECORDDto.AUDIT_INFO.AUDITSTATUS = "01";
                hEALTH_EXAM_RECORDDto.AUDIT_INFO.AUDIT_ORG = "";                

                hEALTH_EXAM_RECORDDto.CONTACT_FACTOR_CODE = riskid;
                hEALTH_EXAM_RECORDDto.CONTACT_FACTOR_OTHER = "";
              

                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO = new ENTERPRISE_INFO1Dto();
                if (!string.IsNullOrEmpty(cusinfo?.StoreAdressQ))
                {
                    hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_CODE = cusinfo?.StoreAdressQ.PadRight(9, '0').ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(ClientInfo.StoreAdressQ))
                    {
                        hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_CODE = ClientInfo.StoreAdressQ.PadRight(9, '0').ToString();
                    }
                    else
                    {
                        hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_CODE = "";
                    }

                    //hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_CODE = "";
                }
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_DETAIL = ClientInfo?.Address??"";              
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_ZIP_CODE = ClientInfo?.PostCode??"";          
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.BUSINESS_SCALE_CODE = ClientInfo?.Scale.ToString();
                //企业规模                              
                if (ClientInfo.Scale.HasValue)
                {
                    var ScaleType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ScaleType);
                    var Scale = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == ScaleType && p.Value == ClientInfo.Scale);
                    if (Scale != null && !string.IsNullOrEmpty(Scale.Remarks))
                    {
                        hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.BUSINESS_SCALE_CODE = Scale.Remarks;
                    }
                }
                if (!string.IsNullOrEmpty(ClientInfo?.Telephone))
                {
                    hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.CONTACT_TELPHONE = ClientInfo?.Telephone.ToString();
                }
                else
                { hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.CONTACT_TELPHONE = ""; }
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.CREDIT_CODE = ClientInfo?.SocialCredit;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ECONOMIC_TYPE_CODE = ClientInfo?.EconomicType.ToString();
                // 经济类型                 
                if (ClientInfo.EconomicType.HasValue)
                {
                    var EconomicsTypes = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.EconomicsType);
                    var EconomicType = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == EconomicsTypes && p.Value == ClientInfo.EconomicType);
                    if (EconomicType != null && !string.IsNullOrEmpty(EconomicType.Remarks))
                    {
                        hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ECONOMIC_TYPE_CODE = EconomicType.Remarks;
                    }
                }
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ENTERPRISE_CONTACT = ClientInfo?.LinkMan;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ENTERPRISE_NAME = ClientInfo?.ClientName;

                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.INDUSTRY_CATEGORY_CODE = ClientInfo?.Clientlndutry;

                if (!string.IsNullOrEmpty(ClientInfo.Clientlndutry) && int.TryParse(ClientInfo.Clientlndutry, out int hybm))
                {
                    var clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
                    var Clientlndutry = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == clientlndutry && p.Value == hybm);
                    if (Clientlndutry != null && !string.IsNullOrEmpty(Clientlndutry.Remarks))
                    {
                        hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.INDUSTRY_CATEGORY_CODE = Clientlndutry.Remarks;
                    }
                }

                var Qname = "";
                if (ClientInfo != null)
                {
                    var StoreAdressP = _AdministrativeDivision.FirstOrDefault(p => p.Code == ClientInfo.StoreAdressP)?.Name;
                    var StoreAdressS = _AdministrativeDivision.FirstOrDefault(p => p.Code == ClientInfo.StoreAdressS)?.Name;
                     Qname = _AdministrativeDivision.FirstOrDefault(p => p.Code == ClientInfo.StoreAdressQ)?.Name;
                    Qname = StoreAdressP + StoreAdressS + Qname;
                }
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ALL_NAME = Qname;
                //用工单位
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER = new ENTERPRISE_INFO_EMPLOYERDto();
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.ADDRESS_CODE_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ADDRESS_CODE;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.ALL_NAME_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ALL_NAME;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.BUSINESS_SCALE_CODE_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.BUSINESS_SCALE_CODE;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.CREDIT_CODE_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.CREDIT_CODE;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.ECONOMIC_TYPE_CODE_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ECONOMIC_TYPE_CODE;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.ENTERPRISE_NAME_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.ENTERPRISE_NAME;
                hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO_EMPLOYER.INDUSTRY_CATEGORY_CODE_EMPLOYER = hEALTH_EXAM_RECORDDto.ENTERPRISE_INFO.INDUSTRY_CATEGORY_CODE;



                hEALTH_EXAM_RECORDDto.EXAM_CONCLUSION = new List<EXAM_CONCLUSIONDto>();
                var SumCode = occSumlist.FirstOrDefault(p => p.Text == occsum?.Conclusion)?.code?.ToString();              
                var allyears = 0;
                var allMonth = 0;
                var hazardyears = 0;
                var hazardmonth = 0;
                var TotalAge = cus.TotalWorkAge.Split('.');
                if (cus.WorkAgeUnit == "月")
                {
                    if (TotalAge.Count() > 0 && int.TryParse(TotalAge[0], out int year))
                    {
                        var years = year / 12;
                        var Moth = year % 12;
                        allyears = years;
                        allMonth = Moth;
                    }
                    else
                    {
                        allyears = 0;
                        allMonth = 0;
                    }
                }
                else //年计算
                {
                    if (TotalAge.Count() > 0 && int.TryParse(TotalAge[0], out int year))
                    {
                        allyears = year;
                        if (TotalAge.Count() > 1 && int.TryParse(TotalAge[1], out int mouth))
                        {
                            var moth = mouth * 1.2;//换算成月
                            var zmoth = int.Parse(Math.Floor(moth).ToString());
                            allMonth = zmoth;
                        }
                        else
                        { allMonth = 0; }
                    }
                    else
                    {
                        allyears = 0;
                        allMonth = 0;
                    }
                }
                var hazardAge = cus.InjuryAge.Split('.');
                if (cus.InjuryAgeUnit == "月")
                {
                    if (hazardAge.Count() > 0 && int.TryParse(hazardAge[0], out int year))
                    {
                        var years = year / 12;
                        var Moth = year % 12;
                        hazardyears = years;
                        hazardmonth = Moth;
                    }
                    else
                    {
                        hazardyears = 0;
                        hazardmonth = 0;
                    }
                }
                else //年计算
                {
                    if (hazardAge.Count() > 0 && int.TryParse(hazardAge[0], out int year))
                    {
                        hazardyears = year;
                        if (hazardAge.Count() > 1 && int.TryParse(hazardAge[1], out int mouth))
                        {
                            var moth = mouth * 1.2;//换算成月
                            var zmoth = int.Parse(Math.Floor(moth).ToString());
                            hazardmonth = zmoth;
                        }
                        else
                        { hazardmonth = 0; }
                    }
                    else
                    {
                        hazardyears = 0;
                        hazardmonth = 0;
                    }
                }

                foreach (var ri in risk)
                {
                    EXAM_CONCLUSIONDto eXAM_CONCLUSION  = new EXAM_CONCLUSIONDto();
                    eXAM_CONCLUSION.EXAM_CONCLUSION_CODE = SumCode;
                    eXAM_CONCLUSION.HARM_AGE_MONTH = hazardmonth.ToString();
                    eXAM_CONCLUSION.HARM_AGE_YEAR = hazardyears.ToString();
                    eXAM_CONCLUSION.HARM_START_DATE = cus.LoginDate.Value.AddYears(-hazardyears).AddMonths(-hazardmonth).ToString();
                    eXAM_CONCLUSION.ITAM_CODE = ri.OrderNum.ToString();
                    eXAM_CONCLUSION.ITAM_NAME = ri.Text;
                    eXAM_CONCLUSION.QTJB_NAME = "";
                    eXAM_CONCLUSION.YSZYB_CODE =string.Join(",", occsum.OccCustomerOccDiseases.Select(p=>p.OrderNum).ToList());
                    eXAM_CONCLUSION.ZYJJZ_NAME= string.Join(",", occsum.OccCustomerContraindications.Select(p => p.OrderNum).ToList());
                    hEALTH_EXAM_RECORDDto.EXAM_CONCLUSION.Add(eXAM_CONCLUSION);
                }
                if (hEALTH_EXAM_RECORDDto.EXAM_CONCLUSION.Count == 0)
                {
                    EXAM_CONCLUSIONDto eXAM_CONCLUSION = new EXAM_CONCLUSIONDto();
                    eXAM_CONCLUSION.EXAM_CONCLUSION_CODE = "";
                    eXAM_CONCLUSION.HARM_AGE_MONTH = "";
                    eXAM_CONCLUSION.HARM_AGE_YEAR = "";
                    eXAM_CONCLUSION.HARM_START_DATE = "";
                    eXAM_CONCLUSION.ITAM_CODE = "";
                    eXAM_CONCLUSION.ITAM_NAME = "";
                    eXAM_CONCLUSION.QTJB_NAME = "";
                    eXAM_CONCLUSION.YSZYB_CODE = "";
                    eXAM_CONCLUSION.ZYJJZ_NAME = "";
                    hEALTH_EXAM_RECORDDto.EXAM_CONCLUSION.Add(eXAM_CONCLUSION);
                }
                if (cus.LoginDate.HasValue)
                {
                    hEALTH_EXAM_RECORDDto.EXAM_DATE = cus.LoginDate.Value.ToString("yyyyMMdd");
                }
                else
                { hEALTH_EXAM_RECORDDto.EXAM_DATE = ""; }
             
                hEALTH_EXAM_RECORDDto.EXAM_ITEM_PNAME = new List<EXAM_ITEM_RESULTDto>();
                #region 检查项目集合节点
                //非胸片
                 var  ItemList1 = cusItemlist.Where(p => p.ItemCodeBM != "801002" && p.ItemCodeBM != "801004" && p.ItemCodeBM != "801005"
                && p.ItemCodeBM != "801021" && p.ItemCodeBM != "801028" && p.ItemCodeBM != "801031").Select(p => new EXAM_ITEM_RESULTDto
                {


                    EXAM_RESULT = p.Symbol == "P" ? (p.ItemDiagnosis != "" ? p.ItemDiagnosis : p.ItemResultChar) : p.ItemResultChar,                 
                    EXAM_ITEM_PNAME = p.ItemGroupBM.ItemGroupName,
                    EXAM_ITEM_NAME=p.ItemName,
                    EXAM_ITEM_CODE=p.ItemBM.StandardCode,
                    EXAM_RESULT_TYPE = p.ItemBM.moneyType== 1 ? "01" : p.ItemBM.moneyType == 2?"01": "02",
                    EXAM_ITEM_UNIT_CODE=p.Stand,
                    ABNORMAL = p.ProcessState== 1 ? "3" : (p.Symbol == "" ? "0" : (p.Symbol == "M" ? "0" : "1")),
                    REFERENCE_RANGE_MAX = p.ItemBM.moneyType == 1 ? p.Stand == null ? "无" : (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(p.Stand.IndexOf("－") + 1, (p.Stand.Length - p.Stand.IndexOf("－") - 1)) : "无") : "无",
                    REFERENCE_RANGE_MIN = p.ItemBM.moneyType == 1 ? p.Stand == null ? "无" : (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(0, p.Stand.IndexOf("－")) : "无") : "无",
                   
                    
                }).ToList();
                //胸片
                var ItemList2 = cusItemlist.Where(p => p.ItemCodeBM == "801002" || p.ItemCodeBM == "801004" || p.ItemCodeBM == "801005"
                      || p.ItemCodeBM == "801021" || p.ItemCodeBM == "801028" || p.ItemCodeBM == "801031").Select(p => new EXAM_ITEM_RESULTDto
                      {
                          EXAM_RESULT = p.Symbol == "P" ? (p.ItemDiagnosis != "" ? p.ItemDiagnosis : p.ItemResultChar) : p.ItemResultChar,
                          EXAM_ITEM_PNAME = p.ItemGroupBM.ItemGroupName,
                          EXAM_ITEM_NAME = p.ItemName,
                          EXAM_ITEM_CODE = p.ItemBM.StandardCode,
                          EXAM_RESULT_TYPE = p.ProcessState == 1 ? "3" : p.ItemBM.moneyType == 1 ? "01" : p.ItemBM.moneyType == 2 ? "01" : "02",
                          EXAM_ITEM_UNIT_CODE = p.Stand,
                          ABNORMAL = (p.Symbol == "" ? "0" : (p.Symbol == "M" ? "0" : p.ItemResultChar.Contains("肺尘样改变") ? "2" : "3")),
                          REFERENCE_RANGE_MAX = p.ItemBM.moneyType == 1 ? p.Stand == null ? "无" : (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(p.Stand.IndexOf("－") + 1, (p.Stand.Length - p.Stand.IndexOf("－") - 1)) : "无") : "无",
                          REFERENCE_RANGE_MIN = p.ItemBM.moneyType == 1 ? p.Stand == null ? "无" : (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(0, p.Stand.IndexOf("－")) : "无") : "无",

                      }).ToList();
               
                ItemList1.AddRange(ItemList2);
                var reItems = ItemList1.Where(p => p.EXAM_ITEM_CODE.Contains("|")).ToList();
                if (reItems.Count > 0)
                {
                    var copyreItems = reItems.ToList();
                    foreach (var item in copyreItems)
                    {
                        var IDs = item.EXAM_ITEM_CODE.Split('|');
                        foreach (var it in IDs)
                        {
                            EXAM_ITEM_RESULTDto EXAM_ITEM_RESULT = new EXAM_ITEM_RESULTDto();
                            EXAM_ITEM_RESULT.EXAM_RESULT = item.EXAM_RESULT;
                            EXAM_ITEM_RESULT.EXAM_ITEM_PNAME = item.EXAM_ITEM_PNAME;
                            EXAM_ITEM_RESULT.EXAM_ITEM_NAME = item.EXAM_ITEM_NAME;
                            EXAM_ITEM_RESULT.EXAM_ITEM_CODE =it;
                            EXAM_ITEM_RESULT.EXAM_RESULT_TYPE = item.EXAM_RESULT_TYPE;
                            EXAM_ITEM_RESULT.EXAM_ITEM_UNIT_CODE = item.EXAM_ITEM_UNIT_CODE;
                            EXAM_ITEM_RESULT.ABNORMAL = item.ABNORMAL;
                            EXAM_ITEM_RESULT.REFERENCE_RANGE_MAX = item.REFERENCE_RANGE_MAX;
                            EXAM_ITEM_RESULT.REFERENCE_RANGE_MIN = item.REFERENCE_RANGE_MIN;

                            if ((it == "801002" || it == "801004" || it == "801005"
                     || it == "801021" || it == "801028" || it == "801031") && EXAM_ITEM_RESULT.ABNORMAL != "1")
                            {
                                if (EXAM_ITEM_RESULT.EXAM_RESULT!=null  && EXAM_ITEM_RESULT.EXAM_RESULT.Contains("肺尘样改变"))
                                { EXAM_ITEM_RESULT.ABNORMAL = "1"; }
                                else
                                { EXAM_ITEM_RESULT.ABNORMAL = "2"; }

                            }
                            else
                            { EXAM_ITEM_RESULT.ABNORMAL = item.ABNORMAL; }

                            ItemList1.Add(EXAM_ITEM_RESULT);
                        }
                        ItemList1.Remove(item);
                    }

                }
                hEALTH_EXAM_RECORDDto.EXAM_ITEM_PNAME = ItemList1;
                if (ItemList1.Count == 0)
                {
                   var  EXAM_ITEM_RESULTDto = new EXAM_ITEM_RESULTDto();
                    EXAM_ITEM_RESULTDto.ABNORMAL = "";
                    EXAM_ITEM_RESULTDto.EXAM_ITEM_CODE = "";
                    EXAM_ITEM_RESULTDto.EXAM_ITEM_NAME = "";
                    EXAM_ITEM_RESULTDto.EXAM_ITEM_PNAME = "";
                    EXAM_ITEM_RESULTDto.EXAM_ITEM_UNIT_CODE = "";
                    EXAM_ITEM_RESULTDto.EXAM_RESULT = "";
                    EXAM_ITEM_RESULTDto.EXAM_RESULT_TYPE = "";
                    EXAM_ITEM_RESULTDto.REFERENCE_RANGE_MAX = "";
                    EXAM_ITEM_RESULTDto.REFERENCE_RANGE_MIN = "";
                   
                    hEALTH_EXAM_RECORDDto.EXAM_ITEM_PNAME.Add(EXAM_ITEM_RESULTDto);
                }
                #endregion


                hEALTH_EXAM_RECORDDto.EXAM_TYPE_CODE = Checktype.FirstOrDefault(p => p.Text == cus.PostState) == null ? "" :
                   Checktype.FirstOrDefault(p => p.Text == cus.PostState).code?.ToString();
                hEALTH_EXAM_RECORDDto.FACTOR_OTHER = "";
                hEALTH_EXAM_RECORDDto.ID = cus.CustomerBM.ToString();
                 
                hEALTH_EXAM_RECORDDto.IS_REVIEW = "0";
                if (occsum != null && occsum.Conclusion.Contains("复查"))
                {
                    hEALTH_EXAM_RECORDDto.IS_REVIEW = "1";
                }
                hEALTH_EXAM_RECORDDto.JC_TYPE = "01";
                hEALTH_EXAM_RECORDDto.ORG_CODE = InstitutionBM;
                hEALTH_EXAM_RECORDDto.OTHER_WORK_TYPE = "";
                hEALTH_EXAM_RECORDDto.REMARK = "";
                hEALTH_EXAM_RECORDDto.REPORT_DATE = System.DateTime.Now.ToString();
                hEALTH_EXAM_RECORDDto.REPORT_ORGAN_CREDIT_CODE = InstitutionName;
                hEALTH_EXAM_RECORDDto.REPORT_PERSON = InstitutionUser;
                hEALTH_EXAM_RECORDDto.REPORT_PERSON_TEL = InstitutionUserTel;
                hEALTH_EXAM_RECORDDto.REPORT_UNIT = InstitutionName;
                hEALTH_EXAM_RECORDDto.WORKER_INFO = new WORKER_INFODto();
                if (cusinfo.Birthday.HasValue)
                {
                    hEALTH_EXAM_RECORDDto.WORKER_INFO.BIRTH_DATE = cusinfo.Birthday.Value.ToString("yyyyMMdd");
                }
                else
                {
                    hEALTH_EXAM_RECORDDto.WORKER_INFO.BIRTH_DATE = "";
                }
                hEALTH_EXAM_RECORDDto.WORKER_INFO.GENDER_CODE = cusinfo.Sex.ToString();
                hEALTH_EXAM_RECORDDto.WORKER_INFO.ID_CARD = cusinfo.IDCardNo;

                hEALTH_EXAM_RECORDDto.WORKER_INFO.ID_CARD_TYPE_CODE = cusinfo.IDCardType.ToString();//待处理
                // 证件类型                 
                if (cusinfo.IDCardType.HasValue)
                {
                    var CertificateType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.CertificateType);
                    var IDCardType = _TbmBasicDictionary.GetAll().FirstOrDefault(p => p.Type == CertificateType && p.Value == cusinfo.IDCardType);
                    if (IDCardType != null && !string.IsNullOrEmpty(IDCardType.Remarks))
                    {
                        hEALTH_EXAM_RECORDDto.WORKER_INFO.ID_CARD_TYPE_CODE = IDCardType.Remarks;
                    }
                }

                hEALTH_EXAM_RECORDDto.WORKER_INFO.WORKER_NAME = cusinfo.Name;
                hEALTH_EXAM_RECORDDto.WORKER_INFO.WORKER_TELPHONE = cusinfo.Mobile;



                hEALTH_EXAM_RECORDDto.WORK_TYPE_CODE = (TypeWork.FirstOrDefault(p => p.Text == cus.TypeWork) == null ? "" :
                   TypeWork.FirstOrDefault(p => p.Text == cus.TypeWork).code?.ToString())??"";
                if (cusSum.ConclusionDate.HasValue)
                {
                    hEALTH_EXAM_RECORDDto.WRITE_DATE = cusSum.ConclusionDate.Value.ToString("yyyyMMdd");
                }
                hEALTH_EXAM_RECORDDto.WRITE_PERSON = InstitutionUser;
                hEALTH_EXAM_RECORDDto.WRITE_PERSON_TELPHONE = InstitutionUserTel;
                xmlEventBodyDto.EventBody.HEALTH_EXAM_RECORD.Add(hEALTH_EXAM_RECORDDto);
            }

            return xmlEventBodyDto;
        }
        /// <summary>
        /// 保存国家疾控设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateCountrySet SaveCountry(CreateCountrySet input)
        {

            var tbmcountryset = _TbmCountrySet.GetAll().FirstOrDefault();
            if (tbmcountryset != null)
            {
               
                input.MapTo(tbmcountryset); // 赋值
                _TbmCountrySet.Update(tbmcountryset);
                return tbmcountryset.MapTo<CreateCountrySet>();
            }
            else
            {
             var countryset=   input.MapTo<TbmCountrySet>();
                countryset.Id = Guid.NewGuid();
                _TbmCountrySet.Insert(countryset);
                return countryset.MapTo<CreateCountrySet>();
            }
        }
        /// <summary>
        /// 获取国家疾控设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateCountrySet GetCountry()
        {
           var countryset = _TbmCountrySet.GetAll().FirstOrDefault();
           return countryset.MapTo<CreateCountrySet>();
        }
    }
}
