
using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease;
using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccNationalDisease
{
    public class OccNationalDiseaseAppService : AppServiceApiProxyBase, IOccNationalDiseaseAppService
    {
        public DataNodeDto GetEnterpriseInfo(InSearchDto input)
        {
            return GetResult<InSearchDto, DataNodeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public XMLDataNodeDto GetExamRecord(InSearchDto input)
        {
            return GetResult<InSearchDto, XMLDataNodeDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CreateCountrySet SaveCountry(CreateCountrySet input)
        {
            return GetResult<CreateCountrySet, CreateCountrySet>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public CreateCountrySet GetCountry()
        {
            return GetResult<CreateCountrySet>( DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
