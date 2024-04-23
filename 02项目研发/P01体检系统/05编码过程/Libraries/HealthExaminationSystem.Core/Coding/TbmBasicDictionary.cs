using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 基本字典，仅键值对的字典表
    /// </summary>
    public class TbmBasicDictionary : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual int Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(64)]
        public virtual string Code { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        [StringLength(64)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string helpChar { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }


        /// <inheritdoc />
        public virtual int TenantId { get; set; }
    }
}