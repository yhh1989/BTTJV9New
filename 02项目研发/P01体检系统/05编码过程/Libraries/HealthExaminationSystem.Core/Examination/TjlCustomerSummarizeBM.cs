using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检总检建议BM
    /// </summary>
    public class TjlCustomerSummarizeBM : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约id
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 建议id
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用!", true)]
        public virtual int? SummarizeBM { get; set; }

        /// <summary>
        /// 总检建议标识
        /// </summary>
        [ForeignKey("SummarizeAdvice")]
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual TbmSummarizeAdvice SummarizeAdvice { get; set; }

        /// <summary>
        /// 建议类别 1健康体检建议2职业健康建议3保健建议4专科建议
        /// </summary>
        public virtual int? SummarizeType { get; set; }

        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(128)]
        public virtual string SummarizeName { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        [StringLength(3072)]
        public virtual string Advice { get; set; }
        
        /// <summary>
        /// 建议顺序
        /// </summary>
        public virtual int? SummarizeOrderNum { get; set; }

        /// <summary>
        /// 是否隐私
        /// </summary>
        public virtual bool? IsPrivacy { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 父级建议
        /// </summary>
        public virtual Guid? ParentAdviceId { get; set; }
        /// <summary>
        /// 是否复查
        /// </summary>

        public int isReview { get; set; }


        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
        /// <summary>
        ///是否上传1是0否
        /// </summary>
        public virtual int? IsSC { get; set; }


    }
}