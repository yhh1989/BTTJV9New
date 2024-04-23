using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics
{
    [AbpAuthorize]

    public class OccConclusionStatisticsAppService : MyProjectAppServiceBase, IOccConclusionStatisticsAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _tjlOccCustomerSum;
        private readonly IRepository<TbmOccHazardFactor, Guid> _OccHazardFactor;
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        public OccConclusionStatisticsAppService(IRepository<TjlOccCustomerSum, Guid> tjlOccCustomerSum,
            IRepository<TjlOccCustomerHazardSum, Guid> TjlOccCustomerHazardSum,
            IRepository<TbmOccHazardFactor, Guid> OccHazardFactor)
        {
            _tjlOccCustomerSum = tjlOccCustomerSum;
            _TjlOccCustomerHazardSum = TjlOccCustomerHazardSum;
            _OccHazardFactor = OccHazardFactor;
        }


        public List<OccConclusionStatisticsShowDto> GetCus(OccStatisticsShowGet input)
        {
            var CusRegSum = _TjlOccCustomerHazardSum.GetAll();
            if (!string.IsNullOrEmpty(input.ClientName))
            {
                CusRegSum = CusRegSum.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);
            }
            if (input.ClientegId.HasValue)
            {
                CusRegSum = CusRegSum.Where(i => i.CustomerRegBM.ClientRegId == input.ClientegId);
            }
            if (input.NavigationStartTime.HasValue)
            {
                if (input.TimeType == 1)
                { CusRegSum = CusRegSum.Where(o => o.CustomerRegBM.LoginDate >= input.NavigationStartTime.Value); }
                else
                { CusRegSum = CusRegSum.Where(o => o.CustomerRegBM.BookingDate >= input.NavigationStartTime.Value); }
            }
            if (input.NavigationEndTime.HasValue)
            {
                if (input.TimeType == 1)
                {
                    CusRegSum = CusRegSum.Where(o => o.CustomerRegBM.LoginDate < input.NavigationEndTime.Value);
                }
                else
                { CusRegSum = CusRegSum.Where(o => o.CustomerRegBM.BookingDate < input.NavigationEndTime.Value); }
            }

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {

                if (input.TimeType == 1)
                {
                    CusRegSum = CusRegSum.Where(i => i.CustomerRegBM.LoginDate.Value.Year.ToString() == input.YearTime);
                }
                else
                { CusRegSum = CusRegSum.Where(i => i.CustomerRegBM.BookingDate.Value.Year.ToString() == input.YearTime); }
            }
            var result = CusRegSum.Select(o => new OccConclusionStatisticsShowDto
            {

                Name = o.CustomerRegBM.Customer.Name,
                Age = o.CustomerRegBM.Customer.Age,
                Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
                TypeWork = o.CustomerRegBM.TypeWork,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                PostState = o.CustomerRegBM.PostState,
                Conclusion = o.Conclusion,
                RiskS = o.OccHazardFactors.Text,
                 IdCarNo=o.CustomerRegBM.Customer.IDCardNo,
                InjuryAge = o.CustomerRegBM.InjuryAge,
                Advise = o.Advise
                
            }).ToList();
            return result.OrderBy(p=>p.Name).ToList();

        }

        /// <summary>
        /// 体检结论统计（饼状图）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DQQuery> StatisticalChar(OccStatisticsShowGet input)
        {
            var data = _tjlOccCustomerSum.GetAllIncluding(a => a.CustomerRegBM.ClientInfo.ClientName);
            data = data.Where(r => input.ClientName.Contains(r.CustomerRegBM.ClientInfo.ClientName));

            //if (input.NavigationStartTime != null)
            //    data = data.Where(r => r.CustomerRegBM.NavigationStartTime >= input.NavigationStartTime);
            //if (input.NavigationEndTime != null)
            //    data = data.Where(r => r.CustomerRegBM.NavigationEndTime <= input.NavigationEndTime);
            if (input.ClientName != null)
                data = data.Where(r => r.CustomerRegBM.ClientInfo.ClientName == input.ClientName);

            var show = data.GroupBy(r => new { r.Conclusion }).Select(r => new DQQuery
            {
                CurrentData = r.Count()
            }).ToList();
            return show;
        }
        /// <summary>
        /// 职业健康检查结果汇总表（按用人单位统计）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OccYearsAllStaticsDto getOCCYears(OccYearsStatisticsDto input)
        {
            OccYearsAllStaticsDto occYearsAllStaticsDto = new OccYearsAllStaticsDto();

            var que = _TjlOccCustomerHazardSum.GetAll();
            if (input.StarDate.HasValue)
            {

                que = que.Where(p => p.CustomerSummarize != null && p.CustomerSummarize.ConclusionDate != null
                && p.CustomerSummarize.ConclusionDate.Value.Year == input.StarDate.Value.Year);
            }
            if (input.ClientegId.HasValue)
            {
                que = que.Where(p => p.CustomerRegBM.ClientRegId == input.ClientegId);
            }
            //年度职业健康检查结果汇总表（按用人单位统计）
            var outClientResult = que.GroupBy(p => new
            {
                clientName = p.CustomerRegBM.ClientInfo != null ? p.CustomerRegBM.ClientInfo.ClientName : "个人",
               // p.Conclusion,
               // p.CustomerRegBM.PostState

            }).Select(p => new OccYearsSaticsDto
            {
                SaticsName = p.Key.clientName,
                BeforeCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("上岗前")).Count(),
                BeforeJJZCount = p.Where(o => o.CustomerRegBM.PostState.Contains("上岗前") && o.Conclusion.Contains("禁忌证")).Count(),
                OnCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗")).Count(),
                OnJJZCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("禁忌证")).Count(),
                OnZYBCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("职业病")).Count(),
                OnFCCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("复查")).Count(),
                AfterCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗")).Count(),
                AfterZYBCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗") && o.Conclusion.Contains("职业病")).Count(),
                AfterFCCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗") && o.Conclusion.Contains("复查")).Count(),
            }).ToList();
            occYearsAllStaticsDto.ClientStatic = outClientResult;
            //年度职业健康检查结果汇总表（按职业病危害因素类别统计） 
            var riskque = _TjlOccCustomerHazardSum.GetAll();
            if (input.StarDate.HasValue)
            {
                riskque = riskque.Where(p =>  p.CreationTime.Year == input.StarDate.Value.Year);
            }
            if (input.ClientegId.HasValue)
            {
                riskque = riskque.Where(p => p.CustomerRegBM.ClientRegId == input.ClientegId);
            }
            var outRiskResult = riskque.GroupBy(p => new
            {
                clientName = p.OccHazardFactors==null?"": p.OccHazardFactors.Text,
               // p.Conclusion,
               // p.CustomerRegBM.PostState

            }).Select(p => new OccYearsSaticsDto
            {
                SaticsName = p.Key.clientName,
                BeforeCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("上岗前")).Count(),
                BeforeJJZCount = p.Where(o => o.CustomerRegBM.PostState.Contains("上岗前") && o.Conclusion.Contains("禁忌证")).Count(),
                OnCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗")).Count(),
                OnJJZCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("禁忌证")).Count(),
                OnZYBCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("职业病")).Count(),
                OnFCCount = p.Where(o => o.CustomerRegBM.PostState.Contains("在岗") && o.Conclusion.Contains("复查")).Count(),
                AfterCheckCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗")).Count(),
                AfterZYBCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗") && o.Conclusion.Contains("职业病")).Count(),
                AfterFCCount = p.Where(o => o.CustomerRegBM.PostState.Contains("离岗") && o.Conclusion.Contains("复查")).Count(),
            }).ToList();
            occYearsAllStaticsDto.RiskTypeStatic = outRiskResult;
            if (occYearsAllStaticsDto.ZYBStatistics == null)
            {
                occYearsAllStaticsDto.ZYBStatistics = new List<OccZYBStatisticsDto>();
            }
            //职业病
            var outzyblist = que.Where(p => p.Conclusion.Contains("职业病")).ToList();
            foreach (var p in outzyblist)
            {
                OccZYBStatisticsDto dto = new OccZYBStatisticsDto();

                dto.Age = p.CustomerRegBM.Customer.Age;
                dto.ClientNamed = p.CustomerRegBM.ClientInfo?.ClientName;
                dto.InjurAge = p.CustomerRegBM.InjuryAge;
                dto.Name = p.CustomerRegBM.Customer.Name;
                dto.PostName = p.CustomerRegBM.PostState;
                dto.Remark = "";
                dto.ReportBM = p.CustomerRegBM.ClientReg?.ClientRegBM;
                dto.RiskName = p.CustomerRegBM.RiskS;
                dto.Sex = p.CustomerRegBM.Customer.Sex == 1 ? "男" : "女";
                dto.ZYBName = string.Join(",", p.OccCustomerOccDiseases.Select(o => o.Text).ToList());
                occYearsAllStaticsDto.ZYBStatistics.Add(dto);
            }
            //var outzyb = que.Where(p => p.Conclusion.Contains("职业病")).Select(p => new 
            //{
            //    Age = p.CustomerRegBM.Customer.Age,
            //    ClientNamed = p.CustomerRegBM.ClientInfo.ClientName,
            //    InjurAge = p.CustomerRegBM.InjuryAge,
            //    Name = p.CustomerRegBM.Customer.Name,
            //    PostName = p.CustomerRegBM.PostState,
            //    Remark = "",
            //    ReportBM = p.CustomerRegBM.ClientReg.ClientRegBM,
            //    RiskName = p.CustomerRegBM.RiskS,
            //    Sex = p.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    ZYBName = string.Join(",", p.OccCustomerOccDiseases.Select(o => o.Text).ToList())
            //}).ToList();

            //occYearsAllStaticsDto.ZYBStatistics = outzyb;
            ////禁忌证
            //var outjjz= que.Where(p => p.Conclusion.Contains("禁忌证")).Select(p => new OccZYBStatisticsDto
            //{
            //    Age = p.CustomerRegBM.Customer.Age,
            //    ClientNamed = p.CustomerRegBM.ClientInfo.ClientName,
            //    InjurAge = p.CustomerRegBM.InjuryAge,
            //    Name = p.CustomerRegBM.Customer.Name,
            //    PostName = p.CustomerRegBM.PostState,
            //    Remark = "",
            //    ReportBM = p.CustomerRegBM.ClientReg.ClientRegBM,
            //    RiskName = p.CustomerRegBM.RiskS,
            //    Sex = p.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    //ZYBName = string.Join(",", p.OccDictionarys.Select(o => o.Text).ToList())
            //}).ToList();
            //occYearsAllStaticsDto.JJZStatistics = outjjz;


            //禁忌证
            if (occYearsAllStaticsDto.JJZStatistics == null)
            {
                occYearsAllStaticsDto.JJZStatistics = new List<OccZYBStatisticsDto>();
            }
            var outjjzlist = que.Where(p => p.Conclusion.Contains("禁忌证")).ToList();
            foreach (var p in outzyblist)
            {
                OccZYBStatisticsDto dto = new OccZYBStatisticsDto();

                dto.Age = p.CustomerRegBM.Customer.Age;
                dto.ClientNamed = p.CustomerRegBM.ClientInfo?.ClientName;
                dto.InjurAge = p.CustomerRegBM.InjuryAge;
                dto.Name = p.CustomerRegBM.Customer.Name;
                dto.PostName = p.CustomerRegBM.PostState;
                dto.Remark = "";
                dto.ReportBM = p.CustomerRegBM.ClientReg?.ClientRegBM;
                dto.RiskName = p.CustomerRegBM.RiskS;
                dto.Sex = p.CustomerRegBM.Customer.Sex == 1 ? "男" : "女";
                dto.ZYBName = string.Join(",", p.OccDictionarys.Select(o => o.Text).ToList());
                occYearsAllStaticsDto.JJZStatistics.Add(dto);
            }
            return occYearsAllStaticsDto;
        }
    }
}
