using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 复查人员
    /// </summary>
    public class TjlCustomerReview : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }


        /// <summary>
        /// 回访状态 1未回访2已回访
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 复查状态 1未复查2已复查
        /// </summary>
        public virtual int? ReSate { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(32)]
        public virtual string Tell { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(256)]
        public virtual string Adress { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(32)]
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>
        /// 体检状态 1体检中2体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2初步总检3总检审核
        /// </summary>
        public virtual int? SummState { get; set; }

        /// <summary>
        /// 报告状态 1未打印2已打印2未领取4已领取
        /// </summary>
        public virtual int? PrintState { get; set; }

        /// <summary>
        /// 预约复查时间
        /// </summary>
        public virtual DateTime? BespeakDate { get; set; }

        /// <summary>
        /// 到店复查时间
        /// </summary>
        public virtual DateTime? BespeakTime { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}