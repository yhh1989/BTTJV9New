using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Abp.AutoMapper;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegSimpleViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
       

        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 错误原因
        /// </summary>
        public virtual string errorinfo { get; set; }

        /// <summary>
        /// 复查状态 1正常2复查3回访
        /// </summary>
        public virtual int? ReviewSate { get; set; }

    }
}
