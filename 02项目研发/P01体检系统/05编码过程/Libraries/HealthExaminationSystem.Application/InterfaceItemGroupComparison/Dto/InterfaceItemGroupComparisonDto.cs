using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO
{
   public  class InterfaceItemGroupComparisonDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室
        /// </summary>
        public virtual CreateDepartmentDto Department { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        [ForeignKey(nameof(Department))]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual CreateOrUpdateItemGroup ItemGroup { get; set; }

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
