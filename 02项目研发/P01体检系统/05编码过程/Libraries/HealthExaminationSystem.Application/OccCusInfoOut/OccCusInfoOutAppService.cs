using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccCusInfoOut
{
    [AbpAuthorize]
    public class OccCusInfoOutAppService : MyProjectAppServiceBase, IOccCusInfoOutAppService
    {

        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg;
        private readonly IRepository<TbmBasicDictionary, Guid> _TbmBasicDictionary;

        private readonly IRepository<TbmOccDictionary, Guid> _TbmOccDictionary;

        private readonly IRepository<TjlCustomerRegItem, Guid> _TjlCustomerRegItem;

        private readonly IRepository<TjlCusReview, Guid> _TjlCusReview;
        private readonly IRepository<AdministrativeDivision, Guid> _AdministrativeDivision;
        public OccCusInfoOutAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
            IRepository<TbmBasicDictionary, Guid> TbmBasicDictionary,
            IRepository<TbmOccDictionary, Guid> TbmOccDictionary,
            IRepository<TjlCustomerRegItem, Guid> TjlCustomerRegItem,
            IRepository<TjlCusReview, Guid> TjlCusReview,
            IRepository<AdministrativeDivision, Guid> AdministrativeDivision)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCustomerReg = TjlCustomerReg;
            _TbmBasicDictionary = TbmBasicDictionary;
            _TbmOccDictionary = TbmOccDictionary;
            _TjlCustomerRegItem = TjlCustomerRegItem;
            _TjlCusReview = TjlCusReview;
            _AdministrativeDivision = AdministrativeDivision;
        }
        /// <summary>
        /// 1.职业健康档案信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public OutOccAllDto getOccCusInfoDto(InOccSearchDto input)
        {
            OutOccAllDto outOccAllDto = new OutOccAllDto();
            //单位信息
            string InstitutionName = "";
            string InstitutionBM = "";
            var Institution = _TbmBasicDictionary.GetAll().Where(o => o.Type == "Institution").ToList();
            if (Institution != null && Institution.Count > 0)
            {
                InstitutionName = Institution.FirstOrDefault(o => o.Value == 1)?.Remarks;
                InstitutionBM = Institution.FirstOrDefault(o => o.Value == 2)?.Remarks;
            }
            //职业健康主检结论
            var occSumlist = _TbmOccDictionary.GetAll().Where(o => o.Type == "Conclusion").Select(o => new { o.code, o.Text }).ToList();
            //职业健康工种
            var TypeWork = _TbmOccDictionary.GetAll().Where(o => o.Type == "WorkType").Select(o => new { o.code, o.Text }).ToList();
            //体检类型
            var Checktype = _TbmOccDictionary.GetAll().Where(o => o.Type == "Checktype").Select(o => new { o.code, o.Text }).ToList();

            var que = _TjlOccCustomerSum.GetAll();
            if (input.Stardt.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.LoginDate >= input.Stardt);
            }
            if (input.Enddt.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.LoginDate < input.Enddt);
            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.ClientRegId == input.ClientRegId);
            }

            var cuslit = que.Select(o => new
            { 
                出生日期 = o.CustomerRegBM.Customer.Birthday,
                填表日期 = o.CustomerRegBM.BookingDate,

                体检机构编码 = InstitutionBM,
                体检机构名称 = InstitutionName,
                填表单位名称 = InstitutionName,
                检查类型 = "11",

                用人单位联系电话 = o.CustomerRegBM.ClientInfo.Mobile,
                用人单位名称 = o.CustomerRegBM.ClientInfo == null ? "" : o.CustomerRegBM.ClientInfo.ClientName,
                用人单位统一社会信用代码 = o.CustomerRegBM.ClientInfo.SocialCredit,
                体检编号 = o.CustomerRegBM.CustomerBM,
                联系电话 = o.CustomerRegBM.Customer.Mobile,
                证件号码 = o.CustomerRegBM.Customer.IDCardNo,
                证件类型 = o.CustomerRegBM.Customer.IDCardType,
                接触工龄年 = o.CustomerRegBM.InjuryAgeUnit == "年" ? (o.CustomerRegBM.InjuryAge == "" ? "0" : o.CustomerRegBM.InjuryAge) : "0",
                接触工龄月 = o.CustomerRegBM.InjuryAgeUnit == "月" ? (o.CustomerRegBM.InjuryAge == "" ? "0" : o.CustomerRegBM.InjuryAge) : "0",
                紧急联系人 = o.CustomerRegBM.ClientInfo == null ? "" : o.CustomerRegBM.ClientInfo.LinkMan,
                紧急联系电话 = o.CustomerRegBM.ClientInfo == null ? "" : o.CustomerRegBM.ClientInfo.Telephone,
                体检时间 = o.CustomerRegBM.LoginDate,
                婚姻状况 = o.CustomerRegBM.MarriageStatus,
                姓名 = o.CustomerRegBM.Customer.Name,
                主检建议 = o.Advise,
                监测类型代码 = "01", 
                主检结论 = o.Conclusion,
                主检医生 = o.CustomerSummarize == null ? "" : o.CustomerSummarize.EmployeeBM.Name,
                填表人电话 = o.CustomerRegBM.OrderUser.PhoneNumber,
                填表人名称 = o.CustomerRegBM.OrderUser.Name,
                体检类型编码 = o.CustomerRegBM.PostState,
                体检报告时间 = o.CreationTime,
                职业健康档案类别 = "101",
                复检对应上次的职业健康档案编号 = "",
                备注 = o.CustomerRegBM.Remarks,
                报告单位名称 = InstitutionName,
                报告人联系电话 = o.CustomerRegBM.OrderUser.PhoneNumber,
                报告人姓名 = o.CustomerRegBM.OrderUser.Name,
                性别 = o.CustomerRegBM.Customer.Sex,
                总工龄年 = o.CustomerRegBM.WorkAgeUnit == "年" ? (o.CustomerRegBM.TotalWorkAge == "" ? "0" : o.CustomerRegBM.TotalWorkAge) : "0",
                总工龄月 = o.CustomerRegBM.WorkAgeUnit == "月" ? (o.CustomerRegBM.TotalWorkAge == "" ? "0" : o.CustomerRegBM.TotalWorkAge) : "0",
                工种编码 = o.CustomerRegBM.TypeWork,
                劳动者工号 = o.CustomerRegBM.Customer.WorkNumber,
                车间 = o.CustomerRegBM.WorkName,
                cusretid = o.CustomerRegBMId.Value,
                Id = o.Id,
                Risk = o.CustomerRegBM.OccHazardFactors.Select(p => new { p.OrderNum }).ToList(),
                OccCustomerOccDiseases = o.OccCustomerOccDiseases.Select(p => new { p.OrderNum }).ToList(),
                OccCustomerContraindications = o.OccCustomerContraindications.Select(p => new { p.OrderNum }).ToList(),
                ClientInfo = o.CustomerRegBM.ClientInfo,



            }).ToList();
            List<GBoDto> AdminiName = new List<GBoDto>();
            outOccAllDto.OutOccCusInfos = new List<OutOccCusInfoDto>();
            outOccAllDto.OutOccClientInfos = new List<OutOccClientInfoDto>();
            outOccAllDto.OutOccRiskS = new List<OutOccRiskSDto>();
            outOccAllDto.OutOccRiskSTimes = new List<OutOccRiskSTimeDto>();
            outOccAllDto.OutOccCusItems = new List<OutOccCusItemDto>();
            outOccAllDto.OutOccCusSums = new List<OutOccCusSumDto>();

            //列为名称转编码
            foreach (var occCus in cuslit)
            {

                #region 1.职业健康档案信息
                OutOccCusInfoDto outOccCusInfoDto = new OutOccCusInfoDto();
                outOccCusInfoDto.出生日期 = occCus.出生日期 == null ? null : occCus.出生日期.Value.ToString("yyyy-MM-dd");
                outOccCusInfoDto.填表日期 = occCus.填表日期 == null ? null : occCus.填表日期.Value.ToString("yyyy-MM-dd");

                outOccCusInfoDto.体检机构编码 = occCus.体检机构编码;
                outOccCusInfoDto.体检机构名称 = occCus.体检机构名称;
                outOccCusInfoDto.填表单位名称 = occCus.填表单位名称;
                outOccCusInfoDto.检查类型 = "11";

                outOccCusInfoDto.用人单位联系电话 = occCus.用人单位联系电话;
                outOccCusInfoDto.用人单位名称 = occCus.用人单位名称;
                outOccCusInfoDto.用人单位统一社会信用代码 = occCus.用人单位统一社会信用代码;
                outOccCusInfoDto.体检编号 = occCus.体检编号;
                outOccCusInfoDto.联系电话 = occCus.联系电话;
                outOccCusInfoDto.证件号码 = occCus.证件号码;
                outOccCusInfoDto.证件类型 = occCus.证件类型;
                outOccCusInfoDto.接触工龄年 = occCus.接触工龄年;
                outOccCusInfoDto.接触工龄月 = occCus.接触工龄月;
                outOccCusInfoDto.紧急联系人 = occCus.紧急联系人;
                outOccCusInfoDto.紧急联系电话 = occCus.紧急联系电话;
                outOccCusInfoDto.体检时间 = occCus.体检时间 == null ? null : occCus.体检时间.Value.ToString("yyyy-MM-dd");
                outOccCusInfoDto.婚姻状况 = occCus.婚姻状况;
                outOccCusInfoDto.姓名 = occCus.姓名;
                outOccCusInfoDto.主检建议 = occCus.主检建议;
                outOccCusInfoDto.监测类型代码 = "01";
                outOccCusInfoDto.主检结论 = occSumlist.FirstOrDefault(p => p.Text == occCus.主检结论) == null ? "" :
                  occSumlist.FirstOrDefault(p => p.Text == occCus.主检结论).code?.ToString();
                outOccCusInfoDto.主检医生 = occCus.主检医生;
                outOccCusInfoDto.填表人电话 = occCus.填表人电话;
                outOccCusInfoDto.填表人名称 = occCus.填表人名称;
                outOccCusInfoDto.体检类型编码 = Checktype.FirstOrDefault(p => p.Text == occCus.体检类型编码) == null ? "" :
                   Checktype.FirstOrDefault(p => p.Text == occCus.体检类型编码).code?.ToString();
                outOccCusInfoDto.体检报告时间 = occCus.体检报告时间.ToString("yyyy-MM-dd");
                outOccCusInfoDto.职业健康档案类别 = "101";
                outOccCusInfoDto.复检对应上次的职业健康档案编号 = occCus.复检对应上次的职业健康档案编号;
                outOccCusInfoDto.备注 = occCus.备注;
                outOccCusInfoDto.报告单位名称 = occCus.报告单位名称;
                outOccCusInfoDto.报告人联系电话 = occCus.报告人联系电话;
                outOccCusInfoDto.报告人姓名 = occCus.报告人姓名;
                outOccCusInfoDto.性别 = occCus.性别;
                outOccCusInfoDto.总工龄年 = occCus.总工龄年;
                outOccCusInfoDto.总工龄月 = occCus.总工龄月;
                outOccCusInfoDto.工种编码 = TypeWork.FirstOrDefault(p => p.Text == occCus.工种编码) == null ? "" :
                   TypeWork.FirstOrDefault(p => p.Text == occCus.工种编码).code?.ToString();
                outOccCusInfoDto.劳动者工号 = occCus.劳动者工号;
                outOccCusInfoDto.车间 = occCus.车间;
                outOccAllDto.OutOccCusInfos.Add(outOccCusInfoDto);
                #endregion

                #region  2.职业健康档案-用人单位信息
                string qyName = ""; 
                string qy = "";
                if (occCus.ClientInfo != null)
                {
                    qy = occCus.ClientInfo.StoreAdressS + occCus.ClientInfo.StoreAdressQ;
                    if (!AdminiName.Any(p => p.MacId == qy) && !string.IsNullOrWhiteSpace(occCus.ClientInfo.StoreAdressS)
                    && !string.IsNullOrWhiteSpace(occCus.ClientInfo.StoreAdressQ))
                    {

                        var pname = _AdministrativeDivision.GetAll().FirstOrDefault(p => p.Code == occCus.ClientInfo.StoreAdressS)?.Name;
                        var qname = _AdministrativeDivision.GetAll().FirstOrDefault(p => p.Code == occCus.ClientInfo.StoreAdressQ)?.Name;
                        qyName = pname + qname;
                        AdminiName.Add(new GBoDto { MacId = qy, Value = qyName });
                    }
                    else if (!string.IsNullOrWhiteSpace(occCus.ClientInfo.StoreAdressS)
                        && !string.IsNullOrWhiteSpace(occCus.ClientInfo.StoreAdressQ))
                    {
                        qyName = AdminiName.First(p => p.MacId == qy)?.Value;
                    }
                }

                OutOccClientInfoDto outOccClientInfoDto = new OutOccClientInfoDto();
                outOccClientInfoDto.体检编号 = occCus.体检编号;
                outOccClientInfoDto.企业规模 = occCus.ClientInfo == null ? null : occCus.ClientInfo.Scale;
                outOccClientInfoDto.所属地区编码 = occCus.ClientInfo == null ? "" : occCus.ClientInfo.StoreAdressX;
                outOccClientInfoDto.用人单位名称 = occCus.ClientInfo == null ? "" : occCus.ClientInfo.ClientName;
                outOccClientInfoDto.用人单位所在区名称 = qyName;
                outOccClientInfoDto.用人单位统一社会信用代码 = occCus.ClientInfo == null ? "" : occCus.ClientInfo.SocialCredit;
                outOccClientInfoDto.用人单位联系人 = occCus.ClientInfo == null ? "" : occCus.ClientInfo.LinkMan;
                outOccClientInfoDto.用人单位联系电话 = occCus.ClientInfo == null ? null : occCus.ClientInfo.Telephone;
                outOccClientInfoDto.经济类型编码 = occCus.ClientInfo == null ? null : occCus.ClientInfo.EconomicType;
                outOccClientInfoDto.行业编码 = occCus.ClientInfo == null ? null : occCus.ClientInfo.Clientlndutry;
                outOccClientInfoDto.通讯地址 = occCus.ClientInfo == null ? null : occCus.ClientInfo.Address;
                outOccClientInfoDto.邮政编码 = occCus.ClientInfo == null ? null : occCus.ClientInfo.PostCode;
                outOccAllDto.OutOccClientInfos.Add(outOccClientInfoDto);
                #endregion
                #region 3.职业健康档案-接触危害因素
                OutOccRiskSDto outOccRiskSDto = new OutOccRiskSDto();
                outOccRiskSDto.体检编号 = occCus.体检编号;
                var risBMs = "";
                if (occCus.Risk != null && occCus.Risk.Count > 0)
                {
                    var sss = occCus.Risk.Where(o => o.OrderNum != null).Select(p => p.OrderNum)?.ToList();
                    if (sss != null && sss.Count > 0)
                    {
                        risBMs = string.Join(",", sss);
                    }
                }
                outOccRiskSDto.危害因素编码 = risBMs;
                outOccAllDto.OutOccRiskS.Add(outOccRiskSDto);
                #endregion

                #region 4.职业健康档案-体检危害因素
                OutOccRiskSTimeDto outOccRiskSTimeDto = new OutOccRiskSTimeDto();

                outOccRiskSTimeDto.体检编号 = occCus.体检编号;
                outOccRiskSTimeDto.危害因素编码 = risBMs;
                string year = "0";
                string mouth = "0";
                if (decimal.TryParse(occCus.接触工龄年, out decimal noeyeas))
                {
                      year = decimal.Parse(occCus.接触工龄年).ToString("0");
                }
                if (decimal.TryParse(occCus.接触工龄月, out decimal noemoth))
                {
                      mouth = decimal.Parse(occCus.接触工龄月).ToString("0");
                }
                outOccRiskSTimeDto.开始接害日期 = occCus.体检时间.Value.AddYears(-int.Parse(year)).AddMonths(-int.Parse(mouth));
                outOccRiskSTimeDto.接触所监测危害因素工龄年 = occCus.接触工龄年;
                outOccRiskSTimeDto.接触所监测危害因素工龄月 = occCus.接触工龄月;
                outOccAllDto.OutOccRiskSTimes.Add(outOccRiskSTimeDto);

                #endregion

                #region  6.职业健康档案-体检结论
                OutOccCusSumDto outOccCusSumDto = new OutOccCusSumDto();
                outOccCusSumDto.体检编号 = occCus.体检编号;
                if (occCus.主检结论 != null && occCus.主检结论.Contains("复查"))
                {
                    var itemlist = _TjlCusReview.GetAll().Where(o => o.CustomerRegId == occCus.cusretid).SelectMany(o => o.ItemGroup).
                        SelectMany(o => o.ItemInfos).Select(o => o.StandardCode).ToList();
                    outOccCusSumDto.需复查的检查项目编码 = string.Join(",", itemlist);

                }
                else if (occCus.主检结论 != null && occCus.主检结论.Contains("职业健康"))
                {

                    outOccCusSumDto.危害因素编码 = risBMs;
                    var zybbm = "";
                    if (occCus.OccCustomerOccDiseases != null && occCus.OccCustomerOccDiseases.Count > 0)
                    {
                        zybbm = string.Join(",", occCus.OccCustomerOccDiseases.Where(o => o.OrderNum != null).Select(o => o.OrderNum).ToList());

                    }

                    outOccCusSumDto.职业禁忌证编码 = zybbm;



                }
                else if (occCus.主检结论 != null && occCus.主检结论.Contains("禁忌证"))
                {
                    outOccCusSumDto.危害因素编码 = risBMs;
                    var jjzbm = "";
                    if (occCus.OccCustomerContraindications != null && occCus.OccCustomerContraindications.Count > 0)
                    {
                        jjzbm = string.Join(",", occCus.OccCustomerContraindications.Where(o => o.OrderNum != null).Select(o => o.OrderNum).ToList());

                    }

                    outOccCusSumDto.疑似职业健康编码 = jjzbm;
                }



                outOccCusSumDto.体检结论编码 = occSumlist.FirstOrDefault(p => p.Text == occCus.主检结论) == null ? "" :
                    occSumlist.FirstOrDefault(p => p.Text == occCus.主检结论).code?.ToString();

                outOccAllDto.OutOccCusSums.Add(outOccCusSumDto);
                #endregion




            }

            var queitem = _TjlCustomerRegItem.GetAll().Where(o => o.CustomerRegBM.RiskS != "" && o.CustomerRegBM.RiskS != null && o.ProcessState == (int)ProjectIState.Complete &&
            (o.CustomerRegBM.SummSate == (int)SummSate.HasInitialReview || o.CustomerRegBM.SummSate == (int)SummSate.Audited));
            if (input.Stardt.HasValue)
            {
                queitem = queitem.Where(o => o.CustomerRegBM.LoginDate >= input.Stardt);
            }
            if (input.Enddt.HasValue)
            {
                queitem = queitem.Where(o => o.CustomerRegBM.LoginDate < input.Enddt);
            }
            if (input.ClientRegId.HasValue)
            {
                queitem = queitem.Where(o => o.CustomerRegBM.ClientRegId == input.ClientRegId);
            }
            var ss = queitem.ToList();
            var occItem = queitem.Select(p => new OutOccCusItemDto
            {
                体检编号 = p.CustomerRegBM.CustomerBM,
                体检项目编号 = p.ItemBM.StandardCode,
                参考范围最小值 = p.ItemBM.moneyType == (int)ItemType.Number ? (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(0, p.Stand.IndexOf("－")) : "") : "",
                参考范围最大值 = p.ItemBM.moneyType == (int)ItemType.Number ? (p.Stand.IndexOf("－") > 0 ? p.Stand.Substring(p.Stand.IndexOf("－") + 1, (p.Stand.Length - p.Stand.IndexOf("－") - 1)) : "") : "",
                其他项目名称 = p.ItemName,
                合格标记 = (p.Symbol == "" ? "合格" : (p.Symbol == "M" ? "合格" : "不合格")),
                检查医生 = p.InspectEmployeeBM == null ? "" : p.InspectEmployeeBM.Name,
                检查日期 = p.CustomerItemGroupBM.FirstDateTime,
                检查科室 = p.DepartmentBM.Name,
                检查结果类别编码 = "2",
                检查项目结果 = p.Symbol == "P" ? (p.ItemDiagnosis != "" ? p.ItemDiagnosis : p.ItemResultChar) : p.ItemResultChar,
                计量单位 = p.Unit,
                项目组合名称 = p.ItemGroupBM.ItemGroupName
            }).ToList();

            outOccAllDto.OutOccCusItems = occItem;

            return outOccAllDto;
        }

    }
}
