using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 复合判断设置明细
    /// </summary>
    public class TbmDiagnosisData : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 复合判断主目id
        /// </summary>
        [ForeignKey("Diagnosis")]
        public virtual Guid? DiagnosisId { get; set; }
        /// <summary>
        /// 复合判断ID
        /// </summary>
        public virtual TbmDiagnosis Diagnosis { get; set; }

        /// <summary>
        /// 项目编码id
        /// </summary>
        [ForeignKey("ItemInfo")]
        public virtual Guid? ItemInfoId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual TbmItemInfo ItemInfo { get; set; }

        /// <summary>
        /// 项目标准值:用于文本型项目
        /// </summary>
        [StringLength(128)]
        public virtual string ItemStandard { get; set; }

        /// <summary>
        /// 仪器Id:用于文本型项目
        /// </summary>
        [StringLength(128)]
        public virtual string InstrumentId { get; set; }

        /// <summary>
        /// 项目下限:用于数值型项目
        /// </summary>
        public virtual decimal? minValue { get; set; }

        /// <summary>
        /// 项目上限:用于数值型项目
        /// </summary>
        public virtual decimal? maxValue { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 项目类型:1说明型2数值型
        /// </summary>
        public virtual int? ItemType { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}