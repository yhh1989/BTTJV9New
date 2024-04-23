using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{ 
    /// <summary>
    /// 复查回访表
    /// </summary>
    public class TjlCusReview : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 复查阳性
        /// </summary>
        [ForeignKey("SummarizeAdvice")]
        public virtual Guid? SummarizeAdviceId { get; set; }
        /// <summary>
        /// 复查阳性
        /// </summary>
        public virtual TbmSummarizeAdvice SummarizeAdvice { get; set; }

        /// <summary>
        /// 复查项目组合
        /// </summary>
       
        public virtual ICollection<TbmItemGroup> ItemGroup { get; set; }

        /// <summary>
        /// 复查时间
        /// </summary>
        public int ReviewDay { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// 复查备注
        /// </summary>
        public string Remart { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
