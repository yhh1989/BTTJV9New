using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 放弃待查
    /// </summary>
    public class TjlCusGiveUp : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomer Customer { get; set; }
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }
        /// <summary>
        /// 体检人预约项目组主键
        /// </summary>
        [ForeignKey("CustomerItemGroup")]
        public virtual Guid? CustomerItemGroupId { get; set; }
        /// <summary>
        /// 体检人项目组合
        /// </summary>
        public virtual TjlCustomerItemGroup CustomerItemGroup { get; set; }

        /// <summary>
        /// 待查时间（下次检查的时间）
        /// </summary>
        public virtual DateTime? stayDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string remart { get; set; }
        /// <summary>
        /// 类型 1放弃 2待查
        /// </summary>
        public virtual int stayType { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
