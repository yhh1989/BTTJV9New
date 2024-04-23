using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sw.Hospital.HealthExaminationSystem.Core.Crisis
{
    /// <summary>
    /// 客户回访记录表
    /// </summary>
    public class TjlCustomerServiceCallBack : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 危急值记录
        /// </summary>
        public TjlCrisisSet TjlCrisisSet { get; set; }
        /// <summary>
        /// 危急值记录Id
        /// </summary>
        [ForeignKey("TjlCrisisSet")]
        public Guid TjlCrisisSetId { get; set; }
        /// <summary>
        /// 回访方式 微信0 电话1 短信2 到店3
        /// </summary>
        public int CallBackType { get; set; }
        /// <summary>
        /// 回访内容
        /// </summary>
        [StringLength(3072)]
        public string CallBacKContent { get; set; }
        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime CallBacKDate { get; set; }
        /// <summary>
        /// 回访状态 完成0 关闭1
        /// </summary>
        public int? CallBackState { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public int TenantId { get; set; }

    }
}
