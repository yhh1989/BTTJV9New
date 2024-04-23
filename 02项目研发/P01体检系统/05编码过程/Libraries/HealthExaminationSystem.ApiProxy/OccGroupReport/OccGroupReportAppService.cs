using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.OccGroupReport
{
    public class OccGroupReportAppService : AppServiceApiProxyBase, IOccGroupReportAppService
    {
        /// <summary>
        /// 获取职业健康体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccCustomerSumDto> getOccCustomerSum(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<OccCustomerSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OccCustomerRegHazardSumDto> getCustomerRegHazardSum(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<OccCustomerRegHazardSumDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<OccTargetCountDto> getTargetCount(InOcccCusIDDto input)
        {
            return GetResult<EntityDto<Guid>, List<OccTargetCountDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<OccCustomerItemDto> getCusItemResult(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, List<OccCustomerItemDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

    }
}
