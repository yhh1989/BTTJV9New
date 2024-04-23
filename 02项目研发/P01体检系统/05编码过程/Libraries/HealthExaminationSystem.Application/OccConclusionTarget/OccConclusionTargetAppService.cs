using Abp.Authorization;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget
{
    [AbpAuthorize]

    public class OccConclusionTargetAppService : MyProjectAppServiceBase, IOccConclusionTargetAppService
    {
        private readonly IRepository<TjlOccCustomerSum, Guid> _TjlOccCustomerSum;
        private readonly IRepository<TjlCustomerReg, Guid> _TjlCustomerReg;
        private readonly IRepository<TbmOccTargetDisease, Guid> _TbmOccTargetDisease;
        public OccConclusionTargetAppService(IRepository<TjlOccCustomerSum, Guid> TjlOccCustomerSum,
         IRepository<TjlCustomerReg, Guid> TjlCustomerReg,
         IRepository<TbmOccTargetDisease, Guid> TbmOccTargetDisease
      )
        {
            _TjlOccCustomerSum = TjlOccCustomerSum;
            _TjlCustomerReg = TjlCustomerReg;
            _TbmOccTargetDisease = TbmOccTargetDisease;
        }
        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccConclusionTargetDto> getTargetCount(TargetGetDto input)
        {
            List<OccConclusionTargetDto> occTargetDtos = new List<OccConclusionTargetDto>();
            //获取危害因素及检查类型

            var quelk = _TjlCustomerReg.GetAll();
            if (!string.IsNullOrWhiteSpace(input.ClientName))
            {
               quelk = quelk.Where(i => i.ClientInfo.ClientName == input.ClientName);
            }
            if (!string.IsNullOrWhiteSpace(input.TeamName))
            {
                quelk = quelk.Where(i => i.ClientTeamInfo.TeamName == input.TeamName);
            }
            if (input.PhysicalType.HasValue)
            {
                quelk = quelk.Where(i => i.PhysicalType == input.PhysicalType);
            }
            if (input.StartCheckDate.HasValue)
                quelk = quelk.Where(o => o.CreationTime >= input.StartCheckDate.Value);

            if (input.EndCheckDate.HasValue)
                quelk = quelk.Where(o => o.CreationTime <= input.EndCheckDate.Value);

            if (!string.IsNullOrWhiteSpace(input.YearTime))
            {
                quelk = quelk.Where(i => i.CreationTime.Year.ToString() == input.YearTime);
            }
            //var quel = quelk.Where(o => o.ClientRegId == input.Id && o.OccHazardFactors != null
            //&& o.OccHazardFactors.Count > 0 && o.RiskS != null && o.PostState != null).ToList();

            var que = quelk.GroupBy(o => new { o.RiskS, o.PostState }).Select(
                g => new {
                    risknames = g.FirstOrDefault(r => r.RiskS == g.Key.RiskS && r.PostState == g.Key.PostState).OccHazardFactors.Select(f => f.Text).ToList(),
                    checktaype = g.Key.PostState
                }).ToList();
            if (que != null && que.Count > 0)
            {
                foreach (var risk in que)
                {//找到目标疾病
                    var risks = risk.risknames.ToList();
                    var targs = _TbmOccTargetDisease.GetAll().Where(o => risks.Contains(o.OccHazardFactors.Text) && o.CheckType == risk.checktaype).ToList();
                    if (targs != null && targs.Count > 0)
                    {
                        foreach (var targ in targs)
                        {
                            if (occTargetDtos.Where(o => o.RiskNames == targ.OccHazardFactors.Text
                             && o.ChckType == targ.CheckType).Count() > 0)
                            {
                                continue;
                            }
                            OccConclusionTargetDto occTargetCountDto = new OccConclusionTargetDto();
                            occTargetCountDto.RiskNames = targ.OccHazardFactors.Text;
                            occTargetCountDto.ChckType = targ.CheckType;
                            var groups = targ.MustIemGroups.Select(o => o.ItemGroupName).ToList();
                            occTargetCountDto.Groups = string.Join("、", groups).TrimEnd('、');
                            var Diss = targ.OccDiseases.Select(o => o.Text).ToList();
                            var targDiss = "职业健康：" + string.Join("、", Diss).TrimEnd('、') + Environment.NewLine;
                            var Cation = targ.Contraindications.Select(o => o.Text).ToList();
                            var argCation = "禁忌证：" + string.Join("、", Cation).TrimEnd('、');
                            occTargetCountDto.AllCount = quelk.Where(o => o.OccHazardFactors.Any(g => g.Text == targ.OccHazardFactors.Text)).Count();
                            occTargetCountDto.HasCount = quelk.Where(o => o.SummSate != 1 && o.OccHazardFactors.Any(g => g.Text == targ.OccHazardFactors.Text)).Count();
                            occTargetCountDto.Target = targDiss + argCation;
                            occTargetDtos.Add(occTargetCountDto);


                        }
                    }

                }
            }
            return occTargetDtos;
        }
    }
}
