using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sw.Hospital.HealthExaminationSystem.Core.Crisis
{
    /// <summary>
    /// 危急值设置记录表
    /// </summary>
    public class TjlCrisisSet : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 人员项目结果表
        /// </summary>
        public virtual TjlCustomerRegItem TjlCustomerRegItem { get; set; }
        /// <summary>
        /// 项目结果表id 外键
        /// </summary>
        [ForeignKey("TjlCustomerRegItem")]
        public virtual Guid TjlCustomerRegItemId { get; set; }
        /// <summary>
        /// 客户回访记录集合
        /// </summary>
        public virtual ICollection<TjlCustomerServiceCallBack> TjlCustomerServiceCallBacks { get; set; }
        /// <summary>
        /// 设置说明
        /// </summary>
        [StringLength(3072)]
        public virtual string SetNotice { get; set; }
        /// <summary>
        /// 通知消息状态 未发送0 已发送1 发送失败2
        /// </summary>
        public virtual int? MsgState { get; set; }
        /// <summary>
        /// 危急值类型 接口回传0 系统生成1 人为设置2
        /// </summary>
        public virtual int CrisisType { get; set; }
        /// <summary>
        /// 回访状态 否0 是1
        /// </summary>
        public virtual int CallBackState { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public int TenantId { get; set; }

    }
}
