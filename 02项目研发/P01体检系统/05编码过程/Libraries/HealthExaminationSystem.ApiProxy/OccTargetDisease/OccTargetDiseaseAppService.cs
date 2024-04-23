using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccTargetDisease
{
    public class OccTargetDiseaseAppService : AppServiceApiProxyBase, IOccTargetDiseaseAppService
    {
        public OutTbmOccTargetDiseaseDto Add(FullTargetDiseaseDto input)
        {
            return GetResult<FullTargetDiseaseDto, OutTbmOccTargetDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutTbmOccTargetDiseaseDto> AddExcel(List<OutOccTargetDiseaseExcel> input)
        {
            return GetResult<List<OutOccTargetDiseaseExcel>, List<OutTbmOccTargetDiseaseDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutTbmOccTargetDiseaseDto Edit(FullTargetDiseaseDto input)
        {
            return GetResult<FullTargetDiseaseDto, OutTbmOccTargetDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutTbmOccTargetDiseaseDto GetOccTargetDisease(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, OutTbmOccTargetDiseaseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void Del(EntityDto<Guid> input)
        {
            GetResult<EntityDto<Guid>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OutTbmOccTargetDiseaseDto> ShowOccHazardFactor(SeachOccTargetDiseaseDto input)
        {
            return GetResult<SeachOccTargetDiseaseDto, List<OutTbmOccTargetDiseaseDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public OutIItemGroupsDto getOccHazardFactors(InputRisksDto input)
        {
            return GetResult<InputRisksDto,OutIItemGroupsDto>(input,DynamicUriBuilder.GetAppSettingValue());
        }
        public List<TbmOccDiseaseDto> GetTbmOccDisease()
        {
            return GetResult<List<TbmOccDiseaseDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
