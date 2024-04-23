using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 接口项目组合对应
    /// </summary>
    public class TdbInterfaceItemGroupComparison : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 科室
        /// </summary>
        public virtual TbmDepartment Department { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
       [ForeignKey(nameof(Department))]      
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual TbmItemGroup ItemGroup { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>
        [ForeignKey(nameof(ItemGroup))]
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 对应项目ID
        /// </summary>
        public virtual string ObverseItemId { get; set; }

        /// <summary>
        /// 对应项目名称?
        /// </summary>
        [MaxLength(64)]
        public virtual string ObverseItemName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1024)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 仪器型号
        /// </summary>
        [MaxLength(64)]
        public virtual string InstrumentModelNumber { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}