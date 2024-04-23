using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class GroupCustomerRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual GroupCustomerDto Customer { get; set; }
    }
}