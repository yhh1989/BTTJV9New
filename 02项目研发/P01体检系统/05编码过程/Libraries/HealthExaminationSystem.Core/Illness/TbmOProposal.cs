using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Illness
{
    /// <summary>
    /// 职业结论字典
    /// </summary>
    public class TbmOProposal : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 职业结论类别 检查结论、处理意见、结论依据(诊断标准)、职业健康解释、职业健康禁忌证解释
        /// </summary>
        [MaxLength(16)]
        public virtual string DictionaryType { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        [MaxLength(64)]
        public virtual string TreatmentAdvice { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? Ordernum { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}