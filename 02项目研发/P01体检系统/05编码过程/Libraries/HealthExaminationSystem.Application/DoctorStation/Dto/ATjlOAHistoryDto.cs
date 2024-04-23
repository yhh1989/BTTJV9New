using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 工作历史
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlOAHistory))]
#endif
    public class ATjlOAHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(64)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(64)]
        public virtual string Workshop { get; set; }

        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(64)]
        public virtual string Work { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        [StringLength(64)]
        public virtual string RiskName { get; set; }

        /// <summary>
        /// 是否痊愈
        /// </summary>
        public virtual bool? Protect { get; set; }

        /// <summary>
        /// 防护措施
        /// </summary>
        [StringLength(64)]
        public virtual string Fhcs { get; set; }
        
    }
}