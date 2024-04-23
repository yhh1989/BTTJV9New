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
    /// <summary>
    /// 复合诊断
    /// </summary>
    public class TbmSummHB : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 复合诊断
        /// </summary>      
        public virtual Guid SummarizeAdviceId { get; set; }

        /// <summary>
        /// 复合诊断名称
        /// </summary>
        public virtual string AdviceName { get; set; }


        /// <summary>
        /// 诊断信息
        /// </summary>
        [InverseProperty(nameof(TbmSummarizeAdvice.SummHBs))]
        public virtual ICollection<TbmSummarizeAdvice>  Advices { get; set; }


        /// <inheritdoc />
        public int TenantId { get; set; }

    }
}
