using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Crisis
{
    public class TjlCrisVisit : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 危急值记录
        /// </summary>
        public TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 预约id
        /// </summary>
        [ForeignKey("CustomerReg")]
        public Guid CustomerRegId { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>
        [StringLength(3072)]
        public string CallBacKContent { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? CallBacKDate { get; set; }


        /// <summary>
        /// 处理人
        /// </summary>
        public virtual User CallBacKUser { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        [ForeignKey(nameof(CallBacKUser))]
        public virtual long? CallBacKUserId { get; set; }
        /// <summary>
        /// 审核内容
        /// </summary>
        [StringLength(3072)]
        public string SHContent { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? SHDate { get; set; }


        /// <summary>
        /// 审核人
        /// </summary>
        public virtual User SHUser { get; set; }

        /// <summary>
        /// 审核人标识
        /// </summary>
        [ForeignKey(nameof(SHUser))]
        public virtual long? SHUserId { get; set; }


        /// <summary>
        /// 回访状态 处理1 审核2
        /// </summary>
        public int? CallBackState { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public int TenantId { get; set; }

    }
}
