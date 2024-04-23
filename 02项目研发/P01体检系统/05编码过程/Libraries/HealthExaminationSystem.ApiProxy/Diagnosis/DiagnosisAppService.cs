using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Diagnosis.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Diagnosis
{
    public class DiagnosisAppService : AppServiceApiProxyBase, IDiagnosisAppService
    {
        public List<ItemInfoGroupDto> QueryInfoGroup(ItemInfoGroupDto input)
        {
            return GetResult <ItemInfoGroupDto, List<ItemInfoGroupDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<TbmDiagnosisDto> QueryDiagnosis(PageInputDto<TbmDiagnosisDto> input)
        {
            return GetResult<PageInputDto<TbmDiagnosisDto>, PageResultDto<TbmDiagnosisDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }



        public TbmDiagnosisDto GetById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, TbmDiagnosisDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void InsertDiagnosis(TbmDiagnosisDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteDiagnosis(EntityDto<Guid> input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<CusRegInfoDto> getIllCount(SearchItem intput)
        {
            return GetResult<SearchItem, List<CusRegInfoDto>>(intput, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}