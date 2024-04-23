using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected
{
    [AbpAuthorize]

    public class OccConclusionSuspectedAppService:MyProjectAppServiceBase, IOccConclusionSuspectedAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _tjlOccCustomerSum;
        public OccConclusionSuspectedAppService(IRepository<TjlOccCustomerSum, Guid> tjlOccCustomerSum)
        {
            _tjlOccCustomerSum = tjlOccCustomerSum;
        }

        /// <summary>
        /// 获取疑似职业健康统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccConclusionSuspectedShow> GetOccConclusionSuspected(OccSuspectedGet input)
        {
            var query = _tjlOccCustomerSum.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ClientName))
                query = query.Where(i => i.CustomerRegBM.ClientInfo.ClientName == input.ClientName);

            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CreationTime >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CreationTime <= input.EndCheckDate.Value);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                query = query.Where(i => i.CreationTime.Year.ToString() == input.YearTime);
            }

            var result = query.Select(o => new OccConclusionSuspectedShow
            {
                CustomerBM = o.CustomerRegBM.CustomerBM,
                CustomerRegBMId = o.CustomerRegBMId,
                TypeWork = o.CustomerRegBM.TypeWork,
                Name = o.CustomerRegBM.Customer.Name,
                Sex = o.CustomerRegBM.Customer.Sex,
                Age = o.CustomerRegBM.Customer.Age,
                Conclusion = o.Conclusion,
                //ClientName = o.CustomerRegBM.ClientInfo.ClientName,
                ZYRiskName = o.CustomerSummarize.ZYRiskName,               
                ZYTreatmentAdvice = o.CustomerSummarize.ZYTreatmentAdvice,
                PostState = o.CustomerRegBM.PostState,
                //YearTime = o.CreationTime.Year.ToString(),
                
            }).ToList();
            return result;
        }
    }
}
