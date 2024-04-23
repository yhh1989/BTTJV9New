using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionYear
{
    [AbpAuthorize]
    public class OccConclusionYearAppService : MyProjectAppServiceBase, IOccConclusionYearAppService
    {
        private readonly IRepository<TjlCustomerReg, Guid> _TjlOccCustomerReg ;
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        public OccConclusionYearAppService(IRepository<TjlCustomerReg, Guid> TjlOccCustomerReg,
             IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum)
        {
            _TjlOccCustomerReg = TjlOccCustomerReg;
            _TjlOccCustomerSum = TjlOccCustomerSum;
        }
        public List<OutOccMothDto> GetOccAbnormalResult(OccConclusionYearShowDto input)
        {
            var query = _TjlOccCustomerSum.GetAll();
            if (input.ClientregID.HasValue)
                query = query.Where(i => i.CustomerRegBM.ClientRegId == input.ClientregID);
            if (input.StartCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate.Value.Year >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                query = query.Where(o => o.CustomerRegBM.LoginDate.Value.Year <= input.EndCheckDate.Value);
            var ss = query.ToList();
            var querys = query.GroupBy(o => new { o.CustomerRegBM.ClientInfo.ClientName, o.CustomerRegBM.LoginDate.Value.Year })
                .Select(o => new OutOccMothDto
                {
                    ClientName = o.Key.ClientName,
                    ConCount = o.Count(),
                    ConName = o.Key.Year                     

                   
                }).ToList();
            return querys;
        }
    }
}
