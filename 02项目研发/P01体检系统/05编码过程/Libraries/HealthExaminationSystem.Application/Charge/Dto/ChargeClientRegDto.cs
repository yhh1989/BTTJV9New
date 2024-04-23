using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
#if !Proxy
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class ChargeClientRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        [Required]
        public virtual ChargeClientDto ClientInfo { get; set; }

        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 预约次数
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 预约人数
        /// </summary>
        public virtual int? RegPersonCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(128)]
        public virtual string Remark { get; set; }
    }
}