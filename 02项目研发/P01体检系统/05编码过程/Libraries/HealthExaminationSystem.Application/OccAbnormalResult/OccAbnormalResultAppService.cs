using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult
{
    [AbpAuthorize]
    public class OccAbnormalResultAppService : MyProjectAppServiceBase, IOccAbnormalResultAppService
    {
        private readonly IRepository<TjlCustomerSummarizeBM, Guid> _TjlOccCustomerSum;


        public OccAbnormalResultAppService(IRepository<TjlCustomerSummarizeBM, Guid> TjlOccCustomerSum)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
        }

        public List<OutOccAbnormalResult> GetOccAbnormalResult(OutOccAbnormalResult input)
        {
            var query = _TjlOccCustomerSum.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ClientName))
                query = query.Where(i => i.CustomerReg.ClientInfo.ClientName == input.ClientName);
            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CreationTime >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CreationTime <= input.EndCheckDate.Value);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                query = query.Where(i => i.CreationTime.Year.ToString() == input.YearTime);
            }

            var querys = query.GroupBy(o => new { o.SummarizeName })
                .Select(group=>new { Name = group.Key.SummarizeName, CountNumber = group.Count(), MenCount = group.Where(o=>o.CustomerReg.Customer.Sex==1).Count(),
                WoMenCount = group.Where(o => o.CustomerReg.Customer.Sex == 0).Count() }).ToList();          
                       
            var que = query.Select(count=>new { counts=count.CustomerReg.Customer.Name}).ToList();
            int Numbers = 0;            
            foreach (var s in que)
            {
                Numbers = s.counts.Count();
            }                      
            var result = querys.Select(o => new OutOccAbnormalResult
            {
                Name=o.Name,
                CountNumber=o.CountNumber,
                CheckNumber= o.CountNumber/ Numbers,
                MenCount =o.MenCount,
                CheckMen = o.MenCount / Numbers,
                WoMenCount =o.WoMenCount,
                CheckWoMen = o.WoMenCount / Numbers,
            }).ToList();
            return result;
        }
    }
}
