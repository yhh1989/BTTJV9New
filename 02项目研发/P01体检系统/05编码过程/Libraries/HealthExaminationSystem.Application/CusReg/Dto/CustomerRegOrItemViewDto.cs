using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 体检人预约登记信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegOrItemViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分组id
        /// </summary>
        public virtual Guid ClientTeamInfo_Id { get; set; }
        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual ICollection<TjlCustomerItemGroupDto> CustomerItemGroup { get; set; }
    }
}