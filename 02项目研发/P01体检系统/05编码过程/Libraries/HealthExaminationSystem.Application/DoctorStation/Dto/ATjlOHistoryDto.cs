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
    /// 既往史
    /// 医生站读取患者信息用
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlOHistory))]
#endif
    public class ATjlOHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(64)]
        public virtual string ZyContents { get; set; }

        /// <summary>
        /// 诊断日期
        /// </summary>
        public virtual DateTime? ZddAte { get; set; }

        /// <summary>
        /// 诊断单位
        /// </summary>
        [StringLength(64)]
        public virtual string Zdclient { get; set; }

        /// <summary>
        /// 是否痊愈
        /// </summary>
        public virtual bool? Protect { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public virtual DateTime? LrdAte { get; set; }
        
    }
}