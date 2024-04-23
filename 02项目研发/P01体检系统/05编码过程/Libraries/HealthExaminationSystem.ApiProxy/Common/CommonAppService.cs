using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common
{
    /// <summary>
    /// 公共应用服务
    /// </summary>
    public class CommonAppService : AppServiceApiProxyBase, ICommonAppService
    {
        /// <inheritdoc />
        public TimeDto GetDateTimeNow()
        {
            return GetResult<TimeDto>(DynamicUriBuilder.GetAppSettingValue());
        }
        /// <inheritdoc />
        public ChineseDto GetHansBrief(ChineseDto input)
        {
            return GetResult<ChineseDto, ChineseDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<AdministrativeDivisionDto> GetAdministrativeDivisions()
        {
            return GetResult<List<AdministrativeDivisionDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<ShowOpLogDto> SeachOpLog(PageInputDto<SearchOpLogDto> pinput)
        {
            return GetResult<PageInputDto<SearchOpLogDto>, PageResultDto<ShowOpLogDto>>(pinput, DynamicUriBuilder.GetAppSettingValue());
        }

        public CreateOpLogDto SaveOpLog(CreateOpLogDto input)
        {
            return GetResult<CreateOpLogDto, CreateOpLogDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}