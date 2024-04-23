using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{ 
#if !Proxy
    [AutoMap(typeof(TbmSummarizeAdvice))]
#endif
    public class SimpleSummarizeAdviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Uid { get; set; }

        /// <summary>
        /// 建议名称
        /// </summary>
        [StringLength(1024)]
        public virtual string AdviceName { get; set; }

        /// <summary>
        /// 输入码
        /// </summary>
        [StringLength(1024)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}