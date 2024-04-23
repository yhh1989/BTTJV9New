using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport
{
   public interface IOccGroupReportAppService
#if !Proxy
        : IApplicationService
#endif
    {
     

        /// <summary>
        /// 获取目标疾病人数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<OccTargetCountDto> getTargetCount(InOcccCusIDDto input);
        /// <summary>
        /// 获取职业健康体检信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
//#if Application
//        Task<List<OccCustomerSumDto>> getOccCustomerSum(EntityDto<Guid> input);
//#elif Proxy
         List<OccCustomerSumDto> getOccCustomerSum(EntityDto<Guid> input);
//#endif
         List<OccCustomerItemDto> getCusItemResult(EntityDto<Guid> input);
        List<OccCustomerRegHazardSumDto> getCustomerRegHazardSum(EntityDto<Guid> input);
        

    }
}
