using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition
{
    [AbpAuthorize]
    public class OccDisRequisitionAppService : MyProjectAppServiceBase, IOccDisRequisitionAppService
    {

        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlCusReview, Guid> _TjlCusReview;
        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        public OccDisRequisitionAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlCusReview, Guid> TjlCusReview, IRepository<TjlOccCustomerHazardSum, Guid> OccCustomerHazardSum)       
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCusReview = TjlCusReview;
            _TjlOccCustomerHazardSum=OccCustomerHazardSum;
    }
        /// <summary>
        /// 获取职业健康通知单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OutOccCustomerSumDto> GetOccDisRequisition(OutOccCustomerSumDto input)
        {
            var query = _TjlOccCustomerHazardSum.GetAll().Where(p=>p.CustomerRegBM!=null);
            if (input.Id != null && input.Id!=Guid.Empty)
            {
                query = query.Where(i => i.Id == input.Id);
            }
            if (!string.IsNullOrWhiteSpace(input.ClientName))
                query = query.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);

            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate <= input.EndCheckDate.Value);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                query = query.Where(i => i.CustomerRegBM.LoginDate != null &&  i.CustomerRegBM.LoginDate.Value.Year.ToString() == input.YearTime);
            }
            if (input.CustomerRegBMId.HasValue)
            {
                query = query.Where(i => i.CustomerRegBMId == input.CustomerRegBMId);
            }
            if (!string.IsNullOrWhiteSpace(input.CustomerBM))
            {
                query = query.Where(i => i.CustomerRegBM.CustomerBM == input.CustomerBM || i.CustomerRegBM.Customer.Name.Contains(input.CustomerBM));
            }
            if (input.ClientRegID.HasValue)
            { query = query.Where(i => i.CustomerRegBM.ClientRegId == input.ClientRegID); }
            //var aaa = "";
            //foreach (var s in query)
            //{
            //    if (!string.IsNullOrEmpty(s.Conclusion))
            //    {
            //        if (s.Conclusion.Contains("复查"))
            //        {
            //            aaa = "是";
            //        }
            //        else
            //        {
            //            aaa = "否";
            //        }
            //    }
            //    else
            //    {
            //        continue;
            //    }
                
            //}
          
            var result = query.Select(o => new OutOccCustomerSumDto
            {
                Id=o.Id,
                CustomerBM = o.CustomerRegBM.CustomerBM,
                CustomerRegBMId = o.CustomerRegBMId,
                TypeWork = o.CustomerRegBM.TypeWork,
                Name = o.CustomerRegBM.Customer.Name,
                Sex = o.CustomerRegBM.Customer.Sex == 1?"男":"女",
                Age = o.CustomerRegBM.Customer.Age,
                IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
                Conclusion = o.Conclusion,              
                ClientName = o.CustomerRegBM.ClientInfo.ClientName,
                ZYRiskName = o.OccHazardFactors.Text,
                ReviewContent = o.CustomerSummarize.ReviewContent,
                ReviewContentDate = o.CustomerSummarize.ReviewContentDate,
                CheckDate = o.CustomerRegBM.LoginDate,
                WorkName = o.CustomerRegBM.WorkName,
                ZYTabooIllness = o.OccCustomerOccDiseases.Select(p=>new OccNameDto {  Text=p.Text}).ToList(),             
                OccCustomerContraindications = o.OccDictionarys.Select(p => new OccNameDto { Text = p.Text }).ToList(),
                 Description=o.Description,
                ZYTreatmentAdvice = o.Advise,
                PostState = o.CustomerRegBM.PostState,
                TotalWorkAge = o.CustomerRegBM.TotalWorkAge,
                  InjuryAge=o.CustomerRegBM.InjuryAge,
                CustomerRegNum = o.CustomerRegBM.CustomerRegNum,
                YearTime = o.CreationTime.Year.ToString(),
                IsReview =o.Conclusion.Contains("复查")?"是": "否",
                 TZDPrintSate=o.CustomerRegBM.TZDPrintSate
            }).ToList();
            foreach (var curr in result)
            {
                Guid cusregid = curr.CustomerRegBMId.Value;
                var cusolsit= _TjlCusReview.GetAll().Where(o => o.CustomerRegId == cusregid && o.ReviewDate !=null).ToList();
                if (cusolsit.Count > 0)
                {
                    var day = cusolsit.OrderByDescending(p => p.ReviewDate).First();
                    curr.ReviewContentDate = day.ReviewDate.AddDays(day.ReviewDay);
                }
            }
            var outlist= result.OrderBy(p => p.CheckDate).Distinct().ToList();
            return result.OrderBy(p=>p.CheckDate).Distinct().ToList();
        }

        ///// <summary>
        ///// 获取职业健康通知单信息
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public List<InspectionTotal.Dto.OccCustomerHazardSumDto> GetOccDisRiskRequisition(OutOccCustomerSumDto input)
        //{
        //    var query = _TjlOccCustomerHazardSum.GetAll();
        //    if (input.Id != null && input.Id != Guid.Empty)
        //    {
        //        query = query.Where(i => i.Id == input.Id);
        //    }
        //    if (!string.IsNullOrWhiteSpace(input.ClientName))
        //        query = query.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);

        //    if (input.StartCheckDate.HasValue)
        //        query = query.Where(o => o.CustomerRegBM.LoginDate >= input.StartCheckDate.Value);

        //    if (input.EndCheckDate.HasValue)
        //        query = query.Where(o => o.CustomerRegBM.LoginDate <= input.EndCheckDate.Value);

        //    if (!string.IsNullOrWhiteSpace(input.YearTime))
        //    {
        //        query = query.Where(i => i.CustomerRegBM.LoginDate != null && i.CustomerRegBM.LoginDate.Value.Year.ToString() == input.YearTime);
        //    }
        //    if (input.CustomerRegBMId.HasValue)
        //    {
        //        query = query.Where(i => i.CustomerRegBMId == input.CustomerRegBMId);
        //    }
        //    if (!string.IsNullOrWhiteSpace(input.CustomerBM))
        //    {
        //        query = query.Where(i => i.CustomerRegBM.CustomerBM == input.CustomerBM || i.CustomerRegBM.Customer.Name.Contains(input.CustomerBM));
        //    }
        //    if (input.ClientRegID.HasValue)
        //    { query = query.Where(i => i.CustomerRegBM.ClientRegId == input.ClientRegID); }
        //    var aaa = "";
        //    foreach (var s in query)
        //    {
        //        if (!string.IsNullOrEmpty(s.Conclusion))
        //        {
        //            if (s.Conclusion.Contains("复查"))
        //            {
        //                aaa = "是";
        //            }
        //            else
        //            {
        //                aaa = "否";
        //            }
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //    }

        //    var result = query.Select(o => new InspectionTotal.Dto.OccCustomerHazardSumDto
        //    {
        //        Id = o.Id,
        //        CustomerBM = o.CustomerRegBM.CustomerBM,
        //        CustomerRegBMId = o.CustomerRegBMId,
        //        TypeWork = o.CustomerRegBM.TypeWork,
        //        Name = o.CustomerRegBM.Customer.Name,
        //        Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
        //        Age = o.CustomerRegBM.Customer.Age,
        //        IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
        //        Conclusion = o.Conclusion,
        //        ClientName = o.CustomerRegBM.ClientInfo.ClientName,
        //        ZYRiskName = o.CustomerRegBM.RiskS,
        //        ReviewContent = o.CustomerSummarize.ReviewContent,
        //        ReviewContentDate = o.CustomerSummarize.ReviewContentDate,
        //        CheckDate = o.CustomerRegBM.LoginDate,
        //        WorkName = o.CustomerRegBM.WorkName,
        //        ZYTabooIllness = o.OccCustomerOccDiseases.Select(p => new OccNameDto { Text = p.Text }).ToList(),
        //        OccCustomerContraindications = o.OccCustomerContraindications.Select(p => new OccNameDto { Text = p.Text }).ToList(),


        //        ZYTreatmentAdvice = o.Advise,
        //        PostState = o.CustomerRegBM.PostState,
        //        TotalWorkAge = o.CustomerRegBM.TotalWorkAge,
        //        InjuryAge = o.CustomerRegBM.InjuryAge,
        //        CustomerRegNum = o.CustomerRegBM.CustomerRegNum,
        //        YearTime = o.CreationTime.Year.ToString(),
        //        IsReview = aaa,
        //        TZDPrintSate = o.CustomerRegBM.TZDPrintSate
        //    }).ToList();
        //    foreach (var curr in result)
        //    {
        //        Guid cusregid = curr.CustomerRegBMId.Value;
        //        var cusolsit = _TjlCusReview.GetAll().Where(o => o.CustomerRegId == cusregid && o.ReviewDate != null).ToList();
        //        if (cusolsit.Count > 0)
        //        {
        //            var day = cusolsit.OrderByDescending(p => p.ReviewDate).First();
        //            curr.ReviewContentDate = day.ReviewDate.AddDays(day.ReviewDay);
        //        }
        //    }
        //    return result.OrderBy(p => p.CheckDate).ToList();
        //}
    }
}
