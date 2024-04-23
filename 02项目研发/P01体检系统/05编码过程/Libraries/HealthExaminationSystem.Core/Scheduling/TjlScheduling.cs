using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Scheduling
{
    /// <summary>
    /// 排期记录表
    /// </summary>
    public class TjlScheduling : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 是否团体
        /// </summary>
        public virtual bool IsTeam { get; set; }

        ///// <summary>
        ///// 体检人预约登记信息表外键
        ///// </summary>
        //[Obsolete("暂停使用")]
        //[ForeignKey(nameof(CustomerReg))]
        //public virtual Guid? CustomerRegId { get; set; }

        ///// <summary>
        ///// 体检人预约登记信息表
        ///// </summary>
        //[Obsolete("暂停使用")]
        //public virtual TjlCustomerReg CustomerReg { get; set; }

        ///// <summary>
        ///// 单位预约表外键
        ///// </summary>
        //[Obsolete("暂停使用")]
        //[ForeignKey(nameof(ClientReg))]
        //public virtual Guid? ClientRegId { get; set; }

        ///// <summary>
        ///// 单位预约表
        ///// </summary>
        //[Obsolete("暂停使用")]
        //public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        [Required]
        public virtual DateTime ScheduleDate { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public virtual int TotalNumber { get; set; }

        /// <summary>
        /// 男性人数
        /// </summary>
        [Obsolete("暂停使用")]
        public virtual int? ManNumber { get; set; }

        /// <summary>
        /// 女人数
        /// </summary>
        [Obsolete("暂停使用")]
        public virtual int? WomanNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 单位信息表示
        /// </summary>
        [ForeignKey(nameof(ClientInfo))]
        public Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        [MaxLength(64)]
        public string Introducer { get; set; }

        /// <summary>
        /// 个人名称
        /// </summary>
        [MaxLength(64)]
        public string PersonalName { get; set; }

        /// <summary>
        /// 项目组合信息
        /// </summary>
        [InverseProperty(nameof(TbmItemGroup.Schedulings))]
        public virtual ICollection<TbmItemGroup> ItemGroups { get; set; }

        /// <summary>
        /// 项目组合信息
        /// </summary>
        [InverseProperty(nameof(TbmItemSuit.Schedulings))]
        public virtual ICollection<TbmItemSuit> ItemSuits { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 时段
        /// </summary>
        [MaxLength(8)]
        public string TimeFrame { get; set; }
    }
}