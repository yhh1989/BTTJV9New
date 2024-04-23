using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 发票管理
    /// </summary>
    public class TbmMReceiptManager : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [ForeignKey("User")]
        public virtual long? UserId { get; set; }
        /// <summary>
        /// 领票id
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? SerialNumber { get; set; }

        /// <summary>
        /// 开始号
        /// </summary>
        public virtual int? StratCard { get; set; }

        /// <summary>
        /// 结束号
        /// </summary>
        public virtual int? EndCard { get; set; }

        /// <summary>
        /// 当前号
        /// </summary>
        public virtual int? NowCard { get; set; }

        /// <summary>
        /// 类别:1收据2发票
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 状态:1启用2停止
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(64)]
        public virtual string Remarks { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}