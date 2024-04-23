using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 项目字典
    /// </summary>
    public class TbmItemDictionary : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 字典内容
        /// </summary>
        [StringLength(1024)]
        public virtual string Word { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(256)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(32)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 科室ID外键
        /// </summary>
        [ForeignKey("DepartmentBM")]
        public virtual Guid? DepartmentBMId { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 项目ID外键
        /// </summary>
        [ForeignKey("iteminfoBM")]
        public virtual Guid? iteminfoBMId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual TbmItemInfo iteminfoBM { get; set; }

        /// <summary>
        /// 是否疾病 1重大疾病2一般疾病3阳性发现
        /// </summary>
        public virtual int? IsSickness { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 是否常用 1常用2正常
        /// </summary>
        public virtual int? ApplySate { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        [StringLength(128)]
        public virtual string Period { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 公卫编码
        /// </summary>     
        [MaxLength(64)]
        public virtual string GWBM { get; set; }
    }
}