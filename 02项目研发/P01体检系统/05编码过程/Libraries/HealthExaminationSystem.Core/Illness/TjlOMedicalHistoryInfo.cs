using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业健康问诊
    /// </summary>
    [Obsolete("暂停使用")]
    public class TjlOMedicalHistoryInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 月经初潮
        /// </summary>
        [MaxLength(64)]
        public virtual string Menarche { get; set; }

        /// <summary>
        /// 经期
        /// </summary>
        [MaxLength(32)]
        public virtual string Menstruation { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        [MaxLength(32)]
        public virtual string Cycle { get; set; }

        /// <summary>
        /// 停经年龄
        /// </summary>
        [MaxLength(32)]
        public virtual string MenopauseAge { get; set; }

        /// <summary>
        /// 现有子女
        /// </summary>
        [MaxLength(32)]
        public virtual string ExistenceChild { get; set; }

        /// <summary>
        /// 流产
        /// </summary>
        [MaxLength(32)]
        public virtual string Abortion { get; set; }

        /// <summary>
        /// 早产
        /// </summary>
        [MaxLength(32)]
        public virtual string Premature { get; set; }

        /// <summary>
        /// 死胎
        /// </summary>
        [MaxLength(32)]
        public virtual string Stillbirth { get; set; }

        /// <summary>
        /// 异常胎
        /// </summary>
        [MaxLength(32)]
        public virtual string AbnormalFetal { get; set; }

        /// <summary>
        /// 先天畸形
        /// </summary>
        [MaxLength(32)]
        public virtual string CongenitalMalformations { get; set; }

        /// <summary>
        /// 吸烟状态
        /// </summary>
        [MaxLength(32)]
        public virtual string SmokStatus { get; set; }

        /// <summary>
        /// 一天多少支
        /// </summary>
        [MaxLength(32)]
        public virtual string SmokAmount { get; set; }

        /// <summary>
        /// 吸烟时间 年
        /// </summary>
        [MaxLength(32)]
        public virtual string SmokTime { get; set; }

        /// <summary>
        /// 饮酒状态
        /// </summary>
        [MaxLength(32)]
        public virtual string DrinkStatus { get; set; }

        /// <summary>
        /// 一天多少ml
        /// </summary>
        [MaxLength(32)]
        public virtual string DrinkAmount { get; set; }

        /// <summary>
        /// 饮酒多少年
        /// </summary>
        [MaxLength(32)]
        public virtual string DrinkTime { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        [MaxLength(128)]
        public virtual string FamilyHistory { get; set; }

        /// <summary>
        /// 问诊史
        /// </summary>
        [MaxLength(256)]
        public virtual string Symptom { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [MaxLength(128)]
        public virtual string Sign { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}