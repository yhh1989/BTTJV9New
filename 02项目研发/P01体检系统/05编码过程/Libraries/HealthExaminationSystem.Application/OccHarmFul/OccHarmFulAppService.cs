using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul
{
    [AbpAuthorize]
    public class OccHarmFulAppService : MyProjectAppServiceBase, IOccHarmFulAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;

        private readonly IRepository<TjlOccCustomerHazardSum, Guid> _TjlOccCustomerHazardSum;
        public OccHarmFulAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
            IRepository<TjlOccCustomerHazardSum, Guid>  OccCustomerHazardSum)
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlOccCustomerHazardSum = OccCustomerHazardSum;
        }


        public List<OutOccFactoryDto> GetOutOccFactories(OutOccFactoryDto input)
        {
            var query = _TjlOccCustomerHazardSum.GetAll();
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
            if (!string.IsNullOrEmpty(input.TextType))
            {
                query = query.Where(p=> p.OccHazardFactors.Parent!=null && 
                p.OccHazardFactors.Parent.Text== input.TextType);
            }

            //var risls = query.GroupBy(o => new { RiskS= o.OccHazardFactors.Text  , o.CustomerRegBM.PostState }).Select(
            //     g => new {
            //         risknames = g.FirstOrDefault(r => r.CustomerRegBM.OccHazardFactors.Any(p=>p.Text == g.Key.RiskS) && r.CustomerRegBM.PostState == g.Key.PostState).CustomerRegBM.OccHazardFactors.Select(f => new { f.Text, type = f.Parent.Text }).ToList(),
            //         checktaype = g.Key.PostState
            //     }).ToList();
            //int CheckNumber = 1;
            //int countS = 1;
            //var sss="";
            //foreach (var ris in risls)
            //{               
            //    var risks = ris.risknames.ToList();
            //    foreach (var risk in risks)
            //    {
            //         sss = risk.type; 
            //          var count = query.Where(o => o.CustomerRegBM.OccHazardFactors.Any(r => r.Text == risk.Text)).ToList();
            //        if (count.Count() > 0)
            //        {
            //            CheckNumber = count.Count();
            //        }
                    
            //        countS = query.Where(o => o.Opinions.Contains("职业健康")).Count();
                    
            //    }
            //}
            
            
            var result = query.GroupBy(o => new { RiskS = o.OccHazardFactors.Text,
                o.CustomerRegBM.PostState,
                Parent= o.OccHazardFactors.Parent==null?"": o.OccHazardFactors.Parent.Text }).Select(o => new OutOccFactoryDto
            {
                PostState = o.Key.PostState,
                CheckNumber = o.Count(),
                Text = o.Key.RiskS,
                TextType= o.Key.Parent,
                AbnormalNumber = query.Where(p=>!p.Conclusion.Contains("未见异常")
                && !p.Conclusion.Contains("未见明显异常")).Count(),
                recallNumber = query.Where(p => !p.Conclusion.Contains("未见异常")
                && !p.Conclusion.Contains("未见明显异常")).Count() / o.Count(),
            }).ToList();
            return result;
        }
    }
}
