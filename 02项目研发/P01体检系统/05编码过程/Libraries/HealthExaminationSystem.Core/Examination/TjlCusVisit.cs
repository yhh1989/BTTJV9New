using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 回访
    /// </summary>
  public  class TjlCusVisit : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        ///// <summary>
        ///// 体检人外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        ///// </summary>
        //[ForeignKey("Customer")]
        //public virtual Guid CustomerId { get; set; }

        ///// <summary>
        ///// 体检人
        ///// </summary>
        //public virtual TjlCustomer Customer { get; set; }
        /// <summary>
        /// 预约标识
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 预约
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 回访状态0未回访1已回访3已取消
        /// </summary>
        public virtual int? VisitState { get; set; }

        /// <summary>
        ///回访日期
        /// </summary>
        public virtual DateTime? VisitDate { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [MaxLength(32)]
        public virtual string colour  { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(32)]
        public virtual string remarks { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
