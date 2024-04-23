using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 危机值设置
    /// </summary>
   public  class TbmCriticalSet : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey(nameof(Department))]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual TbmDepartment Department { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>
        [ForeignKey(nameof(ItemInfo))]
        public virtual Guid ItemInfoId { get; set; }

        /// <summary>
        /// 组合
        /// </summary>
        public virtual TbmItemInfo ItemInfo { get; set; }


        /// <summary>
        /// 重要异常结果分类
        /// </summary>         
        public virtual int CriticalType { get; set; }

        /// <summary>
        /// 判断类型
        /// </summary>            
        public virtual int CalculationType { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>            
        public virtual int Operator { get; set; }

        /// <summary>
        /// 数值结果
        /// </summary>            
        public virtual decimal? ValueNum { get; set; }


        /// <summary>
        /// 诊断结果
        /// </summary>            
        [StringLength(640)]
        public virtual string ValueChar { get; set; }
        /// <summary>
        /// 性别
        /// </summary>            
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 往次
        /// </summary>           
        public virtual int? Old { get; set; }

        public virtual ICollection<TbmCriticalDetail> CriticalDetails { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
