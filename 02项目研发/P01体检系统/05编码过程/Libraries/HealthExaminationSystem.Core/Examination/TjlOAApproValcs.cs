using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
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
    /// 审批
    /// </summary>
  public   class TjlOAApproValcs : FullAuditedEntity<Guid>, IMustHaveTenant
    {

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        //[NotMapped]
        //[Obsolete("暂停使用", true)]
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        //[NotMapped]
        //[Obsolete("暂停使用", true)]
        public virtual TjlClientReg ClientReg { get; set; }


        /// <summary>
        /// 单位
        /// </summary>
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        [ForeignKey(nameof(ClientInfo))]
        public virtual Guid? ClientInfoId { get; set; }
        /// <summary>
        /// 表头编码
        /// </summary>
        [StringLength(500)]
        public virtual string TitleBM { get; set; }
        /// <summary>
        /// 表头名称
        /// </summary>
        [StringLength(500)]
        public virtual string TitleName { get; set; }
        /// <summary>
        /// 最高折扣
        /// </summary>

        public virtual Decimal? DiscountRate { get; set; }
        /// <summary>
        /// 加项最高折扣
        /// </summary>

        public virtual Decimal? AddDiscountRate { get; set; }


        /// <summary>
        /// 申请人
        /// </summary>
        [StringLength(50)]
        public virtual string Applicant { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>

        public virtual DateTime? AppliTime { get; set; }

        /// <状态>
        /// 审批状态 0未审批 1已审批2已拒绝
        /// </summary>

        public virtual int? AppliState { get; set; }

        /// <状态>
        /// 确认状态 0未确认1已确认
        /// </summary>
        public virtual int? OKState { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>

        public virtual DateTime? ApprovalTime { get; set; }


        /// <summary>
        /// 审批人Id
        /// </summary>
        [ForeignKey("ApprovalUser")]
        public virtual long? ApprovalUserId { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public virtual User ApprovalUser { get; set; }


        /// <summary>
        /// 抄送人Id
        /// </summary>
        [ForeignKey("CCUser")]
        public virtual long? CCUserId { get; set; }

        /// <summary>
        /// 抄送人人
        /// </summary>
        public virtual User CCUser { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 批示
        /// </summary>
        [StringLength(500)]
        public virtual string Comments { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
