using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 总检建议记录表
    /// </summary>
#if !Proxy 
    [AutoMap(typeof(TjlCustomerSummarizeBM))]
#endif
    public class TjlCustomerSummarizeBMDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual Guid CustomerRegID { get; set; }
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerRegForInspectionTotalDto CustomerReg { get; set; }

        /// <summary>
        /// 总检建议标识
        /// </summary>
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual FullSummarizeAdviceDto SummarizeAdvice { get; set; }

        /// <summary>
        /// 建议类别 1健康体检建议2职业健康建议3保健建议4专科建议
        /// </summary>
        public virtual int? SummarizeType { get; set; }

        /// <summary>
        /// 建议名称
        /// </summary>
        public virtual string SummarizeName { get; set; }

        /// <summary>
        /// 建议内容
        /// </summary>
        public virtual string Advice { get; set; }

        /// <summary>
        /// 建议顺序
        /// </summary>
        public virtual int? SummarizeOrderNum { get; set; }


        /// <summary>
        /// 父级建议
        /// </summary>
        public virtual Guid? ParentAdviceId { get; set; }

        /// <summary>
        /// 是否复查
        /// </summary>

        public int isReview { get; set; }

        /// <summary>
        /// 是否隐私 
        /// </summary>
        public virtual bool? IsPrivacy { get; set; }

        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }

        /// <summary>
        ///是否上传1是0否
        /// </summary>
        public virtual int? IsSC { get; set; }
    }
}