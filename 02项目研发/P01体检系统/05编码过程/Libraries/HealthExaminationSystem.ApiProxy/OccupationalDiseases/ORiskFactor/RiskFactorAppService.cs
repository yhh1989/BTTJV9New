using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.RiskFactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.ORiskFactor;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.ORiskFactor
{
    public class RiskFactorAppService : AppServiceApiProxyBase, IRiskFactorAppService
    {
        public void Delete(ORiskFactorDto input)
        {
            GetResult<ORiskFactorDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public ORiskFactorDto Edit(ORiskFactorDto input)
        {
            return GetResult<ORiskFactorDto, ORiskFactorDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ORiskFactorDto> Get(ORiskFactorDto input)
        {
            return GetResult<ORiskFactorDto, List<ORiskFactorDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<ORiskFactorDto> GetAll()
        {
            return GetResult< List<ORiskFactorDto>>( DynamicUriBuilder.GetAppSettingValue());
        }

        public bool Insert(ORiskFactorDto input)
        {
            return GetResult<ORiskFactorDto, bool>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}
