using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 跟踪表
    /// </summary>
    //[Obsolete("暂停使用", true)]
    public class TjlCustomerReviewFollow : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        ///     预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        /// <summary>
        ///     跟踪时间
        /// </summary>
        public virtual DateTime? RDateTime { get; set; }

        /// <summary>
        ///     跟踪人
        /// </summary>
        public virtual User EmployeeBM { get; set; }

        /// <summary>
        ///     客户反馈
        /// </summary>
        [MaxLength(1024)]
        public virtual string ReSult { get; set; }

        /// <summary>
        ///     回访记录
        /// </summary>
        [MaxLength(1024)]
        public virtual string Explain { get; set; }

        /// <summary>
        ///     第几次回访
        /// </summary>
        public virtual int? Num { get; set; }

        /// <summary>
        ///     回访方式 1电话回访2qq回访
        /// </summary>
        public virtual int? reType { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}