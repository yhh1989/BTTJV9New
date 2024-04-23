using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease
{
   public  interface IOccTargetDiseaseAppService
#if !Proxy
        : IApplicationService
#endif
    {
        OutTbmOccTargetDiseaseDto Add(FullTargetDiseaseDto input);
        List<OutTbmOccTargetDiseaseDto> AddExcel(List<OutOccTargetDiseaseExcel> input);

        List<OutTbmOccTargetDiseaseDto> ShowOccHazardFactor(SeachOccTargetDiseaseDto input);

        void Del(EntityDto<Guid> input);

        OutTbmOccTargetDiseaseDto Edit(FullTargetDiseaseDto input);

        OutTbmOccTargetDiseaseDto GetOccTargetDisease(EntityDto<Guid> input);

        /// <summary>
        /// 根据目标疾病和岗位类别获取可选项目和必选项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        OutIItemGroupsDto getOccHazardFactors(InputRisksDto input);
        List<TbmOccDiseaseDto> GetTbmOccDisease();
    }
}
