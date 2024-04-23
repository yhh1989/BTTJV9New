using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlScheduling))]
#endif
    public class SchedulingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否团体
        /// </summary>
        public virtual bool IsTeam { get; set; }

        /// <summary>
        /// 体检人预约登记信息表外键
        /// </summary>
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约登记信息表
        /// </summary>
        public virtual SimpleCustomerRegDto CustomerReg { get; set; }

        /// <summary>
        /// 单位预约表外键
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约表
        /// </summary>
        public virtual SimpleClientRegDto ClientReg { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        [Required]
        public virtual DateTime? ScheduleDate { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public virtual int TotalNumber { get; set; }

        /// <summary>
        /// 男人数
        /// </summary>
        public virtual int? ManNumber { get; set; }

        /// <summary>
        /// 女人数
        /// </summary>
        public virtual int? WomanNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }
    }
}