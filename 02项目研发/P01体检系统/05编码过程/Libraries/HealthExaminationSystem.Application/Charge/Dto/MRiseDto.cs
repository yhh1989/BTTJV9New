using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 发票抬头
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlMRise))]
#endif
    public class MRiseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 抬头
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 税号
        /// </summary>
        [StringLength(64)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        public int TenantId { get; set; }
    }
}