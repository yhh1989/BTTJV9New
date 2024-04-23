using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 既往史
    /// </summary>
    [Obsolete("暂停使用")]
    public class TjlOHistory : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约登记信息表 体检人预约登记信息表
        /// </summary>
        [Required]
        public virtual TjlCustomerReg CustomerReg { get; set; }

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

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}