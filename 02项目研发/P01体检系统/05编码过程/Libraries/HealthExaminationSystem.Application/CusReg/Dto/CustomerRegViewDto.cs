using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
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
    public class CustomerRegViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检ID
        /// </summary>
        public virtual string CustomerBM { get; set; }
        /// <summary>
        ///单位信息
        /// </summary>
        public virtual ClientInfosNameDto ClientInfo { get; set; }
        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual ClientRegDto ClientReg { get; set; }
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerEssentialInfoViewDto Customer { get; set; }
    }
}