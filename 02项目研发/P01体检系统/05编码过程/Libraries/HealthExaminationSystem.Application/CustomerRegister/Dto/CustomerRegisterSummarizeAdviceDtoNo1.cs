using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto
{
    /// <summary>
    /// 体检人预约总检建议数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Examination.TjlCustomerSummarizeBM))]
#endif
    public class CustomerRegisterSummarizeAdviceDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
        public virtual Guid CustomerRegID { get; set; }

        /// <summary>
        /// 总检建议标识
        /// </summary>
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 建议类别 1健康体检建议2职业病建议3保健建议4专科建议
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
    }
}