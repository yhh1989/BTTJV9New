using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
   public  class TbmReSultDepart : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey("ReSultSet")]
        public virtual Guid ReSultSetId { get; set; }
        

        /// <summary>
        /// 科室表
        /// </summary>
        public virtual TbmReSultSet ReSultSet { get; set; }

        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey("Department")]
        public virtual Guid DepartmentId { get; set; }

        public string DepartName { get; set; }

        /// <summary>
        /// 科室表
        /// </summary>
        public virtual TbmDepartment Department { get; set; }
        /// <summary>
        /// 查询类型 1项目 2组合小结3科室小结
        /// </summary>
        public int ShowType { get; set; }
        public int TenantId { get; set; }
        //public int TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
